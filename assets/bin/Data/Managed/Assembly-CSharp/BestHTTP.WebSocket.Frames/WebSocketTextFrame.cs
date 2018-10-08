using System.Text;

namespace BestHTTP.WebSocket.Frames
{
	public sealed class WebSocketTextFrame : WebSocketBinaryFrame
	{
		public override WebSocketFrameTypes Type => WebSocketFrameTypes.Text;

		public WebSocketTextFrame(string text)
			: base(Encoding.UTF8.GetBytes(text))
		{
		}
	}
}
