using System.Collections.Generic;
using UnityEngine;

public class NetworkStateView : MonoBehaviourSingleton<NetworkStateView>
{
	protected override void Awake()
	{
		base.Awake();
	}

	public void CreateSession()
	{
	}

	public void SearchSession()
	{
	}

	public List<HostData> GetSessionList()
	{
		return null;
	}

	public void JoinSession(HostData hostData)
	{
	}

	public void LeaveSession()
	{
	}
}
