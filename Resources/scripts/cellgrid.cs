using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cellgrid : MonoBehaviour
{
    public GameObject RayHitObject;
    public GameObject selected;
    public int placed_qty = 0;
    public int checked_qty = 0;
    public int covered_qty;
    public int checkno = 0;
    public int possiblemoves = 0;
    public GameObject check_break_obj;
    public  GameObject break_check_recursion_object;

    public GameObject[] check_object;
    public GameObject[] checkhex_object;
    public static Object hex;
    
    GameObject[] check_neighbour;
    public string selectedside;
    public string checkside;

    public static Vector2 firstturnplace;
    public static Vector2 check_position;
    Vector2 neighbour;

    int a;
    Vector3 center;

    public int turn;
    public int activeplayer;
    public string activeside;
    public bool possiblemove = false;

    //public Vector3 screenpoint;
    public Ray ray;
    public static float xoffs = 0.875f;
    public static float yoffs = 0.760f;

    public   float sizexmin = 12;
    public  float sizexmax = 12;
    public  float sizeymin = 10;
    public  float sizeymax = 10;
    public float centerx = 12;
    public float centery = 10;
    public float addzoomx;
    public float addzoomy;




    public Camera camera2;
    public Camera camera3;
    public Camera camera4;

    public bool game_ended = false;

    public GameObject checkObj;
    public  int placesq;
    public static int hand;
    public bool checkposmove;

    public GameObject wqueen;
    public GameObject bqueen;

    // Use this for initialization
    void Start()
    {

        hex = Resources.Load("prefabs/hex");

        activeplayer = 1;
        turn = 1;
        if (activeplayer == 1)
        {
            activeside = "white";

        }
        else
        {
            activeside = "black";

        }


    }

    // Update is called once per frame
    void Update()
    {





        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if(turn > 2)
            { 
                if(hit.collider == null)


                if (Camera.main.GetComponent<camera>().drag_complete == false && turn >2   && hit.collider == null)
                {
                    reclick();
                }
            }

        }




        if (Input.GetMouseButtonDown(0))
             {
          
            ray = Camera.main.GetComponent<camera>().ray;
            checkraycast();
             }



       
    }

    void checkraycast()

    {
        
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null)
        {
        
             RayHitObject = hit.collider.gameObject;
         

            if (RayHitObject.tag == "unit")
            {
             
                if (selected != null)
                {

                    selected.GetComponent<unitplaced>().possiblemove = false;
                    selected.GetComponent<unitplaced>().selected = false;

                    selected.transform.localScale = new Vector3(1, 1, 1);
                    selected = null;
                    hide_grid();
                    try { GameObject.Find("camera5").GetComponent<covered>().show_covered = false; } catch { }
                }



                if (activeplayer == RayHitObject.GetComponentInParent<baseside>().player)
                {
                  
                    selectedside = RayHitObject.GetComponentInParent<baseside>().side;





                    hide_grid();


                    selected = RayHitObject;
                    selected.GetComponent<unitplaced>().selected = true;

                    sizemap();
                    if (Mathf.Max(addzoomx, addzoomy) <= 1.4f)
                    {
                        Camera.main.GetComponent<camera>().centering = true;
                    }
                    else { Camera.main.GetComponent<camera>().centering = false; }
                    








                }

            }



            if (RayHitObject.tag == "turnplace" & selected != null)
            {
                selected.transform.GetComponent<unitplaced>().placed = true;
                selected.GetComponent<unitplaced>().selected = false;
                hide_grid();
                GameObject.Find("cancel").GetComponent<cancel>().obj = selected;
                GameObject.Find("cancel").GetComponent<cancel>().coordinates = selected.transform.position;
                selected.transform.position = RayHitObject.transform.position;
                selected.transform.localScale = new Vector3(1, 1, 1);
                try { GameObject.Find("camera5").GetComponent<covered>().show_covered = false; } catch { }
                hide_grid();
                next_turn();
                


            }





        }
        else
        {

            //    reclick();


        }

    
    }

    void reclick()
    {

        if (selected != null)
        {
         
            selected.GetComponent<unitplaced>().selected = false;
            //  print(selected + "change unit / reclick");
            selected.transform.localScale = new Vector3(1, 1, 1);
            selected = null;
            hide_grid();


        }
        try { GameObject.Find("camera5").GetComponent<covered>().show_covered = false; } catch { }


    }

    void hide_grid()
    {

        possiblemove = false;
        check_object = GameObject.FindGameObjectsWithTag("turnplace");
            foreach (GameObject check in check_object)
            {

                Destroy(check);
            }
        
    }


    Vector3 createCircle(Vector3 center, float radius, int a)
    {
        float ang = a;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }



    void sizemap()
    {
        check_object = GameObject.FindGameObjectsWithTag("unit");

        sizexmax = centerx;
        sizexmin = centerx;
        sizeymax = centery;
        sizeymin = centery;


        foreach (GameObject check in check_object)
        {

            if (check.transform.GetComponent<unitplaced>().placed == true)
            {
               if (check.transform.position.x > sizexmax) { sizexmax = check.transform.position.x; }
               if (check.transform.position.x < sizexmin) { sizexmin = check.transform.position.x; }
               if (check.transform.position.y > sizeymax) { sizeymax = check.transform.position.y; }
               if (check.transform.position.y < sizeymin) { sizeymin = check.transform.position.y; }
            }
        
        }

             centerx = (sizexmax + sizexmin)/2;
             centery = (sizeymin + sizeymax)/2;
             addzoomx =  Mathf.Max(Mathf.Abs(sizexmax) - Mathf.Abs(sizexmin) - 3.5f ,0);
             addzoomy =  Mathf.Max(Mathf.Abs(sizeymax) - Mathf.Abs(sizeymin) - 1.8f, 0);
}
    //проверка на разрыв (определение соседнего юнита)

    public void next_turn()
    {
        try { StopCoroutine("checkposmoves"); } catch { }
        wqueen.GetComponent<queen>().checkgameover = true;
        bqueen.GetComponent<queen>().checkgameover = true;
        selected = null;
        
        selectedside = null;
            checkside = null;
            activeplayer++;

        if (activeplayer > 2)
            {
                activeplayer = 1;
                turn++;
            }

        if (activeplayer < 1)
        {
            activeplayer = 2;
        //    turn++;
        }

        if (activeplayer == 1)
        {
            activeside = "white";

        }
        else
        {
            activeside = "black";

        }
      


          if(turn > 2) { possiblemoves = 0; checkposmove = true; StartCoroutine(stop_checkposmoves()); StartCoroutine(checkposmoves());  } // 

    }



    IEnumerator checkposmoves()
    {
     

        yield return new WaitForSeconds(0.01f);
        check_object = GameObject.FindGameObjectsWithTag("unit");


        int a = 0;
        foreach (GameObject check in check_object) // проверка возможных ходов
        {
           
            
            checkside = check.GetComponentInParent<baseside>().side;



            hide_grid();
            if (activeside == checkside)
            {
                if (check.GetComponent<unitplaced>().covered == false)
                { 
                check.GetComponent<unitplaced>().selected = true;
                }
                a++;

            }



        }

        
        yield return new WaitUntil(() => a >=12);

        yield return new WaitForSeconds(0.2f);
        
        hide_grid();
        check_object = GameObject.FindGameObjectsWithTag("unit");

   
 
        
        foreach (GameObject check in check_object) // проверка возможных ходов
        {


            checkside = check.GetComponentInParent<baseside>().side;


   

            if (activeside == checkside)
            {
   //             print(checkside + " " + check.gameObject + " " + check.GetComponent<unitplaced>().possiblemoves + " possiblemoves");
                possiblemoves += check.GetComponent<unitplaced>().possiblemoves;
                check.GetComponent<unitplaced>().selected = false;
                
            }
        }

        yield return new WaitForSeconds(0.2f);




   
        if (possiblemoves == 0)
        {
            print("there aren't any moves! next turn.");
            next_turn();

        }
        reset_count_places();

        checkposmove = false;
        StopAllCoroutines();

    }

    IEnumerator stop_checkposmoves()
    {
        while (true) {
         //  yield return new WaitForEndOfFrame();
       
        yield return possiblemove;
         
            if (possiblemoves > 0)
        {
                StopAllCoroutines();
   //             print(possiblemoves + "moves / stop coroot");

                
                reset_count_places();
                
                
                checkposmove = false;
            
  //              print("after stop check");
                hide_grid();
            }
        }
        
        // yield return 0;
    }








    void reset_count_places()
    {
        checkhex_object = GameObject.FindGameObjectsWithTag("unit");


        foreach (GameObject checks in checkhex_object)
        {
            checks.GetComponent<unitplaced>().selected = false;

            checks.GetComponent<unitplaced>().possiblemoves = 0;
            

        }
 


    }




}
