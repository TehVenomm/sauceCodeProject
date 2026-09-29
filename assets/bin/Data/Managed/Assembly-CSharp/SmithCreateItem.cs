// Decompiled with JetBrains decompiler
// Type: SmithCreateItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class SmithCreateItem : EquipGenerateBase
{
  public override void Initialize()
  {
    SmithManager.SmithCreateData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
    this.smithType = SmithEquipBase.SmithType.GENERATE;
    GameSection.SetEventData((object) smithData.generateTableData);
    base.Initialize();
    EquipItemTable.EquipItemData equipTableData = this.GetEquipTableData();
    if (equipTableData == null)
      return;
    MonoBehaviourSingleton<UIManager>.I.common.AttachCaption((UIBehaviour) this, this.sectionData.backButtonIndex, !equipTableData.IsWeapon() ? this.sectionData.GetText("CAPTION_DEFENCE") : this.sectionData.GetText("CAPTION_WEAPON"));
  }

  protected override void InitNeedMaterialData()
  {
    SmithManager.SmithCreateData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
    this.needMaterial = this.MaterialSort(smithData.createEquipItemTable.needMaterial);
    this.needMoney = (int) smithData.createEquipItemTable.needMoney;
    this.CheckNeedMaterialNumFromInventory();
  }

  protected override void OnQuery_START()
  {
    if (MonoBehaviourSingleton<AchievementManager>.I.CheckEquipItemCollection(this.GetEquipTableData()))
      GameSection.ChangeEvent("START_REGISTED");
    base.OnQuery_START();
  }

  protected void OnQuery_LOTTERY_LIST()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>(),
      (object) SmithEquipBase.SmithType.GENERATE
    });
  }

  private void OnQuery_SmithConfirmCreate_YES() => this.OnQueryConfirmYES();

  private void OnQuery_SmithConfirmCreateRegisted_YES() => this.OnQueryConfirmYES();

  protected override uint GetCreateEquiptableID()
  {
    return MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>().createEquipItemTable.id;
  }

  private void OnQuery_SECTION_BACK()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("SmithCreateItemSelect"))
      return;
    GameSection.StopEvent();
    this.TO_UNIQUE_OR_MAIN_STATUS();
  }
}
