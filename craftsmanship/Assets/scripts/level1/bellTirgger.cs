using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class bellTirgger : MonoBehaviour
{
    [SerializeField] private timeLine timeLinePlot;
    [SerializeField] private winPlot winPrefab;
    [SerializeField] GameObject firstCanvas;
    [SerializeField] GameObject secondCanvas;
    [SerializeField] Button nextSceneBu;
    [SerializeField] RectTransform secondUI;
    [SerializeField] Transform bellSpoil;
    private void Start()
    {
        nextSceneBu.onClick.AddListener(() =>
        {
            StartCoroutine(nextScene());
        });
        if(globalManager.instance.IsRefineOn)
        {
            firstCanvas.SetActive(false);
            secondCanvas.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "player")
        {
            timeLinePlot.setText("（仔细端详着手中的司南）从前偷偷闯进过先生的书房，" +
                "见过这个物件，不想竟然有如此妙用。");
            StartCoroutine(winLevel());
            if (globalManager.instance.IsRefineOn)
                secondUI.DOAnchorPos(new Vector2(0, 0), 1);
        }
    }
    private IEnumerator winLevel()
    {
        soundManage.instance.playAcquireSound();
        transform.DOScale(new Vector2(0, 0), 1);
        bellSpoil.DOScale(new Vector2(1, 1), 1);
        Vector2 middlePos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2 + 1));
        bellSpoil.DOMove(new Vector2(middlePos.x, middlePos.y + 1), 1);
        yield return new WaitForSeconds(5);
        winPrefab.gameObject.SetActive(true);
        winPrefab.openSoup();
    }
    private IEnumerator nextScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
        DOTween.KillAll(secondUI);
    }
}
