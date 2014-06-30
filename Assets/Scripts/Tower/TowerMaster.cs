using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerMaster
{

    // ############
    // Variablen
    // ------------

    public string towerName;           // Tower Name
    public float towerDamage;          // ausgeteilter Schaden
    public int towerCosts;             // Tower Kauf kosten
    public float towerMoveSpeed;       // Drehgeschwindigkeit des Towers
    public float towerShotFrequence;   // Schussfrequenz



    // ############
    // Methoden
    // ------------


    public TowerMaster(string name, float damage, int costs, float moveSpeed, float shotFrequence)
    {

        towerName = name;
        towerDamage = damage;
        towerCosts = costs;
        towerMoveSpeed = moveSpeed;
        towerShotFrequence = shotFrequence;

    } // END TowerMaster


    public static Dictionary<int, TowerMaster> GenerateTower()
    {
        Dictionary<int, TowerMaster> towerDictionary = new Dictionary<int, TowerMaster>();

        towerDictionary.Add(0, new TowerMaster("Standard", 5f, 100, 2f, 3f));

        return towerDictionary;
    }


} // END Class
