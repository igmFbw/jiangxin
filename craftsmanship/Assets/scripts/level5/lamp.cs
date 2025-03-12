using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class lamp : MonoBehaviour
{
    [SerializeField] private Transform[] mirrorList;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private timeLine firstWinTimeLine;
    [SerializeField] private timeLine secondWinTimeLine;
    [SerializeField] private RectTransform loseUI;
    [SerializeField] private GameObject loseBlackImage;
    [SerializeField] private winPlotDailog winPlot;
    [SerializeField] GameObject firstCanvas;
    [SerializeField] GameObject secondCanvas;
    [SerializeField] CanvasGroup paperCg;
    [SerializeField] Transform chestSpoil;
    private Vector2 launchPoint;
    private Vector2 reflectDir;
    private int lineNum;
    private GameObject lastGo;
    private bool isWin;
    private void Start()
    {
        lr.positionCount = 16;
        if (globalManager.instance.IsRefineOn)
        {
            firstCanvas.SetActive(false);
            secondCanvas.SetActive(true);
        }
    }
    private void Update()
    {
        if (isWin)
            return;
        rayDetect();
    }

    private void rayDetect()
    {
        launchPoint = transform.position;
        lr.SetPosition(0, transform.position);
        reflectDir = Vector2.right;
        lastGo = gameObject;

        lineNum = 1;
        RaycastHit2D hit;
        do
        {
            RaycastHit2D[] hitList = Physics2D.RaycastAll(launchPoint, reflectDir, 25);          
            if (lastGo.tag == "mirror")
                hit = hitList[1];
            else
                hit = hitList[0];
            if (hit.collider.gameObject.tag == "mirror")
            {
                mirror m = hit.collider.gameObject.GetComponent<mirror>();
                Vector2 initialDir = (launchPoint - hit.point).normalized;
                reflectDir = m.countReflectVe(initialDir);
                launchPoint = hit.collider.transform.position;
                lastGo = hit.collider.gameObject;
                lr.SetPosition(lineNum, hit.collider.transform.position);
            }
            else if (hit.collider.gameObject.tag == "barrier")
            {
                lr.SetPosition(lineNum, hit.point);
                while(lineNum<lr.positionCount-1)
                {
                    lineNum++;
                    lr.SetPosition(lineNum, hit.point);
                }
            }
            else if (hit.collider.gameObject.tag == "chest")
            {
                isWin = true;
                lr.SetPosition(lineNum, hit.point);
                while (lineNum < lr.positionCount - 1)
                {
                    lineNum++;
                    lr.SetPosition(lineNum, hit.point);
                }
                if(!globalManager.instance.IsRefineOn)
                {
                    globalManager.instance.IsRefineOn = true;
                    StartCoroutine(firstWin());
                }
                else
                {
                    StartCoroutine(secondWin());
                }
            }
            lineNum++;
        }
        while (hit.collider.tag == "mirror");
    }
    private IEnumerator firstWin()
    {
        yield return new WaitForSeconds(1);
        firstWinTimeLine.setText("（光线照到宝箱上，锁自动脱落，你想打开宝箱，却发现锁的后面还贴了一张纸，上面写着一行字。）");
        paperCg.DOFade(1, 1);
        yield return new WaitForSeconds(3.5f);
        paperCg.DOFade(0, 1);
        firstWinTimeLine.setText("（你明白了先生的意思，退出了房间回到了山脚。）");
        yield return new WaitForSeconds(2);
        loseUI.gameObject.SetActive(true);
        loseUI.DOAnchorPos(new Vector2(0, 1), 1);
    }
    private IEnumerator secondWin()
    {
        yield return new WaitForSeconds(1);
        soundManage.instance.playAcquireSound();
        chestSpoil.DOScale(new Vector3(2, 2, 1), 1);
        yield return new WaitForSeconds(1);
        chestSpoil.GetComponentInChildren<Animator>().SetBool("isOpen", true);
        soundManage.instance.playKeySound();
        yield return new WaitForSeconds(.5f);
        winPlot.gameObject.SetActive(true);
    }
    public void returnOrigin()
    {
        loseBlackImage.SetActive(true);
        StartCoroutine(losegame());
    }
    private IEnumerator losegame()
    {
        yield return new WaitForSeconds(2);
        globalManager.instance.currentLevelNum = 0;
        SceneManager.LoadScene(1);
    }
}
