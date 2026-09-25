// Decompiled with JetBrains decompiler
// Type: Network.HomeCharaInfoList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Network;

[Serializable]
public class HomeCharaInfoList
{
  public CharaInfo own = new CharaInfo();
  public List<FriendCharaInfo> chara = new List<FriendCharaInfo>();
}
