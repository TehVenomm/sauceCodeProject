// Decompiled with JetBrains decompiler
// Type: Coop_Model_RoomSyncPlayerStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class Coop_Model_RoomSyncPlayerStatus : Coop_Model_Base
{
  public int hp;
  public BuffParam.BuffSyncParam buff;
  public int wid;
  public List<int> exst = new List<int>();

  public Coop_Model_RoomSyncPlayerStatus() => this.packetType = PACKET_TYPE.ROOM_SYNC_PLAYER_STATUS;

  public void SetExtraStatus(Self self, List<int> prevStatus)
  {
    bool flag = prevStatus != null && prevStatus.Count > 0;
    for (int index = 0; index < UIStatusIcon.NON_BUFF_STATUS.Length; ++index)
    {
      UIStatusIcon.STATUS_TYPE status = UIStatusIcon.NON_BUFF_STATUS[index];
      if (!flag || !prevStatus.Contains(index))
        this.AddExtraStatusIfEnabled(self, status);
    }
  }

  private void AddExtraStatusIfEnabled(Self self, UIStatusIcon.STATUS_TYPE status)
  {
    if (!Coop_Model_RoomSyncPlayerStatus.StatusEnabled((Player) self, status))
      return;
    this.exst.Add((int) status);
  }

  public static bool StatusEnabled(Player self, UIStatusIcon.STATUS_TYPE status)
  {
    return UIStatusIcon.CheckStatus(status, (Character) self, false);
  }

  public override string ToString()
  {
    return base.ToString() + $"hp={this.hp} wid={this.wid} buff={this.buff}";
  }
}
