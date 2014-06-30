using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	
	public string unitType;
	
	public float ownUnit_WalkSpeed; //Bewegungsgeschwindigkeit der Einheit
	
	public float ownUnit_AreaRadius; //Radius des SelectionRing-Projektors
	
	public string ownUnit_OnAction; //derzeitige Aktion der Einheit
	public Vector2 ownUnit_TargetPoint; //für "walk"-Aktion; Zielort
	
	public GameObject selectionProjector_public; //Prefab des SelectionRing-Projektors
	GameObject selectionProjector_own;
	
	void Awake ()
	{
		ownUnit_OnAction = "idle";
	}
	
	void Update ()
	{
		switch(ownUnit_OnAction)
		{
		case "walk":
                //Debug.Log("Gehe in Walk");
			//höchstens eine Frame-Bewegung vom Ziel
			if ((ownUnit_TargetPoint - new Vector2(transform.position.x, transform.position.z)).magnitude >= ownUnit_WalkSpeed*Time.deltaTime)
			{       
					transform.LookAt(new Vector3(ownUnit_TargetPoint.x, transform.position.y, ownUnit_TargetPoint.y));
					transform.Translate(Vector3.forward * ownUnit_WalkSpeed *Time.deltaTime);
					transform.position = new Vector3 (transform.position.x, Terrain.activeTerrain.SampleHeight(transform.position)+transform.localScale.y, transform.position.z);	
			}
			else 
			{
				ownUnit_OnAction = "idle"; //nach Ankommen auf "idle"-Aktion
			}
		break;
		}
	}
	
	
	
	public void Unit_GoToPoint (Vector3 newTargetPoint) //in Bewegung setzen, falls keine vorrangige Aktion läuft
	{
        Debug.Log("Erhalte Befehle ("+ownUnit_OnAction+") --> Fahre zu " + newTargetPoint);
		if (ownUnit_OnAction != "Aktion mit Vorrang")
		{
			ownUnit_OnAction = "walk";
			ownUnit_TargetPoint = new Vector2(newTargetPoint.x, newTargetPoint.z);
		}
	}
	
	void OnDrawGizmos ()
	{
		Gizmos.DrawWireSphere(transform.position, ownUnit_AreaRadius ); //Einschätzen des Radius des SelectionRing-Projektors
	}
}
