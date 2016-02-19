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

    private float cameraScreenedgeAngle;

    void Start()
    {
        cameraScreenedgeAngle = Camera.main.fieldOfView / 2f;
        verticalMarker.position = new Vector3(-Mathf.Tan(cameraScreenedgeAngle * Mathf.Deg2Rad) * cameraDistance, 0f, cameraDistance);
        horizontalMarker.position = new Vector3(0f, -Mathf.Tan(cameraScreenedgeAngle * Mathf.Deg2Rad) * cameraDistance, cameraDistance);
    }

    void Update()
    {
        verticalMarker.position += Vector3.right * markerSpeed * Time.deltaTime;
        horizontalMarker.position += Vector3.up * markerSpeed * Time.deltaTime;

        markerText.text = verticalMarker.position.x + " X, " + horizontalMarker.position.y + " Y";
    }
}
