using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ResetBall()
    {
        rb.velocity  = Vector3.zero;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
