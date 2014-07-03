using UnityEngine;
using System.Collections;

public class Mob_Movement : MonoBehaviour
{

    public enum ACTIONS { Idle, Move };

    public ACTIONS action = ACTIONS.Idle;

    private Mob mobSettings;

    public Vector2 moveTo;
    public ArrayList path;
    public Waypoint startWaypoint;
    public Waypoint endWaypoint;
    public int curWaypoint;

    void Awake()
    {
        mobSettings = gameObject.GetComponent<Mob>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 curPosition = new Vector2(transform.position.x, transform.position.z);

        if (path != null)
        {
            if (curWaypoint + 1 == path.Count && action == ACTIONS.Idle)
            {
                curWaypoint = 0;
                path = null;
                gameObject.GetComponent<Mob>().MobFinishedMace();
            }
            else
            {
                if (action == ACTIONS.Idle)
                {
                    Waypoint nextWaypoint = (Waypoint)path[curWaypoint];
                    moveTo = new Vector2(nextWaypoint.transform.position.x, nextWaypoint.transform.position.z);
                    action = ACTIONS.Move;
                    curWaypoint++;
                }
                else if (action == ACTIONS.Move)
                {
                    if ((moveTo - curPosition).magnitude >= mobSettings.curSpeed * Time.deltaTime)
                    {
                        transform.LookAt(new Vector3(moveTo.x, transform.position.y, moveTo.y));
                        transform.Translate(Vector3.forward * mobSettings.curSpeed * Time.deltaTime);
                        transform.position = new Vector3(transform.position.x, Terrain.activeTerrain.SampleHeight(transform.position) + transform.localScale.y, transform.position.z);
                        //transform.position=(new Vector3(tran
                    }
                    else { action = ACTIONS.Idle; }
                }
            }

        }
    }

    private void MoveToPoint(Vector3 moveTo)
    {
        //action = ACTIONS.Move;
        //this.moveTo = new Vector2(moveTo.x, moveTo.z);
    }

    public void StartMoving(Waypoint startWaypoint, Waypoint endWaypoint, Spawner spawner)
    {
        this.startWaypoint = startWaypoint;
        this.endWaypoint = endWaypoint;
        gameObject.GetComponent<Mob>().spawner = spawner;

        path = PathFinder.GetPath(startWaypoint, endWaypoint);
        curWaypoint = 0;
        action = ACTIONS.Idle;
    }


}
