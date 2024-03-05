using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] private float WalkMoveForce;
    [SerializeField] private float SprintMoveForce;
    [SerializeField] private float AngularSpeed;

    [SerializeField] private string VerticalAxis;
    [SerializeField] private string HorizontalAxis;
    [SerializeField] private string SprintAxis;
    [SerializeField] private string JumpAxis;
    [SerializeField, Range(0, 100)] private float Energy = 33;
    [SerializeField] private Slider EnergySlider;
    [SerializeField] private AnimationCurve JumpConsumption;

    private Rigidbody rb;
    private float ActualMoveForce;
    private float vert;
    private float hor;
    private float sprint;
    private float jump;

    public float GetEnergy()
    {
        return Energy;
    }
    public void SetEnergy(float new_Energy)
    {
        Energy = new_Energy;
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ActualMoveForce = WalkMoveForce;
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
        sprint = Input.GetAxis(SprintAxis);
        jump = Input.GetAxis(JumpAxis);

        if (Energy + Time.deltaTime < 100 )
        {
            Energy += Time.deltaTime * 5;
        }
        else
        {
            Energy = 100;
        }
        EnergySlider.value = Energy;

        if(Energy > 0 && sprint > 0)
        {
            ActualMoveForce = SprintMoveForce;
            Energy -=  Time.deltaTime * 20;
        }
        else
        {
            ActualMoveForce = WalkMoveForce;
        }

        transform.Rotate(transform.up, AngularSpeed * 10 * hor * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(rb.velocity * 2);
        }
    }


    private void FixedUpdate()
    {
        rb.AddForce(ActualMoveForce * vert * transform.forward);

        if(jump > 0)
        {
            
        }

       /* Physics.Raycast()*/
    }
}