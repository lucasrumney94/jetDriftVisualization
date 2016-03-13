﻿using UnityEngine;
using System.Collections;
using System;

public class PlayerCamera : MonoBehaviour
{
    public float defaultCameraDistance;
    public float targetCameraDistance;
    private float cameraDistance; //Radius of camera movement sphere

    public float currentLongitude = 0;
    private float targetLongitude;

    public float currentLatitude = 0;
    private float targetLatitude;

    public float verticalAngleLimit = 80f;

    public float cameraSpeed = 1f; //How fast will the camera move into position?

    public Vector3 anchorDistance; //Distance of the camera center of rotation from the player

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraDistance = defaultCameraDistance;
        targetCameraDistance = defaultCameraDistance;
    }

    void Update()
    {
        LerpCameraToPosition();
    }

    private void LerpCameraToPosition()
    {
        //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraSpeed);
        cameraDistance = Mathf.Lerp(cameraDistance, targetCameraDistance, Time.deltaTime * cameraSpeed);
    }

	public void MoveCamera(Vector3 cameraDelta)
    {
        OrbitCamera(Mathf.Deg2Rad * cameraDelta.x, Mathf.Deg2Rad * cameraDelta.y);
        targetCameraDistance += cameraDelta.z * Mathf.Log10(targetCameraDistance);
        targetCameraDistance = Mathf.Clamp(targetCameraDistance, 100f, 1000f);
    }

    /// <summary>
    /// Orbits the camera around the player at an offset
    /// </summary>
    /// <param name="angleX">Longitudal delta</param>
    /// <param name="angleY">Latitudal delta</param>
    private void OrbitCamera(float angleX, float angleY)
    {
        targetLatitude = Mathf.Clamp(currentLatitude + angleY, Mathf.Deg2Rad * -verticalAngleLimit, Mathf.Deg2Rad * verticalAngleLimit);
        targetLongitude = currentLongitude + angleX;

        Vector3 orbitCenter;
        orbitCenter.x = Mathf.Cos(targetLongitude) * anchorDistance.x;
        orbitCenter.z = -Mathf.Sin(targetLongitude) * anchorDistance.x;
        orbitCenter.y = anchorDistance.y;
        orbitCenter += player.transform.position;
        Debug.DrawLine(orbitCenter, player.transform.position, Color.blue);

        float newY = Mathf.Sin(targetLatitude) * cameraDistance;

        //Adjust x,z based on y position
        float newX = -Mathf.Sin(targetLongitude) * Mathf.Sqrt((cameraDistance * cameraDistance) - (newY * newY));
        float newZ = -Mathf.Cos(targetLongitude) * Mathf.Sqrt((cameraDistance * cameraDistance) - (newY * newY));

        Vector3 newPosition = new Vector3(newX, newY, newZ) + orbitCenter;
        transform.position = newPosition;

        currentLatitude = targetLatitude;
        currentLongitude = targetLongitude;
        LookAtPlayer(orbitCenter);
    }

    /// <summary>
    /// Turn the camera to face the player
    /// </summary>
    private void LookAtPlayer(Vector3 center)
    {
        transform.LookAt(center);
        //if (followAnchor) transform.LookAt(anchor.transform);
        //else transform.LookAt(player.transform);
    }
}
