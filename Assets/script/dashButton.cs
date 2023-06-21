using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dashButton : MonoBehaviour
{
    public GameObject character;
    
    public void dash()
    {
        if (Gamemanager.Instance.decideAttack==false)
        {// 왼쪽 공격상태라면
            character.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            character.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
