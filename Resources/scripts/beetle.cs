using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beetle : MonoBehaviour {


    GameObject[] check_object;
    public string checkside;
    public static float xoffs = 0.875f;
    public static float yoffs = 0.760f;
    public static Object hex;
    int a;
    int resultthrough;
    int resultslip;
    Vector3 center;
    
    RaycastHit2D hit;
    public static Ray ray;
    public static float offs = 0.033f;
    public  float cur_offs;
    public static Vector2 beetle_coord;
    
    void Start()
    {
        hex = Resources.Load("prefabs/hex");
    }

    // Update is called once per frame
    void Update()
    {

        try { if (gameObject.tag == "clone") { remove_offset(); } } catch { }

        try
        { if (gameObject.GetComponent<unitplaced>().can_move == true) { move();  } }
        catch { }




              cur_offs = gameObject.transform.position.z;


               if (gameObject.GetComponent<unitplaced>().placed == true && gameObject.GetComponent<unitplaced>().covered == false && gameObject.tag != "clone")
                   {

                   if(cur_offs < 0)
                   { 
                   offset();
                   }
                     else { remove_offset(); }
               }



        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hitthischeck = Physics2D.GetRayIntersection(ray);


        if (Input.GetMouseButtonDown(0)) { 
        try
        {
               // print("beetlehit" + hitthischeck.collider.gameObject + " " + gameObject);
                if (hitthischeck.collider.gameObject == gameObject  && gameObject.GetComponent<unitplaced>().placed == true)
                {
                    if(cur_offs < 0)
                    {
                    GameObject.Find("Camera5").GetComponent<covered>().show = false;
                    GameObject.Find("Camera5").GetComponent<covered>().show_covered = true;
                    GameObject.Find("Camera5").GetComponent<covered>().get_from = gameObject.transform.position;
                    GameObject.Find("Camera5").GetComponent<covered>().offspoint = gameObject.transform.position.z;
                    }
                    else { GameObject.Find("Camera5").GetComponent<covered>().show_covered = false; }
                }
                
                
        }
        catch { GameObject.Find("Camera5").GetComponent<covered>().show_covered = false; }

        }

    }


           void offset()

           {
               Transform[] allChildren = GetComponentsInChildren<Transform>();

                   foreach (Transform child in allChildren)
                   {
                   
                      if (gameObject != child.gameObject)
                      { 
                   child.localPosition = new Vector3(-cur_offs, +cur_offs, child.localPosition.z);
                      }
                   }



           }

    void remove_offset()
    {

        Transform[] allChildren = GetComponentsInChildren<Transform>();

        foreach (Transform child in allChildren)
        {

            if (gameObject != child.gameObject)
            {
                child.localPosition = new Vector3(0, 0, child.localPosition.z);
            }
        }





    }



    void move()
    {
        gameObject.GetComponent<unitplaced>().can_move = false;
        gameObject.GetComponent<unitplaced>().selected = false;


       
        center = transform.position;
      //  print(center + " center bug");
        for (int i = 0; i < 6; i++)
        {
            a = i * 60;
            Vector3 pos = createCircle(center, xoffs, a + 30);
            hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider == null)
            {


                check_through();

                if ( resultslip > 0)
                {
                    if (GameObject.Find("map").GetComponent<cellgrid>().checkposmove == false)
                    {
                        Instantiate(hex, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
                    }
             
                    gameObject.GetComponent<unitplaced>().possiblemoves++;
             
                }
            }



        }

        for (int i = 0; i < 6; i++)
        {
            a = i * 60;
            Vector3 pos = createCircle(center, xoffs, a + 30);
            hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null && hit.collider.tag != "turnplace")
            {
      //          float target_offs = hit.collider.transform.position.z;
                if (GameObject.Find("map").GetComponent<cellgrid>().checkposmove == false)
                {
                    Instantiate(hex, new Vector3(pos.x, pos.y, hit.collider.transform.position.z - offs), Quaternion.identity);
                }
     
                gameObject.GetComponent<unitplaced>().possiblemoves++;

            }



        }





        check_object = GameObject.FindGameObjectsWithTag("turnplace");

        foreach (GameObject check in check_object)
        {
            if (check.transform.position.z != 0)
            {
                foreach (SpriteRenderer chcolor in check.GetComponentsInChildren<SpriteRenderer>())
                {

                    chcolor.color = new Color(0.3f, 0.3f, 0.3f, 0.4f);
                }
            }
        }


     


    }


    void check_through()
    {
        resultslip = 0;
  


        Vector3 checkpos = createCircle(center, xoffs, a + 30 - 60);
        hit = Physics2D.Raycast(checkpos, Vector2.zero);



        
        if (hit.collider != null && hit.collider.tag != "turnplace")
        {
            resultslip++;
        }




        checkpos = createCircle(center, xoffs, a + 30 + 60);
        hit = Physics2D.Raycast(checkpos, Vector2.zero);

        if (hit.collider != null && hit.collider.tag != "turnplace")
        {
            resultslip++;
        }


        if(cur_offs != 0)
        {
            resultslip++;
        }


    }








    Vector3 createCircle(Vector3 center, float radius, int a)
    {
        //Debug.Log(a);
        float ang = a;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }



}

