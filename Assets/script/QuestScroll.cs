using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestScroll : MonoBehaviour
{
    public GameObject Scroll;
    TextMeshProUGUI Button;
    void Start()
    {
        Button = GetComponent<TextMeshProUGUI>();
    }

    public void ScrollOpen()
    {
        Scroll.SetActive(!Scroll.activeSelf);
    }
}
