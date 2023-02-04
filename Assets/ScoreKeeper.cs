using Assets.GenericTools.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int playerScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Subscribe(GameEvent.ScoreIncremented, (amount) =>
        {
            playerScore += (int)amount;
            print(playerScore);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
