using Assets.GenericTools.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float moveDelay = 0.4f;
    float moveWaitTime = 0f;
    float gridSpacing = 1f;
    Vector3 targetPosition;
    float speed = 8f;
    Vector2Int gridPosition;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        EventManager.Emit(GameEvent.PlayerMoved, gridPosition);
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardInput = Input.GetKey("up");
        bool backwardInput = Input.GetKey("down");
        bool rightInput = Input.GetKey("right");
        bool leftInput = Input.GetKey("left");



        if (moveWaitTime >= moveDelay) {
            if (forwardInput) 
            {
                if (canMakeMove("forward"))
                {
                    targetPosition = transform.position + (transform.forward * gridSpacing);
                    moveWaitTime = 0f;
                    gridPosition.y++;
                    EventManager.Emit(GameEvent.PlayerMoved, gridPosition);
                }
            }
            if (backwardInput)
            {
                if (canMakeMove("backward"))
                {
                    targetPosition = transform.position + (-transform.forward * gridSpacing);
                    moveWaitTime = 0f;
                    gridPosition.y--;
                    EventManager.Emit(GameEvent.PlayerMoved, gridPosition);
                }
            }
            if (rightInput)
            {
                if (canMakeMove("right"))
                {
                    targetPosition = transform.position + (transform.right * gridSpacing);
                    moveWaitTime = 0f;
                    gridPosition.x++;
                    EventManager.Emit(GameEvent.PlayerMoved, gridPosition);
                }
            }
            if (leftInput)
            {
                if (canMakeMove("left"))
                {
                    targetPosition = transform.position + (-transform.right * gridSpacing);
                    moveWaitTime = 0f;
                    gridPosition.x--;
                    EventManager.Emit(GameEvent.PlayerMoved, gridPosition);
                }
            }
        }
        moveWaitTime += Time.deltaTime;
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }

    private bool canMakeMove(string moveDirection) 
    {
        if (moveDirection == "forward") {
            return gridPosition.y < Globals.GridSizeY - 1;
        }
        if (moveDirection == "backward") {
            return gridPosition.y > 0;
        }
        if (moveDirection == "left") {
            return gridPosition.x > 0;
        }
        if (moveDirection == "right") {
            return gridPosition.x < Globals.GridSizeX - 1;
        }
        return false;
    }

    private void printPosition(string key)
    {
        print($"{key}:{gridPosition}");
    }
}
