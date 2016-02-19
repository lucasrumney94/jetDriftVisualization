using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MarkerMover : MonoBehaviour
{
    public RectTransform canvas;

    public RectTransform verticalMarker;
    public RectTransform horizontalMarker;

    public Text MarkerPositionDisplay;

    void FixedUpdate()
    {
        verticalMarker.position += Vector3.right;
        horizontalMarker.position += Vector3.up;
        if (verticalMarker.position.x > canvas.rect.width)
        {
            verticalMarker.position = Vector3.zero;
        }
        if (horizontalMarker.position.y > canvas.rect.height)
        {
            horizontalMarker.position = Vector3.zero;
        }
        MarkerPositionDisplay.text = verticalMarker.position.x + " X, " + horizontalMarker.position.y + "Y";
    }
}
