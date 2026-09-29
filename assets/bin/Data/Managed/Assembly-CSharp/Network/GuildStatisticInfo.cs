// Decompiled with JetBrains decompiler
// Type: Network.GuildStatisticInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Network;

[Serializable]
public class GuildStatisticInfo
{
  public int level;
  public string description;
  public int privacy;
  public int currentMem;
  public string createAt;
  public string clanName;
  public int behemoth;
  public int ally;
  public int missions;
  public int gather_mine;
  public int parts;
  public string location;
  public bool canJoin;
  public string tag;
  public int donate;
  public int[] emblem;
  public int min_level;
  public int memCap;
}
