using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{

    MeshRenderer renderer;
    Rigidbody rigidbody;
    [SerializeField] float waitTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody = GetComponent<Rigidbody>();
        renderer = GetComponent<MeshRenderer>();


        if (Time.time >= waitTime) {
            rigidbody.useGravity = true;   
            renderer.enabled = true;   
        };
        
    }
}
