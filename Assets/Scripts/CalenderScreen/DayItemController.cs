using System;
using UnityEngine;
using UnityEngine.UI;

public class DayItemController : MonoBehaviour
{
    [SerializeField] Text uiDate;
    [SerializeField] Image uiSelectedIndicator;
    [SerializeField] Button button;
    DateTime thisDay;

    CalenderManager calenderMan;

    public bool Intaractable
    {
        set { this.button.interactable = value; }
    }
    public DateTime ThisDay
    {
        get { return thisDay; }
    }

    public void SetDate(DateTime date, bool isSelected, bool isThisMonth)
    {
        thisDay = date;
        uiDate.text = date.Day.ToString();
        uiSelectedIndicator.enabled = isSelected;
        uiDate.enabled = isThisMonth;
    }

    public void ShowContent(bool isShow)
    {
        this.button.enabled = isShow;
        foreach(Transform child in this.transform)
        {
            child.gameObject.SetActive(isShow);
        }
    }

    public void OnClickDay()
    {
        Debug.Log("The date on " + thisDay.ToString("yyyy/MM/dd") + " was clicked");
        calenderMan = GameObject.Find("CalenderManager").GetComponent<CalenderManager>();
        calenderMan.SelectDateAction(this.thisDay);
    }
}
