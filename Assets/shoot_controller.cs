using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

public class shoot_controller : MonoBehaviour
{
    // Start is called before the first frame update
    Myinputactions actions;
    private GameObject sphere;
    private bool ball=false;
    [SerializeField] public UIToggleController ui;
    [SerializeField] private Transform shoot_point;
    [SerializeField] public Camera camera;
    [SerializeField] private Material material;
    [SerializeField] private float shootdistance=10f;
    [SerializeField] private LayerMask shootable;
    [SerializeField] private LayerMask self_layer;
    [SerializeField] private LayerMask target_layer;
    [SerializeField] private float shootCd=1f;
    [SerializeField] private float bulletspeed=10f;
    [SerializeField] public AudioClip audioClip;
    private float shootCdTime;
    private GameObject bulletPrefab;
    private GameObject bulletInstance;
    private AudioSource audioSource;
    void Awake(){
        actions = new Myinputactions();
        shootCdTime=shootCd;
        audioSource=GetComponent<AudioSource>();
        // bulletPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Bullet Casing/Prefabs/Bullets/762x39.prefab");
        bulletPrefab = Resources.Load<GameObject>("Assets/Bullet Casing/Prefabs/Bullets/762x39.prefab");
        actions.hook.shoot.performed += m_shoot;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!ui._isUIActive()){
            if(shootCdTime>0)
                shootCdTime-=Time.deltaTime;
            // if(ball==true){
            //     Destroy(sphere);
            //     ball=false;
            // }
            // RaycastHit hit;
            // Physics.Raycast(shoot_point.position, camera.transform.forward, out hit,shootdistance);
            // // if(Is_shootable(hit.point)){
            // // Debug.Log(hit.transform.name);
            // sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            // sphere.transform.position = hit.point;
            // sphere.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            // Renderer sphereRenderer = sphere.GetComponent<Renderer>();
            // SphereCollider sc=sphere.GetComponent<SphereCollider>();
            // Destroy(sc);
            // Material redMaterial = material;
            // sphereRenderer.material = redMaterial;
            // ball=true;
            // // }
        }
    }
    private bool Is_shootable(Vector3 pos){
        return Physics.CheckSphere(pos,0.02f,shootable);
    }
    private void m_shoot(InputAction.CallbackContext context){
        if(!ui._isUIActive()){
            Debug.Log("shoot!");
            _shoot();
        }
    }
    private void _shoot(){
        if(shootCdTime>0)return ;
        RaycastHit hit;
        Physics.Raycast(shoot_point.position, camera.transform.forward, out hit,shootdistance);
        // if(Is_shootable(hit.point)){
        Debug.Log("Shoot!");
        audioSource.clip = audioClip;
        audioSource.Play();
        // Vector3 direction=(hit.point-shoot_point.position).normalized;
        Vector3 direction=camera.transform.forward.normalized;
        Vector3 up = Vector3.Cross(direction, Vector3.down);
        Vector3 bulletScale = new Vector3(0.5f, 0.5f, 0.5f); // 将子弹缩小到原来的一半
        bulletInstance = Instantiate(bulletPrefab, shoot_point.position,Quaternion.LookRotation(-Vector3.Cross(up, direction),up));
        Quaternion desiredRotation = Quaternion.LookRotation(bulletInstance.transform.forward, direction);
        bulletInstance.transform.rotation=desiredRotation;
        bulletInstance.transform.localScale = bulletScale;
        // 3. 设置子弹的移动方向和速度
        bullet_controller bulletScript = bulletInstance.AddComponent<bullet_controller>();
        bulletScript.speed = bulletspeed;
        bulletScript.direction = direction;
        bulletScript.shootable=shootable;
        bulletScript.live_time=5f;
        bulletScript.self_layer=self_layer;
        bulletScript.target_layer=target_layer;
        
        // }
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