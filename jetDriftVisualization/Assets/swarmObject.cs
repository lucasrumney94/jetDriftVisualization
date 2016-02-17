using UnityEngine;
using System.Collections;

public class swarmObject : MonoBehaviour {

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
        this.gameObject.GetComponent<Rigidbody>().AddForce((targetGridSquare - this.gameObject.transform.position)+9*Random.insideUnitSphere);

        this.gameObject.GetComponent<Rigidbody>().AddForce(5*new Vector3(0, 0, Mathf.Sin(Time.time + targetGridSquare.magnitude / 10*Mathf.Sin(Time.time/10))));
    }
}
