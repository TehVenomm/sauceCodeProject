// Decompiled with JetBrains decompiler
// Type: Network.ClearStatusQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Network;

[Serializable]
public class ClearStatusQuest
{
  public int questId;
  public int questStatus;
  public List<int> missionStatus = new List<int>();
  public List<int> story = new List<int>();
  public int clearTime;
}
