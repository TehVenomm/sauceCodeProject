// Decompiled with JetBrains decompiler
// Type: ChatNetworkManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ChatNetworkManager : MonoBehaviourSingleton<ChatNetworkManager>
{
  public static void ClearPoolObjects() => rymTPool<List<ChatPacket>>.Clear();

  [SerializeField]
  protected ChatPacketReceiver packetReceiver { get; set; }

  protected override void Awake()
  {
    base.Awake();
    this.packetReceiver = ((Component) this).gameObject.AddComponent<ChatPacketReceiver>();
  }

  private void Clear()
  {
  }

  private void Logd(string str, params object[] objs)
  {
    int num = Log.enabled ? 1 : 0;
  }

  private void Update() => this.PacketUpdate();

  public void PacketUpdate()
  {
    List<ChatPacket> chatPacketList = rymTPool<List<ChatPacket>>.Get();
    if (chatPacketList.Capacity < this.packetReceiver.packets.Count)
      chatPacketList.Capacity = this.packetReceiver.packets.Count;
    int index1 = 0;
    for (int count = chatPacketList.Count; index1 < count; ++index1)
      chatPacketList[index1] = this.packetReceiver.packets[index1];
    int index2 = 0;
    for (int count = chatPacketList.Count; index2 < count; ++index2)
    {
      ChatPacket packet = chatPacketList[index2];
      if (this.HandleChatPacket(packet))
        this.packetReceiver.AddDeleteQueue(packet);
    }
    chatPacketList.Clear();
    rymTPool<List<ChatPacket>>.Release(ref chatPacketList);
    this.packetReceiver.EraseUsedPacket();
  }

  private bool HandleChatPacket(ChatPacket packet)
  {
    int packetType = (int) packet.packetType;
    this.Logd(packet.ToString());
    return true;
  }

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
}
