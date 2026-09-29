// Decompiled with JetBrains decompiler
// Type: LoungeMoveNPC
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class LoungeMoveNPC : HomeCharacterBase
{
  private NPCTable.NPCData npcData;
  private OutGameSettingsManager.HomeScene.NPC npcInfo;

  public void SetNPCData(NPCTable.NPCData data) => this.npcData = data;

  public void SetNPCInfo(OutGameSettingsManager.HomeScene.NPC npcInfo) => this.npcInfo = npcInfo;

  protected override ModelLoaderBase LoadModel()
  {
    bool useSpecialModel = false;
    if (this.npcData.specialModelID > 0)
      useSpecialModel = true;
    return this.npcData.LoadModel(((Component) this).gameObject, true, true, (Action<Animator>) null, useSpecialModel);
  }

  protected override void InitAnim()
  {
    PLCA default_anim = PLCA.IDLE_01;
    string loopAnim = this.npcInfo.GetLoopAnim();
    if (!string.IsNullOrEmpty(loopAnim))
      default_anim = PlayerAnimCtrl.StringToEnum(loopAnim);
    this.animCtrl = PlayerAnimCtrl.Get(this.animator, default_anim, new Action<PlayerAnimCtrl, PLCA>(((HomeCharacterBase) this).OnAnimPlay), on_end: new Action<PlayerAnimCtrl, PLCA>(((HomeCharacterBase) this).OnAnimEnd));
  }

  protected override void InitCollider()
  {
  }
}
