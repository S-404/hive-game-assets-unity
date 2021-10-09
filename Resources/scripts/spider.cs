using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider : MonoBehaviour {

    GameObject[] check_object;
    public string checkside;
    public static float xoffs = 0.875f;
    public static float yoffs = 0.760f;
    public static Object hex;
    int a;
    int resultthrough;
    int resultslip;
    int resultthis;
    int contactthistotal;
    Vector3 center;
    RaycastHit2D hit;
    Vector3 pos;


    int step = 1;

    int checkq;
    
    GameObject check_obj_through;


    // Use this for initializationss
    void Start()
    {
        hex = Resources.Load("prefabs/hex");
    }

    // Update is called once per frame
    void Update()
    {

        try
        { if (gameObject.GetComponent<unitplaced>().can_move == true) { move(); } }
        catch { }
    }



    void move()
    {
        step = 1;
        gameObject.GetComponent<unitplaced>().can_move = false;
        gameObject.GetComponent<unitplaced>().selected = false;
        //   print(GameObject.Find("map").GetComponent<cellgrid>().selected + " - cellgrid //  + " + gameObject + " generate poss moves");
        //   gameObject.GetComponent<unitplaced>().hidegridcell = true;
        RaycastHit2D hit;

        center = transform.position;
        for (int i = 0; i < 6; i++)
        {
            a = i * 60;
            Vector3 pos = createCircle(center, xoffs, a + 30);
            hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider == null)
            {


                check_through();

                if (resultthrough > 0 && resultslip > 0)
                {

                    Instantiate(hex, pos, Quaternion.identity);


                }
            }

        }


        check_object = GameObject.FindGameObjectsWithTag("turnplace");
        foreach (GameObject check in check_object)
        {

            check.GetComponent<hex>().marked = true;
            check.GetComponent<hex>().step = step;



        }

       nextstep2();
        delete_excess();
        contactthistotal = 0;
   
    }



    void nextstep2()
    {
        step = 2;

          
            check_object = GameObject.FindGameObjectsWithTag("turnplace");
            
            foreach (GameObject check in check_object)
            {


                if (check.GetComponent<hex>().step == 1) {

                  
                    center = check.transform.position;
                    for (int i = 0; i < 6; i++)
                    {

                        a = i * 60;
                        Vector3 pos = createCircle(center, xoffs, a + 30);
                        hit = Physics2D.Raycast(pos, Vector2.zero);

                        if (hit.collider == null || (hit.collider.tag == "turnplace" ))  
                    {



                            nextstep_check_through();
                            //    

                            if (resultthrough > 0 && resultslip > 0 && resultthis == 0) // 
                            {
       
                                Instantiate(hex, pos, Quaternion.identity);
                                hit = Physics2D.Raycast(pos, Vector2.zero);
                                hit.collider.gameObject.GetComponent<hex>().chain = check.transform.position;


                            }
 

                        }

                    }

                }



       




            markhex();

     


            }





        nextstep3();




    }

    void nextstep3()
    {
        step = 3;

     
        check_object = GameObject.FindGameObjectsWithTag("turnplace");

        foreach (GameObject check in check_object)
        {


            if (check.GetComponent<hex>().step == 2)
            {

       
                center = check.transform.position;
                for (int i = 0; i < 6; i++)
                {

                    a = i * 60;
                    Vector3 pos = createCircle(center, xoffs, a + 30);
                    hit = Physics2D.Raycast(pos, Vector2.zero);

                    if (hit.collider == null || (hit.collider.tag == "turnplace" && hit.collider.transform.position != check.GetComponent<hex>().chain)) // || (hit.collider.tag == "turnplace" && hit.collider.transform.position != checked_obj.transform.position
                    {



                        nextstep_check_through();
                        //    

                        if (resultthrough > 0 && resultslip > 0 && resultthis == 0) // 
                        {
                            if (GameObject.Find("map").GetComponent<cellgrid>().checkposmove == false)
                            {
                                Instantiate(hex, pos, Quaternion.identity);
                            }
                           
                            gameObject.GetComponent<unitplaced>().possiblemoves++;
                 


                        }


                    }

                }

            }








            markhex();

          


        }



    




    }


    void markhex()
    {
        check_object = GameObject.FindGameObjectsWithTag("turnplace");
        foreach (GameObject check in check_object)
        {
            if (check.GetComponent<hex>().step == 0)
            {
                check.GetComponent<hex>().step = step;
            }
        }
    }




    

    void delete_excess()
    {


        check_object = GameObject.FindGameObjectsWithTag("turnplace");
        foreach (GameObject check in check_object)
        {
            if (check.GetComponent<hex>().step != 3)
            {
                Destroy(check);
            }
           
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

    void check_through()
    {
        resultslip = 0;
        resultthrough = 0;




        Vector3 poscheck = createCircle(center, xoffs, a + 30 - 60);
        hit = Physics2D.Raycast(poscheck, Vector2.zero);

        if (hit.collider == null || hit.collider.tag == "turnplace")
        {
            resultthrough++;
        }

        if (hit.collider != null && hit.collider.tag != "turnplace")
        {

            resultslip++;

        }


        poscheck = createCircle(center, xoffs, a + 30 + 60);
        hit = Physics2D.Raycast(poscheck, Vector2.zero);

        if (hit.collider == null || hit.collider.tag == "turnplace")
        {
            resultthrough++;
        }
        if (hit.collider != null && hit.collider.tag != "turnplace")
        {
            resultslip++;
        }




    }


    void nextstep_check_through()
    {
   //     print("start next_check_through");
        resultslip = 0;
        resultthrough = 0;
        resultthis = 0;



        Vector3 poscheck = createCircle(center, xoffs, a + 30 - 60);
        hit = Physics2D.Raycast(poscheck, Vector2.zero);

        if (hit.collider == null || hit.collider.tag == "turnplace" || hit.collider.gameObject == gameObject )
        {
            resultthrough++;
        }

        if (hit.collider != null && hit.collider.tag != "turnplace" && hit.collider.gameObject != gameObject) //
        {

            resultslip++;

        }

        try
        {
            if (hit.collider.gameObject == gameObject )
            {

                resultthis++;
                contactthistotal++;
            }
        }
        catch { }

        poscheck = createCircle(center, xoffs, a + 30 + 60);
        hit = Physics2D.Raycast(poscheck, Vector2.zero);

        if (hit.collider == null || hit.collider.tag == "turnplace" || hit.collider.gameObject == gameObject)
        {
            resultthrough++;
        }
        if (hit.collider != null && hit.collider.tag != "turnplace" && hit.collider.gameObject != gameObject) // 
        {
            resultslip++;
        }

        try
        {
            if (hit.collider.gameObject == gameObject  )
            {
                resultthis++;
                contactthistotal++;
            }

        }
        catch { }

        if (resultslip>0 && resultthis > 0) { resultthis = 0; }


    }







}
