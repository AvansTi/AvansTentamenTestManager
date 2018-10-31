package com.avans.tentamenmanager;


import com.avans.tentamenmanager.data.Student;
import com.avans.tentamenmanager.events.EventManager;
import com.avans.tentamenmanager.events.OnPathScanned;
import com.avans.tentamenmanager.events.OnTestCompleted;
import fi.helsinki.cs.tmc.edutestutils.Points;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;
import org.junit.Ignore;
import org.junit.Test;
import org.junit.runner.JUnitCore;
import org.junit.runner.Request;
import org.junit.runner.Result;

import javax.tools.*;
import java.io.File;
import java.io.IOException;
import java.lang.annotation.Annotation;
import java.lang.reflect.Field;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLClassLoader;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.time.LocalDateTime;
import java.util.*;
import java.lang.reflect.Method;


public class TestManager extends EventManager {
    private ArrayList<Student> students = new ArrayList<>();
    private Path path;


    public void setPath(String path) {
        this.path = Paths.get(path);
        scanDir(this.path);
        this.<OnPathScanned>trigger(e -> e.onPathScanned(students));
    }

    private void scanDir(Path path)
    {
        try {
            Files.list(path).forEach(file ->
            {
                if(file.toString().endsWith(".iml")) {
									students.add(new Student(this.path.relativize(file), this.path));
									return; // don't look any deeper
								}
            });
						Files.list(path).forEach(file ->
						{
							if (Files.isDirectory(file))
								scanDir(file);
						});

				} catch (IOException e) {
            e.printStackTrace();
        }
    }


	public void runAllTests() {
    	students.forEach(student -> runTest(student));
	}



	public void runTest(Student student) {
		Path projectDir = path.resolve(student.getPath()).getParent();

		JSONArray log = new JSONArray();

		try {
			if(Files.exists(projectDir.resolve("log.json")))
				log = (JSONArray)new JSONParser().parse(Files.newBufferedReader(projectDir.resolve("log.json")));
		} catch (IOException e) {
			e.printStackTrace();
		} catch (ParseException e) {
			e.printStackTrace();
		}

		JSONObject currentLog = new JSONObject();
		currentLog.put("time", LocalDateTime.now().toString());

		Path src = projectDir.resolve("src");
		Path test = path.resolve("tests");
		Path lib = path.resolve("lib");

		Path out = projectDir.resolve("finalOut");
		try {
			if(!Files.exists(out))
				Files.createDirectory(out);
		} catch (IOException e) {
		}

		try {
			Files.list(out).forEach(f -> {
				try {
					Files.delete(f);
				} catch (IOException e) {
					e.printStackTrace();
				}
			});
		} catch (IOException e) {
			e.printStackTrace();
		}

		currentLog.put("compile", new JSONObject());
		JSONObject compileLog = ((JSONObject)currentLog.get("compile"));

		try {
			Files.list(src).filter(file -> file.toString().endsWith(".java")).forEach(file ->	compileLog.put(file.toString(), compile(file.toString(), src, out, lib)));
			Files.list(test).filter(file -> file.toString().endsWith(".java")).forEach(file ->	compileLog.put(file.toString(), compile(file.toString(), src, out, lib)));
		} catch (IOException e) {
			e.printStackTrace();
		}

		URL url = null;
		try {
			url = out.toUri().toURL();
		} catch (MalformedURLException e) {
			e.printStackTrace();
		}

		ClassLoader systemClassloader = ClassLoader.getSystemClassLoader();
		final URLClassLoader urlClassLoader = new URLClassLoader(new URL[]{url}, systemClassloader);

		//DIT IS VIES EN ZORGT ERVOOR DAT DIT NIET MULTITHEADED KAN WERKEN
		//Er is ook nog iets als Thread.currentThread().getContextClassLoader() , maar deze lijkt niets te doen
		try {
			Field scl = ClassLoader.class.getDeclaredField("scl");
			scl.setAccessible(true); // Set accessible
			scl.set(null, urlClassLoader); // Update it to your class loader

		} catch (NoSuchFieldException e) {
			e.printStackTrace();
		} catch (IllegalAccessException e) {
			e.printStackTrace();
		}


		class TotalResult
		{
			int count = 0;
			int score = 0;
			int failed = 0;
			int passed = 0;

			Map<String, Integer> scorePerClass = new HashMap<>();
			Map<String, String> errorLog = new HashMap<>();
		}

		final TotalResult totalResult = new TotalResult();


		try {
			JUnitCore junit = new JUnitCore();

			Files.list(out).filter(file -> file.getFileName().toString().contains(".class")).forEach(file ->
			{
				try {
					String className = file.getFileName().toString();
					className = className.substring(0, className.indexOf(".class"));

					boolean hasTests = false;
					Class aClass = urlClassLoader.loadClass(className);
					for (Method method : aClass.getDeclaredMethods()) {
						// Check for @Test, @Ignore and @Points
						if (method.isAnnotationPresent(Test.class)) {
							hasTests = true;
							if (method.isAnnotationPresent(Ignore.class)) {
							} else {
								if (method.isAnnotationPresent(Points.class)) {
									Annotation annotation = method.getAnnotation(Points.class);
									Points points = (Points) annotation;

									try {
										Result result = junit.run(Request.method(aClass, method.getName()));
										if (result.wasSuccessful()) {
											totalResult.score += Integer.parseInt(points.value());
											if(!totalResult.scorePerClass.containsKey(className))
												totalResult.scorePerClass.put(className, 0);
											totalResult.scorePerClass.put(className, totalResult.scorePerClass.get(className) + Integer.parseInt(points.value()));
											totalResult.passed++;
										} else {
											totalResult.errorLog.put(aClass.getSimpleName() + "." + method.getName(), result.getFailures().toString());
											totalResult.failed++;
										}
									}catch(Exception e)
									{
										e.printStackTrace();
									}
								}
							}
						}
					}
					if(hasTests && !totalResult.scorePerClass.containsKey(className))
						totalResult.scorePerClass.put(className, 0);

				} catch (ClassNotFoundException e) {
					e.printStackTrace();
				}
			});
		}catch (IOException e) {
			e.printStackTrace();
		}


		JSONObject testResults = new JSONObject();

		testResults.put("count", totalResult.count);
		testResults.put("passed", totalResult.passed);
		testResults.put("failed", totalResult.failed);
		testResults.put("score", totalResult.score);
		JSONObject perTest = new JSONObject();
		testResults.put("scores", perTest);
		totalResult.scorePerClass.forEach((ex, score) -> perTest.put(ex, score));

		JSONObject errors = new JSONObject();
		testResults.put("errors", errors);
		totalResult.errorLog.forEach((ex, error) -> errors.put(ex, error));

		currentLog.put("test", testResults);

		try {
			Field scl = ClassLoader.class.getDeclaredField("scl");
			scl.setAccessible(true); // Set accessible
			scl.set(null, systemClassloader); // Update it to your class loader

		} catch (NoSuchFieldException e) {
			e.printStackTrace();
		} catch (IllegalAccessException e) {
			e.printStackTrace();
		}




		try {
			log.add(currentLog);
			Files.write(projectDir.resolve("log.json"), log.toString().getBytes());
		} catch (IOException e) {
			e.printStackTrace();
		}

		this.<OnTestCompleted>trigger(e -> e.onTestCompleted(student));
	}


	private String compile(String file, Path src, Path out, Path lib)
	{
		System.out.println("Compiling " + file);
		JavaCompiler compiler = ToolProvider.getSystemJavaCompiler();
		DiagnosticCollector<JavaFileObject> diagnostics = new DiagnosticCollector<JavaFileObject>();
		StandardJavaFileManager fileManager = compiler.getStandardFileManager(diagnostics, null, null);

		//Iterable<? extends JavaFileObject> compilationUnits = fileManager.getJavaFileObjectsFromFiles(Arrays.asList(javaFiles));
		Iterable<? extends JavaFileObject> compilationUnits = fileManager.getJavaFileObjectsFromStrings(Arrays.asList(new String[] { file }));

		String classpath = lib.toString()  + "\\*" + ";" + src.toString();

		List<String> optionList = new ArrayList<String>();

		try {
			fileManager.setLocation(StandardLocation.CLASS_OUTPUT, Arrays.asList(new File[] { out.toFile() }));

			fileManager.setLocation(StandardLocation.CLASS_PATH, Arrays.asList(new File[] {
				lib.resolve("junit-4.11.jar").toFile(),
				lib.resolve("hamcrest-core-1.3.jar").toFile(),
				lib.resolve("edu-test-utils-0.4.2.jar").toFile(),
				out.toFile(),
				src.toFile()}));
		} catch (IOException e) {
			e.printStackTrace();
		}
		JavaCompiler.CompilationTask task = compiler.getTask(null, fileManager, diagnostics, optionList, null, compilationUnits);
		if(!task.call())
		{
			return "Compile error: \n" + diagnostics.getDiagnostics();
		}

		return "Compile success";
	}


}
