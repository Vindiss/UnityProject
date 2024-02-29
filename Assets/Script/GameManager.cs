using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Timers;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Transform Player1PositionReset;
    [SerializeField] private Transform Player2PositionReset;
    [SerializeField] private Transform BallPositionReset;
    [SerializeField] private Player player1;
    [SerializeField] private Player player2;
    [SerializeField] private Ball ball;

    public GameObject TextScorePlayer1;
    public GameObject TextScorePlayer2;

    public int PlayerScore1 = 0;
    public int PlayerScore2 = 0;

    public static GameManager instance;

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
        Reset();
    }

    void Update()
    {
        
    }
}
