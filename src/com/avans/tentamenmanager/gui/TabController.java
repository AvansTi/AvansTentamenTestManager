package com.avans.tentamenmanager.gui;

import com.sun.javafx.collections.ObservableListWrapper;
import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
import javafx.fxml.FXML;
import javafx.scene.control.Label;
import javafx.scene.control.ListCell;
import javafx.scene.control.ListView;
import javafx.scene.control.TextArea;
import javafx.scene.layout.HBox;
import javafx.scene.paint.Color;
import javafx.scene.shape.Rectangle;
import javafx.util.Callback;
import org.json.simple.JSONObject;

import java.util.List;
import java.util.stream.Collectors;

public class TabController {

	@FXML
	public ListView<String> compileList;

	@FXML
	public ListView<String> testList;

	@FXML
	public TextArea output;

	@FXML
	public TextArea sourceCode;


	public void setLog(JSONObject logEntry) {

		//compile list
		final List<String> compile = (List<String>) ((JSONObject) logEntry.get("compile"))
				.keySet().stream()
				.sorted()
				.collect(Collectors.toList());
		compileList.setItems(new ObservableListWrapper(compile));
		compileList.setCellFactory(new Callback<ListView<String>, ListCell<String>>() {
			@Override
			public ListCell<String> call(ListView<String> param) {
				return new ListCell<String>() {
					@Override
					protected void updateItem(String item, boolean empty) {
						super.updateItem(item, empty);
						super.updateItem(item, empty);
						if (item != null) {
							boolean error = ((JSONObject) logEntry.get("compile")).get(item).toString().contains("error");


							HBox box = new HBox(8);
							Rectangle rect = new Rectangle(16, 16);
							rect.setFill(Color.rgb(error ? 255 : 0, error ? 0 : 255, 0));
							box.getChildren().add(rect);
							box.getChildren().add(new Label(item.substring(item.lastIndexOf("\\") + 1)));
							setGraphic(box);
						}
					}
				};
			}
		});


		compileList.getSelectionModel().selectedItemProperty().addListener(new ChangeListener<String>() {
			@Override
			public void changed(ObservableValue<? extends String> observable, String oldValue, String newValue) {
				String log = ((JSONObject) logEntry.get("compile")).get(newValue).toString();
				output.setText(log);
			}
		});


		////////////////////////////////////////////////////////////////tests

		final List<String> test = (List<String>) ((JSONObject) ((JSONObject) logEntry.get("test")).get("scores"))
				.keySet().stream()
				.sorted()
				.collect(Collectors.toList());
		testList.setItems(new ObservableListWrapper(test));
		testList.setCellFactory(new Callback<ListView<String>, ListCell<String>>() {
			@Override
			public ListCell<String> call(ListView<String> param) {
				return new ListCell<String>() {
					@Override
					protected void updateItem(String item, boolean empty) {
						super.updateItem(item, empty);
						super.updateItem(item, empty);
						if (item != null) {

							int score = ((Long) ((JSONObject) ((JSONObject) logEntry.get("test")).get("scores")).get(item)).intValue();

							HBox box = new HBox(8);
							Rectangle rect = new Rectangle(16, 16);
							rect.setFill(Color.rgb(score == 0 ? 255 : 0, score == 0 ? 0 : 255, 0));
							box.getChildren().add(rect);
							box.getChildren().add(new Label(item));
							setGraphic(box);
						}
					}
				};
			}
		});


		testList.getSelectionModel().selectedItemProperty().addListener(new ChangeListener<String>() {
			@Override
			public void changed(ObservableValue<? extends String> observable, String oldValue, String newValue) {
				int score = ((Long) ((JSONObject) ((JSONObject) logEntry.get("test")).get("scores")).get(newValue)).intValue();

				String log = "Score: " + score + "\n";

				JSONObject errors = (JSONObject) ((JSONObject) logEntry.get("test")).get("errors");

				for (Object test : errors.keySet()) {
					if (test.toString().contains(newValue))
						log += test.toString() + ":\n" + errors.get(test).toString() + "\n\n";
				}
				;

				output.setText(log);
			}
		});


	}
}
