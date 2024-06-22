using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class door_controller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public UIToggleController ui;
    [SerializeField] public GameObject XR_origin;
    [SerializeField] public Camera camera;
    [SerializeField] public target_controller tc;
    Myinputactions actions;
    private Animator anim;
    private float distance = 3f;
    private bool is_open=false;
    private Collider doorCollider;
    [SerializeField] public GameObject key;
    private bool is_freeopen=false;
    void Awake(){
        GameObject currentGameObject = gameObject;
        actions = new Myinputactions();
        anim=GetComponent<Animator>();
        doorCollider = GetComponent<Collider>();
        actions.basic.interact_door.performed+=Ondoor;
        if(key==null && tc==null){
            is_freeopen=true;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!is_freeopen && key!=null){
            RaycastHit hit;
            Physics.Raycast(key.transform.position, camera.transform.forward, out hit,distance);
            if(hit.collider!=null){
                Debug.Log(hit.collider.gameObject.name);
                if(hit.collider.gameObject==gameObject || hit.collider.transform.IsChildOf(gameObject.transform)){
                    float distance = Vector3.Distance(key.transform.position,hit.point);
                    if(distance<=3f){
                        is_freeopen=true;
                        Destroy(key);
                        Debug.Log("Interact door!");
                        is_open=!is_open;
                        if(is_open==true){
                            anim.SetTrigger("Open");
                        }else{
                            anim.SetTrigger("Closed");
                        }
                    }
                }
            }
        }else if(!is_freeopen && tc!=null){
            if(tc.kill_count>=10){
                is_freeopen=true;
                is_open=!is_open;
                if(is_open==true){
                    anim.SetTrigger("Open");
                }else{
                    anim.SetTrigger("Closed");
                }
            }
        }
    }
    private void Ondoor(InputAction.CallbackContext context)
    {
        if(!ui._isUIActive()){
            Debug.Log("interact door?");
            door();
        }
    }
    private void door(){
        if(is_freeopen){
            RaycastHit hit;
            Physics.Raycast(XR_origin.transform.position, camera.transform.forward, out hit,distance);
            if(hit.collider!=null){
                Debug.Log(hit.collider.gameObject.name);
                if(hit.collider.gameObject==gameObject || hit.collider.transform.IsChildOf(gameObject.transform)){
                    Debug.Log("Interact door!");
                    is_open=!is_open;
                    if(is_open==true){
                        anim.SetTrigger("Open");
                    }else{
                        anim.SetTrigger("Closed");
                    }
                }
            }
        }
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
