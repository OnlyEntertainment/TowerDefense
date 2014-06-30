using UnityEngine;
using System.Collections;

public class EnemyDetectorData : MonoBehaviour {

	void Start () 
    {
	
	} // END Start
	
	void Update () 
    {
	
	} // END Update


    void OnTriggerEnter(Collider collision)
    {

        if (collision.tag == "Enemy")
        {

            TowerProperties towerData = gameObject.transform.parent.gameObject.GetComponent<TowerProperties>();
            towerData.SetTarget(collision.gameObject);


        }

    } // END OnTriggerEnter


    void OnTriggerExit(Collider collision)
    {

        if (collision.tag == "Enemy")
        {
            TowerProperties towerData = gameObject.transform.parent.gameObject.GetComponent<TowerProperties>();
            towerData.RemoveTarget(collision.gameObject);
        }


    } // END OnTriggerExit



} // END Class
