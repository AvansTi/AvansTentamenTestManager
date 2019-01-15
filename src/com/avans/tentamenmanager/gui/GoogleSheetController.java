package com.avans.tentamenmanager.gui;

import com.avans.tentamenmanager.GoogleSheetOrganizer;
import com.avans.tentamenmanager.TestManager;
import freemarker.template.TemplateException;
import javafx.fxml.FXML;
import javafx.scene.control.TextField;

import javax.mail.MessagingException;
import java.io.IOException;
import java.security.GeneralSecurityException;

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
	void CreateOverviewSheet() {
		try {
			googleSheet.buildOverview();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

	@FXML
	void FillTestResult() {
		try {
			googleSheet.buildTestResultSheet();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

	@FXML
	public void CreateManualSheet()
	{
		try {
			googleSheet.buildManualCorrection();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}


	@FXML
	public void CreateReports()
	{
		try {
			googleSheet.buildReports();
		} catch (IOException e) {
			e.printStackTrace();
		} catch (TemplateException e) {
			e.printStackTrace();
		}
	}

	@FXML
	public void SendReports()
	{
		try {
			googleSheet.sendReports(false);


		} catch (IOException e) {
			e.printStackTrace();
		} catch (GeneralSecurityException e) {
			e.printStackTrace();
		} catch (MessagingException e) {
			e.printStackTrace();
		}
	}
	@FXML
	public void SendReportsDry()
	{
		try {
			googleSheet.sendReports(true);


		} catch (IOException e) {
			e.printStackTrace();
		} catch (GeneralSecurityException e) {
			e.printStackTrace();
		} catch (MessagingException e) {
			e.printStackTrace();
		}
	}
}
