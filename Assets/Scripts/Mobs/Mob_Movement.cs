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

    public float timeToReachNextWaypoint = 3.0f;
    public float timeRemainingToNextWaypoint;
    public int timesToReachSameWaypoint = 0;
    public int maxTriesToReachSameWaypoint = 3;

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
                if (timesToReachSameWaypoint >= maxTriesToReachSameWaypoint)
                {
                    timesToReachSameWaypoint = 0;
                    transform.position = gameObject.GetComponent<Mob>().spawner.startWaypoint.transform.position;
                    this.StartMoving(gameObject.GetComponent<Mob>().spawner.path, gameObject.GetComponent<Mob>().spawner);
                }
                else if (timeRemainingToNextWaypoint <= 0 && curWaypoint > 0)
                {

                    gameObject.GetComponent<Mob>().spawner.path = gameObject.GetComponent<Mob>().spawner.gameObject.GetComponent<WaypointGenerator>().RefreshWaypoints();// 
                    path = PathFinder.GetPath((Waypoint)path[curWaypoint - 1], (Waypoint)path[path.Count - 1]);
                    this.StartMoving(path, gameObject.GetComponent<Mob>().spawner);
                    timesToReachSameWaypoint++;
                }
                else
                {
                    timeRemainingToNextWaypoint -= Time.deltaTime;

                    if (action == ACTIONS.Idle)
                    {
                        Waypoint nextWaypoint = (Waypoint)path[curWaypoint];
                        if (nextWaypoint == null)
                        { timeRemainingToNextWaypoint = 0; }
                        else
                        {
                            moveTo = new Vector2(nextWaypoint.transform.position.x, nextWaypoint.transform.position.z);
                            action = ACTIONS.Move;
                            timeRemainingToNextWaypoint = timeToReachNextWaypoint;
                            curWaypoint++;
                        }

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
                        else
                        {
                            action = ACTIONS.Idle;
                            timesToReachSameWaypoint = 0;
                        }
                    }
                }
            }

        }
    }

    private void MoveToPoint(Vector3 moveTo)
    {
        //action = ACTIONS.Move;
        //this.moveTo = new Vector2(moveTo.x, moveTo.z);
    }

    public void StartMoving(ArrayList path, Spawner spawner)
    {
        this.startWaypoint = startWaypoint;
        this.endWaypoint = endWaypoint;
        gameObject.GetComponent<Mob>().spawner = spawner;

        this.path = path; //  PathFinder.GetPath(startWaypoint, endWaypoint);
        curWaypoint = 0;
        timeRemainingToNextWaypoint = timeToReachNextWaypoint;
        
        action = ACTIONS.Idle;
    }


}
