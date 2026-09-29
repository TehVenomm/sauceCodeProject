// Decompiled with JetBrains decompiler
// Type: ChatPacketReceiver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[Serializable]
public class ChatPacketReceiver : MonoBehaviour
{
  [SerializeField]
  private List<ChatPacket> m_packets = new List<ChatPacket>();
  [SerializeField]
  public Queue<ChatPacket> m_reserved_delete = new Queue<ChatPacket>();

  public bool stopPacketUpdate { get; set; }

  public ChatPacketReceiver() => this.stopPacketUpdate = false;

  public virtual void Set(ChatPacket packet) => this.packets.Add(packet);

  public void AddDeleteQueue(ChatPacket packet) => this.m_reserved_delete.Enqueue(packet);

  private void Update() => this.EraseUsedPacket();

  public virtual void OnUpdate() => this.PacketUpdate();

  protected virtual void PacketUpdate()
  {
    if (this.stopPacketUpdate)
      return;
    int index = 0;
    for (int count = this.packets.Count; index < count && !this.stopPacketUpdate; ++index)
    {
      ChatPacket packet = this.packets[index];
      if (this.HandleCoopEvent(packet))
        this.AddDeleteQueue(packet);
    }
    this.EraseUsedPacket();
  }

  protected virtual bool HandleCoopEvent(ChatPacket packet) => false;

  public virtual bool ForcePacketProcess(ChatPacket packet) => this.HandleCoopEvent(packet);

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

  public List<ChatPacket> packets => this.m_packets;
}
