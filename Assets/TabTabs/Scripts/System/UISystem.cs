using TabTabs.NamChanwoo;
using UnityEngine;
using UnityEngine.UI;


public class UISystem : GameSystem
{
    public Slider AttackSliderUI = null;
    
    void Start()
    {
        GameManager.NotificationSystem.SceneMonsterSpawned.AddListener(HandleSceneMonsterSpawned);
    }

    private void HandleSceneMonsterSpawned(EnemyBase spawnedEnemy)
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}