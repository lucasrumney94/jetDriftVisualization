using UnityEngine;
using System.Collections;

public static class TLEtoVec3 {

    public static Vector3 Convert(float inclination, float rightAscensionOfAscendingNode, float eccentricity, float arguementOfPeriapsis, float meanAnomaly, float meanMotion, int accuracy)
    {
        Vector3 position = Vector3.zero;



        return position;
    }

    private static float EccentricAnomaly(float eccentricity, float meanAnomaly, int accuracy)
    {
        float eccentricAnomaly = meanAnomaly;
        for(int i = 0; i < accuracy; i++)
        {
            eccentricAnomaly = meanAnomaly - (eccentricity * Mathf.Sin(eccentricAnomaly));
        }

        return eccentricAnomaly;
    }
}
