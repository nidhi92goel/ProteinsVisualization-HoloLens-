using UnityEngine;

public class Maximize : MonoBehaviour {

	public void ZoomIn()
    {
        GameObject Main = GameObject.Find("Main");
        Vector3 change_z = new Vector3(0, 0, 10.0F);
        Main.transform.position -= change_z; 
    }
}
