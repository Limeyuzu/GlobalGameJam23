using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempBarController : MonoBehaviour
{

    public TimeBar bar;
    private int time;

    // Start is called before the first frame update
    void Start()
    {
        time = 100;
        bar.SetMaxBarValue(100);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            time -= 10;
            bar.SetBarValue(time);
        }
    }
}
