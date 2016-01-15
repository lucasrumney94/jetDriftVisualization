using UnityEngine;
using System.Collections;

public class AudioAnalyzer : MonoBehaviour
{

    public float RMSValue;
    public float DbValue;

    private const int QSamples = 8192;
    private const float RefValue = 0.1f;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
