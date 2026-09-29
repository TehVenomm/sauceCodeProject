// Decompiled with JetBrains decompiler
// Type: Network.DonateInvitationInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Network;

[Serializable]
public class DonateInvitationInfo
{
  public int id;
  public int userId;
  public string nickName;
  public string msg;
  public int itemId;
  public string itemName;
  public int itemNum;
  public List<DonateHelperInfo> helpers;
  public int quantity;
  public double expired;
  public int status;

  public DonateInfo ParseDonateInfo()
  {
    return new DonateInfo()
    {
      id = this.id,
      nickName = this.nickName,
      materialName = this.itemName,
      userId = this.userId,
      msg = this.msg,
      itemId = this.itemId,
      itemNum = this.itemNum,
      quantity = this.quantity
    };
  }
}
