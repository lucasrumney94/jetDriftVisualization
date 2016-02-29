using UnityEngine;
using System.Collections;

public static class TLEtoVec3 {

    public static float _SGP = 3.986004418f * Mathf.Pow(10, 14);

    public static Vector3 Convert(float inclination, float rightAscensionOfAscendingNode, float eccentricity, float arguementOfPeriapsis, float meanAnomaly, float meanMotion, int accuracy, float inverseScale = 1f)
    {
        Vector2 elipticalCoordinates = ElipticalCoordinates(eccentricity, meanAnomaly, meanMotion, accuracy);

        Vector3 position = new Vector3(elipticalCoordinates.x, elipticalCoordinates.y, 0f);

        position = Incline(position, inclination);

        return position * (1f / inverseScale);
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

        Vector2 pos = new Vector2(x, y) - FocusPosition(semiMajor, semiMinor);

        return pos;
    }

    private static Vector2 FocusPosition(float semiMajor, float semiMinor)
    {
        float x = Mathf.Pow(Mathf.Pow(semiMajor, 2f) - Mathf.Pow(semiMinor, 2f), 1f / 2f);

        return new Vector2(x, 0f);
    }

    public static Vector3 Incline(Vector3 position, float inclination)
    {
        float y = Mathf.Cos(inclination * Mathf.Deg2Rad) * position.y;
        float z = Mathf.Sin(inclination * Mathf.Deg2Rad) * position.y;

        Vector3 newPos = new Vector3(position.x, y, z);

        return newPos;
    }
}
