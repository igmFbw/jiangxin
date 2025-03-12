using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class timeLine : MonoBehaviour
{
    [SerializeField] private Text PlotText;
    private float fadeTime;
    public void setText(string text,float time = 3)
    {
        PlotText.text = text;
        this.fadeTime = time;
        gameObject.SetActive(true);
    }
    private void OnEnable()
    {
        StartCoroutine(closeText());
    }
    private IEnumerator closeText()
    {
        yield return new WaitForSeconds(fadeTime);
        gameObject.SetActive(false);
    }
}
