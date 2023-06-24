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


        private EAlignment GetSpawnAlignment(NodeSheet nodeSheet)
        {
            if (IsSpawnAlignmentRandom)
            {
                switch (nodeSheet.m_NodeType)
                {
                    case ENodeType.Up:
                        return EAlignment.Center;
                    case ENodeType.Left:
                        return EAlignment.Left;
                    case ENodeType.Right:
                        return EAlignment.Right;
                    default: return EAlignment.Center;
                }
            }
            else
            {
                return (EAlignment)Random.Range(0, 3);
            }
        }

        

    }
}