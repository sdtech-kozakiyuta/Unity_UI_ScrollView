using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class InfiniteScroll : UIBehaviour
{
	[SerializeField] private RectTransform _itemPrototype;       // 表示コンテンツのプレファブ


	[SerializeField, Range(0, 30)]
	int _instantateItemCount = 27;      // いくつインスタンスを構築するか

	[SerializeField]
	private Direction _direction;       // Scroll方向

	public OnItemPositionChange onUpdateItem = new OnItemPositionChange();

	// 非表示されるコンテンツをまとめるリスト
	[System.NonSerialized]
	public LinkedList<RectTransform> itemList = new LinkedList<RectTransform>();

	protected float diffPreFramePosition = 0;

	protected int currentItemNo = 0;

	private UnityEvent setupFinishEvent = new UnityEvent();

	public enum Direction
	{
		Vertical,
		Horizontal,
	}

	// cache component
	private RectTransform _rectTransform;
	protected RectTransform rectTransform
	{
		get
		{
			if (_rectTransform == null) _rectTransform = GetComponent<RectTransform>();
			return _rectTransform;
		}
	}

	private float anchoredPosition
	{
		get
		{
			return _direction == Direction.Vertical ? -rectTransform.anchoredPosition.y : rectTransform.anchoredPosition.x;
		}
	}

	private float _itemScale = -1;
	public float itemScale
	{
		get
		{
			if (_itemPrototype != null && _itemScale == -1)
			{
				_itemScale = _direction == Direction.Vertical ? _itemPrototype.sizeDelta.y : _itemPrototype.sizeDelta.x;
			}
			return _itemScale;
		}
	}

	protected override void Start()
	{
		var controllers = GetComponents<MonoBehaviour>()
				.Where(item => item is IInfiniteScrollSetup)
				.Select(item => item as IInfiniteScrollSetup)
				.ToList();

		// ScrollRectコンポーネントの初期設定
		var scrollRect = GetComponentInParent<ScrollRect>();
		scrollRect.horizontal = _direction == Direction.Horizontal;
		scrollRect.vertical = _direction == Direction.Vertical;
		scrollRect.content = rectTransform;
		scrollRect.scrollSensitivity = 50;

		_itemPrototype.gameObject.SetActive(false);

		// スクロールビューに要素（プレファブ）を展開．
		for (int i = 0; i < _instantateItemCount; i++)
		{
			var item = GameObject.Instantiate(_itemPrototype) as RectTransform;
			item.SetParent(transform, false);
			item.name = i.ToString();
			item.anchoredPosition = _direction == Direction.Vertical ? new Vector2(0, -itemScale * i) : new Vector2(itemScale * i, 0);
			itemList.AddLast(item);

			item.gameObject.SetActive(true);
			// item.gameObject.GetComponent<ScrollViewItem>().UpdateItem(i);
		}

		// その他オプションの更新
		foreach (var controller in controllers)
		{
			controller.OnPostSetupItems();
		}
		setupFinishEvent.Invoke();
	}

	void Update()
	{
		if (itemList.First == null)
		{
			return;
		}

		while (anchoredPosition - diffPreFramePosition < -itemScale * 2)
		{
			diffPreFramePosition -= itemScale;

			var item = itemList.First.Value;
			itemList.RemoveFirst();
			itemList.AddLast(item);

			var pos = itemScale * _instantateItemCount + itemScale * currentItemNo;
			item.anchoredPosition = (_direction == Direction.Vertical) ? new Vector2(0, -pos) : new Vector2(pos, 0);

			onUpdateItem.Invoke(currentItemNo + _instantateItemCount, item.gameObject);

			currentItemNo++;
		}

		while (anchoredPosition - diffPreFramePosition > 0)
		{
			diffPreFramePosition += itemScale;

			var item = itemList.Last.Value;
			itemList.RemoveLast();
			itemList.AddFirst(item);

			currentItemNo--;

			var pos = itemScale * currentItemNo;
			item.anchoredPosition = (_direction == Direction.Vertical) ? new Vector2(0, -pos) : new Vector2(pos, 0);
			onUpdateItem.Invoke(currentItemNo, item.gameObject);
		}
	}

	/// <summary>
	/// インスタンス化されたリストの要素のViewを更新
	/// </summary>
	public void RefreshView()
    {
    	if ( itemList.First == null )
    	{
    		return;
    	}

		currentItemNo = (int)(-itemList.First.Value.anchoredPosition.y / itemScale);
		int currentItemNum = currentItemNo;
		
		foreach (var temp in itemList)
		{
			//Debug.Log("foreach_currentItemNo: " + currentItemNum + ", " + itemList.Count + ", " + temp.gameObject.name);
			onUpdateItem.Invoke(currentItemNum, temp.gameObject);
			currentItemNum++;
		}
	}

	public void AddListenerForSetupFinish(UnityAction action){
		setupFinishEvent.AddListener(action);
	}

	[System.Serializable]
	public class OnItemPositionChange : UnityEngine.Events.UnityEvent<int, GameObject> { }
}
