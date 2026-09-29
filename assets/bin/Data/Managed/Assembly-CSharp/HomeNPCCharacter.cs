// Decompiled with JetBrains decompiler
// Type: HomeNPCCharacter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class HomeNPCCharacter : HomeCharacterBase
{
  private NPCTable.NPCData npcData;
  private const float LoungeBorudonScaleRate = 1.3f;

  public OutGameSettingsManager.HomeScene.NPC npcInfo { get; private set; }

  public PLCA nearAnim { get; private set; }

  public void SetNPCInfo(OutGameSettingsManager.HomeScene.NPC npcInfo) => this.npcInfo = npcInfo;

  public void SetNPCData(NPCTable.NPCData data) => this.npcData = data;

  protected override ModelLoaderBase LoadModel()
  {
    bool useSpecialModel = false;
    HomeThemeTable.HomeThemeData homeThemeData = Singleton<HomeThemeTable>.I.GetHomeThemeData(Singleton<HomeThemeTable>.I.CurrentHomeTheme);
    if (homeThemeData != null && (this.npcData.specialModelID > 0 || homeThemeData.name != "NORMAL"))
      useSpecialModel = true;
    return this.npcData.LoadModel(((Component) this).gameObject, true, true, (Action<Animator>) null, useSpecialModel);
  }

  protected override void InitCollider()
  {
    if (!string.IsNullOrEmpty(this.npcInfo.eventName))
      base.InitCollider();
    else
      this.SetCollider(0.3f, 0.1f);
  }

  protected override void ChangeScale()
  {
    if (!MonoBehaviourSingleton<LoungeManager>.IsValid() || this.npcInfo.npcID != 4)
      return;
    Vector3 localScale = ((Component) this).transform.localScale;
    float num = 1.3f;
    ((Component) this).transform.localScale = new Vector3(localScale.x * num, localScale.y * num, localScale.z * num);
  }

  protected override void InitAnim()
  {
    PLCA default_anim = PLCA.IDLE_01;
    string loopAnim = this.npcInfo.GetLoopAnim();
    if (!string.IsNullOrEmpty(loopAnim))
      default_anim = PlayerAnimCtrl.StringToEnum(loopAnim);
    this.animCtrl = PlayerAnimCtrl.Get(this.animator, default_anim, new Action<PlayerAnimCtrl, PLCA>(((HomeCharacterBase) this).OnAnimPlay), on_end: new Action<PlayerAnimCtrl, PLCA>(((HomeCharacterBase) this).OnAnimEnd));
    string nearAnim = this.npcInfo.GetNearAnim();
    if (!string.IsNullOrEmpty(nearAnim))
      this.nearAnim = PlayerAnimCtrl.StringToEnum(nearAnim);
    else
      this.nearAnim = PLCA.IDLE_01;
  }

  public void Play(PLCA anim, bool instant)
  {
    if (Object.op_Equality((Object) this.animCtrl, (Object) null))
      this.InitAnim();
    this.animCtrl.Play(anim, instant);
  }

  public override bool DispatchEvent()
  {
    if (!TutorialStep.HasAllTutorialCompleted() || MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() || Object.op_Inequality((Object) TutorialMessage.GetCursor(), (Object) null) || HomeBase.OnAfterGacha2Tutorial || this.state != HomeCharacterBase.STATE.FREE || this.npcInfo == null || string.IsNullOrEmpty(this.npcInfo.eventName))
      return false;
    MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (HomeNPCCharacter), ((Component) this).gameObject, this.npcInfo.eventName);
    return true;
  }

  public void SetQuestBalloon(Transform t)
  {
    ((Object) t).name = HomeBase.QuestBalloonName;
    this.namePlate = t;
  }

  protected override bool IsVisibleNamePlate()
  {
    return this.state == HomeCharacterBase.STATE.FREE && base.IsVisibleNamePlate();
  }

  public bool IsLeaveState() => this.state == HomeCharacterBase.STATE.LEAVE;

  public void HideShadow()
  {
    NPCLoader loader = this.loader as NPCLoader;
    if (Object.op_Implicit((Object) loader))
    {
      if (!Object.op_Implicit((Object) loader.shadow))
        return;
      ((Component) loader.shadow).gameObject.SetActive(false);
    }
    else
    {
      Transform transform = this._transform.Find("CircleShadow");
      if (!Object.op_Inequality((Object) null, (Object) transform))
        return;
      ((Component) transform).gameObject.SetActive(false);
    }
  }
}
