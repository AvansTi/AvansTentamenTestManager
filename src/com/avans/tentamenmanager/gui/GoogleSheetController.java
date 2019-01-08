package com.avans.tentamenmanager.gui;

import com.avans.tentamenmanager.GoogleSheetOrganizer;
import com.avans.tentamenmanager.TestManager;
import javafx.fxml.FXML;
import javafx.scene.control.TextField;

import java.io.IOException;

public class GoogleSheetController {

	GoogleSheetOrganizer googleSheet;
	TestManager testManager;

	@FXML
	TextField sheetId;
	@FXML
	TextField overviewSheetName;
	@FXML
	TextField testSheetName;
	@FXML
	TextField manualSheetName;
	@FXML
	TextField studentlistSheetName;
	@FXML
	TextField idColumn;
	@FXML
	TextField firstnameColumn;
	@FXML
	TextField lastnameColumn;

	public void init(GoogleSheetOrganizer googleSheet, TestManager testManager)
	{
		this.googleSheet = googleSheet;
		this.testManager = testManager;

		this.googleSheet.spreadsheetId = sheetId.getText();
		sheetId.textProperty().addListener(e ->this.googleSheet.spreadsheetId = sheetId.getText());
		this.googleSheet.overviewName = overviewSheetName.getText();
		overviewSheetName.textProperty().addListener(e ->this.googleSheet.overviewName = overviewSheetName.getText());
		this.googleSheet.testSheetName = testSheetName.getText();
		testSheetName.textProperty().addListener(e ->this.googleSheet.testSheetName = testSheetName.getText());
		this.googleSheet.manualSheetName = manualSheetName.getText();
		manualSheetName.textProperty().addListener(e ->this.googleSheet.manualSheetName = manualSheetName.getText());
		this.googleSheet.studentlistSheetName = studentlistSheetName.getText();
		studentlistSheetName.textProperty().addListener(e ->this.googleSheet.studentlistSheetName = studentlistSheetName.getText());
		this.googleSheet.idColumn = idColumn.getText();
		idColumn.textProperty().addListener(e ->this.googleSheet.idColumn = idColumn.getText());

	}

	@FXML
	public void CreateOverviewSheet() throws IOException {
		googleSheet.buildOverview();
	}

}
