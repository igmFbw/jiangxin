using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class stone : MonoBehaviour
{
    [SerializeField] private lever myLever;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "basketTrigger")
        {
            soundManage.instance.playImpactSound();
            myLever.score();
        }
    }
}
