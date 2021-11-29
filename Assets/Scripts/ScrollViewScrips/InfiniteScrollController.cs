using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(InfiniteScroll))]
public class InfiniteScrollController : UIBehaviour, IInfiniteScrollSetup
{
	public void OnPostSetupItems()
	{
		GetComponent<InfiniteScroll>().onUpdateItem.AddListener(OnUpdateItem);
		GetComponentInParent<ScrollRect>().movementType = ScrollRect.MovementType.Unrestricted;
	}

	public void OnUpdateItem(int itemCount, GameObject obj)
	{
		var item = obj.GetComponentInChildren<ScrollViewItem>();
		item.UpdateItem(itemCount);
	}
}
