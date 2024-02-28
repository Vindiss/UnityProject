using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float MoveForce;
    [SerializeField] private float AngularSpeed;

    [SerializeField] private string VerticalAxis;
    [SerializeField] private string HorizontalAxis;
    [SerializeField, Range(0, 100)] private float Energy = 33;

    private Rigidbody rb;
    private float vert;
    private float hor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        vert = Input.GetAxis(VerticalAxis);
        hor = Input.GetAxis(HorizontalAxis);
        Energy += Time.deltaTime;

        transform.Rotate(transform.up, AngularSpeed * 10 * hor * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        rb.AddForce(MoveForce * vert * transform.forward);
    }
}