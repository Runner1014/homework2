using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {
    public Transform sun;
    public Transform Mercury;
    public Transform Venus;
    public Transform Earth;
    public Transform Mars;
    public Transform Jupiter;
    public Transform Saturn;
    public Transform Uranus;
    public Transform Neptune;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Mercury.Rotate(Vector3.up * Time.deltaTime * 100);
        Mercury.RotateAround(sun.position, new Vector3(0, 1, 0), 100 * Time.deltaTime);

        Venus.Rotate(Vector3.up * Time.deltaTime * 100);
        Venus.RotateAround(sun.position, new Vector3(0.1f, 1, 0), 90 * Time.deltaTime);

        Earth.Rotate(Vector3.up * Time.deltaTime * 100);
        Earth.RotateAround(sun.position, new Vector3(0.2f, 1, 0), 80 * Time.deltaTime);

        Mars.Rotate(Vector3.up * Time.deltaTime * 100);
        Mars.RotateAround(sun.position, new Vector3(0.3f, 1, 0), 70 * Time.deltaTime);

        Jupiter.Rotate(Vector3.up * Time.deltaTime * 100);
        Jupiter.RotateAround(sun.position, new Vector3(0, 1, 0.1f), 60 * Time.deltaTime);

        Saturn.Rotate(Vector3.up * Time.deltaTime * 100);
        Saturn.RotateAround(sun.position, new Vector3(0, 1, 0.2f), 50 * Time.deltaTime);

        Uranus.Rotate(Vector3.up * Time.deltaTime * 100);
        Uranus.RotateAround(sun.position, new Vector3(0, 1, 0.3f), 40 * Time.deltaTime);

        Neptune.Rotate(Vector3.up * Time.deltaTime * 100);
        Neptune.RotateAround(sun.position, new Vector3(0.1f, 1, 0.1f), 30 * Time.deltaTime);
    }
}
