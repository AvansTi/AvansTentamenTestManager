package com.avans.tentamenmanager.events;

import com.avans.tentamenmanager.events.Event;
import com.avans.tentamenmanager.events.EventHandler;

import java.util.ArrayList;

public class EventManager {
    private ArrayList<Event> handlers = new ArrayList<>();

    public void addEventHandler(Event handler)
    {
        handlers.add(handler);
    }

    protected <EventType extends Event> void trigger(EventHandler<EventType> callback)
    {
        for(Event e : handlers)
        {
            try {
                callback.handle(((EventType) e));
            } catch(ClassCastException ex) { }
        }
    }
}
