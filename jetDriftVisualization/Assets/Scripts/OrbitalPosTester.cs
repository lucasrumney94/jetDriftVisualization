using UnityEngine;
using System.Collections;

public class OrbitalPosTester : MonoBehaviour {

    public float inversePositionScale;

    public float inclination;
    public float rightAscensionOfAscendingNode;
    [Range(0f, 1f)]
    public float eccentricity;
    public float arguementOfPeriapsis;
    [Range(0f, Mathf.PI * 2f)]
    public float meanAnomaly;
    public float meanMotion;

    public int accuracy;

    public GameObject planet;

    void Update()
    {
        Vector3 orbitalPositionVector = TLEtoVec3.Convert(inclination, rightAscensionOfAscendingNode, eccentricity, arguementOfPeriapsis, meanAnomaly, meanMotion, accuracy);
        orbitalPositionVector *= 1f / inversePositionScale;
        planet.transform.position = orbitalPositionVector;
        Debug.Log(orbitalPositionVector.x + ", " + orbitalPositionVector.y);
        //Debug.Log(orbitalPositionVector.x - (eccentricity * Mathf.Sin(orbitalPositionVector.x)));
    }
}
