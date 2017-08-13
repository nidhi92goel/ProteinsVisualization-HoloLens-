
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public void LoadView()
    {
        InputField link = GameObject.Find("Link").GetComponent<InputField>();
        PlayerPrefs.SetString("idcode", link.text);
        SceneManager.LoadScene("Atoms view");
    }

    public void LoadMenu()
    {
        
        SceneManager.LoadScene("Search");
    }
}
