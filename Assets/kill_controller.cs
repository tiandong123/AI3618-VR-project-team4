using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kill_controller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private LayerMask kill_layer_lv2;
    [SerializeField] private LayerMask kill_layer_lv3;
    [SerializeField] private LayerMask win_layer;
    [SerializeField] private LayerMask win_layer2;
    [SerializeField] private Transform start_lv2;
    [SerializeField] private Transform start_lv3;
    [SerializeField] public AudioClip audioClip;
    [SerializeField] public AudioClip audioClip2;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSource2;
    private bool win=false;
    private bool win2=false;
    void Update(){
        if(check_layer(transform.position,kill_layer_lv2)){
            transform.position=start_lv2.position;
        }else if(check_layer(transform.position,kill_layer_lv3)){
            transform.position=start_lv3.position;
        }else if(check_layer(transform.position,win_layer)){
            if(win==false){
                audioSource.clip = audioClip;
                audioSource.Play();
                audioSource.time = 20f;
                audioSource.volume = 0.1f;
                win=true;
            }
        }else if(check_layer(transform.position,win_layer2)){
            if(win2==false){
                audioSource2.clip = audioClip2;
                audioSource2.Play();
                audioSource2.time = 20f;
                audioSource2.volume = 0.1f;
                win2=true;
            }
        }
    }
    private bool check_layer(Vector3 position,LayerMask layer){
        return Physics.CheckSphere(position,2f,layer);
    }
}
