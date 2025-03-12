using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class globalManager : MonoBehaviour
{
    public static globalManager _instance;
    public static globalManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<globalManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("globalManage");
                    go.AddComponent<globalManager>();
                }
            }
            return _instance;
        }
    }
    public int currentLevelNum = 0;
    public bool IsRefineOn = false;
    public AudioSource audioPlayer;
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            audioPlayer.Play();
        }
    }
}
