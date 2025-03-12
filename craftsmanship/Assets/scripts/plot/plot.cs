using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class plot : MonoBehaviour
{
    [SerializeField] private List<string> dailogStr;
    [SerializeField] private GameObject poem;
    [SerializeField] private CanvasGroup poemCg;
    [SerializeField] private CanvasGroup plotCg;
    [SerializeField] private dailog dailogPrefab;
    [SerializeField] private RectTransform plotTip;
    [SerializeField] private Button closeTipBu;
    private int dailogIndex;
    private int dailogNum;
    private void Start()
    {
        dailogIndex = 0;
        dailogNum = dailogStr.Count;
        dailogPrefab.dailogText.text = " ";
        closeTipBu.onClick.AddListener(() =>
        {
            closeTip();
        });
    }
    private void OnEnable()
    {
        dailogPrefab.clickAction += updateDailog;
    }
    private void updateDailog()
    {
        if (dailogIndex < dailogNum)
        {
            dailogPrefab.dailogText.text = dailogStr[dailogIndex++];
            if(dailogIndex == 1)
            {
                poemCg.gameObject.SetActive(true);
                soundManage.instance.playPaperSound();
                poemCg.DOFade(1, 2).OnComplete(()=>
                {
                    poemCg.GetComponent<Image>().raycastTarget = true;
                    poemCg.blocksRaycasts = true;
                });
            }
        }
        else
        {
            Sequence se = DOTween.Sequence();
            se.Append(plotCg.DOFade(0, 2).OnComplete(()=>
            {
                gameObject.SetActive(false);
            }));
            plotTip.gameObject.SetActive(true);
            se.Append(plotTip.DOAnchorPos(new Vector2(0, 0), .3f));
            //gameObject.SetActive(false);
        }
    }
    private void closeTip()
    {
        plotTip.DOAnchorPos(new Vector2(0,400),2).OnComplete(()=>
        {
            plotTip.gameObject.SetActive(false);
        });
    }
    private void OnDisable()
    {
        dailogPrefab.clickAction -= updateDailog;
    }
}
