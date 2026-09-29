// Decompiled with JetBrains decompiler
// Type: QuestAcceptSearchRoomCondition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class QuestAcceptSearchRoomCondition : QuestSearchRoomCondition
{
  public override void Initialize()
  {
    base.Initialize();
    this.SetActive((Enum) QuestSearchRoomCondition.UI.PRIORITY_ROOT, true);
    UIWidget component = this.GetComponent<UIWidget>((Enum) QuestSearchRoomCondition.UI.OBJ_FRAME);
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.height = 722;
    ((Component) this).transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    component.UpdateAnchors();
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    this.UpdatePriorityToggles();
  }

  private void UpdatePriorityToggles()
  {
    this.SetToggle(this.GetCtrl((Enum) QuestSearchRoomCondition.UI.TGL_BTN_FRIEND), this.searchRequest.isFs == 1);
    this.SetToggle(this.GetCtrl((Enum) QuestSearchRoomCondition.UI.TGL_BTN_CLAN), this.searchRequest.isCs == 1);
  }
}
