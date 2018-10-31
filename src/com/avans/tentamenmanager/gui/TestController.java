package com.avans.tentamenmanager.gui;

import com.avans.tentamenmanager.data.Student;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.control.Label;
import javafx.scene.control.Tab;
import javafx.scene.control.TabPane;
import javafx.scene.layout.AnchorPane;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

import java.io.IOException;
import java.net.URL;
import java.util.ResourceBundle;

public class TestController {

	private Student student;

	@FXML
	Label pathLabel;

	@FXML
	TabPane tabPane;

	public void setStudent(Student student)
	{
		this.student = student;
		pathLabel.setText(student.getPath().toString());


		JSONArray log = student.getLog();
		if(log == null)
			return;

		log.forEach(logEntryRaw ->
		{
			JSONObject logEntry = (JSONObject)logEntryRaw;

			try {
				FXMLLoader loader = new FXMLLoader(getClass().getResource("/tab.fxml"));
				Tab tab = new Tab(logEntry.get("time").toString(), loader.load());
				tabPane.getTabs().add(tab);

				((TabController)loader.getController()).setLog(logEntry);


			} catch (IOException e) {
				e.printStackTrace();
			}
		});


	}

}
