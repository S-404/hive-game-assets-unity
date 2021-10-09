using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnc : MonoBehaviour {

	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {
		
        if(gameObject.GetComponentInParent<baseside>().side == GameObject.Find("map").GetComponent<cellgrid>().activeside)
        {
            if(GameObject.Find("map").GetComponent<cellgrid>().selected == null && GameObject.Find("reset").GetComponent<reset>().check != true) { 
            gameObject.transform.localScale = new Vector3(Mathf.PingPong(Time.time * 0.15f, 0.05f) + 0.7f, Mathf.PingPong(Time.time * 0.15f, 0.05f) + 0.7f, 1);
                
            }
        }
        else
        {
            gameObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }
	}
}
