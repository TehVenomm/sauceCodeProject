// Decompiled with JetBrains decompiler
// Type: AIUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AIUtility
{
  public const float MAX_FAR_DISTANCE = 100f;

  public static PLACE GetPlaceOfAngle360(float angle)
  {
    if ((double) angle >= 45.0 && (double) angle < 135.0)
      return PLACE.RIGHT;
    if ((double) angle >= 135.0 && (double) angle < 225.0)
      return PLACE.BACK;
    return (double) angle >= 225.0 && (double) angle < 315.0 ? PLACE.LEFT : PLACE.FRONT;
  }

  public static PLACE GetSideOfAngle360(float angle)
  {
    return (double) angle >= 0.0 && (double) angle < 180.0 ? PLACE.RIGHT : PLACE.LEFT;
  }

  public static float GetAngle360OfTargetPos(Character client, Vector3 target)
  {
    Vector2 vector2Xz = client._position.ToVector2XZ();
    Vector2 p2 = Vector2.op_Subtraction(target.ToVector2XZ(), vector2Xz);
    return Utility.Angle360(client.forwardXZ, p2);
  }

  public static bool IsAlive(StageObject obj)
  {
    if (Object.op_Equality((Object) obj, (Object) null))
      return false;
    return !(obj is Character) || !(obj as Character).isDead;
  }

  public static List<Character> GetListOfDeadAllys(Character client)
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return (List<Character>) null;
    List<StageObject> stageObjectList = !(client is Player) ? (!(client is Enemy) ? MonoBehaviourSingleton<StageObjectManager>.I.characterList : MonoBehaviourSingleton<StageObjectManager>.I.enemyList) : MonoBehaviourSingleton<StageObjectManager>.I.playerList;
    List<Character> dead_charas = new List<Character>();
    stageObjectList.ForEach((Action<StageObject>) (o =>
    {
      Character character = o as Character;
      if (Object.op_Equality((Object) character, (Object) client) || !character.isDead)
        return;
      dead_charas.Add(character);
    }));
    return dead_charas;
  }

  public static NonPlayer GetNearestAliveNpc(StageObject client)
  {
    NonPlayer nearestAliveNpc = (NonPlayer) null;
    float num = float.MaxValue;
    int index = 0;
    for (int count = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count; index < count; ++index)
    {
      if (MonoBehaviourSingleton<StageObjectManager>.I.playerList[index] is NonPlayer player && !Object.op_Equality((Object) player, (Object) client))
      {
        switch (player.CanGoPray(client))
        {
          case NonPlayer.eNpcAllayState.SAME:
            return player;
          case NonPlayer.eNpcAllayState.CAN:
            float withBetweenObject = AIUtility.GetLengthWithBetweenObject(client, (StageObject) player);
            if ((double) withBetweenObject < (double) num)
            {
              nearestAliveNpc = player;
              num = withBetweenObject;
              continue;
            }
            continue;
          default:
            continue;
        }
      }
    }
    return nearestAliveNpc;
  }

  public static Enemy GetNearestAliveEnemy(StageObject baseObj)
  {
    Enemy nearestAliveEnemy = (Enemy) null;
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return (Enemy) null;
    List<StageObject> enemyList = MonoBehaviourSingleton<StageObjectManager>.I.enemyList;
    if (enemyList == null || enemyList.Count <= 0)
      return (Enemy) null;
    float num = float.MaxValue;
    foreach (StageObject stageObject in enemyList)
    {
      Enemy target = stageObject as Enemy;
      if (!Object.op_Equality((Object) target, (Object) null) && !target.isDead)
      {
        float withBetweenObject = AIUtility.GetLengthWithBetweenObject(baseObj, (StageObject) target);
        if ((double) withBetweenObject < (double) num)
        {
          nearestAliveEnemy = target;
          num = withBetweenObject;
        }
      }
    }
    return nearestAliveEnemy;
  }

  public static Enemy GetNearestAliveEnemy(Vector3 basePos)
  {
    Enemy nearestAliveEnemy = (Enemy) null;
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return (Enemy) null;
    List<StageObject> enemyList = MonoBehaviourSingleton<StageObjectManager>.I.enemyList;
    if (enemyList == null || enemyList.Count <= 0)
      return (Enemy) null;
    float num = float.MaxValue;
    foreach (StageObject stageObject in enemyList)
    {
      Enemy enemy = stageObject as Enemy;
      if (!Object.op_Equality((Object) enemy, (Object) null) && !enemy.isDead)
      {
        float withBetweenPosition = AIUtility.GetLengthWithBetweenPosition(basePos, enemy._position);
        if ((double) withBetweenPosition < (double) num)
        {
          nearestAliveEnemy = enemy;
          num = withBetweenPosition;
        }
      }
    }
    return nearestAliveEnemy;
  }

  public static DecoyBulletObject GetNearestDecoyObject(Vector3 basePos)
  {
    DecoyBulletObject nearestDecoyObject = (DecoyBulletObject) null;
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return (DecoyBulletObject) null;
    List<StageObject> decoyList = MonoBehaviourSingleton<StageObjectManager>.I.decoyList;
    if (decoyList.IsNullOrEmpty<StageObject>())
      return (DecoyBulletObject) null;
    float num = float.MaxValue;
    int index = 0;
    for (int count = decoyList.Count; index < count; ++index)
    {
      DecoyBulletObject decoyBulletObject = decoyList[index] as DecoyBulletObject;
      if (!Object.op_Equality((Object) decoyBulletObject, (Object) null) && decoyBulletObject.IsActive())
      {
        float withBetweenPosition = AIUtility.GetLengthWithBetweenPosition(basePos, decoyBulletObject._position);
        if ((double) withBetweenPosition < (double) num)
        {
          nearestDecoyObject = decoyBulletObject;
          num = withBetweenPosition;
        }
      }
    }
    return nearestDecoyObject;
  }

  public static FieldWaveTargetObject GetNearestWaveMatchTargetObject(Vector3 basePos)
  {
    FieldWaveTargetObject matchTargetObject = (FieldWaveTargetObject) null;
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return (FieldWaveTargetObject) null;
    List<StageObject> waveTargetList = MonoBehaviourSingleton<StageObjectManager>.I.waveTargetList;
    if (waveTargetList.IsNullOrEmpty<StageObject>())
      return (FieldWaveTargetObject) null;
    float num = float.MaxValue;
    int index = 0;
    for (int count = waveTargetList.Count; index < count; ++index)
    {
      FieldWaveTargetObject waveTargetObject = waveTargetList[index] as FieldWaveTargetObject;
      if (!Object.op_Equality((Object) waveTargetObject, (Object) null) && !waveTargetObject.isDead)
      {
        float withBetweenPosition = AIUtility.GetSqrLengthWithBetweenPosition(basePos, waveTargetObject._position);
        if ((double) withBetweenPosition < (double) num)
        {
          matchTargetObject = waveTargetObject;
          num = withBetweenPosition;
        }
      }
    }
    return matchTargetObject;
  }

  public static float GetLengthWithBetweenObject(StageObject client, StageObject target)
  {
    Vector3 targetPosition = client.GetTargetPosition(target);
    return AIUtility.GetLengthWithBetweenPosition(client._position, targetPosition);
  }

  public static float GetLengthWithBetweenPosition(Vector3 client_pos, Vector3 target_pos)
  {
    if (Vector3.op_Equality(client_pos, target_pos))
      return 0.0f;
    Vector3 vector3 = Vector3.op_Subtraction(target_pos, client_pos);
    vector3.y = 0.0f;
    return ((Vector3) ref vector3).magnitude;
  }

  public static float GetSqrLengthWithBetweenPosition(Vector3 client_pos, Vector3 target_pos)
  {
    if (Vector3.op_Equality(client_pos, target_pos))
      return 0.0f;
    Vector3 vector3 = Vector3.op_Subtraction(target_pos, client_pos);
    vector3.y = 0.0f;
    return ((Vector3) ref vector3).sqrMagnitude;
  }

  public static int GetObstacleMask() => 393728 /*0x060200*/;

  public static int GetWallAndBlockMask() => 131584 /*0x020200*/;

  public static int GetOpponentMask(StageObject client)
  {
    switch (client)
    {
      case Player _:
        return 2048 /*0x0800*/;
      case Enemy _:
        return 256 /*0x0100*/;
      default:
        return 0;
    }
  }

  public static bool RaycastObstacle(StageObject client, StageObject target, out RaycastHit hit)
  {
    Vector3 position1 = client._position;
    Vector3 position2 = target._position;
    int obstacleMask = AIUtility.GetObstacleMask();
    Vector3 target1 = position2;
    int mask = obstacleMask;
    ref RaycastHit local = ref hit;
    return AIUtility.RaycastForTargetPos(position1, target1, mask, out local);
  }

  public static bool RaycastObstacle(StageObject client, Vector3 target_pos, out RaycastHit hit)
  {
    Vector3 position = client._position;
    int obstacleMask = AIUtility.GetObstacleMask();
    Vector3 target = target_pos;
    int mask = obstacleMask;
    ref RaycastHit local = ref hit;
    return AIUtility.RaycastForTargetPos(position, target, mask, out local);
  }

  public static bool RaycastWallAndBlock(StageObject client, Vector3 targetPos, out RaycastHit hit)
  {
    Vector3 position = client._position;
    int wallAndBlockMask = AIUtility.GetWallAndBlockMask();
    Vector3 target = targetPos;
    int mask = wallAndBlockMask;
    ref RaycastHit local = ref hit;
    return AIUtility.RaycastForTargetPos(position, target, mask, out local);
  }

  public static bool RaycastOpponent(StageObject client, Vector3 target_pos, out RaycastHit hit)
  {
    Vector3 position = client._position;
    int opponentMask = AIUtility.GetOpponentMask(client);
    Vector3 target = target_pos;
    int mask = opponentMask;
    ref RaycastHit local = ref hit;
    return AIUtility.RaycastForTargetPos(position, target, mask, out local);
  }

  public static bool RaycastObstacleOrOpponent(
    StageObject client,
    Vector3 target_pos,
    out RaycastHit hit)
  {
    Vector3 position = client._position;
    int num = AIUtility.GetObstacleMask() | AIUtility.GetOpponentMask(client);
    Vector3 target = target_pos;
    int mask = num;
    ref RaycastHit local = ref hit;
    return AIUtility.RaycastForTargetPos(position, target, mask, out local);
  }

  public static bool RaycastForTargetPos(
    Vector3 pos,
    Vector3 target,
    int mask,
    out RaycastHit hit)
  {
    hit = new RaycastHit();
    if (mask == 0)
      return false;
    Vector3 vector3 = Vector3.op_Subtraction(target, pos);
    float magnitude = ((Vector3) ref vector3).magnitude;
    return Physics.Raycast(pos, vector3, ref hit, magnitude, mask);
  }

  public static bool IsHitObstacleOrOpponentWithPlace(StageObject client, PLACE place, float range)
  {
    Vector3 vector3 = ((Component) client).transform.TransformDirection(place.GetVector3());
    int num = AIUtility.GetObstacleMask() | AIUtility.GetOpponentMask(client);
    return Physics.Raycast(client._position, vector3, range, num);
  }

  public static bool IsHitObjectFromMoveObject(
    Transform moveObj,
    Transform checkObj,
    float radius,
    int mask)
  {
    Vector3 vector3_1 = moveObj.TransformDirection(Vector3.forward);
    Vector3 vector3_2 = Vector3.op_Subtraction(moveObj.position, checkObj.position);
    float magnitude = ((Vector3) ref vector3_2).magnitude;
    foreach (RaycastHit raycastHit in Physics.SphereCastAll(moveObj.position, radius, vector3_1, magnitude, mask))
    {
      if (Object.op_Equality((Object) ((RaycastHit) ref raycastHit).transform, (Object) checkObj))
        return true;
    }
    return false;
  }

  public static void DrawRay(
    StageObject client,
    Vector3 target_pos,
    float len,
    Color color,
    float sec)
  {
    Vector3 position = client._position;
    Vector3 vector3 = Vector3.op_Subtraction(target_pos, position);
    if ((double) len < 0.0)
      len = ((Vector3) ref vector3).magnitude;
    ((Vector3) ref vector3).Normalize();
    Debug.DrawRay(position, Vector3.op_Multiply(vector3, len), color, sec);
  }
}
