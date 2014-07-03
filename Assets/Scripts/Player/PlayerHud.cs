using UnityEngine;
using System.Collections;

public class PlayerHud : MonoBehaviour {

    public PlayerKeyBinding playerKeyBinding;
    
    
    
    public int playerMoney;
    public float playerHealth;


    public bool menuChoice = false;             // true - Baumodus | false - Kampfmodus

    public Texture2D iconSword;
    public Texture2D iconGun;
    public Texture2D iconTower;
    public Texture2D iconWall;

	void Start () 
    {
	
	} // END Start
	
	void Update () 
    {

        if (Input.GetKeyDown(playerKeyBinding.menuChangeChoice))
        {
            if (menuChoice == false)
            {
                menuChoice = true;
            }
            else
            {
                menuChoice = false;
            }
        }


	} // END Update


    void OnGUI()
    {

        if (menuChoice == false)
        {
            GUI.Box(new Rect(5, 5, 100, 100), iconSword);
            GUI.Box(new Rect(115, 5, 100, 100), iconGun);

            GUI.Box(new Rect(5, 115, 100, 100), "Fight");

        }
        else
        {
            GUI.Box(new Rect(5, 5, 100, 100), iconTower);
            GUI.Box(new Rect(115, 5, 100, 100), iconWall);

            GUI.Box(new Rect(5, 115, 100, 100), "Build");
        }


        GUI.Label(new Rect(5, Screen.height - 50, 200, 20), "Money: " + playerMoney);
        GUI.Label(new Rect(5, Screen.height - 20, 200, 20), "Health: " + playerHealth);
    } // END OnGUI




} // END Class
