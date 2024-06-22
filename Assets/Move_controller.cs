using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_controller : MonoBehaviour
{
    // Start is called before the first frame update
    Myinputactions actions;
    [SerializeField] private CharacterController cc;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float movespeed=1f;
    [SerializeField] public Camera camera;
    [SerializeField] public Hook hook;
    [SerializeField] public Transform left;
    [SerializeField] public Transform right;
    [SerializeField] public UIToggleController ui;

    // private Vector3[] collision_array;
    // private int collision_array_len;
    void Awake(){
        actions = new Myinputactions();
    }
    // void OnCollisionEnter(Collision collisionInfo)
    // void OnCollisionStay(Collision collisionInfo){
    //     collision_array=collisionInfo.contacts[i].normal;
    //     collision_array_len=collisionInfo.contactCount;
    // }
    void Update(){
        if(!ui._isUIActive()){
            if(!hook.is_hook()){
                float moveVectorX=actions.basic.MoveX.ReadValue<float>();
                float moveVectorZ=actions.basic.MoveZ.ReadValue<float>();
                float y=camera.transform.rotation.eulerAngles.y;
                
                Vector3 movement=new Vector3(moveVectorX,0,moveVectorZ);
                movement = Quaternion.Euler(0, y, 0) * movement;
                movement.Normalize();
                RaycastHit hit;
                Physics.Raycast(left.position, movement, out hit,10f);
                float distance=Vector3.Distance(hit.point,left.position);
                Physics.Raycast(right.position, movement, out hit,10f);
                distance=Mathf.Min(Vector3.Distance(hit.point,right.position),distance);
                if(distance>0.5){
                    camera.transform.Translate(movement * Time.deltaTime * movespeed, Space.World);
                    transform.Translate(movement * Time.deltaTime * movespeed, Space.World);
                }
                // Debug.Log("transform");
                // Debug.Log("end");
                // cc.Move(movement*Time.deltaTime*movespeed);
            }
        }
    }
    private bool IsGrounded(Vector3 pos){
        return Physics.CheckSphere(pos,0.02f,groundLayers);
    }
    void OnEnable()
    {
        actions.basic.Enable();
    }
    void OnDisable()
    {
        actions.basic.Disable();
    }
}
