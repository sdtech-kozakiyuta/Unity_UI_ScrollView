using UnityEngine;
using UnityEngine.UI;

public class ThreeColumnItemController : ItemControllerAbstract
{
    [SerializeField] Image thumbnailImage;
    [SerializeField] Text itemNumText;
    [SerializeField] Text contentName;
    ThreeColumnViewController viewController;
    TestContent targetContent;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override async void UpdateData(int num)
    {
        if(this.viewController.GetTestContents().Count <= num)
        {
            this.thumbnailImage.sprite = null;
            this.thumbnailImage.color = new Color(0,0,0,0);
            this.contentName.text = "";
            this.itemNumText.text = "";
            return;
        }
        
        this.targetContent = this.viewController.GetTestContents()[num];

        // StreamingAssets内に配置されているpngから該当のSpriteデータを取得．
        string contentPath = Application.dataPath + "/StreamingAssets/" + this.targetContent.thumbnailName;
        this.thumbnailImage.sprite = await AsyncUtil.LoadAsSpriteAsync(contentPath);
        this.thumbnailImage.color = new Color(1,1,1,1);

        // TestContentインスタンスに登録されている番号
        this.contentName.text = this.viewController.GetTestContents()[num].number.ToString();
        this.itemNumText.text = num.ToString("00");
    }

    public override void UpdateIcons()
    {
        // throw new System.NotImplementedException();
    }

    protected override void OnClickAction()
    {
        this.viewController.OnItemClick(this.targetContent);
        // throw new System.NotImplementedException();
    }

    public void RegistViewController(ThreeColumnViewController controller)
    {
        this.viewController = controller;
    }
}
