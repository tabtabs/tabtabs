
using TabTabs.NamChanwoo;
using UnityEngine;
using UnityEngine.Events;


public class NotificationSystem : GameSystem
{
    [Header("Gameplay Events")]
    public UnityEvent<EnemyBase> SceneMonsterSpawned = new UnityEvent<EnemyBase>();
    
    [Header("Gameplay Events")]
    public UnityEvent<EnemyBase> SceneMonsterDeath = new UnityEvent<EnemyBase>();

}