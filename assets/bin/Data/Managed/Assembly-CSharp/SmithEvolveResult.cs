// Decompiled with JetBrains decompiler
// Type: SmithEvolveResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class SmithEvolveResult : EquipResultBase
{
  private bool direction;

  public override void Initialize()
  {
    this.smithType = SmithEquipBase.SmithType.EVOLVE;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    if (!this.direction)
    {
      this.OnFinishedAddAbilityDirection();
      this.direction = true;
    }
    base.UpdateUI();
  }

  private void OnQuery_TO_SELECT()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("SmithGrowItemSelect"))
      return;
    GameSection.StopEvent();
    this.TO_UNIQUE_OR_MAIN_STATUS();
  }
}
