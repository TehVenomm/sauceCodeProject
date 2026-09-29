// Decompiled with JetBrains decompiler
// Type: Network.GuildRequestItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Network;

public class GuildRequestItem
{
  public int slotNo;
  public int crystalNum;
  public int questId;
  public int num;
  public EndDate endAt;
  public EndDate expiredAt;

  public TimeSpan GetQuestRemainTime()
  {
    if (this.endAt == null)
      return TimeSpan.FromTicks(-1L);
    if (!this.IsExpired())
      return this.endAt.CalcRemainTime();
    TimeSpan timeSpan = this.endAt.ConvToDateTime() - this.expiredAt.ConvToDateTime();
    return timeSpan.TotalSeconds < 0.0 ? TimeSpan.FromTicks(-1L) : timeSpan;
  }

  public string GetQuestRemainTimeWithFormat()
  {
    TimeSpan timeSpan = this.GetQuestRemainTime();
    if (timeSpan.TotalSeconds < 0.0)
      timeSpan = TimeSpan.FromTicks(0L);
    return new DateTime(0L).Add(timeSpan).ToString("H:mm:ss");
  }

  public int GetQuestRemainPoint()
  {
    TimeSpan questRemainTime = this.GetQuestRemainTime();
    return questRemainTime.TotalSeconds < 0.0 ? -1 : MonoBehaviourSingleton<GuildRequestManager>.I.CalcPointFromTimeSpan(questRemainTime);
  }

  public TimeSpan GetHoundRemainTime()
  {
    return this.expiredAt == null ? TimeSpan.FromTicks(-1L) : this.expiredAt.CalcRemainTime();
  }

  public string GetHoundRemainTimeWithFormat()
  {
    TimeSpan timeSpan = this.GetHoundRemainTime();
    if (timeSpan.TotalSeconds < 0.0)
      timeSpan = TimeSpan.FromTicks(0L);
    return new DateTime(0L).Add(timeSpan).ToString("H:mm:ss");
  }

  public TimeSpan GetBonusRemainTime()
  {
    TimeSpan timeSpan1 = TimeSpan.FromMinutes((double) MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.GUILD_REQUEST_EARLY_RECEIVE_MIN);
    if (this.endAt == null)
      return TimeSpan.FromTicks(0L);
    TimeSpan timeSpan2 = TimeManager.GetNow() - this.endAt.ConvToDateTime();
    TimeSpan bonusRemainTime = timeSpan1 - timeSpan2;
    if (bonusRemainTime.TotalSeconds < 0.0)
      bonusRemainTime = TimeSpan.FromTicks(0L);
    return bonusRemainTime;
  }

  public string GetBonusRemainTimeWithFormat()
  {
    TimeSpan timeSpan = this.GetBonusRemainTime();
    if (timeSpan.TotalSeconds < 0.0)
      timeSpan = TimeSpan.FromTicks(0L);
    return new DateTime(0L).Add(timeSpan).ToString("H:mm:ss");
  }

  public bool IsExpired() => this.crystalNum != 0 && this.GetHoundRemainTime().TotalSeconds <= 0.0;

  public bool IsSortieing() => this.endAt != null;

  public bool IsComplete() => (int) this.GetQuestRemainTime().TotalSeconds <= 0;
}
