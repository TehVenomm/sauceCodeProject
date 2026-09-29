// Decompiled with JetBrains decompiler
// Type: GuildRequestChallengeRoomCondition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class GuildRequestChallengeRoomCondition : QuestAcceptChallengeRoomCondition
{
  public override void UpdateUI()
  {
    base.UpdateUI();
    ((Component) this.GetCtrl((Enum) GuildRequestChallengeRoomCondition.UI.POP_TARGET_LEVEL)).GetComponent<UIButton>().isEnabled = false;
  }

  protected override QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam GetInitChallengeSearchParam()
  {
    if (!(GameSection.GetEventData() is QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam challengeSearchParam))
      challengeSearchParam = new QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam();
    challengeSearchParam.enemyLevel = MonoBehaviourSingleton<UserInfoManager>.I.GetEnemyLevelFromUserLevel();
    return challengeSearchParam;
  }

  public new enum UI
  {
    BTN_N,
    BTN_HN,
    BTN_R,
    BTN_HR,
    BTN_SR,
    BTN_HSR,
    BTN_SSR,
    TGL_ORDER,
    TGL_EVENT,
    TGL_STORY,
    POP_TARGET_ENEMY_TYPE,
    LBL_TARGET_ENEMY_TYPE,
    BTN_FIRE,
    BTN_WATER,
    BTN_THUNDER,
    BTN_SOIL,
    BTN_LIGHT,
    BTN_DARK,
    POP_TARGET_MIN_LEVEL,
    POP_TARGET_MAX_LEVEL,
    LBL_TARGET_MIN_LEVEL,
    LBL_TARGET_MAX_LEVEL,
    OBJ_SEARCH,
    OBJ_MY_SEARCH,
    POP_TARGET_LEVEL,
    LBL_TARGET_LEVEL,
  }
}
