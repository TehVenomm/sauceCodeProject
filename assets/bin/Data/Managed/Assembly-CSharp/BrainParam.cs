// Decompiled with JetBrains decompiler
// Type: BrainParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class BrainParam
{
  public BrainParam.SensorParam sensorParam = new BrainParam.SensorParam();
  public BrainParam.MoveParam moveParam = new BrainParam.MoveParam();
  public BrainParam.ThinkParam thinkParam = new BrainParam.ThinkParam();
  public BrainParam.ScountingParam scoutParam;

  [Serializable]
  public class SensorParam
  {
    [Tooltip("視野角")]
    public float viewAngle = 110f;
    [Tooltip("視野距離")]
    public float viewDistance = 40f;
    [Tooltip("聞こえる範囲")]
    public float hearingRange = 20f;
    [Tooltip("内円半径")]
    public float internalRadius = 1f;
    [Tooltip("短距離(未満で超短い距離)")]
    public float shortDistance = 3f;
    [Tooltip("中距離(未満で短い距離)")]
    public float middleDistance = 6f;
    [Tooltip("長距離(未満で中くらいの距離、以上で長い距離)")]
    public float longDistance = 10f;
    [Tooltip("近距離チェック距離")]
    public float nearCheckDistance = 5f;
  }

  [Serializable]
  public class MoveParam
  {
    [Tooltip("回転をモーションで行うか")]
    public bool motionRotate = true;
    [Tooltip("移動最大距離")]
    public float moveMaxLength = 10f;
    [Tooltip("対象が逃げたときの追う距離")]
    public float moveOverDistance = 2f;
    [Tooltip("行動前の移動をホーミングタイプにする")]
    public bool enableActionMoveHoming;
    [Tooltip("ホーミング移動最大距離")]
    public float moveHomingMaxLength = 20f;
  }

  [Serializable]
  public class ThinkParam
  {
    [Tooltip("対象の情報更新間隔")]
    public float opponentMemorySpan = 1f;
    [Tooltip("対象の変更更新間隔")]
    public float targetUpdateSpan = 10f;
  }

  public class ScountingParam
  {
    public float scountigRangeSqr;
    public float scoutingSightCos;
    public float scoutingAudibilitySqr;
    public const int ACTIVATE_HATE_VALUE = 100;

    public bool IsScouted(Transform self, Transform target)
    {
      Vector3 vector3 = Vector3.op_Subtraction(target.position, self.position);
      return (double) ((Vector3) ref vector3).sqrMagnitude < (double) this.scountigRangeSqr && (double) Vector3.Dot(self.forward, ((Vector3) ref vector3).normalized) >= (double) this.scoutingSightCos;
    }
  }
}
