using UnityEngine;
using System.Collections;

public class PlayerHud : MonoBehaviour {

    public PlayerKeyBinding playerKeyBinding;
    public SetTower setTower;
    
    
    public int playerMoney;
    public float playerHealth;


    public bool menuChoice = false;             // true - Baumodus | false - Kampfmodus

    public Texture2D iconSword;
    public Texture2D iconGun;
    public Texture2D iconTower;
    public Texture2D iconWall;

    // Initialisieren
    private int currentFightChoice = 1;
    private int currentBuildChoice = 1;





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
            GUI.Box(new Rect(5, 5, 50, 50), iconSword);
            GUI.Box(new Rect(75, 5, 50, 50), iconGun);

            GUI.Label(new Rect(5, 65, 200, 30), "Fight");

            if (currentFightChoice == 1)
            {
                GUI.Box(new Rect(5, 90, 50, 50), iconSword);
            }
            else
            {
                GUI.Box(new Rect(5, 90, 50, 50), iconGun);
            }

            if (Input.GetKeyDown(playerKeyBinding.menuFightChoice1))
            {currentFightChoice = 1; }
            else if (Input.GetKeyDown(playerKeyBinding.menuFightChoice2))
            {currentFightChoice = 2;}
        }
        else
        {
            GUI.Box(new Rect(5, 5, 50, 50), iconTower);
            GUI.Box(new Rect(75, 5, 50, 50), iconWall);

            GUI.Label(new Rect(5, 65, 200, 30), "Build");

            if (currentBuildChoice == 1)
            {
                GUI.Box(new Rect(5, 90, 50, 50), iconTower);
            }
            else
            {
                GUI.Box(new Rect(5, 90, 50, 50), iconWall);
            }


            if (Input.GetKeyDown(playerKeyBinding.menuFightChoice1))
            { currentBuildChoice = 1; setTower.SetCurrentBuildingObject(currentBuildChoice); }
            else if (Input.GetKeyDown(playerKeyBinding.menuFightChoice2))
            { currentBuildChoice = 2; setTower.SetCurrentBuildingObject(currentBuildChoice); }
        }


        GUI.Label(new Rect(5, Screen.height - 50, 200, 20), "Money: " + playerMoney);
        GUI.Label(new Rect(5, Screen.height - 20, 200, 20), "Health: " + playerHealth);
    } // END OnGUI




} // END Class
