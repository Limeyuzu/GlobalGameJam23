using Assets.GenericTools.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float moveDelay = 0.2f;
    float moveWaitTime = 0f;
    float gridSpacing = 1f;
    Vector3 targetPosition;
    float speed = 7f;
    Vector2Int gridPosition;
    Vector3 moveDirectionVector;
    string moveDirection;
    public Transform weedyModel;
    float weedyRotation = 0f;
    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        EventManager.Emit(GameEvent.PlayerMoved, gridPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            weedyModel.transform.rotation = Quaternion.identity;
            weedyModel.transform.Rotate(90f, 0, 0);
            return;
        }

        SetMoveDirection();

        if (moveWaitTime >= moveDelay)
        {
            if (canMakeMove(moveDirection)) {
                targetPosition = transform.position + (moveDirectionVector * gridSpacing);
                moveWaitTime = 0f;
                setGridPosition(moveDirection);
                EventManager.Emit(GameEvent.PlayerMoved, gridPosition);
            }
        }
        moveWaitTime += Time.deltaTime;
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        weedyModel.transform.rotation = Quaternion.identity;
        weedyModel.transform.Rotate(0, weedyRotation, 0);
    }

    private void setGridPosition(string moveDirection)
    {
        if(moveDirection=="forward")
        {
            gridPosition.y++;
            return;
        }

        if (moveDirection == "backward")
        {
            gridPosition.y--;
            return;
        }

        if (moveDirection == "left")
        {
            gridPosition.x--;
            return;
        }

        if (moveDirection == "right")
        {
            gridPosition.x++;
            return;
        }

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

    void SetMoveDirection()
    {
        bool forwardInput = Input.GetKey("up");
        bool backwardInput = Input.GetKey("down");
        bool rightInput = Input.GetKey("right");
        bool leftInput = Input.GetKey("left");

            if (forwardInput)
            {
                moveDirectionVector = transform.forward;
                moveDirection = "forward";
                weedyRotation = 0f; 
                return;
            }
            if (backwardInput)
            {
                moveDirectionVector = -transform.forward;
                moveDirection = "backward";
                weedyRotation = 180f;
            return;
            }
            if (rightInput)
            {
                moveDirectionVector = transform.right;
                moveDirection = "right";
                weedyRotation = 90f;
                return;
            }
            if (leftInput)
            {
                moveDirectionVector = -transform.right;
                moveDirection = "left";
                weedyRotation = 270f;
            return;
            }

            return;
    }

    private void printPosition(string key)
    {
        print($"{key}:{gridPosition}");
    }

    void OnTriggerEnter(Collider collider)
    {
        var lawnmower = collider.gameObject.GetComponent(typeof(Lawnmower)) as Lawnmower;
        if (lawnmower != null && moveDirection != null)
        {
            Debug.Log("game over");
            lawnmower.SlowerMower();
            speed = 0f;
            isDead = true;
            EventManager.Emit(GameEvent.GameOver);
        }
    }
}
