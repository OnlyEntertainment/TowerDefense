using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerProperties : MonoBehaviour
{

    // ############
    // Variablen
    // ------------

    public string towerName = "normal";         // Tower Name
    public float towerDamage = 15f;              // ausgeteilter Schaden
    public int towerCosts = 100;                // Tower Kauf kosten
    public float towerMoveSpeed = 3f;           // Drehgeschwindigkeit des Towers
    public float towerShotFrequence = 2f;       // Schussfrequenz

    public GameObject rotateObject;


    private float shotTime = 0f;

    public List<GameObject> enemyList = new List<GameObject>();
    public GameObject targetEnemy;                     // aktuell anvisiertes Ziel
    bool targetActivated = false;               // hat der Tower ein aktuelles Ziel?
    // ############
    // Methoden
    // ------------

    void Start()
    {

    } // END Start

    void Update()
    {

        if (enemyList.Count == 0)
        {
            targetActivated = false;
        }

        if (targetActivated == true)
        {
            if (enemyList[0] == null)
            {
                enemyList.RemoveAt(0);
            }
            else
            {
                targetEnemy = enemyList[0];
                Vector3 enemyPosition = new Vector3(targetEnemy.transform.position.x, rotateObject.transform.position.y, targetEnemy.transform.position.z);
                rotateObject.transform.LookAt(enemyPosition);

                Shotting();
            }
        }


    } // END Update


    public void SetTarget(GameObject enemyObject)
    {

        if (enemyList.Contains(enemyObject) == false)
        {

            enemyList.Add(enemyObject);
            targetActivated = true;

        }


    } // END SetTarget

    public void RemoveTarget(GameObject enemyObject)
    {

        if (enemyList.Contains(enemyObject) == true)
        {
            enemyList.Remove(enemyObject);
        }

    } // END RemoveTarget

    void Shotting()
    {

        shotTime -= 1 * Time.deltaTime;

        if (shotTime <= 0f)
        {

            targetEnemy.GetComponent<Mob>().TakeDamage(towerDamage);

            shotTime = towerShotFrequence;
        }



    } // END Shotting


    public void TargetDestroyed()
    {



    } // END TargetDestroyed


} // END Class
