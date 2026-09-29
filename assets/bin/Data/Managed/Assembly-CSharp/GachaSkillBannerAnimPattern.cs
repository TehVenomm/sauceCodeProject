// Decompiled with JetBrains decompiler
// Type: GachaSkillBannerAnimPattern
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class GachaSkillBannerAnimPattern : UIBehaviour
{
  [SerializeField]
  private UILabel lblName;
  [SerializeField]
  private UILabel lblDescription;
  [SerializeField]
  private UILabel lblSubDescription;
  [SerializeField]
  private UILabel lblAddText_A;
  [SerializeField]
  private UILabel lblAddText_B;
  [SerializeField]
  private UITexture uiTex;
  [SerializeField]
  private UITweenCtrl entryAnimCtrl;
  private bool nowEntryAnim;

  public void Init(
    int index,
    SkillItemTable.SkillItemData table,
    Texture tex,
    GachaList.GachaPickupAnim anim)
  {
    GachaList.GachaPickupAnim.TextStyle name = anim.name;
    this.SetFontStyle(((Component) this.lblName).transform, name.italic == 1 ? (FontStyle) 2 : (FontStyle) 0);
    this.SetPatternText(this.lblName, table.name, name.size, name.toColor(), name.toOutColor());
    GachaList.GachaPickupAnim.TextStyle description = anim.description;
    this.SetFontStyle(((Component) this.lblDescription).transform, description.italic == 1 ? (FontStyle) 2 : (FontStyle) 0);
    this.SetPatternText(this.lblDescription, SkillItemInfo.GetExplanationText(table, 1, 0), description.size, description.toColor(), description.toOutColor());
    GachaList.GachaPickupAnim.TextStyle sub = anim.sub;
    this.SetFontStyle(((Component) this.lblSubDescription).transform, sub.italic == 1 ? (FontStyle) 2 : (FontStyle) 0);
    this.SetPatternText(this.lblSubDescription, sub.text, sub.size, sub.toColor(), sub.toOutColor());
    GachaList.GachaPickupAnim.TextStyle adda = anim.adda;
    this.SetFontStyle(((Component) this.lblAddText_A).transform, adda.italic == 1 ? (FontStyle) 2 : (FontStyle) 0);
    this.SetPatternText(this.lblAddText_A, adda.text, adda.size, adda.toColor(), adda.toOutColor());
    GachaList.GachaPickupAnim.TextStyle addb = anim.addb;
    this.SetFontStyle(((Component) this.lblAddText_B).transform, addb.italic == 1 ? (FontStyle) 2 : (FontStyle) 0);
    this.SetPatternText(this.lblAddText_B, addb.text, addb.size, addb.toColor(), addb.toOutColor());
    this.uiTex.mainTexture = tex;
    ((Component) this).gameObject.SetActive(true);
  }

  public void Finish()
  {
    ((Component) this).gameObject.SetActive(false);
    if (!Object.op_Inequality((Object) this.uiTex.mainTexture, (Object) null))
      return;
    this.uiTex.mainTexture = (Texture) null;
  }

  public void AnimStart(bool is_entry, bool is_skip, EventDelegate.Callback end_callback)
  {
    this.nowEntryAnim = is_entry;
    Transform t = this.nowEntryAnim ? ((Component) this.entryAnimCtrl).transform : ((Component) this).transform;
    this.ResetTween(t);
    if (is_skip)
    {
      this.SkipTween(t);
      if (end_callback != null)
        end_callback();
      this.nowEntryAnim = false;
    }
    else
      this.PlayTween(t, callback: end_callback, is_input_block: false);
  }

  private void SetPatternText(
    UILabel lbl,
    string text,
    int size,
    Color text_color,
    Color out_line_color)
  {
    if (!Object.op_Inequality((Object) lbl, (Object) null))
      return;
    lbl.text = text;
    lbl.color = text_color;
    lbl.effectColor = out_line_color;
    lbl.fontSize = size;
  }

  public void DebugAnimReset()
  {
    Array.ForEach<UITweener>(this.GetDirectionTweenCtrl().tweens, (Action<UITweener>) (tw => ((Behaviour) tw).enabled = false));
    Array.ForEach<UITweener>(this.GetEntryTweenCtrl().tweens, (Action<UITweener>) (tw => ((Behaviour) tw).enabled = false));
    this.ResetTween(((Component) this).transform);
    this.ResetTween(((Component) this.entryAnimCtrl).transform);
  }

  public UITweenCtrl GetDirectionTweenCtrl()
  {
    return this.GetComponent<UITweenCtrl>(((Component) this).transform);
  }

  public UITweenCtrl GetEntryTweenCtrl()
  {
    return this.GetComponent<UITweenCtrl>(((Component) this.entryAnimCtrl).transform);
  }
}
