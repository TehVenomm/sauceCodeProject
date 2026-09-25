// Decompiled with JetBrains decompiler
// Type: QuestAcceptRoomUserInfoDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class QuestAcceptRoomUserInfoDetail : QuestRoomUserInfoDetail
{
  public override void Initialize() => base.Initialize();

  protected void OnQuery_QuestAcceptRoomInvalid_EquipChange_OK()
  {
    this.OnQuery_QuestRoomInvalid_EquipChange_OK();
  }

  private new void OnEnable()
  {
    InputManager.OnDragAlways += new InputManager.OnTouchDelegate(this.OnDrag);
  }

  private new void OnDisable()
  {
    InputManager.OnDragAlways -= new InputManager.OnTouchDelegate(this.OnDrag);
    this.nowSectionName = string.Empty;
  }

  private void OnDrag(InputManager.TouchInfo touch_info)
  {
    if (Object.op_Equality((Object) this.loader, (Object) null) || MonoBehaviourSingleton<UIManager>.I.IsDisable() || !this.CanRotateSection())
      return;
    ((Component) this.loader).transform.Rotate(GameDefine.GetCharaRotateVector(touch_info));
  }

  private bool CanRotateSection()
  {
    return MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == nameof (QuestAcceptRoomUserInfoDetail);
  }
}
