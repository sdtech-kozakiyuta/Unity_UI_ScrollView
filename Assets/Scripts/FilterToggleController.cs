using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FilterToggleEvent : UnityEvent<int>{}

public class FilterToggleController : MonoBehaviour
{
    public FilterToggleEvent        mFilterToggleEvent = new FilterToggleEvent();

    public void OnAllFilterClicked(Toggle toggle)
    {
        Debug.Log("ALL");
        if (toggle.isOn) mFilterToggleEvent.Invoke(1);
    }

    public void On100FilterClicked(Toggle toggle)
    {
        Debug.Log("100");
        if (toggle.isOn) mFilterToggleEvent.Invoke(100);
    }

    public void On50FilterClicked(Toggle toggle)
    {
        Debug.Log("50");
        if (toggle.isOn) mFilterToggleEvent.Invoke(50);
    }

    public void On30FilterClicked(Toggle toggle)
    {
        Debug.Log("30");
        if (toggle.isOn) mFilterToggleEvent.Invoke(30);
    }

    public void OnNullFilterClicked(Toggle toggle)
    {
        Debug.Log("Null");
        if (toggle.isOn) mFilterToggleEvent.Invoke(0);
    }
}
