// Decompiled with JetBrains decompiler
// Type: UIEnemyAnnounce
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Text;
using UnityEngine;

#nullable disable
public class UIEnemyAnnounce : UIAnnounceBase<UIEnemyAnnounce>
{
  [SerializeField]
  protected UILabel announce;
  [SerializeField]
  protected UILabel announceEffect;
  protected StringBuilder stateNameBuilder = new StringBuilder(1024 /*0x0400*/);

  protected override float GetDispSec() => 4.5f;

  public void RequestAnnounce(string enemyName, STRING_CATEGORY category, uint stringID)
  {
    ((Component) this).gameObject.SetActive(true);
    if (!this.AnnounceStart())
      return;
    this.SetupAnnounceInfo(enemyName, category, stringID);
  }

  public void StartBuff(string enemy_name, BuffParam.BUFFTYPE type)
  {
    if (!this.IsAnnounce(type))
      return;
    ((Component) this).gameObject.SetActive(true);
    if (!this.AnnounceStart())
      return;
    this.SetupAnnounceInfo(enemy_name, STRING_CATEGORY.BUFF, (uint) type);
  }

  public void EndBuff(string enemy_name, BuffParam.BUFFTYPE type)
  {
    if (!this.IsAnnounce(type))
      return;
    ((Component) this).gameObject.SetActive(true);
    if (!this.AnnounceStart())
      return;
    this.stateNameBuilder.Length = 0;
    this.stateNameBuilder.Append(enemy_name);
    this.stateNameBuilder.Append(" ");
    this.stateNameBuilder.Append(StringTable.Get(STRING_CATEGORY.BUFF, (uint) type));
    this.stateNameBuilder.Append(StringTable.Get(STRING_CATEGORY.BUFF, 9999U));
    string str = this.stateNameBuilder.ToString();
    this.announce.text = str;
    this.announceEffect.text = str;
    this.announce.fontStyle = this.style;
    this.announceEffect.fontStyle = this.style;
  }

  private void SetupAnnounceInfo(string enemyName, STRING_CATEGORY category, uint stringID)
  {
    this.stateNameBuilder.Length = 0;
    this.stateNameBuilder.Append(enemyName);
    this.stateNameBuilder.Append(" ");
    this.stateNameBuilder.Append(StringTable.Get(category, stringID));
    string str = this.stateNameBuilder.ToString();
    this.announce.text = str;
    this.announceEffect.text = str;
    this.announce.fontStyle = this.style;
    this.announceEffect.fontStyle = this.style;
  }

  private bool IsAnnounce(BuffParam.BUFFTYPE type)
  {
    return type != BuffParam.BUFFTYPE.POISON && type != BuffParam.BUFFTYPE.BURNING && type != BuffParam.BUFFTYPE.DEADLY_POISON && type != BuffParam.BUFFTYPE.GHOST_FORM && type != BuffParam.BUFFTYPE.ELECTRIC_SHOCK && type != BuffParam.BUFFTYPE.INK_SPLASH && type != BuffParam.BUFFTYPE.DEFDOWN_RATE_NORMAL && type != BuffParam.BUFFTYPE.DEFDOWN_RATE_FIRE && type != BuffParam.BUFFTYPE.DEFDOWN_RATE_WATER && type != BuffParam.BUFFTYPE.DEFDOWN_RATE_THUNDER && type != BuffParam.BUFFTYPE.DEFDOWN_RATE_SOIL && type != BuffParam.BUFFTYPE.DEFDOWN_RATE_LIGHT && type != BuffParam.BUFFTYPE.DEFDOWN_RATE_DARK && type != BuffParam.BUFFTYPE.DEFDOWN_RATE_ALLELEMENT && type != BuffParam.BUFFTYPE.MAD_MODE && type != BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN && type != BuffParam.BUFFTYPE.MOVE_SPEED_DOWN && type != BuffParam.BUFFTYPE.EROSION && type != BuffParam.BUFFTYPE.SOIL_SHOCK && type != BuffParam.BUFFTYPE.ACID && type != BuffParam.BUFFTYPE.DAMAGE_MOTION_STOP && type != BuffParam.BUFFTYPE.CORRUPTION && type != BuffParam.BUFFTYPE.STIGMATA && type != BuffParam.BUFFTYPE.CYCLONIC_THUNDERSTORM;
  }

  public void RequestFieldBuffAnnounce()
  {
    uint currentFieldBuffId = MonoBehaviourSingleton<FieldManager>.I.currentFieldBuffId;
    if (currentFieldBuffId <= 0U)
      return;
    FieldBuffTable.FieldBuffData data = Singleton<FieldBuffTable>.I.GetData(currentFieldBuffId);
    if (data == null)
      return;
    ((Component) this).gameObject.SetActive(true);
    if (!this.AnnounceStart())
      return;
    string str = string.Format(StringTable.Get(STRING_CATEGORY.IN_GAME, 150U), (object) data.name);
    this.announce.text = str;
    this.announceEffect.text = str;
    this.announce.fontStyle = this.style;
    this.announceEffect.fontStyle = this.style;
  }

  public void RequestTextAnnounce(string text)
  {
    ((Component) this).gameObject.SetActive(true);
    if (!this.AnnounceStart())
      return;
    this.announce.text = text;
    this.announceEffect.text = text;
    this.announce.fontStyle = this.style;
    this.announceEffect.fontStyle = this.style;
  }

  protected override void OnStart() => ((Component) this).gameObject.SetActive(false);

  protected override void OnAfterAnimation() => ((Component) this).gameObject.SetActive(false);
}
