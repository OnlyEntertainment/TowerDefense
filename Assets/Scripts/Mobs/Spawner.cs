using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{

    public Waypoint startWaypoint;
    public Waypoint endWaypoint;

    public float delay = 3;
    public float timer;


    public List<GameObject> waveMob;
    public List<int> waveCount;


    public int curWave = 0;
    public int mobsSpawned = 0;
    public List<GameObject> MobsOfWave;
    public bool waveRunning = false;

    public float waveEveryXSeconds = 300;
    public float nextWaveinXSeconds;

    // Use this for initialization
    void Start()
    {
        startWaypoint = GameObject.FindGameObjectWithTag("Waypoint_Spawn").GetComponent<Waypoint>();
        endWaypoint = GameObject.FindGameObjectWithTag("Waypoint_Goal").GetComponent<Waypoint>();

        for (int i = waveCount.Count + 1; i <= waveMob.Count; i++)
        {
            waveCount.Add(5);
        }
        nextWaveinXSeconds = waveEveryXSeconds;

    }

    // Update is called once per frame
    void Update()
    {
        if (!waveRunning)
        {
            nextWaveinXSeconds -= Time.deltaTime;

            if (nextWaveinXSeconds <= 0) { SendWave(); }
        }
        else if (waveRunning)
        {
            if (mobsSpawned < waveCount[curWave])
            {
                timer -= Time.deltaTime;

                if (timer <= 0) SendMob();
            }
            else
            {
                if (MobsOfWave.Count == 0)
                {
                    waveRunning = false;
                    curWave++;
                }
            }

        }

    }

    public void SendWave()
    {

        if (curWave < waveMob.Count)
        {
            gameObject.GetComponent<WaypointGenerator>().RefreshWaypoints();

            mobsSpawned = 0;
            waveRunning = true;
            SendMob();
            nextWaveinXSeconds = waveEveryXSeconds;
        }
    }

    private void SendMob()
    {

        GameObject mob = (GameObject)Instantiate(waveMob[curWave], startWaypoint.gameObject.transform.position, waveMob[curWave].transform.rotation);
        mob.GetComponent<Mob_Movement>().StartMoving(startWaypoint, endWaypoint, this);
        MobsOfWave.Add(mob);
        timer = delay;
        mobsSpawned++;
    }



}
