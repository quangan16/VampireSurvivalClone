using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    public PlayerController player;
    public float levelTimeElapsed;
    public float enemyKilled;

    public void Start()
    {
        Init();
    }
    public void Init()
    {
        enemyKilled = 0;
        levelTimeElapsed = 0f;
    }
    public void Update()
    {
        if (player.IsAlive)
        {
            levelTimeElapsed += Time.deltaTime;
        }
      
    }

    public void UpdateTimeRecord()
    {
        
        if (levelTimeElapsed > DataManager.LoadBestTime())
        {
            DataManager.SaveBestTime(levelTimeElapsed);
        }
        
       
    }

    public static class EnemyLevelManager
    {
        public static int currentEnemyLevel;
        
        public delegate void LevelUpgrade();
        public static event LevelUpgrade OnLevelUpgrade;

        public static void UpgradeLevel()
        {
            
        }
            
    }

}
