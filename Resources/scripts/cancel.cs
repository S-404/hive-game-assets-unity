using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cancel : MonoBehaviour {

    public GameObject obj;
    public Vector3 coordinates;
    static Ray ray;
    public bool check;
    static int aplayer;
    public static Object hex;
    public static bool hexcreated;
    // Use this for initialization
    void Start () {

        hex = Resources.Load("prefabs/hex");
    }
	
	// Update is called once per frame
	void Update () {
        if (check == true)
        {
           
            gameObject.transform.localScale = new Vector3(Mathf.PingPong(Time.time * 0.15f, 0.05f) + 0.95f, Mathf.PingPong(Time.time * 0.15f, 0.05f) + 0.95f, 1);

            if (hexcreated == false)
            { 
            GameObject cancelhex = (GameObject) Instantiate(hex, coordinates, Quaternion.identity);
            cancelhex.GetComponentInChildren<SpriteRenderer>().color = new Color(139, 69, 19);
            hexcreated = true;
            }


            obj.GetComponentInChildren<SpriteRenderer>().color = new Color(0.9f, 0.05f, 0.05f);

            gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(0.9f, 0.05f, 0.05f);

        }
        else
        {
            gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.01f);
            hexcreated = false;

            



            gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(0.95f, 0.95f, 0.95f);
        }


        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.GetComponent<camera>().ray;
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            try
            {

                if (hit.collider.gameObject == gameObject)
                {
                    if (check == true)
                    {
                        if (obj != null) { 
                            obj.transform.position = coordinates;


                          




                          //  int activeplayer =  GameObject.Find("map").GetComponent<cellgrid>().activeplayer;

                                GameObject.Find("map").GetComponent<cellgrid>().turn--;
                                GameObject.Find("map").GetComponent<cellgrid>().activeplayer--;
                                

                                GameObject.Find("map").GetComponent<cellgrid>().next_turn();









                            obj = null;
                            check = false;
                            GameObject.Find("map").GetComponent<color>().colors();
                        }

                    } else { if (obj != null) { check = true; } }

                }
                else { check = false; }


            }
            catch { }

        }

      
	}
}
