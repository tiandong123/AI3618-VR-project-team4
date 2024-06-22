using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_controller : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public Vector3 direction;
    public LayerMask shootable;
    public LayerMask self_layer;
    public LayerMask target_layer;
    public float live_time;
    private float shootdistance=2f;
    private Rigidbody bulletRigidbody;
    private Collider co;
    void Start()
    {
        co=gameObject.AddComponent<SphereCollider>();
        bulletRigidbody = gameObject.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(direction * speed, ForceMode.Impulse);
        bulletRigidbody.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(live_time>0)
            live_time-=Time.deltaTime;
        else{
            Destroy(gameObject);
        }
        RaycastHit hit;
        Physics.Raycast(transform.position,direction, out hit,shootdistance);
        if(Vector3.Distance(hit.point,transform.position)<=0.5f){
            if(Is_shootable(hit.point)){
                Rigidbody RB=hit.collider.gameObject.GetComponent<Rigidbody>();
                RB.useGravity=true;
                Destroy(gameObject);
            }else if(is_target(hit.point)){
                target_script ts=hit.collider.gameObject.GetComponentInParent<target_script>();
                ts.alive_state=1;
                bulletRigidbody.useGravity = true;
            }else if(!Is_self(hit.point)){
                bulletRigidbody.useGravity = true;
            }
        }
    }
    private bool Is_shootable(Vector3 pos){
        return Physics.CheckSphere(pos,0.02f,shootable);
    }
    private bool is_target(Vector3 pos){
        return Physics.CheckSphere(pos,0.02f,target_layer);
    }
    private bool Is_self(Vector3 pos){
        return Physics.CheckSphere(pos,0.02f,self_layer);
    }
}
