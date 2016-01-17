using UnityEngine;
using System.Collections;

public class graphController : MonoBehaviour {

    
    [Range(6, 13)]
    public int powerOfTwoSamples = 6;
    public int numberOfBars = 40;

    public float timingInterval;

    public GameObject cubeMUTE;


    public float[] Samples = new float[64];

    private float Timer = 0.0f;
	// Use this for initialization
	void Start ()
    {
        //SET i to the beginning multiple
        int i = 0; //keeps track of each range
        for (int j = 2; j<numberOfBars; j++)
        {
            GameObject temp = GameObject.Instantiate(cubeMUTE, new Vector3(j,0.0f,0.0f), Quaternion.identity) as GameObject;
            temp.transform.parent = this.gameObject.transform;
        }

        foreach (SpectrumReactor SR in this.gameObject.GetComponentsInChildren<SpectrumReactor>())
        {
            SR.myIndexLow = i * Mathf.FloorToInt(powerOfTwoSamples / this.gameObject.transform.childCount);
            i++;
            SR.myIndexHigh = i*Mathf.FloorToInt(powerOfTwoSamples / this.gameObject.transform.childCount);
            //i++;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0.0f)
        {
            Timer = timingInterval;
            Samples = gameObject.GetComponent<AudioSource>().GetSpectrumData((int)Mathf.Pow(2, powerOfTwoSamples), 0, FFTWindow.BlackmanHarris);
        }
        foreach (giveMeAmplitude bar in this.gameObject.GetComponentsInChildren<giveMeAmplitude>())
        {
            //bar.amplitude = 
        }
    }
}
