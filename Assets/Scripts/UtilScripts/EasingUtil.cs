using UnityEngine;

public static class EasingUtil
{
    /// <summary>
    /// デフォルト動作の EaseIn 関数
    /// </summary>
    /// <param name="elapsedTime">経過時間</param>
    /// <param name="startVal">開始値</param>
    /// <param name="targetVal">終了値</param>
    /// <param name="duration">アニメーション時間</param>
    /// <returns>Tween value</returns>
    public static float EaseIn(float elapsedTime, float startVal, float targetVal, float duration)
    {
        return EaseInQuint(elapsedTime, startVal, targetVal, duration);
    }

    /// <summary>
    /// デフォルト動作の EaseOut 関数
    /// </summary>
    /// <param name="elapsedTime">経過時間</param>
    /// <param name="startVal">開始値</param>
    /// <param name="targetVal">終了値</param>
    /// <param name="duration">アニメーション時間</param>
    /// <returns>Tween value</returns>
    public static float EaseOut(float elapsedTime, float startVal, float targetVal, float duration)
    {
        return EaseOutQuint(elapsedTime, startVal, targetVal, duration);
    }

    #region Easing Quadratic
    public static float EaseInQuad(float elapsedTime, float startVal, float targetVal, float duration)
    {
        float t = elapsedTime / duration;
        float a = targetVal - startVal;
        return a * t * t + startVal;
    }

    public static float EaseOutQuad(float elapsedTime, float startVal, float targetVal, float duration)
    {
        float t = elapsedTime / duration;
        float a = targetVal - startVal;
        return - a * t * (t - 2.0f) + startVal;
    }

    public static float EaseInOutQuad(float elapsedTime, float startVal, float targetVal, float duration)
    {
        float t = elapsedTime / duration * 2.0f;
        float a = targetVal - startVal;
        if(t < 1)
        {
            return a / 2.0f * t * t + startVal;
        }
        else
        {
            t -= 1;
            return - a / 2.0f * (t * (t-2.0f) - 1) + startVal;
        }
    }
    #endregion

    #region Easing Quintic
    public static float EaseInQuint(float elapsedTime, float startVal, float targetVal, float duration)
    {
        float t = elapsedTime / duration;
        float a = targetVal - startVal;
        return a * Mathf.Pow(t, 5f) + startVal;
    }

    public static float EaseOutQuint(float elapsedTime, float startVal, float targetVal, float duration)
    {
        float t = elapsedTime / duration;
        t -= 1f;
        float a = targetVal - startVal;
        return a * (Mathf.Pow(t, 5f) + 1f) + startVal;
    }
    #endregion

    public static float Linear(float elapsedTime, float startVal, float targetVal, float duration)
    {
        float t = elapsedTime / duration;
        float a = targetVal - startVal;
        return a * t + startVal;
    }
}
