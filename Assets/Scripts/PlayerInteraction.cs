using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : ItemInteraction {

    public GameObject heldObject;
	public UnityEngine.AI.NavMeshAgent playerAgent;

    RaycastHit hitInfo = new RaycastHit();
    

    void Update()
    {

        if (heldObject) { heldObject.transform.position = transform.position + transform.forward; }
    }
	public void moveTo( Vector3 point){
			playerAgent.destination=point;
	}
    public void nothingDoubleClicked()
    {
        heldObject = null;
    }
}
