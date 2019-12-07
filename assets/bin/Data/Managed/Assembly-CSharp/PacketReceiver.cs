using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PacketReceiver : MonoBehaviour
{
	[SerializeField]
	private List<CoopPacket> m_packets = new List<CoopPacket>();

	[SerializeField]
	public Queue<CoopPacket> m_reserved_delete = new Queue<CoopPacket>();

	public bool stopPacketUpdate
	{
		get;
		private set;
	}

	public List<CoopPacket> packets => m_packets;

	public PacketReceiver()
	{
		stopPacketUpdate = false;
	}

	public virtual void Set(CoopPacket packet)
	{
		packets.Add(packet);
	}

	public void AddDeleteQueue(CoopPacket packet)
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

	public virtual void SetStopPacketUpdate(bool is_stop)
	{
		stopPacketUpdate = is_stop;
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
			CoopPacket packet = packets[i];
			if (HandleCoopEvent(packet))
			{
				AddDeleteQueue(packet);
			}
		}
		EraseUsedPacket();
	}

	protected virtual bool HandleCoopEvent(CoopPacket packet)
	{
		return false;
	}

	public virtual bool ForcePacketProcess(CoopPacket packet)
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
