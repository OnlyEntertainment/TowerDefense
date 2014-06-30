using UnityEngine;
using System.Collections;

public class WaypointGenerator : MonoBehaviour
{


    public GameObject waypointGoal;
    public GameObject waypointSpawn;
    public float maxDistanceBetweenWaypoints = 3.0f;


    public bool GenerateLevel = false;


    public GameObject prefabWaypoint;    
    public GameObject prefabLevelWall;
    public GameObject prefabGoal;
    public GameObject prefabSpawn;


    public float groesseSpielflaecheX = 20.0f; //Felder
    public float groesseSpielflaecheZ = 40.0f; //Felder

    public float yPosition = 0.5f;
    public float groesseFelder = 2.0f;
    public float heightFelder = 0.2f;
    public float wallHeight = 5.0f;
    public float wallThickness = 2.0f;

    

    
    void Awake()
    {
        if (GenerateLevel) GenerateWaypoints();

        Waypoint.waypoints = new ArrayList();
        CalculateWaypoints(transform);
        CalculateNeighbours();
    }

    // Use this for initialization
    void Start()
    {
        

        
    }

    

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateWaypoints()
    {
        //float xAbstandZumRand = prefabWaypoint.transform.localScale.x / 2;
        //float zAbstandZumRand = prefabWaypoint.transform.localScale.z / 2 + prefabWaypoint.transform.localScale.z;
        
        float wallWidthX = ((groesseSpielflaecheX + 2) * groesseFelder);
        float wallWidthXHalf = (wallWidthX - groesseFelder*2) / 2;    

        GameObject wallObject;
        //Linke Abgrenzung
        wallObject = (GameObject)Instantiate(prefabLevelWall, new Vector3(groesseFelder / 2, wallHeight / 2, ((groesseSpielflaecheZ + 4) * groesseFelder) / 2), prefabLevelWall.transform.rotation);
        wallObject.transform.localScale = new Vector3(groesseFelder,wallHeight,(groesseSpielflaecheZ + 4) * groesseFelder);

        //Rechte Abgrenzung
        wallObject = (GameObject)Instantiate(prefabLevelWall, new Vector3((groesseSpielflaecheX + 1.5f) * groesseFelder, wallHeight / 2, ((groesseSpielflaecheZ + 4) * groesseFelder) / 2), prefabLevelWall.transform.rotation);
        wallObject.transform.localScale = new Vector3(groesseFelder, wallHeight, (groesseSpielflaecheZ + 4) * groesseFelder);

        //Untere Abgrenzung

        wallObject = (GameObject)Instantiate(prefabLevelWall, new Vector3(wallWidthX / 2, wallHeight / 2, groesseFelder / 2), prefabLevelWall.transform.rotation);
        wallObject.transform.localScale = new Vector3(wallWidthX, wallHeight, groesseFelder);

        //UntenLinks Abgrenzung
        wallObject = (GameObject)Instantiate(prefabLevelWall, new Vector3(wallWidthXHalf/2, wallHeight / 2, groesseFelder / 2 + groesseFelder), prefabLevelWall.transform.rotation);
        wallObject.transform.localScale = new Vector3(wallWidthXHalf, wallHeight, groesseFelder);
        
        //UntenRechts Abgrenzung
        wallObject = (GameObject)Instantiate(prefabLevelWall, new Vector3(wallWidthXHalf/2+wallWidthXHalf+groesseFelder*2, wallHeight / 2, groesseFelder / 2+groesseFelder), prefabLevelWall.transform.rotation);
        wallObject.transform.localScale = new Vector3(wallWidthXHalf, wallHeight, groesseFelder);

        //Obere Abgrenzung
        wallObject = (GameObject)Instantiate(prefabLevelWall, new Vector3(wallWidthX / 2, wallHeight / 2, (groesseSpielflaecheZ + 3) * groesseFelder + groesseFelder / 2), prefabLevelWall.transform.rotation);
        wallObject.transform.localScale = new Vector3(wallWidthX, wallHeight, groesseFelder);

        //ObenLinks Abgrenzung
        wallObject = (GameObject)Instantiate(prefabLevelWall, new Vector3(wallWidthXHalf / 2, wallHeight / 2, (groesseSpielflaecheZ + 2) * groesseFelder + groesseFelder / 2), prefabLevelWall.transform.rotation);
        wallObject.transform.localScale = new Vector3(wallWidthXHalf, wallHeight, groesseFelder);

        //ObenRechts Abgrenzung
        wallObject = (GameObject)Instantiate(prefabLevelWall, new Vector3(wallWidthXHalf / 2 + wallWidthXHalf + groesseFelder * 2, wallHeight / 2, (groesseSpielflaecheZ + 2) * groesseFelder + groesseFelder / 2), prefabLevelWall.transform.rotation);
        wallObject.transform.localScale = new Vector3(wallWidthXHalf, wallHeight, groesseFelder);


        //Spawn
        GameObject waypoint;
        waypoint = (GameObject)Instantiate(prefabSpawn,new Vector3((groesseSpielflaecheX+groesseFelder)*groesseFelder/2,yPosition,1.5f*groesseFelder), prefabWaypoint.transform.rotation);
        waypoint.transform.localScale = new Vector3(2*groesseFelder,heightFelder,groesseFelder);
        waypoint.name = "Spawn";
        waypoint.transform.parent = gameObject.transform;
        waypointSpawn = waypoint;

        //Goal
        waypoint = (GameObject)Instantiate(prefabGoal, new Vector3((groesseSpielflaecheX + groesseFelder) * groesseFelder / 2, yPosition, (groesseSpielflaecheZ + 2) * groesseFelder + groesseFelder / 2), prefabWaypoint.transform.rotation);
        waypoint.transform.localScale = new Vector3(2 * groesseFelder, heightFelder, groesseFelder);
        waypoint.name = "Goal";
        waypoint.transform.parent = gameObject.transform;
        waypointGoal = waypoint;

        for (float x = 0; x < groesseSpielflaecheX; x++)
        {
            for (float z = 0; z < groesseSpielflaecheZ; z++)
            {
                Vector3 newPosition = new Vector3(x * groesseFelder + 1.5f * groesseFelder, yPosition, z * groesseFelder + 2.5f * groesseFelder);

                waypoint = (GameObject)Instantiate(prefabWaypoint,newPosition , prefabWaypoint.transform.rotation);
                waypoint.transform.localScale = new Vector3(groesseFelder, heightFelder, groesseFelder);
                waypoint.name = "Waypoint (" + x + "/" + z + ")";
                waypoint.transform.parent = gameObject.transform;

            }
        }
    }

    public void CalculateWaypoints(Transform parentTransform)
    {
        foreach (Transform child in parentTransform)
        {
            if (child.GetComponent<Waypoint>())
            {
                Waypoint waypoint = child.GetComponent<Waypoint>();
                Waypoint.waypoints.Add(waypoint);
            }

            if (child.childCount > 0) { CalculateWaypoints(child); }

        }
    }

    public void CalculateNeighbours()
    {
        foreach (Waypoint waypoint in Waypoint.waypoints)
        {
            foreach (Waypoint neighbour in Waypoint.waypoints)
            {
                if (waypoint != neighbour)
                {
                    Vector3 posWaypoint = waypoint.transform.position;
                    Vector3 posNeighbour = neighbour.transform.position;
                    float entfernung = Vector3.Distance(posWaypoint, posNeighbour);

                    if (entfernung <= maxDistanceBetweenWaypoints)
                    {
                        RaycastHit[] hits;
                        hits = Physics.RaycastAll(posWaypoint, Vector3.Normalize(posNeighbour - posWaypoint), entfernung);
                        bool isWayFree = true;

                        foreach (RaycastHit hit in hits)
                        {
                            if (!hit.collider.isTrigger && hit.collider.tag != "Player")
                            { 
                                isWayFree = false;
                                break;
                            }
                        }

                        if (isWayFree) //wenn der weg frei ist, ist es ein begehbarer Nachbar
                        {
                            waypoint.addNeighbour(neighbour);
                        }



                    }


                }
            }
        }
    }
}
