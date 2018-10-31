package com.avans.tentamenmanager.gui;

import com.avans.tentamenmanager.OnPathScanned;
import com.avans.tentamenmanager.TestManager;
import com.avans.tentamenmanager.data.Student;
import com.sun.javafx.collections.ObservableListWrapper;
import javafx.collections.FXCollections;
import javafx.collections.ObservableArray;
import javafx.collections.ObservableList;
import javafx.fxml.FXML;
import javafx.scene.control.ListView;
import javafx.stage.DirectoryChooser;

import java.io.File;
import java.util.ArrayList;
import java.util.stream.Collectors;


public class Controller implements OnPathScanned {
    private TestManager testManager;

    @FXML
    public ListView studentList;

    @FXML
    public void openPath()
    {
        DirectoryChooser directoryChooser = new DirectoryChooser();
        File selectedDirectory = directoryChooser.showDialog(studentList.getScene().getWindow());

        testManager.setPath(selectedDirectory.getAbsolutePath());
    }

    public void setTestManager(TestManager testManager) {
        this.testManager = testManager;
        testManager.addEventHandler(this);
    }

    @Override
    public void onPathScanned(ArrayList<Student> students) {

        ObservableList<String> items = new ObservableListWrapper<>(new ArrayList<String>(students.stream().map(s -> s.getName()).collect(Collectors.toList())));
        studentList.setItems(items);
    }
}
