using System.Collections;
using System.Collections.Generic;
using TabTabs.NamChanwoo;
using UnityEngine;
using UnityEngine.Animations;
using Spine;
using Spine.Unity;

public class PlayerBase : CharacterBase
{
    public Animator PlayerAnim;
    void Start()
    {
        PlayerAnim = GetComponent<Animator>();
    }

    
    void Update()
    {
        
    }

    public void PlayerAnimEvent()
    {
        Vector3 targetPosition = new Vector3(-1.2f, 1.5f, 0f);
        transform.position = targetPosition;
    }
}
