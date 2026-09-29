// Decompiled with JetBrains decompiler
// Type: UIPlayerAnnounce
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class UIPlayerAnnounce : UIAnnounceBase<UIPlayerAnnounce>
{
  [SerializeField]
  protected UILabel playerName;
  [SerializeField]
  protected UILabel announceName;
  [SerializeField]
  protected UILabel announceEffect;
  [SerializeField]
  protected UIPlayerAnnounce.LabelSettings[] labelSettings = new UIPlayerAnnounce.LabelSettings[9];

  public void Announce(UIPlayerAnnounce.ANNOUNCE_TYPE type, Player player)
  {
    ((Component) this).gameObject.SetActive(true);
    if (!this.AnnounceStart(player))
      return;
    this.announceName.text = this.labelSettings[(int) type].text;
    this.announceName.gradientTop = this.labelSettings[(int) type].topColor;
    this.announceName.gradientBottom = this.labelSettings[(int) type].bottomColor;
    this.announceEffect.text = this.labelSettings[(int) type].text;
    this.playerName.text = player.charaName;
    this.announceName.fontStyle = this.style;
    this.announceEffect.fontStyle = this.style;
    this.playerName.fontStyle = this.style;
  }

  public void StartSkill(string skill_name, Player player)
  {
    ((Component) this).gameObject.SetActive(true);
    if (!this.AnnounceStart(player))
      return;
    this.announceName.text = skill_name;
    this.announceName.gradientTop = this.labelSettings[3].topColor;
    this.announceName.gradientBottom = this.labelSettings[3].bottomColor;
    this.announceEffect.text = skill_name;
    this.playerName.text = player.charaName;
    this.announceName.fontStyle = this.style;
    this.announceEffect.fontStyle = this.style;
    this.playerName.fontStyle = this.style;
  }

  protected override void OnStart() => ((Component) this).gameObject.SetActive(false);

  protected override void OnAfterAnimation() => ((Component) this).gameObject.SetActive(false);

  public enum ANNOUNCE_TYPE
  {
    REGION,
    WEAK,
    DOWN,
    SKILL,
    LEVEL_UP,
    SHIELD_ON,
    SHIELD_OFF,
    DRAGON_ARMOR,
    GIMMICK_EVOLVE,
    MAX,
  }

  [Serializable]
  public class LabelSettings
  {
    public string text;
    public Color topColor;
    public Color bottomColor;
  }
}
