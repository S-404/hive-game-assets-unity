using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitplaced : MonoBehaviour
{
    public bool placed = false;

    public bool selected = false;
    public bool can_move = false;
    public bool marked = false;
    public bool check = false;
    public bool covered = false;
    GameObject[] check_object;
    RaycastHit2D hit;




    public  bool possiblemove = false;
    public int placed_qty = 0;
    public int checked_qty = 0;
    public int covered_qty;
    public int checkno = 0;
    Vector3 center;
    int a;
    public static float xoffs = 0.875f;
    public static float yoffs = 0.760f;
    public GameObject break_check_recursion_object;
    public GameObject check_break_obj;
 
    public static Vector2 firstturnplace;

    public string checkside;
    Vector2 neighbour;
    static int result = 0;
    public static Object hex;


   public bool  chk = false;



    public int possiblemoves;


    void Start()
    {

        hex = Resources.Load("prefabs/hex");


    }
    
    void Update()
    {

        if (gameObject.transform.position.z < -29) { placed = false; }

        if (gameObject == GameObject.Find("cancel").GetComponent<cancel>().obj)
        {
           if( GameObject.Find("cancel").GetComponent<cancel>().check == true) { gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(0.9f, 0.05f, 0.05f); }
            else { gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(0.05f, 0.5f, 0.05f); }
 
            

        }
        else
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(0.24f, 0.24f, 0.24f);
        }

            if (placed == true && selected == true && possiblemove == true && covered == false) { can_move = true; }

        Vector3 pos = gameObject.transform.position;
        hit = Physics2D.Raycast(pos, Vector2.zero);
            
        if(placed == true && hit.collider.gameObject != gameObject)  {covered = true; } else {covered = false; }





//unit scaling
            
                if (GameObject.Find("map").GetComponent<cellgrid>().selected == gameObject)
                {
                    transform.localScale = new Vector3(Mathf.PingPong(Time.time * 0.15f, 0.05f) + 0.95f, Mathf.PingPong(Time.time * 0.15f, 0.05f) + 0.95f, 1);
                }
                else { chk = false; transform.localScale = new Vector3(1, 1, 1); possiblemove = false; }




        if (selected ==true && placed == true && chk == false && covered == false) { possiblemove = false; possible_moves(); chk = true; }    //   <<<<--- перенести в логику юнита
        else { if (selected == true && chk == false) { place_turnplace(); chk = true; } }

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

    void possible_moves() // проверка на возможность хода
    {
        hide_grid();


        
        if (GameObject.Find("map").GetComponent<cellgrid>().turn <= 4 && GameObject.Find("/" + GameObject.Find("map").GetComponent<cellgrid>().activeside + "/Queen Bee").GetComponent<unitplaced>().placed != true)
        {

            Debug.Log("you should place Queen Bee before you can move");
            selected = false;
            GameObject.Find("map").GetComponent<cellgrid>().selected = null;
        }
        else
        {

            break_check();//проверка на разрыв
                          //print ("placed_qty = " + placed_qty + " // " + " checked_qty = " +   checked_qty + " // "+"covered = "+ covered_qty + "checkno = " +  checkno + "// transform.position.z  = " + transform.position.z);

            if (placed_qty - 1 == checked_qty + covered_qty || transform.position.z != 0)
            { possiblemove = true;   }
            else {  selected = false; GameObject.Find("map").GetComponent<cellgrid>().selected = null; possiblemove = false;

               if( GameObject.Find("map").GetComponent<cellgrid>().checkposmove == false)
                { 
                print("this unit breaks hive, choose another unit");
                }
            }
            placed_qty = 0; checked_qty = 0; checkno = 0; covered_qty = 0;


        }






    }




    void break_check() //проверка на разрыв
    {

        RaycastHit2D hit;

        center = transform.position;
        for (int i = 0; i < 6; i++)
        {
            a = i * 60;
            Vector3 pos = createCircle(center, xoffs, a + 30);
            hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null) { break_check_recursion_object = hit.collider.gameObject; }



        }

        break_check_recursion_object.GetComponent<unitplaced>().marked = true;

        break_check_recursion();



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


    void break_check_recursion()
    {
        check_object = GameObject.FindGameObjectsWithTag("unit");
        RaycastHit2D hit;

        do
        {

            check_break_obj = null;

            foreach (GameObject check in check_object)
            {




                if (check.GetComponent<unitplaced>().check != true && check.GetComponent<unitplaced>().marked == true)
                {

                    checked_qty++;

                    check.GetComponent<unitplaced>().check = true;

                    center = check.transform.position;
                    for (int i = 0; i < 6; i++)
                    {
                        a = i * 60;
                        Vector3 pos = createCircle(center, xoffs, a + 30);
                        hit = Physics2D.Raycast(pos, Vector2.zero);
                        if (hit.collider != null && hit.collider.gameObject != gameObject) { hit.collider.gameObject.GetComponent<unitplaced>().marked = true; }



                    }

                    check_break_obj = check.gameObject;



                }




            }

        } while (check_break_obj != null);

        foreach (GameObject check in check_object)
        {
            if (check.GetComponent<unitplaced>().placed == true)
            {

                placed_qty++;

            }
            if (check.GetComponent<unitplaced>().covered == true)
            {
                covered_qty++;
            }

            check.GetComponent<unitplaced>().check = false;
            check.GetComponent<unitplaced>().marked = false;
        }


    }



    void place_turnplace()
    {

        possiblemoves = 0;

        GameObject.Find("map").GetComponent<cellgrid>().placesq = 0;

        check_object = GameObject.FindGameObjectsWithTag("unit");
        if (GameObject.Find("map").GetComponent<cellgrid>().turn == 1 && GameObject.Find("map").GetComponent<cellgrid>().activeplayer == 1) { firstturn(); }
        if (GameObject.Find("map").GetComponent<cellgrid>().turn == 1 && GameObject.Find("map").GetComponent<cellgrid>().activeplayer == 2) { secondturn(); } // принуждение поставить второму игроку юнита рядом с противником на первом ходу
        else //   для остальных ходов 
        {

            if (GameObject.Find("map").GetComponent<cellgrid>().turn == 4 && GameObject.Find("/" + GameObject.Find("map").GetComponent<cellgrid>().activeside + "/Queen Bee").GetComponent<unitplaced>().placed != true)
            {

                selected = false;
                GameObject.Find("map").GetComponent<cellgrid>().selected = GameObject.Find("/" + GameObject.Find("map").GetComponent<cellgrid>().activeside + "/Queen Bee");
                GameObject.Find("map").GetComponent<cellgrid>().selected.GetComponent<unitplaced>().selected = true;
                //   GameObject.Find("map").GetComponent<cellgrid>().selected.GetComponent<unitplaced>().selected = true;
                Debug.Log("you have to place Queen Bee");
            }


            foreach (GameObject check in check_object) //генерация возможных мест рядом с юнитами
            {
                try { check.GetComponent<unitplaced>().enabled = true; } catch { }

                try
                {
                    if (check.transform.GetComponent<unitplaced>().placed == true)
                    {

                        checkside = check.GetComponentInParent<baseside>().side;

                        if (GameObject.Find("map").GetComponent<cellgrid>().activeside == checkside) ////генерация возможных мест рядом с юнитами выбраного цвета
                        {

                            RaycastHit2D hit;

                            center = check.transform.position;
                            for (int i = 0; i < 6; i++)
                            {
                                a = i * 60;
                                Vector3 pos = createCircle(center, xoffs, a + 30);
                                hit = Physics2D.Raycast(pos, Vector2.zero);
                                if (hit.collider == null)
                                {
                                    neighbour = pos;

                                    check_neigh();

                                    if (result == 0)
                                    {
                                        if (GameObject.Find("map").GetComponent<cellgrid>().checkposmove == false)
                                        {
                                            Instantiate(hex, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
                                        }
                                        GameObject.Find("map").GetComponent<cellgrid>().possiblemoves++;
                                        possiblemoves++;
                                       

                                    }

                                }



                            }


                        }





                    }

                }
                catch { }


            }


        }
        

        return;

    }

    void check_neigh()
    {
        result = 0;
        string checkneighbourside;

        RaycastHit2D hit;
        Vector3 centerneighbour = neighbour;


        for (int i = 0; i < 6; i++)
        {
            a = i * 60;
            Vector3 posneighbour = createCircle(centerneighbour, xoffs, a + 30);
            hit = Physics2D.Raycast(posneighbour, Vector2.zero);
            if (hit.collider != null && hit.collider.tag != "turnplace")
            {
                checkneighbourside = hit.collider.GetComponentInParent<baseside>().side;
                if (GameObject.Find("map").GetComponent<cellgrid>().activeside != checkneighbourside) { result++; }
            }

        }



    }


    void secondturn()
    {

        hide_grid();


        center = firstturnplace;
        for (int i = 0; i < 6; i++)
        {
            a = i * 60;
            Vector3 pos = createCircle(center, xoffs, a + 30);
           
            Instantiate(hex, pos, Quaternion.identity);
          

        }





    }
    //условия для первого хода
    void firstturn()
    {
        hide_grid();
        firstturnplace = new Vector3(GameObject.Find("map").GetComponent<cellgrid>().sizexmin, GameObject.Find("map").GetComponent<cellgrid>().sizeymin, 0);
        Instantiate(hex, firstturnplace, Quaternion.identity);


    }


}