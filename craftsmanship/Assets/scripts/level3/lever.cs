using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class lever : MonoBehaviour
{
    [SerializeField] private List<stone> stoneList;
    private stone currentStone;
    [SerializeField] private Transform leverPoint;
    private float leverPower;
    [SerializeField] private Animator anim;
    [HideInInspector] public int stoneNum;
    [SerializeField] private Transform bigStone;
    [SerializeField] private Transform basket;
    [SerializeField] private Slider leverSlider;
    [SerializeField] private timeLine timeLinePlot;
    [SerializeField] private winPlot winPrefab;
    [SerializeField] private Transform candleSpoil;
    [SerializeField] private GameObject candle;
    [SerializeField] GameObject firstCanvas;
    [SerializeField] GameObject secondCanvas;
    [SerializeField] Button nextSceneBu;
    [SerializeField] RectTransform secondUI;
    private bool isReady;
    private bool isWin;
    private void Start()
    {
        if (globalManager.instance.IsRefineOn)
            isReady = true;
        stoneNum = 0;
        leverPower = 0;
        currentStone = null;
        isWin = false;
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
        if (isWin)
            return;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (currentStone == null)
                return;
            soundManage.instance.playThrowSound();
            Vector2 leverForce = new Vector2(0.5f,1.3f) * leverPower;
            currentStone.GetComponent<Rigidbody2D>().AddForce(leverForce, ForceMode2D.Impulse);
            anim.SetBool("isShoot", true);
            currentStone = null;
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            currentStone = stoneList[stoneNum];
            resetPos();
        }
    }
    private void resetPos()
    {
        currentStone.transform.rotation = Quaternion.identity;
        currentStone.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        currentStone.transform.position = leverPoint.position;
    }
    public void leverRecycle()
    {
        anim.SetBool("isShoot", false);
    }
    public void score()
    {
        if(stoneNum!=2)
        {
            stoneNum++;
        }
        else
        {
            isWin = true;
            basket.DOMove(new Vector3(0, -2.56f, 0), 2).SetRelative();
            bigStone.DOMove(new Vector3(0, 2.56f, 0), 2).SetRelative()
                .OnComplete(()=>
                {
                    Destroy(candle);
                    soundManage.instance.playAcquireSound();
                    candleSpoil.DOScale(new Vector3(1, 1, 1), 1);
                    candleSpoil.DOMove(new Vector2(0, 1), 1);
                    StartCoroutine(winLevel());
                });
        }
    }
    public void setLeverPower()
    {
        leverPower = leverSlider.value;
    }
    private IEnumerator winLevel()
    {
        yield return new WaitForSeconds(1);
        timeLinePlot.setText("总算取到了，真是累人。这些石头......平时我是不可能去搬的，哪怕一块。以前我视它们为脏累活，但好像也没什么，我还从中获悉了这种如此省力的杠杆法，研习技艺应脚踏实地，先生平日教诲果然不错，怪我太贪玩，惹怒了先生，如今只能奋力攀登了，定要闯到最后一关。",5);
        if(globalManager.instance.IsRefineOn)
            secondUI.DOAnchorPos(new Vector2(0, 0), 1);
        yield return new WaitForSeconds(6);
        winPrefab.gameObject.SetActive(true);
        winPrefab.openSoup();
        //Destroy(gameObject);
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
