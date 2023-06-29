using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace TabTabs.NamChanwoo
{
    public enum ECharacterState
    {
        Idle,
        Running,
        Hit,
        Attacking
    }
    
    public enum EDirection
    {
        Left,
        Right,
        Default = Left
    }

    public class CharacterBase : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody2D m_rigidbody = null;
        
        [Header("Movement Settings")]
        [SerializeField] private float m_moveSpeed = 4.0f;
        private Vector2 m_movementDirection;
        
        ECharacterState m_currentState = ECharacterState.Idle;
        
        public ECharacterState currentState
        {
            get => m_currentState;
            set => m_currentState = value;
        }


        public event Action<ECharacterState> ChangeState;

        
        private void Start()
        {
            if (m_rigidbody == null)
            {
                m_rigidbody = GetComponent<Rigidbody2D>();
            }
        }

        private void FixedUpdate()
        {
            TryMove(m_movementDirection, m_moveSpeed);
        }
        
        private void TryMove(Vector2 direction, float speed)
        {
            if (direction == Vector2.zero)
            {
                m_rigidbody.velocity = Vector2.zero;
                
                if (m_currentState!= ECharacterState.Attacking)
                {
                    m_currentState = ECharacterState.Idle;
                }
            }
            else
            {
                Vector2 newPosition = m_rigidbody.position + direction * speed * Time.fixedDeltaTime;
                m_rigidbody.MovePosition(newPosition);
                m_currentState = ECharacterState.Running;
            }
            
            ChangeState?.Invoke(m_currentState);
        }
        
        
        virtual public void Attack()
        {
            m_currentState = ECharacterState.Attacking;
            ChangeState?.Invoke(m_currentState);
        }
        
        public void SetMovementDirection(Vector2 direction)
        {
            m_movementDirection = direction;
        }
        
        public bool IsMovingUp() => m_movementDirection.y > 0.0f;
        public bool IsMovingDown() => m_movementDirection.y < 0.0f;
        public bool IsMovingLeft() => m_movementDirection.x < 0.0f;
        public bool IsMovingRight() => m_movementDirection.x > 0.0f;
        public bool IsMoving() => m_movementDirection.magnitude > 0.0f;
    }

}