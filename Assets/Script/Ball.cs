using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ball : MonoBehaviour {
	
	private ContrllLines cl;
	private Matrix mat;
	public GameObject ball;
	private GameObject point;
	public int i, j;
	// Use this for initialization
	void Start () {
		cl = FindObjectOfType(typeof(ContrllLines)) as ContrllLines;
		mat = FindObjectOfType(typeof(Matrix)) as Matrix;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnMouseDown(){
		
		cl.SetPoint(transform.position, ball);
		
	}
	
	public void OnMouseUp(){
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		point = mat.nearestPoint(mousePosition);
		cl.SetPoint(point.transform.position, point);
	}
}
