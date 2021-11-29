using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Scrollbar))]
public class ScrollbarManager : MonoBehaviour
{
    [SerializeField] Image handle;
    [SerializeField] bool isShow = true;
    bool scrollBarEnabled = false;
    // Sceneのロードが終わってから一度EnableScrollBarのイベントが呼ばれてしまい，それのせいでScrollbarがチラつくので，このフラグで初期呼び出しをしないように管理．
    bool initialCalled = false;
    [SerializeField] float durationFadeIn = 0.1f;
    [SerializeField] float durationFadeOut = 0.3f;

    [SerializeField] float alphaValue = 1.0f;
    float showingTime;

    Coroutine coroutine;

    public float value
    {
        get{ return this.GetComponent<Scrollbar>().value; }
        set{ this.GetComponent<Scrollbar>().value = value; }
    }

    void Start()
    {
        this.GetComponent<Scrollbar>().onValueChanged.AddListener(EnableScrollBar);
        Color col = this.handle.color;
        col.a = 0;
        this.handle.color = col;
        this.handle.enabled = this.isShow;
    }

    void Update()
    {
        if(scrollBarEnabled)
        {
            this.showingTime += Time.deltaTime;
            if(this.showingTime > 0.3f)
            {
                this.DisableScrollBar();
            }
        }
    }

    void EnableScrollBar(float val)
    {
        if (this.scrollBarEnabled == false && this.initialCalled == true && this.gameObject.activeSelf == true)
        {
            this.scrollBarEnabled = true;
            if(this.coroutine != null) StopCoroutine(this.coroutine);
            this.coroutine = StartCoroutine(Fade(true));
            this.SetVisibility();
        }
        this.showingTime = 0.0f;
        this.initialCalled = true;
    }

    void DisableScrollBar()
    {
        this.coroutine = StartCoroutine(Fade(false));
        this.scrollBarEnabled = false;
    }
    
    IEnumerator Fade(bool isIn)
    {
        float startAlpha  = isIn ? 0.0f : this.alphaValue;
        float targetAlpha = isIn ? this.alphaValue : 0.0f;
        float duration    = isIn ? this.durationFadeIn : this.durationFadeOut;

        float elapsedTime = 0;
        while(elapsedTime < duration)
        {
            Color col = this.handle.color;
            col.a = EasingUtil.Linear(elapsedTime, startAlpha, targetAlpha, duration);
            this.handle.color = col;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Color c = this.handle.color;
        c.a = isIn ? this.alphaValue : 0.0f;
        this.handle.color = c;
    }
    public IEnumerator SetScrollbarVal(float val)
    {
        yield return null;
        this.GetComponent<Scrollbar>().value = val;
    }

    void SetVisibility()
    {
        // Scroll可能領域が二画面分あればハンドルを表示．
        if(this.GetComponent<Scrollbar>().size < 0.5f)
        {
            this.handle.enabled = this.isShow;
        }
        else
        {
            this.handle.enabled = false;
        }
    }
}
