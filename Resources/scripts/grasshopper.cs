using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grasshopper : MonoBehaviour {
    GameObject[] check_object;
    public string checkside;
    public static float xoffs = 0.875f;
    public static float yoffs = 0.760f;
    public static Object hex;



    int a;
    Vector3 center;
    RaycastHit2D hit;
    // Use this for initialization
    void Start()
    {
        hex = Resources.Load("prefabs/hex");
    }

    // Update is called once per frame
    void Update()
    {

      try
            { if (gameObject.GetComponent<unitplaced>().can_move == true) { move();  } }
            catch { }
    }





    void move()
    {
        gameObject.GetComponent<unitplaced>().can_move = false;
        gameObject.GetComponent<unitplaced>().selected = false;

        Vector2 check_position;

     

        checkside = gameObject.GetComponentInParent<baseside>().side;




        RaycastHit2D hit;


        center = transform.position;
        for (int i = 0; i < 6; i++)
        {
            a = i * 60;
            Vector3 pos = createCircle(center, xoffs, a + 30);
            hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null)
            {
                check_position = pos;
                do
                {
                    check_position = createCircle(check_position, xoffs, a + 30);
                    hit = Physics2D.Raycast(check_position, Vector2.zero);

                }
                while (hit.collider != null);
                if (GameObject.Find("map").GetComponent<cellgrid>().checkposmove == false)
                {
                    Instantiate(hex, check_position, Quaternion.identity);
                }

               
                gameObject.GetComponent<unitplaced>().possiblemoves++;
         
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




}
