using UnityEngine;
using System.Collections;
using System;

public class OrbitalPosTester : MonoBehaviour {

    public float inversePositionScale = 1f;
    public float timeScale = 1f;

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

    public DateTime readingTime;

    private DateTime startTime;

    private Vector3[] orbitPositions;
    private GameObject[] satellites;
    private bool loadedSatellites = false;

    public GameObject planet;
    public HTMLParser parser;

    void Start()
    {
        startTime = DateTime.UtcNow;
    }

    void Update()
    {
        AdvanceTime();
        CheckSatellites();
        //double epoch = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
    }

    private void AdvanceTime()
    {
        readingTime = startTime.AddSeconds((double)(Time.time * timeScale));
    }

    private void CheckSatellites()
    {
        if (parser.loadedSatellites)
        {
            if (loadedSatellites == false)
            {
                LoadSatellites();
            }
            MoveSatellites();
        }
    }

    private void LoadSatellites()
    {
        satellites = new GameObject[parser.Satellites.Count];
        for (int i = 0; i < satellites.Length; i++)
        {
            satellites[i] = Instantiate(planet);
            satellites[i].transform.SetParent(transform);
            satellites[i].name = parser.Satellites[i].Name;
        }
        loadedSatellites = true;
    }

    private void MoveSatellites()
    {
        for (int i = 0; i < satellites.Length; i++)
        {
            Vector3 position = TLEtoVec3.Convert(parser.Satellites[i], readingTime, accuracy, inversePositionScale);
            satellites[i].transform.position = position;
        }
    }

    private void FillPositions()
    {
        orbitPositions = new Vector3[conicSegments];
        for(int i = 0; i < conicSegments; i++)
        {
            float anomaly = (i * 2f * Mathf.PI) / (conicSegments);
            int tleYear = DateTime.UtcNow.Year % 100;
            float tleDay = DateTime.UtcNow.DayOfYear + ((float)DateTime.UtcNow.TimeOfDay.TotalSeconds / 86164f);
            Satellite sat = new Satellite("Test", tleYear, tleDay, inclination, rightAscensionOfAscendingNode, eccentricity, arguementOfPeriapsis, anomaly, meanMotion);
            orbitPositions[i] = TLEtoVec3.Convert(sat, DateTime.UtcNow, accuracy, inversePositionScale);
        }
    }

    void OnDrawGizmos()
    {
        FillPositions();
        Gizmos.color = Color.red;
        for (int i = 0; i < orbitPositions.Length - 1; i++)
        {
            //Gizmos.DrawCube(orbitPositions[i], Vector3.one);
            Gizmos.DrawLine(orbitPositions[i], orbitPositions[i + 1]);
        }
        Gizmos.DrawLine(orbitPositions[conicSegments - 1], orbitPositions[0]);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(orbitPositions[0], Vector3.zero);
    }
}
