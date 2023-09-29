using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAGunFirepowerControllerScript : MonoBehaviour
{
    public static float FireDelay = 20;
    public static float CurrentFireDelay = 0;
    public static float AAAmmo = 300;

    public AudioClip fireSound1;
    public AudioClip fireSound2;
    public AudioClip reloadSound;

    private GameObject AAGunRotatableObject;
    private GameObject AAGunDizuo;

    // Start is called before the first frame update
    void Start()
    {
        AAGunDizuo = gameObject.transform.Find("naval_gun_armour").gameObject;
        AAGunRotatableObject = AAGunDizuo.transform.Find("naval_gun").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (CurrentFireDelay == 0)
            {
                CurrentFireDelay = FireDelay;

                GameObject bullet = Resources.Load("bullet") as GameObject;
                GameObject bulletInstance = Instantiate(bullet, bullet.transform.position, bullet.transform.rotation);
                bulletInstance.transform.parent = AAGunRotatableObject.transform;

                bulletInstance.transform.rotation = AAGunRotatableObject.transform.rotation;
                bulletInstance.transform.localPosition = new Vector3(0, -1.234f, 0);

                var audioSource = AAGunRotatableObject.GetComponent<AudioSource>();
                randomPlay(audioSource);

                AAAmmo--;

                bulletInstance.transform.parent = null;
            }
            else
            {
                CurrentFireDelay--;
            }
        }
    }

    private void randomPlay(AudioSource audioSource)
    {
        var randomNum = Random.Range(1.0f, 3.0f);
        if (randomNum >= 1.0f && randomNum < 2.0f)
        {
            audioSource.clip = fireSound1;
        }
        else
        {
            audioSource.clip = fireSound2;
        }
        audioSource.Play();
    }
}
