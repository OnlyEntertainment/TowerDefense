using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

    public Waypoint[] weg;

    public Waypoint startWegpunkt;
    public Waypoint endWegpunkt;
    public ArrayList WegListe;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(100, 100, 100, 100), "Klick Mich"))
        {

            WegListe = PathFinder.GetPath(startWegpunkt, endWegpunkt);
            Debug.Log("Anzahl " + WegListe.Count.ToString());
            weg = new Waypoint[WegListe.Count];
            for (int i = 0; i < WegListe.Count; i++)
            {
                Debug.Log(i);
                weg[i] = (Waypoint)WegListe[i];

            }
        }

        if (GUI.Button(new Rect(200, 100, 100, 100), "Refresh Waypoints"))
        {
            GameProperties.waypointGenerator.RefreshWaypoints();
        }
    }
}
