package com.avans.tentamenmanager.events;

public interface EventHandler<T> {
	public void handle(T t);
}
