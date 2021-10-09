using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hex : MonoBehaviour {

    public bool marked = false;
    public bool check = false;
    public int step = 0;
    public Vector3 chain;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale =  Vector3.Lerp (transform.localScale, new Vector3 (0.95f, 0.95f, 1), Time.deltaTime * 15f);
    }
}
