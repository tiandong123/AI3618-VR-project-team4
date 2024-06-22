using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hook : MonoBehaviour
{
    // Start is called before the first frame update
    Myinputactions actions;
    [SerializeField] LineRenderer lr;
    Vector3 GrapPoint;
    private bool IsHook=false;
    private bool ball=false;
    private Vector3 HookDirection;
    private GameObject sphere;
    [SerializeField] private Transform Hookpoint;
    [SerializeField] private Material material;
    [SerializeField] private float hookdistance=10f;
    [SerializeField] private LayerMask hookable;
    [SerializeField] public Camera camera;
    [SerializeField] private CharacterController cc;
    [SerializeField] private float tolerence=0.5f;
    [SerializeField] public UIToggleController ui;
    [SerializeField] public float hook_speed;
    private SpringJoint spring;
    public float Hookspring = 5f;
    [SerializeField] private float grapplingCd=0.5f;
    private float grapplingCdTime;
    [SerializeField] private float grappleDelayTime;
    void Awake(){
        actions = new Myinputactions();
        grapplingCdTime=grapplingCd;
        actions.hook.hook.performed += m_ThrowHook;
        lr.positionCount = 0;
    }
    public bool is_hook(){
        return IsHook;
    }
    public Vector3 _HookDirection(){
        return  HookDirection;
    }

    // Update is called once per frame
    void Update()
    {
        if(!ui._isUIActive()){
            if(grapplingCdTime>0)
                grapplingCdTime-=Time.deltaTime;
            if(ball==true){
                Destroy(sphere);
                ball=false;
            }
            if(IsHook){
                float Hookdistance=Vector3.Distance(GrapPoint,Hookpoint.position);
                if(Hookdistance<=tolerence){
                    Debug.Log("delete hook!");
                    DeleteHook();
                }
            }
            if(IsHook){
                // float moveVector=actions.hook.hookmove.ReadValue<float>();
                
                HookDirection=(GrapPoint-Hookpoint.position).normalized;
                Vector3 movement=HookDirection*hook_speed;
                Debug.Log(HookDirection);
                cc.Move(movement*Time.deltaTime);
            }
            if(!IsHook){
                RaycastHit hit;
                Physics.Raycast(Hookpoint.position, camera.transform.forward, out hit,hookdistance);
                // if(Ishookable(hit.point)){
                //     Debug.Log(hit.transform.name);
                //     sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //     sphere.transform.position = hit.point;
                //     sphere.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                //     Renderer sphereRenderer = sphere.GetComponent<Renderer>();
                //     SphereCollider sc=sphere.GetComponent<SphereCollider>();
                //     Destroy(sc);
                //     Material redMaterial = material;
                //     sphereRenderer.material = redMaterial;
                //     ball=true;
                // }
            }
        }
    }
    void LateUpdate(){
        DrawRope();
    }
    public Vector3 CalculateJumpVelocity(Vector3 startpoint, Vector3 endpoint,float trajectoryHeight){
        float gravity = Physics.gravity.y;
        float displacementY = endpoint.y-startpoint.y;
        Vector3 displacementXZ = new Vector3(endpoint.x-startpoint.x,0f,endpoint.z-startpoint.z);
        Vector3 velocityY= Vector3.up * Mathf.Sqrt(-2*gravity*trajectoryHeight);
        Vector3 velocityXZ=displacementXZ/(Mathf.Sqrt(-2*trajectoryHeight/gravity)
                           +Mathf.Sqrt(2*(displacementY-trajectoryHeight)/gravity));
        return velocityXZ+velocityY;
    }
    public void jumptoposition(Vector3 endpoint,float trajectoryHeight){

    }
    private void m_ThrowHook(InputAction.CallbackContext context){
        if(!ui._isUIActive()){
            if(IsHook==false){
                Debug.Log("throw hook!");
                ThrowHook();
            }else{
                Debug.Log("delete hook!");
                DeleteHook();
            }
        }
    }
    private void ThrowHook(){
        if(grapplingCdTime>0) return ;
        RaycastHit hit;
        //发射射线
        // float y=camera.transform.rotation.eulerAngles.y;
        // Vector3 movement=new Vector3(1,0,0);
        // movement = Quaternion.Euler(0, y, 0) * movement;
        // movement.Normalize();
        
        Physics.Raycast(Hookpoint.position, camera.transform.forward, out hit,hookdistance);
        Debug.Log(hit.point);
        if(Ishookable(hit.point)){
            IsHook=true;
            GrapPoint=hit.point;
            // Invoke(nameof(ExecuteGrapple),grappleDelayTime);
            lr.positionCount = 2;
        }
    }
    private void ExecuteGrapple(){

    }
    private void DeleteHook(){
        lr.positionCount = 0;
        grapplingCdTime=grapplingCd;
        // Destroy(spring);
        IsHook = false;
    }
    void DrawRope() {
        if (!IsHook) return;
        lr.SetPosition(0, Hookpoint.position);
        lr.SetPosition(1, GrapPoint);
    }
    private bool Ishookable(Vector3 pos){
        return Physics.CheckSphere(pos,0.02f,hookable);
    }
    void OnEnable()
    {
        actions.hook.Enable();
    }
    void OnDisable()
    {
        actions.hook.Disable();
    }
}
