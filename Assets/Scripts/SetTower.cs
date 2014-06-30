using UnityEngine;
using System.Collections;

public class SetTower : MonoBehaviour
{
    enum XZPOS { X, Z };

    public GameObject Tower;

    public GameObject currentTower = null;


    public int debugInt = 0;
    public float debugFloat = 0;

    public float buildingRange = 10.0f;
    // Use this for initialization

    private Material towerInitMaterial;

    void Start()
    {
        Screen.lockCursor = true;
        //Screen.showCursor = true;

    }

    public bool isBuilding = false;

    void OnGUI()
    {
        Debug.DrawRay(gameObject.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * 10, Color.red);
        //Debug.DrawRay(Camera.main.transform.position, Vector3.forward * 10, Color.blue);

        float boxSize = 10.0f;
        GUI.Box(new Rect(Screen.width / 2 - boxSize/2, Screen.height / 2 - boxSize/2, boxSize,boxSize), "");

    }

    // Update is called once per frame
    void Update()
    {


        Input.mousePosition.Set(Screen.width / 2, Screen.height / 2, 0);
        if (!isBuilding)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isBuilding = true;
                currentTower = (GameObject)Instantiate(Tower);
                towerInitMaterial = currentTower.renderer.material;
                currentTower.collider.enabled = false;
                currentTower.renderer.material.color = Color.green;

            }
        }
        else if (isBuilding)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isBuilding = false;
                Destroy(currentTower);
                currentTower = null;
            }
            if (currentTower != null)
            {
                RaycastHit hit_MouseCourser;
                if (Physics.Raycast(gameObject.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit_MouseCourser, Mathf.Infinity, 512)) //Layer 9 Terrain
                //if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit_MouseCourser, Mathf.Infinity/*9: Weg collider*/))
                {

                    Debug.DrawRay(currentTower.transform.position, Vector3.up * currentTower.transform.localScale.y);
                    RaycastHit hit_Structure;
                    if (Physics.Raycast(currentTower.transform.position, Vector3.up, out hit_Structure, currentTower.transform.localScale.y))
                    {
                        currentTower.renderer.material.color = Color.red;
                    }
                    else
                    {
                        currentTower.renderer.material.color = Color.green;

                    
                    Debug.Log("RaycastHit Point = " + hit_MouseCourser.point);
                    
                    Vector3 towerPosition = new Vector3(CalculatePosition(hit_MouseCourser.point.x, XZPOS.X),
                                                        currentTower.transform.localScale.y / 2,
                                                        CalculatePosition(hit_MouseCourser.point.z, XZPOS.Z));
                    
                    Debug.Log("Raycast Hit Calculated = " + towerPosition);

                    currentTower.transform.position = towerPosition;

                    if (Vector3.Distance(gameObject.transform.position, currentTower.transform.position) > buildingRange)
                    {
                        currentTower.GetComponent<MeshRenderer>().enabled = false;
                    }
                    else
                    {
                        currentTower.GetComponent<MeshRenderer>().enabled = true;
                        if (Input.GetMouseButtonDown(0))
                        {
                            currentTower.collider.enabled = true;
                            currentTower.renderer.material = towerInitMaterial;
                            currentTower = null;
                            currentTower = (GameObject)Instantiate(Tower);


                        }

                    }

                    }
                }
                else
                {
                    Debug.Log("MausCursor Position = " + Input.mousePosition);
                }
            }

        }

    }

    private float CalculatePosition(float currentPosition, XZPOS xzPos)
    {
        WaypointGenerator waypointGenerator = GameProperties.waypointGenerator.GetComponent<WaypointGenerator>();

        float feldGroesse = waypointGenerator.groesseFelder;

        currentPosition = Mathf.Clamp(currentPosition, 0, currentPosition);

        float ganzzahl = (float)(int)(currentPosition / feldGroesse); ;
        float rest = currentPosition % feldGroesse;
        float add = 0;
        //if (rest >= (feldGroesse / 2)) add++;


        float ergebnis = (ganzzahl + add) * feldGroesse + feldGroesse/2;

        if (xzPos == XZPOS.X) ergebnis = Mathf.Clamp(ergebnis, (1.5f * feldGroesse), waypointGenerator.groesseSpielflaecheX * feldGroesse+feldGroesse/2);
        else ergebnis = Mathf.Clamp(ergebnis, (2.5f * feldGroesse), waypointGenerator.groesseSpielflaecheZ * feldGroesse+feldGroesse*1.5f);



        debugFloat = ergebnis;

        return ergebnis;
        //WaypointGenerator wpGenerator = GameProperties.
        //float groesseFeld = 
    }
}
