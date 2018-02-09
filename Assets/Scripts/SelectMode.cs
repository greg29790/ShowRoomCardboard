using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using Ovr;

public class SelectMode : MonoBehaviour {
	
	public GameObject[]	listObject;
	[Range (1f, 3f)]
	public float 		CenterScale = 2.5f;
	[Range (0.5f, 2f)]
	public float		MinScale	= 1f;
	[Range (-100f, 100f)]
	public float 		rotateSpeed = 40f;

	public	GameObject		Head;
	private RaycastHit 		hit;

	private	GameObject 	clone;
	private	GameObject 	clone_min_left;
	private	GameObject 	clone_min_right;

	private GameObject	look;

	private int			index = 1;
	private int 		index_left = 0;
	private int 		index_right = 2;

	private Vector2		first;
	private Vector2		second;
	private Vector3	 	currentSwipe;

	private bool 		one_click = false;
	private bool 		timer_running;
	private float 		timer_for_double_click;

	// Use this for initialization
	void Start () {
		if (listObject.Length == 3)
		{
			clone = Instantiate(listObject[index]) as GameObject;
			clone_min_left = Instantiate(listObject[index - 1]) as GameObject;
			clone_min_right = Instantiate(listObject[index + 1]) as GameObject;

			MoveToCenter(clone);
			MoveToLeft(clone_min_left);
			MoveToRigth(clone_min_right);

			AddBoxCollider(clone);
			AddBoxCollider(clone_min_left);
			AddBoxCollider(clone_min_right);
			look = clone;
		}
	}


	// Update is called once per frame
	void Update () {

		changeObject();
		Rotate(look);
		Select();
	}

	void Rotate(GameObject obj)
	{
		obj.transform.RotateAround(obj.transform.position, obj.transform.up, Time.deltaTime * rotateSpeed);
	}

	void StopRotate(GameObject obj)
	{
		obj.transform.RotateAround(obj.transform.position, obj.transform.up, 0);
		obj.transform.eulerAngles = new Vector3(0, 0, 0);
	}

	void DoubleTap()
	{
		float delay = 0.5f;

		if (Input.GetMouseButtonDown(0))
		{
			if (!one_click)
			{
				one_click = true;
				timer_for_double_click = Time.time;
			}
			else
			{
				one_click = false;
				Select();
			}
		}

		if (one_click)
		{
			if (Time.time - timer_for_double_click > delay)
				one_click = false;
		}
	}

	void Select()
	{
		if (Input.GetMouseButtonUp(0))
		{
			Debug.Log(look.name);
			DontDestroyOnLoad(look);
			look.transform.eulerAngles = new Vector3(0f, -90f,0f);
			look.transform.gameObject.tag = "Select";
			Application.LoadLevel("Room");
		}
	}

	void MoveToCenter(GameObject obj)
	{
		obj.transform.position = Vector3.zero;
		obj.transform.eulerAngles = new Vector3(0, 0, 0);
		scaleObject(obj, CenterScale);
	}

	void MoveToLeft(GameObject obj)
	{
		obj.transform.position = new Vector3(-8f, 0f, 0f);
		obj.transform.eulerAngles = new Vector3(0, 0, 0);
		scaleObject(obj, CenterScale);
	}

	void MoveToRigth(GameObject obj)
	{
		obj.transform.position = new Vector3(8f, 0f, 0f);
		obj.transform.eulerAngles = new Vector3(0, 0, 0);
		scaleObject(obj, CenterScale);
	}


	void changeObject()
	{
		Vector3 rayDirection = Head.transform.TransformDirection(Vector3.forward);
		Vector3 rayStart = Head.transform.position + rayDirection;
		
		Debug.DrawRay(rayStart,rayDirection * 10f, Color.red);
		if (Physics.Raycast (rayStart, rayDirection, out hit, 10f))
		{
			//Debug.Log(hit.collider.name);
			if (look.name != hit.collider.name)
			{
				StopRotate(look);
				if (hit.collider.gameObject.transform.parent == null)
					look = hit.collider.gameObject;
				else
				{
					look = hit.collider.gameObject.transform.parent.gameObject;
					look.name = hit.collider.name;
				}

			}
		}
	}

	void scaleObject(GameObject obj, float coef)
	{
		if (obj.GetComponents<MeshRenderer>().Length > 0)
			obj.transform.localScale *= ScaleCoef(obj.GetComponent<MeshRenderer>(), coef);
		else
		{
			if (obj.GetComponentsInChildren<MeshRenderer>().Length == 0)
				Debug.Log ("No mesh Render");
			else
				obj.transform.GetChild(0).localScale *= ScaleCoef(obj.GetComponentsInChildren<MeshRenderer>()[0], coef);
		}
	}

	float ScaleCoef(MeshRenderer mesh, float coef)
	{
		Vector3 size = mesh.bounds.extents;

		return (coef / size.z);
	}

	Vector3 GetSize(GameObject obj)
	{
		if (obj.GetComponents<MeshRenderer>().Length > 0)
			return obj.GetComponent<MeshRenderer>().bounds.extents;
		else
		{
			if (obj.GetComponentsInChildren<MeshRenderer>().Length == 0)
			{
				Debug.Log ("No mesh Render");
				return Vector3.one;
			}
			else
				return obj.GetComponentsInChildren<MeshRenderer>()[0].bounds.extents;
		}
	}

	void AddBoxCollider(GameObject obj)
	{
		if (obj.GetComponents<MeshRenderer>().Length > 0)
		{
			obj.AddComponent<BoxCollider>();
		}
		else
		{
			if (obj.GetComponentsInChildren<MeshRenderer>().Length == 0)
			{
				obj.AddComponent<BoxCollider>();
			}
			else
			{
				List<GameObject> list = new List<GameObject>();

				foreach(Transform t in obj.transform)
					list.Add(t.gameObject);
				GameObject child = list[0];
			
				child.AddComponent<BoxCollider>();
				child.tag = "Select";
			}
		}
	}
}
