using System;
using System.Collections;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AtomsCreate : MonoBehaviour
{
   
    private int count = 0;
    public string content = "";

    // Use this for initialization
    void Start ()
    {
       
        GetCheck();
    }

    private void GetCheck()
    {
        String IDCode= PlayerPrefs.GetString("idcode");

        Text code = GameObject.Find("Code").GetComponent<Text>();
        code.text = IDCode.ToUpper().ToString();

        string link = "https://files.rcsb.org/view/" + IDCode + ".pdb";
      

        UnityWebRequest w = UnityWebRequest.Get(link);
        
        StartCoroutine(WaitForRequest(w));
    }

    IEnumerator WaitForRequest(UnityWebRequest www)
    {
        
        yield return www.Send();

        // check for errors
        if (www.isError)
        {
            Debug.Log(www.error);
        }
        else
        {
            content = www.downloadHandler.text;
            GenerateAtoms(content);
        }
       
    }

    void GenerateAtoms(String pdb)
    {
        GameObject Main = new GameObject();
        Main.name = "Main";

        float cam_x = 0;
        float cam_y = 0;
        float cam_z = 0;

        StringReader strReader = new StringReader(content);

        string title_string = "";

        string technique_string = "Technique(s) used:\n";


        string x_value;
        string y_value;
        string z_value;
        string[] atom_desc = new string[4];


        float x;
        float y;
        float z;

        string line;

        while ((line = strReader.ReadLine()) != null)
        {


            if (line.StartsWith("TITLE"))
            {
                for (int i = 10; i < 80; i++)
                {
                    if (!line[i].Equals(""))
                        title_string += line[i];
                    title_string = title_string.Replace("  ", " ");
                }
            }
            else if (line.StartsWith("EXPDTA"))
            {
                for (int i = 10; i < 79; i++)
                {
                    if (!line[i].Equals(";"))
                        technique_string += line[i];
                    else
                        technique_string += "\n";

                    technique_string = technique_string.Replace("  ", " ");
                }
            }


            if (line.StartsWith("ATOM"))
            {
                x_value = null;
                y_value = null;
                z_value = null;

             

                atom_desc[0] = "";
                atom_desc[1] = "";
                atom_desc[2] = "";
                atom_desc[3] = "";

                count++;

                //name of atom
                for (int i = 12; i < 16; i++)
                {
                    if (!line[i].Equals(" "))
                        atom_desc[0] += line[i];
                }

                //name of residue
                for (int i = 17; i < 20; i++)
                {
                    if (!line[i].Equals(" "))
                        atom_desc[2] += line[i];
                }

                //chain id
                if (!line[21].Equals(" "))
                    atom_desc[3] += line[21];


                //x coordinate
                for (int i = 30; i < 38; i++)
                {
                    if (!line[i].Equals(""))
                        x_value += line[i];
                }


                //y coordinate
                for (int i = 38; i < 46; i++)
                {
                    if (!line[i].Equals(""))
                        y_value += line[i];

                }

                //z coordinate
                for (int i = 46; i < 54; i++)
                {
                    if (!line[i].Equals(""))
                        z_value += line[i];

                }

                //name of element
                for (int i = 76; i < 78; i++)
                {
                    if (!line[i].Equals(" "))
                        atom_desc[1] += line[i];

                }

                GameObject atom = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                atom.transform.parent = Main.transform;
                atom.name = atom_desc[0] +";"+ atom_desc[1] + ";" + atom_desc[2] + ";" + atom_desc[3];

                x = float.Parse(x_value, CultureInfo.InvariantCulture.NumberFormat);
                y = float.Parse(y_value, CultureInfo.InvariantCulture.NumberFormat);
                z = float.Parse(z_value, CultureInfo.InvariantCulture.NumberFormat);

                atom.transform.position = new Vector3(x, y, z);
                atom.transform.localScale = new Vector3(3.0F, 3.0F, 3.0F);

                if (atom_desc[1].Equals(" O"))
                { atom.GetComponent<Renderer>().material.color = Color.red; }
                else if (atom_desc[1].Equals(" N"))
                { atom.GetComponent<Renderer>().material.color = Color.blue; }
                else if (atom_desc[1].Equals(" S"))
                { atom.GetComponent<Renderer>().material.color = Color.yellow; }
                else if (atom_desc[1].Equals(" C"))
                { atom.GetComponent<Renderer>().material.color = Color.grey; }
                else if (atom_desc[1].Equals(" H"))
                {
                    atom.GetComponent<Renderer>().material.color = Color.white;
                    atom.transform.localScale = new Vector3(2.5F, 2.5F, 2.5F);
                }

                
                atom.AddComponent<AtomClick>();
              

                cam_x += x;
                cam_y += y;
                cam_z += z;
            }



        }

        SetTitle(title_string);
        SetTechnique(technique_string);

        Main.transform.position=new Vector3(-cam_x/count, -cam_y/count, -cam_z/count+100);
     
       
    }

    void SetTitle(String title_string)
    {
        Text title = GameObject.Find("Title").GetComponent<Text>();
        if (!title_string.Equals(""))
            title.text = title_string;
    }

    void SetTechnique(String technique_string)
    {
        Text technique = GameObject.Find("Technique").GetComponent<Text>();
        if (!technique_string.Equals(""))
            technique.text = technique_string;
    }

 

    // Update is called once per frame
    void Update ()
    {
       
    }
}





