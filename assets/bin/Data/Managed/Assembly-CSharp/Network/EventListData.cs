// Decompiled with JetBrains decompiler
// Type: Network.EventListData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace Network;

public class EventListData : EventData
{
  public int leftBadge;
  public int rightBadge;
  public string rightValue;
  public int place;

  public BADGE_1_CATEGORY leftBadgeEnum { get; protected set; }

  public BADGE_2_CATEGORY rightBadgeEnum { get; protected set; }

  public EVENT_DISPLAY_PLACE placeEnum { get; protected set; }

  public override void SetupEnum()
  {
    base.SetupEnum();
    this.leftBadgeEnum = (BADGE_1_CATEGORY) this.leftBadge;
    this.rightBadgeEnum = (BADGE_2_CATEGORY) this.rightBadge;
    this.placeEnum = (EVENT_DISPLAY_PLACE) this.place;
  }
}
