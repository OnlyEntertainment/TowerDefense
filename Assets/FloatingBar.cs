using UnityEngine;
using System.Collections;

public class FloatingBar : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }




    public GameObject target;
    public Vector3 offset = Vector3.up;        // Units in world space to offset; 1 unit above object by default

    Camera mainCamera;
    private bool clampToScreen = false;      // If true, label will be visible even if object is off screen
    private float clampBorderSize = 0.05f;      // How much viewport space to leave at the borders when a label is being clamped

    // Update is called once per frame
    void Update()
    {
        mainCamera = Camera.main;
        if (clampToScreen)
        {
            var relativePosition = mainCamera.transform.InverseTransformPoint(target.transform.position);
            relativePosition.z = Mathf.Max(relativePosition.z, 1.0f);

            transform.position = mainCamera.WorldToViewportPoint(mainCamera.transform.TransformPoint(relativePosition + offset));

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, clampBorderSize, 1.0f - clampBorderSize), Mathf.Clamp(transform.position.y, clampBorderSize, 1.0f - clampBorderSize), transform.position.z);

        }

        else
        {

            transform.position = mainCamera.WorldToViewportPoint(target.transform.position + offset);

        }

    }
}
