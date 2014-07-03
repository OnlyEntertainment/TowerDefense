using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    public GameObject curWeapon;

    public float weaponRange = 5.0f;
    public float weaponDamage = 10.0f;

    public GameObject enemyObject;
    public Mob enemyMob;
    bool showEnemyStats = false;

    void Start()
    {

    }

    void OnGUI()
    {
        Debug.DrawRay(gameObject.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * weaponRange, Color.blue);
        //Debug.DrawRay(Camera.main.transform.position, Vector3.forward * 10, Color.blue);

        if (showEnemyStats) GUI.Label(new Rect(Screen.width / 2 - 100, 50, 200, 100), enemyObject.name);
        if (showEnemyStats) GUI.Label(new Rect(Screen.width / 2 - 100, 70, 200, 100), "Shield " + " - " + enemyMob.curShield + " / " + enemyMob.maxShield);
        if (showEnemyStats) GUI.Label(new Rect(Screen.width / 2 - 100, 90, 200, 100), "Armor " + " - " + enemyMob.curArmor + " / " + enemyMob.maxArmor);
        if (showEnemyStats) GUI.Label(new Rect(Screen.width / 2 - 100, 110, 200, 100), "Health " + " - " + enemyMob.curHealth + " / " + enemyMob.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        showEnemyStats = false;
        RaycastHit hit_MouseCourser;

        if (Physics.Raycast(gameObject.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit_MouseCourser, weaponRange)) //Layer 9 Terrain
        {
            enemyObject = hit_MouseCourser.collider.gameObject;
            if (enemyObject.tag == "Enemy")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (!curWeapon.GetComponent<Animation>().isPlaying)
                    {
                        enemyMob.TakeDamage(weaponDamage);
                        curWeapon.GetComponent<Animation>().Play();
                    }

                }

                showEnemyStats = true;
                enemyMob = enemyObject.GetComponent<Mob>();


            }

        }
    }
}

