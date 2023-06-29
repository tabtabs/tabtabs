using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using TabTabs.NamChanwoo;
using UnityEngine.Serialization;




public class EnemyBase : CharacterBase
{
    private NodeArea m_nodeArea;
    
    public NodeArea nodeArea => m_nodeArea;
    
    //에너미가 소유하고 있는 노드들 입니다.
    Queue<Node> m_nodeQueue = new Queue<Node>();

    private void Awake()
    {
        m_nodeArea = GetComponentInChildren<NodeArea>();
    }

    private void Start()
    {
        
    }
    
    public void AddNodes(Node spawnedNode)
    {
        m_nodeQueue.Enqueue(spawnedNode);
    }
    

    public Queue<Node> GetOwnNodes()
    {
        return m_nodeQueue;
    }
}