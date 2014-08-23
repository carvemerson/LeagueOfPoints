using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Matrix : MonoBehaviour {

	public GameObject objeto;
	public int columnSize;
	public int lineSize;
	public float dist;
	private List <GameObject> cells;
	private GameObject nearPoint;
	List <int> setedPoints;
	
	// Use this for initialization
	void Start () {
		cells = new List<GameObject>();
		setedPoints = new List<int>();
		nearPoint = new GameObject("nearPoint");
		
		InitMatatrix();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public GameObject nearestPoint(Vector3 v){
		
		float distance = 10000f;
		foreach(GameObject x in cells){
			float aux = Vector3.Distance(v, x.transform.position);
			if(aux < distance){
				distance = aux;
				nearPoint = x;
				setedPoints.Add (0);
			}
		}
		return nearPoint;
	}
	
	void InitMatatrix(){
		for(int i = 0; i < columnSize; i++){
			for(int j = 0; j < lineSize; j++){
				GameObject x = Instantiate(objeto) as GameObject;
				x.transform.position = new Vector3((i - (columnSize/2))*dist, (j - (lineSize/2))*dist, 0);
				x.gameObject.name = "Ball " + i.ToString() + " " + j.ToString();
				cells.Add(x);	
			}
		}
	}
	
}
