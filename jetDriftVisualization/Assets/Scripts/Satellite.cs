using UnityEngine;
using System.Collections;

public class Satellite
{
    public string Name;
    public float Inclination;
    public float RightAscensionOfTheAscendingNode;
    public float Eccentricity;
    public float ArgumentOfPeriapsis;
    public float MeanAnomaly;
    public float MeanMotion;

    public Satellite(string name, float inclination, float rightAscensionOfTheAscendingNode, float eccentricity, float argumentOfPeriapsis, float meanAnomaly, float meanMotion)
    {
        this.Name = name;
        this.Inclination = inclination;
        this.RightAscensionOfTheAscendingNode = rightAscensionOfTheAscendingNode;
        this.Eccentricity = eccentricity;
        this.ArgumentOfPeriapsis = argumentOfPeriapsis;
        this.MeanAnomaly = meanAnomaly;
        this.MeanMotion = meanMotion;
    }
}
