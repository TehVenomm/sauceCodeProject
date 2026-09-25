// Decompiled with JetBrains decompiler
// Type: Network.EventBanner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Network;

[Serializable]
public class EventBanner
{
  public int bannerId;
  public int linkType;
  public int param;
  public int orderNo;
  public EndDate endDate = new EndDate();

  public LINK_TYPE LinkType => (LINK_TYPE) this.linkType;
}
