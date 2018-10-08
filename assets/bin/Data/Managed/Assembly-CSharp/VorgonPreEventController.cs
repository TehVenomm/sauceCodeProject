using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VorgonPreEventController
{
	private const int ENERGY_BALL_INDEX = 14;

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

	private Enemy enemy;

	private QuestManager.VorgonQuetType questType;

	private bool finish;

	private bool vorgonBarrierBrokenProc;

	private bool npcStartedTalking;

	private bool npcAnnouncedHint;

	public VorgonPreEventController()
		: this()
	{
	}

	private IEnumerator Start()
	{
		while (enemy == null)
		{
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				enemy = MonoBehaviourSingleton<StageObjectManager>.I.boss;
			}
			yield return (object)null;
		}
		if (MonoBehaviourSingleton<QuestManager>.IsValid())
		{
			questType = MonoBehaviourSingleton<QuestManager>.I.GetVorgonQuestType();
		}
		while (!MonoBehaviourSingleton<InGameProgress>.I.isBattleStart)
		{
			yield return (object)null;
		}
		if (questType == QuestManager.VorgonQuetType.BATTLE_WITH_VORGON)
		{
			EnemyBrain brain = enemy.GetComponentInChildren<EnemyBrain>();
			brain.actionCtrl.actions[14].isForceDisable = true;
		}
	}

	private void UpdateWyburnBattle()
	{
		if ((float)enemy.hp / (float)enemy.hpMax <= 0.33f)
		{
			MonoBehaviourSingleton<InGameProgress>.I.BattleComplete(false);
			finish = true;
		}
	}

	private void UpdateVorgonBattle()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		if (!npcStartedTalking)
		{
			npcStartedTalking = true;
			this.StartCoroutine("UpdateNPCTalking");
		}
		if (vorgonBarrierBrokenProc && !npcAnnouncedHint && !enemy.IsValidBarrier)
		{
			this.StopCoroutine("UpdateNPCTalking");
			this.StartCoroutine("UpdateNPCTalkingAfterBreakedBariier");
			npcAnnouncedHint = true;
		}
		if ((float)enemy.hp / (float)enemy.hpMax <= 0.5f || MonoBehaviourSingleton<InGameProgress>.I.remaindTime < 84f)
		{
			this.StopCoroutine("UpdateNPCTalking");
			this.StopCoroutine("UpdateNPCTalkingAfterBreakedBariier");
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
		yield return (object)new WaitForSeconds(5f);
		Player hound_A = MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList[0] as Player;
		Player hound_B = MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList[1] as Player;
		Player beginner = MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList[2] as Player;
		hound_A.uiPlayerStatusGizmo.SetChatDuration(4f);
		hound_B.uiPlayerStatusGizmo.SetChatDuration(4f);
		beginner.uiPlayerStatusGizmo.SetChatDuration(4f);
		beginner.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_0"));
		yield return (object)new WaitForSeconds(5f);
		hound_A.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_1"));
		yield return (object)new WaitForSeconds(10f);
		hound_B.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_2"));
		yield return (object)new WaitForSeconds(4f);
		hound_B.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_3"));
		yield return (object)new WaitForSeconds(6f);
		beginner.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_4"));
		yield return (object)new WaitForSeconds(4f);
		hound_A.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_5"));
		yield return (object)new WaitForSeconds(10f);
		hound_B.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_6"));
		yield return (object)new WaitForSeconds(5f);
		hound_A.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_7"));
		yield return (object)new WaitForSeconds(4f);
		hound_A.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_8"));
		yield return (object)new WaitForSeconds(4f);
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
		yield return (object)new WaitForSeconds(4f);
		hound_A.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_10"));
		yield return (object)new WaitForSeconds(4f);
		beginner.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_11"));
		yield return (object)new WaitForSeconds(20f);
		hound_B.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_12"));
		EnemyBrain brain = enemy.GetComponentInChildren<EnemyBrain>();
		for (int i = 0; i < brain.actionCtrl.actions.Count; i++)
		{
			brain.actionCtrl.actions[i].isForceDisable = (i != 14);
			finish = true;
		}
		yield return (object)new WaitForSeconds(5f);
		hound_A.uiPlayerStatusGizmo.SayChat(sectionData.GetText("STR_VORGON_HINT_13"));
		yield return (object)null;
	}

	private void UpdateNPC()
	{
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
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
