using UnityEngine;
using System.Collections;

public class childSpin : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.Rotate(new Vector3(0, -4, 0));
        //this.transform.Ro
        //this.transform.Rotate(new Vector3(0, 0, 01));
    }
}
