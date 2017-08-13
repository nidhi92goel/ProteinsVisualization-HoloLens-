using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    

    public void Up()
    {
        GameObject Main = GameObject.Find("Main");
        Main.transform.Rotate(10, 0, 0, Space.Self);
    }

    public void Down()
    {
        GameObject Main = GameObject.Find("Main");
        Main.transform.Rotate(-10, 0, 0, Space.Self);
    }

    public void Left()
    {
        GameObject Main = GameObject.Find("Main");
        Main.transform.Rotate(0, 10, 0,Space.Self);
    }

    public void Right()
    {
        GameObject Main = GameObject.Find("Main");
        Main.transform.Rotate(0, -10, 0, Space.Self);
    }
}
