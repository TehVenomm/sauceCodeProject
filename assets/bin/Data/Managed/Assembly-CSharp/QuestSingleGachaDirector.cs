using Network;
using System.Collections;
using UnityEngine;

public class QuestSingleGachaDirector : QuestGachaDirectorBase
{
	public Transform enemyPosition;

	protected override IEnumerator GetDirectionCoroutine()
	{
		return DoQuestGacha();
	}

	private IEnumerator DoQuestGacha()
	{
		Init();
		int display_rarity = 0;
		SetLinkCamera(true);
		int enemy_id = 0;
		EnemyTable.EnemyData enemy_data = null;
		if (MonoBehaviourSingleton<GachaManager>.I.gachaResult != null)
		{
			GachaResult.GachaReward reward = MonoBehaviourSingleton<GachaManager>.I.gachaResult.reward[0];
			uint reward_quest_id = (uint)reward.itemId;
			QuestTable.QuestTableData quest_data = Singleton<QuestTable>.I.GetQuestData(reward_quest_id);
			if (quest_data != null)
			{
				enemy_data = Singleton<EnemyTable>.I.GetEnemyData((uint)quest_data.GetMainEnemyID());
			}
		}
		if (enemy_data == null)
		{
			if (enemy_id == 0)
			{
				enemy_id = 1101001;
			}
			enemy_data = Singleton<EnemyTable>.I.GetEnemyData((uint)enemy_id);
		}
		NPCLoader npc_loader = LoadNPC();
		EnemyLoader enemy_loader = null;
		if (enemy_data != null)
		{
			int displayAnimID = enemy_data.animId;
			OutGameSettingsManager.EnemyDisplayInfo displayInfo = MonoBehaviourSingleton<OutGameSettingsManager>.I.SearchEnemyDisplayInfoForGacha(enemy_data);
			int modelID = enemy_data.modelId;
			float displayScale = enemy_data.modelScale;
			if (displayInfo != null)
			{
				displayAnimID = displayInfo.animID;
				displayScale = displayInfo.gachaScale;
			}
			enemy_loader = LoadEnemy(enemyPosition, modelID, displayAnimID, displayScale, enemy_data.baseEffectName, enemy_data.baseEffectNode);
			while (enemy_loader.isLoading)
			{
				yield return (object)null;
			}
			enemy_loader.ApplyGachaDisplayScaleToParentNode();
			CheckAndReplaceShader(enemy_loader);
			enemy_loader.get_gameObject().SetActive(false);
		}
		while (npc_loader.isLoading)
		{
			yield return (object)null;
		}
		LoadingQueue lo_queue = new LoadingQueue(this);
		CacheAudio(lo_queue);
		if (enemy_data != null)
		{
			CacheEnemyAudio(enemy_data, lo_queue);
		}
		while (lo_queue.IsLoading())
		{
			yield return (object)null;
		}
		PlayerAnimCtrl npc_anim = PlayerAnimCtrl.Get(npc_loader.animator, PLCA.IDLE_01, null, null, null);
		CreateNPCEffect(npc_loader.model);
		yield return (object)null;
		stageAnimator.set_cullingMode(0);
		stageAnimator.Rebind();
		targetRarity = MonoBehaviourSingleton<GachaManager>.I.GetMaxRarity().ToRarityExpressionID() + 1;
		if (targetRarity > 4)
		{
			targetRarity = 4;
		}
		stageAnimator.Play("StageAnim_Main");
		Play("MainAnim_Start", null, 0f);
		PlayEffect(startEffectPrefabs[0]);
		npc_anim.Play(PLCA.QUEST_GACHA, true);
		bool rankup3 = UpdateDisplayRarity(ref display_rarity);
		PlayAudio(AUDIO.OPENING_01);
		while (Step(0.5f))
		{
			yield return (object)null;
		}
		PlayAudio(AUDIO.OPENING_02);
		PlayAudio(AUDIO.OPENING_03);
		while (Step(1.4f))
		{
			yield return (object)null;
		}
		PlayAudio(AUDIO.OPENING_04);
		while (Step(2.6f))
		{
			yield return (object)null;
		}
		PlayAudio(AUDIO.DOOR_01);
		while (Step(3.2f))
		{
			yield return (object)null;
		}
		PlayAudio(AUDIO.DOOR_02);
		while (Step(3.38f))
		{
			yield return (object)null;
		}
		PlayAudio(AUDIO.MAGI_INTRO_01);
		while (Step(6.5f))
		{
			yield return (object)null;
		}
		npc_loader.get_gameObject().SetActive(false);
		PlayMeteorEffect(display_rarity);
		PlayAudio(AUDIO.METEOR_01);
		while (Step(7.5f))
		{
			yield return (object)null;
		}
		PlayMagicEffect(display_rarity, rankup3);
		PlayMagicAudio(display_rarity);
		rankup3 = UpdateDisplayRarity(ref display_rarity);
		PlayMeteorEffect(display_rarity);
		while (Step(8.5f))
		{
			yield return (object)null;
		}
		PlayMagicEffect(display_rarity, rankup3);
		PlayMagicAudio(display_rarity);
		rankup3 = UpdateDisplayRarity(ref display_rarity);
		PlayMeteorEffect(display_rarity);
		PlayAudio(AUDIO.METEOR_02);
		while (Step(9.5f))
		{
			yield return (object)null;
		}
		PlayMagicEffect(display_rarity, rankup3);
		PlayMagicAudio(display_rarity);
		while (Step(10.5f))
		{
			yield return (object)null;
		}
		if (enemy_loader != null)
		{
			enemy_loader.get_gameObject().SetActive(true);
		}
		if (!skip)
		{
			PlayEnemyAnimation(enemy_loader, "Base Layer.GACHA_SINGLE");
		}
		Play("MainAnim_End", null, 0f);
		UpdateDisplayRarity(ref display_rarity);
		PlayEndEffect(display_rarity);
		RARITY_TYPE rarity = MonoBehaviourSingleton<GachaManager>.I.GetMaxRarity();
		PlayAppearAudio(rarity, false);
		if (enemy_data != null)
		{
			PlayEnemyAudio(enemy_data, false);
		}
		while (Step(11.5f))
		{
			yield return (object)null;
		}
		while (Step(13f))
		{
			yield return (object)null;
		}
		if (skip)
		{
			while (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
			{
				yield return (object)null;
			}
			PlayEnemyAnimation(enemy_loader, "Base Layer.IDLE");
			Time.set_timeScale(1f);
			if (MonoBehaviourSingleton<TransitionManager>.I.isTransing)
			{
				yield return (object)MonoBehaviourSingleton<TransitionManager>.I.In();
			}
		}
		else
		{
			skip = true;
		}
		Time.set_timeScale(1f);
		sectionCommandReceiver.OnEnd();
	}
}
