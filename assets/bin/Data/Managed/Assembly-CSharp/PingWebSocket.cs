using BestHTTP.WebSocket;
using System;
using System.Runtime.CompilerServices;

public class PingWebSocket
{
	private const float HEARTBEAT_TIMEOUT = 5f;

	private const float HEARTBEAT_INTERVAL = 1f;

	private WebSocket sock;

	private DateTime lastPacketReceivedTime;

	private bool isConnect;

	public event Action OnOpen
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			this.OnOpen = Delegate.Combine((Delegate)this.OnOpen, (Delegate)value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			this.OnOpen = Delegate.Remove((Delegate)this.OnOpen, (Delegate)value);
		}
	}

	public event Action OnClosed
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			this.OnClosed = Delegate.Combine((Delegate)this.OnClosed, (Delegate)value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			this.OnClosed = Delegate.Remove((Delegate)this.OnClosed, (Delegate)value);
		}
	}

	public event Action OnError
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			this.OnError = Delegate.Combine((Delegate)this.OnError, (Delegate)value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			this.OnError = Delegate.Remove((Delegate)this.OnError, (Delegate)value);
		}
	}

	public event Action<double> OnPong;

	public PingWebSocket()
		: this()
	{
	}

	public unsafe void Connect(string relayServer)
	{
		sock = new WebSocket(new Uri(relayServer));
		WebSocket webSocket = sock;
		webSocket.OnOpen = (Action<WebSocket>)Delegate.Combine(webSocket.OnOpen, (Action<WebSocket>)delegate
		{
			lastPacketReceivedTime = DateTime.Now;
			isConnect = true;
			if (this.OnOpen != null)
			{
				this.OnOpen.Invoke();
			}
		});
		WebSocket webSocket2 = sock;
		webSocket2.OnClosed = Delegate.Combine((Delegate)webSocket2.OnClosed, (Delegate)new Action<WebSocket, ushort, string>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		WebSocket webSocket3 = sock;
		webSocket3.OnError = Delegate.Combine((Delegate)webSocket3.OnError, (Delegate)new Action<WebSocket, Exception>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		WebSocket webSocket4 = sock;
		webSocket4.OnPong = Delegate.Combine((Delegate)webSocket4.OnPong, (Delegate)new Action<WebSocket, byte[]>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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
