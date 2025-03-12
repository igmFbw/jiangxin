using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class stickTrigger : MonoBehaviour
{
    [SerializeField] private timeLine timeLinePlot;
    [SerializeField] private winPlot winPrefab;
    [SerializeField] private Transform stickSpoil;
    [SerializeField] private GameObject globalLight;
    [SerializeField] GameObject firstCanvas;
    [SerializeField] GameObject secondCanvas;
    [SerializeField] Button nextSceneBu;
    [SerializeField] RectTransform secondUI;
    private void Start()
    {
        nextSceneBu.onClick.AddListener(() =>
        {
            StartCoroutine(nextScene());
        });
        if (globalManager.instance.IsRefineOn)
        {
            firstCanvas.SetActive(false);
            secondCanvas.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            globalLight.SetActive(true);
            soundManage.instance.playAcquireSound();
            stickSpoil.DOScale(new Vector3(1, 1, 1), 1);
            stickSpoil.DOMove(new Vector2(0, 1), 1);
            StartCoroutine(winLevel());
            transform.DOScale(0, .5f);
        }
    }
    private IEnumerator winLevel()
    {
        yield return new WaitForSeconds(1);
        timeLinePlot.setText("没想到声音还有如此妙用，竟能代替双目，助人行路，多亏先生的提示......先生有如此大才，我平日里触及那些旁门左道在先生看来恐怕愚笨至极，怪不得触怒了先生，以后定要向先生好好请教。");
        if (globalManager.instance.IsRefineOn)
            secondUI.DOAnchorPos(new Vector2(0, 0), 1);
        yield return new WaitForSeconds(3f);
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
