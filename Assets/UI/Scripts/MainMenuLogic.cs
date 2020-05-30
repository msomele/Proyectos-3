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
    [Header("CharactersMenu")]
    public GameObject SelectPlayers;
    [Header("Animations")]
    public Animator credits;


    private void Start()
    {if (SceneManager.GetActiveScene().name != ("Scenario"))
            BackToMainMenu();
        else
            SelectPlayers = GameObject.FindObjectOfType<CharacterSelector>().gameObject;
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
    public void ShowCharacerSelector()
    {
        MainMenu.SetActive(false);
        SelectPlayers.SetActive(true);
    }
    public void ShowCredits()
    {
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
        OptionsMenu.SetActive(false);
        credits.Play("Credits");
    }
    public void StartGame()
    {
        MainMenu.SetActive(false);
        SelectPlayers.SetActive(false);
        this.GetComponent<Animator>().SetTrigger("PlayButton");
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


    ////CUTRES
    ///
    public void ResumeGame()
    {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        Camera.main.GetComponentInChildren<PostProcessingRealtimeChanger>().ChangeFov(20);
    }
    public void PauseGame()
    {
        MainMenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }
    public void GoToMainMenu(string Scene2LoadName)
    {
        Destroy(SelectPlayers);
        SceneManager.LoadScene(Scene2LoadName);
    }

    public void CutrePause()
    {
        if (Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name == ("Scenario"))
        {
            BackToMainMenu();
            Camera.main.GetComponentInChildren<PostProcessingRealtimeChanger>().ChangeFov(0);
        }
    }
    private void Update()
    {
        CutrePause();
    }

}
