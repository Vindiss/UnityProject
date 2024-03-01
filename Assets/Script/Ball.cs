using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private string BallAxisPlayer1;
    [SerializeField] private string BallAxisPlayer2;


    private Rigidbody rb;
    private bool lauchBallPlayer1;
    private bool lauchBallPlayer2;

    public Rigidbody GetRB() {  return rb; }
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
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 playervel = collision.gameObject.GetComponent<Rigidbody>().velocity;
            float playerEner = collision.gameObject.GetComponent<Player>().GetEnergy();
            if (lauchBallPlayer1 == true || lauchBallPlayer2  == true)
            {
                rb.AddForce(playervel * playerEner / 30);
                collision.gameObject.GetComponent<Player>().SetEnergy(0);
            }
            else
            {
                rb.AddForce(playervel * 2);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        lauchBallPlayer1 = Input.GetButton(BallAxisPlayer1);
        lauchBallPlayer2 = Input.GetButton(BallAxisPlayer2);
    }
}
