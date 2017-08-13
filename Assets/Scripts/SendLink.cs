using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SendLink : MonoBehaviour
{
   

    public void LoadScene()
    {

        InputField link = GameObject.Find("Link").GetComponent<InputField>();

        PlayerPrefs.SetString("idcode", link.text);

        SceneManager.LoadScene("Atoms view");

    }
}
