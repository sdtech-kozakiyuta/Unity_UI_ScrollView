using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollViewItem))]
public class RowItemManager : MonoBehaviour
{
    [SerializeField] Text uiText;

    [SerializeField] Image uiBackground;

    private readonly Color[] colors = new Color[] {
        new Color(1, 1, 1, 1),
        new Color(0.9f, 0.9f, 1, 1),
    };

    public ItemControllerAbstract[] items;

    private void Awake()
    {
        this.GetComponent<ScrollViewItem>().AddEventListener(UpdateItem);
    }

    public void UpdateItem(int num)
    {
        uiText.text = num.ToString("00");
        uiBackground.color = colors[Mathf.Abs(num) % colors.Length];

        for (int i = 0; i < items.Length; i++)
        {
            items[i].DataNum = num * items.Length + i;
        }
    }
}
