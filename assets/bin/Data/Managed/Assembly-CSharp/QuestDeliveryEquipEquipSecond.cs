// Decompiled with JetBrains decompiler
// Type: QuestDeliveryEquipEquipSecond
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class QuestDeliveryEquipEquipSecond : QuestAcceptEquipSecond
{
  private void OnCloseDialog_QuestAcceptEquipSort() => this.OnCloseSortDialog();

  private void OnQuery_QuestDeliveryEquipMigrationSkillConfirm_YES()
  {
    this.OnQuery_StatusMigrationSkillConfirm_YES();
  }

  private void OnQuery_QuestDeliveryEquipMigrationSkillConfirm_NO()
  {
    this.OnQuery_StatusMigrationSkillConfirm_NO();
  }

  private void OnQuery_QuestDeliveryEquipSwapEquipConfirm_YES()
  {
    this.OnQuery_StatusSwapEquipConfirm_YES();
  }
}
