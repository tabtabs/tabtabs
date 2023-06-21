using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftButton : MonoBehaviour
{
    int LeftNum = 0;
    private timebar timebar;
    private void Awake()
    {
        timebar = FindObjectOfType<timebar>();
    }
    public void LeftButtonClick()
    {
        if (Gamemanager.Instance.decideAttack==false)
        {// 오른쪽 적을 공격
            switch (Gamemanager.Instance.line)
            {
                case line.LINE0:
                    if (Gamemanager.Instance.tabs[0] == LeftNum)
                    {// tabs 리스트에서 저장된 첫번째 값이 LeftNum값과 일치하다면
                        Destroy(Gamemanager.Instance.ranobj[0]);
                        // 추후에 이미지변경 or 애니메이션 추가 예정
                        Gamemanager.Instance.line = line.LINE1;
                        // 1번째 라인으로 상태변경
                        timebar.timeer += 1.0f;
                        // 시간 추가
                        Gamemanager.Instance.enemyHp--;
                    }
                    else
                    {
                        Gamemanager.Instance.characterHp -= 10;
                    }
                    break;
                case line.LINE1:
                    if (Gamemanager.Instance.tabs[1] == LeftNum)
                    {
                        Destroy(Gamemanager.Instance.ranobj[1]);
                        Gamemanager.Instance.line = line.LINE2;
                        timebar.timeer += 1.0f;
                        Gamemanager.Instance.enemyHp--;
                    }
                    else
                    {
                        Gamemanager.Instance.characterHp -= 10;
                    }
                    break;
                case line.LINE2:
                    if (Gamemanager.Instance.tabs[2] == LeftNum)
                    {
                        Destroy(Gamemanager.Instance.ranobj[2]);
                        Gamemanager.Instance.line = line.LINE3;
                        timebar.timeer += 1.0f;
                        Gamemanager.Instance.enemyHp--;
                    }
                    else
                    {
                        Gamemanager.Instance.characterHp -= 10;
                    }
                    break;
                case line.LINE3:
                    if (Gamemanager.Instance.tabs[3] == LeftNum)
                    {
                        Destroy(Gamemanager.Instance.ranobj[3]);
                        Gamemanager.Instance.line = line.LINE4;
                        timebar.timeer += 1.0f;
                        Gamemanager.Instance.enemyHp--;
                    }
                    else
                    {
                        Gamemanager.Instance.characterHp -= 10;
                    }
                    break;
                case line.LINE4:
                    if (Gamemanager.Instance.tabs[4] == LeftNum)
                    {
                        Destroy(Gamemanager.Instance.ranobj[4]);
                        Gamemanager.Instance.line = line.LINE5;
                        timebar.timeer += 1.0f;
                        Gamemanager.Instance.enemyHp--;
                    }
                    else
                    {
                        Gamemanager.Instance.characterHp -= 10;
                    }
                    break;
                case line.LINE5:
                    if (Gamemanager.Instance.tabs[5] == LeftNum)
                    {
                        Destroy(Gamemanager.Instance.ranobj[5]);
                        Gamemanager.Instance.line = line.LINE6;
                        timebar.timeer += 1.0f;
                        Gamemanager.Instance.enemyHp--;
                    }
                    else
                    {
                        Gamemanager.Instance.characterHp -= 10;
                    }
                    break;
                case line.LINE6:
                    if (Gamemanager.Instance.tabs[6] == LeftNum)
                    {
                        Destroy(Gamemanager.Instance.ranobj[6]);
                        Gamemanager.Instance.line = line.LINE0;
                        timebar.timeer += 1.0f;
                        Gamemanager.Instance.enemyHp--;
                        Gamemanager.Instance.tabs.Clear();

                    }
                    else
                    {
                        Gamemanager.Instance.characterHp -= 10;
                    }
                    break;
                default:
                    break;
            }
        }
        else // decideAttack == true (왼쪽적을 공격할 차례라면)
        {
            switch (Gamemanager.Instance.line)
            {
                case line.LINE0:
                    if (Gamemanager.Instance.tabs2[0] == LeftNum)
                    {// tabs 리스트에서 저장된 첫번째 값이 LeftNum값과 일치하다면
                        Destroy(Gamemanager.Instance.ranobj2[0]);
                        // 추후에 이미지변경 or 애니메이션 추가 예정
                        Gamemanager.Instance.line = line.LINE1;
                        // 1번째 라인으로 상태변경
                        timebar.timeer += 1.0f;
                        // 시간 추가
                        Gamemanager.Instance.enemyHp2--;
                    }
                    else
                    {
                        Gamemanager.Instance.characterHp -= 10;
                    }
                    break;
                case line.LINE1:
                    if (Gamemanager.Instance.tabs2[1] == LeftNum)
                    {
                        Destroy(Gamemanager.Instance.ranobj2[1]);
                        Gamemanager.Instance.line = line.LINE2;
                        timebar.timeer += 1.0f;
                        Gamemanager.Instance.enemyHp2--;
                    }
                    else
                    {
                        Gamemanager.Instance.characterHp -= 10;
                    }
                    break;
                case line.LINE2:
                    if (Gamemanager.Instance.tabs2[2] == LeftNum)
                    {
                        Destroy(Gamemanager.Instance.ranobj2[2]);
                        Gamemanager.Instance.line = line.LINE3;
                        timebar.timeer += 1.0f;
                        Gamemanager.Instance.enemyHp2--;
                    }
                    else
                    {
                        Gamemanager.Instance.characterHp -= 10;
                    }
                    break;
                case line.LINE3:
                    if (Gamemanager.Instance.tabs2[3] == LeftNum)
                    {
                        Destroy(Gamemanager.Instance.ranobj2[3]);
                        Gamemanager.Instance.line = line.LINE4;
                        timebar.timeer += 1.0f;
                        Gamemanager.Instance.enemyHp2--;
                    }
                    else
                    {
                        Gamemanager.Instance.characterHp -= 10;
                    }
                    break;
                case line.LINE4:
                    if (Gamemanager.Instance.tabs2[4] == LeftNum)
                    {
                        Destroy(Gamemanager.Instance.ranobj2[4]);
                        Gamemanager.Instance.line = line.LINE5;
                        timebar.timeer += 1.0f;
                        Gamemanager.Instance.enemyHp2--;
                    }
                    else
                    {
                        Gamemanager.Instance.characterHp -= 10;
                    }
                    break;
                case line.LINE5:
                    if (Gamemanager.Instance.tabs2[5] == LeftNum)
                    {
                        Destroy(Gamemanager.Instance.ranobj2[5]);
                        Gamemanager.Instance.line = line.LINE6;
                        timebar.timeer += 1.0f;
                        Gamemanager.Instance.enemyHp2--;
                    }
                    else
                    {
                        Gamemanager.Instance.characterHp -= 10;
                    }
                    break;
                case line.LINE6:
                    if (Gamemanager.Instance.tabs2[6] == LeftNum)
                    {
                        Destroy(Gamemanager.Instance.ranobj2[6]);
                        Gamemanager.Instance.line = line.LINE0;
                        timebar.timeer += 1.0f;
                        Gamemanager.Instance.enemyHp2--;
                        Gamemanager.Instance.tabs2.Clear();

                    }
                    else
                    {
                        Gamemanager.Instance.characterHp -= 10;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
