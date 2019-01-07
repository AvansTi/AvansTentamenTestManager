package com.avans.tentamenmanager.data;

import com.avans.tentamenmanager.TestManager;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLClassLoader;
import java.nio.file.Files;
import java.nio.file.Path;
import java.time.LocalDateTime;

public class Student {
	private Path path;
	private Path absolutePath;
	private String name;
	private String username;
	private Status status = Status.Untested;
	private int studentId;

	public Student(Path projectFile, Path workDir) {
		path = projectFile;
		absolutePath = workDir.resolve(projectFile);
		username = parsePathForUsername(projectFile);


		updateStatus();
	}

	public void updateStatus() {
		Path projectDir = absolutePath.getParent();

		status = Status.Untested;

		if (Files.exists(projectDir.resolve("log.json"))) {
			JSONArray log = getLog();

			JSONObject compileLog = (JSONObject) ((JSONObject) log.get(log.size() - 1)).get("compile");
			status = Status.Ok;
			compileLog.forEach((file, result) ->
			{
				if (((String) result).contains("error"))
					status = Status.Error;
			});
		}


	}

	public int getStudentId()
	{
		Path projectDir = absolutePath.getParent();

		Path src = projectDir.resolve("src");
		Path test = path.resolve("tests");
		Path lib = path.resolve("lib");

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
		} catch (ClassNotFoundException e) {
//			e.printStackTrace();
		} catch (IllegalAccessException e) {
//			e.printStackTrace();
		} catch (NoSuchFieldException e) {
//			e.printStackTrace();
		}
		try {
			Class main = urlClassLoader.loadClass("StudentNummer");
			main.getDeclaredField("studentNummer").setAccessible(true);
			return main.getField("studentNummer").getInt(null);
		} catch (ClassNotFoundException e) {
	//		e.printStackTrace();
		} catch (IllegalAccessException e) {
	//		e.printStackTrace();
		} catch (NoSuchFieldException e) {
	//		e.printStackTrace();
		}
		System.err.println("Could not find student number for " + this.username);
		return 0;
	}



	private String parsePathForUsername(Path projectFile) {
		Path rootDir = projectFile;
		while (rootDir.getParent() != null)
			rootDir = rootDir.getParent();

		String username = rootDir.toString();
		username = username.substring(username.indexOf("_") + 1);
		username = username.substring(0, username.indexOf("_"));
		return username;
	}

	public String getUsername() {
		return username;
	}

	public String getName() {
		return name;
	}

	public Status getStatus() {
		return status;
	}

	public Path getPath() {
		return path;
	}

	public JSONArray getLog() {
		Path projectDir = absolutePath.getParent();
		try {
			return (JSONArray) new JSONParser().parse(Files.newBufferedReader(projectDir.resolve("log.json")));
		} catch (IOException e) {
		//	e.printStackTrace();
		} catch (ParseException e) {
		//	e.printStackTrace();
		}
		return null;
	}
}
