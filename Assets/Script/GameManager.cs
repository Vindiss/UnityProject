using System.Collections;
using UnityEngine;
using System.Timers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

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
    [SerializeField] private GameObject PlayerControleUI;

    public GameObject TextGameTimer;
    public GameObject TextScorePlayer1;
    public GameObject TextScorePlayer2;
    public GameObject TextForGameAnnouncement;

    private System.Timers.Timer GameTimerIntervalle;
    public int GameTimer;
    public bool Overtime;
    public bool GameStart;
    public bool GameEnd;
    public bool KickOff;
    private int KickOffCountDown;

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
    public System.Timers.Timer GetGameTimerIntervalle() { return GameTimerIntervalle; }

    public void Reset()
    {
        player1.transform.position = Player1PositionReset.position;
        player1.transform.rotation = Player1PositionReset.rotation;
        player1.SetEnergy(33);
        player2.transform.rotation = Player2PositionReset.rotation;
        player2.transform.position = Player2PositionReset.position;
        player2.SetEnergy(33);
        ball.transform.position = BallPositionReset.position;
        ball.transform.localScale = BallPositionReset.localScale;
        ball.ResetBall();
    }

    void Start()
    {
        ResultUI.SetActive(false);
        GameTimerIntervalle = new System.Timers.Timer(1000);
        GameTimerIntervalle.Elapsed += OnTimedEvent;
        GameTimerIntervalle.Start();
        Overtime = false;
        GameStart = true;
        GameEnd = false;
        KickOff = false;
        KickOffCountDown = 3;
        TextGameTimer.GetComponent<TextMeshProUGUI>().SetText($"{GameTimer / 60}:{GameTimer % 60}");
        TextScorePlayer1.GetComponent<TextMeshProUGUI>().SetText($"{PlayerScore1}");
        TextScorePlayer2.GetComponent<TextMeshProUGUI>().SetText($"{PlayerScore2}");
        Reset();
        PrintControles();
    }

    void Update()
    {
        TextGameTimer.GetComponent<TextMeshProUGUI>().SetText($"{GameTimer / 60}:{(GameTimer % 60):D2}");
        if (GameTimer == 0 && !Overtime)
        {
            if (PlayerScore1 != PlayerScore2)
            {
                Result();
            }
            else
            {
                Overtime = true;
                TextForGameAnnouncement.GetComponent<TextMeshProUGUI>().SetText("Overtime !");
                Invoke(nameof(ClearGameAnnouncement), 2f);
                Reset();
                Kickoff();
            }
        }
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

        GameTimerIntervalle.Stop();
        GameEnd = true;
        ball.GetRB().isKinematic = true;
    }

    public void ClearGameAnnouncement()
    {
        TextForGameAnnouncement.GetComponent<TextMeshProUGUI>().SetText("");
    }

    public void Kickoff()
    {
        GameTimerIntervalle.Stop();
        KickOff = true;
        ball.GetRB().isKinematic = true;
        StartCoroutine(KickOffAnnouncement());
    }

    public IEnumerator KickOffAnnouncement()
    {
        for (int i = KickOffCountDown; i > 0; i--) 
        {
            TextForGameAnnouncement.GetComponent<TextMeshProUGUI>().SetText($"{i}");

            yield return new WaitForSeconds(1f);
        }
        KickOff = false;
        ball.GetRB().isKinematic = false;
        GameTimerIntervalle.Start();
        TextForGameAnnouncement.GetComponent<TextMeshProUGUI>().SetText("GO");

        yield return new WaitForSeconds(1f);

        ClearGameAnnouncement();
    }

    public void PrintControles()
    {
        PlayerControleUI.SetActive(true);
        GameStart = false;
        ball.GetRB().isKinematic = true;
        GameTimerIntervalle.Stop();
        StartCoroutine(WaitForPlay());
    }

    public IEnumerator WaitForPlay()
    {
        float enter = 0;
        while (enter == 0)
        {
            enter = Input.GetAxis("LaunchGame");

            yield return new WaitForNextFrameUnit();
        }

        PlayerControleUI.SetActive(false);
        GameStart = true;
        ball.GetRB().isKinematic = false;
        Kickoff();
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void StopGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
