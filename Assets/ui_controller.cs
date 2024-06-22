using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ui_controller : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Button Button1, Button2, Button3,Button4,Button5;

    [SerializeField] public GameObject transport_target;
    [SerializeField] public Vector3 pos1;
    [SerializeField] public Transform pos2;
    [SerializeField] public Transform pos3;
    [SerializeField] public Transform pos4;
    [SerializeField] public Transform pos5;

    void Start()
    {

        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        Button1.onClick.AddListener(() =>Click_button1(transport_target,pos1));
        Button2.onClick.AddListener(() =>Click_button2(transport_target,pos2.position));
        Button3.onClick.AddListener(() =>Click_button3(transport_target,pos3.position));
        Button4.onClick.AddListener(() =>Click_button4(transport_target,pos4.position));
        Button5.onClick.AddListener(() =>Click_button4(transport_target,pos5.position));
    }
    private void Click_button1(GameObject transport_target,Vector3 pos){
        teleport(transport_target,pos);
    }
    private void Click_button2(GameObject transport_target,Vector3 pos){
        teleport(transport_target,pos);
    }
    private void Click_button3(GameObject transport_target,Vector3 pos){
        teleport(transport_target,pos);
    }
    private void Click_button4(GameObject transport_target,Vector3 pos){
        teleport(transport_target,pos);
    }
    private void Click_button5(GameObject transport_target,Vector3 pos){
        teleport(transport_target,pos);
    }
    void teleport(GameObject transport_target,Vector3 pos){
        transport_target.transform.position=pos;
    }
}