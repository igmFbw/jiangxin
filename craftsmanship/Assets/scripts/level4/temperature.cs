using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class temperature : MonoBehaviour
{
    [SerializeField] private GameObject beam;
    [SerializeField] private Slider temSlider;
    [SerializeField] private Transform arrow;
    [SerializeField] private timeLine timeLinePlot;
    [SerializeField] private winPlot winPrefab;
    [SerializeField] private Transform mirrorSpoil;
    [SerializeField] GameObject firstCanvas;
    [SerializeField] GameObject secondCanvas;
    [SerializeField] Button nextSceneBu;
    [SerializeField] RectTransform secondUI;
    private float timer;
    private float beamIncrease;
    private float beamDecrease;
    private int num = 6;
    private bool isReady;
    private void Start()
    {
        if (globalManager.instance.IsRefineOn)
            isReady = true;
        beamIncrease = 0.75f / 100 * 8;
        beamDecrease = 0.75f / 100;
        num--;
        arrow.position = new Vector2(4.36f, Random.Range(1.00f,2.48f));
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
    private void Update()
    {
        if (!isReady)
            return;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            temSlider.value += 8;
            if (beam.transform.localPosition.y <= 0.75)
            {
                beam.transform.localPosition += new Vector3(0, beamIncrease, 0);
                exchangeArrow();
            }
        }
        timer += Time.deltaTime;
        if (timer >= .05f)
        {
            timer = 0;
            temSlider.value -= 1;
            if (beam.transform.localPosition.y >= 0)
            {
                beam.transform.localPosition -= new Vector3(0, beamDecrease, 0);
                exchangeArrow();
            }
        }
    }
    public void exchangeArrow()
    {
        if(Mathf.Abs(beam.transform.position.y-arrow.position.y)<.08f)
        {
            soundManage.instance.playDingSound();
            if (num > 0)
            {
                num--;
                Vector2 newPos = new Vector2(4.36f, Random.Range(1.00f, 2.48f));
                while (Mathf.Abs(newPos.y - arrow.position.y)<.5f)
                    newPos = new Vector2(4.36f, Random.Range(1.00f, 2.48f));
                arrow.position = newPos;
            }
            else
            {
                arrow.gameObject.SetActive(false);
                Sequence se = DOTween.Sequence();
                soundManage.instance.playAcquireSound();
                se.Append(mirrorSpoil.DOScale(new Vector3(1, 1, 1), 1));
                if (globalManager.instance.IsRefineOn)
                {
                    se.Append(secondUI.DOAnchorPos(new Vector2(0, 0), 1));
                }
                StartCoroutine(winLevel());
            }
        }
    }
    private IEnumerator winLevel()
    {
        yield return new WaitForSeconds(1);
        timeLinePlot.setText("蜡烛本是生活中常见的物品，谁又能想到它是解决问题的关键，如果没有提示，我恐怕永远无法解开这道关卡。技艺原来并不需要束之高阁，更多来源于生活的经历。");

        yield return new WaitForSeconds(4);
        winPrefab.gameObject.SetActive(true);
        winPrefab.openSoup();
    }
    public void setIsReady()
    {
        isReady = true;
    }
    private IEnumerator nextScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
        DOTween.KillAll(secondUI);
    }
}
