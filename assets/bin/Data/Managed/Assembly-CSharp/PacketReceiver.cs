// Decompiled with JetBrains decompiler
// Type: PacketReceiver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[Serializable]
public class PacketReceiver : MonoBehaviour
{
  [SerializeField]
  private List<CoopPacket> m_packets = new List<CoopPacket>();
  [SerializeField]
  public Queue<CoopPacket> m_reserved_delete = new Queue<CoopPacket>();

  public bool stopPacketUpdate { get; private set; }

  public PacketReceiver() => this.stopPacketUpdate = false;

  public virtual void Set(CoopPacket packet) => this.packets.Add(packet);

  public void AddDeleteQueue(CoopPacket packet) => this.m_reserved_delete.Enqueue(packet);

  private void Update() => this.EraseUsedPacket();

  public virtual void OnUpdate() => this.PacketUpdate();

  public virtual void SetStopPacketUpdate(bool is_stop) => this.stopPacketUpdate = is_stop;

  protected virtual void PacketUpdate()
  {
    if (this.stopPacketUpdate)
      return;
    int index = 0;
    for (int count = this.packets.Count; index < count && !this.stopPacketUpdate; ++index)
    {
      CoopPacket packet = this.packets[index];
      if (this.HandleCoopEvent(packet))
        this.AddDeleteQueue(packet);
    }
    this.EraseUsedPacket();
  }

  protected virtual bool HandleCoopEvent(CoopPacket packet) => false;

  public virtual bool ForcePacketProcess(CoopPacket packet) => this.HandleCoopEvent(packet);

  public void EraseUsedPacket()
  {
    while (this.m_reserved_delete.Count > 0)
      this.m_packets.Remove(this.m_reserved_delete.Dequeue());
  }

  public void EraseAllPackets()
  {
    this.m_packets.Clear();
    this.m_reserved_delete.Clear();
  }

  public List<CoopPacket> packets => this.m_packets;
}
