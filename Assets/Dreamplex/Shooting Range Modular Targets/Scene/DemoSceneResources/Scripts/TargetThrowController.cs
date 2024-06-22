using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetThrowController : MonoBehaviour
{
    public GameObject axePrefab;
    public float throwForce;
    public Transform throwPoint;


    public void Throw()
    {
        float randomMultip = Random.Range(0.9f, 1.2f);
        GameObject axe = Instantiate(axePrefab, throwPoint.position, throwPoint.transform.rotation);
        Rigidbody myRigidBody = axe.GetComponent<Rigidbody>();
        myRigidBody.AddForce((axe.transform.up + axe.transform.forward) * throwForce * randomMultip, ForceMode.Impulse);
        myRigidBody.AddTorque(axe.transform.right * throwForce*2, ForceMode.Impulse);
    }


}
