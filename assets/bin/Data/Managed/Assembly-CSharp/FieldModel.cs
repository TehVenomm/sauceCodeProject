// Decompiled with JetBrains decompiler
// Type: FieldModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class FieldModel : BaseModel
{
  public FieldModel.Param result = new FieldModel.Param();

  public class Param
  {
    public FieldModel.Field field;
    public List<int> gather;
    public List<GatherGrowthInfo> growth;
    public string raidBossHp;
    public string noticeText = "";
    public int mapFlag;
  }

  public class Field
  {
    public string id;
    public int mapId;
    public List<FieldModel.SlotInfo> slotInfos;
    public string wsHost;
    public List<int> wsPorts;
    public int expiredAt;
    public string createdAt;
    public int enableStandby;

    public bool TryGetCreatedAt(out DateTime createdAt)
    {
      return DateTime.TryParse(this.createdAt, out createdAt);
    }
  }

  public class SlotInfo
  {
    public int userId;
    public string token;
    public FriendCharaInfo userInfo;
  }

  public class RequestMatching
  {
    public static string path = "ajax/field/matching";
    public int portalId;
    public string token;
    public int dId;
    public int prevId;
    public int toUserId;
  }

  public class RequestQuest
  {
    public static string path = "ajax/field/quest";
    public string token;
    public int qid;
  }

  public class RequestCreate
  {
    public static string path = "ajax/field/create";
    public string partyId;
    public string token;
  }

  public class RequestEnter
  {
    public static string path = "ajax/field/enter";
    public string partyId;
    public string token;
  }

  public class RequestInfo
  {
    public static string path = "ajax/field/info";
  }
}
