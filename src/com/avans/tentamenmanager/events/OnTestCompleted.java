package com.avans.tentamenmanager.events;

import com.avans.tentamenmanager.data.Student;

public interface OnTestCompleted extends Event {
	void onTestCompleted(Student student);
}
