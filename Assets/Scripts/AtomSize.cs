using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomSize : MonoBehaviour
{
    public void Plus()
    {
        GameObject Main = GameObject.Find("Main");
        Transform[] children = Main.GetComponentsInChildren<Transform>();

     
        for (int i=1;i<children.Length;i++)
        {

            children[i].localScale += new Vector3(0.5F, 0.5F, 0.5F);
        }
     

    }

    public void Minus()
    {
        GameObject Main = GameObject.Find("Main");
        Transform[] children = Main.GetComponentsInChildren<Transform>();


        for (int i = 1; i < children.Length; i++)
        {

            children[i].localScale -= new Vector3(0.5F, 0.5F, 0.5F);
        }
    }

}
