using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
public class depressionPlayerControl : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private Animator anim;
    public Action flipAction;
    private float xInput;
    private float yInput;
    private int facingDir = 1;
    private bool isFaced = true;
    private bool isReady;
    private void Start()
    {
        flipAction += flip;
        if(globalManager.instance.IsRefineOn)
            isReady = true;
    }
    private void FixedUpdate()
    {
        if (!isReady)
            return;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        if (xInput == 0 && yInput == 0)
            anim.SetBool("isWalk", false);
        else
            anim.SetBool("isWalk", true);
            setRb();
    }
    private void flip()
    {
        isFaced = !isFaced;
        facingDir *= -1;
        transform.Rotate(0, 180, 0);
    }
    private void setRb()
    {
        rb.velocity = new Vector2(xInput * speed, yInput * speed);
        if (xInput > 0 && !isFaced)
        {
            flipAction?.Invoke();
        }
        if (xInput < 0 && isFaced)
        {
            flipAction?.Invoke();
        }
    }
    private void OnDisable()
    {
        flipAction = null;
    }
    public void setIsReady()
    {
        isReady = true;
    }
}