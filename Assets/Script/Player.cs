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
    [SerializeField] private float JumpForce;

    [SerializeField] private string VerticalAxis;
    [SerializeField] private string HorizontalAxis;
    [SerializeField] private string SprintAxis;
    [SerializeField] private string JumpAxis;
    [SerializeField, Range(0, 100)] private float Energy = 33;
    [SerializeField] private Slider EnergySlider;
    [SerializeField] private AnimationCurve JumpConsumption;

    private Rigidbody rb;
    private Camera cam;
    private GameManager gameManager;
    private float ActualMoveForce;
    private float vert;
    private float hor;
    private float sprint;
    private float jump;
    private float jumpTime;

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
        cam = GetComponentInChildren<Camera>();
        gameManager = GameManager.Instance();
        ActualMoveForce = WalkMoveForce;
        jumpTime = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GameEnd || gameManager.KickOff || !gameManager.GameStart)
        {
            vert = 0; 
            hor = 0;
            sprint = 0;
            jump = 0;
        }
        else
        {
            vert = Input.GetAxis(VerticalAxis);
            hor = Input.GetAxis(HorizontalAxis);
            sprint = Input.GetAxis(SprintAxis);
            jump = Input.GetAxis(JumpAxis);
        }
        if (gameManager.GameStart)
        {
            if (Energy + Time.deltaTime < 100)
            {
                Energy += Time.deltaTime * 9;
            }
            else
            {
                Energy = 100;
            }
            EnergySlider.value = Energy;
        }
        

        if(Energy > 0 && sprint > 0 && jump == 0)
        {
            ActualMoveForce = SprintMoveForce;
            Energy -=  Time.deltaTime * 30;
            if(cam.fieldOfView < 100)
            {
                cam.fieldOfView += 0.25f;
            }
        }
        else
        {
            ActualMoveForce = WalkMoveForce;
            if (cam.fieldOfView > 80)
            {
                cam.fieldOfView -= 0.25f;
            }
        }
        transform.Rotate(transform.up, AngularSpeed * 10 * hor * Time.deltaTime);
    }

    private void FixedUpdate()
    {

        RaycastHit hitInfo;
        if (jump > 0 && Energy > 0 && sprint == 0)
        {
            rb.drag = 0.001f;
            rb.AddForce(ActualMoveForce * vert * transform.forward / 1800);
            rb.AddForce(JumpForce * Vector3.up * jump * JumpConsumption.Evaluate(jumpTime));
            
            Energy -= Time.deltaTime * 45;
            jumpTime += Time.deltaTime;
        }
        else
        {
            rb.AddForce(ActualMoveForce * vert * transform.forward);
            if (Physics.Raycast(transform.position, -transform.up, out hitInfo))
            {
                if (transform.position.y - hitInfo.transform.position.y < 1.25f)
                {
                    rb.drag = 1.8f;
                }
            }
        }
    }
}