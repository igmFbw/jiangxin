using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class drawLine : MonoBehaviour
{
    [SerializeField] private LineRenderer lr;
    [SerializeField] private LineRenderer basketlr;
    [SerializeField] private LineRenderer stonelr;
    [SerializeField] private Transform left1;
    [SerializeField] private Transform right1;
    [SerializeField] private Transform left2;
    [SerializeField] private Transform right2;
    [SerializeField] private Transform basketVertex;
    [SerializeField] private Transform stoneVertex;
    private void Start()
    {
        lr.positionCount = 2;
        lr.SetPosition(0, right1.position);
        lr.SetPosition(1, left2.position);
        basketlr.positionCount = 2;
        basketlr.SetPosition(0, left1.position);
        stonelr.positionCount = 2;
        stonelr.SetPosition(0, right2.position);
    }
    private void Update()
    {
        basketlr.SetPosition(1, basketVertex.position);
        stonelr.SetPosition(1, stoneVertex.position);
    }
}
