namespace BestHTTP
{
	internal enum HTTPConnectionStates
	{
		Initial,
		Processing,
		Redirected,
		Upgraded,
		WaitForProtocolShutdown,
		WaitForRecycle,
		Free,
		Closed
	}
}
