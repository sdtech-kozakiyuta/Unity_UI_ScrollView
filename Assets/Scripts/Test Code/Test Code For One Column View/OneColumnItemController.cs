using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollViewItem))]
public class OneColumnItemController : ItemControllerAbstract
{
    [SerializeField] Image thumbnailImage;
    [SerializeField] Text itemNumText;
    [SerializeField] Text contentName;
    [SerializeField] Image backgroundImage;
    OneColumnViewController viewController;
    TestContent targetContent;

    readonly Color[] colors = new Color[] {
        new Color(1, 1, 1, 1),
        new Color(0.9f, 0.9f, 1, 1),
    };

    protected override void Awake()
    {
        base.Awake();
        this.GetComponent<ScrollViewItem>().AddEventListener(UpdateData);
    }

    protected async override void UpdateData(int num)
    {
        this.targetContent = this.viewController.GetTestContents()[num];
        this.itemNumText.text = num.ToString("00");
        this.backgroundImage.color = colors[Mathf.Abs(num) % colors.Length];

        // StreamingAssets内に配置されているpngから該当のSpriteデータを取得．
        string contentPath = Application.dataPath + "/StreamingAssets/" + this.targetContent.thumbnailName;
        this.thumbnailImage.sprite = await AsyncUtil.LoadAsSpriteAsync(contentPath);

        // TestContentインスタンスに登録されている番号
        this.contentName.text = "ContentNumber is " + this.viewController.GetTestContents()[num].number.ToString();
    }

    public override void UpdateIcons()
    {
        // アイテムの上に重畳させて表示させたいアイコン等の表示を制御するスクリプト書いたり．無くてもいいかも
    }

    /// <summary>
    /// クリックされた時に呼び出される．
    /// </summary>
    protected override void OnClickAction()
    {
        this.viewController.OnItemClick(this.targetContent);
        Debug.Log("Item was Clicked. The item number is " + this.targetContent.number);
    }

    public void RegistViewController(OneColumnViewController controller)
    {
        this.viewController = controller;
    }
}
