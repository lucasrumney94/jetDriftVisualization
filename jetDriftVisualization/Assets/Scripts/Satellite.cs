using UnityEngine;
using System.Collections;

public class Satellite
{
    public string Name;
    public int EpochYear;
    public float EpochDay;
    public float Inclination;
    public float RightAscensionOfTheAscendingNode;
    public float Eccentricity;
    public float ArgumentOfPeriapsis;
    public float MeanAnomaly;
    public float MeanMotion;

    public Satellite(string name, int epochYear, float epochDay, float inclination, float rightAscensionOfTheAscendingNode, float eccentricity, float argumentOfPeriapsis, float meanAnomaly, float meanMotion)
    {
        this.Name = name;
        this.EpochYear = epochYear;
        this.EpochDay = epochDay;
        this.Inclination = inclination;
        this.RightAscensionOfTheAscendingNode = rightAscensionOfTheAscendingNode;
        this.Eccentricity = eccentricity;
        this.ArgumentOfPeriapsis = argumentOfPeriapsis;
        this.MeanAnomaly = meanAnomaly;
        this.MeanMotion = meanMotion;
    }
}
