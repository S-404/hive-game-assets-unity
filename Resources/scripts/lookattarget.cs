using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookattarget : MonoBehaviour
{

    public Transform target;

  //  public Transform meeple;
  //  public GameObject grandChild;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Assigns the transform of the first child of the Game Object this script is attached to.
       // meeple = this.gameObject.transform.GetChild(0);

        //Assigns the first child of the first child of the Game Object this script is attached to.
       // grandChild = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;

      //  Vector3 toTargetVector = target.position - transform.position;
     //   float zRotation = Mathf.Atan2(toTargetVector.y, toTargetVector.x) * Mathf.Rad2Deg - 90.0f;
      //  transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));
        
    }
}
