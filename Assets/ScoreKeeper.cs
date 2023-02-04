using Assets.GenericTools.Event;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    int playerScore = 0;
    public TextMeshProUGUI score;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Subscribe(GameEvent.ScoreIncremented, (amount) =>
        {
            playerScore += (int)amount;
            score.text = $"SCORE: {playerScore}";
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
