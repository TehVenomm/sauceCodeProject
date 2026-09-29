// Decompiled with JetBrains decompiler
// Type: UIDeadAnnounce
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class UIDeadAnnounce : UIAnnounceBase<UIDeadAnnounce>
{
  [SerializeField]
  protected UILabel playerName;
  [SerializeField]
  protected UILabel announceName;
  [SerializeField]
  protected UILabel announceEffect;
  [SerializeField]
  protected GameObject deadBack;
  [SerializeField]
  protected GameObject rescueBack;
  [SerializeField]
  protected GameObject deadEff;
  [SerializeField]
  protected GameObject rescueEff;
  [SerializeField]
  protected UIDeadAnnounce.LabelSettings[] labelSettings = new UIDeadAnnounce.LabelSettings[8];

  public void Announce(UIDeadAnnounce.ANNOUNCE_TYPE type, Player player)
  {
    ((Component) this).gameObject.SetActive(true);
    if (!this.AnnounceStart(player))
      return;
    this.SetupAnnounce(type, player.charaName);
  }

  public void Announce(UIDeadAnnounce.ANNOUNCE_TYPE type, string charaName)
  {
    ((Component) this).gameObject.SetActive(true);
    if (!this.AnnounceStart())
      return;
    this.SetupAnnounce(type, charaName);
  }

  private void SetupAnnounce(UIDeadAnnounce.ANNOUNCE_TYPE type, string charaName)
  {
    if (type == UIDeadAnnounce.ANNOUNCE_TYPE.DEAD || type == UIDeadAnnounce.ANNOUNCE_TYPE.RETIRE || type == UIDeadAnnounce.ANNOUNCE_TYPE.STONE)
    {
      this.deadBack.SetActive(true);
      this.deadEff.SetActive(true);
      this.rescueBack.SetActive(false);
      this.rescueEff.SetActive(false);
    }
    else
    {
      this.deadBack.SetActive(false);
      this.deadEff.SetActive(false);
      this.rescueBack.SetActive(true);
      this.rescueEff.SetActive(true);
    }
    this.announceName.text = this.labelSettings[(int) type].text;
    this.announceName.gradientTop = this.labelSettings[(int) type].topColor;
    this.announceName.gradientBottom = this.labelSettings[(int) type].bottomColor;
    this.announceName.effectColor = this.labelSettings[(int) type].effectColor;
    this.announceEffect.text = this.labelSettings[(int) type].text;
    this.playerName.text = charaName;
    this.announceName.fontStyle = this.style;
    this.announceEffect.fontStyle = this.style;
    this.playerName.fontStyle = this.style;
  }

  protected override void OnStart() => ((Component) this).gameObject.SetActive(false);

  protected override void OnAfterAnimation() => ((Component) this).gameObject.SetActive(false);

  public enum ANNOUNCE_TYPE
  {
    DEAD,
    RETIRE,
    STONE,
    CONTINUE,
    RESCURE,
    AUTO_REVIVE,
    REACH_NEXT_WAVE,
    RESCUE_STONE,
    MAX,
  }

  [Serializable]
  public class LabelSettings
  {
    public string text;
    public Color topColor;
    public Color bottomColor;
    public Color effectColor;
  }
}
