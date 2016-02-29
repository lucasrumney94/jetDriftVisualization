using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;



public class HTMLParser : MonoBehaviour {

    public bool loadedSatellites = false;

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
                int EpochYear;
                float EpochDay;
                float Inclination;
                float RightAscensionOfTheAscendingNode;
                float Eccentricity;
                float ArgumentOfPeriapsis;
                float MeanAnomaly;
                float MeanMotion;

                Name = parseTextNewlined[i];
                EpochYear = int.Parse(parseTextNewlined[i + 1].Substring(18, 2));
                EpochDay = float.Parse(parseTextNewlined[i + 1].Substring(20, 9));
                Inclination = float.Parse(parseTextNewlined[i + 2].Substring(8, 7));
                RightAscensionOfTheAscendingNode = float.Parse(parseTextNewlined[i + 2].Substring(17, 7));
                Eccentricity = 0f;// float.Parse(parseTextNewlined[i + 2].Substring(26, 6)); //Needs to be converted to a decimal
                ArgumentOfPeriapsis = float.Parse(parseTextNewlined[i + 2].Substring(34, 7));
                MeanAnomaly = float.Parse(parseTextNewlined[i + 2].Substring(43, 6));
                MeanMotion = float.Parse(parseTextNewlined[i + 2].Substring(52, 10));

                Satellites.Add(new Satellite(Name, EpochYear, EpochDay, Inclination, RightAscensionOfTheAscendingNode, Eccentricity, ArgumentOfPeriapsis, MeanAnomaly, MeanMotion));

            }
            loadedSatellites = true;
        }

        //foreach (Satellite sputnik in Satellites)
        //{
        //    Debug.Log("Name: " + sputnik.Name);
        //    Debug.Log("Inclination: " + sputnik.Inclination);
        //    Debug.Log("Eccentricity: " + sputnik.Eccentricity);
        //    Debug.Log("Argument Of Periapsis: " + sputnik.ArgumentOfPeriapsis);
        //    Debug.Log("Mean Anomaly: " + sputnik.MeanAnomaly);
        //    Debug.Log("Mean Motion: " + sputnik.Inclination);
        //}

    }

    // Update is called once per frame
    void Update ()
    {
    }
}
