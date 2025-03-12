using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.UI.Image;
public class bell : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask spoilLayer;
    [SerializeField] private LayerMask barrierLayer;
    [SerializeField] private GameObject wave;
    [SerializeField] private float maxDetectDistance;
    [SerializeField] private GameObject[] echoWave;
    [SerializeField] private float bellCool;
    [SerializeField] private GameObject echoWaveParent;
    [SerializeField] Animator anim;
    [SerializeField] private AudioSource audioS;
    private bool isReady;
    private float bellTimer;
    private void Start()
    {
        bellTimer = bellCool;
        if(globalManager.instance.IsRefineOn)
            isReady = true;
        player.GetComponent<depressionPlayerControl>().flipAction += echoFlip;
    }
    private void echoFlip()
    {
        echoWaveParent.transform.Rotate(0, 180, 0);
    }
    private Vector2[] directions = new Vector2[]
    {
            Vector2.up,
            new Vector2(1f, 1f).normalized,
            Vector2.right,
            new Vector2(1f, -1f).normalized,
            Vector2.down,
            new Vector2(-1f, -1f).normalized,
            Vector2.left,
            new Vector2(-1f, 1f).normalized
    };
    private void Update()
    {
        if (!isReady)
            return;
        bellTimer += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(bellTimer>=bellCool)
            {
                audioS.Play();
                bellTimer = 0;
                anim.SetBool("isBell", true);
                Instantiate(wave, player.position, Quaternion.identity);
                rayDetect();
            }
        }
    }
    private void rayDetect()
    {
        for(int i=0;i<=7;i++)
        {
            RaycastHit2D baHit = Physics2D.Raycast
                (player.position, directions[i], maxDetectDistance, barrierLayer);
            RaycastHit2D spHit = Physics2D.Raycast
                (player.position, directions[i], maxDetectDistance, spoilLayer);
            if (baHit || spHit)
                StartCoroutine(echoWaveEffect(spHit, i, baHit ? baHit.distance : spHit.distance));

        }
    }
    private IEnumerator echoWaveEffect(bool isSpoil, int i, float distance)
    {
        float scale = 2 - (distance / 10.0f * 2);
        echoWave[i].transform.localScale = new Vector3(scale, scale, scale);
        echoWave[i].SetActive(true);
        if (isSpoil)
        {
            echoWave[i].GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            echoWave[i].GetComponent<SpriteRenderer>().color = Color.white;
        }
        yield return new WaitForSeconds(2);
        echoWave[i].SetActive(false);
    }
    private void OnDrawGizmos()
    {
        foreach(var value in directions)
        {
            Gizmos.DrawLine(player.position, new Vector2(player.position.x + maxDetectDistance * value.x, player.position.y + maxDetectDistance * value.y));

        }
    }
    public void stopShake()
    {
        anim.SetBool("isBell", false);
    }
    public void setIsReady()
    {
        isReady = true;
    }
}
