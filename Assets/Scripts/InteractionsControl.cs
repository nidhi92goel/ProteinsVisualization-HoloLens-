/*
 * The class contains functions which describe the various components of the interactive 
 * user interface provided to the user. These help control and manipulate the hologram so 
 * as to analyze and study the protein structure better
*/

using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class InteractionsControl : MonoBehaviour
{ 
    public GameObject Main;          //Space fill structure
    public GameObject Water;         //Water molecules surrounding the protein structures
    public GameObject Backbone;      //Backbone structure
    public GameObject Secondary;     //Cartoon strcuture

    Vector3 center=Vector3.zero;     //Vector to find the center of the structure

    public Button plus_button;       //Button to increase size of atoms in space fill view
    public Button minus_button;      //Button to decrease size of atoms in space fill view

    private bool val_s;              //boolean value to know if the spin toggle button is clicked

	/* -------------------------------------------------------------------------------------------
     * This function is used to make the appropriate protein structure visible and others 
     * invisible when the user selects from space fill, backbone or cartoons views in the
     * drop down menu available.
     * Here, integer index determined the item of the dropdown menu selected.
	*/
    public void viewchanged(int index)
    { 
        if(index==0)                     //index value for space fill view
        {
            Main.SetActive(true);        //space fill structure being enabled
			Backbone.SetActive(false);   //backbone structure being disabled
			Secondary.SetActive(false);  //secondary structure being disabled

			//atom size control buttons need to be made active in space fill view
            plus_button.interactable = true;
            minus_button.interactable = true;
        }
		else if(index==1)           //index value for backbone view
        {
            Main.SetActive(false);
            Backbone.SetActive(true);
            Secondary.SetActive(false);

			//atom size control buttons need to be made inactive in backbone view
            plus_button.interactable = false;
            minus_button.interactable = false;
        }
		else if(index==2)          //index value for cartoon view
        {
            Main.SetActive(false);
            Backbone.SetActive(false);
            Secondary.SetActive(true);

			//atom size control buttons need to be made inactive in cartoon view
            plus_button.interactable = false;
            minus_button.interactable = false;
        }
    }
        
	/* -------------------------------------------------------------------------------------------
     * These functions are used to control the size of the protein structure. They control size of 
     * space fill, backbone and cartoon strcutures as well as water molecules as a whole. 
     * The structures are being enlarged keeping in mind they always need to be placed in the 
     * center of the screen. Hence, their position is also being controlled realtive to their change
     * in size
	*/

    //fuction to increase the size of the structures
    public void ZoomIn()
    {
		//vector by which size is increased in each click
        Vector3 change_z = new Vector3(0.005F, 0.005F, 0.005F);        

		//factor determining the change in position in relation with the change in size
        float factor = (Main.transform.localScale.x+0.005F)/ Main.transform.localScale.x;  

        Main.transform.localScale += change_z;  //size change

		//position change (only the x and y positions are changed and not z because 
		//the hologram needs to be at a contant distance away from the camera 
		Main.transform.position = new Vector3
			(Main.transform.position.x*factor,Main.transform.position.y*factor,Main.transform.position.z); 
        
		Water.transform.localScale += change_z;
        Water.transform.position = new Vector3
			(Main.transform.position.x*factor,Main.transform.position.y*factor,Main.transform.position.z);
        
		Backbone.transform.localScale += change_z;
        Backbone.transform.position = new Vector3
			(Main.transform.position.x*factor,Main.transform.position.y*factor,Main.transform.position.z);
        
		Secondary.transform.localScale += change_z;
        Secondary.transform.position = new Vector3
			(Main.transform.position.x*factor,Main.transform.position.y*factor,Main.transform.position.z);
    }

	//fuction to decrease the size of the structures
    public void ZoomOut()
    {
		//vector by which size is increased in each click
        Vector3 change_z = new Vector3(0.005F, 0.005F, 0.005F);

		//factor determining the change in position in relation with the change in size
        float factor = (Main.transform.localScale.x - 0.005F) / Main.transform.localScale.x;

		if (Main.transform.localScale.x > 0.01F)    //ensures that size of structure doesn't become negative
		{
			Main.transform.localScale -= change_z; //size change

			//position change (only the x and y positions are changed and not z because 
			//the hologram needs to be at a contant distance away from the camera 
			Main.transform.position = new Vector3 
				(Main.transform.position.x*factor,Main.transform.position.y*factor,Main.transform.position.z);

			Water.transform.localScale -= change_z;
			Water.transform.position = new Vector3 
				(Main.transform.position.x*factor,Main.transform.position.y*factor,Main.transform.position.z);

			Backbone.transform.localScale -= change_z;
			Backbone.transform.position = new Vector3 
				(Main.transform.position.x*factor,Main.transform.position.y*factor,Main.transform.position.z);

			Secondary.transform.localScale -= change_z;
			Secondary.transform.position = new Vector3 
				(Main.transform.position.x*factor,Main.transform.position.y*factor,Main.transform.position.z);
		}
    }

	/* -------------------------------------------------------------------------------------------
     * These functions are used to control the size of the atoms in the space fill view of the 
     * protein structure. They increase and decrease the size of atoms of both the space fill 
     * structure and the water molecules surrounding them.
	*/

    //function to increase the size of the atoms in space fill view
    public void Plus()
    {
		//gets the transform components of all children with the parent and stores them in an array
        Transform[] children = Main.GetComponentsInChildren<Transform>();

        for (int i = 1; i < children.Length; i++)  //indexing from 1 as the first transform component 
												   //is that of the parent
        {
            if (children[1].localScale.x < 5)      //ensures that atoms don't become too big in size
                children[i].localScale += new Vector3(0.5F, 0.5F, 0.5F);  //size increase
        }

    }

	//function to decrease the size of the atoms in space fill view
    public void Minus()
    {
		//gets the transform components of all children with the parent and stores them in an array
        Transform[] children = Main.GetComponentsInChildren<Transform>();

		for (int i = 1; i < children.Length; i++) //indexing from 1 as the first transform component 
												  //is that of the parent
        {
			if (children[1].localScale.x > 0 && children[i].localScale.x>0) //ensures that the atom's 
																	  //don't become too small in size
            {
                children[i].localScale -= new Vector3(0.5F, 0.5F, 0.5F);  //size decrease
            }
        }
    }

	/* -------------------------------------------------------------------------------------------
     * These functions are used to control the rotation of the structure in different directions 
	*/
    public void Up()
    {
        Main.transform.Rotate(10, 0, 0, Space.World);
        Water.transform.Rotate(10, 0, 0, Space.World);
        Backbone.transform.Rotate(10, 0, 0, Space.World);
        Secondary.transform.Rotate(10, 0, 0, Space.World);
    }
    public void Down()
    {
        Main.transform.Rotate(-10, 0, 0, Space.World);
        Water.transform.Rotate(-10, 0, 0, Space.World);
        Backbone.transform.Rotate(-10, 0, 0, Space.World);
        Secondary.transform.Rotate(-10, 0, 0, Space.World);
    }
    public void Left()
    {
        Main.transform.Rotate(0, 10, 0, Space.World);
        Water.transform.Rotate(0, 10, 0, Space.World);
        Backbone.transform.Rotate(0, 10, 0, Space.World);
        Secondary.transform.Rotate(0, 10, 0, Space.World);
    }
    public void Right()
    {
        Main.transform.Rotate(0, -10, 0, Space.World);
        Water.transform.Rotate(0, -10, 0, Space.World);
        Backbone.transform.Rotate(0, -10, 0, Space.World);
        Secondary.transform.Rotate(0, -10, 0, Space.World);
    }

	/* -------------------------------------------------------------------------------------------
     * These set of functions are used to control the spinning of the structure when the user
     * selects the toggle button to do so
	*/

	//function called when the user clicks the spin on/off toggle button
    public void SpinChange(bool value)
    {
        val_s = value;  //value to determine user's selection out of switching on or off the spin

		//to find the center of the whole structure about which the structures need to be rotated
		if (center == Vector3.zero) 
		{
			//space fill structure's name already contains the coordinates of the center as it
			//added to it in the AtomsCreate class
			string x_center = Main.name.Split (';') [1]; 
			string y_center = Main.name.Split (';') [2];
			string z_center = Main.name.Split (';') [3];

			//converts string to float
			float x_mid = float.Parse (x_center, CultureInfo.InvariantCulture.NumberFormat);
			float y_mid = float.Parse (y_center, CultureInfo.InvariantCulture.NumberFormat);
			float z_mid = float.Parse (z_center, CultureInfo.InvariantCulture.NumberFormat);

			center = new Vector3 (x_mid, y_mid, z_mid);
		}

    }

	//this function is called once per frame 
    void Update()
    {
        if (val_s == true)    //if user has chosen to switch on the spinning
        {
			//rotate the structures around its center point along the y axis
            Main.transform.RotateAround(center, Vector3.up, 15*Time.deltaTime); 
            Water.transform.RotateAround(center, Vector3.up, 15 * Time.deltaTime);
            Backbone.transform.RotateAround(center, Vector3.up, 15 * Time.deltaTime);
            Secondary.transform.RotateAround(center, Vector3.up, 15 * Time.deltaTime);
        }
    }

	/* -------------------------------------------------------------------------------------------
     * This function is used to control the visibility of the water molecules depending on the 
     * user's selection in the toggle button
	*/
    public void WaterVisible(bool value)
    {
			Water.SetActive(value);  
    }

	/* -------------------------------------------------------------------------------------------
     * This function is used to control the spinning of the structure when the user
     * selects the toggle butto to do so
	*/
    public void HydrogenVisible(bool value)
    {
       
        Renderer[] children = Main.GetComponentsInChildren<Renderer>();

        string name;
        char seperator = '.';
        string[] atom_desc;
        for (int i = 1; i < children.Length; i++)
        {
            name = children[i].name;
            atom_desc = name.Split(seperator);
            if(atom_desc[1].Contains("H"))
            {
                if (value == true)
                {
                    children[i].enabled = true;
                    
                }
                else
                    children[i].enabled = false;
            }
        }
    }
}
