using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("Start Panel")]
    public TMP_Text nickname;

    [Header("Room Panel")]
    GameObject[] players;

    [Header("Cannot Find Room Panel")]
    public GameObject cannotFindRoomPanel;

    private void Awake()
    {
        var obj = FindObjectsOfType<NetworkManager>();

        if (obj.Length == 1)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);

        PhotonNetwork.NickName = PlayerPrefs.GetString("PlayerID", "");
        if (PhotonNetwork.NickName == null )
        {
            PhotonNetwork.NickName = "temporaryName";
        }
        nickname.text = PhotonNetwork.NickName;
    }

    string GetMasterClientNickname()
    {
        Player masterPlayer;
        PhotonNetwork.CurrentRoom.Players.TryGetValue(PhotonNetwork.CurrentRoom.MasterClientId, out masterPlayer);
        return masterPlayer.NickName;
    }

    #region          
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
        SceneManager.LoadSceneAsync(1);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        print(PhotonNetwork.LocalPlayer.NickName + " joined the lobby.");
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadSceneAsync(0);
    }
    #endregion

    #region room list
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // if UI for a room list is made, this function will be used.
    }
    #endregion

    #region create myroom      
    public void CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            print("PhotonNetwork is not connected.");
            return;
        }

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
        options.EmptyRoomTtl = 0;
        PhotonNetwork.CreateRoom(PhotonNetwork.LocalPlayer.NickName, options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("roomId"))
            return;

        Hashtable playerProperties = new Hashtable();
        playerProperties["roomId"] = PhotonNetwork.CountOfRooms;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        

        print(PhotonNetwork.NickName + " created a new room.");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print(PhotonNetwork.NickName + " failed to create a new room: " + message);
    }

    public void JoinRoom(TMP_Text gameName)
    {
        print(PhotonNetwork.NickName + " tried to join the room " + gameName.text + ".");
        PhotonNetwork.JoinRoom(gameName.text);
    }

    public void JoinRoom(TMP_InputField gameName)
    {
        print(PhotonNetwork.NickName + " tried to join the room " + gameName.text + ".");
        PhotonNetwork.JoinRoom(gameName.text);
    }


    public override void OnJoinedRoom()
    {
        print(PhotonNetwork.NickName + " joined the room.");

        /*for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            players[i].gameObject.SetActive(true);
        }*/
        SceneManager.LoadSceneAsync(3);
        // json 형식으로 room에 배치된 아이템 및 게시물 정보 가져오기
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        cannotFindRoomPanel.SetActive(true);
        print(PhotonNetwork.NickName + " couldn't join the room: " + message);
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        print("newPlayer " + newPlayer.NickName + " entered the room.");

        players[1].gameObject.SetActive(true);
        players[1].GetComponent<Text>().text = PhotonNetwork.PlayerList[1].NickName;
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(PhotonNetwork.CurrentRoom.PlayerCount == 1 ? true : false);

        SceneManager.LoadSceneAsync(1);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RemotePlayerLeftRoom(otherPlayer);
    }

    public void RemotePlayerLeftRoom(Player otherPlayer)
    {
        players[PhotonNetwork.CurrentRoom.PlayerCount].gameObject.SetActive(false);
    }
    #endregion

    public void QuitGame()
    {
        Application.Quit();
    }
}