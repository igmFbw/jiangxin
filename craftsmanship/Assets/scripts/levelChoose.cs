using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class levelChoose : MonoBehaviour
{
    #region walkLine
    [SerializeField] private Transform startPos0;
    [SerializeField] private Transform startPos1;
    [SerializeField] private Transform startPos2;
    [SerializeField] private Transform startPos3;
    [SerializeField] private Transform startPos4;
    [SerializeField] private Transform[] line1;
    [SerializeField] private Transform[] line2;
    [SerializeField] private Transform[] line3;
    [SerializeField] private Transform[] line4;
    [SerializeField] private Transform[] line5;
    [SerializeField] private GameObject playerHead;
    #endregion
    private void Start()
    {
        int num = globalManager.instance.currentLevelNum;
        globalManager.instance.currentLevelNum++;
        Sequence se = DOTween.Sequence();
        Vector3[] lineVector;
        switch (num)
        {
            case 0:
                playerHead.transform.position = startPos0.position;
                lineVector = line1.Select(x => x.position).ToArray();
                se.Append(playerHead.transform.DOPath(lineVector, 2.5f, PathType.CatmullRom, PathMode.Full3D));
                se.AppendCallback(() => SceneManager.LoadScene(2));
                break;
            case 1:
                playerHead.transform.position = startPos1.position;
                lineVector = line2.Select(x => x.position).ToArray();
                se.Append(playerHead.transform.DOPath(lineVector, 2.5f, PathType.CatmullRom, PathMode.Full3D));
                se.AppendCallback(() => SceneManager.LoadScene(3));
                break;
            case 2:
                playerHead.transform.position = startPos2.position;
                lineVector = line3.Select(x => x.position).ToArray();
                se.Append(playerHead.transform.DOPath(lineVector, 2.5f, PathType.CatmullRom, PathMode.Full3D));
                se.AppendCallback(() => SceneManager.LoadScene(4));
                break;
            case 3:
                playerHead.transform.position = startPos3.position;
                lineVector = line4.Select(x => x.position).ToArray();
                se.Append(playerHead.transform.DOPath(lineVector, 2.5f, PathType.CatmullRom, PathMode.Full3D));
                se.AppendCallback(() => SceneManager.LoadScene(5));
                break;
            case 4:
                playerHead.transform.position = startPos4.position;
                lineVector = line5.Select(x => x.position).ToArray();
                se.Append(playerHead.transform.DOPath(lineVector, 2.5f, PathType.CatmullRom, PathMode.Full3D));
                se.AppendCallback(() => SceneManager.LoadScene(6));
                break;
        }
    }
}
