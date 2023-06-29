using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace TabTabs.NamChanwoo
{
    public class AIController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CharacterBase m_character = null; // AI 캐릭터 참조
        
        [Header("Target Following Settings")]
        [SerializeField] private Vector2 m_detectionArea = new Vector2(5f, 5f); // 플레이어 감지 영역
        [SerializeField] float m_targetDistance = 1.0f; // 타겟 추적 거리
        [SerializeField] float m_soughtDistanceFromTarget = 1.0f; // 타겟 추적 거리
        
        
        
        [Header("Attack Settings")]
        public Slider attackGaugeSlider; // Reference to the Slider UI
        [SerializeField] public float m_initialattackGauge = 10.0f; // 초기 공격 쿨다운
        
        private float m_attackGauge = 10.0f; // 공격 쿨다운
        
        private bool isCoroutineRunning = false;
        
        private CharacterBase m_target = null; // AI 캐릭터의 목표
        
        public float AttackGauge => m_attackGauge; 
     
        private void Awake() 
        {
        }

        private void Start()
        {
            m_attackGauge = m_initialattackGauge;
            attackGaugeSlider.maxValue = m_initialattackGauge; // 슬라이더의 최대값 설정
            attackGaugeSlider.value = m_attackGauge; //선택 사항: 시작 시 전체 게이지를 시각적으로 표시하기 위해
        }
        
        private void FixedUpdate() // 고정 업데이트 함수 (물리 계산용)
        {
            // 목표가 없다면
            if (!m_target)
            {
                m_target = FindTarget(); // 목표를 찾다
                return;
            }
            
            // 대상이 존재하는 경우
            
            Vector2 direction = GetTargetMovementDirection();
            
            if (m_targetDistance <= m_soughtDistanceFromTarget)
            {
                m_character.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                m_character.SetMovementDirection(Vector2.zero);
                if (!isCoroutineRunning)
                {
                    StartCoroutine(CountdownAttackGauge());
                }
            }
            else
            {
                m_character.SetMovementDirection(direction);    
            }
        }

        /// <summary>
        /// 타겟을 찾습니다.
        /// </summary>
        private CharacterBase FindTarget()
        {
            // 겹침 영역 확인을 위한 영역 정의
            Vector2 pointA = (Vector2)transform.position - m_detectionArea / 2;
            Vector2 pointB = (Vector2)transform.position + m_detectionArea / 2;

            // 플레이어 레이어에서 감지 영역 내의 첫 번째 개체 가져오기
            Collider2D hit = Physics2D.OverlapArea(pointA, pointB, LayerManager.PlayerLayerMask);

            // 히트 콜라이더가 null이 아닌 경우 
            if (hit != null)
            {
                CharacterBase character = hit.transform.GetComponent<CharacterBase>();
                if(character != null)
                {
                    return character;
                }
            }
            return null; // 감지 영역 내에 물체가 없으면 null을 반환합니다.
        }
        
        /// <summary>
        /// 타겟의 위치를 가져옴
        /// </summary>
        private Vector2 GetTargetPosition() 
        {
            return m_target ? (Vector2)m_target.gameObject.transform.position 
                : gameObject.transform.position; // 타겟이 있으면 타겟의 위치, 자신의 위치를 반환 
        }

        /// <summary>
        /// 타겟의 이동 방향을 가져옵니다
        /// </summary>
        private Vector2 GetTargetMovementDirection()
        {
            Vector2 targetPosition = GetTargetPosition(); // 목표물의 위치 파악
            Vector2 currentPosition = transform. position; // 현재 위치 얻기
            Vector2 targetMovementDirection = targetPosition - currentPosition; // 대상의 이동 방향 계산
            
            m_targetDistance = targetMovementDirection.magnitude;
            
            targetMovementDirection.Normalize();
            targetMovementDirection.y = 0; // 왼쪽 또는 오른쪽으로만 이동하려면 y 값을 0으로 설정합니다.
    
            return targetMovementDirection;
        }
        
        // 타겟 공격 시도
        private void AttackProcess(float distanceToTarget)
        {
            if(m_character.currentState != ECharacterState.Attacking)
            {
                StartCoroutine(CountdownAttackGauge());
            }
        }
        
        private void UpdateAttackGaugeSlider()
        {
            attackGaugeSlider.value = m_attackGauge;
        }
        
        private IEnumerator CountdownAttackGauge()
        {
            isCoroutineRunning = true;
            
            while(m_attackGauge > 0)
            {
                yield return new WaitForSeconds(Time.fixedDeltaTime);
                m_attackGauge-= Time.fixedDeltaTime;
                UpdateAttackGaugeSlider();
            }
            m_character.Attack();
            m_attackGauge = m_initialattackGauge;
            UpdateAttackGaugeSlider();
            isCoroutineRunning = false;
        }
        
        
        private void OnDrawGizmosSelected()
        {
            // 감지 영역을 나타내는 사각형을 그립니다.
            Gizmos.color = Color.red;
            Vector2 pointA = (Vector2)transform.position - m_detectionArea / 2;
            Vector2 pointB = (Vector2)transform.position + m_detectionArea / 2;

            Gizmos.DrawLine(new Vector2(pointA.x, pointA.y), new Vector2(pointB.x, pointA.y));
            Gizmos.DrawLine(new Vector2(pointA.x, pointA.y), new Vector2(pointA.x, pointB.y));
            Gizmos.DrawLine(new Vector2(pointB.x, pointB.y), new Vector2(pointB.x, pointA.y));
            Gizmos.DrawLine(new Vector2(pointB.x, pointB.y), new Vector2(pointA.x, pointB.y));
        }
    }
}