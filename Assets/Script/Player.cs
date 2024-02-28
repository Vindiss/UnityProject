using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] private float MoveForce;
    [SerializeField] private float AngularSpeed;

    [SerializeField] private string VerticalAxis;
    [SerializeField] private string HorizontalAxis;
    [SerializeField, Range(0, 100)] private float Energy = 33;
    [SerializeField] private Slider EnergySlider;

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
        if(Energy + Time.deltaTime < 100 )
        {
            Energy += Time.deltaTime * 5;
        }
        else
        {
            Energy = 100;
        }
        EnergySlider.value = Energy;
        transform.Rotate(transform.up, AngularSpeed * 10 * hor * Time.deltaTime);

    }

    private void FixedUpdate()
    {
        rb.AddForce(MoveForce * vert * transform.forward);
    }
}