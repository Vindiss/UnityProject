using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField]  private PlayerEnum player;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (player == PlayerEnum.Player1)
            {
                gameManager.PlayerScore1 += 1;
                Debug.Log(gameManager.PlayerScore1);
            }
            else
            {
                gameManager.PlayerScore2 += 1;
                Debug.Log(gameManager.PlayerScore2);
            }
            GameManager.Instance().Reset();
        }
    }
}
