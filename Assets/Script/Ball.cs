using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log(rb);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ResetBall()
    {
        Debug.Log(rb.velocity);
        rb.velocity = Vector3.zero;
        Debug.Log(rb.velocity);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
