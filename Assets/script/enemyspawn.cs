using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyspawn : MonoBehaviour
{
    Vector3 targetPos = new Vector3(0.7f, -1.2f, 0);// ��������
    Vector3 spawnPos = new Vector3(4, -1.2f, 0); // ��������
    void Start()
    {
        transform.position = spawnPos; // �������� ��ǥ
    }

    
    void Update()
    {
        if (Vector3.Distance(targetPos,transform.position)>0.2f)
        {// ���������� ���������� ���̰� 0.2f���� ũ�ٸ�
            transform.Translate(Vector3.left * 1.0f * Time.deltaTime);
            // �̵�
        }

        if (Gamemanager.Instance.enemyHp==0)
        {// Ʈ����
            Gamemanager.Instance.enemyHp = 7;
            transform.position = spawnPos; // �ٽ� ���� ��ġ�� �̵�
            Gamemanager.Instance.ranObjOrkRight();
            Gamemanager.Instance.decideAttack = true;
        }
    }
}
