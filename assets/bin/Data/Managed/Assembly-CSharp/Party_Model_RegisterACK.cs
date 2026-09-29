// Decompiled with JetBrains decompiler
// Type: Party_Model_RegisterACK
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
public class Party_Model_RegisterACK : Coop_Model_ACK
{
  public static readonly DateTime UTC_TIME = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
  public List<string> userinfo;

  public List<Party_Model_RegisterACK.UserInfo> GetConvertUserInfo()
  {
    return this.userinfo == null ? (List<Party_Model_RegisterACK.UserInfo>) null : this.userinfo.Select<string, Party_Model_RegisterACK.UserInfo>((Func<string, Party_Model_RegisterACK.UserInfo>) (x => new Party_Model_RegisterACK.UserInfo(x))).ToList<Party_Model_RegisterACK.UserInfo>();
  }

  public Party_Model_RegisterACK() => this.packetType = PACKET_TYPE.PARTY_REGISTER_ACK;

  public override string ToString()
  {
    string str = base.ToString();
    str = $"{str},ack={(object) this.ack}";
    str = $"{str},positive={this.positive.ToString()}";
    str += ",userInfo=[";
    if (this.userinfo != null && this.userinfo.Any<string>())
      this.userinfo.ForEach((Action<string>) (x => str = $"{str},u={x}"));
    str += "]";
    return str;
  }

  public class UserInfo
  {
    public int userId;
    public string fieldId;
    public int fieldMapId;
    public string partyId;
    public int questId;
    public DateTime lastExecTime;

    public UserInfo(string data)
    {
      string[] strArray = data.Split(',');
      if (strArray.Length < 6)
        return;
      int.TryParse(strArray[0], out this.userId);
      this.fieldId = strArray[1];
      int.TryParse(strArray[2], out this.fieldMapId);
      this.partyId = strArray[3];
      int.TryParse(strArray[4], out this.questId);
      int result;
      int.TryParse(strArray[5], out result);
      this.lastExecTime = Party_Model_RegisterACK.UTC_TIME.AddSeconds((double) result);
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder(base.ToString());
      stringBuilder.Append(",cid=");
      stringBuilder.Append(this.userId);
      stringBuilder.Append(",fid=");
      stringBuilder.Append(this.fieldId);
      stringBuilder.Append(",fmid=");
      stringBuilder.Append(this.fieldMapId);
      stringBuilder.Append(",pid=");
      stringBuilder.Append(this.partyId);
      stringBuilder.Append(",qid=");
      stringBuilder.Append(this.questId);
      return stringBuilder.ToString();
    }
  }
}
