// Decompiled with JetBrains decompiler
// Type: SmithGrowResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class SmithGrowResult : EquipResultBase
{
  private bool direction;

  public override void Initialize()
  {
    this.smithType = SmithEquipBase.SmithType.GROW;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    if (!this.direction)
    {
      EquipItemInfo itemData = this.resultData.itemData as EquipItemInfo;
      if (this.resultData.isExceed && itemData != null && itemData.exceed > 0)
        this.StartExceedDirection(new string[1]
        {
          itemData.tableData.GetExceedParamName(itemData.exceed)
        });
      else
        this.OnFinishedAddAbilityDirection();
      this.direction = true;
    }
    base.UpdateUI();
  }

  private void OnQuery_NEXT()
  {
    MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithGrowData>().selectEquipData = this.resultData.itemData as EquipItemInfo;
  }

  private void OnQuery_NEXT_EVOLVE()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("SmithGrowItemSelect"))
      return;
    GameSection.SetEventData((object) new object[2]
    {
      (object) SmithEquipBase.SmithType.GROW,
      (object) (this.resultData.itemData as EquipItemInfo).tableData.type
    });
  }

  private void OnQuery_TO_SELECT()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("SmithGrowItemSelect"))
      return;
    GameSection.StopEvent();
    this.TO_UNIQUE_OR_MAIN_STATUS();
  }
}
