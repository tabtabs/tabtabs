using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Animations;

namespace TabTabs.NamChanwoo
{
    public class Test2BattleSystem : GameSystem
    {
        // BattleSystem���� ������ �͵�
        // 1. ��������
        // 2. Timebar�� ���ʹ��� ü�� --
        // NodeSheetŬ������ m_NodeType;(����Ű) => ������ ������ ��
        // SpawnSystemŬ������ SpawnNode(GameObject enemy)�Լ� => ��Ŭ��
        // EnemyBaseŬ������ m_nodeQueue����(Queue ���) => GetownNodes�Լ� ������ ��
        //NodeSheet NodeSheetInstance;
        public EnemyBase selectEnemy;
        public EnemyBase LeftEnemy;
        public EnemyBase RightEnemy;
        public CharacterBase CharacterBaseInstance;
        public List<EnemyBase> SceneEnemyList = new List<EnemyBase>();
        public ENodeType ClickNode;
        public Button LeftButton;
        public Button UpButton;
        public Button RightButton;
        public Button SelectEnemyButton;
        public PlayerBase PlayerBaseInstance;
        public Player_Effect Player_EffectInstance;
        public GameObject Player_Effect;
        public GameObject Left_Ork;
        public GameObject Right_Ork;
        public bool MonsterDie;
        public Node NodeInstance;


        void Start()
        {
            ClickNode = ENodeType.Default;
            CharacterBaseInstance = FindObjectOfType<CharacterBase>();
            PlayerBaseInstance = FindObjectOfType<PlayerBase>();
            Player_EffectInstance = FindObjectOfType<Player_Effect>();
            LeftButton.onClick.AddListener(LeftB);
            UpButton.onClick.AddListener(UpB);
            RightButton.onClick.AddListener(RightB);
            SelectEnemyButton.onClick.AddListener(SelectEnemy);
            StartSpawn();
            MonsterDie = false;
            NodeInstance = FindObjectOfType<Node>();
        }

        public override void OnSystemInit()
        {
            GameManager.NotificationSystem.SceneMonsterSpawned.AddListener(HandleSceneMonsterSpawned);
        }

        private void HandleSceneMonsterSpawned(EnemyBase spawnedEnemy)
        {
            spawnedEnemy.SetupAttackSliderUI(GameManager.UISystem.AttackSliderUI);

            if (selectEnemy == null)
                selectEnemy = spawnedEnemy;

            SceneEnemyList.Add(spawnedEnemy);
        }

        void Update()
        {
            if (ClickNode != ENodeType.Default)
            {// ClickNode�� �߸��� �ƴ϶��(��ư�� Ŭ���ߴٸ�)

                if (selectEnemy == null) { return; }

                if (ClickNode == selectEnemy.GetOwnNodes().Peek().nodeSheet.m_NodeType)
                {//������ ���� ���Ÿ�԰� ��(���� NodeType�� Ŭ���ߴٸ�)

                    GameManager.NotificationSystem.NodeHitSuccess?.Invoke();

                    // 1. �ش��ϴ� enemy�� �� destroy
                    // 2. ĳ���Ͱ� �ش��ϴ� enemy�� ����ġ�� �̵� �� ���� �ִϸ��̼� ��� �� ������ġ�� �̵�
                    // 3. �ð����� +

                    CharacterBaseInstance.gameObject.transform.position = new Vector3(selectEnemy.GetOwnNodes().Peek().gameObject.transform.position.x
                    , selectEnemy.GetOwnNodes().Peek().gameObject.transform.position.y, 0.0f);
                    PlayerBaseInstance.PlayerAnim.SetTrigger("Atk1_Triger"); // ��ũ�� ��ġ�� �̵��� ���ݸ��
                    Vector3 targetPosition = selectEnemy.GetOwnNodes().Peek().gameObject.transform.position;
                    Instantiate(Player_Effect, targetPosition, Quaternion.identity);
                    Destroy(selectEnemy.GetOwnNodes().Peek().gameObject);
                    
                    selectEnemy.GetOwnNodes().Dequeue();
                    selectEnemy.Hit();

                    if (selectEnemy.GetOwnNodes().Count <= 0)
                    {
                        // ���ʹ� ����� �����ִ� ������ 0���� �۰ų� ���ٸ�
                        // ���� ���� �� �ٽû���
                        
                        //if (selectEnemy.gameObject==SceneEnemyList[0])
                        //{
                        //    SceneEnemyList.RemoveAt(0);
                        //}
                        //else
                        //{
                        //    SceneEnemyList.RemoveAt(1);
                        //}
                        selectEnemy.Die();

                        if (MonsterDie)
                        {// ������ ���Ͱ� �������¶��
                            RightMonsterSpawn();
                            // ���� ��
                            MonsterDie = false;
                        }
                        else
                        {
                            LeftMonsterSpawn();
                        }
                        //HandleSceneMonsterSpawned(SceneEnemyList[0]);
                        
                        //if (SceneEnemyList.Count > 0)
                        //{
                        //    selectEnemy = SceneEnemyList[0];
                        //    selectEnemy.SetupAttackSliderUI(GameManager.UISystem.AttackSliderUI);
                        //}
                    }
                }
                else
                {
                    GameManager.NotificationSystem.NodeHitFail?.Invoke();

                    if (selectEnemy != null)
                    {
                        selectEnemy.Attack();
                    }

                    // �ƴ϶��
                    // 1. ĳ���� Hp --
                    // 2. ĳ���� ������ġ�� �̵�
                    // 3. Enemy�� ĳ���� ���� �ִϸ��̼� ���
                    // 4. ��� �ٽ� �����ϴ� �Լ� ȣ��
                    if (selectEnemy==RightEnemy)
                    {
                        Test2SpawnSystem.Instance.Spawn_RightNode(selectEnemy);
                    }
                    else if (selectEnemy==LeftEnemy)
                    {
                        Test2SpawnSystem.Instance.SpawnLeft_Node(selectEnemy);
                    }
                }

                ClickNode = ENodeType.Default; // reset
                Debug.Log(ClickNode);
            }
        }

        void LeftB()
        {
            ClickNode = ENodeType.Left;
        }
        void UpB()
        {
            ClickNode = ENodeType.Up;
        }
        void RightB()
        {
            ClickNode = ENodeType.Right;
        }

        void SelectEnemy()
        {
            if (selectEnemy == RightEnemy)
            {
                selectEnemy = LeftEnemy;
                Debug.Log("selectEnemyBu");
                CharacterBaseInstance.gameObject.transform.position = new Vector3(selectEnemy.GetOwnNodes().Peek().gameObject.transform.position.x
                    , selectEnemy.GetOwnNodes().Peek().gameObject.transform.position.y, 0.0f);
                PlayerBaseInstance.PlayerAnim.SetTrigger("Atk1_Triger"); // ��ũ�� ��ġ�� �̵��� ���ݸ��
                Vector3 targetPosition = selectEnemy.GetOwnNodes().Peek().gameObject.transform.position;
                Instantiate(Player_Effect, targetPosition, Quaternion.identity);
                Destroy(selectEnemy.GetOwnNodes().Peek().gameObject);

                selectEnemy.GetOwnNodes().Dequeue();
                selectEnemy.Hit();
                PlayerBaseInstance.PlayerTransform.localScale = 
                new Vector3(-1f, PlayerBaseInstance.PlayerTransform.localScale.y, PlayerBaseInstance.PlayerTransform.localScale.z);
            }
            else if (selectEnemy == LeftEnemy)
            {
                Debug.Log("selectEnemyBu");
                selectEnemy = RightEnemy;
                PlayerBaseInstance.PlayerTransform.localScale =
                new Vector3(1f, PlayerBaseInstance.PlayerTransform.localScale.y, PlayerBaseInstance.PlayerTransform.localScale.z);
            }
        }

        public void StartSpawn()
        {
            GameObject RightMonster = Right_Ork;
            GameObject RightSpawnMonster = Instantiate(RightMonster,new Vector3(4.0f,1.5f,0),Quaternion.identity);
            EnemyBase spawnEnemy = RightSpawnMonster.GetComponent<EnemyBase>();
            if (spawnEnemy != null)
            {
                //GameManager.NotificationSystem.SceneMonsterSpawned.Invoke(spawnEnemy); // ���Ͱ� �����Ǿ����� �ý��ۿ� �˸��ϴ�.
                Test2SpawnSystem.Instance.Spawn_RightNode(spawnEnemy);
            }
            RightEnemy = spawnEnemy; // �� ����
            selectEnemy = spawnEnemy; // ������ ���Ͱ� ����Ʈ��

            GameObject LefttMonster = Left_Ork;
            GameObject LeftSpawnMonster = Instantiate(LefttMonster,new Vector3(-4.0f,1.5f,0),Quaternion.identity);
            EnemyBase spawnEnemy2 = LeftSpawnMonster.GetComponent<EnemyBase>();
            if (spawnEnemy2 != null)
            {
                //GameManager.NotificationSystem.SceneMonsterSpawned.Invoke(spawnEnemy); // ���Ͱ� �����Ǿ����� �ý��ۿ� �˸��ϴ�.
                Test2SpawnSystem.Instance.SpawnLeft_Node(spawnEnemy2);
            }
            LeftEnemy = spawnEnemy2; // �� ����
        }
        public void RightMonsterSpawn()
        {
            GameObject RightMonster = Right_Ork;
            GameObject RightSpawnMonster = Instantiate(RightMonster, new Vector3(4.0f, 1.5f, 0), Quaternion.identity);
            EnemyBase spawnEnemy = RightSpawnMonster.GetComponent<EnemyBase>();
            if (spawnEnemy != null)
            {
                //GameManager.NotificationSystem.SceneMonsterSpawned.Invoke(spawnEnemy); // ���Ͱ� �����Ǿ����� �ý��ۿ� �˸��ϴ�.
                Test2SpawnSystem.Instance.Spawn_RightNode(spawnEnemy);
            }
            selectEnemy = spawnEnemy;
            RightEnemy = spawnEnemy;
        }
        public void LeftMonsterSpawn()
        {
            GameObject LefttMonster = Left_Ork;
            GameObject LeftSpawnMonster = Instantiate(LefttMonster, new Vector3(-4.0f, 1.5f, 0), Quaternion.identity);
            EnemyBase spawnEnemy2 = LeftSpawnMonster.GetComponent<EnemyBase>();
            if (spawnEnemy2 != null)
            {
                //GameManager.NotificationSystem.SceneMonsterSpawned.Invoke(spawnEnemy); // ���Ͱ� �����Ǿ����� �ý��ۿ� �˸��ϴ�.
                Test2SpawnSystem.Instance.SpawnLeft_Node(spawnEnemy2);
            }
            selectEnemy = spawnEnemy2;
            LeftEnemy = spawnEnemy2;
        }
    }
}


