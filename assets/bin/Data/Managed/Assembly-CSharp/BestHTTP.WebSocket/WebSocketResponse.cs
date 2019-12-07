using BestHTTP.WebSocket.Frames;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace BestHTTP.WebSocket
{
	public class WebSocketResponse : HTTPResponse
	{
		private const int PingThreadFrequency = 100;

		public Action<WebSocketResponse, string> OnText;

		public Action<WebSocketResponse, byte[]> OnBinary;

		public Action<WebSocketResponse, byte[]> OnPong;

		public Action<WebSocketResponse, WebSocketFrameReader> OnIncompleteFrame;

		public Action<WebSocketResponse, ushort, string> OnClosed;

		private List<WebSocketFrameReader> IncompleteFrames = new List<WebSocketFrameReader>();

		private List<WebSocketFrameReader> CompletedFrames = new List<WebSocketFrameReader>();

		private WebSocketFrameReader CloseFrame;

		private Thread ReceiverThread;

		private Thread PingThread;

		private object FrameLock = new object();

		private object SendLock = new object();

		private bool closeSent;

		private bool closed;

		private int ClosedCount;

		private int MinClosedCount;

		public bool IsClosed => ClosedCount >= MinClosedCount;

		public int PingFrequnecy
		{
			get;
			private set;
		}

		internal WebSocketResponse(HTTPRequest request, Stream stream, bool isStreamed, bool isFromCache)
			: base(request, stream, isStreamed, isFromCache)
		{
			closed = false;
			ClosedCount = 0;
			MinClosedCount = 1;
		}

		internal override bool Receive(int forceReadRawContentLength = -1)
		{
			bool num = base.Receive(forceReadRawContentLength);
			if (num && base.IsUpgraded)
			{
				ReceiverThread = new Thread(ReceiveThreadFunc);
				ReceiverThread.Name = "WebSocket Receiver Thread";
				ReceiverThread.IsBackground = true;
				ReceiverThread.Start();
			}
			return num;
		}

		public void Send(string message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message must not be null!");
			}
			Send(new WebSocketTextFrame(message));
		}

		public void Send(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data must not be null!");
			}
			ulong num = 32758uL;
			if ((long)data.Length > (long)num)
			{
				lock (SendLock)
				{
					Send(new WebSocketBinaryFrame(data, 0uL, num, isFinal: false));
					ulong num3;
					for (ulong num2 = num; num2 < (ulong)data.Length; num2 += num3)
					{
						num3 = Math.Min(num, (ulong)((long)data.Length - (long)num2));
						Send(new WebSocketContinuationFrame(data, num2, num3, num2 + num3 >= (ulong)data.Length));
					}
				}
			}
			else
			{
				Send(new WebSocketBinaryFrame(data));
			}
		}

		public void Send(IWebSocketFrameWriter frame)
		{
			if (frame == null)
			{
				throw new ArgumentNullException("frame is null!");
			}
			if (!closed)
			{
				byte[] array = frame.Get();
				lock (SendLock)
				{
					Stream.Write(array, 0, array.Length);
				}
				if (frame.Type == WebSocketFrameTypes.ConnectionClose)
				{
					closeSent = true;
				}
			}
		}

		public void Close()
		{
			Close(1000, "Bye!");
		}

		public void Close(ushort code, string msg)
		{
			if (!closed)
			{
				Send(new WebSocketClose(code, msg));
			}
		}

		public void StartPinging(int frequency)
		{
			if (frequency < 100)
			{
				throw new ArgumentException("frequency must be at least 100 millisec!");
			}
			PingFrequnecy = frequency;
			MinClosedCount = 2;
			PingThread = new Thread(PingThreadFunc);
			PingThread.Name = "WebSocket Ping Thread";
			PingThread.IsBackground = true;
			PingThread.Start();
		}

		private void ReceiveThreadFunc()
		{
			while (!closed)
			{
				try
				{
					WebSocketFrameReader webSocketFrameReader = new WebSocketFrameReader();
					webSocketFrameReader.Read(Stream);
					if (webSocketFrameReader.HasMask)
					{
						Close(1002, "Protocol Error: masked frame received from server!");
					}
					else if (!webSocketFrameReader.IsFinal)
					{
						if (OnIncompleteFrame == null)
						{
							IncompleteFrames.Add(webSocketFrameReader);
						}
						else
						{
							lock (FrameLock)
							{
								CompletedFrames.Add(webSocketFrameReader);
							}
						}
					}
					else
					{
						switch (webSocketFrameReader.Type)
						{
						case (WebSocketFrameTypes)3:
						case (WebSocketFrameTypes)4:
						case (WebSocketFrameTypes)5:
						case (WebSocketFrameTypes)6:
						case (WebSocketFrameTypes)7:
							break;
						case WebSocketFrameTypes.Continuation:
							if (OnIncompleteFrame != null)
							{
								lock (FrameLock)
								{
									CompletedFrames.Add(webSocketFrameReader);
								}
								break;
							}
							webSocketFrameReader.Assemble(IncompleteFrames);
							IncompleteFrames.Clear();
							goto case WebSocketFrameTypes.Text;
						case WebSocketFrameTypes.Text:
						case WebSocketFrameTypes.Binary:
							if (OnText != null)
							{
								lock (FrameLock)
								{
									CompletedFrames.Add(webSocketFrameReader);
								}
							}
							break;
						case WebSocketFrameTypes.Ping:
							if (!closeSent && !closed)
							{
								Send(new WebSocketPong(webSocketFrameReader));
							}
							break;
						case WebSocketFrameTypes.Pong:
							if (OnPong != null)
							{
								lock (FrameLock)
								{
									CompletedFrames.Add(webSocketFrameReader);
								}
							}
							break;
						case WebSocketFrameTypes.ConnectionClose:
							CloseFrame = webSocketFrameReader;
							if (!closeSent)
							{
								Send(new WebSocketClose());
							}
							closed = closeSent;
							break;
						}
					}
				}
				catch (ThreadAbortException)
				{
					IncompleteFrames.Clear();
					closed = true;
				}
				catch (Exception exception)
				{
					baseRequest.Exception = exception;
					closed = true;
				}
			}
			Interlocked.Increment(ref ClosedCount);
			if (PingThread != null && PingThread.Join(1000))
			{
				PingThread.Abort();
			}
		}

		private void PingThreadFunc()
		{
			int num = 0;
			while (!closed)
			{
				try
				{
					Thread.Sleep(100);
					num += 100;
					if (num >= PingFrequnecy)
					{
						Send(new WebSocketPing(string.Empty));
						num = 0;
					}
				}
				catch (ThreadAbortException)
				{
					closed = true;
				}
				catch (Exception exception)
				{
					baseRequest.Exception = exception;
					closed = true;
				}
			}
			Interlocked.Increment(ref ClosedCount);
			if (ReceiverThread.Join(1000))
			{
				ReceiverThread.Abort();
			}
		}

		internal void HandleEvents()
		{
			lock (FrameLock)
			{
				for (int i = 0; i < CompletedFrames.Count; i++)
				{
					WebSocketFrameReader webSocketFrameReader = CompletedFrames[i];
					try
					{
						switch (webSocketFrameReader.Type)
						{
						case WebSocketFrameTypes.Continuation:
							if (OnIncompleteFrame != null)
							{
								OnIncompleteFrame(this, webSocketFrameReader);
							}
							break;
						case WebSocketFrameTypes.Text:
							if (webSocketFrameReader.IsFinal)
							{
								if (OnText != null)
								{
									OnText(this, Encoding.UTF8.GetString(webSocketFrameReader.Data, 0, webSocketFrameReader.Data.Length));
								}
								break;
							}
							goto case WebSocketFrameTypes.Continuation;
						case WebSocketFrameTypes.Binary:
							if (webSocketFrameReader.IsFinal)
							{
								if (OnBinary != null)
								{
									OnBinary(this, webSocketFrameReader.Data);
								}
								break;
							}
							goto case WebSocketFrameTypes.Continuation;
						case WebSocketFrameTypes.Pong:
							if (OnPong != null)
							{
								OnPong(this, webSocketFrameReader.Data);
							}
							break;
						}
					}
					catch
					{
					}
				}
				CompletedFrames.Clear();
			}
			if (IsClosed && OnClosed != null)
			{
				try
				{
					ushort arg = 0;
					string arg2 = string.Empty;
					if (CloseFrame != null && CloseFrame.Data != null && CloseFrame.Data.Length >= 2)
					{
						if (BitConverter.IsLittleEndian)
						{
							Array.Reverse((Array)CloseFrame.Data, 0, 2);
						}
						arg = BitConverter.ToUInt16(CloseFrame.Data, 0);
						if (CloseFrame.Data.Length > 2)
						{
							arg2 = Encoding.UTF8.GetString(CloseFrame.Data, 2, CloseFrame.Data.Length - 2);
						}
					}
					OnClosed(this, arg, arg2);
				}
				catch
				{
				}
			}
		}
	}
}
