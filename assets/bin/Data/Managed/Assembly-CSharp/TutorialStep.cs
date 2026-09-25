// Decompiled with JetBrains decompiler
// Type: TutorialStep
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class TutorialStep
{
  public static bool isSendFirstRewardComplete;
  public static bool isChangeLocalEquip;

  public static bool HasAllTutorialCompleted() => TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.END);

  public static bool HasFirstDeliveryCompleted()
  {
    return TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.WORK_SHOP_06);
  }

  public static bool HasChangeEquipCompleted()
  {
    return TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.CHANGE_EQUIP_08);
  }

  public static bool HasDeliveryRewardCompleted()
  {
    return TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.DELIVERY_REWARD_05);
  }

  public static bool IsTheTutorialOver(TUTORIAL_STEP step)
  {
    return MonoBehaviourSingleton<UserInfoManager>.IsValid() && (TUTORIAL_STEP) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep >= step;
  }

  public static bool HasQuestSpecialUnlocked() => TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.END);

  public static bool HasDailyBonusUnlocked() => TutorialStep.HasChangeEquipCompleted();

  public static bool IsPlayingFirstAccept()
  {
    return TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.USER_CREATE_02) && !TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.ENTER_FIELD_03);
  }

  public static bool IsPlayingFirstDelivery()
  {
    return TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.ENTER_FIELD_03) && !TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.DELIVERY_COMPLETE_04);
  }

  public static bool IsPlayingFirstBackHome()
  {
    return TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.DELIVERY_COMPLETE_04) && !TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.DELIVERY_REWARD_05);
  }

  public static bool IsPlayingFirstReward()
  {
    return TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.DELIVERY_REWARD_05) && !TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.WORK_SHOP_06);
  }

  public static bool IsPlayingStudioTutorial()
  {
    return TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.DELIVERY_REWARD_05) && !TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.CHANGE_EQUIP_08);
  }
}
