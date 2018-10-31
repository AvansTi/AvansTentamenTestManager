package com.avans.tentamenmanager.data;

import javafx.beans.Observable;
import javafx.beans.value.ObservableValue;
import javafx.scene.image.Image;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import javax.swing.*;
import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;

public class Student  {
	private Path path;
	private Path absolutePath;
	private String name;
	private String username;
	private Status status = Status.Untested;

	public Student(Path projectFile, Path workDir) {
		path = projectFile;
		absolutePath = workDir.resolve(projectFile);
		username = parsePathForUsername(projectFile);


		updateStatus();
	}

	public void updateStatus() {
		Path projectDir = absolutePath.getParent();

		status = Status.Untested;

		if(Files.exists(projectDir.resolve("log.json")))
		{
			JSONArray log = getLog();

			JSONObject compileLog = (JSONObject) ((JSONObject)log.get(log.size()-1)).get("compile");
			status = Status.Ok;
			compileLog.forEach((file, result) ->
			{
				if(((String)result).contains("error"))
					status = Status.Error;
			});
		}


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
			return (JSONArray)new JSONParser().parse(Files.newBufferedReader(projectDir.resolve("log.json")));
		} catch (IOException e) {
			e.printStackTrace();
		} catch (ParseException e) {
			e.printStackTrace();
		}
		return null;
	}
}
