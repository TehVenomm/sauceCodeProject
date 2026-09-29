// Decompiled with JetBrains decompiler
// Type: HomeNPCArenaSoldier
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class HomeNPCArenaSoldier : HomeNPCCharacter
{
  protected const float TURN_DEGREE_MAX = 90f;
  private const int TURN_FRAME = 25;
  private const float TURN_DEGREE_PER_FRAME = 3.6f;
  private bool isTurned;
  private float rotatedDegree;

  protected override void PlayNearAnim(HomeNPCCharacter npc)
  {
    if (!MonoBehaviourSingleton<UserInfoManager>.I.isArenaOpen)
      this.animCtrl.Play(npc.nearAnim);
    else if (MonoBehaviourSingleton<UserInfoManager>.I.isJoinedArenaRanking && this.animCtrl.playingAnim == PLCA.IDLE_01 && this.isTurned)
      this.animCtrl.Play(PLCA.THROUGH_BOW);
    else if ((int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 50)
    {
      if (this.isTurned)
        return;
      this.StartCoroutine(this.PlayThroughTurn());
      this.isTurned = true;
    }
    else
      this.animCtrl.Play(npc.nearAnim);
  }

  private IEnumerator PlayThroughTurn()
  {
    this.animCtrl.Play(PLCA.THROUGH_TURN);
    int turnSign = (double) this.npcInfo.scaleX > 0.0 ? 1 : -1;
    float beforeTurnRot = this._transform.eulerAngles.y;
    while ((double) this.rotatedDegree <= 90.0)
    {
      this.rotatedDegree += 3.6f;
      if ((double) this.rotatedDegree > 90.0)
        this.rotatedDegree = 90f;
      this._transform.rotation = Quaternion.Euler(0.0f, beforeTurnRot + this.rotatedDegree * (float) turnSign, 0.0f);
      yield return (object) null;
    }
  }
}
