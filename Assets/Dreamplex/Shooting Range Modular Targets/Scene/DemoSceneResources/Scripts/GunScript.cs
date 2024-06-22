using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject bullet;
    public Transform nozzle;

    public float bulletForce = 20f;

    public float fireRate = 0.5F;
    private float myTime = 0.0F;

    private Vector3 rot;
    // Update is called once per frame
    void Update()
    {
        myTime = myTime + Time.deltaTime;

        if (Input.GetButton("Fire1") && myTime>=fireRate)
        {
            GameObject newBullet =  Instantiate(bullet, nozzle.position, nozzle.rotation) as GameObject;
            newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * bulletForce, ForceMode.Impulse);
            myTime = 0.0F;
        }
    }
}
