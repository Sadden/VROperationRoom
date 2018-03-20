using System.Collections;
using Photon;
using UnityEngine;

public class PhotonConnection : PunBehaviour {
    string temp;

	
	void Start () {
        temp = PhotonNetwork.connectionStateDetailed.ToString();
        Debug.Log(temp);

        PhotonNetwork.ConnectUsingSettings("1.0"); //connect photon server
	}
	
	
	void Update () {
		if(temp!=PhotonNetwork.connectionStateDetailed.ToString())
        {
            temp = PhotonNetwork.connectionStateDetailed.ToString();
            Debug.Log(temp);
        }
	}

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();//run default lobby when connected to master server.
    }
    public override void OnJoinedLobby()
    {
        PhotonNetwork.CreateRoom("");//create room and join room automatically when join lobby
    }
}
