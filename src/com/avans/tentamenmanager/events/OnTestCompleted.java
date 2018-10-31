package com.avans.tentamenmanager.events;

import com.avans.tentamenmanager.data.Student;

import java.util.ArrayList;

public interface OnTestCompleted extends Event {
    void onTestCompleted(Student student);
}
