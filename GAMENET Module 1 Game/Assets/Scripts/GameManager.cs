using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    //The player prefab
    [SerializeField]
    private GameObject playerPrefab;


    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (playerPrefab != null)
            { 
                // spawns in a random area
                int xRandomPoint = Random.Range(-20, 20);
                int zRandomPoint = Random.Range(-20, 20);
                //Photn instatniate
                PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(xRandomPoint, 0, zRandomPoint), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " has entered the room");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(PhotonNetwork.NickName + " has entered the room " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Room has now " + PhotonNetwork.CurrentRoom.PlayerCount + "/20 players");
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("GameLauncherScene");
    }



    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

}
