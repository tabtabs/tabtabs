using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestButton : MonoBehaviour
{
    public TextMeshProUGUI Button;
    public GameObject targetUI;
    public GameObject targetUI2;
    void Start()
    {
        Button = GetComponent<TextMeshProUGUI>();
    }

    public void Quest()
    {
        targetUI.SetActive(!targetUI.activeSelf);
        targetUI2.SetActive(!targetUI2.activeSelf);
    }
}
