using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {


    public Ray ray;

    public Camera camera2;
    public Camera camera3;
    public Camera camera4;
    public float movespeed = 5;

    public static bool zoomout;

    public static float orthoffset = 0.1f; //for zooming offset
    public static int zoominlimit = 2;
    public static int zoomoutlimit = 5;
    public static float addzoom;

    public static float centerx;
    public static float centery;
    public float maxx;

    //for camera drag move
    public static float screenpointy;
    public static float screenpointx;
    public static Vector3 initial;
    public static Vector3 dif;
    public static Vector3 final;

    public static float sizex;
    public static float sizey;

    private Vector3 Origin;
    public static Vector3 Diference;
    public bool Drag = false;
    public bool drag_complete = false;
    public bool centering;

    public bool endgame = false;
    public GameObject endgameObj;
    // Use this for initialization
    void Start () {
   
	}
	
	// Update is called once per frame
	void Update () {
        
        centerx = GameObject.Find("map").GetComponent<cellgrid>().centerx;
        centery = GameObject.Find("map").GetComponent<cellgrid>().centery;
        addzoom = Mathf.Max(GameObject.Find("map").GetComponent<cellgrid>().addzoomx, GameObject.Find("map").GetComponent<cellgrid>().addzoomy);
        sizex = GameObject.Find("map").GetComponent<cellgrid>().sizexmax - GameObject.Find("map").GetComponent<cellgrid>().sizexmin;
        sizey = GameObject.Find("map").GetComponent<cellgrid>().sizeymax - GameObject.Find("map").GetComponent<cellgrid>().sizeymin;

        if (endgame == true)
        {
            centering = false;
            Vector3 position = endgameObj.transform.position;
            Vector3 camposition = transform.position;
            camposition = Vector3.Lerp(camposition, new Vector3(position.x, position.y, camposition.z), Time.deltaTime *0.5f);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 1.6f, 0.1f);
        }

        if (centering == true)
        { 

            transform.position = Vector3.Lerp(transform.position,new Vector3(centerx, centery, -27),Time.deltaTime * movespeed);
            
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 2.15f + addzoom/2,0.1f);
            
          
        }

        screenpointx = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
        screenpointy = Camera.main.ScreenToViewportPoint(Input.mousePosition).y;
        if (screenpointx > 1)
        {  ray = camera3.ScreenPointToRay(Input.mousePosition);  }
        else if (screenpointx < 0)
        {  ray = camera2.ScreenPointToRay(Input.mousePosition);  }
        else if  (screenpointy > 1)
        { ray = camera4.ScreenPointToRay(Input.mousePosition); }
        else
        {  ray = Camera.main.ScreenPointToRay(Input.mousePosition);  }

 
     





        // -------------------Code for Zooming Out------------
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            centering = false;
            if (Camera.main.orthographicSize <= zoomoutlimit)
            {

                Camera.main.orthographicSize += orthoffset*3;
            }
        }


        // ---------------Code for Zooming In------------------------
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            centering = false;
            if (Camera.main.orthographicSize >= zoominlimit)
            { Camera.main.orthographicSize -= orthoffset*3; }
        }


        // ---------------Code for pinch Zooming------------------------
       

        if (Input.touchCount == 2)
        {
            Drag = false;
            centering = false;


            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudediff = prevTouchDeltaMag - touchDeltaMag;

            Camera.main.orthographicSize += deltaMagnitudediff * 0.02f;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, zoominlimit,zoomoutlimit);














            // Vector2 touch0, touch1;
            // touch0 = Input.GetTouch(0).position;
            // touch1 = Input.GetTouch(1).position;

            //float curDist = Vector2.Distance(touch0, touch1); //current distance between finger touches
            //float prevDist = Vector2.Distance(touch0, Input.GetTouch(0).deltaPosition) - Vector2.Distance(touch1, Input.GetTouch(1).deltaPosition); //difference in previous locations using delta position

            // print("curdist: " + curDist + " / prevdist: " + prevDist);


            // // -------------------Code for Zooming Out------------
            // if ((curDist > prevDist))
            // {
            //     centering = false;
            //     if (Camera.main.orthographicSize <= zoomoutlimit)
            //     {

            //         Camera.main.orthographicSize += orthoffset;
            //     }
            // }
            // // ---------------Code for Zooming In------------------------
            // if ((curDist < prevDist))
            // {
            //     centering = false;
            //     if (Camera.main.orthographicSize >= zoominlimit)
            //     { Camera.main.orthographicSize -= orthoffset; }
            // }




        }


        //---------code for drag-n-move camera---------

        if (Input.GetMouseButtonDown(0) ) { initial = Camera.main.ScreenToViewportPoint(Input.mousePosition); }

        if (Input.GetMouseButtonUp(0)) { initial = final; }

        if (Input.GetMouseButtonDown(0))
        { drag_complete = false; }

            if (Input.GetMouseButton(0))
        {
          //  drag_complete = false;
            final = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            dif = initial - final; //<--расстояние проведенное курсором по экрану [swipe]

            Vector3 pos = Camera.main.transform.position;
            Diference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - pos;

            if(Mathf.Abs(dif.x)>0.05 || Mathf.Abs(dif.y) > 0.05) {
            if (Drag == false)
            {
                Drag = true;

                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                drag_complete = true;

            }
                

            }
            else { drag_complete = false; }
        }
        else
        {
            Drag = false;

        }



        if (Drag == true)
        {
            
            Vector3 pos = Camera.main.transform.position;

            pos.x = Origin.x - Diference.x;
            pos.y = Origin.y - Diference.y;

            float alfax = Mathf.Abs (Camera.main.orthographicSize)+sizex/2;
            float alfay = Mathf.Abs(Camera.main.orthographicSize) + sizey / 2;


            Camera.main.transform.position = new Vector3(Mathf.Clamp(pos.x, centerx - alfax, centerx +  alfax), Mathf.Clamp(pos.y, centery - alfay, centery + alfay), pos.z);

            {

            }
           

        }




    }



}













