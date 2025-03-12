using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class poem :MonoBehaviour ,IPointerDownHandler
{
    private CanvasGroup cg;
    private void Start()
    {
        cg = GetComponent<CanvasGroup>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        cg.DOFade(0, 2).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
