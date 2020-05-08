using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class CharacterSwiper : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public GameObject characterSelectPanel;
    public GameObject abilityPanel;

    public CharacterSelector characterSelector;

    public Sprite[] characters = new Sprite[2];
    public Image p1Reference;
    public Image p2Reference;
    public TMP_Text p1Text;
    public TMP_Text p2Text;

    public void StartGame()
    {
        StartCoroutine(LoadSceneAsync());   
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Barbarian");
        asyncLoad.allowSceneActivation = true;
        while(!asyncLoad.isDone)
        {
            yield return null;
        }
        //characterSelectPanel.SetActive(false);
        abilityPanel.SetActive(true);
        characterSelector.StartGame(GetCurrentCharacters()[0], player1);
        characterSelector.StartGame(GetCurrentCharacters()[1], player2);
    }


    void ChangeText()
    {
        if (p1Reference.sprite == characters[0])
            p1Text.text = "BARBARIAN";
        if (p1Reference.sprite == characters[1])
            p1Text.text = "MAGICIAN";


        if (p2Reference.sprite == characters[0])
            p2Text.text = "BARBARIAN";
        if (p2Reference.sprite == characters[1])
            p2Text.text = "MAGICIAN";
    }
    public void SwipeLeft(Image character)
    {
        if (character.sprite != characters[0])
            character.sprite = characters[0];
        else
            SwipeRight(character);

        ChangeText();
    }

    public void SwipeRight(Image character)
    {
        if (character.sprite != characters[1])
            character.sprite = characters[1];
        else
            SwipeLeft(character);

        ChangeText();
    }
    public int[] GetCurrentCharacters()
    {
        int[] elecciones = new int[2];

        if (p1Reference.sprite == characters[0])
        {
            Debug.Log("Player 1 chooses Barbarian");
            elecciones[0] = 0;
        }
        else
        {
            Debug.Log("Player 1 chooses magician");
            elecciones[0] = 1;
        }
        if (p2Reference.sprite == characters[0])
        {
            Debug.Log("Player 2 chooses Barbarian");
            elecciones[1] = 0;
        }
        else
        {
            Debug.Log("Player 2 chooses magician");
            elecciones[1] = 1;
        }
        return elecciones;
    }
}
