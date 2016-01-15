using UnityEngine;
using System.Collections;

public class waveGenerator : MonoBehaviour {

    public float[] Samples = new float[100];

    public float sampleRate = 20.0f;
    public float freq = 1.0f;
    public float A = 1.0f;

    private int j = 0;

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < Samples.Length; i++)
        {
            Samples[i] = A*Mathf.Cos(2 * Mathf.PI * i * freq / sampleRate);
            //Debug.Log(Samples[i]);
        }
       
    }
	
	// Update is called once per frame
	void Update ()
    {

        
        for (int i = 0; i < Samples.Length; i++)
        {
            Samples[i] = Mathf.Cos(2 * Mathf.PI * i / sampleRate + (j * Mathf.PI / sampleRate));
            //Debug.Log(Samples[i]);
        }
        j++;
        if (j > 314159)
            j = 0;
        
    }
}
