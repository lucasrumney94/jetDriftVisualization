using UnityEngine;
using System.Collections;

public class swarmObject : MonoBehaviour {

    public float zDriftMagnitude = 9f;

    public Vector3 targetGridSquare = new Vector3();

	// Use this for initialization
	void Awake ()
    {
        gameObject.GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Color"));
        //gameObject.GetComponent<Renderer>().material.SetFloat("_Mode", 3);
        //gameObject.GetComponent<Renderer>().material.SetFloat("_Glossiness", 0);

    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        //gameObject.GetComponent<Renderer>().material.SetFloat("_Mode", 3);
        //gameObject.GetComponent<Renderer>().material.SetFloat("_Glossiness", 0);
        Vector3 zDrift = new Vector3(0f, 0f, Random.Range(-1f, 1f)) * zDriftMagnitude;
        this.gameObject.GetComponent<Rigidbody>().AddForce((targetGridSquare - this.gameObject.transform.position) + zDrift); // +9*Random.insideUnitSphere);
        //if (Vector3.Distance(targetGridSquare, transform.position) < 0.1f)
        //{
        //    transform.position = new Vector3(targetGridSquare.x, targetGridSquare.y, transform.position.z);
        //    GetComponent<Rigidbody>().velocity = Vector3.zero;
        //}

        this.gameObject.GetComponent<Rigidbody>().AddForce(5*new Vector3(0, 0, Mathf.Sin(Time.time + targetGridSquare.magnitude / 10*Mathf.Sin(Time.time/10))));
    }
}
