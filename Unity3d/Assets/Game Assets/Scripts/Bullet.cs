using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game_Assets.Scripts
{
    public class Bullet
    {
        private GameObject shooter;
        private GameObject bulletObject;

        public GameObject Shooter
        {
            get { return shooter; }
        }

        public GameObject BulletObject
        {
            get { return bulletObject; }
        }

        public Bullet(GameObject shooter, GameObject bulletObject)
        {
            this.shooter = shooter;
            this.bulletObject = bulletObject;
        }

        public void Fly()
        {
            bulletObject.transform.position = new Vector3
                (
                    bulletObject.transform.position.x,
                    bulletObject.transform.position.y,
                    bulletObject.transform.position.z + 0.2f
                );
        }
    }
}
