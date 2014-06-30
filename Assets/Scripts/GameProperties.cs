using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    public class GameProperties:MonoBehaviour
    {
        public static GameObject waypointHolder;
        public static WaypointGenerator waypointGenerator;

        public int maxTriesToFindObjects = 5;
        private int triedToFindObjects = 0;
        public int i;


        void Start()
        {
            waypointHolder= GameObject.Find("Waypoints-Holder");
            waypointGenerator = waypointHolder.GetComponent<WaypointGenerator>();
        }

        void Update()
        {


            //if (triedToFindObjects < maxTriesToFindObjects)
            //{
            //    foreach(GameObject in Application.
            //    if (waypointGenerator == null) ;
            //    GameObject.FindGameObjectsWithTag
            //}
        }
    }
