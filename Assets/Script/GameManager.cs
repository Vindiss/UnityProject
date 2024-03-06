using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Timers;
using UnityEditor;
using TMPro;
using Unity.VisualScripting;
using System.Drawing;
using UnityEditor.SearchService;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Transform Player1PositionReset;
    [SerializeField] private Transform Player2PositionReset;
    [SerializeField] private Transform BallPositionReset;
    [SerializeField] private Player player1;
    [SerializeField] private Player player2;
    [SerializeField] private Ball ball;

    [SerializeField] private GameObject ResultUI;
    [SerializeField] private GameObject ResultScorePlayer1Text;
    [SerializeField] private GameObject ResultScorePlayer2Text;
    [SerializeField] private GameObject ResultWinnerText;

    public GameObject TextGameTimer;
    public GameObject TextScorePlayer1;
    public GameObject TextScorePlayer2;
    public GameObject TextForGameAnnouncement;

    private System.Timers.Timer GameTimerIntervalle;
    public int GameTimer;
    public bool Overtime;
    public bool GameEnd;

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

    public Ball GetBall() { return ball; }

    public void Reset()
    {
        player1.transform.position = Player1PositionReset.position;
        player1.transform.rotation = Player1PositionReset.rotation;
        player2.transform.rotation = Player2PositionReset.rotation;
        player2.transform.position = Player2PositionReset.position;
        ball.transform.position = BallPositionReset.position;
        ball.transform.localScale = BallPositionReset.localScale;
        ball.ResetBall();
    }

    void Start()
    {
       /* ResultUI.SetActive(false);*/
        GameTimerIntervalle = new System.Timers.Timer(1000);
        GameTimerIntervalle.Elapsed += OnTimedEvent;
        GameTimerIntervalle.Start();
        GameTimer = 15;
        Overtime = false;
        GameEnd = false;
        TextGameTimer.GetComponent<TextMeshProUGUI>().SetText($"{GameTimer / 60}:{GameTimer % 60}");
        TextScorePlayer1.GetComponent<TextMeshProUGUI>().SetText($"{PlayerScore1}");
        TextScorePlayer2.GetComponent<TextMeshProUGUI>().SetText($"{PlayerScore2}");
        FindAnyObjectByType<Obstacles>().Generate();
        Reset();
    }

    void Update()
    {
        TextGameTimer.GetComponent<TextMeshProUGUI>().SetText($"{GameTimer / 60}:{(GameTimer % 60):D2}");
    }

    private void OnTimedEvent(object sender, ElapsedEventArgs e)
    {
        if (Overtime)
        {
            GameTimer++;
        }
        else
        {
            GameTimer--;
        }
        if( GameTimer == 0 )
        {
            if(PlayerScore1 != PlayerScore2)
            {
                Result();
            }
            else
            {
                Overtime = true;
                TextForGameAnnouncement.GetComponent<TextMeshProUGUI>().text = "Overtime !";
                Invoke(TextForGameAnnouncement.GetComponent<TextMeshProUGUI>().text = "Overtime !", 2f);
                Reset();
            }
        }
    }   

    public void Result()
    {
        ResultUI.SetActive(true);
        ResultScorePlayer1Text.GetComponent<TextMeshProUGUI>().text = $"{PlayerScore1}";
        ResultScorePlayer2Text.GetComponent<TextMeshProUGUI>().text = $"{PlayerScore2}";
        if (PlayerScore1 > PlayerScore2)
        {
            ResultWinnerText.GetComponent<TextMeshProUGUI>().text = "Player1 Wins";
            ResultWinnerText.GetComponent<TextMeshProUGUI>().color = TextScorePlayer1.GetComponent<TextMeshProUGUI>().color;
        }
        else
        {
            ResultWinnerText.GetComponent<TextMeshProUGUI>().text = "Player2 Wins";
            ResultWinnerText.GetComponent<TextMeshProUGUI>().color = TextScorePlayer2.GetComponent<TextMeshProUGUI>().color;
        }

        GameEnd = true;
        ball.GetRB().isKinematic = true;
    }

    void StopGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
