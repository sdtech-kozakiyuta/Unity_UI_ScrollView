using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ItemControllerAbstract : MonoBehaviour
{
    protected int itemNum;
    Button button;

    public int DataNum
    {
        get { return itemNum; }
        set
        {
            itemNum = value;
            this.UpdateData(value);
        }
    }

    protected virtual void Awake()
    {
        this.button = this.GetComponent<Button>();
        this.button.onClick.AddListener(OnClickAction);
    }
    abstract protected  void UpdateData(int itemNum);
    abstract public     void UpdateIcons();
    abstract protected  void OnClickAction();

    protected void SetButtonEnabled(bool isEnabled)
    {
        button.enabled = isEnabled;
    }
}
