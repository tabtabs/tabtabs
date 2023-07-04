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
        [FormerlySerializedAs("m_chargAttackGauge")] [SerializeField] private float m_maxAttackGauge = 10.0f; 
        private float m_attackGauge = 10.0f; // 공격 쿨다운
        
        public GameObject skullPrefab;

        private int attackDamage = 3;
        
        public float AttackGauge
        {
            get => CurrentState == ECharacterState.Attacking ? 0.0f : m_attackGauge;
            set
            {
                m_attackGauge = Mathf.Clamp(value, 0.0f, m_maxAttackGauge);
                UpdateSliderAttackUI();
            }
        }

        
        [Header("Movement Settings")]
        [SerializeField] protected float m_moveSpeed = 4.0f;
        
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

        protected void Update()
        {
            /*Debug.Log("EnemyBase Update : " + CurrentState);*/
        }

        protected void FixedUpdate()
        {
            if (m_rigidbody== null)
                return;
            
            if (m_movementDirection == Vector2.zero)
            {
                m_rigidbody.velocity = Vector2.zero;
                
                if (CurrentState != ECharacterState.Die && CurrentState != ECharacterState.Attacking)
                {
                    SetState(ECharacterState.Idle);
                }
            }
            else
            {
                Vector2 newPosition = m_rigidbody.position + m_movementDirection * (m_moveSpeed * Time.fixedDeltaTime);
                m_rigidbody.MovePosition(newPosition);
                //Vector3 currentPosition = m_rigidbody.position;
                //float newY = 1.1f;
                //Vector3 newPosition = new Vector3(Vector2.zero.x, newY, currentPosition.z);
                //m_rigidbody.MovePosition(newPosition);

                //if (CurrentState == ECharacterState.Idle )
                //{
                //    SetState(ECharacterState.Running);
                //}

            }
        }
        
        public void SetupAttackSliderUI(Slider attackSliderUI)
        {
            if (m_attackGaugeSlider ==null)
            {
                m_attackGaugeSlider = attackSliderUI;
                m_attackGauge = m_maxAttackGauge;
                m_attackGaugeSlider.maxValue = m_maxAttackGauge;
                m_attackGaugeSlider.value = m_attackGauge;
            }
        }

        private void UpdateSliderAttackUI()
        {
            if (m_attackGaugeSlider != null)
            {
                m_attackGaugeSlider.maxValue = m_maxAttackGauge;
                m_attackGaugeSlider.value = m_attackGauge;
            }
        }
        
        virtual public void Attack()
        {
            if (CurrentState != ECharacterState.Die)
            {
                SetState(ECharacterState.Attacking);
                AttackGauge = m_maxAttackGauge;
                GameManager.UISystem.CalculateDamage(attackDamage);
            }
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
        
        public void HitIncreaseAttackGauge()
        {
            AttackGauge =AttackGauge + (m_maxAttackGauge * 0.1f);
        }
        
        public bool IsAttackGaugeEmpty()
        {
            return !(AttackGauge > 0.0f);
        }

        public void Hit()
        {
            SetState(ECharacterState.Hit);
            HitIncreaseAttackGauge();
        }
        
        public void Die()
        {
            SetState(ECharacterState.Die);

            BoxCollider2D orcCollider = GetComponent<BoxCollider2D>();
            Vector3 skullPosition = transform.position;

            if (orcCollider != null)
            {
                skullPosition += new Vector3(0, orcCollider.size.y / 2, 0);
            }

            GameObject skull = Instantiate(skullPrefab, skullPosition, Quaternion.identity);
            Destroy(gameObject);
            GameManager.NotificationSystem.SceneMonsterDeath?.Invoke(this);
        }

    }
}