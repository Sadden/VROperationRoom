using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateRoomController : MonoBehaviour {

	public GameObject createRoomPanel;		
	public GameObject roomLoadingPanel;		
	public Text roomName;					
	public Text roomNameHint;				
	public GameObject maxPlayerToggle;		

	private byte[] maxPlayerNum = { 2, 4 };	

	
	void OnEnable(){
		roomNameHint.text = "";	
	}

	
	public void ClickConfirmCreateRoomButton(){
		RoomOptions roomOptions=new RoomOptions();
		RectTransform toggleRectTransform = maxPlayerToggle.GetComponent<RectTransform> ();
		int childCount = toggleRectTransform.childCount;
		
		for (int i = 0; i < childCount; i++) {
			if (toggleRectTransform.GetChild (i).GetComponent<Toggle> ().isOn == true) {
				roomOptions.maxPlayers = maxPlayerNum [i];
				break;
			}
		}

		RoomInfo[] roomInfos = PhotonNetwork.GetRoomList();	
		bool isRoomNameRepeat = false;
		
		foreach (RoomInfo info in roomInfos) {
			if (roomName.text == info.name) {
				isRoomNameRepeat = true;
				break;
			}
		}
		
		if (isRoomNameRepeat) {
			roomNameHint.text = "Repeated!";
		}
		
		else {
			PhotonNetwork.CreateRoom (roomName.text, roomOptions, TypedLobby.Default);	
			createRoomPanel.SetActive (false);	
			roomLoadingPanel.SetActive (true);	
		}
	}

	
	public void ClickCancelCreateRoomButton(){
		createRoomPanel.SetActive (false);		
		roomNameHint.text = "";					
	}
}
