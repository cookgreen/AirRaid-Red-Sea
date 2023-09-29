using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = transform.up * Speed * -1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetInstanceID() == this.GetInstanceID())
            return;

        float damage = Random.Range(40, 60);

        AircraftControllerScript aircraftScript;
        if(other.TryGetComponent(out aircraftScript))
        {
            aircraftScript.Hitpoint -= damage;
        }
    }
}
