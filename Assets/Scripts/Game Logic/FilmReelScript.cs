using UnityEngine;
using System.Collections;

public class FilmReelScript : MonoBehaviour {
	///public GameObject prefab;
	//public Transform target;
	
	private int maxCount = 5;
	private int currentCount = 1;
	private float speed = 3.0f;
	void Start () {
		 
	}
	

	void Update () {
	for(int i =0; i <= maxCount; i++){
			Vector3 pos = new Vector3(this.transform.position.x , this.transform.position.y, this.transform.position.z+3); 
			Instantiate(this.gameObject,pos,Quaternion.identity);
			currentCount++;}
		float amtToMove = speed * Time.deltaTime;
		this.transform.Translate(Vector3.back * amtToMove, Space.World);
	
	
	}
}