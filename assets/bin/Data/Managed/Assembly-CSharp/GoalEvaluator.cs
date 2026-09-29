// Decompiled with JetBrains decompiler
// Type: GoalEvaluator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public abstract class GoalEvaluator
{
  public float bias { get; private set; }

  public GoalEvaluator(float bias) => this.bias = bias;

  public abstract float CalcEvaluateValue(Brain brain);

  public abstract void SetGoal(Brain brain, Goal_Think think);

  private float EvaluateValue(float val, float min, float max)
  {
    val = Mathf.Clamp(val, min, max);
    return val / max;
  }

  protected float EvaluateDistanceWithObject(Brain brain, StageObject obj)
  {
    return this.EvaluateDistance(AIUtility.GetLengthWithBetweenObject((StageObject) brain.owner, obj));
  }

  protected float EvaluateDistance(float length)
  {
    float min = 1f;
    float max = 100f;
    return this.EvaluateValue(length, min, max);
  }

  protected float EvaluateHealth(Brain brain)
  {
    return brain.owner.hpMax == 0 ? 1f : this.EvaluateValue((float) brain.owner.hp, 1f, (float) brain.owner.hpMax);
  }

  protected float EvaluateDangerWithTargetCondition(Brain brain, StageObject target)
  {
    float val = 1f;
    if (target is Character)
      val = (target as Character).actionID != Character.ACTION_ID.ATTACK ? 50f : 80f;
    return this.EvaluateValue(val, 1f, 100f);
  }

  protected float EvaluateDangerWithTargetPlace(Brain brain, StageObject target)
  {
    float val = 1f;
    OpponentMemory.OpponentRecord opponentRecord = brain.opponentMem.Find(target);
    if (opponentRecord != null)
    {
      switch (opponentRecord.record.placeOfOpponent)
      {
        case PLACE.FRONT:
          val = 90f;
          break;
        case PLACE.RIGHT:
        case PLACE.LEFT:
          val = 50f;
          break;
        case PLACE.BACK:
          val = 10f;
          break;
      }
    }
    return this.EvaluateValue(val, 1f, 100f);
  }
}
