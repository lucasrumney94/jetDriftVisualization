using UnityEngine;
using System.Collections;

public class rotatePingPong : MonoBehaviour {

    public float amplitude;
    public float frequency = 1;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.eulerAngles = new Vector3(amplitude*Mathf.Cos(Time.time*frequency*2*Mathf.PI),0.0f,0.0f);
	}
}
