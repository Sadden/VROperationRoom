
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class ClickManager : MonoBehaviour
{
	public Text text;
	
	public TextMesh screenText;

    RaycastHit hitInfo = new RaycastHit();
    //properties
    public float MaxTimeToClick { get { return _maxTimeToClick; } set { _maxTimeToClick = value; } }
    public float MinTimeToClick { get { return _minTimeToClick; } set { _minTimeToClick = value; } }
    public bool IsDebug { get { return _Isdebug; } set { _Isdebug = value; } }

    //property variables
    private float _maxTimeToClick = 0.60f;
    private float _minTimeToClick = 0.05f;
    private bool _Isdebug = false;

    //private variables to keep track
    private float _minCurrentTime;
    private float _maxCurrentTime;
	
	private int clickCount=0;
	
	public static PlayerInteraction player;

	void Start () {
        player = Object.FindObjectOfType(typeof(PlayerInteraction)) as PlayerInteraction;
    }
    void Update()
    {
		
		Ray ray = new Ray();
		RaycastHit hitInfo;
		ray.origin = transform.position;
		ray.direction = transform.forward;
		Physics.Raycast(ray, out hitInfo);
		//left click
        if (Input.GetMouseButtonDown(0))
        {
			bool doubleClicked = DoubleClick();//ony left clicks can triger double click for now
			if (hitInfo.collider != null)
			{
				ItemInteraction i = hitInfo.collider.GetComponent<ItemInteraction>();
				if (i != null)
				{
					if (doubleClicked) { text.text=" "+i.doubleClicked(); clickCount++;screenText.text="You interacted with items "+clickCount+" times";}
					else { text.text="That's "+i.clicked(); }
				}
				else
				{
					string n=hitInfo.collider.gameObject.name;
					if(System.String.IsNullOrEmpty(n)){text.text="clicked on an unnamed object";}
					else{text.text="clicked on "+n;}
					if (doubleClicked)
					{
						player.nothingDoubleClicked();
					}
				}
			}
			else
			{
				text.text="clicked on nothing";
				if (doubleClicked) {
					player.nothingDoubleClicked();
				}
			}
        }
		//right click to move
		if(Input.GetMouseButtonDown(1)){
			//tell the player to move
			player.moveTo(hitInfo.point);
		}
    }

    public bool DoubleClick()
    {
        if (Time.time >= _minCurrentTime && Time.time <= _maxCurrentTime)
        {
            if (_Isdebug)
                Debug.Log("Double Click");
            _minCurrentTime = 0;
            _maxCurrentTime = 0;
            return true;
        }
        _minCurrentTime = Time.time + MinTimeToClick; _maxCurrentTime = Time.time + MaxTimeToClick;
        return false;
    }
}