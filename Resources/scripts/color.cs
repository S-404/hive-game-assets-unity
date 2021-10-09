using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color : MonoBehaviour
{
    public GameObject[] check_object;
    // Use this for initialization
    void Start()
    {



        colors();


        //   print("start color");





        /*       if (transform.name == "white")
                 { 


                     foreach (SpriteRenderer c in GetComponentsInChildren<SpriteRenderer>())
                     {

                        // c.material.color = Color.white;
                         c.color = Color.white;

                     }

                   print("color worked");
          */





        //    print("end color");


    }

    public void colors()
    {
        check_object = GameObject.FindGameObjectsWithTag("swicher");

        foreach (GameObject check in check_object)
        {
            if (check.transform.parent.parent.name == "white")
            {
                check.GetComponent<SpriteRenderer>().color = new Color(0.85f, 0.85f, 0.85f);

            }
            else
            {
                check.GetComponent<SpriteRenderer>().color = new Color(0.05f, 0.05f, 0.05f);
            }


        }

        check_object = GameObject.FindGameObjectsWithTag("pic");

        foreach (GameObject check in check_object)
        {
            if (check.transform.parent.parent.name == "white")
            {
                check.GetComponent<SpriteRenderer>().color = new Color(0.05f, 0.05f, 0.05f);
            }
            else
            {
                check.GetComponent<SpriteRenderer>().color = new Color(0.85f, 0.85f, 0.85f);
            }


        }


        check_object = GameObject.FindGameObjectsWithTag("turnswicher");

        foreach (GameObject check in check_object)
        {
            if (check.transform.parent.parent.name == "white")
            {
                check.GetComponent<SpriteRenderer>().color = new Color(0.85f, 0.85f, 0.85f);

            }
            else
            {
                check.GetComponent<SpriteRenderer>().color = new Color(0.05f, 0.05f, 0.05f);
            }


        }

    }
}