using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class textSlider : MonoBehaviour
{
    [SerializeField] private CanvasGroup cg;
    [SerializeField] private RectTransform slideText;
    [SerializeField] private float slideSpeed;
    [SerializeField] private float endPos;
    public Action textAction;
    private void Start()
    {
        StartCoroutine(startSlider());
    }
    private IEnumerator startSlider()
    {
        cg.DOFade(1, 1);
        yield return new WaitForSeconds(2);
        Sequence se = DOTween.Sequence();
        se.Append(slideText.DOAnchorPos(new Vector2(0, endPos), slideSpeed));
        se.Append(cg.DOFade(0, 1));
        se.AppendCallback(() =>
        {
            textAction?.Invoke();
        });
    }
}
