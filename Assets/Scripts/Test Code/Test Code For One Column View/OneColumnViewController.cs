using System.Collections.Generic;
using UnityEngine;

public class OneColumnViewController : MonoBehaviour
{
    [SerializeField] LimitedScrollController scrollController;      // ScrollViewの参照
    [SerializeField] GameObject filterToggleGroup;                  // Filter
    [SerializeField] int inflationSize = 100;

    List<TestContent> displayTarget;    // filterToggleGroupでtestContentsの中から間引かれたコンテンツ
    public int currentfilterNum = 1;    // filterの初期値

    void Awake()
    {
        // ScrollViewの初期化が終わった後に呼ばれる処理
        this.scrollController.AddListenerForOnPostSetUp(()=>{
            this.RegisterItemEvent(this.scrollController.transform);
            this.Refresh(this.currentfilterNum);
        });

        // Filterのリスナ登録．
        this.filterToggleGroup.GetComponent<FilterToggleController>().mFilterToggleEvent.AddListener(OnToggleFilter);
    }

    /// <summary>
    /// Filterの値が変わった時に呼ばれる．
    /// Awakeでリスナ登録されている．
    /// </summary>
    /// <param name="num">Filterで指定される条件</param>
    public void OnToggleFilter(int num)
    {
        this.currentfilterNum = num;
        this.Refresh(this.currentfilterNum);
    }

    /// <summary>
    /// Filterの条件にあったコンテンツのリストが再編され，それをScrollViewに展開．
    /// </summary>
    /// <param name="num"></param>
    void Refresh(int num)
    {
        this.displayTarget = new List<TestContent>();
        if(num == 1) this.displayTarget = TestContentDatabase.contentList;
        else if(num != 0)
        {
            foreach(TestContent content in TestContentDatabase.contentList)
            {
                int tempNum = content.number;
                Debug.Log(tempNum);
                if(tempNum % num == 0) this.displayTarget.Add(content);
            }
        }
        
        int maxContentNum = this.displayTarget.Count;
        this.scrollController.SetMaxContentNum(maxContentNum, this.inflationSize);
    }

    /// <summary>
    ///  ItemControllerより参照される．
    /// </summary>
    /// <returns></returns>
    public List<TestContent> GetTestContents()
    {
        return this.displayTarget;
    }

    /// <summary>
    /// ItemControllerでOnClickされた時に呼ばれる．
    /// </summary>
    /// <param name="targetContent"></param>
    public void OnItemClick(TestContent targetContent)
    {
        Debug.Log("Itemがクリックされました．");
        Debug.Log("TestContentの番号は " + targetContent.number);
    }

    /// <summary>
    /// ScrollViewにてインスタンス化されたItemに本クラスの参照を登録する．
    /// ItemControllerから本クラスを参照できるようにするため．
    /// </summary>
    /// <param name="contentTrans">インスタンス化されるObjの親Transform</param>
    void RegisterItemEvent(Transform contentTrans)
    {
        foreach(Transform item in contentTrans)
        {
            item.GetComponent<OneColumnItemController>().RegistViewController(this);
        }
    }
}
