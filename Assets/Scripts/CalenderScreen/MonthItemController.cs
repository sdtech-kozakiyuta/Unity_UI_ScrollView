using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MonthItemController : MonoBehaviour
{
    [SerializeField] Text yearMonthText;
    [SerializeField] GameObject daysGrid;
    List<DayItemController> days = new List<DayItemController>();

    DateTime baseDate;   // 起動時の日時を起点に，カレンダリストの0番目の月を設定．（起動中日をまたいでもこの値は変更しない．）
    DateTime thisMonth;

    UpdateEvent updateEvent = new UpdateEvent();

    int rowId;

    private void Awake()
    {
        this.GetComponent<ScrollViewItem>().AddEventListener(UpdateItem);
        this.baseDate = DateTime.Now;

        foreach (Transform dayTrans in daysGrid.transform)
        {
            days.Add(dayTrans.GetComponent<DayItemController>());
        }

        GameObject.Find("CalenderManager").GetComponent<CalenderManager>().AddEventListenerForBaseDataUpdate(SetBaseDate);
    }

    public void UpdateItem(int num)
    {
        this.rowId = num;
        this.UpdateItem();
    }

    void UpdateItem(){
        thisMonth = new DateTime(baseDate.AddMonths(-this.rowId).Year, baseDate.AddMonths(-this.rowId).Month, 1);

        yearMonthText.text = thisMonth.ToString("yyyy/MM") + "_" + this.rowId.ToString("00");

        DateTime initialDate = DateTimeUtil.GetDaysInTheWeek(thisMonth, 0)[0];

        for (int i = 0; i < days.Count; i++)
        {
            DateTime tempDate = initialDate.AddDays(i);
            days[i].SetDate(tempDate, false, tempDate.Month == this.thisMonth.Month);
        }
        updateEvent.Invoke(thisMonth);
    }

    public void SetBaseDate(DateTime date){
        this.baseDate = date;
        this.UpdateItem();
    }

    public void AddUpdateEventListener(UnityAction<DateTime> action)
    {
        updateEvent.AddListener(action);
    }

    class UpdateEvent : UnityEvent<DateTime> { }
}
