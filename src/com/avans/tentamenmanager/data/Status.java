package com.avans.tentamenmanager.data;

import javafx.scene.image.Image;

public enum Status {
	Untested	("test-unknown.png"),
	Error			("test-error.png"),
	Ok				("test-ok.png");




	private Image image;

	Status(String name)
	{
		image = new Image(getClass().getResourceAsStream("/icons/" + name));
	}
	public Image getIcon() {
		return image;
	}
}
