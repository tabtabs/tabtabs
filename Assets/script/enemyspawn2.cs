using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyspawn2 : MonoBehaviour
{
    Vector3 targetPos = new Vector3(-0.8f, -1.2f, 0);// 도착지점
    Vector3 spawnPos = new Vector3(-4, -1.2f, 0); // 생성지점
    void Start()
    {
        transform.position = spawnPos; // 생성지점 좌표
    }


    void Update()
    {
        if (Vector3.Distance(targetPos, transform.position) > 0.2f)
        {// 도착지점과 생성지점의 차이가 0.2f보다 크다면
            transform.Translate(Vector3.right * 1.0f * Time.deltaTime);
            // 이동
        }

        if (Gamemanager.Instance.enemyHp2 == 0)
        {// 트루라면
            Gamemanager.Instance.enemyHp2 = 7;
            transform.position = spawnPos; // 다시 원래 위치로 이동
            Gamemanager.Instance.ranObjOrkLeft();
            Gamemanager.Instance.decideAttack = false;
        }
    }
}
