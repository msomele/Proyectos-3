using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HorizontalToggleController : MonoBehaviour
{
    public TMP_Text currentText;
    public int currentIndex;
    public int defaultIndex;
    public string[] optionsText;
    public int maxIndex, minIndex;

    private void Start()
    {
        currentIndex = defaultIndex;
        currentText.text = optionsText[currentIndex];
        minIndex = 0;
        maxIndex = optionsText.Length-1;
    }
    public void Previous()
    {
        if (currentIndex == 0)
        {
            currentIndex = maxIndex;
        }
        else
        {
            currentIndex--;
        }
        currentText.text = optionsText[currentIndex];
    }

    public void Next()
    {
        if (currentIndex == maxIndex)
        {
            currentIndex = minIndex;
        }
        else
        {
            currentIndex++;
        }
        currentText.text = optionsText[currentIndex];
    }
}
