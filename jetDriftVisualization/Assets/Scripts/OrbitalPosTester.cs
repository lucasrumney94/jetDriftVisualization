using UnityEngine;
using System.Collections;

public class OrbitalPosTester : MonoBehaviour {

    public float inclination;
    public float rightAscensionOfAscendingNode;
    public float eccentricity;
    public float arguementOfPeriapsis;
    public float meanAnomaly;
    public float meanMotion;

    public int accuracy;

    void Start()
    {
        Vector3 orbitalPositionVector = TLEtoVec3.Convert(inclination, rightAscensionOfAscendingNode, eccentricity, arguementOfPeriapsis, meanAnomaly, meanMotion, accuracy);
        Debug.Log(orbitalPositionVector);
    }
}
