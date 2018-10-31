package com.avans.tentamenmanager;

public interface EventHandler<T> {
    public void handle(T t);
}
