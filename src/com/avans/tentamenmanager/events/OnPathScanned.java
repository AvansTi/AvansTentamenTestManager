package com.avans.tentamenmanager.events;

import com.avans.tentamenmanager.data.Student;

import java.util.ArrayList;

public interface OnPathScanned extends Event {
	void onPathScanned(ArrayList<Student> students);
}
