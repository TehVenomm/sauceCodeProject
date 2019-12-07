using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChatPacketReceiver : MonoBehaviour
{
	[SerializeField]
	private List<ChatPacket> m_packets = new List<ChatPacket>();

	[SerializeField]
	public Queue<ChatPacket> m_reserved_delete = new Queue<ChatPacket>();

	public bool stopPacketUpdate
	{
		get;
		set;
	}

	public List<ChatPacket> packets => m_packets;

	public ChatPacketReceiver()
	{
		stopPacketUpdate = false;
	}

	public virtual void Set(ChatPacket packet)
	{
		packets.Add(packet);
	}

	public void AddDeleteQueue(ChatPacket packet)
	{
		m_reserved_delete.Enqueue(packet);
	}

	private void Update()
	{
		EraseUsedPacket();
	}

	public virtual void OnUpdate()
	{
		PacketUpdate();
	}

	protected virtual void PacketUpdate()
	{
		if (stopPacketUpdate)
		{
			return;
		}
		int i = 0;
		for (int count = packets.Count; i < count; i++)
		{
			if (stopPacketUpdate)
			{
				break;
			}
			ChatPacket packet = packets[i];
			if (HandleCoopEvent(packet))
			{
				AddDeleteQueue(packet);
			}
		}
		EraseUsedPacket();
	}

	protected virtual bool HandleCoopEvent(ChatPacket packet)
	{
		return false;
	}

	public virtual bool ForcePacketProcess(ChatPacket packet)
	{
		return HandleCoopEvent(packet);
	}

	public void EraseUsedPacket()
	{
		while (m_reserved_delete.Count > 0)
		{
			m_packets.Remove(m_reserved_delete.Dequeue());
		}
	}

	public void EraseAllPackets()
	{
		m_packets.Clear();
		m_reserved_delete.Clear();
	}
}
