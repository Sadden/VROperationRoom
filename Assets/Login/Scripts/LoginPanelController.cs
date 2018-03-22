using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon;

public class LoginPanelController : PunBehaviour {

	public GameObject loginPanel;		
	public GameObject userMessage;		
	public Button backButton;			
	public GameObject lobbyPanel;		
	public GameObject roomPanel;		
	public Text username;				
	public Text connectionState;		

	//Initialization, show the corresponding panel according to the connection status of the client
	void Start () {
		//if not connected
		if (!PhotonNetwork.connected) {
			SetLoginPanelActive ();								
			username.text = PlayerPrefs.GetString ("Username");	//store the username first locally
		} 
		//If connected
		else
			SetLobbyPanelActive ();	
		connectionState.text = "";	//initialize connectionstate
	}


//#if(UNITY_EDITOR)	
	void Update(){		
		connectionState.text = PhotonNetwork.connectionStateDetailed.ToString ();
	}
//#endif

	
	public void SetLoginPanelActive(){
		loginPanel.SetActive (true);				
		userMessage.SetActive (false);				
		backButton.gameObject.SetActive (false);	
		lobbyPanel.SetActive (false);				
		if(roomPanel!=null)
			roomPanel.SetActive (false);				
	}
	
	public void SetLobbyPanelActive(){				
		loginPanel.SetActive (false);				
		userMessage.SetActive (true);				
		backButton.gameObject.SetActive (true);		
		lobbyPanel.SetActive (true);				
	}

	
	public void ClickLogInButton(){							
		SetLobbyPanelActive ();			
		//game version 1.0
		if (!PhotonNetwork.connected)						
			PhotonNetwork.ConnectUsingSettings ("1.0");		
		//if player havn't input a user name, randomly pick one for him
		if (username.text == "")							
			username.text = "guest" + Random.Range (1, 9999);
		PhotonNetwork.player.name = username.text;			
		PlayerPrefs.SetString ("Username", username.text);	
	}
	
	public void ClickExitGameButton(){
		Application.Quit ();			
	}

	
	public override void OnJoinedLobby(){
		userMessage.GetComponentInChildren<Text> ().text
		= "Welcome，" + PhotonNetwork.player.name;
	}

	
	public override void OnConnectionFail(DisconnectCause cause){
		SetLoginPanelActive ();
	}
}
