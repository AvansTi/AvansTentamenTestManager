package com.avans.tentamenmanager;

import com.avans.tentamenmanager.gui.Controller;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;

public class Main extends Application {
    Controller controller;
    TestManager testManager = new TestManager();

    @Override
    public void start(Stage primaryStage) throws Exception{
        FXMLLoader loader = new FXMLLoader(getClass().getResource("/main.fxml"));

        VBox root = loader.load();
        primaryStage.setTitle("Avans Tentamen Test Manager");
        primaryStage.setScene(new Scene(root));

        controller = loader.getController();
        controller.setTestManager(testManager);

        testManager.setPath("C:\\Users\\johan\\Desktop\\Avans\\Kwartalen\\Voltijd TI\\1.1 Voltijd\\2018-2019\\OGP0\\Tentamen\\work");
        primaryStage.show();
    }


    public static void main(String[] args) {
        launch(args);
    }
}
