using UnityEngine;
using System.Collections;

public class varyMyScale : MonoBehaviour {

    public float Amplitude = 1;

	// Use this for initialization
	void Start ()
    { 

	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.localScale = Amplitude * new Vector3(1+.5f*Mathf.Sin(Time.time/5), 1.0f, 1+ .5f * Mathf.Sin(Time.time/5));
	}
}
