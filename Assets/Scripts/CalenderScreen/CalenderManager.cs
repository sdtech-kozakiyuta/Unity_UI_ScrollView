using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CalenderManager : MonoBehaviour
{
    DateTime baseDate;
    EventForDate dateSelectEvent = new EventForDate();
    EventForDate baseDateUpdateEvent = new EventForDate();

    public DateTime BaseDate
    {
        get { return baseDate; }
        set
        {
            this.baseDate = value;
            baseDateUpdateEvent.Invoke(this.baseDate);
        }
    }
    public void SelectDateAction(DateTime date)
    {
        dateSelectEvent.Invoke(date);
    }

    public void AddEventListenerForDateSelection(UnityAction<DateTime> action)
    {
        dateSelectEvent.AddListener(action);
    }

    public void AddEventListenerForBaseDataUpdate(UnityAction<DateTime> action)
    {
        baseDateUpdateEvent.AddListener(action);
    }
    class EventForDate : UnityEvent<DateTime> { }
}
