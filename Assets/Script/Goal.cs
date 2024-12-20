using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField]  private PlayerEnum player;


    private bool haveGoal;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance();
        haveGoal = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && !haveGoal)
        {
            gameManager.GetBall().GetRB().isKinematic = true;
            gameManager.GetGameTimerIntervalle().Stop();
            if (player == PlayerEnum.Player1)
            {
                gameManager.PlayerScore1 += 1;
                haveGoal = true;
                gameManager.TextScorePlayer1.GetComponent<TextMeshProUGUI>().SetText($"{gameManager.PlayerScore1}");
                gameManager.TextForGameAnnouncement.GetComponent<TextMeshProUGUI>().SetText("Player 1 Scored !");
                StartCoroutine(GoalAnimation());
            }
            else
            {
                gameManager.PlayerScore2 += 1;
                haveGoal = true;
                gameManager.TextScorePlayer2.GetComponent<TextMeshProUGUI>().SetText($"{gameManager.PlayerScore2}");
                gameManager.TextForGameAnnouncement.GetComponent<TextMeshProUGUI>().SetText("Player 2 Scored !");
                StartCoroutine(GoalAnimation());
            }
        }
    }

    private IEnumerator GoalAnimation()
    {
        for (float i = 1; i > 0.2f; i -= 0.1f)
        {
            gameManager.GetBall().transform.localScale *= i;
            yield return new WaitForSeconds(0.2f);
        }
        gameManager.TextForGameAnnouncement.GetComponent<TextMeshProUGUI>().SetText("");
        if (gameManager.Overtime)
        {
            gameManager.Result();
        }
        else
        {
            gameManager.Reset();
            gameManager.Kickoff();
        }
        haveGoal = false;
    }
}
