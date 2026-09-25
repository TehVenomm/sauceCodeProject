// Decompiled with JetBrains decompiler
// Type: EnemyWaveStrategyController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class EnemyWaveStrategyController
{
  private const float RADIUS_WAVE_TARGET = 4f;
  private bool isActive;
  private Enemy owner;
  private EnemyActionController.ActionInfo moveActionInfo;

  public EnemyWaveStrategyController(Enemy enemy)
  {
    if (!QuestManager.IsValidInGameWaveStrategy())
    {
      this.isActive = false;
    }
    else
    {
      this.owner = enemy;
      this.moveActionInfo = new EnemyActionController.ActionInfo()
      {
        data = new EnemyActionTable.EnemyActionData()
      };
      this.moveActionInfo.data.atkRange = 1;
      this.moveActionInfo.data.name = "防衛対象オブジェクトへ移動";
      this.moveActionInfo.data.combiActionTypeInfos = EnemyActionTable.GetActionTypeInfos("rotate", "move");
      this.isActive = true;
    }
  }

  public bool IsActive() => this.isActive;

  public bool IsArrivedTarget()
  {
    return Object.op_Equality((Object) this.owner, (Object) null) || Object.op_Equality((Object) this.owner.actionTarget, (Object) null) || this.owner.IsArrivalPosition(this.owner.actionTarget._position, 4f);
  }

  public EnemyActionController.ActionInfo GetAlteredAction() => this.moveActionInfo;
}
