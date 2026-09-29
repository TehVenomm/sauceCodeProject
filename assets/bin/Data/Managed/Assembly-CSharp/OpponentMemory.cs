// Decompiled with JetBrains decompiler
// Type: OpponentMemory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class OpponentMemory
{
  private Brain brain;
  public OpponentMemory.Counter counter = new OpponentMemory.Counter();
  public UIntKeyTable<OpponentMemory.OpponentRecord> opponentRecords = new UIntKeyTable<OpponentMemory.OpponentRecord>();
  private OpponentMemory.OpponentRecord emptyOpponent = new OpponentMemory.OpponentRecord((StageObject) null);

  public OpponentMemory(Brain brain) => this.brain = brain;

  public OpponentMemory.OpponentRecord Find(StageObject obj)
  {
    return Object.op_Equality((Object) obj, (Object) null) ? (OpponentMemory.OpponentRecord) null : this.opponentRecords.Get((uint) obj.id);
  }

  public OpponentMemory.OpponentRecord FindOrEmpty(StageObject obj)
  {
    return this.Find(obj) ?? this.emptyOpponent;
  }

  public OpponentMemory.OpponentRecord FindOrRegist(StageObject obj)
  {
    if (Object.op_Equality((Object) obj, (Object) null))
      return this.emptyOpponent;
    OpponentMemory.OpponentRecord orRegist = this.Find(obj);
    if (orRegist == null)
    {
      orRegist = new OpponentMemory.OpponentRecord(obj);
      this.opponentRecords.Add((uint) obj.id, orRegist);
    }
    return orRegist;
  }

  public void Remove(StageObject obj) => this.opponentRecords.Remove((uint) obj.id);

  public List<OpponentMemory.OpponentRecord> GetListOfSensedOpponent()
  {
    List<OpponentMemory.OpponentRecord> list = new List<OpponentMemory.OpponentRecord>();
    this.opponentRecords.ForEach((Action<OpponentMemory.OpponentRecord>) (o =>
    {
      if (!o.IsAlive())
        return;
      list.Add(o);
    }));
    return list;
  }

  public void Update()
  {
    BrainParam.SensorParam sensorParam = this.brain.param.sensorParam;
    Character owner = this.brain.owner;
    Vector2 owner_pos2 = owner.positionXZ;
    Vector2 owner_forward2 = owner.forwardXZ;
    this.counter.Clear();
    bool isAutoMode = false;
    TargetPoint actionTargetPoint = (TargetPoint) null;
    if (this.brain.owner is Self)
    {
      Self owner1 = this.brain.owner as Self;
      isAutoMode = owner1.isAutoMode;
      if (isAutoMode)
        actionTargetPoint = (owner1.controller as AutoSelfController).actionTargetPoint;
    }
    this.brain.GetTargetObjectList().ForEach((Action<StageObject>) (opponent_obj =>
    {
      Character character = opponent_obj as Character;
      if (Object.op_Inequality((Object) character, (Object) null) && character.isDead)
        return;
      OpponentMemory.OpponentRecord orRegist = this.FindOrRegist(opponent_obj);
      bool flag = orRegist.record != null;
      Vector3 client_pos;
      if (flag)
      {
        client_pos = orRegist.record.pos;
      }
      else
      {
        client_pos = Vector3.zero;
        orRegist.record = new OpponentMemory.RecordData();
      }
      OpponentMemory.RecordData record = orRegist.record;
      record.pos = owner.GetTargetPosition(opponent_obj);
      Vector2 vector2Xz = record.pos.ToVector2XZ();
      Vector2 p2_1 = Vector2.op_Subtraction(vector2Xz, owner_pos2);
      Vector2 p2_2 = Vector2.op_Subtraction(vector2Xz, this.brain.frontPositionXZ);
      record.distance = ((Vector2) ref p2_1).magnitude;
      record.distanceFront = ((Vector2) ref p2_2).magnitude;
      record.rootAngle = Utility.Angle360(owner_forward2, p2_1);
      record.frontAngle = Utility.Angle360(owner_forward2, p2_2);
      record.isView = false;
      if ((double) record.distanceFront <= (double) sensorParam.viewDistance && ((double) record.frontAngle + (double) sensorParam.viewAngle / 2.0) % 360.0 < (double) sensorParam.viewAngle)
      {
        record.isView = true;
        ++this.counter.viewNum;
      }
      record.place = AIUtility.GetPlaceOfAngle360(record.rootAngle);
      switch (record.place)
      {
        case PLACE.FRONT:
          ++this.counter.frontNum;
          break;
        case PLACE.RIGHT:
          ++this.counter.rightNum;
          break;
        case PLACE.LEFT:
          ++this.counter.leftNum;
          break;
        case PLACE.BACK:
          ++this.counter.backNum;
          break;
      }
      record.isNearPlace = false;
      if ((double) record.distance <= (double) sensorParam.nearCheckDistance)
      {
        record.isNearPlace = true;
        ++this.counter.nearNum;
      }
      float num1 = record.distance - this.brain.rootInternalRedius;
      switch (record.place)
      {
        case PLACE.FRONT:
          num1 -= this.brain.rootFrontDistance;
          break;
        case PLACE.BACK:
          num1 -= this.brain.rootBackDistance;
          break;
      }
      record.distanceType = (double) num1 >= (double) sensorParam.shortDistance ? ((double) num1 < (double) sensorParam.shortDistance || (double) num1 >= (double) sensorParam.middleDistance ? ((double) num1 < (double) sensorParam.middleDistance || (double) num1 >= (double) sensorParam.longDistance ? DISTANCE.LONG : DISTANCE.MIDDLE) : DISTANCE.SHORT) : DISTANCE.SHORT_SHORT;
      Vector3 vector3 = record.pos;
      float num2 = record.distance;
      if (isAutoMode)
      {
        if (Object.op_Inequality((Object) actionTargetPoint, (Object) null))
        {
          vector3 = actionTargetPoint.GetTargetPoint();
          Vector2 vector2 = Vector2.op_Subtraction(vector3.ToVector2XZ(), owner_pos2);
          num2 = ((Vector2) ref vector2).magnitude;
        }
      }
      else if (owner is Player)
      {
        Player player = owner as Player;
        if (Object.op_Inequality((Object) player.targetingPoint, (Object) null))
        {
          vector3 = player.targetingPoint.GetTargetPoint();
          Vector2 vector2 = Vector2.op_Subtraction(vector3.ToVector2XZ(), owner_pos2);
          num2 = ((Vector2) ref vector2).magnitude;
        }
      }
      record.attackPos = vector3;
      record.attackPosDistance = num2;
      record.moveLength = 0.0f;
      if (flag)
        record.moveLength = AIUtility.GetLengthWithBetweenPosition(client_pos, record.pos);
      Vector2 p2_3 = Vector2.op_Subtraction(owner_pos2, vector2Xz);
      float angle = Utility.Angle360(opponent_obj.forwardXZ, p2_3);
      record.placeOfOpponent = AIUtility.GetPlaceOfAngle360(angle);
    }));
  }

  public DISTANCE GetDistance(float distanceSqr)
  {
    BrainParam.SensorParam sensorParam = this.brain.param.sensorParam;
    float num1 = sensorParam.shortDistance * sensorParam.shortDistance;
    float num2 = sensorParam.middleDistance * sensorParam.middleDistance;
    float num3 = sensorParam.longDistance * sensorParam.longDistance;
    if ((double) distanceSqr < (double) num1)
      return DISTANCE.SHORT_SHORT;
    if ((double) distanceSqr >= (double) num1 && (double) distanceSqr < (double) num2)
      return DISTANCE.SHORT;
    return (double) distanceSqr >= (double) num2 && (double) distanceSqr < (double) num3 ? DISTANCE.MIDDLE : DISTANCE.LONG;
  }

  public void OnTargetOpponent(StageObject now_target, StageObject prev_target)
  {
    Hate hate = this.GetHate(now_target);
    ++hate.cycleLockCount;
    ++hate.totalLockCount;
    if (Object.op_Equality((Object) now_target, (Object) prev_target))
      ++hate.continuousLockCount;
    else
      this.GetHate(prev_target).continuousLockCount = 0;
  }

  public bool IsPlaceOpponent(StageObject obj, PLACE place)
  {
    OpponentMemory.OpponentRecord opponentRecord = this.Find(obj);
    return opponentRecord != null && opponentRecord.record.place == place;
  }

  public bool IsAttackableOpponent(StageObject obj)
  {
    OpponentMemory.OpponentRecord opponentRecord = this.Find(obj);
    return opponentRecord != null && (double) opponentRecord.record.attackPosDistance < (double) this.brain.weaponCtrl.GetAttackReach();
  }

  public bool IsSpecialAttackableOpponent(StageObject obj)
  {
    OpponentMemory.OpponentRecord opponentRecord = this.Find(obj);
    return opponentRecord != null && (double) opponentRecord.record.attackPosDistance < (double) this.brain.weaponCtrl.GetSpecialReach();
  }

  public bool IsAvoidAttackableOpponent(StageObject obj)
  {
    OpponentMemory.OpponentRecord opponentRecord = this.Find(obj);
    return opponentRecord != null && (double) opponentRecord.record.attackPosDistance < (double) this.brain.weaponCtrl.GetAvoidAttackReach();
  }

  public bool IsArrivalPosition(StageObject obj)
  {
    OpponentMemory.OpponentRecord opponentRecord = this.Find(obj);
    return opponentRecord != null && this.brain.owner.IsArrivalPosition(opponentRecord.record.pos);
  }

  public bool IsArrivalAttackPosition(StageObject obj)
  {
    OpponentMemory.OpponentRecord opponentRecord = this.Find(obj);
    return opponentRecord != null && this.brain.owner.IsArrivalPosition(opponentRecord.record.attackPos);
  }

  public float GetLengthWithAttackPos(StageObject obj, Vector3 check_pos)
  {
    OpponentMemory.OpponentRecord opponentRecord = this.Find(obj);
    return opponentRecord == null ? 0.0f : AIUtility.GetLengthWithBetweenPosition(opponentRecord.record.attackPos, check_pos);
  }

  public bool IsOverMoveLength(StageObject obj, float len)
  {
    OpponentMemory.OpponentRecord opponentRecord = this.Find(obj);
    return opponentRecord != null && (double) len <= (double) opponentRecord.record.moveLength;
  }

  public HateParam hateParam { get; private set; }

  public int turnCountForHateCycle { get; private set; }

  public bool haveHateControl => this.hateParam != null;

  public void SetHateParam(uint id)
  {
    EnemyPersonalityTable.Data data = Singleton<EnemyPersonalityTable>.I.GetData(id);
    if (data == null)
      return;
    this.SetHateParam(data.param);
  }

  public void SetHateParam(HateParam data) => this.hateParam = data;

  public bool IsHateCycleLastTurn()
  {
    return this.haveHateControl && this.turnCountForHateCycle == this.hateParam.cycleTurnMax;
  }

  public Hate GetHate(StageObject obj) => this.FindOrRegist(obj).hate;

  public void AddHate(StageObject obj, int hate_val, Hate.TYPE type)
  {
    Hate hate = this.GetHate(obj);
    hate.val[(int) type] = Mathf.Min(hate.val[(int) type] + hate_val, 1000);
    if (hate.val[(int) type] < 0)
      hate.val[(int) type] = 0;
    hate.turnVal += hate_val;
    if (hate.turnVal >= 0)
      return;
    hate.turnVal = 0;
  }

  public void UpdateHate()
  {
    if (!this.haveHateControl)
      return;
    bool flag = false;
    ++this.turnCountForHateCycle;
    if (this.turnCountForHateCycle > this.hateParam.cycleTurnMax)
    {
      this.turnCountForHateCycle = 0;
      flag = true;
    }
    List<OpponentMemory.OpponentRecord> ofSensedOpponent = this.GetListOfSensedOpponent();
    for (int index1 = 0; index1 < ofSensedOpponent.Count; ++index1)
    {
      OpponentMemory.OpponentRecord opponentRecord = ofSensedOpponent[index1];
      if (opponentRecord.IsAlive())
      {
        opponentRecord.hate.val[0] = this.hateParam.distanceHateParams[(int) opponentRecord.record.distanceType];
        Player player = opponentRecord.obj as Player;
        if (Object.op_Inequality((Object) player, (Object) null))
        {
          int num = (int) Mathf.Lerp(1000f, 0.0f, (float) player.hp / (float) player.hpMax);
          opponentRecord.hate.val[1] = num;
        }
        if (Object.op_Equality((Object) opponentRecord.obj, (Object) this.brain.targetCtrl.GetCurrentTarget()))
        {
          for (int index2 = 2; index2 < 7; ++index2)
          {
            if (opponentRecord.record.isDamaged)
            {
              opponentRecord.hate.val[index2] = (int) ((double) opponentRecord.hate.val[index2] * (double) this.hateParam.categoryParam[index2].atackedVolatizeRate);
              opponentRecord.record.isDamaged = false;
            }
            else
              opponentRecord.hate.val[index2] = (int) ((double) opponentRecord.hate.val[index2] * (double) this.hateParam.categoryParam[index2].volatilizeRate);
          }
        }
        opponentRecord.hate.turnVal = 0;
        if (flag)
          opponentRecord.hate.cycleLockCount = 0;
      }
    }
  }

  public OpponentMemory.OpponentRecord GetOpponentWithNotTargetInHateCycle()
  {
    OpponentMemory.OpponentRecord opponent = (OpponentMemory.OpponentRecord) null;
    this.opponentRecords.ForEach((Action<OpponentMemory.OpponentRecord>) (o =>
    {
      if (!o.IsAlive() || o.hate.cycleLockCount > 0)
        return;
      opponent = o;
    }));
    return opponent;
  }

  public OpponentMemory.OpponentRecord GetOpponentWithHigherHate()
  {
    OpponentMemory.OpponentRecord higher = (OpponentMemory.OpponentRecord) null;
    this.opponentRecords.ForEach((Action<OpponentMemory.OpponentRecord>) (o =>
    {
      if (!o.IsAlive() || higher != null && o.hate.CalcTotalHate(this.hateParam) <= higher.hate.CalcTotalHate(this.hateParam))
        return;
      higher = o;
    }));
    return higher;
  }

  public OpponentMemory.OpponentRecord[] GetOpponentWithRankingHate(
    bool isIncludeDead,
    int minimumNum = 4)
  {
    List<OpponentMemory.OpponentRecord> ranking = new List<OpponentMemory.OpponentRecord>();
    this.opponentRecords.ForEach((Action<OpponentMemory.OpponentRecord>) (o =>
    {
      if (Object.op_Equality((Object) o.obj, (Object) null))
        return;
      Character character = o.obj as Character;
      if (Object.op_Equality((Object) character, (Object) null) || !isIncludeDead && character.isDead)
        return;
      ranking.Add(o);
    }));
    if (ranking.Count == 0)
      return (OpponentMemory.OpponentRecord[]) null;
    ranking.Sort((Comparison<OpponentMemory.OpponentRecord>) ((a, b) => a.hate.CalcTotalHate(this.hateParam) - b.hate.CalcTotalHate(this.hateParam)));
    int count1 = ranking.Count;
    int count2 = ranking.Count;
    for (; count1 < minimumNum; ++count1)
    {
      int index = (count1 - count2) % count2;
      ranking.Add(ranking[index]);
    }
    return ranking.ToArray();
  }

  public bool IsOpponentInterestLoseOfHate(StageObject obj)
  {
    if (!this.haveHateControl)
      return true;
    OpponentMemory.OpponentRecord opponentRecord = this.Find(obj);
    if (opponentRecord == null || !opponentRecord.IsAlive() || !opponentRecord.record.isView && opponentRecord.hate.CalcTotalHate(this.hateParam) <= 0 || this.IsHateCycleLastTurn() || this.hateParam.missTargetFromContinuousLockNum > 0 && opponentRecord.hate.continuousLockCount >= this.hateParam.missTargetFromContinuousLockNum)
      return true;
    int num = (int) ((double) this.brain.owner.hpMax * (double) this.hateParam.missTargetUnderStockPerMaxHp);
    return opponentRecord.hate.turnVal <= num || obj.objectType != StageObject.OBJECT_TYPE.DECOY && this.IsExistDecoy();
  }

  private bool IsExistDecoy()
  {
    bool bExist = false;
    this.opponentRecords.ForEach((Action<OpponentMemory.OpponentRecord>) (o =>
    {
      if (!Object.op_Inequality((Object) o.obj, (Object) null) || o.obj.objectType != StageObject.OBJECT_TYPE.DECOY)
        return;
      bExist = true;
    }));
    return bExist;
  }

  public class Counter
  {
    public int viewNum;
    public int frontNum;
    public int backNum;
    public int rightNum;
    public int leftNum;
    public int nearNum;

    public void Clear()
    {
      this.viewNum = 0;
      this.frontNum = 0;
      this.backNum = 0;
      this.rightNum = 0;
      this.leftNum = 0;
      this.nearNum = 0;
    }
  }

  public class RecordData
  {
    public float distance;
    public float distanceFront;
    public DISTANCE distanceType;
    public bool isView;
    public float rootAngle;
    public float frontAngle;
    public PLACE place;
    public bool isNearPlace;
    public float attackPosDistance;
    public float moveLength;
    public PLACE placeOfOpponent;
    public bool isDamaged;

    public Vector3 pos { get; set; }

    public Vector3 attackPos { get; set; }

    public override string ToString()
    {
      return $"{$"{$"{$"{$"{$"{$"{$"{$"{$"pos={(object) this.pos}"}, len={(object) this.distance}"}, front={(object) this.distanceFront}"}, D={(object) this.distanceType}"}, P={(object) this.place}"}, angle={(object) this.rootAngle}"}, frontAngle={(object) this.frontAngle}"}, View={this.isView.ToString()}"}, Near={this.isNearPlace.ToString()}"}, moveLength={(object) this.moveLength}";
    }
  }

  public class OpponentRecord
  {
    public StageObject obj;
    public OpponentMemory.RecordData record = new OpponentMemory.RecordData();
    public Hate hate = new Hate();

    public OpponentRecord(StageObject obj) => this.obj = obj;

    public bool IsAlive()
    {
      return !Object.op_Inequality((Object) this.obj, (Object) null) || !(this.obj is Character) || !(this.obj as Character).isDead;
    }
  }
}
