// Decompiled with JetBrains decompiler
// Type: GachaSkillBannerAnim
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using UnityEngine;

#nullable disable
public class GachaSkillBannerAnim : UIBehaviour
{
  private GachaSkillBannerAnimPattern[] pattern;
  private int targetIndex;

  public void Init(
    int anim_index,
    SkillItemTable.SkillItemData table,
    Texture tex,
    GachaList.GachaPickupAnim anim)
  {
    if (this.pattern == null)
      this.pattern = ((Component) this).GetComponentsInChildren<GachaSkillBannerAnimPattern>();
    if (this.pattern == null || this.pattern.Length <= anim_index)
    {
      Log.Error("Skill Gacha Anim Pattern is not Found!");
      if (this.pattern == null)
        return;
      Log.Error("index = " + (object) anim_index);
    }
    else
    {
      int index = 0;
      for (int length = this.pattern.Length; index < length; ++index)
        this.pattern[index].Finish();
      GachaSkillBannerAnimPattern bannerAnimPattern = this.pattern[anim_index];
      this.targetIndex = anim_index;
      int targetIndex = this.targetIndex;
      SkillItemTable.SkillItemData table1 = table;
      Texture tex1 = tex;
      GachaList.GachaPickupAnim anim1 = anim;
      bannerAnimPattern.Init(targetIndex, table1, tex1, anim1);
    }
  }

  public void Entry(bool is_skip, EventDelegate.Callback end_callback)
  {
    if (this.pattern == null)
      return;
    this.pattern[this.targetIndex].AnimStart(true, is_skip, end_callback);
  }

  public void WaitAndNextPickup(EventDelegate.Callback end_callback)
  {
    if (this.pattern == null)
      return;
    this.pattern[this.targetIndex].AnimStart(false, false, end_callback);
  }
}
