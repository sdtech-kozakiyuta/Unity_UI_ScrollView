using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ScrollViewItem : UIBehaviour
{
	int curretNum;

	OnItemUpdate onItemUpdateEvent = new OnItemUpdate();

	/// <summary>
	/// 要素の更新
	/// </summary>
	/// <param name="count">更新したい番号</param>
    public void UpdateItem(int count)
	{
		curretNum = count;
		ViewUpdate(curretNum);
	}

	/// <summary>
	/// ViewのUpdate
	/// </summary>
	public void ViewUpdate(int Num)
    {
		onItemUpdateEvent.Invoke(Num);
    }

	/// <summary>
	/// 子要素にあたるコンポーネントを，自身の更新のタイミングに合わせて更新するためのイベントリスナ登録
	/// </summary>
	/// <param name="action"></param>
	public void AddEventListener(UnityAction<int> action)
	{
		onItemUpdateEvent.AddListener(action);
	}

	public class OnItemUpdate : UnityEvent<int> { }
}
