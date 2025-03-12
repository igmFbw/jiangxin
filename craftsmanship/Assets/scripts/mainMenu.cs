using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class mainMenu : MonoBehaviour
{
    [SerializeField] private GameObject blackImage;
    [SerializeField] private textSlider plotText;
    private void Start()
    {
        plotText.textAction += exchangeScene;
    }
    public void startGame()
    {
        blackImage.SetActive(true);
        plotText.gameObject.SetActive(true);
    }
    public void exitGame()
    {
        Application.Quit();
    }
    private void exchangeScene()
    {
        SceneManager.LoadScene(1);
    }
    private void OnEnable()
    {
        plotText.textAction -= exchangeScene;
    }
}
