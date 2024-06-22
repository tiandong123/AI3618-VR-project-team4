using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class jump_controller : MonoBehaviour
{
	//定义我们的ActionsAsset导出包装类ExampleActions 
    Myinputactions actions;
    [SerializeField] private float jumpHight=2f;
    [SerializeField] private CharacterController cc;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private LayerMask wallLayers;
    [SerializeField] public Hook hook;
    [SerializeField] public UIToggleController ui;
    private float gravity=Physics.gravity.y;
    private Vector3 movement;
    void Awake()
    {
        // 实例化Action包装类
        actions = new Myinputactions();
        // 直接通过api使用jump动作，绑定回调
        actions.basic.jump.performed += OnJump;
    }
    //update=gravity 
    void Update()
    {
        if(!ui._isUIActive()){
            if(!hook.is_hook()){
                Debug.Log("jumpupdate");
                // 直接通过api访问move的值
                if(!IsGrounded(transform.position)){
                    movement.y+=gravity*Time.deltaTime;
                }
                cc.Move(movement*Time.deltaTime);
            }else{
                movement.y=0;
            }
        }
    }
    private Vector3 verticl_movement(Vector3 rope_direction_unit,Vector3 movement){
        rope_direction_unit.Normalize();
        Vector3 projection=Vector3.Dot(rope_direction_unit,movement)*rope_direction_unit;
        movement=movement-projection;
        return movement;
    }
    private void OnJump(InputAction.CallbackContext context)
    {
        if(!ui._isUIActive()){
            Debug.Log("jump?");
            if (IsGrounded(transform.position))
            {
                Jump();
            }
        }
    }
    private void Jump(){
        Debug.Log("Jump!!");
        movement.y=Mathf.Sqrt(jumpHight*-3.0f*gravity);
    }
    private bool IsGrounded(Vector3 position){
        // Debug.Log(transform.position);
        return Physics.CheckSphere(position,0.2f,groundLayers);
    }
    private bool Reach_wall(Vector3 position){
        return Physics.CheckSphere(position,0.5f,wallLayers);
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
