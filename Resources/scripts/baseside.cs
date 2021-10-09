using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseside : MonoBehaviour {
    public string side;
    public int player;
    public string opside;

	// Use this for initialization
	void Start () {

        if (name == "white")
        {
            side = "white";
            player = 1;
            opside = "black";
        }
        else
        {
            side = "black";
            player = 2;
            opside = "white";
        }

    
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
