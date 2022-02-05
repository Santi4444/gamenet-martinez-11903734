using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LaunchManager: MonoBehaviourPunCallbacks
{

    public GameObject EnterGamePanel;

    public GameObject ConnectionStatusPanel;

    public GameObject LobbyPanel;

     void Awake()
    {
        //Every player will load the scene the master client has loaded
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        //Makes it visible
        EnterGamePanel.SetActive(true);
        //unchecks and make it invisible
        ConnectionStatusPanel.SetActive(false);

        LobbyPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        //When connected to server it does this who is it
        Debug.Log( PhotonNetwork.NickName + "  connected to Photon Servers");

        //Removes the connecting.. panel
        ConnectionStatusPanel.SetActive(false);

        //Turns on Lobby Panel
        LobbyPanel.SetActive(true);
    }

    public override void OnConnected()
    {
        //If connected to internet display this
        Debug.Log("connected to Internet");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //returns a message when you fail to join a random room
        Debug.LogWarning(message);

        //When you fail to enter the room we create a random room for you 
        CreateAndJoinRoom();
    }


    public void ConnectToPhotonServer()
    {

        if (!PhotonNetwork.IsConnected)
        {
            //Connects to Photon Server
            PhotonNetwork.ConnectUsingSettings();

   
            ConnectionStatusPanel.SetActive(true);
            EnterGamePanel.SetActive(false);
        }
    
    }
    public void JoinRandomRoom()
    {
        //Join a random room
        PhotonNetwork.JoinRandomRoom();
    }


    private void CreateAndJoinRoom()
    {
        //Creates room name
        string randomRoomName = "Room " + Random.Range(0, 10000);
        
        //Room stats
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 20;

        //The actual room/lobby
        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);

    }

    public override void OnJoinedRoom()
    {
        //When you enter a room it says this
        Debug.Log(PhotonNetwork.NickName + " has entered " + PhotonNetwork.CurrentRoom.Name);
        //Load scene with name
        PhotonNetwork.LoadLevel("GameScene");
    
    
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " has entered room " + PhotonNetwork.CurrentRoom.Name + ". Room has now " + PhotonNetwork.CurrentRoom.PlayerCount 
            + " players");
    }
}
