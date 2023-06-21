using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upButton : MonoBehaviour
{
    int upNum = 1;
    private timebar timebar;
    private void Awake()
    {
        timebar = FindObjectOfType<timebar>();
    }
    public void upButtonClick()
    {
        if (Gamemanager.Instance.decideAttack == false)
        {// ������ ���� ����
            switch (Gamemanager.Instance.line)
            {
                case line.LINE0:
                    if (Gamemanager.Instance.tabs[0] == upNum)
                    {// tabs ����Ʈ���� ����� ù��° ���� LeftNum���� ��ġ�ϴٸ�
                        Destroy(Gamemanager.Instance.ranobj[0]);
                        // ���Ŀ� �̹������� or �ִϸ��̼� �߰� ����
                        Gamemanager.Instance.line = line.LINE1;
                        // 1��° �������� ���º���
                        timebar.timeer += 1.0f;
                        // �ð� �߰�
                        Gamemanager.Instance.enemyHp--;
                    }
                    else
                    {
                        Gamemanager.Instance.characterHp -= 10;
                    }
                    break;
                case line.LINE1:
                    if (Gamemanager.Instance.tabs[1] == upNum)
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
                    if (Gamemanager.Instance.tabs[2] == upNum)
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
                    if (Gamemanager.Instance.tabs[3] == upNum)
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
                    if (Gamemanager.Instance.tabs[4] == upNum)
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
                    if (Gamemanager.Instance.tabs[5] == upNum)
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
                    if (Gamemanager.Instance.tabs[6] == upNum)
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
        else // decideAttack == true (�������� ������ ���ʶ��)
        {
            switch (Gamemanager.Instance.line)
            {
                case line.LINE0:
                    if (Gamemanager.Instance.tabs2[0] == upNum)
                    {// tabs ����Ʈ���� ����� ù��° ���� LeftNum���� ��ġ�ϴٸ�
                        Destroy(Gamemanager.Instance.ranobj2[0]);
                        // ���Ŀ� �̹������� or �ִϸ��̼� �߰� ����
                        Gamemanager.Instance.line = line.LINE1;
                        // 1��° �������� ���º���
                        timebar.timeer += 1.0f;
                        // �ð� �߰�
                        Gamemanager.Instance.enemyHp2--;
                    }
                    else
                    {
                        Gamemanager.Instance.characterHp -= 10;
                    }
                    break;
                case line.LINE1:
                    if (Gamemanager.Instance.tabs2[1] == upNum)
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
                    if (Gamemanager.Instance.tabs2[2] == upNum)
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
                    if (Gamemanager.Instance.tabs2[3] == upNum)
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
                    if (Gamemanager.Instance.tabs2[4] == upNum)
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
                    if (Gamemanager.Instance.tabs2[5] == upNum)
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
                    if (Gamemanager.Instance.tabs2[6] == upNum)
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
