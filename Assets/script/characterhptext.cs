using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class characterhptext : MonoBehaviour
{
    TextMeshProUGUI characterHpText;
    int HpText;
    void Start()
    {
        characterHpText = GetComponent<TextMeshProUGUI>();
    }


    void Update()
    {
        if (Gamemanager.Instance.characterHp != HpText)
        {
            HpText = Gamemanager.Instance.characterHp;
            characterHpText.text = "x"+HpText.ToString();
        }
    }
}
