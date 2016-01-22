using UnityEngine;
using System.Collections;

public class increaseMyFreq : MonoBehaviour {

    public float increment = 0.0001f;
    private waveGenerator waveGen;
	// Use this for initialization
	void Start ()
    {
        waveGen = gameObject.GetComponent<waveGenerator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        waveGen.freq += increment;
	}
}
