using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class soundWave : MonoBehaviour
{
    private float growSpeed = 3;
    private void Update()
    {
        transform.localScale += Vector3.one * Time.deltaTime * growSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
