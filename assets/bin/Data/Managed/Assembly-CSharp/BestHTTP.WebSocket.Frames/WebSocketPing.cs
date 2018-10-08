using System.Text;

namespace BestHTTP.WebSocket.Frames
{
	public sealed class WebSocketPing : WebSocketBinaryFrame
	{
		public override WebSocketFrameTypes Type => WebSocketFrameTypes.Ping;

		public WebSocketPing(string msg)
			: base(Encoding.UTF8.GetBytes(msg))
		{
		}
	}
}
