// Decompiled with JetBrains decompiler
// Type: GrabController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GrabController
{
  private List<Player> grabbedPlayers = new List<Player>(4);
  private float duration;
  private float grabStartTime;

  public bool releaseByWeakHit { get; private set; }

  public bool releaseBySpWeakHit { get; private set; }

  public int releaseActionId { get; private set; }

  public DrainAttackInfo drainAtkInfo { get; private set; }

  public bool IsGrabing() => this.grabbedPlayers.Count > 0;

  public void AddGrabbedObject(Player player) => this.grabbedPlayers.Add(player);

  public void ReleaseAll(float angle, float power)
  {
    for (int index = 0; index < this.grabbedPlayers.Count; ++index)
    {
      if (!this.grabbedPlayers[index].isDead && this.grabbedPlayers[index].actionID == (Character.ACTION_ID) 29)
        this.grabbedPlayers[index].ActGrabbedEnd(angle, power);
    }
    this.grabbedPlayers.Clear();
    this.releaseByWeakHit = false;
    this.releaseBySpWeakHit = false;
  }

  private void Grab(
    float _duration,
    Player _player,
    int releaseActId,
    bool _releaseByWeakHit,
    bool _releaseBySpWeakHit,
    DrainAttackInfo _drainAtkInfo)
  {
    if (!this.IsGrabing())
    {
      this.grabStartTime = Time.realtimeSinceStartup;
      this.duration = _duration;
      this.releaseActionId = releaseActId;
      this.releaseByWeakHit = _releaseByWeakHit;
      this.releaseBySpWeakHit = _releaseBySpWeakHit;
      this.drainAtkInfo = _drainAtkInfo;
    }
    this.AddGrabbedObject(_player);
  }

  public void Grab(Player _player, GrabInfo info, DrainAttackInfo _drainAtkInfo)
  {
    this.Grab(info.duration, _player, info.releaseAttackId, info.releaseByWeakHit, info.releaseByWeaponWeakHit, _drainAtkInfo);
  }

  public bool IsReadyForRelease()
  {
    return this.IsGrabing() && (double) Time.realtimeSinceStartup - (double) this.grabStartTime >= (double) this.duration;
  }

  public bool IsAliveGrabbedPlayerAll()
  {
    if (this.grabbedPlayers == null)
      return false;
    for (int index = 0; index < this.grabbedPlayers.Count; ++index)
    {
      if (Object.op_Inequality((Object) this.grabbedPlayers[index], (Object) null) && !this.grabbedPlayers[index].isDead)
        return true;
    }
    return false;
  }
}
