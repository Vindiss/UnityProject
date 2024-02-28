using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Transform Player1PositionReset;
    [SerializeField] Transform Player2PositionReset;
    [SerializeField] Transform BallPositionReset;
    [SerializeField] Player player1;
    [SerializeField] Player player2;
    [SerializeField] Ball ball;

    public static GameManager instance;

    private void Awake()
    {
        Reset();
    }
    public static GameManager Instance()
    {
        if (instance == null)
        {
            instance = FindAnyObjectByType<GameManager>();
        }
        return instance;
    }

    public void Reset()
    {
        player1.transform.position = Player1PositionReset.position;
        player1.transform.rotation = Player1PositionReset.rotation;
        player2.transform.rotation = Player2PositionReset.rotation;
        player2.transform.position = Player2PositionReset.position;
        ball.transform.position = BallPositionReset.position;
        ball.ResetBall();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
