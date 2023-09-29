using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftControllerScript : MonoBehaviour
{
    public float Hitpoint = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Hitpoint <= 0)
        {
            AAGunPlayerControllerScript.Score += 10;

            var randomAmmoReward = Random.Range(100, 200);
            AAGunFirepowerControllerScript.AAAmmo += randomAmmoReward;

            SPGameScript.currentEnemyNum--;

            Destroy(gameObject);
        }
    }
}
