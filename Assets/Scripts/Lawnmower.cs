using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    North, East, South, West
}


public class Lawnmower : MonoBehaviour
{
    private Direction direction;

    private float xPos;

    private float yPos;

    private float countDown;

    private float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        direction = (Direction)(int)Random.Range(0, 3);
        Debug.Log(direction.ToString());
        transform.rotation.Set(0f, 0f, 0f, 1f);
        switch (direction)
        {
            case Direction.North:
                xPos = -Random.Range(0, Globals.gridSizeX - 1);
                yPos = -1;
                break;
            case Direction.South:
                xPos = -Random.Range(0, Globals.gridSizeX - 1);
                yPos = Globals.gridSizeY + 1;
                transform.Rotate(0f, 180.0f, 0f);
                break;
            case Direction.East:
                xPos = -Globals.gridSizeX - 1;
                yPos = Random.Range(0, Globals.gridSizeY);
                transform.Rotate(0f, 90.0f, 0f);
                break;
            case Direction.West:
                xPos = 1;
                yPos = Random.Range(0, Globals.gridSizeY);
                transform.Rotate(0f, -90.0f, 0f);
                break;

        }
        countDown = 1f;
        transform.position = new Vector3(xPos, 0.0f, yPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
        }
        else
        {
            switch (direction)
            {
                case Direction.North:
                    yPos += speed * Time.deltaTime;
                    if (yPos > Globals.gridSizeY) Start();
                    break;
                case Direction.South:
                    yPos -= speed * Time.deltaTime;
                    if (yPos < 0) Start();
                    break;
                case Direction.East:
                    xPos += speed * Time.deltaTime;
                    if (xPos > 0) Start();
                    break;
                case Direction.West:
                    xPos -= speed * Time.deltaTime;
                    if (xPos < Globals.gridSizeX) Start();
                    break;
            }
            transform.position = new Vector3(xPos, 0.0f, yPos);
        }
    }
}