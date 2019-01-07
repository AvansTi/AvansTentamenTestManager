package com.avans.tentamenmanager.gui;

import com.avans.tentamenmanager.data.Student;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.control.Label;
import javafx.scene.control.Tab;
import javafx.scene.control.TabPane;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

import java.io.IOException;

public class TestController {

	private Student student;

	@FXML
	Label pathLabel;

	@FXML
	Label studentIdLabel;

	@FXML
	TabPane tabPane;

	public void setStudent(Student student) {
		this.student = student;
		pathLabel.setText(student.getPath().toString());
		studentIdLabel.setText(student.getStudentId() + "");


		JSONArray log = student.getLog();
		if (log == null)
			return;

		log.forEach(logEntryRaw ->
		{
			JSONObject logEntry = (JSONObject) logEntryRaw;

			try {
				FXMLLoader loader = new FXMLLoader(getClass().getResource("/tab.fxml"));
				Tab tab = new Tab(logEntry.get("time").toString(), loader.load());
				tabPane.getTabs().add(tab);

				((TabController) loader.getController()).setLog(logEntry);


			} catch (IOException e) {
				e.printStackTrace();
			}
		});


	}

}
