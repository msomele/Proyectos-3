using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class CharacterSwiper : MonoBehaviour
{
    public string Scene2LoadName;
    public GameObject player1;
    public GameObject player2;
    public int[] elecciones = new int[2];
    public GameObject characterSelectPanel;
    public GameObject abilityPanel;

    public CharacterSelector characterSelector;
    public GameObject Barbarian3DModelP1;
    public GameObject Magician3DModelP1;

    public GameObject Barbarian3DModelP2;
    public GameObject Magician3DModelP2;

    public Sprite[] characters = new Sprite[2];
    public Image p1Reference;
    public Image p2Reference;
    public TMP_Text p1Text;
    public TMP_Text p2Text;

    public void StartGame()
    {
        elecciones = GetCurrentCharacters();
        StartCoroutine(LoadSceneAsync());   
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Scene2LoadName);
        asyncLoad.allowSceneActivation = true;
        while(!asyncLoad.isDone)
        {
            yield return null;
        }
        abilityPanel.SetActive(true);
        characterSelectPanel.SetActive(false);
        characterSelector.StartGame(elecciones[0], player1);
        characterSelector.StartGame(elecciones[1], player2);
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

    public void SwipeLeftP1(Image character)
    {
        if (character.sprite != characters[0])
        {
            character.sprite = characters[0]; //0 barbarian 1magician
            Barbarian3DModelP1.SetActive(true);
            Magician3DModelP1.SetActive(false);
        }
        else
            SwipeRightP1(character);

        ChangeText();
    }

    public void SwipeRightP1(Image character)
    {
        if (character.sprite != characters[1])
        {
            character.sprite = characters[1];
            Barbarian3DModelP1.SetActive(false);
            Magician3DModelP1.SetActive(true);

        }
        else
            SwipeLeftP1(character);

        ChangeText();
    }

    public void SwipeLeftP2(Image character)
    {
        if (character.sprite != characters[0])
        {
            character.sprite = characters[0]; //0 barbarian 1magician
            Barbarian3DModelP2.SetActive(true);
            Magician3DModelP2.SetActive(false);
        }
        else
            SwipeRightP2(character);

        ChangeText();
    }

    public void SwipeRightP2(Image character)
    {
        if (character.sprite != characters[1])
        {
            character.sprite = characters[1];
            Barbarian3DModelP2.SetActive(false);
            Magician3DModelP2.SetActive(true);

        }
        else
            SwipeLeftP2(character);

        ChangeText();
    }


    public int[] GetCurrentCharacters()
    {

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
