package com.avans.tentamenmanager;

import com.avans.tentamenmanager.data.Student;

import java.util.ArrayList;

public interface OnPathScanned extends Event {
    void onPathScanned(ArrayList<Student> students);
}
