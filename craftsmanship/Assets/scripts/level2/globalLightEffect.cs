using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class globalLightEffect : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;

    private void OnEnable()
    {
        globalLight.intensity = 0;
    }
    private void Update()
    {
        if(globalLight.intensity<=1)
        {
            globalLight.intensity += Time.deltaTime * 1f;
        }
        else
        {
            globalLight.intensity = 1;
        }
    }
}
