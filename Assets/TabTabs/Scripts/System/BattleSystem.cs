using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TabTabs.NamChanwoo
{

    public class BattleSystem : MonoBehaviour
    {
        // BattleSystem에서 구현될 것들
        // 1. 전투로직
        // 2. Timebar와 에너미의 체력 --
        // NodeSheet클래스의 m_NodeType;(방향키) => 변수로 가져다 씀
        // SpawnSystem클래스의 SpawnNode(GameObject enemy)함수 => 싱클톤
        // EnemyBase클래스의 m_nodeQueue변수(Queue 블록) => GetownNodes함수 가져다 씀
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
            {// ClickNode가 중립이 아니라면(버튼을 클릭했다면)
                if (ClickNode==EnemyBaseInstance.GetOwnNodes().Peek().nodeSheet.m_NodeType)
                {//다음에 나갈 노드타입과 비교(같은 NodeType을 클릭했다면)

                    // 1. 해당하는 enemy의 블럭 destroy
                    // 2. 캐릭터가 해당하는 enemy의 블럭위치로 이동 후 공격 애니메이션 재생 후 원래위치로 이동
                    // 3. 시간변수 +
                    Destroy(EnemyBaseInstance.GetOwnNodes().Peek().gameObject);
                    EnemyBaseInstance.GetOwnNodes().Dequeue();
                    // m_AttackGauge+

                    if (EnemyBaseInstance.GetOwnNodes().Count<=0)
                    {// 에너미의 노드가 0보다 작거나 같다면
                        // 몬스터 제거 후 다시생성
                        Destroy(SpawnSystem.Instance.Enemy);
                    }
                }
                else
                {// 아니라면
                    // 1. 캐릭터 Hp --
                    // 2. 캐릭터 원래위치로 이동
                    // 3. Enemy의 캐릭터 공격 애니메이션 재생
                    // 4. 노드 다시 생성하는 함수 호출
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
