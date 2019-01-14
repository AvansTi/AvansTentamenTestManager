package com.avans.tentamenmanager;

import com.avans.tentamenmanager.gui.MainController;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;

public class Main extends Application {
	MainController mainController;
	TestManager testManager = new TestManager();

	@Override
	public void start(Stage primaryStage) throws Exception {
		FXMLLoader loader = new FXMLLoader(getClass().getResource("/main.fxml"));

		VBox root = loader.load();
		primaryStage.setTitle("Avans Tentamen Test Manager");
		primaryStage.setScene(new Scene(root));

		mainController = loader.getController();
		mainController.setTestManager(testManager);

		mainController.init();


		//testManager.setPath("C:\\Users\\johan\\Desktop\\Avans\\Kwartalen\\Voltijd TI\\1.2 Voltijd\\2018-2019\\OGP1\\Tentamen\\work2");
		testManager.setPath("D:\\avans\\Kwartalen\\Voltijd TI\\1.2 Voltijd\\2018-2019\\OGP1\\Tentamen\\work2");
		primaryStage.show();
	}


	public static void main(String[] args) {
		launch(args);
	}
}
