using UnityEngine;
using System.Collections;

public class PlaceObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject obj = GameObject.FindGameObjectWithTag("Select");
		if (obj != null)
		{
			obj.transform.eulerAngles = new Vector3(0f, 180f, 0f);
			obj.transform.position = new Vector3(0f, 0.374f, 00f);
			//obj.GetComponent<BoxCollider>().center = new Vector3(0f, 0f, 0f);
		}
	}

}
