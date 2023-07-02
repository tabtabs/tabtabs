using System;
using UnityEngine;


namespace TabTabs.NamChanwoo
{
    public enum ECharacterState
    {
        Idle,
        Running,
        Hit,
        Attacking,
        Die
    }
    
    public enum EDirection
    {
        Left,
        Right,
    }

    public class CharacterBase : MonoBehaviour
    {
        protected Rigidbody2D m_rigidbody;
        
        [Header("Movement Settings")]
        [SerializeField] protected float m_moveSpeed = 4.0f;
        
        protected Vector2 m_movementDirection;
        
        private ECharacterState m_currentState = ECharacterState.Idle;
        
        public ECharacterState CurrentState
        {
            get => m_currentState;
            set
            {
                if (m_currentState != value)
                {
                    m_currentState = value;
                    ChangeState?.Invoke(m_currentState);
                }
            }
        }
        
        public event Action<ECharacterState> ChangeState;
        
        public bool IsMovingUp() => m_movementDirection.y > 0.0f;
        public bool IsMovingDown() => m_movementDirection.y < 0.0f;
        public bool IsMovingLeft() => m_movementDirection.x < 0.0f;
        public bool IsMovingRight() => m_movementDirection.x > 0.0f;
        public bool IsMoving() => m_movementDirection.magnitude > 0.0f;


        private void Start()
        {
            if (m_rigidbody ==null)
            {
                m_rigidbody = GetComponent<Rigidbody2D>();
            }
        }
        
        protected void FixedUpdate()
        {
            TryMove(m_movementDirection, m_moveSpeed);
        }
        
        protected void TryMove(Vector2 direction, float speed)
        {
            if (m_rigidbody== null)
                return;
            
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
    }

}