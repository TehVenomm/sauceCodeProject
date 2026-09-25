// Decompiled with JetBrains decompiler
// Type: GuildRequestChallengeCounter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class GuildRequestChallengeCounter : QuestChallengeSelect
{
  public override void Initialize() => base.Initialize();

  public override void OnQuery_SELECT_ORDER()
  {
    int eventData = (int) GameSection.GetEventData();
    if (eventData < 0 || eventData >= this.challengeData.Length)
      GameSection.StopEvent();
    else if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog((uint) this.challengeData[eventData].questId))
    {
      GameSection.StopEvent();
    }
    else
    {
      MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID((uint) this.challengeData[eventData].questId);
      GameSection.SetEventData((object) MonoBehaviourSingleton<QuestManager>.I.GetQuestChallengeInfoData((uint) this.challengeData[eventData].questId));
      this.isScrollViewReady = false;
    }
  }

  private void OnCloseDialog_GuildRequestChallengeRoomCondition()
  {
    this.OnCloseDialog_QuestAcceptChallengeRoomCondition();
  }
}
