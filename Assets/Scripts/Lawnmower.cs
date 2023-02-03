/*using System.Collections;
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

    LevelGrid levelGrid;

    private float countDown;

    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        levelGrid = GetComponent<LevelGrid>();
        direction = (Direction)(int)Random.Range(0, 3);
        switch (direction)
        {
            case Direction.North:
                xPos = Random.Range(0, levelGrid.width);
                yPos = -1;
                break;
            case Direction.South:
                xPos = Random.Range(0, levelGrid.width);
                yPos = levelGrid.width + 1;
                break;
            case Direction.East:
                xPos = levelGrid.height + 1;
                yPos = Random.Range(0, levelGrid.width);
                break;
            case Direction.West:
                xPos = -1;
                yPos = Random.Range(0, levelGrid.width);
                break;

        }
        countDown = 5f;
        speed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(countDown > 0)
        {
            countDown -= Time.deltaTime;
        } else
        {
            switch(direction)
            {
                case Direction.North:
                    yPos += speed * Time.deltaTime;
                    break;
                case Direction.South:
                    yPos -= speed * Time.deltaTime;
                    break;
                case Direction.East:
                    xPos += speed * Time.deltaTime;
                    break;
                case Direction.West:
                    xPos -= speed * Time.deltaTime;
                    break;

            }
        }
    }
}
*/