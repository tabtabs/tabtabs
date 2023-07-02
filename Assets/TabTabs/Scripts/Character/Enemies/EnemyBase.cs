using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace TabTabs.NamChanwoo
{
    public class EnemyBase : CharacterBase
    {
        private NodeArea m_nodeArea;
        
        public NodeArea nodeArea => m_nodeArea;

        private Queue<Node> m_nodeQueue = new Queue<Node>();
       
        [Header("Attack Properties")]
        public Slider m_attackGaugeSlider;
        [SerializeField] private float m_chargAttackGauge = 10.0f; 
        private float m_attackGauge = 10.0f; // 공격 쿨다운
        
        public float AttackGauge
        {
            get => CurrentState == ECharacterState.Attacking ? 0.0f : m_attackGauge;
            set
            {
                m_attackGauge = Mathf.Clamp(value, 0.0f, m_chargAttackGauge);
                UpdateSliderAttackUI();
            }
        }

        private void Awake()
        {
            m_nodeArea = GetComponentInChildren<NodeArea>();
        }
        
        private void Start()
        {
            if (m_rigidbody ==null)
            {
                m_rigidbody = GetComponent<Rigidbody2D>();
            }
        }

        public void SetupAttackSliderUI(Slider attackSliderUI)
        {
            if (m_attackGaugeSlider ==null)
            {
                m_attackGaugeSlider = attackSliderUI;
                m_attackGauge = m_chargAttackGauge;
                m_attackGaugeSlider.maxValue = m_chargAttackGauge;
                m_attackGaugeSlider.value = m_attackGauge;
            }
        }

        private void UpdateSliderAttackUI()
        {
            if (m_attackGaugeSlider != null)
            {
                m_attackGaugeSlider.maxValue = m_chargAttackGauge;
                m_attackGaugeSlider.value = m_attackGauge;
            }
        }
        
        virtual public void Attack()
        {
            CurrentState = ECharacterState.Attacking;
            AttackGauge = m_chargAttackGauge;
        }
        
        
        public void AddNodes(Node spawnedNode)
        {
            m_nodeQueue.Enqueue(spawnedNode);
        }

        public Queue<Node> GetOwnNodes()
        {
            return m_nodeQueue;
        }
        
        public void IncreaseAttackGauge(float amount)
        {
            AttackGauge += amount;
        }
        
        public bool IsAttackGaugeEmpty()
        {
            return !(AttackGauge > 0.0f);
        }
    }
}