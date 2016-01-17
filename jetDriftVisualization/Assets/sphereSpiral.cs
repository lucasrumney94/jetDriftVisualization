using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class sphereSpiral : MonoBehaviour {

    
    public const int numberOfSpheres = 2048;

    public GameObject spiralSphere;
    public bool goldenRatio = true;
    public bool e = false;
    public bool sqrt2 = false;
    public bool sqrt3 = false;
    public bool pi = false;
    public bool sqrt7 = false;
    public bool other = false;
    public float otherNum = 0.0f;
    public bool flange = false;
    public float flangeAmount = 0.01f;

    private float seedAngle = 0.0f;// Mathf.Sqrt(2);//Mathf.PI * (3 - Mathf.Sqrt(5));

    private Vector3[] positions = new Vector3[numberOfSpheres];

    private float theta = 0.0f;
    private float r = 0.0f;
    private List<float> ys = new List<float>();
    private float radius = 0.0f;

    private float sqrtNumOfSpheres; 

    private List<GameObject> spheres = new List<GameObject>();
    private waveGenerator waveGen;
    private Vector3 tempPos = new Vector3(0.0f,0.0f,0.0f);

    // Use this for initialization
    void Start ()
    {
        if (goldenRatio)
            seedAngle = Mathf.PI * (3 - Mathf.Sqrt(5));
        if (sqrt2)
            seedAngle = Mathf.Sqrt(2);
        if (sqrt3)
            seedAngle = Mathf.Sqrt(3);
        if (pi)
            seedAngle = Mathf.PI;
        if (sqrt7)
            seedAngle = Mathf.Sqrt(7);
        if (e)
            seedAngle = Mathf.Exp(1);
        if (other)
            seedAngle = otherNum;
        if (flange)
            seedAngle = Mathf.Sin(2 * Mathf.PI * Time.time * flangeAmount);

        waveGen = this.gameObject.GetComponent<waveGenerator>();
        sqrtNumOfSpheres = Mathf.Sqrt(numberOfSpheres);

        ys = libSVM.Numpy.LinSpace(1 - 1 / numberOfSpheres, 1 / numberOfSpheres - 1, numberOfSpheres).ToList<double>().ConvertAll(x => (float)x);

        for (int i = 0; i < numberOfSpheres; i++)
        {
            theta = i * seedAngle;
            r = Mathf.Sqrt(i) / sqrtNumOfSpheres;
            
            radius = Mathf.Sqrt(1 - ys[i] * ys[i]);
            positions[i] = new Vector3(radius*Mathf.Cos(theta),ys[i],radius*Mathf.Sin(theta));

            GameObject temp = GameObject.Instantiate(spiralSphere, positions[i], Quaternion.identity) as GameObject;
            temp.transform.parent = this.gameObject.transform;
            spheres.Add(temp);
        }
        Debug.Log(ys.Count);
    }
	
	// Update is called once per frame
	void Update ()
    {

        /*
        if (goldenRatio)
            seedAngle = Mathf.PI * (3 - Mathf.Sqrt(5));
        if (sqrt2)
            seedAngle = Mathf.Sqrt(2);
        if (sqrt3)
            seedAngle = Mathf.Sqrt(3);
        if (pi)
            seedAngle = Mathf.PI;
        if (sqrt7)
            seedAngle = Mathf.Sqrt(7);
        if (e)
            seedAngle = Mathf.Exp(1);
        if (other)
            seedAngle = otherNum;
        */
        if (flange)
            seedAngle = Mathf.Sin(2 * Mathf.PI * Time.time * flangeAmount);
        

        for (int i = 0; i < numberOfSpheres; i++)
        {
            theta = i * seedAngle;
            r = Mathf.Sqrt(i) / sqrtNumOfSpheres;
            
            radius = Mathf.Sqrt(1 - ys[i] * ys[i]);
            tempPos.x = radius * Mathf.Cos(theta) * waveGen.Samples[i];
            tempPos.y = ys[i];
            tempPos.z = radius * Mathf.Sin(theta) * waveGen.Samples[i];
            spheres[i].transform.position = tempPos;

            //spheres[i].transform.position = new Vector3(radius * Mathf.Cos(theta) * waveGen.Samples[i], y, radius * Mathf.Sin(theta) * waveGen.Samples[i]);
        }
    }
}
