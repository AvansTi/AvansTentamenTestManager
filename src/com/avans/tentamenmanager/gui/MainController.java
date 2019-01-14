package com.avans.tentamenmanager.gui;

import com.avans.tentamenmanager.GoogleSheetOrganizer;
import com.avans.tentamenmanager.TestManager;
import com.avans.tentamenmanager.data.Student;
import com.avans.tentamenmanager.events.OnPathScanned;
import com.avans.tentamenmanager.events.OnTestCompleted;
import com.sun.javafx.collections.ObservableListWrapper;
import javafx.beans.Observable;
import javafx.beans.property.BooleanProperty;
import javafx.beans.property.SimpleBooleanProperty;
import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
import javafx.collections.ObservableList;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.control.Label;
import javafx.scene.control.ListCell;
import javafx.scene.control.ListView;
import javafx.scene.image.ImageView;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.HBox;
import javafx.stage.DirectoryChooser;
import javafx.util.Callback;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.security.GeneralSecurityException;
import java.util.ArrayList;


public class MainController implements OnPathScanned, OnTestCompleted, ChangeListener<Student> {
	private TestManager testManager;
	private BooleanProperty studentObservable;

	private Student selectedStudent;

	@FXML
	public ListView<Student> studentList;

	@FXML
	public AnchorPane TestPanel;

	public void init() {

		studentList.setCellFactory(new Callback<ListView<Student>, ListCell<Student>>() {
			@Override
			public ListCell<Student> call(ListView<Student> list) {
				return new ListCell<Student>() {
					@Override
					protected void updateItem(Student item, boolean empty) {
						super.updateItem(item, empty);
						if (item != null) {
							HBox box = new HBox(8);
							ImageView image = new ImageView(item.getStatus().getIcon());
							box.getChildren().add(image);
							box.getChildren().add(new Label(item.getUsername()));
							setGraphic(box);
						}
					}
				};
			}
		});

		studentList.getSelectionModel().selectedItemProperty().addListener(this);


	}


	@FXML
	public void openPath() {
		DirectoryChooser directoryChooser = new DirectoryChooser();
		File selectedDirectory = directoryChooser.showDialog(studentList.getScene().getWindow());

		testManager.setPath(selectedDirectory.getAbsolutePath());
	}

	@FXML
	public void runTest()
	{
		testManager.runTest(selectedStudent);
	}

	@FXML
	public void runAllTests() {
		testManager.runAllTests();
	}

	@FXML
	public void fixSheet() throws IOException, GeneralSecurityException {
		selectedStudent = null;
		FXMLLoader loader = new FXMLLoader(getClass().getResource("/googlesheet.fxml"));
		AnchorPane root = loader.load();
		GoogleSheetController controller = loader.getController();
		controller.init(new GoogleSheetOrganizer(testManager), testManager);

		TestPanel.getChildren().clear();
		TestPanel.getChildren().add(root);

	}


	@FXML
	public void exportJson() {
		JSONObject results = new JSONObject();

		for(Student student : testManager.getStudents())
		{
			int studentId = student.getStudentId();
			if(studentId == 0)
			{
				System.err.println("Could not get student ID for student " + student.getName());
				continue;
			}
			JSONArray logs = student.getLog();
			JSONObject log = (JSONObject)logs.get(logs.size()-1);
			log.put("id", studentId);



			results.put(studentId + "", log);
		}



		try {
			String data = results.toString();
			data = data.replace(testManager.getPath().toString().replace("\\", "\\\\"), "");
			Files.write(Paths.get("results.json"), data.getBytes());
		} catch (IOException e) {
			e.printStackTrace();
		}

	}

	public void setTestManager(TestManager testManager) {
		this.testManager = testManager;
		testManager.addEventHandler(this);
	}



	@Override
	public void onPathScanned(ArrayList<Student> students) {

		studentObservable = new SimpleBooleanProperty();
		ObservableList<Student> items = new ObservableListWrapper<>(students, new Callback<Student, Observable[]>() {
			@Override
			public Observable[] call(Student param) {
				return new Observable[] { studentObservable };
			}
		});
		studentList.setItems(items);
	}

	/**
	 * Gets called whenever the student list changes. Wanted to bind this with fxml, but fxmylife
	 *
	 * @param observable
	 * @param oldValue
	 * @param newValue
	 */
	@Override
	public void changed(ObservableValue<? extends Student> observable, Student oldValue, Student newValue) {
		try {
			selectedStudent = newValue;
			FXMLLoader loader = new FXMLLoader(getClass().getResource("/test.fxml"));
			AnchorPane root = loader.load();

			TestPanel.getChildren().clear();
			TestPanel.getChildren().add(root);
			if (selectedStudent != null)
				((TestController) loader.getController()).setStudent(selectedStudent);

		} catch (IOException e) {
			e.printStackTrace();
		}

	}


	@Override
	public void onTestCompleted(Student student) {
		studentObservable.setValue(!studentObservable.getValue());
	}
}
