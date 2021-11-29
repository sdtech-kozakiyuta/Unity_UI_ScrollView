using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// InfiniteScroll.csに対してオプションとして機能するクラス．
/// </summary>
[RequireComponent(typeof(InfiniteScroll))]
public class LimitedScrollController : UIBehaviour, IInfiniteScrollSetup
{
	/// <summary>
	/// スクロールビューの中に展開したい要素の数．
	/// ダウンロードされたデータ数 / 3 と同義．(3列のため)
	/// </summary>
	[SerializeField, Range(1, 9999)]
	int max = 30;
	float inflationSize = 0;
	UnityEvent OnPostSetUpEvent = new UnityEvent();

	/// <summary>
	/// InfiniteScrollのプロパティを調整し，有限スクロールの設定を行う．
	/// </summary>
	public void OnPostSetupItems()
	{
		var infiniteScroll = GetComponent<InfiniteScroll>();
		infiniteScroll.onUpdateItem.AddListener(OnUpdateItem);
		GetComponentInParent<ScrollRect>().movementType = ScrollRect.MovementType.Clamped;
		OnPostSetUpEvent.Invoke();
		//SetContentAreaSize();
	}

	public void AddListenerForOnPostSetUp(UnityAction action)
	{
		OnPostSetUpEvent.AddListener(action);
	}

	/// <summary>
	/// インスタンス化されているGameObjectの更新処理
	/// </summary>
	/// <param name="itemCount">そのGameObjに付加する新しい番棒</param>
	/// <param name="obj">対象Obj</param>
	public void OnUpdateItem(int itemCount, GameObject obj)
	{
		if (itemCount < 0 || itemCount >= max)
		{
			obj.SetActive(false);
		}
		else
		{
			obj.SetActive(true);
			var item = obj.GetComponentInChildren<ScrollViewItem>();
			item.UpdateItem(itemCount);
		}
	}

	/// <summary>
	/// リストの行数を指定し，コンテンツ領域を定義する．
	/// この時コンテンツ領域の高さに下駄をはかせたい場合はinflatioSizeに値を入れる．
	/// </summary>
	/// <param name="maxContentNum">リスト表示する最大の行数</param>
	/// <param name="inflationSize">下駄をはかせる際の高さ</param>
	public void SetMaxContentNum(int maxContentNum, float inflationSize = 0)
	{
		this.max = maxContentNum;
		this.inflationSize = inflationSize;
		this.SetContentAreaSize();   						// 要素数に更新がある場合，コンテンツ領域も更新
		this.GetComponent<InfiniteScroll>().RefreshView();  // 展開予定のデータが準備できたら，このデータをViewに反映
	}

	/// <summary>
	/// コンテンツ領域の更新
	/// </summary>
	void SetContentAreaSize()
    {
		var rectTransform = GetComponent<RectTransform>();
		var delta = rectTransform.sizeDelta;
		delta.y = GetComponent<InfiniteScroll>().itemScale * this.max + this.inflationSize;
		rectTransform.sizeDelta = delta;
	}
}
