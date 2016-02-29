using UnityEngine;
using System.Collections;

public static class TLEtoVec3 {

    public static float _SGP = 3.986004418f * Mathf.Pow(10, 14);

    public static Vector3 Convert(float inclination, float rightAscensionOfAscendingNode, float eccentricity, float arguementOfPeriapsis, float meanAnomaly, float meanMotion, int accuracy, float inverseScale = 1f)
    {
        Vector2 elipticalCoordinates = ElipticalCoordinates(eccentricity, meanAnomaly, meanMotion, accuracy);

        Vector3 position = new Vector3(elipticalCoordinates.x, elipticalCoordinates.y, 0f);

        //position = ApplyInclination(position, inclination);
        //position = ApplyRightAscensionOfAscendingNode(position, rightAscensionOfAscendingNode);
        //position = ApplyArguementOfPeriapsis(position, arguementOfPeriapsis);

        position = ApplyRotation(position, inclination, rightAscensionOfAscendingNode, arguementOfPeriapsis);

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

    private static Vector3 ApplyRotation(Vector3 position, float inclination, float rightAscensionOfAscendingNode, float arguementOfPeriapsis)
    {
        Vector3 rotation = new Vector3(inclination, rightAscensionOfAscendingNode, arguementOfPeriapsis);
        return Rotate(position, rotation);
    }

    private static Vector3 ApplyInclination(Vector3 position, float inclination)
    {
        float y = Mathf.Cos(inclination * Mathf.Deg2Rad) * position.y;
        float z = Mathf.Sin(inclination * Mathf.Deg2Rad) * position.y;

        Vector3 rotation = new Vector3(inclination, 0f, 0f);
        Vector3 newPos = Rotate(position, rotation);

        return newPos;
    }

    private static Vector3 ApplyRightAscensionOfAscendingNode(Vector3 position, float rightAscensionOfAscendingNode)
    {
        Vector3 rotation = new Vector3(0f, 0f, rightAscensionOfAscendingNode);
        return Rotate(position, rotation);
    }

    private static Vector3 ApplyArguementOfPeriapsis(Vector3 position, float arguementOfPeriapsis)
    {
        Vector3 rotation = new Vector3(0f, arguementOfPeriapsis, 0f);
        return Rotate(position, rotation);
    }

    private static Vector3 Rotate(Vector3 position, Vector3 rotation)
    {
        float radX = rotation.x * Mathf.Deg2Rad;
        float radY = rotation.y * Mathf.Deg2Rad;
        float radZ = rotation.z * Mathf.Deg2Rad;

        float sinX = Mathf.Sin(radX);
        float cosX = Mathf.Cos(radX);
        float sinY = Mathf.Sin(radY);
        float cosY = Mathf.Cos(radY);
        float sinZ = Mathf.Sin(radZ);
        float cosZ = Mathf.Cos(radZ);

        Vector3 xAxis = new Vector3(
            cosY * cosZ,
            cosX * sinZ + sinX * sinY * cosZ,
            sinX * sinZ - cosX * sinY * cosZ);
        Vector3 yAxis = new Vector3(
            -cosY * sinZ,
            cosX * cosZ - sinX * sinY * sinZ,
            sinX * cosZ + cosX * sinY * sinZ);
        Vector3 zAxis = new Vector3(
            sinY,
            -sinX * cosY,
            cosX * cosY);

        return position.x * xAxis + position.y * yAxis + position.z * zAxis;
    }
}
