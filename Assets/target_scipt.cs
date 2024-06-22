using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
public class target_script : MonoBehaviour
{
    public event Action OnTargetDestroyed;
    private Animator anim;
    private bool is_up=false;
    public int alive_state=0;
    public LayerMask targetlayer;
    private void Awake(){
        anim=GetComponent<Animator>();
    }
    private void Start(){

    }
    private void Update(){
        if(is_up==false){
            is_up=true;
            anim.SetTrigger("Rise");
        }
        if(alive_state==1){
            alive_state=2;
            Debug.Log("seems ok");
            start_destroy();
        }
    }
    private void start_destroy(){
        anim.SetTrigger("Down");
    }
    private void destroy_target(){
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        OnTargetDestroyed?.Invoke();
    }
}