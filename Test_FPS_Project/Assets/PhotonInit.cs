using UnityEngine;
using System.Collections;

public class PhotonInit : MonoBehaviour {
    public string version = "v1.0";

    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(version);
    }

    void OnJoinedLobby()
    {
        Debug.Log("Entered Lobby");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("No rooms");
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(null, ro, TypedLobby.Default);
    }

    void OnJoinedRoom()
    {
        Debug.Log("Enter Room");
        CreatePlayer();
    }

    void CreatePlayer()
    {
        PhotonNetwork.Instantiate("Player", new Vector3(-48.99288f, 15.75f, -15.52526f), Quaternion.identity, 0);
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
}
