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
//import sun.net.www.protocol.file.FileURLConnection;

import javax.tools.*;
import java.io.File;
import java.io.IOException;
import java.lang.annotation.Annotation;
import java.lang.reflect.Field;
import java.lang.reflect.Method;
import java.net.*;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.time.LocalDateTime;
import java.util.*;
import java.util.stream.Collectors;

public class RunTest {
	public static void main(String[] args)
	{
		runTest(Paths.get(args[1]), Paths.get(args[0]));
	}

	private static int getVersion() {
		String version = System.getProperty("java.version");
		if(version.startsWith("1.")) {
			version = version.substring(2, 3);
		} else {
			int dot = version.indexOf(".");
			if(dot != -1) { version = version.substring(0, dot); }
		} return Integer.parseInt(version);
	}

	public static void runTest(Path projectDir, Path path) {

		JSONArray log = new JSONArray();

		try {
			if (Files.exists(projectDir.resolve("log.json")))
				log = (JSONArray) new JSONParser().parse(Files.newBufferedReader(projectDir.resolve("log.json")));
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
			if (!Files.exists(out))
				Files.createDirectory(out);
		} catch (IOException e) {
		}

		try {
			Files.walk(out).forEach(f -> {
				try {
					if(Files.isRegularFile(f))
						Files.delete(f);
				} catch (IOException e) {
					e.printStackTrace();
				}
			});
		} catch (IOException e) {
			e.printStackTrace();
		}

		currentLog.put("compile", new JSONObject());
		JSONObject compileLog = ((JSONObject) currentLog.get("compile"));

		try {
			Files.walk(src).filter(file -> file.toString().endsWith(".java")).forEach(file -> compileLog.put(file.toString(), compile(file.toString(), src, test, out, lib)));
			Files.walk(test).filter(file -> file.toString().endsWith(".java")).forEach(file -> compileLog.put(file.toString(), compile(file.toString(), src, test, out, lib)));
		} catch (IOException e) {
			e.printStackTrace();
		}

		URL url = null;
		try {
			url = out.toUri().toURL();
		} catch (MalformedURLException e) {
			e.printStackTrace();
		}

		if(MyClassLoader.instance != null)
			MyClassLoader.instance.addURL(url);


		URLClassLoader urlClassLoader;
		ClassLoader systemClassloader = ClassLoader.getSystemClassLoader();
		if(getVersion() <= 8) {
			urlClassLoader = new URLClassLoader(new URL[]{url}, systemClassloader);

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
		}
		else
			urlClassLoader = MyClassLoader.instance;

		class TotalResult {
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
				System.out.print("Testing " + file.getFileName().toString());
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
											System.out.print(" ok");
											totalResult.score += Integer.parseInt(points.value());
											if (!totalResult.scorePerClass.containsKey(className))
												totalResult.scorePerClass.put(className, 0);
											totalResult.scorePerClass.put(className, totalResult.scorePerClass.get(className) + Integer.parseInt(points.value()));
											totalResult.passed++;
										} else {
											System.out.print(" fail");
											System.out.println(result.getFailures().toString());
											totalResult.errorLog.put(aClass.getSimpleName() + "." + method.getName(), result.getFailures().toString());
											totalResult.failed++;
										}
									} catch (Exception e) {
										e.printStackTrace();
									}
								}
							}
						}
					}
					if (hasTests && !totalResult.scorePerClass.containsKey(className))
						totalResult.scorePerClass.put(className, 0);

				} catch (Exception e) {
					System.out.println("Class not found...");
					e.printStackTrace();
					System.out.println("---");
				}
				System.out.println("");
			});
		} catch (Exception e) {
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

		int studentId = getStudentId(projectDir);
		System.out.println("Found student ID " + studentId);
		currentLog.put("studentid", studentId);

		if(getVersion() <= 8) {
			try {
				Field scl = ClassLoader.class.getDeclaredField("scl");
				scl.setAccessible(true); // Set accessible
				scl.set(null, systemClassloader); // Update it to your class loader

			} catch (NoSuchFieldException e) {
				e.printStackTrace();
			} catch (IllegalAccessException e) {
				e.printStackTrace();
			}
		}


		try {
			log.add(currentLog);
			Files.write(projectDir.resolve("log.json"), log.toString().getBytes());
		} catch (IOException e) {
			e.printStackTrace();
		}

	}


	private static String compile(String file, Path src, Path test, Path out, Path lib) {
		System.out.println("Compiling " + file);
		JavaCompiler compiler = ToolProvider.getSystemJavaCompiler();
		DiagnosticCollector<JavaFileObject> diagnostics = new DiagnosticCollector<JavaFileObject>();
		StandardJavaFileManager fileManager = compiler.getStandardFileManager(diagnostics, null, null);

		//Iterable<? extends JavaFileObject> compilationUnits = fileManager.getJavaFileObjectsFromFiles(Arrays.asList(javaFiles));
		Iterable<? extends JavaFileObject> compilationUnits = fileManager.getJavaFileObjectsFromStrings(Arrays.asList(new String[]{file}));

		String classpath = lib.toString() + "\\*" + ";" + src.toString();

		List<String> optionList = new ArrayList<String>();

		try {
			fileManager.setLocation(StandardLocation.CLASS_OUTPUT, Arrays.asList(new File[]{out.toFile()}));

			List<File> cp = Files.list(lib).map(e -> e.toFile()).collect(Collectors.toList());
			cp.add(out.toFile());
			cp.add(test.toFile());
			cp.add(src.toFile());

			fileManager.setLocation(StandardLocation.CLASS_PATH, cp);
		} catch (IOException e) {
			e.printStackTrace();
		}
		JavaCompiler.CompilationTask task = compiler.getTask(null, fileManager, diagnostics, optionList, null, compilationUnits);
		if (!task.call()) {
			return "Compile error: \n" + diagnostics.getDiagnostics();
		}

		return "Compile success";
	}





	public static int getStudentId(Path projectDir)
	{
		Path out = projectDir.resolve("finalOut");

		URL url = null;
		try {
			url = out.toUri().toURL();
		} catch (MalformedURLException e) {
			e.printStackTrace();
		}

		ClassLoader systemClassloader = ClassLoader.getSystemClassLoader();
		final URLClassLoader urlClassLoader = new URLClassLoader(new URL[]{url}, systemClassloader);

		try {
			Class main = urlClassLoader.loadClass("Main");
			main.getDeclaredField("studentId").setAccessible(true);
			return main.getField("studentId").getInt(null);
		} catch (Exception e) {
//			e.printStackTrace();
		}catch (Error e) {
//			e.printStackTrace();
		}
		try {
			Class main = urlClassLoader.loadClass("StudentNummer");
			main.getDeclaredField("studentNummer").setAccessible(true);
			return main.getField("studentNummer").getInt(null);
		} catch (Exception e) {
			//		e.printStackTrace();
		}catch (Error e) {
//			e.printStackTrace();
		}
		try {
			Class main = urlClassLoader.loadClass("avans.ogp2.StudentNummer");
			main.getDeclaredField("studentNummer").setAccessible(true);
			return Integer.parseInt((String)main.getField("studentNummer").get(null));
		} catch (Exception e) {
			//		e.printStackTrace();
		}catch (Error e) {
//			e.printStackTrace();
		}

		try
		{
			final ArrayList<String> classes = new ArrayList<>();
			Enumeration<URL> resources = urlClassLoader.getResources("");
			URLConnection connection;

			for (URL u = null; resources.hasMoreElements() && ((u = resources.nextElement()) != null);) {
				connection = u.openConnection();
				if (connection instanceof JarURLConnection) {
				//	checkJarFile((JarURLConnection) connection, "", classes, recursive);
				} else if (connection instanceof sun.net.www.protocol.file.FileURLConnection) {
					checkDirectory(new File(URLDecoder.decode(url.getPath(),"UTF-8")), "", classes);
				}
			}

			for(String c : classes)
			{
				if(c.endsWith("StudentNummer") || c.endsWith("StudentNumber")) {
					try {
						Class main = urlClassLoader.loadClass(c);
						main.getDeclaredField("studentNummer").setAccessible(true);
						return main.getField("studentNummer").getInt(null);
					} catch (Exception e) {
						e.printStackTrace();
					}
				}
			}


		}catch(java.io.IOException e) {

		}

		return 0;
	}

	private static void checkDirectory(File directory, String pckgname, ArrayList<String> classes) {
		File tmpDirectory;

		if (directory.exists() && directory.isDirectory()) {
			final String[] files = directory.list();

			for (final String file : files) {
				if (file.endsWith(".class")) {
					try {
						if(pckgname.equals(""))
							classes.add(file.substring(0, file.length() - 6));
						else
							classes.add(pckgname + '.' + file.substring(0, file.length() - 6));
					} catch (final NoClassDefFoundError e) {
						// do nothing. this class hasn't been found by the
						// loader, and we don't care.
					}
				} else if ((tmpDirectory = new File(directory, file)).isDirectory()) {
					if(pckgname.equals(""))
						checkDirectory(tmpDirectory, file, classes);
					else
						checkDirectory(tmpDirectory, pckgname + "." +file, classes);

				}
			}
		}
	}

}
