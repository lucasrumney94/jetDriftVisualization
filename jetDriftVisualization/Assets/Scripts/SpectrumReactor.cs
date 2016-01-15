using UnityEngine;
using System.Collections;

public class SpectrumReactor : MonoBehaviour
{
    private GameObject listenToMe;
    // array of float values we will store the spectrum information in.     
    public float[] Samples = new float[8192];
    //temp storage
    float Sum = 0.0f;
    float R = 0.0f;
    float G = 0.0f;
    float B = 0.0f;

    public int myIndexLow;
    public int myIndexHigh;
    public bool scaleOnlyZ = false;


	public int bandWidth = 2;

    //What mode to operate in
    public bool rotateMe = false;
    public bool colorMe = false;
    public bool scaleMe = false;

    //how frequently to update
    public float Interval;
    [Range(6,13)]
    public int powerOfTwoSamples = 6;

    public float Gain = 100.0f;

    //internal float used to track the timer
    float Timer = 0.0f;


    void Start()
    {
        Timer = Interval;
        if (colorMe)
        {
            this.gameObject.AddComponent<Renderer>();
            gameObject.GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Color"));
        }
        listenToMe = this.gameObject;
    }

    void Update()
    {
        //Debug.Log(new Vector3(R, G, B));
        Timer -= Time.deltaTime;

        if (Timer <= 0.0f)
        {
            //reset timer to interval
            Timer = Interval;

            Samples = listenToMe.GetComponent<AudioSource>().GetSpectrumData((int)Mathf.Pow(2, powerOfTwoSamples), 0, FFTWindow.BlackmanHarris);// AudioListener.GetSpectrumData((int)Mathf.Pow(2,powerOfTwoSamples), 0, FFTWindow.BlackmanHarris);
            //listenToMe.GetComponent<AudioSource>().GetSpectrumData(Samples, 0, FFTWindow.BlackmanHarris);

            if (myIndexHigh != 0)
            {
                for (int i = myIndexLow; i < myIndexHigh; i++)
                {
                    Sum += Samples[i];
                    R = myIndexLow/Gain;
                    G = 0;
                    B = Sum * Gain; 
                    //if (this.gameObject.name == "Cube (6)")
                        //Debug.Log(Gain);
                }
                Sum = 0;
            }
            else
            {
                //AudioListener.GetOutputData(Samples, 0);
                //loop the first 21 samples
                for (int i = 0; i < bandWidth; i++)
                {
                    Sum += Samples[i];
                }

                R = (Sum / bandWidth) * Gain;

                Sum = 0.0f;

                for (int i = bandWidth; i < 2 * bandWidth; i++)
                {
                    Sum += Samples[i];
                }

                G = (Sum / bandWidth) * Gain;

                Sum = 0.0f;

                for (int i = 2 * bandWidth; i < (int)Mathf.Pow(2, powerOfTwoSamples); i++)
                {
                    Sum += Samples[i];
                }

                B = (Sum / bandWidth) * Gain;

                Sum = 0.0f;

            }
            if (rotateMe)
                RotateObject();

            if (colorMe)
                ChangeColor();
            
            if (scaleMe)
                ChangeScale();
            
        }
    }

    void ChangeColor()
    {
        gameObject.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_Color", new Color(R, G, B));
    }

    void RotateObject()
    {
        gameObject.transform.Rotate(new Vector3(R, G, B));
    }
    void ChangeScale()
    {
        if (scaleOnlyZ)
        {
            gameObject.transform.localScale = new Vector3(1 , 1 , 1 + B);
        }
        else
            gameObject.transform.localScale = new Vector3(1+R, 1+G, 1+B);
    }

}