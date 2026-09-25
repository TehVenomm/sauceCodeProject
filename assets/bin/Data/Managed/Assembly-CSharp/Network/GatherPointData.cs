// Decompiled with JetBrains decompiler
// Type: Network.GatherPointData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Network;

[Serializable]
public class GatherPointData
{
  public int gatherPointId;
  public int gatherObjectId;
  public int gatherCount;
  public int status;
  public int rest;
  public int attackTime;
  public EndDate appearAt;
  public EndDate disappearAt;
  public EndDate gatherEndAt;

  public GATHER_POINT_STATUS pointStatus => (GATHER_POINT_STATUS) this.status;
}
