using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private string BallAxisPlayer1;
    [SerializeField] private string BallAxisPlayer2;


    private Rigidbody rb;
    private float lauchBallPlayer1;
    private float lauchBallPlayer2;

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
        GetComponent<ParticleSystem>().Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<ParticleSystem>().Play();
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 playervel = collision.gameObject.GetComponent<Rigidbody>().velocity;
            float playerEner = collision.gameObject.GetComponent<Player>().GetEnergy();
            if (lauchBallPlayer1 > 0 || lauchBallPlayer2 > 0)
            {
                rb.AddForce(playervel * playerEner / 10);
                collision.gameObject.GetComponent<Player>().SetEnergy(0);
            }
            else
            {
                if(playervel.x > 12 || playervel.y > 12 || playervel.z > 12)
                {
                    rb.AddForce(playervel / 100);
                }
                else
                {
                    rb.AddForce(playervel);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        lauchBallPlayer1 = Input.GetAxis(BallAxisPlayer1);
        lauchBallPlayer2 = Input.GetAxis(BallAxisPlayer2);
    }
}
