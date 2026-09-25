// Decompiled with JetBrains decompiler
// Type: QuestAcceptShadowCountDetailGP
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class QuestAcceptShadowCountDetailGP : GameSection
{
  private const int ADD_MSG_HEIGHT = 123;

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) QuestAcceptShadowCountDetailGP.UI.LBL_DATE, $"{MonoBehaviourSingleton<PartyManager>.I.challengeInfo.oldShadowCount.startDate} 〜\n{MonoBehaviourSingleton<PartyManager>.I.challengeInfo.oldShadowCount.endDate}");
    this.SetLabelText((Enum) QuestAcceptShadowCountDetailGP.UI.LBL_DESCRIPTION, StringTable.Get(STRING_CATEGORY.SHADOW_COUNT, 4U));
    this.GetComponent<UITable>((Enum) QuestAcceptShadowCountDetailGP.UI.TBL_CONTENTS).Reposition();
    Transform ctrl = this.GetCtrl((Enum) QuestAcceptShadowCountDetailGP.UI.SPR_FRAME);
    int num = 0;
    int childCount = this.GetCtrl((Enum) QuestAcceptShadowCountDetailGP.UI.TBL_CONTENTS).childCount;
    for (int index = 0; index < childCount; ++index)
    {
      Transform child = this.GetCtrl((Enum) QuestAcceptShadowCountDetailGP.UI.TBL_CONTENTS).GetChild(index);
      if (((Component) child).gameObject.activeSelf)
        num += ((Component) child).GetComponent<UIWidget>().height;
    }
    this.SetHeight((Enum) QuestAcceptShadowCountDetailGP.UI.SPR_FRAME, (int) ((double) (123 + Mathf.Max(num, 0)) / (double) ctrl.localScale.y));
    this.UpdateAnchors();
    base.UpdateUI();
  }

  private enum UI
  {
    SPR_FRAME,
    TBL_CONTENTS,
    LBL_DATE,
    LBL_DESCRIPTION,
  }
}
