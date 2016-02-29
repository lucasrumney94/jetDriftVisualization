using UnityEngine;
using System.Collections;

public class OrbitalPosTester : MonoBehaviour {

    public float inversePositionScale = 1f;

    public float inclination;
    public float rightAscensionOfAscendingNode;
    [Range(0f, 1f)]
    public float eccentricity;
    public float arguementOfPeriapsis;
    [Range(0f, Mathf.PI * 2f)]
    public float meanAnomaly;
    public float meanMotion;

    public int accuracy = 25;
    public int conicSegments = 10;

    private Vector3[] orbitPositions;

    public GameObject planet;

    void Update()
    {
        Vector3 orbitalPositionVector = TLEtoVec3.Convert(inclination, rightAscensionOfAscendingNode, eccentricity, arguementOfPeriapsis, meanAnomaly, meanMotion, accuracy, inversePositionScale);
        planet.transform.position = orbitalPositionVector;
        //Debug.Log(orbitalPositionVector.x + ", " + orbitalPositionVector.y);
        //Debug.Log(orbitalPositionVector.x - (eccentricity * Mathf.Sin(orbitalPositionVector.x)));
    }

    private void FillPositions()
    {
        orbitPositions = new Vector3[conicSegments];
        for(int i = 0; i < conicSegments; i++)
        {
            float anomaly = (i * 2f * Mathf.PI) / (conicSegments);
            orbitPositions[i] = TLEtoVec3.Convert(inclination, rightAscensionOfAscendingNode, eccentricity, arguementOfPeriapsis, anomaly, meanMotion, accuracy, inversePositionScale);
        }
    }

    void OnDrawGizmos()
    {
        FillPositions();
        Gizmos.color = Color.red;
        for (int i = 0; i < conicSegments - 1; i++)
        {
            Gizmos.DrawLine(orbitPositions[i], orbitPositions[i + 1]);
        }
        Gizmos.DrawLine(orbitPositions[conicSegments - 1], orbitPositions[0]);
    }
}
