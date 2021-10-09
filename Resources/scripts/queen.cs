using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class queen : MonoBehaviour {

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
     int result;
    int G_O_result;
    public bool checkgameover = false;


    void Start() {
        hex = Resources.Load("prefabs/hex");
 
        
    }

    // Update is called once per frame
    void Update()
     {

            try
            { if (gameObject.GetComponent<unitplaced>().can_move == true) { move();  } }
            catch { }


        try
        { if (gameObject.GetComponent<unitplaced>().placed == true && checkgameover == true) { check_gameover(); checkgameover = false; } }
        catch { }
    }
  


    void move()
    {
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
                    pos = createCircle(center, xoffs, a + 30);
                    if (GameObject.Find("map").GetComponent<cellgrid>().checkposmove == false)
                    {
                        Instantiate(hex, pos, Quaternion.identity);
                    }
                  
                    gameObject.GetComponent<unitplaced>().possiblemoves++;
                  
                }
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

        Vector3 pos = createCircle(center, xoffs, a + 30 - 60);
        hit = Physics2D.Raycast(pos, Vector2.zero);
        if (hit.collider == null || hit.collider.tag == "turnplace")
        {
            resultthrough++;
        }

        if(hit.collider != null && hit.collider.tag != "turnplace")
        {
            resultslip++;
        }
        
        pos = createCircle(center, xoffs, a + 30 + 60);
        hit = Physics2D.Raycast(pos, Vector2.zero);
        if (hit.collider == null || hit.collider.tag == "turnplace")
        {
            resultthrough++;
        }
        if (hit.collider != null && hit.collider.tag != "turnplace")
        {
            resultslip++;
        }


    }


   void check_gameover()
    {
        G_O_result = 0;
        



        Vector3 center = gameObject.transform.position;


        for (int i = 0; i< 6; i++)
        {
            a = i* 60;
            Vector3 pos = createCircle(center, xoffs, a + 30);
            hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null && hit.collider.tag != "turnplace")
            {
                G_O_result++; 
            }

        }
        if (G_O_result == 6)
        {
            Vector3 pos = gameObject.transform.position;
            hit = Physics2D.Raycast(pos, Vector2.zero);

            print(gameObject.GetComponentInParent<baseside>().side + " Queen Bee is blocked! " + gameObject.GetComponentInParent<baseside>().opside + " WIN!");
            gameObject.GetComponent<unitplaced>().placed = false;

            Camera.main.GetComponent<camera>().endgameObj = gameObject;
            Camera.main.GetComponent<camera>().endgame = true;
        


            Transform[] allChildren = GetComponentsInChildren<Transform>();
            
            foreach (Transform child in allChildren)
            {

                if (child.gameObject.tag == "pic")
                {
                    child.GetComponent<SpriteRenderer>().color = new Color(0.95f, 0.05f, 0.05f);
                }
            }

            GameObject.Find("reset").GetComponent<reset>().check = true;
            GameObject.Find("map").GetComponent<cellgrid>().activeplayer = 0;
            GameObject.Find("map").GetComponent<cellgrid>().game_ended = true;





        }
        
    }



}


      





