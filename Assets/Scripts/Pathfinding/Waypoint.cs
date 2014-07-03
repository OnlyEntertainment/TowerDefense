using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour
{

    public static ArrayList waypoints = new ArrayList();
    public ArrayList waypointsInRange;


    void Awake()
    {
        transform.renderer.enabled = false;

    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 1, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(0.5f, 0.2f, 0.5f));

        if (waypointsInRange != null && waypointsInRange.Count > 0)
        {
            foreach (Waypoint neighbour in waypointsInRange)
            {
                Gizmos.color = new Color(1, 0, 0, 1);
                Gizmos.DrawLine(transform.position, neighbour.transform.position);
            }
        }

    }

    public void CheckToDelete()
    {
        Vector3 originVector = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1.0f, gameObject.transform.position.z);
        RaycastHit hit;
        RaycastHit[] hits = Physics.RaycastAll(originVector, Vector3.up, Mathf.Infinity);
        foreach (RaycastHit singleHit in hits)
        {
            if (singleHit.collider.tag == "Obstacle") { Debug.Log("Found Obstacle, Delete: " + gameObject.name); Destroy(gameObject); return; }
        }

        if (Physics.Raycast(originVector, Vector3.up, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Obstacle") { Debug.Log("Found Obstacle, Delete: " + gameObject.name); Destroy(gameObject); return; }

        }
    }


    // Use this for initialization
    void Start()
    {
        CheckToDelete();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 originVector = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1.0f, gameObject.transform.position.z);



        Debug.DrawRay(originVector, Vector3.up, Color.red);
    }


    public void addNeighbour(Waypoint neighbour)
    {
        if (waypointsInRange == null) { waypointsInRange = new ArrayList(); }

        waypointsInRange.Add(neighbour);

    }

}
