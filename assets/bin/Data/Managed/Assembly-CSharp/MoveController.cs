// Decompiled with JetBrains decompiler
// Type: MoveController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class MoveController
{
  private Brain brain;
  public MoveController.MOVE_TYPE moveType;
  private float saveStopRange;
  private float stopTime;
  private RaycastHit _seekHit;
  private const float AVOID_RANGE = 2f;

  public MoveController(Brain brain) => this.brain = brain;

  private void TypeOn(MoveController.MOVE_TYPE type) => this.moveType |= type;

  private void TypeOff(MoveController.MOVE_TYPE type) => this.moveType &= ~type;

  private bool TypeIsOn(MoveController.MOVE_TYPE type) => (this.moveType & type) == type;

  public void StopOn() => this.TypeOn(MoveController.MOVE_TYPE.STOP);

  public void AvoidOn() => this.TypeOn(MoveController.MOVE_TYPE.AVOID);

  public void SeekOn() => this.TypeOn(MoveController.MOVE_TYPE.SEEK);

  public void RotateOn() => this.TypeOn(MoveController.MOVE_TYPE.ROTATE);

  public void StopOff() => this.TypeOff(MoveController.MOVE_TYPE.STOP);

  public void AvoidOff() => this.TypeOff(MoveController.MOVE_TYPE.AVOID);

  public void SeekOff() => this.TypeOff(MoveController.MOVE_TYPE.SEEK);

  public void RotateOff() => this.TypeOff(MoveController.MOVE_TYPE.ROTATE);

  public bool IsStop() => this.TypeIsOn(MoveController.MOVE_TYPE.STOP);

  public bool IsAvoid() => this.TypeIsOn(MoveController.MOVE_TYPE.AVOID);

  public bool IsSeek() => this.TypeIsOn(MoveController.MOVE_TYPE.SEEK);

  public bool IsRotate() => this.TypeIsOn(MoveController.MOVE_TYPE.ROTATE);

  public void ChangeStopRange(float range)
  {
    this.saveStopRange = this.brain.owner.moveStopRange;
    this.brain.owner.moveStopRange = range;
  }

  public void ResetStopRange()
  {
    if ((double) this.saveStopRange > 0.0)
      this.brain.owner.moveStopRange = this.saveStopRange;
    this.saveStopRange = 0.0f;
  }

  public void SetStopTime(float time) => this.stopTime = time + Time.time;

  public bool isStopTimeOver => (double) this.stopTime < (double) Time.time;

  public RaycastHit seekHit => this._seekHit;

  public Vector2 stickVec { get; private set; }

  public Vector3 targetPos { get; private set; }

  public void SetSeek(Vector2 stick, Vector3 pos)
  {
    this.stickVec = stick;
    this.targetPos = pos;
  }

  public void SetTargetPos(Vector3 pos) => this.targetPos = pos;

  public bool CanSeekToOpponent(Vector3 target_pos, float move_len)
  {
    int obstacleMask = AIUtility.GetObstacleMask();
    return this.CanSeekToPosition(target_pos, move_len, obstacleMask);
  }

  public bool CanSeekToAlly(Vector3 target_pos, float move_len)
  {
    int mask = AIUtility.GetObstacleMask() | AIUtility.GetOpponentMask((StageObject) this.brain.owner);
    return this.CanSeekToPosition(target_pos, move_len, mask);
  }

  public bool CanSeekToPosition(Vector3 target_pos, float move_len, int mask)
  {
    return !AIUtility.RaycastForTargetPos(this.brain.owner._transform.position, target_pos, mask, out this._seekHit) || (double) ((RaycastHit) ref this._seekHit).distance > (double) move_len;
  }

  public PLACE avoidPlace { get; private set; }

  public void SetAvoid(PLACE place) => this.avoidPlace = place;

  public bool CanRightAvoid() => this.CanPlaceAvoid(PLACE.RIGHT);

  public bool CanLeftAvoid() => this.CanPlaceAvoid(PLACE.LEFT);

  public bool CanBackAvoid() => this.CanPlaceAvoid(PLACE.BACK);

  public bool CanFrontAvoid() => this.CanPlaceAvoid(PLACE.FRONT);

  public bool CanPlaceAvoid(PLACE place)
  {
    return !AIUtility.IsHitObstacleOrOpponentWithPlace((StageObject) this.brain.owner, place, 2f);
  }

  public Vector3 rootPosition { get; private set; }

  public void SetRootPosition(Vector3 pos) => this.rootPosition = pos;

  [Flags]
  public enum MOVE_TYPE
  {
    NONE = 0,
    STOP = 1,
    AVOID = 2,
    SEEK = 4,
    ROTATE = 8,
  }
}
