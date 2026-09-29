// Decompiled with JetBrains decompiler
// Type: QuestResultDropIconOpener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class QuestResultDropIconOpener : MonoBehaviour
{
  [SerializeField]
  private UISprite sprite;
  [SerializeField]
  private TweenAlpha iconParent;
  [SerializeField]
  private UISprite spriteRewardCategory;
  private ItemIcon icon;
  private bool isInitialize;
  private QuestResultDropIconOpener.Info m_Info = new QuestResultDropIconOpener.Info();
  private Action<Transform, QuestResultDropIconOpener.Info, bool> loadEffCallback;
  private const string SPR_RARE_ICON = "ItemOpenerIcon_Gold";
  private const string SPR_NORMAL_ICON = "ItemOpenerIcon_Silver";
  private const string SPR_BREAK_ICON = "ItemOpenerIcon_Red";

  public void Initialized(
    ItemIcon _icon,
    QuestResultDropIconOpener.Info info,
    Action<Transform, QuestResultDropIconOpener.Info, bool> load_eff_callback)
  {
    if (Object.op_Equality((Object) this.iconParent, (Object) null))
      return;
    this.SetIcon(_icon);
    this.m_Info = info;
    this.loadEffCallback = load_eff_callback;
    this.SetSpriteRare();
    this.SetSpriteBreakReward(false);
    this.isInitialize = true;
  }

  public void StartEffect(bool is_skip)
  {
    if (!this.isInitialize)
      return;
    if (is_skip)
    {
      this.iconParent.duration = 0.0f;
      this.iconParent.delay = 0.0f;
    }
    this.loadEffCallback(this.icon._transform, this.m_Info, is_skip);
    ((Behaviour) this.sprite).enabled = false;
    this.OpenIcon();
  }

  private void SetIcon(ItemIcon _icon)
  {
    this.icon = _icon;
    this.icon.transform.parent = ((Component) this.iconParent).transform;
    this.icon.VisibleIcon(false);
  }

  private void SetSpriteRare()
  {
    string str = "ItemOpenerIcon_Silver";
    if (this.m_Info.IsBroken)
      str = "ItemOpenerIcon_Red";
    else if (this.m_Info.IsRare)
      str = "ItemOpenerIcon_Gold";
    this.sprite.spriteName = str;
  }

  private void OpenIcon()
  {
    this.iconParent.Play(true);
    this.icon.VisibleIcon(true);
    this.SetSpriteBreakReward(this.m_Info.IsBroken);
  }

  private void SetSpriteBreakReward(bool visible)
  {
    if (!Object.op_Inequality((Object) this.spriteRewardCategory, (Object) null))
      return;
    ((Component) this.spriteRewardCategory).gameObject.SetActive(visible);
  }

  public class Info
  {
    public bool IsRare;
    public bool IsBroken;
  }
}
