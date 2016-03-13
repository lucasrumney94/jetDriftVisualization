using UnityEngine;
using System.Collections;

public class ScaleToPlanet : MonoBehaviour {

    public OrbitalPosTester orbitalStats;

    private float _RadiusOfEarth = 6378137f;

    void Update()
    {
        float inverseScale = orbitalStats.inversePositionScale;
        transform.localScale = Vector3.one * (_RadiusOfEarth / inverseScale);
    }
}
