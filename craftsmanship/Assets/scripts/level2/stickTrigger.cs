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
        timeLinePlot.setText("û�뵽��������������ã����ܴ���˫Ŀ��������·�������������ʾ......��������˴�ţ���ƽ���ﴥ����Щ����������������������ޱ��������ֲ��ô�ŭ���������Ժ�Ҫ�������ú���̡�");
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
