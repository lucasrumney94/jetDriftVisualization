using UnityEngine;
using System.Collections;
using System;

public static class TLEtoVec3 {

    public static float _SGP = 3.986004418f * Mathf.Pow(10, 14);

    public static float _SecondsInDay = 86164.09164f;

    public static Vector3 Convert(Satellite tleData, DateTime timeOfReading, int accuracy = 1, float inverseScale = 1f)
    {
        int epochYear = tleData.EpochYear;
        float epochDay = tleData.EpochDay;
        float inclination = tleData.Inclination;
        float rightAscensionOfAscendingNode = tleData.RightAscensionOfTheAscendingNode;
        float eccentricity = tleData.Eccentricity;
        float arguementOfPeriapsis = tleData.ArgumentOfPeriapsis;
        float meanAnomaly = tleData.MeanAnomaly;
        float meanMotion = tleData.MeanMotion;

        meanAnomaly = AdvanceMeanAnomaly(epochYear, epochDay, meanAnomaly, meanMotion, timeOfReading);

        Vector2 elipticalCoordinates = ElipticalCoordinates(eccentricity, meanAnomaly, meanMotion, accuracy);

        Vector3 position = new Vector3(elipticalCoordinates.x, elipticalCoordinates.y, 0f);

        position = ApplyRotation(position, inclination, rightAscensionOfAscendingNode, arguementOfPeriapsis);

        return position * (1f / inverseScale);
    }

    private static float AdvanceMeanAnomaly(int epochYear, float epochDay, float meanAnomaly, float meanMotion, DateTime timeOfReading)
    {
        float timeChange = TimeChange(epochYear, epochDay, timeOfReading);
        //Debug.Log(timeChange);
        return meanAnomaly + (timeChange * meanMotion);
    }

    //Returns days since the TLE data was taken, as a float
    private static float TimeChange(int epochYear, float epochDay, DateTime timeOfReading)
    {
        int currentEpochYear = timeOfReading.Year % 100;
        float currentEpochDay = (float)timeOfReading.DayOfYear + ((float)timeOfReading.TimeOfDay.TotalSeconds / _SecondsInDay);

        int yearChange = currentEpochYear - epochYear;
        float dayChange = currentEpochDay - epochDay;
        //Debug.Log(yearChange);
        //Debug.Log(dayChange);

        return dayChange + (yearChange * 365);
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
        Vector3 rotation = new Vector3(inclination, 0f, arguementOfPeriapsis);
        Vector3 newPos = Rotate(position, rotation);
        Vector3 secondRotation = new Vector3(0f, 0f, rightAscensionOfAscendingNode);
        newPos = Rotate(newPos, secondRotation);
        return newPos;
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
