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
        gameObject.transform.position = new Vector3(-4.19f, 0);
    }
}
