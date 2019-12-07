using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VorgonPreEventController : MonoBehaviour
{
	public static readonly int[] NPC_ID_LIST = new int[3]
	{
		991,
		992,
		993
	};

	public static readonly int[] NPC_WEAPON_ID_LIST = new int[3]
	{
		10440010,
		10240000,
		10000000
	};

	private const int ENERGY_BALL_INDEX = 14;

	private Enemy enemy;

	private QuestManager.VorgonQuetType questType;

	private bool finish;

	private bool vorgonBarrierBrokenProc;

	private bool npcStartedTalking;

	private bool npcAnnouncedHint;

	private IEnumerator Start()
	{
		while (enemy == null)
		{
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				enemy = MonoBehaviourSingleton<StageObjectManager>.I.boss;
			}
			yield return null;
		}
		if (MonoBehaviourSingleton<QuestManager>.IsValid())
		{
			questType = MonoBehaviourSingleton<QuestManager>.I.GetVorgonQuestType();
		}
		while (!MonoBehaviourSingleton<InGameProgress>.I.isBattleStart)
		{
			yield return null;
		}
		if (questType == QuestManager.VorgonQuetType.BATTLE_WITH_VORGON)
		{
			enemy.GetComponentInChildren<EnemyBrain>().actionCtrl.actions[14].isForceDisable = true;
		}
	}

	private void UpdateWyburnBattle()
	{
		if ((float)enemy.hp / (float)enemy.hpMax <= 0.33f)
		{
			MonoBehaviourSingleton<InGameProgress>.I.BattleComplete();
			finish = true;
		}
	}

	private void UpdateVorgonBattle()
	{
		if (!npcStartedTalking)
		{
			npcStartedTalking = true;
			StartCoroutine("UpdateNPCTalking");
		}
		if (vorgonBarrierBrokenProc && !npcAnnouncedHint && !enemy.IsValidBarrier)
		{
			StopCoroutine("UpdateNPCTalking");
			StartCoroutine("UpdateNPCTalkingAfterBreakedBariier");
			npcAnnouncedHint = true;
		}
		if ((float)enemy.hp / (float)enemy.hpMax <= 0.5f || MonoBehaviourSingleton<InGameProgress>.I.remaindTime < 84f)
		{
			StopCoroutine("UpdateNPCTalking");
			StopCoroutine("UpdateNPCTalkingAfterBreakedBariier");
			EnemyBrain componentInChildren = enemy.GetComponentInChildren<EnemyBrain>();
			for (int i = 0; i < componentInChildren.actionCtrl.actions.Count; i++)
			{
				componentInChildren.actionCtrl.actions[i].isForceDisable = (i == 14);
				finish = true;
			}
		}
	}

	private IEnumerator UpdateNPCTalking()
	{
		GameSceneTables.SectionData sectionData = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection().sectionData;
		yield return new WaitForSeconds(5f);
		Player hound_A = MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList[0] as Player;
		Player hound_B = MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList[1] as Player;
		Player beginner = MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList[2] as Player;
		hound_A.uiPlayerStatusGizmo.SetChatDuration(4f);
		hound_B.uiPlayerStatusGizmo.SetChatDuration(4f);
		beginner.uiPlayerStatusGizmo.SetChatDuration(4f);
		beginner.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_0"));
		yield return new WaitForSeconds(5f);
		hound_A.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_1"));
		yield return new WaitForSeconds(10f);
		hound_B.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_2"));
		yield return new WaitForSeconds(4f);
		hound_B.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_3"));
		yield return new WaitForSeconds(6f);
		beginner.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_4"));
		yield return new WaitForSeconds(4f);
		hound_A.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_5"));
		yield return new WaitForSeconds(10f);
		hound_B.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_6"));
		yield return new WaitForSeconds(5f);
		hound_A.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_7"));
		yield return new WaitForSeconds(4f);
		hound_A.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_8"));
		yield return new WaitForSeconds(4f);
		if ((int)enemy.BarrierHp > 1)
		{
			enemy.BarrierHp = 1;
		}
		vorgonBarrierBrokenProc = true;
	}

	private IEnumerator UpdateNPCTalkingAfterBreakedBariier()
	{
		GameSceneTables.SectionData sectionData = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection().sectionData;
		Player hound_A = MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList[0] as Player;
		Player hound_B = MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList[1] as Player;
		Player beginner = MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList[2] as Player;
		hound_B.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_9"));
		yield return new WaitForSeconds(4f);
		hound_A.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_10"));
		yield return new WaitForSeconds(4f);
		beginner.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_11"));
		yield return new WaitForSeconds(20f);
		hound_B.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_12"));
		EnemyBrain componentInChildren = enemy.GetComponentInChildren<EnemyBrain>();
		for (int i = 0; i < componentInChildren.actionCtrl.actions.Count; i++)
		{
			componentInChildren.actionCtrl.actions[i].isForceDisable = (i != 14);
			finish = true;
		}
		yield return new WaitForSeconds(5f);
		hound_A.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_13"));
		yield return null;
	}

	private void UpdateNPC()
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return;
		}
		List<StageObject> nonplayerList = MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList;
		for (int i = 0; i < nonplayerList.Count; i++)
		{
			Player player = nonplayerList[i] as Player;
			if (!(player == null))
			{
				int num = (int)((float)player.hpMax * 0.8f);
				if (player.hp < num)
				{
					player.hp = num;
				}
			}
		}
	}

	private void Update()
	{
		if (!finish)
		{
			switch (questType)
			{
			case QuestManager.VorgonQuetType.BATTLE_WITH_WYBURN:
				UpdateWyburnBattle();
				break;
			case QuestManager.VorgonQuetType.BATTLE_WITH_VORGON:
				UpdateNPC();
				UpdateVorgonBattle();
				break;
			}
		}
	}
}
