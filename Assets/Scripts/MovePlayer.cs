using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MovePlayer : MonoBehaviour {
	[Range (1, 10)]
	public	float			lengthRay = 4f;
	public	GameObject		Head;
	static public bool		lookAtMarker = false;
	static public bool		lookObject = false;
	
	private RaycastHit 		hit;
	private	GameObject		marker = null;
	private AsyncOperation asyncOp;

	void Start () {
		StartCoroutine("load");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 rayDirection = Head.transform.TransformDirection(Vector3.forward);
		Vector3 rayStart = Head.transform.position + rayDirection;

		//Ray rayLeft = cam.leftEyeCamera.ScreenPointToRay(transform.position);
		Debug.DrawRay(rayStart,rayDirection * lengthRay, Color.red);
		lookAtMarker = false;
		if (marker != null)
		{
			marker.GetComponent<InteractionManager>().desactivate();
			marker = null;
		}
		if (Physics.Raycast (rayStart, rayDirection, out hit, lengthRay))
		{
			Debug.Log(hit.collider.tag);
			if (hit.collider.tag != "Select" && !(Zoom.zoomIn))
				lookObject = false;
			else
				lookObject = true;

			if (hit.collider.name == "Mark" && !(Zoom.zoomIn))
				Move();
			if (hit.collider.name == "Back")
				Back();
		}

	}

	void Move()
	{
		marker = hit.collider.gameObject;
		marker.GetComponent<InteractionManager>().activate();
		lookAtMarker = true;
		
		if (Input.GetMouseButtonUp(0) && lookAtMarker)
		{
			//Debug.Log(fillableImage.enabled);
			//Vector3 oldPos = transform.position;
			Vector3 newPos = hit.collider.gameObject.transform.position;
			
			newPos.y = 2f;
			transform.position = newPos;
			
			Vector3 look = new Vector3(0f, 2f, 0f);
			
			transform.LookAt(Vector3.zero);
			Vector3 tmp = new Vector3 (0f, transform.eulerAngles.y, 0f);
			transform.eulerAngles = tmp;
		}
	}

	void Back()
	{
		marker = hit.collider.gameObject;
		marker.GetComponent<InteractionManager>().activate();
		lookAtMarker = true;
		
		if (Input.GetMouseButtonUp(0) && lookAtMarker)
		{
			asyncOp.allowSceneActivation = true;
			foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Select"))
				Destroy(obj);
		}
	}

	IEnumerator load()
	{
		asyncOp = Application.LoadLevelAsync(1);
		asyncOp.allowSceneActivation = false;
		yield return asyncOp;
	}
}
