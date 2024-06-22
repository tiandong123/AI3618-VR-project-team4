using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
public class target_controller : MonoBehaviour
{
    // Start is called before the first frame update
    private bool Enter_shooting_room=false;
    [SerializeField] private LayerMask enter_shoot_room_layer;
    [SerializeField] private LayerMask shoot_room_layer;
    [SerializeField] private LayerMask target_layer;
    [SerializeField] private Transform origin_position;
    [SerializeField] private Vector3 offset_scale;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] public GameObject transport_target;
    [SerializeField] public UIToggleController ui;
    [SerializeField] public Transform pos;
    [SerializeField] public int MAX_target;
    [SerializeField] public float target_born_time=3f;
    public int kill_count=0;
    private float cool_down_time;
    // [SerializeField] private float targetScale;
    private GameObject targetInstance;
    [SerializeField] private TextMeshProUGUI text;
    private int cnt=0;
    void Awake(){
        // targetPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Dreamplex/Shooting Range Modular Targets/Prefabs/HoldingSomethingTarget (1) Variant.prefab");
        targetPrefab = Resources.Load<GameObject>("Assets/Dreamplex/Shooting Range Modular Targets/Prefabs/HoldingSomethingTarget (1) Variant.prefab");
        cool_down_time=target_born_time;
        text.text=kill_count.ToString()+"/10";
    }
    void Update(){
        if(!ui._isUIActive()){
            if(check_layer(transform.position,enter_shoot_room_layer)){
                Enter_shooting_room=true;
            }
            if(check_layer(transform.position,shoot_room_layer)){
                teleport(transport_target,pos.position);
            }
            if(Enter_shooting_room==true){
                if(cool_down_time>0)
                    cool_down_time-=Time.deltaTime;
                // Debug.Log(cool_down_time);
                if(cnt<=MAX_target && cool_down_time<=0){
                    Vector3 offset;
                    Vector3 born_pos=origin_position.position;
                    born_pos.y=0;
                    offset=Random.Range(-1f,1f)*offset_scale;
                    targetInstance = Instantiate(targetPrefab, born_pos+offset,Quaternion.Euler(-90f, -90f, 0f));
                    Vector3 targetScale = new Vector3(1.5f, 1.5f, 1.5f);
                    targetInstance.transform.localScale=targetScale;
                    cnt=cnt+1;
                    cool_down_time=target_born_time;
                    target_script targetScript = targetInstance.AddComponent<target_script>();
                    targetScript.OnTargetDestroyed += HandleTargetDestroyed;
                    targetScript.targetlayer=target_layer;
                }
            }
        }
    }
    private void HandleTargetDestroyed(){
        kill_count=kill_count+1;
        text.text=kill_count.ToString()+"/10";
        cnt--;
    }
    private bool check_layer(Vector3 position,LayerMask layer){
        return Physics.CheckSphere(position,2f,layer);
    }
    void teleport(GameObject transport_target,Vector3 pos){
        transport_target.transform.position=pos;
    }
}
