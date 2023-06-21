using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestQuit : MonoBehaviour
{
    public GameObject targetUI;
    public GameObject targetUI1;
    public GameObject targetUI2;
    TextMeshProUGUI QuitButton;
    void Start()
    {
        QuitButton = GetComponent<TextMeshProUGUI>();
    }

    public void Quit()
    {
        targetUI.SetActive(false);
        targetUI1.SetActive(false);
        targetUI2.SetActive(false);
    }
}
