using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class sphereSpiral : MonoBehaviour {

    
    public const int numberOfSpheres = 512;

    public GameObject spiralSphere;

    private float goldenAngle = Mathf.PI * (3 - Mathf.Sqrt(5));

    private Vector3[] positions = new Vector3[numberOfSpheres];

    private float theta = 0.0f;
    private float r = 0.0f;
    private float y = 0.0f;
    private float radius = 0.0f;

    private List<GameObject> spheres = new List<GameObject>();
    private waveGenerator waveGen;

    // Use this for initialization
    void Start ()
    {
        waveGen = this.gameObject.GetComponent<waveGenerator>();
        for (int i = 0; i < numberOfSpheres; i++)
        {
            theta = i * goldenAngle;
            r = Mathf.Sqrt(i) / Mathf.Sqrt(numberOfSpheres);
            y = (float)libSVM.Numpy.LinSpace(1 - 1 / numberOfSpheres, 1/ numberOfSpheres - 1, numberOfSpheres).ElementAt<double>(i);
            radius = Mathf.Sqrt(1 - y * y);
            positions[i] = new Vector3(radius*Mathf.Cos(theta),y,radius*Mathf.Sin(theta));

            GameObject temp = GameObject.Instantiate(spiralSphere, positions[i], Quaternion.identity) as GameObject;
            temp.transform.parent = this.gameObject.transform;
            spheres.Add(temp);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < numberOfSpheres; i++)
        {
            theta = i * goldenAngle;
            r = Mathf.Sqrt(i) / Mathf.Sqrt(numberOfSpheres);
            y = (float)libSVM.Numpy.LinSpace(1 - 1 / numberOfSpheres, 1 / numberOfSpheres - 1, numberOfSpheres).ElementAt<double>(i);
            radius = Mathf.Sqrt(1 - y * y);
            spheres[i].transform.position = new Vector3(radius * Mathf.Cos(theta) * waveGen.Samples[i], y, radius * Mathf.Sin(theta) * waveGen.Samples[i]);
        }
    }
}
