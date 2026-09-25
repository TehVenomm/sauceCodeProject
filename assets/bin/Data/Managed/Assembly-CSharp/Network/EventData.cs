// Decompiled with JetBrains decompiler
// Type: Network.EventData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Network;

public class EventData
{
  public int eventId;
  public string name;
  public int bannerId;
  public string linkName;
  public string minVersion;
  public EndDate endDate = new EndDate();
  public int rest;
  public int prologueStoryId;
  public string prologueTitle;
  public bool readPrologueStory;
  public int eventType;
  public int hostCountLimit;
  public int displayLocationType;
  protected DateTime receiveDateTime;
  protected Version requiredVersion;
  public int orderNo;
  public bool enableEvent;
  public int preEventId;
  public int preDeliveryId;
  public int subButtonType;
  public bool enableRanking;

  public bool IsPlayableWith(Version version)
  {
    return string.IsNullOrEmpty(this.minVersion) || version >= this.requiredVersion;
  }

  public bool HasEndDate() => !string.IsNullOrEmpty(this.endDate.date);

  public int GetRest()
  {
    int num = (int) (DateTime.UtcNow - this.receiveDateTime).TotalSeconds;
    if (num < 0)
      num = 0;
    return this.rest - num;
  }

  public void OnRecv()
  {
    this.receiveDateTime = DateTime.UtcNow;
    if (string.IsNullOrEmpty(this.minVersion))
      return;
    this.requiredVersion = new Version(this.minVersion);
  }

  public EVENT_TYPE eventTypeEnum { get; protected set; }

  public virtual void SetupEnum() => this.eventTypeEnum = (EVENT_TYPE) this.eventType;
}
