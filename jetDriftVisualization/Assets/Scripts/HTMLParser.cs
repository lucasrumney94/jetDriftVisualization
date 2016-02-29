using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;



public class HTMLParser : MonoBehaviour {

    public string[] URLs = { "http://celestrak.com/NORAD/elements/weather.txt" };
    public List<Satellite> Satellites = new List<Satellite>();

    private WWW www;
    // Use this for initialization
    IEnumerator Start()
    {
        //TLEtoVec3.Convert(inclination, rightAscensionOfAscendingNode, eccentricity, arguementOfPeriapsis, meanAnomaly, meanMotion, accuracy, inversePositionScale);
       
        foreach (string url in URLs)
        {
            www = new WWW(url);
            yield return www;
            string parseText = www.text;

            //BEGIN TEXT PARSING
            string[] parseTextNewlined = parseText.Split('\n');

            for (int i = 0; i <parseTextNewlined.Length-3; i=i+3)
            {
                //Begin breaking down lines and Creating Satellite Objects
                //create temporary strings to hold extracted data then assign after all data has been extracted
                string Name;
                float Inclination;
                float Eccentricity;
                float ArgumentOfPeriapsis;
                float MeanAnomaly;
                float MeanMotion;

                Name = parseTextNewlined[i];
                Inclination = float.Parse(parseTextNewlined[i + 2].Substring(9,7));
                Eccentricity = float.Parse(parseTextNewlined[i + 2].Substring(27, 6));
                ArgumentOfPeriapsis = float.Parse(parseTextNewlined[i + 2].Substring(35, 7));
                MeanAnomaly = float.Parse(parseTextNewlined[i + 2].Substring(44, 6));
                MeanMotion = float.Parse(parseTextNewlined[i + 2].Substring(53, 10));

                Satellites.Add(new Satellite(Name,Inclination,Eccentricity,ArgumentOfPeriapsis,MeanAnomaly,MeanMotion));

            }


        }

        foreach (Satellite sputnik in Satellites)
        {
            Debug.Log("Name: " + sputnik.Name);
            Debug.Log("Inclination: " + sputnik.Inclination);
            Debug.Log("Eccentricity: " + sputnik.Eccentricity);
            Debug.Log("Argument Of Periapsis: " + sputnik.ArgumentOfPeriapsis);
            Debug.Log("Mean Anomaly: " + sputnik.MeanAnomaly);
            Debug.Log("Mean Motion: " + sputnik.Inclination);
        }

    }

    // Update is called once per frame
    void Update ()
    {
    }
}
