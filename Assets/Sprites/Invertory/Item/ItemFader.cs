using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemFader : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    /// <summary>
    /// 恢复原来颜色
    /// </summary>
    public void FeadIn()
    {
        Color targetColor = new Color(1f, 1f, 1f,1f);
        spriteRenderer.DOColor(targetColor, Settings.feadDuration);
    }
    
    /// <summary>
    /// 变透明
    /// </summary>
    public void FeadOut()
    {
        Color targetColor = new Color(1f, 1f, 1f,Settings.feadAlpha);
        spriteRenderer.DOColor(targetColor, Settings.feadDuration);
    }
}
