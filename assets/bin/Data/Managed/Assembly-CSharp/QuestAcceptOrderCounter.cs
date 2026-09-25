// Decompiled with JetBrains decompiler
// Type: QuestAcceptOrderCounter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class QuestAcceptOrderCounter : QuestOrderSelect
{
  private void OnCloseDialog_QuestAcceptOrderCounterCondition()
  {
    if (!(GameSection.GetEventData() is QuestSearchRoomCondition.SearchRequestParam eventData) || eventData.order != 1)
      return;
    this.param = eventData;
    this.nowPage = 1;
    this.RefreshUI();
  }

  private void OnCloseDialog_QuestAcceptSort() => this._OnCloseDialogSort();
}
