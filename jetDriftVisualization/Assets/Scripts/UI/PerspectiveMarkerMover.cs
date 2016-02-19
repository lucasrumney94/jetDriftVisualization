using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PerspectiveMarkerMover : MonoBehaviour
{
    public float markerSpeed = 1f;

    public float cameraDistance = 50f;

    public Transform verticalMarker;
    public Transform horizontalMarker;

    public Text markerText;
    public Text timeText;

    private float cameraScreenedgeAngle;

    void Start()
    {
        cameraScreenedgeAngle = Camera.main.fieldOfView / 2f;
        verticalMarker.position = new Vector3(-Mathf.Tan(cameraScreenedgeAngle * Mathf.Deg2Rad * 2f) * cameraDistance, 0f, cameraDistance);
        horizontalMarker.position = new Vector3(0f, -Mathf.Tan(cameraScreenedgeAngle * Mathf.Deg2Rad) * cameraDistance, cameraDistance);
    }

    void Update()
    {
        verticalMarker.position += Vector3.right * markerSpeed * Time.deltaTime;
        horizontalMarker.position += Vector3.up * markerSpeed * Time.deltaTime;
        if(verticalMarker.position.x > Mathf.Tan(cameraScreenedgeAngle * Mathf.Deg2Rad * 2f) * cameraDistance)
        {
            verticalMarker.position = new Vector3(-Mathf.Tan(cameraScreenedgeAngle * Mathf.Deg2Rad * 2f) * cameraDistance, 0f, cameraDistance);
        }
        if (horizontalMarker.position.y > Mathf.Tan(cameraScreenedgeAngle * Mathf.Deg2Rad) * cameraDistance)
        {
            horizontalMarker.position = new Vector3(0f, -Mathf.Tan(cameraScreenedgeAngle * Mathf.Deg2Rad) * cameraDistance, cameraDistance);
        }

        markerText.text =  verticalMarker.position.x.ToString("F1") + " X, " + horizontalMarker.position.y.ToString("F1") + " Y";
        timeText.text = "Time Elapsed: " + Time.time.ToString("F1");
    }
}
