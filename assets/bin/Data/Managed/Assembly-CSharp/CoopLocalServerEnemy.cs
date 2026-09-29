// Decompiled with JetBrains decompiler
// Type: CoopLocalServerEnemy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class CoopLocalServerEnemy
{
  private CoopLocalServerEnemyPop owner;
  private float popTime;

  public int sid { get; private set; }

  public int popIndex => this.owner.popIndex;

  public uint enemyId => this.owner.data.enemyID;

  public bool bossFlag => this.owner.data.bossFlag;

  public bool bigMonsterFlag => this.owner.data.bigMonsterFlag;

  public CoopLocalServerEnemy(CoopLocalServerEnemyPop epop, float pop_time)
  {
    this.owner = epop;
    this.popTime = pop_time;
  }

  public bool IsPop() => this.sid > 0;

  public bool IsReady() => (double) this.popTime <= (double) Time.time;

  public void Pop(int sid) => this.sid = sid;

  public void Out(float pop_time)
  {
    this.sid = 0;
    this.popTime = pop_time;
  }

  public override string ToString()
  {
    return $"sid={(object) this.sid},idx={(object) this.popIndex},enemyId={(object) this.enemyId},popTime={(object) this.popTime}/{this.IsReady().ToString()}";
  }
}
