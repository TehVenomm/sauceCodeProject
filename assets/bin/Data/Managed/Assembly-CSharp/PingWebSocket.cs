using BestHTTP.WebSocket;
using System;
using UnityEngine;

public class PingWebSocket : MonoBehaviour
{
	private const float HEARTBEAT_TIMEOUT = 5f;

	private const float HEARTBEAT_INTERVAL = 1f;

	private WebSocket sock;

	private DateTime lastPacketReceivedTime;

	private bool isConnect;

	public event Action OnOpen;

	public event Action OnClosed;

	public event Action OnError;

	public event Action<double> OnPong;

	public void Connect(string relayServer)
	{
		sock = new WebSocket(new Uri(relayServer));
		WebSocket webSocket = sock;
		webSocket.OnOpen = (Action<WebSocket>)Delegate.Combine(webSocket.OnOpen, (Action<WebSocket>)delegate
		{
			lastPacketReceivedTime = DateTime.Now;
			isConnect = true;
			if (this.OnOpen != null)
			{
				this.OnOpen();
			}
		});
		WebSocket webSocket2 = sock;
		webSocket2.OnClosed = (Action<WebSocket, ushort, string>)Delegate.Combine(webSocket2.OnClosed, (Action<WebSocket, ushort, string>)delegate
		{
			isConnect = false;
			if (this.OnClosed != null)
			{
				this.OnClosed();
			}
		});
		WebSocket webSocket3 = sock;
		webSocket3.OnError = (Action<WebSocket, Exception>)Delegate.Combine(webSocket3.OnError, (Action<WebSocket, Exception>)delegate
		{
			if (this.OnError != null)
			{
				this.OnError();
			}
		});
		WebSocket webSocket4 = sock;
		webSocket4.OnPong = (Action<WebSocket, byte[]>)Delegate.Combine(webSocket4.OnPong, (Action<WebSocket, byte[]>)delegate
		{
			if (this.OnPong != null)
			{
				this.OnPong((DateTime.Now - lastPacketReceivedTime).TotalMilliseconds - 1000.0);
			}
			lastPacketReceivedTime = DateTime.Now;
		});
		sock.StartPingThread = true;
		sock.PingFrequency = 1000;
		sock.Open();
	}

	public void Close(ushort code = 1000, string msg = "Bye!")
	{
		if (sock != null)
		{
			sock.Close(code, msg);
		}
	}

	public bool IsConnected()
	{
		return isConnect;
	}
}
