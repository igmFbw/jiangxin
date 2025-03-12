using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class soundManage : MonoBehaviour
{
    public static soundManage _instance;
    public AudioSource audioPlayer;
    public AudioClip acquireSound;
    public AudioClip dingSound;
    public AudioClip throwSound;
    public AudioClip keySound;
    public AudioClip impactSound;
    public AudioClip paperSound;
    public static soundManage instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<soundManage>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("soundManager");
                    go.AddComponent<soundManage>();
                }
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void playAcquireSound()
    {
        audioPlayer.clip = acquireSound;
        audioPlayer.Play();
    }
    public void playDingSound()
    {
        audioPlayer.clip = dingSound;
        audioPlayer.Play();
    }
    public void playThrowSound()
    {
        audioPlayer.clip = throwSound;
        audioPlayer.Play();
    }
    public void playKeySound()
    {
        audioPlayer.clip = keySound;
        audioPlayer.Play();
    }
    public void playImpactSound()
    {
        audioPlayer.clip = impactSound;
        audioPlayer.Play();
    }
    public void playPaperSound()
    {
        audioPlayer.clip = paperSound;
        audioPlayer.Play();
    }
}
