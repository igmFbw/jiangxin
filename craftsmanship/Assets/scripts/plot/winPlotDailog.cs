using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class winPlotDailog : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private Image leadImage;
    [SerializeField] private Image masterImage;
    [SerializeField] private Text dailogText;
    [SerializeField] private List<string> dailogList;
    [SerializeField] private List<bool> isLeader;
    [SerializeField] private GameObject Blackback;
    private int dailogNum;
    private int dailogIndex;
    private float x;
    private void Start()
    {
        x = 0.44f;
        dailogIndex = 0;
        dailogNum = dailogList.Count - 1;
        dailogText.text = dailogList[0];
        leadImage.color = Color.white;
        masterImage.color = new Color(x, x, x);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (dailogIndex < dailogNum) 
        {
            dailogIndex++;
            if (isLeader[dailogIndex])
            {
                leadImage.color = Color.white;
                masterImage.color = new Color(x, x, x);
            }
            else
            {
                masterImage.color = Color.white;
                leadImage.color = new Color(x, x, x);
            }
            dailogText.text = dailogList[dailogIndex];
        }
        else
        {
            Blackback.SetActive(true);
            Invoke("enterEnd", 1);
        }
    }
    private void enterEnd()
    {
        SceneManager.LoadScene(7);
    }
}
