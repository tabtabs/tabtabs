using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyspawn : MonoBehaviour
{
    Vector3 targetPos = new Vector3(0.7f, -1.2f, 0);// 도착지점
    Vector3 spawnPos = new Vector3(4, -1.2f, 0); // 생성지점
    void Start()
    {
        transform.position = spawnPos; // 생성지점 좌표
    }

    
    void Update()
    {
        if (Vector3.Distance(targetPos,transform.position)>0.2f)
        {// 도착지점과 생성지점의 차이가 0.2f보다 크다면
            transform.Translate(Vector3.left * 1.0f * Time.deltaTime);
            // 이동
        }

        if (Gamemanager.Instance.enemyHp==0)
        {// 트루라면
            Gamemanager.Instance.enemyHp = 7;
            transform.position = spawnPos; // 다시 원래 위치로 이동
            Gamemanager.Instance.ranObjOrkRight();
            Gamemanager.Instance.decideAttack = true;
        }
    }
}
