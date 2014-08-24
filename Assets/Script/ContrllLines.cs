using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ContrllLines : MonoBehaviour {
	
	public GameObject line;
	public List<GameObject> listLine;
	public int pointCount = 0;
	public GameObject player1, player2;
	public GameObject score1, score2;
	public float scaleFaces;
	public GameObject voice1, voice2;
	
	private bool firstPointSeted = false;
	private bool b1WasRed = false;
	private bool TimePlayer1 = true;
	
	private Vector3 startPoint;
	private Vector3 endPoint;
	
	private GameObject b1, b2;
	private GameObject followMouse;
	
	private Matrix matrix;
	
	private int scoreP1 = 0, scoreP2 = 0;
	
	
	// Use this for initialization
	void Start () {
		initFollowMouse();
		matrix = FindObjectOfType(typeof(Matrix)) as Matrix;
	}
	
	
	// Update is called once per frame
	void Update () {
		
		if(firstPointSeted){
			LineRenderer lr = followMouse.GetComponent<LineRenderer>();
			lr.SetPosition(0, startPoint);
			Vector3 mouse = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			        mouse.z = 0;
			lr.SetPosition(1,mouse);
		}
		
	}
	
	void initFollowMouse(){
		followMouse = Instantiate(line) as GameObject;
		followMouse.gameObject.name = "followMouse";
		LineRenderer lr = followMouse.GetComponent<LineRenderer>();
		lr.SetPosition(0, new Vector3(0,0,0));
		lr.SetPosition(1, new Vector3(0,0,0));
		
	}
	
	public void SetPoint(Vector3 v3, GameObject obj1){
		if(firstPointSeted == false){
			b1 = obj1;
			startPoint = v3;
			if(obj1.GetComponent<SpriteRenderer>().color == Color.red)  b1WasRed = true;
			else b1WasRed = false;
			obj1.GetComponent<SpriteRenderer>().color = Color.green;
			firstPointSeted = true;
		}else{
			b2 = obj1;
			endPoint = v3;
			float dist = Vector3.Distance(startPoint, endPoint);
			
			if(dist <= matrix.dist && startPoint != endPoint && !ExistsLine(startPoint, endPoint)){
				
				PrintNewLine();
				if(!VerifySquare()){ //  if not made a square, then
					ChangeTimePlayer(); // change player
				}
				ErasefollowMouse();
				
				
				firstPointSeted = false;
				
				b1.GetComponent<SpriteRenderer>().color = Color.red;
				b2.GetComponent<SpriteRenderer>().color = Color.red;
				
			}else{
				
				if(b1WasRed){
				
					b1.GetComponent<SpriteRenderer>().color = Color.red;
					b1WasRed = false;
				
				}else{
				
					b1.GetComponent<SpriteRenderer>().color = Color.white;
				
				}
				ErasefollowMouse();
				firstPointSeted = false;
			}
		}
	}
	
	void ChangeTimePlayer(){
		TimePlayer1 = !TimePlayer1;
	}
	
	bool VerifySquare(){
		bool res = false;
		if(startPoint.y == endPoint.y){ // New line is horizontal
			//Upper side
			Vector3 p1 = new Vector3(startPoint.x, startPoint.y + matrix.dist, startPoint.z);
			Vector3 p2 = new Vector3(endPoint.x, endPoint.y + matrix.dist, endPoint.z);
			bool isSquare = true;
			
			isSquare = isSquare && ExistsLine(startPoint, p1);
			isSquare = isSquare && ExistsLine(endPoint, p2);
			isSquare = isSquare && ExistsLine(p1, p2);
	
			if(isSquare){
				PlacingPlayer((startPoint+p2)/2);
				res = true;
			}
			
			//Underside
			Vector3 p3 = new Vector3(startPoint.x, startPoint.y - matrix.dist, startPoint.z);
			Vector3 p4 = new Vector3(endPoint.x, endPoint.y - matrix.dist, endPoint.z);
			isSquare = true;
			
			isSquare = isSquare && ExistsLine(startPoint, p3);
			isSquare = isSquare && ExistsLine(endPoint, p4);
			isSquare = isSquare && ExistsLine(p3, p4);
			
			if(isSquare){
				PlacingPlayer((startPoint+p4)/2);
				res = true;
			}
			
		}else{ // New line is vertical
			//right side
			Vector3 p1 = new Vector3(startPoint.x + matrix.dist, startPoint.y, startPoint.z);
			Vector3 p2 = new Vector3(endPoint.x + matrix.dist, endPoint.y, endPoint.z);
			bool isSquare = true;
			
			isSquare = isSquare && ExistsLine(startPoint, p1);
			isSquare = isSquare && ExistsLine(endPoint, p2);
			isSquare = isSquare && ExistsLine(p1, p2);
			
			if(isSquare){
				PlacingPlayer((startPoint+p2)/2);
				res = true;
			}
			
			//left side
			Vector3 p3 = new Vector3(startPoint.x - matrix.dist, startPoint.y, startPoint.z);
			Vector3 p4 = new Vector3(endPoint.x - matrix.dist, endPoint.y, endPoint.z);
			isSquare = true;
			
			isSquare = isSquare && ExistsLine(startPoint, p3);
			isSquare = isSquare && ExistsLine(endPoint, p4);
			isSquare = isSquare && ExistsLine(p3, p4);
			
			if(isSquare){
				PlacingPlayer((startPoint+p4)/2);
				res = true;
			}
			
		}
		return res;
	}
	
	void PlacingPlayer(Vector3 position){
		if(TimePlayer1){
			GameObject p = (Instantiate(player1) as GameObject);
			p.transform.position = position;
			p.transform.localScale = new Vector3(scaleFaces,scaleFaces, scaleFaces);
			scoreP1 += 1;
			TextMesh str = score1.GetComponent<TextMesh>();
			str.text = scoreP1.ToString();
			Destroy(Instantiate(voice1), 3f);
		}else{
			GameObject p = (Instantiate(player2) as GameObject);
			p.transform.position = position;
			p.transform.localScale = new Vector3(scaleFaces,scaleFaces,scaleFaces);
			scoreP2 += 1;
			TextMesh str = score2.GetComponent<TextMesh>(); 
 		 	str.text = scoreP2.ToString();
			Destroy(Instantiate(voice2), 3f );
		}
	}
	
	public void PrintNewLine(){
	
		listLine.Add (Instantiate(line) as GameObject); // Create new line
		listLine[listLine.Count-1].transform.position = (startPoint+endPoint)/2; // Define position of line
		
		//Configure LineRenderer
		LineRenderer lr = listLine[listLine.Count-1].GetComponent<LineRenderer>();
		lr.SetVertexCount(2);
		lr.SetPosition(0, startPoint);
		lr.SetPosition(1, endPoint);

		
	}
	
	
	
	bool ExistsLine(Vector3 point1, Vector3 point2){
		bool exist = false;
		Vector3 midlle = (point1 + point2)/2;
		foreach(GameObject g in listLine){
			if(g.transform.position == midlle){
				exist = true;
				break;
			}
		}
		return exist;
	}
	
	void ErasefollowMouse(){
		LineRenderer lr = followMouse.GetComponent<LineRenderer>();
		lr.SetPosition(0, new Vector3(0,0,0));
		lr.SetPosition(1, new Vector3(0,0,0));
	}
}
