using UnityEngine;
using System.Collections;


class PathFinder : MonoBehaviour
{

    public static ArrayList GetPath(Waypoint startWaypoint, Waypoint targetWaypoint)
    {
        ArrayList openList = new ArrayList();
        ArrayList closedList = new ArrayList();
        PathNode currentPathNode;
        
        PathNode startField = (PathNode)ScriptableObject.CreateInstance<PathNode>();
        startField.waypoint = startWaypoint;

        PathNode targetField = null;
        ArrayList pathArray = new ArrayList();
        
        currentPathNode = startField;
        openList.Add(startField);


        if (startWaypoint != targetWaypoint)
        {
            while (targetField == null && openList.Count > 0)
            {
                int index = ReturnPathNodeWithLowesFValue(openList);
                currentPathNode = (PathNode)openList[index];
                openList.RemoveAt(index);
                closedList.Add(currentPathNode);

                ArrayList neighbourWaypoints = currentPathNode.waypoint.waypointsInRange;

                for (int i = 0; i < neighbourWaypoints.Count; i++)
                { 
                    if (!IsInClosedList(closedList,(Waypoint)neighbourWaypoints[i]))
                    {
                        int indexInOpenList = IsInOpenList(openList, (Waypoint)neighbourWaypoints[i]);
                        if (indexInOpenList >= 0)
                        { 
                            //eventuell neu berechnen
                            float newGValue = currentPathNode.gValue + Vector3.Distance(currentPathNode.waypoint.transform.position, ((PathNode)openList[indexInOpenList]).waypoint.transform.position);
                            if (newGValue < ((PathNode)openList[indexInOpenList]).gValue)
                            {

                                ((PathNode)openList[indexInOpenList]).parentPathNode = currentPathNode;
                                ((PathNode)openList[indexInOpenList]).gValue = newGValue;
                                ((PathNode)openList[indexInOpenList]).fValue = (newGValue + ((PathNode)openList[indexInOpenList]).hValue);
                            }

                        }
                        else
                        {
                            //Neue PathNode erzeugen
                            float GValue = currentPathNode.gValue + Vector3.Distance(currentPathNode.waypoint.transform.position, ((Waypoint)neighbourWaypoints[i]).transform.position);
                            float HValue = Vector3.Distance(((Waypoint)neighbourWaypoints[i]).transform.position, targetWaypoint.transform.position);
                            float FValue = GValue + HValue;

                            PathNode newPathNode = (PathNode)ScriptableObject.CreateInstance<PathNode>();
                            newPathNode.waypoint = (Waypoint)neighbourWaypoints[i];
                            newPathNode.gValue = GValue;
                            newPathNode.hValue = HValue;
                            newPathNode.fValue = FValue;
                            newPathNode.parentPathNode = currentPathNode;
                            openList.Add(newPathNode);

                            if (newPathNode.waypoint == targetWaypoint)
                            {

                                targetField = newPathNode;
								pathArray = ReturnPath(startField,targetField);
                                //Pfad berechnen
                                break;
                            }

                            
                        }
                    }
                }
            }
            
        }


        return pathArray;
}


    static public int ReturnPathNodeWithLowesFValue(ArrayList openList)
    {
        PathNode pathNode = null;
        int index = -1;
        if (openList.Count >0)
        {
            for (int i = 0;i < openList.Count;i++)
            {
                if (index == -1 ||
                    pathNode.fValue > ((PathNode) openList[i]).fValue)
                {
                    pathNode =(PathNode)openList[i];
                    index = i;
                    //TODO fdlskfj
                }
            }
        }
        return index;



    }

    public static bool IsInClosedList(ArrayList closedList, Waypoint waypoint)
    {
        bool isInClosedList = false;

        for (int i = 0; i < closedList.Count;i++ )
        {
            if (((PathNode)closedList[i]).waypoint == waypoint)
            {

                isInClosedList = true;
                break;
            }

        }
        //foreach (Waypoint wp in closedList)
        //{
        //    if (wp == waypoint) 
        //    {
        //        isInClosedList = true;
        //        break;
        //    }
        //}
        return isInClosedList;

    }

    public static int IsInOpenList(ArrayList openList, Waypoint waypoint)
    {
        int index = -1;

        for (int i = 0; i < openList.Count; i++)
        {
            if (((PathNode)openList[i]).waypoint == waypoint)
            {

                index = i;
                break;
            }

        }

        //for (int i = 0;i < openList.Count;i++)
        //{
        //    if (openList[i] == waypoint)
        //    {
        //        index = i;                
        //        break;
        //    }
        //}
        return index;

    }

    static public ArrayList ReturnPath(PathNode startField, PathNode targetField)
{
                ArrayList pathArray = new ArrayList();
                PathNode currentPathNode = targetField;

                while(currentPathNode != startField)
                {

                                pathArray.Add(currentPathNode.waypoint);
                                currentPathNode = currentPathNode.parentPathNode;
                }

                pathArray.Reverse();

                return pathArray;
}
}
