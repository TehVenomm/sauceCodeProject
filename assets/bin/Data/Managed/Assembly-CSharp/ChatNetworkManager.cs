using rhyme;
using System.Collections.Generic;
using UnityEngine;

public class ChatNetworkManager : MonoBehaviourSingleton<ChatNetworkManager>
{
	private class Pool_List_ChatPacket
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
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		base.Awake();
		packetReceiver = this.get_gameObject().AddComponent<ChatPacketReceiver>();
	}

	private void Clear()
	{
	}

	private void Logd(string str, params object[] objs)
	{
		if (!Log.enabled)
		{
			return;
		}
	}

	private void Update()
	{
		PacketUpdate();
	}

	public void PacketUpdate()
	{
		List<ChatPacket> list = rymTPool<List<ChatPacket>>.Get();
		if (list.Capacity < packetReceiver.packets.Count)
		{
			list.Capacity = packetReceiver.packets.Count;
		}
		int i = 0;
		for (int count = list.Count; i < count; i++)
		{
			list[i] = packetReceiver.packets[i];
		}
		int j = 0;
		for (int count2 = list.Count; j < count2; j++)
		{
			ChatPacket packet = list[j];
			if (HandleChatPacket(packet))
			{
				packetReceiver.AddDeleteQueue(packet);
			}
		}
		list.Clear();
		rymTPool<List<ChatPacket>>.Release(ref list);
		packetReceiver.EraseUsedPacket();
	}

	private bool HandleChatPacket(ChatPacket packet)
	{
		bool flag = false;
		CHAT_PACKET_TYPE packetType = packet.packetType;
		Logd(packet.ToString());
		return true;
	}
}
