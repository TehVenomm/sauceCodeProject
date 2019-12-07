using System.Collections.Generic;
using UnityEngine;

public class AIUtility
{
	public const float MAX_FAR_DISTANCE = 100f;

	public static PLACE GetPlaceOfAngle360(float angle)
	{
		if (angle >= 45f && angle < 135f)
		{
			return PLACE.RIGHT;
		}
		if (angle >= 135f && angle < 225f)
		{
			return PLACE.BACK;
		}
		if (angle >= 225f && angle < 315f)
		{
			return PLACE.LEFT;
		}
		return PLACE.FRONT;
	}

	public static PLACE GetSideOfAngle360(float angle)
	{
		if (angle >= 0f && angle < 180f)
		{
			return PLACE.RIGHT;
		}
		return PLACE.LEFT;
	}

	public static float GetAngle360OfTargetPos(Character client, Vector3 target)
	{
		Vector2 b = client._position.ToVector2XZ();
		Vector2 p = target.ToVector2XZ() - b;
		return Utility.Angle360(client.forwardXZ, p);
	}

	public static bool IsAlive(StageObject obj)
	{
		if (obj == null)
		{
			return false;
		}
		if (obj is Character)
		{
			return !(obj as Character).isDead;
		}
		return true;
	}

	public static List<Character> GetListOfDeadAllys(Character client)
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return null;
		}
		List<StageObject> list = null;
		list = ((client is Player) ? MonoBehaviourSingleton<StageObjectManager>.I.playerList : ((!(client is Enemy)) ? MonoBehaviourSingleton<StageObjectManager>.I.characterList : MonoBehaviourSingleton<StageObjectManager>.I.enemyList));
		List<Character> dead_charas = new List<Character>();
		list.ForEach(delegate(StageObject o)
		{
			Character character = o as Character;
			if (!(character == client) && character.isDead)
			{
				dead_charas.Add(character);
			}
		});
		return dead_charas;
	}

	public static NonPlayer GetNearestAliveNpc(StageObject client)
	{
		NonPlayer result = null;
		float num = float.MaxValue;
		int i = 0;
		for (int count = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count; i < count; i++)
		{
			NonPlayer nonPlayer = MonoBehaviourSingleton<StageObjectManager>.I.playerList[i] as NonPlayer;
			if ((object)nonPlayer == null || nonPlayer == client)
			{
				continue;
			}
			switch (nonPlayer.CanGoPray(client))
			{
			case NonPlayer.eNpcAllayState.SAME:
				return nonPlayer;
			case NonPlayer.eNpcAllayState.CAN:
			{
				float lengthWithBetweenObject = GetLengthWithBetweenObject(client, nonPlayer);
				if (lengthWithBetweenObject < num)
				{
					result = nonPlayer;
					num = lengthWithBetweenObject;
				}
				break;
			}
			}
		}
		return result;
	}

	public static Enemy GetNearestAliveEnemy(StageObject baseObj)
	{
		Enemy result = null;
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return null;
		}
		List<StageObject> enemyList = MonoBehaviourSingleton<StageObjectManager>.I.enemyList;
		if (enemyList == null || enemyList.Count <= 0)
		{
			return null;
		}
		float num = float.MaxValue;
		foreach (StageObject item in enemyList)
		{
			Enemy enemy = item as Enemy;
			if (!(enemy == null) && !enemy.isDead)
			{
				float lengthWithBetweenObject = GetLengthWithBetweenObject(baseObj, enemy);
				if (lengthWithBetweenObject < num)
				{
					result = enemy;
					num = lengthWithBetweenObject;
				}
			}
		}
		return result;
	}

	public static Enemy GetNearestAliveEnemy(Vector3 basePos)
	{
		Enemy result = null;
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return null;
		}
		List<StageObject> enemyList = MonoBehaviourSingleton<StageObjectManager>.I.enemyList;
		if (enemyList == null || enemyList.Count <= 0)
		{
			return null;
		}
		float num = float.MaxValue;
		foreach (StageObject item in enemyList)
		{
			Enemy enemy = item as Enemy;
			if (!(enemy == null) && !enemy.isDead)
			{
				float lengthWithBetweenPosition = GetLengthWithBetweenPosition(basePos, enemy._position);
				if (lengthWithBetweenPosition < num)
				{
					result = enemy;
					num = lengthWithBetweenPosition;
				}
			}
		}
		return result;
	}

	public static DecoyBulletObject GetNearestDecoyObject(Vector3 basePos)
	{
		DecoyBulletObject result = null;
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return null;
		}
		List<StageObject> decoyList = MonoBehaviourSingleton<StageObjectManager>.I.decoyList;
		if (decoyList.IsNullOrEmpty())
		{
			return null;
		}
		float num = float.MaxValue;
		int i = 0;
		for (int count = decoyList.Count; i < count; i++)
		{
			DecoyBulletObject decoyBulletObject = decoyList[i] as DecoyBulletObject;
			if (!(decoyBulletObject == null) && decoyBulletObject.IsActive())
			{
				float lengthWithBetweenPosition = GetLengthWithBetweenPosition(basePos, decoyBulletObject._position);
				if (lengthWithBetweenPosition < num)
				{
					result = decoyBulletObject;
					num = lengthWithBetweenPosition;
				}
			}
		}
		return result;
	}

	public static FieldWaveTargetObject GetNearestWaveMatchTargetObject(Vector3 basePos)
	{
		FieldWaveTargetObject result = null;
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return null;
		}
		List<StageObject> waveTargetList = MonoBehaviourSingleton<StageObjectManager>.I.waveTargetList;
		if (waveTargetList.IsNullOrEmpty())
		{
			return null;
		}
		float num = float.MaxValue;
		int i = 0;
		for (int count = waveTargetList.Count; i < count; i++)
		{
			FieldWaveTargetObject fieldWaveTargetObject = waveTargetList[i] as FieldWaveTargetObject;
			if (!(fieldWaveTargetObject == null) && !fieldWaveTargetObject.isDead)
			{
				float sqrLengthWithBetweenPosition = GetSqrLengthWithBetweenPosition(basePos, fieldWaveTargetObject._position);
				if (sqrLengthWithBetweenPosition < num)
				{
					result = fieldWaveTargetObject;
					num = sqrLengthWithBetweenPosition;
				}
			}
		}
		return result;
	}

	public static float GetLengthWithBetweenObject(StageObject client, StageObject target)
	{
		Vector3 targetPosition = client.GetTargetPosition(target);
		return GetLengthWithBetweenPosition(client._position, targetPosition);
	}

	public static float GetLengthWithBetweenPosition(Vector3 client_pos, Vector3 target_pos)
	{
		if (client_pos == target_pos)
		{
			return 0f;
		}
		Vector3 vector = target_pos - client_pos;
		vector.y = 0f;
		return vector.magnitude;
	}

	public static float GetSqrLengthWithBetweenPosition(Vector3 client_pos, Vector3 target_pos)
	{
		if (client_pos == target_pos)
		{
			return 0f;
		}
		Vector3 vector = target_pos - client_pos;
		vector.y = 0f;
		return vector.sqrMagnitude;
	}

	public static int GetObstacleMask()
	{
		return 393728;
	}

	public static int GetWallAndBlockMask()
	{
		return 131584;
	}

	public static int GetOpponentMask(StageObject client)
	{
		if (client is Player)
		{
			return 2048;
		}
		if (client is Enemy)
		{
			return 256;
		}
		return 0;
	}

	public static bool RaycastObstacle(StageObject client, StageObject target, out RaycastHit hit)
	{
		Vector3 position = client._position;
		Vector3 position2 = target._position;
		int obstacleMask = GetObstacleMask();
		return RaycastForTargetPos(position, position2, obstacleMask, out hit);
	}

	public static bool RaycastObstacle(StageObject client, Vector3 target_pos, out RaycastHit hit)
	{
		Vector3 position = client._position;
		int obstacleMask = GetObstacleMask();
		return RaycastForTargetPos(position, target_pos, obstacleMask, out hit);
	}

	public static bool RaycastWallAndBlock(StageObject client, Vector3 targetPos, out RaycastHit hit)
	{
		Vector3 position = client._position;
		int wallAndBlockMask = GetWallAndBlockMask();
		return RaycastForTargetPos(position, targetPos, wallAndBlockMask, out hit);
	}

	public static bool RaycastOpponent(StageObject client, Vector3 target_pos, out RaycastHit hit)
	{
		Vector3 position = client._position;
		int opponentMask = GetOpponentMask(client);
		return RaycastForTargetPos(position, target_pos, opponentMask, out hit);
	}

	public static bool RaycastObstacleOrOpponent(StageObject client, Vector3 target_pos, out RaycastHit hit)
	{
		Vector3 position = client._position;
		int mask = GetObstacleMask() | GetOpponentMask(client);
		return RaycastForTargetPos(position, target_pos, mask, out hit);
	}

	public static bool RaycastForTargetPos(Vector3 pos, Vector3 target, int mask, out RaycastHit hit)
	{
		hit = default(RaycastHit);
		if (mask == 0)
		{
			return false;
		}
		Vector3 direction = target - pos;
		float magnitude = direction.magnitude;
		return Physics.Raycast(pos, direction, out hit, magnitude, mask);
	}

	public static bool IsHitObstacleOrOpponentWithPlace(StageObject client, PLACE place, float range)
	{
		Vector3 direction = client.transform.TransformDirection(place.GetVector3());
		int layerMask = GetObstacleMask() | GetOpponentMask(client);
		return Physics.Raycast(client._position, direction, range, layerMask);
	}

	public static bool IsHitObjectFromMoveObject(Transform moveObj, Transform checkObj, float radius, int mask)
	{
		Vector3 direction = moveObj.TransformDirection(Vector3.forward);
		float magnitude = (moveObj.position - checkObj.position).magnitude;
		RaycastHit[] array = Physics.SphereCastAll(moveObj.position, radius, direction, magnitude, mask);
		foreach (RaycastHit raycastHit in array)
		{
			if (raycastHit.transform == checkObj)
			{
				return true;
			}
		}
		return false;
	}

	public static void DrawRay(StageObject client, Vector3 target_pos, float len, Color color, float sec)
	{
		Vector3 position = client._position;
		Vector3 a = target_pos - position;
		if (len < 0f)
		{
			len = a.magnitude;
		}
		a.Normalize();
		Debug.DrawRay(position, a * len, color, sec);
	}
}
