// Decompiled with JetBrains decompiler
// Type: Network.TheaterModePostData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Network;

[Serializable]
public class TheaterModePostData
{
  public int theaterId;
  public int deliveryId;
  public int storyId;

  public TheaterModePostData(int id, int delivery, int story)
  {
    this.theaterId = id;
    this.deliveryId = delivery;
    this.storyId = story;
  }
}
