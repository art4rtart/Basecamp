﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    /// <summary>Sends a packet to the server via TCP.</summary>
    /// <param name="_packet">The packet to send to the sever.</param>
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    /// <summary>Sends a packet to the server via UDP.</summary>
    /// <param name="_packet">The packet to send to the sever.</param>
    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
	}

    #region Packets
    /// <summary>Lets the server know that the welcome message was received.</summary>
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(""); // UIManager.Instance.usernameField.text

			SendTCPData(_packet);
        }
    }
	
	public static void ImageData(byte[] bytes)
	{
		using (Packet _packet = new Packet((int)ClientPackets.fileData))
		{
			_packet.Write(bytes);
			SendTCPData(_packet);
		}
	}

	static string strText;
	public static void MessageData(string _message)
	{
		//var form = new WWWForm();
		//form.AddField(message, strText, System.Text.Encoding.UTF8);
		//Debug.Log(form.ToString());

		byte[] bytes = System.Text.Encoding.Default.GetBytes(_message);

		using (Packet _packet = new Packet((int)ClientPackets.playerMessage))
		{
			_packet.Write(bytes);
			SendUDPData(_packet);
		}
	}

	/// <summary>Sends player input to the server.</summary>
	/// <param name="_inputs"></param>
	public static void PlayerMovement(bool[] _inputs)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
        {
            _packet.Write(_inputs.Length);
            foreach (bool _input in _inputs)
            {
                _packet.Write(_input);
            }
            _packet.Write(GameManager.players[Client.instance.myId].transform.rotation);

            SendUDPData(_packet);
        }
    }

    public static void PlayerShoot(Vector3 _facing)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerShoot))
        {
            _packet.Write(_facing);

            SendTCPData(_packet);
        }
    }

    public static void PlayerThrowItem(Vector3 _facing)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerThrowItem))
        {
            _packet.Write(_facing);

            SendTCPData(_packet);
        }
    }
	#endregion
}