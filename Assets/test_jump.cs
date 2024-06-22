using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// 输入控制类的实例  

public class test_jump : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    InputActionAsset InputControls;

    private InputAction myAction1;
    void Start(){
         myAction1 = InputControls.FindActionMap("basic").FindAction("jump");
         myAction1.performed += onjump;
    }
    void Update(){
        
    }
    void OnEnable()
    {
        InputControls.FindActionMap("basic").Enable();
    }
    void OnDisable()
    {
        InputControls.FindActionMap("basic").Disable();
    }
    private void onjump(InputAction.CallbackContext context)
    {
        // Handle the performed action here
        Debug.Log("jump performed");
    }
}
