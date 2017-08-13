
using UnityEngine;
using UnityEngine.UI;

public class AtomClick : MonoBehaviour {

    void OnMouseDown()
    {
        Text desc= GameObject.Find("Atom_Desc").GetComponent<Text>();
        desc.text = "Atom Description:";

        string info = this.name;
        char seperator = ';';
        string[] atom_desc = info.Split(seperator);

        Text atom_info = GameObject.Find("Atom").GetComponent<Text>();
        atom_info.text = "Name: " + atom_desc[0] + "\n" + "Element: " + atom_desc[1] + "\n" + "Residue Name: " + atom_desc[2] + "\n" + "Chain: " + atom_desc[3]; ;
    }
}
