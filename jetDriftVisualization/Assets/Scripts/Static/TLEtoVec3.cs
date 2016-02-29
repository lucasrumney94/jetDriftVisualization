using UnityEngine;
using System.Collections;

public static class TLEtoVec3 {

    public static float _SGP = 3.986004418f * Mathf.Pow(10, 14);

    public static Vector3 Convert(float inclination, float rightAscensionOfAscendingNode, float eccentricity, float arguementOfPeriapsis, float meanAnomaly, float meanMotion, int accuracy)
    {
        Vector2 elipticalCoordinates = ElipticalCoordinates(eccentricity, meanAnomaly, meanMotion, accuracy);

        Vector3 position = new Vector3(elipticalCoordinates.x, elipticalCoordinates.y, 0f);

        return position;
    }

    private static float EccentricAnomaly(float eccentricity, float meanAnomaly, int accuracy)
    {
        float eccentricAnomaly = meanAnomaly;
        for(int i = 0; i < accuracy; i++)
        {
            eccentricAnomaly = meanAnomaly + (eccentricity * Mathf.Sin(eccentricAnomaly));
        }

        return eccentricAnomaly;
    }

    private static float SemiMajorAxis(float meanMotion)
    {
        float orbitalPeriod = 1f / meanMotion; //in days
        orbitalPeriod *= 60f * 60f * 24f; //in seconds

        float smaCubed = (_SGP * (orbitalPeriod * orbitalPeriod)) / (4f * (Mathf.PI * Mathf.PI));

        return Mathf.Pow(smaCubed, 1f / 3f);
    }

    private static float SemiMinorAxis(float eccentricity, float semiMajorAxis)
    {
        float semiMinorAxis = semiMajorAxis * Mathf.Pow(1f - (eccentricity * eccentricity), 1f / 2f);

        return semiMinorAxis;
    }

    private static Vector2 ElipticalCoordinates(float eccentricity, float meanAnomaly, float meanMotion, int accuracy)
    {
        float eccentricAnomaly = EccentricAnomaly(eccentricity, meanAnomaly, accuracy);
        float semiMajor = SemiMajorAxis(meanMotion);
        float semiMinor = SemiMinorAxis(eccentricity, semiMajor);

        float x = semiMajor * Mathf.Cos(eccentricAnomaly);
        float y = semiMinor * Mathf.Sin(eccentricAnomaly);

        Vector2 pos = new Vector2(x, y);

        return pos;
    }
}
