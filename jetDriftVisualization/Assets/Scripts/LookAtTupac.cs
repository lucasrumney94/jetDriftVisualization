using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LookAtTupac : MonoBehaviour {
    public float argumentOfPeriapsis = 210.2578f;
    public float eccentricity = 0.0001540f;
    public float epochDay = 67.38270921f;
    public int epochYear = 16;
    public float meanAnomaly = 256.0601f;
    public float meanMotion = 1.00271837f;
    public float rightAscensionOfTheAscendingNode = 109.7942f;
    public float inclination = 0.0109f;
    public GameObject tupacPrefab;
    private GameObject myTupacPrefab;

    private Satellite Tupac;

    // Use this for initialization
    void Start ()
    {
        // 2pac
        //1 39481U 13075A   16067.38270921 -.00000212  00000-0  00000+0 0  9998
        //2 39481   0.0109 109.7942 0001540 210.2578 256.0601  1.00271837  8203

        Tupac = new Satellite("Tupac Reference", epochYear, epochDay, inclination, rightAscensionOfTheAscendingNode, eccentricity, argumentOfPeriapsis, meanAnomaly, meanMotion);
        Vector3 position = TLEtoVec3.Convert(Tupac, DateTime.UtcNow , 1, 100000);
        myTupacPrefab = Instantiate(tupacPrefab, position, Quaternion.identity) as GameObject;

    }
	
	// Update is called once per frame
	void Update ()
    {
        
        Vector3 position = TLEtoVec3.Convert(Tupac, DateTime.UtcNow.AddSeconds((double)Time.time*1800f), 1, 100000);
        
        myTupacPrefab.transform.position = position;

        transform.LookAt(myTupacPrefab.transform.position);
        transform.localEulerAngles += new Vector3(-35.7806f, 0.0f,0.0f);
    }
}
