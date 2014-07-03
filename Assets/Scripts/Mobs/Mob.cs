using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour
{
    public Spawner spawner;

    public int moneyDrop;
    public int experienceDrop;
    public int killsPlayerLive = 1;

    public float maxHealth;
    public float curHealth;

    public float maxShield;
    public float curShield;

    public float maxSpeed;
    public float curSpeed;

    public float maxArmor;
    public float curArmor;

    public bool isFlying = false;

    public float regenerationHealth;
    public float regenerationShield;
    public float regenerationArmor;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        curHealth = Mathf.Clamp(curHealth + regenerationHealth, 0, maxHealth);
        curShield = Mathf.Clamp(curShield + regenerationShield, 0, maxShield);
        curArmor = Mathf.Clamp(curArmor + regenerationArmor, 0, maxArmor);

    }


    public void TakeDamage(float damageAmount)
    {
        float damageToShield = Mathf.Clamp(damageAmount, 0, curShield);
        curShield = Mathf.Clamp(curShield - damageToShield, 0, curShield);
        damageAmount = Mathf.Clamp(damageAmount - damageToShield, 0, damageAmount);

        float damageToArmor = Mathf.Clamp(damageAmount, 0, curArmor);
        curArmor = Mathf.Clamp(curArmor - damageToArmor, 0, curArmor);
        damageAmount = Mathf.Clamp(damageAmount - damageToArmor, 0, damageAmount);

        curHealth = Mathf.Clamp(curHealth - damageAmount, 0, curHealth);

        if (curHealth <= 0)
        {


            spawner.MobsOfWave.Remove(gameObject);
            Destroy(gameObject);



        }

    }

    public void MobFinishedMace()
    {
        spawner.MobsOfWave.Remove(gameObject);
        Destroy(gameObject);
    }




    public float adjustment = 2.3f;
    private Vector3 worldPosition = new Vector3();
    private Vector3 screenPosition = new Vector3();
    //private Camera myCamera;
    public int healthBarHeight = 5;
    public int healthBarLeft = 110;
    public int barTop = 1;
    private GUIStyle myStyle = new GUIStyle();

    void Awake()
    {
        //myCamera = Camera.main;
    }



    //void OnGUI()
    //{

    //    worldPosition = new Vector3(transform.position.x, transform.position.y + adjustment, transform.position.z);
    //    screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

    //    //creating a ray that will travel forward from the camera's position    

    //    Ray ray = new Ray(Camera.main.transform.position, transform.forward);
    //    RaycastHit hit;

    //    Vector3 forward = transform.TransformDirection(Vector3.forward);

    //    float distance = Vector3.Distance(Camera.main.transform.position, transform.position); //gets the distance between the camera, and the intended target we want to raycast to

    //    //if something obstructs our raycast, that is if our characters are no longer 'visible,' dont draw their health on the screen.

    //    if (!Physics.Raycast(ray, out hit, distance))
    //    {
    //        GUI.color = Color.red;
    //        GUI.HorizontalScrollbar(new Rect(screenPosition.x - healthBarLeft / 2, Screen.height - screenPosition.y - barTop, 100, 0), 0, curHealth, 0, maxHealth); //displays a healthbar

    //        GUI.color = Color.white;
    //        GUI.contentColor = Color.white;

    //        GUI.Label(new Rect(screenPosition.x - healthBarLeft / 2, Screen.height - screenPosition.y - barTop + 5, 100, 100), "" + curHealth + "/" + maxHealth); //displays health in text format
    //    }

    //}
















    //void OnGUI()
    //{
    //    Vector2 targetPos;

    //    targetPos = Camera.main.WorldToScreenPoint(transform.position);



    //    GUI.Box(new Rect(targetPos.x, targetPos.y-100, 60, 20), curHealth + "/" + maxHealth);
    //}
}
