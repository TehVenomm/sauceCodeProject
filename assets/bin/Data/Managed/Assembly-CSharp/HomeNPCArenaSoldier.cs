using System.Collections;
using UnityEngine;

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
		{
			animCtrl.Play(npc.nearAnim, false);
		}
		else if (MonoBehaviourSingleton<UserInfoManager>.I.isJoinedArenaRanking && animCtrl.playingAnim == PLCA.IDLE_01 && isTurned)
		{
			animCtrl.Play(PLCA.THROUGH_BOW, false);
		}
		else if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 50)
		{
			if (!isTurned)
			{
				StartCoroutine(PlayThroughTurn());
				isTurned = true;
			}
		}
		else
		{
			animCtrl.Play(npc.nearAnim, false);
		}
	}

	private IEnumerator PlayThroughTurn()
	{
		animCtrl.Play(PLCA.THROUGH_TURN, false);
		int turnSign = (base.npcInfo.scaleX > 0f) ? 1 : (-1);
		Vector3 eulerAngles = base._transform.eulerAngles;
		float beforeTurnRot = eulerAngles.y;
		while (rotatedDegree <= 90f)
		{
			rotatedDegree += 3.6f;
			if (rotatedDegree > 90f)
			{
				rotatedDegree = 90f;
			}
			float rotateY = beforeTurnRot + rotatedDegree * (float)turnSign;
			base._transform.rotation = Quaternion.Euler(0f, rotateY, 0f);
			yield return (object)null;
		}
	}
}
