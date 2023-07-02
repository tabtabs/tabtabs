using System;
using Spine;
using Spine.Unity;
using TabTabs.NamChanwoo;
using UnityEngine;
using Animation = Spine.Animation;

public class SpineAnimController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterBase m_model;
    [SerializeField] private SkeletonAnimation m_skeletonAnimation;
    
    ECharacterState m_currentState = ECharacterState.Idle;
    ECharacterState m_previousState= ECharacterState.Idle;
    
    [SerializeField] private AnimationReferenceAsset m_runAnim, m_idleAnim, m_hitAnim, m_attackAnim;
    
    private void Awake()
    {
        if (m_skeletonAnimation == null)
        {
            m_skeletonAnimation = GetComponent<SkeletonAnimation>();
        }
    }

    private void Start()
    {
        if (m_model == null)
        {
            Debug.LogError("CharacterBase 참조가 SpineAnimController에 설정되지 않았습니다!");
            return;
        }
        m_model.ChangeState += PlayNewStableAnimation;

        if (m_skeletonAnimation == null)
        {
            Debug.LogError("SpineAnimController에 SkeletonAnimation 구성 요소가 없습니다!");
            return;
        }
        m_skeletonAnimation.AnimationState.Complete += HandleAnimCompleteEvent;
    }
    
    private void OnDestroy()
    {
        if (m_model != null)
        {
            m_model.ChangeState -= PlayNewStableAnimation;
        }
        m_skeletonAnimation.AnimationState.Complete -= HandleAnimCompleteEvent;
    }

    private void HandleAnimCompleteEvent(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name == "atk")
        {
            Debug.Log("어택 애니메이션 끝남");
            if (m_model.CurrentState != ECharacterState.Die)
            {
                m_model.SetState(ECharacterState.Idle);
            }
            return;
        }
        if (trackEntry.Animation.Name == "Damages")
        {
            Destroy(m_model.gameObject);
            return;
        }
    }
    
    
    void PlayNewStableAnimation (ECharacterState newState) 
    {
        if (m_currentState == newState) return;

        m_previousState = m_currentState;
        m_currentState = newState;
        
        Animation nextAnimation; // 다음에 재생할 애니메이션을 저장할 변수를 선언합니다.
        
        switch (m_currentState)
        {
            case ECharacterState.Running:
                nextAnimation = m_runAnim;
                break;
            case ECharacterState.Attacking:
                nextAnimation = m_attackAnim;
                break;
            case ECharacterState.Hit:
                nextAnimation = m_hitAnim;
                break;
            case ECharacterState.Die:
                nextAnimation = m_hitAnim;
                break;
            default:
                nextAnimation = m_idleAnim;
                break;
        }
        
        m_skeletonAnimation.AnimationState.SetAnimation(0, nextAnimation, true);
    }
}

/*void Update()
{
    if (m_skeletonAnimation == null) 
        return; 
    
    if (m_model == null) 
        return; 
    
    if (m_previousViewState != m_currentState)
    {
        PlayNewStableAnimation(); // 업데이트된 상태를 기반으로 적절한 새 안정적인 애니메이션을 재생합니다.
    }

    // 다음 프레임 비교를 위해 이전 상태를 현재 상태로 업데이트
    m_previousViewState = m_currentState;
}*/