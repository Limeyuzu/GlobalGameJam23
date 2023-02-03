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
        gridPosition.Set(Globals.gridSizeX, Globals.gridSizeY);
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
                targetPosition = transform.position + (transform.forward * gridSpacing);
                moveWaitTime = 0f;
            }
            if (backwardInput)
            {
                targetPosition = transform.position + (-transform.forward * gridSpacing);
                moveWaitTime = 0f;
            }
            if (rightInput)
            {
                targetPosition = transform.position + (transform.right * gridSpacing);
                moveWaitTime = 0f;
            }
            if (leftInput)
            {
                targetPosition = transform.position + (-transform.right * gridSpacing);
                moveWaitTime = 0f;
            }
        }
        moveWaitTime += Time.deltaTime;
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
}
