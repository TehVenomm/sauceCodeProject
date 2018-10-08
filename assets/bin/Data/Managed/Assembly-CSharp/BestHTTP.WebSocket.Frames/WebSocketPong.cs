namespace BestHTTP.WebSocket.Frames
{
	public sealed class WebSocketPong : WebSocketBinaryFrame
	{
		public override WebSocketFrameTypes Type => WebSocketFrameTypes.Pong;

		public WebSocketPong(WebSocketFrameReader ping)
			: base(ping.Data)
		{
		}
	}
}
