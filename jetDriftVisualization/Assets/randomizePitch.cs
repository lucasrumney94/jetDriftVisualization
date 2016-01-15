using UnityEngine;
using System.Collections;

public class randomizePitch : MonoBehaviour {

    private AudioSource audio;
	// Use this for initialization
	void Start ()
    {
        audio = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        audio.pitch = Mathf.Lerp(audio.pitch,audio.pitch + Random.Range(-0.3f, 0.1f),.05f);
	}
}
