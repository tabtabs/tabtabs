using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TabTabs.NamChanwoo
{

    public class BattleSystem : MonoBehaviour
    {
        // BattleSystem���� ������ �͵�
        // 1. ��������
        // 2. Timebar�� ���ʹ��� ü�� --
        // NodeSheetŬ������ m_NodeType;(����Ű) => ������ ������ ��
        // SpawnSystemŬ������ SpawnNode(GameObject enemy)�Լ� => ��Ŭ��
        // EnemyBaseŬ������ m_nodeQueue����(Queue ���) => GetownNodes�Լ� ������ ��
        //NodeSheet NodeSheetInstance;
        EnemyBase EnemyBaseInstance;
        public ENodeType ClickNode;
        public Button LeftButton;
        public Button UpButton;
        public Button RightButton;
        void Start()
        {
            EnemyBaseInstance = FindObjectOfType<EnemyBase>();
            ClickNode = ENodeType.Default;

            if (LeftButton==null)
            {
               LeftButton = GetComponent<Button>();
            }
            if (UpButton==null)
            {
               UpButton = GetComponent<Button>();
            }
            if (RightButton==null)
            {   
               RightButton = GetComponent<Button>();
            }

            LeftButton.onClick.AddListener(LeftB);
            //UpButton = GetComponent<Button>();
            UpButton.onClick.AddListener(UpB);

            //RightButton = GetComponent<Button>();
            RightButton.onClick.AddListener(RightB);
        }
        

        void Update()
        {
            if (ClickNode!=ENodeType.Default)
            {// ClickNode�� �߸��� �ƴ϶��(��ư�� Ŭ���ߴٸ�)
                if (ClickNode==EnemyBaseInstance.GetOwnNodes().Peek().nodeSheet.m_NodeType)
                {//������ ���� ���Ÿ�԰� ��(���� NodeType�� Ŭ���ߴٸ�)

                    // 1. �ش��ϴ� enemy�� �� destroy
                    // 2. ĳ���Ͱ� �ش��ϴ� enemy�� ����ġ�� �̵� �� ���� �ִϸ��̼� ��� �� ������ġ�� �̵�
                    // 3. �ð����� +
                    Destroy(EnemyBaseInstance.GetOwnNodes().Peek().gameObject);
                    EnemyBaseInstance.GetOwnNodes().Dequeue();
                    // m_AttackGauge+

                    if (EnemyBaseInstance.GetOwnNodes().Count<=0)
                    {// ���ʹ��� ��尡 0���� �۰ų� ���ٸ�
                        // ���� ���� �� �ٽû���
                        Destroy(SpawnSystem.Instance.Enemy);
                    }
                }
                else
                {// �ƴ϶��
                    // 1. ĳ���� Hp --
                    // 2. ĳ���� ������ġ�� �̵�
                    // 3. Enemy�� ĳ���� ���� �ִϸ��̼� ���
                    // 4. ��� �ٽ� �����ϴ� �Լ� ȣ��
                    SpawnSystem.Instance.SpawnNode(SpawnSystem.Instance.Enemy);
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
