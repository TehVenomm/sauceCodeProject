// Decompiled with JetBrains decompiler
// Type: HomeStageEventBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class HomeStageEventBase : MonoBehaviour
{
  public string eventName;
  public object eventData;

  protected virtual void Awake() => ((Component) this).gameObject.layer = this.GetLayer();

  protected virtual int GetLayer() => 0;

  public void DispatchEvent()
  {
    if (HomeBase.OnAfterGacha2Tutorial && !this.eventName.Contains("QUEST_COUNTER"))
      return;
    if (this.eventName.Contains("QUEST_COUNTER"))
    {
      if (TutorialStep.HasFirstDeliveryCompleted() && !TutorialStep.HasAllTutorialCompleted() || MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() || Object.op_Inequality((Object) TutorialMessage.GetCursor(), (Object) null))
        return;
    }
    else if (!TutorialStep.HasAllTutorialCompleted() || MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() || Object.op_Inequality((Object) TutorialMessage.GetCursor(), (Object) null))
      return;
    if (this.eventName.Contains("EVENT_COUNTER") && (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < 20)
    {
      MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (HomeStageEventBase), ((Component) this).gameObject, "QUEST_LOCK");
    }
    else
    {
      if (this.eventName.Contains("POINT_SHOP") || this.eventName.Contains("ARENA_LIST") && (!MonoBehaviourSingleton<UserInfoManager>.I.isArenaOpen || (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < 50) || this.eventName.Contains("BINGO") && !MonoBehaviourSingleton<QuestManager>.I.IsBingoPlayableEventExist() || !MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
        return;
      MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (HomeStageEventBase), ((Component) this).gameObject, this.eventName, this.eventData);
    }
  }
}
