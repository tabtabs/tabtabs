using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    private static Gamemanager instance = null;
    public static Gamemanager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject[] objects; // 생성할 오브젝트를 저장할 배열
    public GameObject[] ranobj = new GameObject[7]; // 뽑힌 오브젝트를 저장할 오브젝트 변수 ork_right
    public GameObject[] ranobj2 = new GameObject[7]; // ork_left
    public GameObject[] rows; // 각 행의 빈 오브젝트(행) ork_right
    public GameObject[] rows2; // ork_left
    public GameObject ork;
    public bool decideAttack = false; // 오른쪽 오크가 죽었을때 true 상태로 변경 -> 왼쪽 오크 공격모드 on
    public int characterHp = 70;
    public line line = line.LINE0;
    public int enemyHp = 7;
    public int enemyHp2 = 7;
    [Header("뽑힌 tab 목록")]
    public List<int> tabs = new List<int>(); // ork_right
    public List<int> tabs2 = new List<int>(); // ork_left
    

    void Start()
    {
        ranObjOrkRight();
        ranObjOrkLeft();
    }

    public void ranObjOrkRight()
    {
        for (int i = 0; i < rows.Length; i++)
        {
            int ran = Random.Range(0, 3); // 정수의 range = (0,3) : 0~2까지의 랜덤인덱스가 뽑힘
            tabs.Add(ran); // 뽑힌 ran값을 tabs(int)List에 저장
            if (ran == 0)
            {// 0번쨰 오브젝트가 뽑혔다면
                ranobj[i] = Instantiate(objects[ran],new Vector3(rows[i].transform.position.x-0.2f, rows[i].transform.position.y, 0), Quaternion.identity);
                ranobj[i].transform.parent = rows[i].transform;
                
            }
            else if (ran == 1)
            {// 1
                ranobj[i] = Instantiate(objects[ran],new Vector3( rows[i].transform.position.x, rows[i].transform.position.y, 0), Quaternion.identity);
                ranobj[i].transform.parent = rows[i].transform;

            }
            else
            {// 2
                ranobj[i] = Instantiate(objects[ran], new Vector3(rows[i].transform.position.x+0.2f, rows[i].transform.position.y, 0), Quaternion.identity);
                ranobj[i].transform.parent = rows[i].transform;
            }
        }
    }

    public void ranObjOrkLeft()
    {
        for (int i = 0; i < rows2.Length; i++)
        {
            int ran = Random.Range(0, 3); // 정수의 range = (0,3) : 0~2까지의 랜덤인덱스가 뽑힘
            tabs2.Add(ran); // 뽑힌 ran값을 tabs(int)List에 저장
            if (ran == 0)
            {// 0번쨰 오브젝트가 뽑혔다면
                ranobj2[i] = Instantiate(objects[ran], new Vector3(rows2[i].transform.position.x - 0.2f, rows2[i].transform.position.y, 0), Quaternion.identity);
                ranobj2[i].transform.parent = rows2[i].transform;

            }
            else if (ran == 1)
            {// 1
                ranobj2[i] = Instantiate(objects[ran], new Vector3(rows2[i].transform.position.x, rows2[i].transform.position.y, 0), Quaternion.identity);
                ranobj2[i].transform.parent = rows2[i].transform;

            }
            else
            {// 2
                ranobj2[i] = Instantiate(objects[ran], new Vector3(rows2[i].transform.position.x + 0.2f, rows2[i].transform.position.y, 0), Quaternion.identity);
                ranobj2[i].transform.parent = rows2[i].transform;
            }
        }
    }
}
