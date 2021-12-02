using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CalenderViewController : MonoBehaviour
{
    [SerializeField] CalenderManager calenderMan;
    [SerializeField] InfiniteScrollController scrollController;
    [SerializeField] ScrollbarManager scrollbar;

    void Awake()
    {
        // 月ごとのカレンダーが全てインスタンス化された際に呼び出したいコールバックを登録
        scrollController.transform.GetComponent<InfiniteScroll>().AddListenerForSetupFinish( () => RefreshView() );
        this.calenderMan.AddEventListenerForDateSelection(SelectDate);
    }

    void RefreshView()
    {
        this.calenderMan.BaseDate = DateTime.Now;
    }

    /// <summary>
    /// 日付が選択された時に呼ばれるコールバック関数．
    /// </summary>
    /// <param name="date"></param>
    public void SelectDate(DateTime date)
    {
        Debug.Log("Clicked Date: " + date.ToString("yyyy/MM/dd"));
    }
}
