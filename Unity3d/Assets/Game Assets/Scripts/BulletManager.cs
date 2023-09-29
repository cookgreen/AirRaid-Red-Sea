using Assets.Game_Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private List<Bullet> bullets;

    private static BulletManager instance;
    public static BulletManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BulletManager();
            }
            return instance;
        }
    }

    public BulletManager()
    {
        bullets = new List<Bullet>();
    }

    public void ShootBullet(GameObject shooter, GameObject bulletObject)
    {
        Bullet bullet = new Bullet(shooter, bulletObject);
        bullets.Add(bullet);
    }

    // Start is called before the first frame update
    void Start()
    {
        bullets = new List<Bullet>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
