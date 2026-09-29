// Decompiled with JetBrains decompiler
// Type: EnemyEffectObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class EnemyEffectObject : MonoBehaviour
{
  private EnemyEffectObject.DELETE_CONDITION m_deleteCondition;
  private EnemyRegionWork m_enemyRegionWork;
  private Enemy m_targetEnemy;
  private string m_uniqueName = string.Empty;
  private bool m_isDeleted;

  public void Initialize(
    Enemy enemy,
    EnemyRegionWork regionWork,
    int deleteCondition,
    string uniqueName)
  {
    this.m_targetEnemy = enemy;
    this.m_enemyRegionWork = regionWork;
    this.m_deleteCondition = (EnemyEffectObject.DELETE_CONDITION) deleteCondition;
    this.m_uniqueName = uniqueName;
  }

  private void SelfDestroy()
  {
    if (Object.op_Inequality((Object) this.m_targetEnemy, (Object) null))
    {
      this.m_targetEnemy.OnNotifyDeleteEnemyEffect(this);
      this.m_targetEnemy = (Enemy) null;
    }
    EffectManager.ReleaseEffect(((Component) this).gameObject);
    this.m_isDeleted = true;
  }

  private void Update()
  {
    if (this.m_isDeleted)
      return;
    if (Object.op_Equality((Object) this.m_targetEnemy, (Object) null) || this.m_targetEnemy.isDead)
    {
      this.SelfDestroy();
    }
    else
    {
      bool flag = false;
      switch (this.m_deleteCondition)
      {
        case EnemyEffectObject.DELETE_CONDITION.BarrierHp:
          flag = (int) this.m_targetEnemy.BarrierHp <= 0;
          break;
        case EnemyEffectObject.DELETE_CONDITION.RegionHp:
          flag = (int) this.m_enemyRegionWork.hp <= 0;
          break;
        case EnemyEffectObject.DELETE_CONDITION.ShieldHp:
          flag = (int) this.m_targetEnemy.ShieldHp <= 0;
          break;
      }
      if (!flag)
        return;
      this.SelfDestroy();
    }
  }

  public string UniqueName => this.m_uniqueName;

  public enum DELETE_CONDITION
  {
    None,
    BarrierHp,
    RegionHp,
    ShieldHp,
  }
}
