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
    
    [SerializeField] private SpineAnimation m_spineAnimator;
    
    ECharacterState m_currentState = ECharacterState.Idle;
    ECharacterState m_previousViewState= ECharacterState.Idle;
    
    public AnimationReferenceAsset m_runAnim, m_idleAnim, m_hitAnim, m_attackAnim;

    private void Awake()
    {
        if (m_skeletonAnimation==null)
        {
            m_skeletonAnimation = GetComponent<SkeletonAnimation>();
        }
    }

    void Start()
    {
        m_model.ChangeState += SetState;
        m_skeletonAnimation.AnimationState.Complete += HandleAnimCompleteEvent;
    }

    private void HandleAnimCompleteEvent(TrackEntry trackentry)
    {
        if (trackentry.Animation.Name == "atk")
        {
            m_model.currentState = ECharacterState.Idle;
        }
    }

    private void OnDestroy() 
    {
        m_model.ChangeState -= SetState;
    }


    void AttackAnimEvent () 
    {
        Debug.Log("실행됨 어택이벤트가");
        m_model.currentState = ECharacterState.Idle;
    }

    private void SetState(ECharacterState NewState)
    {
        m_currentState = NewState;
    }

    void Update()
    {
        if (m_skeletonAnimation == null) return; // SkeletonAnimation 구성 요소가 할당되었는지 확인
        
        if (m_model == null) return; // SpineboyBeginnerModel 구성 요소가 할당되었는지 확인
        
        if (m_previousViewState != m_currentState)
        {
            PlayNewStableAnimation(); // 업데이트된 상태를 기반으로 적절한 새 안정적인 애니메이션을 재생합니다.
        }

        // 다음 프레임 비교를 위해 이전 상태를 현재 상태로 업데이트
        m_previousViewState = m_currentState;
    }
    
    void PlayNewStableAnimation () 
    {
        ECharacterState newModelState = m_model.currentState;
        
        Debug.Log("오크 상태 : " +newModelState);
        
        Animation nextAnimation; // 다음에 재생할 애니메이션을 저장할 변수를 선언합니다.
        
        if (newModelState == ECharacterState.Running)
        {
            nextAnimation = m_runAnim;
        }
        else if (newModelState == ECharacterState.Attacking)
        {
            nextAnimation = m_attackAnim;
        }
        else if (newModelState == ECharacterState.Hit)
        {
            nextAnimation = m_hitAnim;
        }
        else  // idle 경우
        {
            nextAnimation = m_idleAnim;
        }
        
        // SkeletonAnimation의 트랙 0에서 nextAnimation을 계속 재생하도록 설정합니다.
        m_skeletonAnimation.AnimationState.SetAnimation(0, nextAnimation, true);
    }
}