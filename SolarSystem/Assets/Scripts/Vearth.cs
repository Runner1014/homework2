using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vearth : MonoBehaviour {
    public Transform sun;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.RotateAround(sun.position, new Vector3(0.2f, 1, 0), 80 * Time.deltaTime);
    }
}
