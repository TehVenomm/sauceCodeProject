// Decompiled with JetBrains decompiler
// Type: QuestAcceptStatusEquipCopySetList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class QuestAcceptStatusEquipCopySetList : StatusEquipCopySetList
{
  private void OnQuery_QuestAcceptStatusOrderEquipCopyConfirm_YES() => this.OrderEquipSetCopy();

  private void OnQuery_QuestAcceptStatusEquipingCopyConfirm_YES() => this.EquipSetCopy();

  private void OnQuery_QuestAcceptStatusOrderEquipingCopyConfirm_YES() => this.OrderEquipSetCopy();

  protected override void TO_EQUIP_TOP()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("QuestAccept", "QuestAcceptSeriesArenaRoomChangeEquipSet");
    this.EquipSetCopy();
  }

  protected override void EquipSetCopy() => base.EquipSetCopy();
}
