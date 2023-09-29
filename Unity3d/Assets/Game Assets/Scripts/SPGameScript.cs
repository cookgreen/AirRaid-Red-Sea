using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPGameScript : MonoBehaviour
{
    public static int currentEnemyNum = 0;
    private int enemyMaxNum = 10;
    private int spawnInitDelay = 100;
    private int spawnCurrentDelay = 100;

    private static bool isStartedGame;

    public GameObject gunnerSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        isStartedGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isStartedGame)
        {
            if (currentEnemyNum < enemyMaxNum)
            {
                if (spawnCurrentDelay == 0)
                {
                    GameObject enemy = Resources.Load("AircraftFighter_GerBf109") as GameObject;
                    GameObject enemyInstance = Instantiate(enemy, gunnerSpawnPoint.transform.position, enemy.transform.rotation);

                    spawnCurrentDelay = spawnInitDelay;

                    currentEnemyNum++;
                }
                else
                {
                    spawnCurrentDelay--;
                }
            }
        }
    }

    public static void StartGame()
    {
        isStartedGame = true;
    }
}
