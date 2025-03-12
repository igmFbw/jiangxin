using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class dailog : MonoBehaviour, IPointerDownHandler
{
    public Action clickAction;
    public Text dailogText;
    public void OnPointerDown(PointerEventData eventData)
    {
        clickAction?.Invoke();
    }
}
