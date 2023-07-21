using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace TabTabs.NamChanwoo
{

    public class Test2SpawnSystem : MonoBehaviour
    {
        // �ν��Ͻ��� ���� ���� ����
        public static Test2SpawnSystem Instance { get; private set; }

        [Header("Spawn Setting")]
        [SerializeField] bool IsSpawnAlignmentRandom = false;
        [SerializeField] public GameObject m_SpawnLocation_Right;
        [SerializeField] public GameObject m_SpawnLocation_Left;
        [SerializeField] List<GameObject> m_NodeList = new List<GameObject>(); // ���Ϳ� ������ ��帮��Ʈ
        [SerializeField] private List<GameObject> m_monsterPrefabList; // ������ ���� ����Ʈ
        public Test2BattleSystem BattleInstance;

        /*[SerializeField] private GameObject m_Enemy;*/


        private void Awake()
        {
            // NodeSpawnManager �ν��Ͻ��� �ϳ��� �ִ��� Ȯ���Ͻʽÿ�.
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // ���� ����: �� �����ڰ� ��� �ε� ���� ���ӵǵ��� �Ϸ���
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            GameManager.NotificationSystem.SceneMonsterDeath.AddListener(HandleSceneMonsterDeath);
            BattleInstance = FindObjectOfType<Test2BattleSystem>();
            // SpawnLocation �ڽ� ��ü ã��
            m_SpawnLocation_Right = transform.Find("SpawnLocation_Right").gameObject;
            m_SpawnLocation_Left = transform.Find("SpawnLocation_Left").gameObject;
            //if (m_SpawnLocation == null)
            //{
            //    Debug.LogError("���� ��ü�� SpawnLocation�� ã�� �� �����ϴ�.");
            //    return;
            //}

            //SpawnMonster();
        }

        private void HandleSceneMonsterDeath(EnemyBase arg0)
        {
            float delayTime = 1.0f;
            StartCoroutine(DelaySpawn(delayTime));
        }

        private IEnumerator DelaySpawn(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            //if (selectMonster.selectEnemy==null)
            //{
            //    EnemySpawn();
            //}
        }

        public void Spawn_RightNode(EnemyBase enemyBase)
        {
            //��尡 0���� �ƴ϶�� ���� �� ��带 �����ϰ� Queue�� ���ϴ�.
            if (enemyBase.GetOwnNodes().Count != 0)
            {
                foreach (Node ownNode in enemyBase.GetOwnNodes())
                {
                    Destroy(ownNode.gameObject);
                }
                enemyBase.GetOwnNodes().Clear();
            }

            NodeArea nodeArea = enemyBase.nodeArea;

            // NodeArea�� �߰ߵǾ����� Ȯ���Ͻʽÿ�.
            if (nodeArea == null)
            {
                Debug.LogError("�� �Ǵ� ���� �ڽĿ��� NodeArea�� ã�� �� �����ϴ�.");
                return;
            }

            // ������ ��� ���� �� ���� �����ϴ�.
            int spawnNodeNum = nodeArea.Rows;

            for (int row = 0; row < spawnNodeNum; row++)
            {
                // �� �࿡�� ���Ƿ� ���� �����մϴ�.
                int randColumn = UnityEngine.Random.Range(0, nodeArea.Columns);

                // ��Ͽ��� ���� �ش��ϴ� ��� �������� �����ɴϴ�.
                GameObject nodePrefab = GetNodePrefab(randColumn);

                // NodeArea���� �� ����� ��ġ�� �����ɴϴ�.
                Vector2 spawnPosition = nodeArea.GetGridPosition(row, randColumn);

                // ���� ��ġ�� ��带 �ν��Ͻ�ȭ�ϰ� nodeArea�� �ڽ����� �����մϴ�.
                GameObject spawnedNode = Instantiate(nodePrefab, spawnPosition, Quaternion.identity, nodeArea.transform);

                // ����� �̸��� �����մϴ�.
                spawnedNode.name = $"Node_{row}_{randColumn}";

                // NodeSheet�� ��带 �ʱ�ȭ�մϴ�.
                Node nodeComponent = spawnedNode.GetComponent<Node>();
                
                nodeComponent.Init_Right();
                
                //���ʹ̰� �����ϴ� ��忡 �߰��մϴ�.
                enemyBase.AddNodes(nodeComponent);
            }
        }
        public void SpawnLeft_Node(EnemyBase enemyBase)
        {
            //��尡 0���� �ƴ϶�� ���� �� ��带 �����ϰ� Queue�� ���ϴ�.
            if (enemyBase.GetOwnNodes().Count != 0)
            {
                foreach (Node ownNode in enemyBase.GetOwnNodes())
                {
                    Destroy(ownNode.gameObject);
                }
                enemyBase.GetOwnNodes().Clear();
            }

            NodeArea nodeArea = enemyBase.nodeArea;

            // NodeArea�� �߰ߵǾ����� Ȯ���Ͻʽÿ�.
            if (nodeArea == null)
            {
                Debug.LogError("�� �Ǵ� ���� �ڽĿ��� NodeArea�� ã�� �� �����ϴ�.");
                return;
            }

            // ������ ��� ���� �� ���� �����ϴ�.
            int spawnNodeNum = nodeArea.Rows;

            for (int row = 0; row < spawnNodeNum; row++)
            {
                // �� �࿡�� ���Ƿ� ���� �����մϴ�.
                int randColumn = UnityEngine.Random.Range(0, nodeArea.Columns);

                // ��Ͽ��� ���� �ش��ϴ� ��� �������� �����ɴϴ�.
                GameObject nodePrefab = GetNodePrefab(randColumn);

                // NodeArea���� �� ����� ��ġ�� �����ɴϴ�.
                Vector2 spawnPosition = nodeArea.GetGridPosition(row, randColumn);

                // ���� ��ġ�� ��带 �ν��Ͻ�ȭ�ϰ� nodeArea�� �ڽ����� �����մϴ�.
                GameObject spawnedNode = Instantiate(nodePrefab, spawnPosition, Quaternion.identity, nodeArea.transform);

                // ����� �̸��� �����մϴ�.
                spawnedNode.name = $"Node_{row}_{randColumn}";

                // NodeSheet�� ��带 �ʱ�ȭ�մϴ�.
                Node nodeComponent = spawnedNode.GetComponent<Node>();

                nodeComponent.Init_Left();

                //���ʹ̰� �����ϴ� ��忡 �߰��մϴ�.
                enemyBase.AddNodes(nodeComponent);
            }
        }
        private GameObject GetNodePrefab(int Column)
        {
            //���� ������ �ƴ϶��
            if (!IsSpawnAlignmentRandom)
            {
                if (Column == 0)
                    return m_NodeList.Find(x => x.GetComponent<Node>().nodeSheet.m_NodeType == ENodeType.Left);
                if (Column == 1)
                    return m_NodeList.Find(x => x.GetComponent<Node>().nodeSheet.m_NodeType == ENodeType.Up);
                if (Column == 2)
                    return m_NodeList.Find(x => x.GetComponent<Node>().nodeSheet.m_NodeType == ENodeType.Right);

                return null;
            }

            //else ���� �����̶��
            return m_NodeList[UnityEngine.Random.Range(0, m_NodeList.Count)];
        }


        //public void SpawnMonster()
        //{
        //    int randomIndex = UnityEngine.Random.Range(0, m_monsterPrefabList.Count);
        //    GameObject monsterPrefab_Left = m_monsterPrefabList[0];
        //    GameObject monsterPrefab_Right = m_monsterPrefabList[1];
            
        //    GameObject spawnMonster_Right = Instantiate(monsterPrefab_Right, m_SpawnLocation_Right.transform.position, Quaternion.identity);

        //    GameObject spawnMonster_Left = Instantiate(monsterPrefab_Left, m_SpawnLocation_Left.transform.position, Quaternion.identity);
        //    Transform buttonTransform = spawnMonster_Left.transform.Find("Button");

        //    if (buttonTransform != null)
        //    {
        //        GameObject buttonObject = buttonTransform.gameObject;
        //        buttonObject.transform.localScale = new Vector3(-0.5f,0.5f,0);
        //    }


        //    EnemyBase spawnEnemy = spawnMonster_Right.GetComponent<EnemyBase>();
        //    EnemyBase spawnEnemy2 = spawnMonster_Left.GetComponent<EnemyBase>();
        //    if (spawnEnemy != null)
        //    {
        //        GameManager.NotificationSystem.SceneMonsterSpawned.Invoke(spawnEnemy); // ���Ͱ� �����Ǿ����� �ý��ۿ� �˸��ϴ�.
        //        SpawnNode(spawnEnemy);
        //    }
            
        //    if (spawnEnemy2 != null)
        //    {
        //        GameManager.NotificationSystem.SceneMonsterSpawned.Invoke(spawnEnemy2); // ���Ͱ� �����Ǿ����� �ý��ۿ� �˸��ϴ�.
        //        SpawnNode(spawnEnemy2);
        //    }
        //}
        //public void RightEnemySpawn()
        //{
        //    GameObject RightMonster = m_monsterPrefabList[1];
        //    GameObject spawnMonster = Instantiate(RightMonster, m_SpawnLocation_Right.transform.position, Quaternion.identity);
        //    EnemyBase spawnEnemy = spawnMonster.GetComponent<EnemyBase>();
        //    if (spawnEnemy != null)
        //    {
        //        //GameManager.NotificationSystem.SceneMonsterSpawned.Invoke(spawnEnemy); // ���Ͱ� �����Ǿ����� �ý��ۿ� �˸��ϴ�.
        //        SpawnNode(spawnEnemy);
        //    }
        //}
    }
}



