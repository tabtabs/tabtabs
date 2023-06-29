using System.Collections;
using System.Collections.Generic;
using TabTabs.NamChanwoo;
using UnityEngine;
using UnityEngine.Events;

public class NotificationSystem : GameSystem
{
    [Header("Gameplay Events")]
    public UnityEvent<CharacterBase> SceenMonsterSpwaned = new UnityEvent<CharacterBase>();
}
