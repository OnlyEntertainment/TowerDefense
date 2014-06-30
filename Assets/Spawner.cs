using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {


    public float delay = 3;
    public float timer;


    public int curWave;
    public List<WAVE> waves;

    struct WAVE {
        public GameObject mob;
        public int count;        
    }


	// Use this for initialization
	void Start () 
    {
        waves = new WAVE();


	}
	
	// Update is called once per frame
	void Update () {
	
	}




}
