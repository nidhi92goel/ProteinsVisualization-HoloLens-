using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimize : MonoBehaviour
{

    public void ZoomOut()
    {
        GameObject Main = GameObject.Find("Main");
        Vector3 change_z = new Vector3(0, 0, 10.0F);
        Main.transform.position += change_z; 
    }
}
