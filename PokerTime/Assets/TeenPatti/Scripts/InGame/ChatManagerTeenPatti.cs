﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class ChatManagerTeenPatti : MonoBehaviour
{

    public static ChatManagerTeenPatti instance;
    private List<ChatMessage> chatList = new List<ChatMessage>();


    private void Awake()
    {
        instance = this;
    }

    public void OnChatMessageReceived(string serverResponse)
    {
        JsonData jsonData = JsonMapper.ToObject(serverResponse);
       // Debug.Log("IIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIZNNNNNNNNNNNNNNNNNNN");
        if (jsonData[0]["from"].ToString() != PlayerManager.instance.GetPlayerGameData().userId)
        {
            ChatMessage data = new ChatMessage();
            data.desc = jsonData[0]["desc"].ToString();
            data.title = jsonData[0]["title"].ToString();
            data.userId = jsonData[0]["userId"].ToString();
            data.isMe = false;
            chatList.Add(data);
        }

        if (ChatUiManagerTeenPatti.instance != null)
        {
            ChatUiManagerTeenPatti.instance.UpdateChatList();
        }
    }

    public void SendChatMessage(string messageToSend)
    {
        ChatMessage chatMessage = new ChatMessage();
        chatMessage.desc = messageToSend;
        chatMessage.isMe = true;
        chatMessage.title = GetUserName();
        chatList.Add(chatMessage);
        SocketController.instance.SendChatMessage(chatMessage.title, messageToSend);
    }

    private string GetUserName()
    {
        string userName = PlayerManager.instance.GetPlayerGameData().userName;
        userName += System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute + ":" + System.DateTime.Now.Second;

        return userName;
    }

    public List<ChatMessage> GetChatList()
    {
        return chatList;
    }

}

public class ChatMessageTeenPatti
{
    public string userName;
    public bool isMe;
    public string desc,title;
    public string userId;
}