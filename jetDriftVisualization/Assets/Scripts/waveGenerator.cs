using UnityEngine;
using System.Collections;

public class waveGenerator : MonoBehaviour {

    public float[] Samples = new float[100];



    public bool travelingWave = true;
    public float freq = 1.0f;
    public float Amplitude = 1.0f;
    public float sampleRate = 20.0f;

    public bool multiCos;
    public float mC1Freq = 20.0f;
    public float mC1Amplitude = 1.0f;
    public float mc1SR = 20.0f;
    public float mC2Freq = 20.0f;
    public float mC2Amplitude = 1.0f;
    public float mc2SR = 20.0f;
    public float mC3Freq = 20.0f;
    public float mC3Amplitude = 1.0f;
    public float mc3SR = 20.0f;

    public bool pulse = false;
    public float pulseAmplitude = 1.0f;
    public float pulseDecay = 0.7f;

    private int j = 0;

    


	// Use this for initialization
	void Start ()
    {
        if (travelingWave)
        {
            for (int i = 0; i < Samples.Length; i++)
            {
                Samples[i] = Amplitude * Mathf.Cos(2 * Mathf.PI * i * freq / sampleRate);
                //Debug.Log(Samples[i]);
            }
        }
       
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (travelingWave)
        {
            for (int i = 0; i < Samples.Length; i++)
            {
                Samples[i] = Mathf.Abs(Amplitude * Mathf.Cos((2 * Mathf.PI * i * freq) / sampleRate + (j * Mathf.PI / sampleRate)));
                //Debug.Log(Samples[i]);
            }
            j++;
            if (j > 314159)
                j = 0;
        }

        if (multiCos)
        {
            for (int i = 0; i < Samples.Length; i++)
            {
                Samples[i] = Mathf.Abs(mC1Amplitude * Mathf.Cos(2 * Mathf.PI * i * mC1Freq + (j * Mathf.PI / mc1SR)) + mC2Amplitude * Mathf.Cos(2 * Mathf.PI * i * mC2Freq + (j * Mathf.PI / mc2SR)) + mC3Amplitude * Mathf.Cos(2 * Mathf.PI * i * mC3Freq + (j * Mathf.PI / mc3SR)));
                //Debug.Log(Samples[i]);
            }
            j++;
            if (j > 314159)
                j = 0;
        }
        if (pulse)
        {
            if (j > Samples.Length-1)
                j = 0;
            for (int i = 0; i < Samples.Length; i++)
            {
                Samples[i] = 0;
                if (i == j)
                    Samples[i] = pulseAmplitude;
                if (j > 15)
                {
                    for (int k = 1; k < 15; k++)
                    {
                        Samples[j - k] = pulseAmplitude*Mathf.Pow(pulseDecay, j - k);
                        
                    }
                }
                //Debug.Log(Samples[i]);
            }
            j++;
            
        }
    }
}
