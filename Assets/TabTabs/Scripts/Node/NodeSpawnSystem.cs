using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TabTabs.NamChanwoo
{
    
    public enum EAlignment
    {
        Left,
        Center,
        Right
    }

    public class NodeSpawnSystem : MonoBehaviour
    {
        
        // 인스턴스에 대한 정적 참조
        public static NodeSpawnSystem Instance { get; private set; }

        [Header("SpawnSetting")]
        [SerializeField] bool IsSpawnAlignmentRandom= false;
    
        [SerializeField] List<GameObject> m_NodeList = new List<GameObject>();
        
        [SerializeField] private GameObject Enemy;
        
        private void Awake()
        {
            // NodeSpawnManager 인스턴스가 하나만 있는지 확인하십시오.
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // 선택 사항: 이 관리자가 장면 로드 간에 지속되도록 하려면
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            SpawnNode(Enemy);
        }


        public void SpawnNode(GameObject enemy)
        {
            EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();
            
            // enemyBase가 발견되었는지 확인하십시오.
            if (enemyBase == null)
            {
                Debug.LogError("적 또는 적의 자식에서 enemyBase를 찾을 수 없습니다.");
                return;
            }
            
            //노드가 0개가 아니라면 생성 된 노드를 삭제하고 Queue를 비웁니다.
            if (enemyBase.GetOwnNodes().Count !=0)
            {
                foreach (Node ownNode in enemyBase.GetOwnNodes())
                {
                    Destroy(ownNode.gameObject);
                }

                enemyBase.GetOwnNodes().Clear();
            } 


            NodeArea nodeArea = enemyBase.nodeArea;

            // NodeArea가 발견되었는지 확인하십시오.
            if (nodeArea == null)
            {
                Debug.LogError("적 또는 적의 자식에서 NodeArea를 찾을 수 없습니다.");
                return;
            }

            // 스폰할 노드 수는 행 수와 같습니다.
            int spawnNodeNum = nodeArea.Rows;

            for (int row = 0; row < spawnNodeNum; row++)
            {
                // 이 행에서 임의로 열을 선택합니다.
                int randColumn = UnityEngine.Random.Range(0, nodeArea.Columns);
                
                // 목록에서 열에 해당하는 노드 프리팹을 가져옵니다.
                GameObject nodePrefab = GetNodePrefab(randColumn);

                // NodeArea에서 이 노드의 위치를 가져옵니다.
                Vector2 spawnPosition = nodeArea.GetGridPosition(row, randColumn);

                // 계산된 위치에 노드를 인스턴스화하고 nodeArea의 자식으로 설정합니다.
                GameObject spawnedNode = Instantiate(nodePrefab, spawnPosition, Quaternion.identity, nodeArea.transform);
                
                // 노드의 이름을 설정합니다.
                spawnedNode.name = $"Node_{row}_{randColumn}";

                // NodeSheet로 노드를 초기화합니다.
                Node nodeComponent = spawnedNode.GetComponent<Node>();
                nodeComponent.Init();
                
                //에너미가 소유하는 노드에 추가합니다.
                enemyBase.AddNodes(nodeComponent);
            }
        }
        
        
        private GameObject GetNodePrefab(int Column)
        {
            //랜덤 스폰이 아니라면
            if (!IsSpawnAlignmentRandom)
            {
                if(Column == 0)
                    return m_NodeList.Find(x => x.GetComponent<Node>().nodeSheet.m_NodeType == ENodeType.Left);
                if(Column == 1)
                    return m_NodeList.Find(x => x.GetComponent<Node>().nodeSheet.m_NodeType == ENodeType.Up);
                if(Column == 2) 
                    return m_NodeList.Find(x => x.GetComponent<Node>().nodeSheet.m_NodeType == ENodeType.Right);
                
                return null;
            }
            
            //else 랜덤 스폰이라면
            return m_NodeList[UnityEngine.Random.Range(0, m_NodeList.Count)];
        }


    }
}