using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reset : MonoBehaviour {
    public Ray ray;
    public bool check = false;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (check==true)
        {

        gameObject.transform.localScale = new Vector3(Mathf.PingPong(Time.time * 0.15f, 0.05f) + 0.95f, Mathf.PingPong(Time.time * 0.15f, 0.05f) + 0.95f, 1);

 



        gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(0.9f, 0.05f, 0.05f);

        }
        else
        {
            gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.01f);



           

     
           gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(0.95f, 0.95f, 0.95f);
        }


        if(GameObject.Find("map").GetComponent<cellgrid>().game_ended == true)
        {
            check = true;
        }


        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.GetComponent<camera>().ray;
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            try
            { 

            if( hit.collider.gameObject == gameObject)
            {
                    if (check == true) { SceneManager.LoadScene("hiveproject"); } else { check = true; }

            }
                else { check = false; }


            }
            catch { }

        }
		
	}
}
