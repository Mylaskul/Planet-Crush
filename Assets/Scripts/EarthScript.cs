using UnityEngine;
using System.Collections;

public class EarthScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.back);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		coll.transform.position = new Vector3(Random.Range(-WorldScript.horzExtent + 50, WorldScript.horzExtent - 50), Random.Range(-WorldScript.vertExtent + 50, WorldScript.vertExtent - 50), 0);
	}
}
