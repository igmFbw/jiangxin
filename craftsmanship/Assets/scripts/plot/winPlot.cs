using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class winPlot : MonoBehaviour
{
    [SerializeField] private Button closeBu;
    [SerializeField] private CanvasGroup soupCg;
    private void Awake()
    {
        closeBu.onClick.AddListener(() => 
        {
            soupCg.DOFade(0, 1.5f).OnComplete(() =>
            {
                SceneManager.LoadScene(1);
            });
        });
    }
    public void openSoup()
    {
        soupCg.DOFade(1, 2);
    }
}
