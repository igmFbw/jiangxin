using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class endGame : MonoBehaviour
{
    [SerializeField] private textSlider endSlider;
    [SerializeField] private Button endBu;
    [SerializeField] private CanvasGroup cg;
    public void Start()
    {
        endBu.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        endSlider.textAction += end;
    }
    public void end()
    {
        cg.DOFade(1, 2).OnComplete(()=>
        {
            endBu.gameObject.SetActive(true);
        });
    }
}
