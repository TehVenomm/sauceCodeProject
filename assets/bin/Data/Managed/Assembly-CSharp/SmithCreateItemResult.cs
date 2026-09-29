// Decompiled with JetBrains decompiler
// Type: SmithCreateItemResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class SmithCreateItemResult : EquipResultBase
{
  private bool direction;

  public override void Initialize()
  {
    this.smithType = SmithEquipBase.SmithType.GENERATE;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    if (!this.direction)
    {
      EquipItemInfo itemData = this.resultData.itemData as EquipItemInfo;
      if (itemData.GetValidLotAbility() > 0)
        this.StartAddAbilityDirection(itemData.GetValidAbility());
      else
        this.OnFinishedAddAbilityDirection();
      this.direction = true;
    }
    base.UpdateUI();
  }

  public void OnQuery_TO_SELECT() => this.TO_UNIQUE_OR_MAIN_STATUS();
}
