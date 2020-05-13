using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuLogic : MonoBehaviour
{
    [Header("MainMenus")]
    public GameObject MainMenu;
    public GameObject CreditsMenu;
    public GameObject OptionsMenu;

    [Header("OptionsMenu")]
    public GameObject videoOptions;
    public GameObject gameplayOptions;
    public GameObject soundOptions;
    public GameObject controlOptions;
    [Header("Animations")]
    public Animator credits;


    private void Start()
    {
        BackToMainMenu();
    }

    public void BackToMainMenu()
    {
        MainMenu.SetActive(true);
        CreditsMenu.SetActive(false);
        OptionsMenu.SetActive(false);
    }

    public void ShowOptionsMenu()
    {
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        OptionsMenu.SetActive(true);
        OptionsMenuVideo();
    }

    public void ShowCredits()
    {
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
        OptionsMenu.SetActive(false);
        credits.Play("Credits");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OptionsMenuVideo()
    {
        videoOptions.SetActive(true);
        gameplayOptions.SetActive(false);
        soundOptions.SetActive(false);
        controlOptions.SetActive(false);
    }

    public void OptionsMenuControlls()
    {
        videoOptions.SetActive(false);
        gameplayOptions.SetActive(false);
        soundOptions.SetActive(false);
        controlOptions.SetActive(true);
    }

    public void OptionsMenuGameplay()
    {
        videoOptions.SetActive(false);
        gameplayOptions.SetActive(true);
        soundOptions.SetActive(false);
        controlOptions.SetActive(false);
    }

    public void OptionsSound()
    {
        videoOptions.SetActive(false);
        gameplayOptions.SetActive(false);
        soundOptions.SetActive(true);
        controlOptions.SetActive(false);
    }
}
