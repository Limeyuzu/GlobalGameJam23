using Assets.GenericTools.Event;
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

    private float zPos = 0.5f;

    private float countDown;

    float initialCountDown = 2f;

    float topSpeed = 30f;

    private float speed;

    float accelleration;

    int resetDistance = 10;

    public TimeBar timeBar;

    public Vector2Int gridPosition;

    public void ReSpawn()
    {
        speed = 0f;
        direction = (Direction)Random.Range(0, 4);
        timeBar.SetMaxBarValue(100);
        transform.rotation = Quaternion.identity;
        switch (direction)
        {
            case Direction.North:
                xPos = Random.Range(0, Globals.GridSizeX);
                yPos = -1;
                break;
            case Direction.South:
                xPos = Random.Range(0, Globals.GridSizeX);
                yPos = Globals.GridSizeY + 1;
                transform.Rotate(0f, 180.0f, 0f);
                break;
            case Direction.East:
                xPos = Globals.GridSizeX + 1;
                yPos = Random.Range(0, Globals.GridSizeY);
                transform.Rotate(0f, -90.0f, 0f);
                break;
            case Direction.West:
                xPos = -1;
                yPos = Random.Range(0, Globals.GridSizeY);
                transform.Rotate(0f, 90.0f, 0f);
                break;

        }
        countDown = initialCountDown;
        transform.position = new Vector3(xPos, zPos, yPos);
        accelleration = 0.1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        ReSpawn();
    }

    public void SlowerMower() 
    {
        speed = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown > 0)
        {
            timeBar.ShowBar();
            countDown -= Time.deltaTime;
            showChargebar();
        }
        else
        {
            if (speed < topSpeed) speed += accelleration;
            timeBar.HideBar();
            switch (direction)
            {
                case Direction.North:
                    yPos += speed * Time.deltaTime;
                    if (yPos > Globals.GridSizeY + resetDistance) ReSpawn();
                    break;
                case Direction.South:
                    yPos -= speed * Time.deltaTime;
                    if (yPos < -resetDistance) ReSpawn();
                    break;
                case Direction.East:
                    xPos -= speed * Time.deltaTime;
                    if (xPos < -resetDistance) ReSpawn();
                    break;
                case Direction.West:
                    xPos += speed * Time.deltaTime;
                    if (xPos > Globals.GridSizeX + resetDistance) ReSpawn();
                    break;
            }
            transform.position = new Vector3(xPos, zPos, yPos);
            gridPosition = new Vector2Int((int)xPos, (int)yPos);
            EventManager.Emit(GameEvent.MowerMoved, this);
        }
    }

    void showChargebar()
    {
        var percent = 100 * (countDown / initialCountDown);
        timeBar.SetBarValue((int)percent);
    }
}