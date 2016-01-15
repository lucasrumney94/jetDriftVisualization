using UnityEngine;
using System.Collections;

public class graphController : MonoBehaviour {

    public int mustBeAPowerOfTwo = 8096;

	// Use this for initialization
	void Start ()
    {
        int i = 0; //keeps track of each range

        foreach (SpectrumReactor SR in this.gameObject.GetComponentsInChildren<SpectrumReactor>())
        {
            SR.myIndexLow = i * Mathf.FloorToInt(mustBeAPowerOfTwo / this.gameObject.transform.childCount);
            i++;
            SR.myIndexHigh = i*Mathf.FloorToInt(mustBeAPowerOfTwo / this.gameObject.transform.childCount);
            //i++;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
       
	}
}
