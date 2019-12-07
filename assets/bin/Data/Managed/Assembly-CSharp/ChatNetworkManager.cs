using rhyme;
using System.Collections.Generic;
using UnityEngine;

public class ChatNetworkManager : MonoBehaviourSingleton<ChatNetworkManager>
{
	private class Pool_List_ChatPacket : rymTPool<List<ChatPacket>>
	{
	}

	public class ConnectData
	{
		public string path = string.Empty;

		public List<int> ports = new List<int>();

		public int fromId;

		public int ackPrefix;

		public string roomId = string.Empty;

		public string token = string.Empty;
	}

	[SerializeField]
	protected ChatPacketReceiver packetReceiver
	{
		get;
		set;
	}

	public static void ClearPoolObjects()
	{
		rymTPool<List<ChatPacket>>.Clear();
	}

	protected override void Awake()
	{
		base.Awake();
		packetReceiver = base.gameObject.AddComponent<ChatPacketReceiver>();
	}

	private void Clear()
	{
	}

	private void Logd(string str, params object[] objs)
	{
		_ = Log.enabled;
	}

	private void Update()
	{
		PacketUpdate();
	}

	public void PacketUpdate()
	{
		List<ChatPacket> obj = rymTPool<List<ChatPacket>>.Get();
		if (obj.Capacity < packetReceiver.packets.Count)
		{
			obj.Capacity = packetReceiver.packets.Count;
		}
		int i = 0;
		for (int count = obj.Count; i < count; i++)
		{
			obj[i] = packetReceiver.packets[i];
		}
		int j = 0;
		for (int count2 = obj.Count; j < count2; j++)
		{
			ChatPacket packet = obj[j];
			if (HandleChatPacket(packet))
			{
				packetReceiver.AddDeleteQueue(packet);
			}
		}
		obj.Clear();
		rymTPool<List<ChatPacket>>.Release(ref obj);
		packetReceiver.EraseUsedPacket();
	}

	private bool HandleChatPacket(ChatPacket packet)
	{
		bool flag = false;
		_ = packet.packetType;
		Logd(packet.ToString());
		return true;
	}
}
