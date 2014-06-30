using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[RequireComponent(typeof(Camera))]

public class UnitController : MonoBehaviour {
	
	/*
	Temporär(Debug):
		-Start()
			37		Rect des Menüs
		-OnGUI()
			98-108	Zeichnen des Menüs
	*/
	
	
	RaycastHit hit_MouseCurser; //Informationsspeicher der Raycasts
	
	Camera camera;

	//*******************************************
	//BUILD-IN***********************************
	//*******************************************
    public GameObject GeheZuGameObject;
    public Vector3 GeheZuPunkt;


    void OnGUI()
    {
        if (GUI.Button(new Rect(100, 200, 100, 100), "Gehe"))
        {
            if (GeheZuGameObject != null)
            { GeheZuPunkt = GeheZuGameObject.transform.position; }
            GetComponent<Unit>().Unit_GoToPoint(GeheZuPunkt);
        }
    }

    public float currentWaypoint = 0;
	void Update ()
	{
        NewBehaviourScript script = GetComponent<NewBehaviourScript>();
        
        if (script.WegListe != null)
        {
            if (currentWaypoint == script.WegListe.Count && GetComponent<Unit>().ownUnit_OnAction == "idle")
            {
                //Beende alles
                currentWaypoint = 0;
                script.WegListe = null;
            }
            else 
            {
                                
                if (GetComponent<Unit>().ownUnit_OnAction == "idle")
                {
                    //PathNode node = (PathNode)script.WegListe[(int)currentWaypoint];
                    //GetComponent<Unit>().Unit_GoToPoint(node.waypoint.transform.position);
                    Waypoint node = (Waypoint)script.WegListe[(int)currentWaypoint];
                    GetComponent<Unit>().Unit_GoToPoint(node.transform.position);
                    currentWaypoint++;
                }
            }

        }

				
	//GetComponent<Unit>().Unit_GoToPoint(hit_MouseCurser.point);
		
	}//ENDE Update()
	
	
	//GUI-Koordinaten und Screen-Koordinaten austauschen
	Vector3 ChangeCoordinates (Vector3 oldCoordinates)
	{
		return new Vector3(oldCoordinates.x, Screen.height -  oldCoordinates.y, oldCoordinates.z);
	}
	
	//Absolutes Rect (0 oben links) 
	Rect AbsRect (Vector2 GUIPoint1, Vector2 GUIPoint2)
	{
		if (GUIPoint1.x > GUIPoint2.x)
		{
			Switch2Floats(ref GUIPoint1.x, ref GUIPoint2.x);
		}
		if (GUIPoint1.y > GUIPoint2.y)
		{
			Switch2Floats(ref GUIPoint1.y, ref GUIPoint2.y);
		}
		return new Rect(GUIPoint1.x, GUIPoint1.y, GUIPoint2.x - GUIPoint1.x, GUIPoint2.y - GUIPoint1.y);
	}
	
	void Switch2Floats (ref float float1, ref float float2)
	{
		float temp_ChangeFloat = float1;
		float1 = float2;
		float2 = temp_ChangeFloat;
	}
}
