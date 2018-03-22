
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteraction : MonoBehaviour {

    public class Effect { }

    public string description;
    public string type;
	public bool moveable=true;

    public Dictionary<string, Effect> effects;

    public static PlayerInteraction player;

	// Use this for initialization
	void Start () {
        player = Object.FindObjectOfType(typeof(PlayerInteraction)) as PlayerInteraction;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public System.String clicked()
    {
        if (description.Length > 0) { return description; }
        else { return  gameObject.name; }
    }

    public System.String doubleClicked()
    {
        if(this.moveable){
			if(player.heldObject==this.gameObject){
				player.heldObject = null;return "Dropped "+this.clicked();
			}
			else{
				player.heldObject = this.gameObject;return "Picked up "+this.clicked();
			}
			
		}
		else{return "Can't pick up "+this.clicked();}
		
    }
}
