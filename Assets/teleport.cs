using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    [SerializeField] public Vector3 teleportation_position;
    [SerializeField] public GameObject teleportation_target;
    // Start is called before the first frame update
    public void _teleport(Vector3 teleportation_position){
        teleportation_target.transform.position=teleportation_position;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
