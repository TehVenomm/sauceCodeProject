// Decompiled with JetBrains decompiler
// Type: QuestSearchRoomConditionBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public abstract class QuestSearchRoomConditionBase : GameSection
{
  protected abstract void LoadSearchRequestParam();

  protected abstract void CopySearchRequestParam();

  protected abstract void SetCondition();

  protected abstract void SendSearch();

  protected abstract void SendRandomMatching();

  protected virtual void OnQuery_SEARCH()
  {
    this.SetCondition();
    this.SendSearch();
  }

  protected virtual void OnQuery_MATCHING()
  {
    this.SetCondition();
    this.SendRandomMatching();
  }

  private void OnQuery_QuestAcceptSearchMatchingFailed_YES() => this.SendRandomMatching();

  protected void OnNotFoundMatchingParty()
  {
    GameSection.ChangeStayEvent("NOT_FOUND_MATCHING_PARTY");
  }

  protected void OnNotFoundQuest() => GameSection.ChangeStayEvent("NOT_FOUND_QUEST");
}
