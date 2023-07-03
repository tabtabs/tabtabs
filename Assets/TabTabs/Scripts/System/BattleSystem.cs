using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TabTabs.NamChanwoo
{

    public class BattleSystem : GameSystem
    {
        // BattleSystem���� ������ �͵�
        // 1. ��������
        // 2. Timebar�� ���ʹ��� ü�� --
        // NodeSheetŬ������ m_NodeType;(����Ű) => ������ ������ ��
        // SpawnSystemŬ������ SpawnNode(GameObject enemy)�Լ� => ��Ŭ��
        // EnemyBaseŬ������ m_nodeQueue����(Queue ���) => GetownNodes�Լ� ������ ��
        //NodeSheet NodeSheetInstance;
        public EnemyBase selectEnemy;
        public CharacterBase CharacterBaseInstance;
        List<EnemyBase> SceneEnemyList = new List<EnemyBase>();
        public ENodeType ClickNode;
        public Button LeftButton;
        public Button UpButton;
        public Button RightButton;
        public PlayerBase PlayerBaseInstance;
        
        void Start()
        {
            ClickNode = ENodeType.Default;
            CharacterBaseInstance = FindObjectOfType<CharacterBase>();
            PlayerBaseInstance = FindObjectOfType<PlayerBase>();
            LeftButton.onClick.AddListener(LeftB);
            UpButton.onClick.AddListener(UpB);
            RightButton.onClick.AddListener(RightB);
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
            if (ClickNode!=ENodeType.Default)
            {// ClickNode�� �߸��� �ƴ϶��(��ư�� Ŭ���ߴٸ�)
                if (ClickNode==selectEnemy.GetOwnNodes().Peek().nodeSheet.m_NodeType)
                {//������ ���� ���Ÿ�԰� ��(���� NodeType�� Ŭ���ߴٸ�)

                    // 1. �ش��ϴ� enemy�� �� destroy
                    // 2. ĳ���Ͱ� �ش��ϴ� enemy�� ����ġ�� �̵� �� ���� �ִϸ��̼� ��� �� ������ġ�� �̵�
                    // 3. �ð����� +
                    CharacterBaseInstance.gameObject.transform.position = new Vector3(selectEnemy.GetOwnNodes().Peek().gameObject.transform.position.x
                    , selectEnemy.GetOwnNodes().Peek().gameObject.transform.position.y,0.0f);
                    PlayerBaseInstance.PlayerAnim.SetTrigger("Atk1_Triger");
                    Destroy(selectEnemy.GetOwnNodes().Peek().gameObject);
                    selectEnemy.GetOwnNodes().Dequeue();
                    selectEnemy.IncreaseAttackGauge(1.0f);

                    if (selectEnemy.GetOwnNodes().Count<=0)
                    {// ���ʹ��� ��尡 0���� �۰ų� ���ٸ�
                        // ���� ���� �� �ٽû���

                        selectEnemy.CurrentState = ECharacterState.Die;
                        
                        //ToDo : ���� ���õ� ���Ͱ� ��������� ���� ���� �ٸ� ���Ͱ� �ִٸ� selectEnemy�� �־������
                    }
                }
                else
                {
                    // �ƴ϶��
                    // 1. ĳ���� Hp --
                    // 2. ĳ���� ������ġ�� �̵�
                    // 3. Enemy�� ĳ���� ���� �ִϸ��̼� ���
                    // 4. ��� �ٽ� �����ϴ� �Լ� ȣ��
                    SpawnSystem.Instance.SpawnNode(selectEnemy);
                }

                ClickNode = ENodeType.Default; // reset
                Debug.Log(ClickNode);
            }
        }

        void LeftB()
        {
            ClickNode = ENodeType.Left;
            Debug.Log(ClickNode);
        }
        void UpB()
        {
            ClickNode = ENodeType.Up;
            Debug.Log(ClickNode);
        }
        void RightB()
        {
            ClickNode = ENodeType.Right;
            Debug.Log(ClickNode);
        }
    }
}