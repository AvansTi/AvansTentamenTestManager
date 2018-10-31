package com.avans.tentamenmanager;


import com.avans.tentamenmanager.data.Student;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;

public class TestManager extends EventManager {
    private ArrayList<Student> students = new ArrayList<>();



    public void setPath(String path) {
        scanDir(Paths.get(path));
        this.<OnPathScanned>trigger(e -> e.onPathScanned(students));
    }

    private void scanDir(Path path)
    {
        try {
            Files.list(path).forEach(file ->
            {
                if(Files.isDirectory(file))
                    scanDir(file);

                if(file.toString().endsWith(".iml"))
                    students.add(new Student(file));



            });
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

}
