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

    public GameObject[] objects; // ������ ������Ʈ�� ������ �迭
    public GameObject[] ranobj = new GameObject[7]; // ���� ������Ʈ�� ������ ������Ʈ ���� ork_right
    public GameObject[] ranobj2 = new GameObject[7]; // ork_left
    public GameObject[] rows; // �� ���� �� ������Ʈ(��) ork_right
    public GameObject[] rows2; // ork_left
    public GameObject ork;
    public bool decideAttack = false; // ������ ��ũ�� �׾����� true ���·� ���� -> ���� ��ũ ���ݸ�� on
    public int characterHp = 70;
    public line line = line.LINE0;
    public int enemyHp = 7;
    public int enemyHp2 = 7;
    [Header("���� tab ���")]
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
            int ran = Random.Range(0, 3); // ������ range = (0,3) : 0~2������ �����ε����� ����
            tabs.Add(ran); // ���� ran���� tabs(int)List�� ����
            if (ran == 0)
            {// 0���� ������Ʈ�� �����ٸ�
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
            int ran = Random.Range(0, 3); // ������ range = (0,3) : 0~2������ �����ε����� ����
            tabs2.Add(ran); // ���� ran���� tabs(int)List�� ����
            if (ran == 0)
            {// 0���� ������Ʈ�� �����ٸ�
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
