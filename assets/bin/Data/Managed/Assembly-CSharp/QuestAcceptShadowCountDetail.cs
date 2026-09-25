// Decompiled with JetBrains decompiler
// Type: QuestAcceptShadowCountDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class QuestAcceptShadowCountDetail : GameSection
{
  private const int ADD_MSG_HEIGHT = 123;

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) QuestAcceptShadowCountDetail.UI.LBL_FIRST_SENTENSE, StringTable.Format(STRING_CATEGORY.SHADOW_COUNT, 0U, (object) MonoBehaviourSingleton<PartyManager>.I.challengeInfo.currentShadowCount.startDate));
    if (MonoBehaviourSingleton<PartyManager>.I.challengeInfo.IsRankingEvent())
    {
      this.SetActive((Enum) QuestAcceptShadowCountDetail.UI.LBL_SECOND_SENTENSE, true);
      this.SetActive((Enum) QuestAcceptShadowCountDetail.UI.PADDING_2, true);
      this.SetLabelText((Enum) QuestAcceptShadowCountDetail.UI.LBL_SECOND_SENTENSE, StringTable.Get(STRING_CATEGORY.SHADOW_COUNT, 1U));
    }
    else
    {
      this.SetActive((Enum) QuestAcceptShadowCountDetail.UI.LBL_SECOND_SENTENSE, false);
      this.SetActive((Enum) QuestAcceptShadowCountDetail.UI.PADDING_2, false);
    }
    this.SetLabelText((Enum) QuestAcceptShadowCountDetail.UI.LBL_DESCRIPTION, StringTable.Get(STRING_CATEGORY.SHADOW_COUNT, 2U));
    this.SetLabelText((Enum) QuestAcceptShadowCountDetail.UI.LBL_BONUS_NUM, MonoBehaviourSingleton<PartyManager>.I.challengeInfo.currentShadowCount.num.ToString());
    this.GetComponent<UITable>((Enum) QuestAcceptShadowCountDetail.UI.TBL_CONTENTS).Reposition();
    Transform ctrl = this.GetCtrl((Enum) QuestAcceptShadowCountDetail.UI.SPR_FRAME);
    int num = 0;
    int childCount = this.GetCtrl((Enum) QuestAcceptShadowCountDetail.UI.TBL_CONTENTS).childCount;
    for (int index = 0; index < childCount; ++index)
    {
      Transform child = this.GetCtrl((Enum) QuestAcceptShadowCountDetail.UI.TBL_CONTENTS).GetChild(index);
      if (((Component) child).gameObject.activeSelf)
        num += ((Component) child).GetComponent<UIWidget>().height;
    }
    this.SetHeight((Enum) QuestAcceptShadowCountDetail.UI.SPR_FRAME, (int) ((double) (123 + Mathf.Max(num, 0)) / (double) ctrl.localScale.y));
    this.UpdateAnchors();
    base.UpdateUI();
  }

  private enum UI
  {
    SPR_FRAME,
    TBL_CONTENTS,
    LBL_BONUS_NUM,
    LBL_FIRST_SENTENSE,
    PADDING_1,
    LBL_SECOND_SENTENSE,
    PADDING_2,
    PADDING_3,
    LBL_DESCRIPTION,
  }
}
