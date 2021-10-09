using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class covered : MonoBehaviour
{
    public bool show_covered = false;
    public Camera camcov;
    public GameObject[] check_object;

    public Vector3 camposition;
    public Vector3 get_from;
    public float offspoint;
    public GameObject clone;

    public bool show = false;

    // Use this for initialization
    void Start()
    {

        camposition = transform.position;

    }
    private void LateUpdate()
    {

    }
    // Update is called once per frame
    void Update()
    {


        try { if (show == false) { remove_clones(); camcov.rect = new Rect(new Vector2(0.8f, 0), new Vector2(0, 0.92f)); } } catch { }

        if (Input.GetMouseButtonUp(0))

        {
            if (show_covered == true && get_from.x != 0) { show_covered_units(); camcov.rect = new Rect(new Vector2(0.8f, 0), new Vector2(0.1f, 0.92f)); show_covered = false; show = true; }
            else { remove_clones(); camcov.rect = new Rect(new Vector2(0.8f, 0), new Vector2(0, 0.92f)); }
        }

  



    }

    void show_covered_units()
    {
        float offs = offspoint;
        Vector3 get_from_point = new Vector3 (get_from.x, get_from.y , 0);
        check_object = GameObject.FindGameObjectsWithTag("unit");

        float n = -1f;
        do
              {
                 foreach (GameObject check in check_object)
                 {

                    if (check.gameObject.transform.position == get_from_point)
                    {

                         
                            clone = check.gameObject;

                        GameObject clone_obj = Instantiate(clone, new Vector3(camposition.x, camposition.y + n  , camposition.z +3 - offs), Quaternion.identity);
                        clone_obj.tag = "clone";
                        try { clone_obj.GetComponent<beetle>().cur_offs = 0; } catch { }
                        


                    }


                 }

                         get_from_point = new Vector3(get_from_point.x , get_from_point.y, get_from_point.z - 0.033f);

            n= n + 0.8f;
            offs = offs + 0.033f;
            } while (get_from_point.z > -0.31f);

    }


    void remove_clones()
    {
        check_object = GameObject.FindGameObjectsWithTag("clone");
        foreach (GameObject check in check_object)
        {
            Destroy(check);
        }
    }

}