using Network;
using System.Collections;
using UnityEngine;

public class QuestReamGachaDirector : QuestGachaDirectorBase
{
	public const string ENEMY_MOTION_BASE_LAYER = "Base Layer.";

	public const string ENEMY_MOTION_STATE_IDLE = "Base Layer.IDLE";

	public const string ENEMY_MOTION_STATE_GACHA = "Base Layer.GACHA_SINGLE";

	public const string ENEMY_MOTION_STATE_GACHA11 = "Base Layer.GACHA_11";

	public const float MAGIC_CIRCLE_EFFECT_FINISH_TIME = 13.5f;

	public const int REAM_MAX = 11;

	public Transform[] magicCircles;

	public Transform[] enemyPositions;

	public float[] meteorTimings;

	public float[] magicTimings;

	public float[] meteorSETimings;

	public float[] magicSETimings;

	public float showRarityWaitTime = 1f;

	public float lastShowRarityWaitTime = 2.5f;

	private bool m_isSkipAll;

	protected override IEnumerator GetDirectionCoroutine()
	{
		return DoQuestGacha();
	}

	private IEnumerator DoQuestGacha()
	{
		m_isSkipAll = false;
		Init();
		SetLinkCamera(true);
		EnemyLoader[] enemyLoaderList = new EnemyLoader[11];
		EnemyTable.EnemyData[] enemy_datas = new EnemyTable.EnemyData[11];
		QuestTable.QuestTableData[] quest_datas = new QuestTable.QuestTableData[11];
		GachaResult currentResult = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult();
		if (MonoBehaviourSingleton<GachaManager>.IsValid() && currentResult != null)
		{
			int count = currentResult.reward.Count;
			int i2 = 0;
			for (int n = count; i2 < n; i2++)
			{
				GachaResult.GachaReward reward = currentResult.reward[i2];
				uint reward_quest_id = (uint)reward.itemId;
				quest_datas[i2] = Singleton<QuestTable>.I.GetQuestData(reward_quest_id);
				if (quest_datas[i2] != null)
				{
					enemy_datas[i2] = Singleton<EnemyTable>.I.GetEnemyData((uint)quest_datas[i2].GetMainEnemyID());
					if (enemy_datas[i2] == null)
					{
						Log.Error("EnemyTable[{0}] == null", quest_datas[i2].GetMainEnemyID());
					}
				}
				else
				{
					Log.Error("QuestTable[{0}] == null", reward.itemId);
					quest_datas[i2] = new QuestTable.QuestTableData();
					enemy_datas[i2] = new EnemyTable.EnemyData();
				}
			}
		}
		NPCLoader npc_loader = LoadNPC();
		npc_loader.Load(Singleton<NPCTable>.I.GetNPCData(2).npcModelID, 0, false, true, SHADER_TYPE.NORMAL, null);
		int m = 0;
		for (int l = 11; m < l; m++)
		{
			float scale = enemy_datas[m].modelScale;
			int displayAnimID = enemy_datas[m].animId;
			int modelID = enemy_datas[m].modelId;
			OutGameSettingsManager.EnemyDisplayInfo displayInfo = MonoBehaviourSingleton<OutGameSettingsManager>.I.SearchEnemyDisplayInfoForGacha(enemy_datas[m]);
			if (displayInfo != null)
			{
				displayAnimID = displayInfo.animID;
				scale = displayInfo.gachaScale;
			}
			enemyLoaderList[m] = LoadEnemy(enemyPositions[m], modelID, displayAnimID, scale, enemy_datas[m].baseEffectName, enemy_datas[m].baseEffectNode);
		}
		int k = 0;
		for (int j = 11; k < j; k++)
		{
			while (enemyLoaderList[k].isLoading)
			{
				yield return (object)null;
			}
			enemyLoaderList[k].ApplyGachaDisplayScaleToParentNode();
			CheckAndReplaceShader(enemyLoaderList[k]);
			enemyLoaderList[k].gameObject.SetActive(false);
		}
		LoadingQueue lo_queue = new LoadingQueue(this);
		CacheAudio(lo_queue);
		for (int i = 0; i < 11; i++)
		{
			CacheEnemyAudio(enemy_datas[i], lo_queue);
		}
		while (npc_loader.isLoading)
		{
			yield return (object)null;
		}
		while (lo_queue.IsLoading())
		{
			yield return (object)null;
		}
		PlayerAnimCtrl npc_anim = PlayerAnimCtrl.Get(npc_loader.animator, PLCA.IDLE_01, null, null, null);
		CreateNPCEffect(npc_loader.model);
		yield return (object)null;
		stageAnimator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		stageAnimator.Rebind();
		stageAnimator.Play("StageAnim_Main");
		Play("MainAnim_Start", null, 0f);
		PlayEffect(startEffectPrefabs[0]);
		npc_anim.Play(PLCA.QUEST_GACHA, true);
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
		while (Step(5.1f))
		{
			yield return (object)null;
		}
		PlayAudio(AUDIO.MAGI_INTRO_02);
		while (Step(5.5f))
		{
			yield return (object)null;
		}
		PlayEffect(startEffectPrefabs[1]);
		Transform[] magic_effects = new Transform[11];
		Transform[] end_effects = new Transform[11];
		int meteor_step = 0;
		int magic_step = 0;
		int max_step = 11;
		time -= Time.deltaTime;
		while (meteor_step < max_step || magic_step < max_step)
		{
			time += Time.deltaTime;
			if (meteor_step < max_step && meteorTimings[meteor_step] <= time)
			{
				PlayEffect(magicCircles[meteor_step], meteorEffectPrefabs[quest_datas[meteor_step].rarity.ToRarityExpressionID()]);
				if (meteor_step == max_step - 1)
				{
					PlayAudio(AUDIO.METEOR_01);
				}
				meteor_step++;
			}
			if (magic_step < max_step && magicTimings[magic_step] <= time)
			{
				magic_effects[magic_step] = PlayEffect(magicCircles[magic_step], magicEffectPrefabs[quest_datas[magic_step].rarity.ToRarityExpressionID()]);
				PlayMagicAudio((int)quest_datas[magic_step].rarity);
				magic_step++;
			}
			yield return (object)null;
		}
		int idx = magic_step - 1;
		if (idx < max_step && idx >= 0)
		{
			PlayMagicAudio((int)quest_datas[idx].rarity);
		}
		while (Step(13.5f))
		{
			yield return (object)null;
		}
		if (skip && !m_isSkipAll)
		{
			if (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
			{
				yield return (object)null;
			}
			Time.timeScale = 1f;
			skip = false;
			time = 13.5f;
			yield return (object)MonoBehaviourSingleton<TransitionManager>.I.In();
			sectionCommandReceiver.ActivateSkipButton();
		}
		ActivateFirstSkipFlag();
		int end_step = 0;
		float end_time2 = 13.5f;
		float end_step_time = 1.5f;
		float end_step_time_last = 2f;
		RARITY_TYPE rarity_type;
		while (true)
		{
			sectionCommandReceiver.OnHideRarity();
			PlayAudio(AUDIO.RARITY_EXPOSITION);
			if (end_step > 0)
			{
				int prevIndex = end_step - 1;
				if ((Object)enemyLoaderList[prevIndex] != (Object)null)
				{
					enemyLoaderList[prevIndex].gameObject.SetActive(false);
				}
				if ((Object)magic_effects[prevIndex] != (Object)null)
				{
					Object.Destroy(magic_effects[prevIndex].gameObject);
					magic_effects[prevIndex] = null;
				}
				if ((Object)end_effects[prevIndex] != (Object)null)
				{
					Object.Destroy(end_effects[prevIndex].gameObject);
					end_effects[prevIndex] = null;
				}
			}
			EnemyLoader nowEnemyLoader = enemyLoaderList[end_step];
			nowEnemyLoader.gameObject.SetActive(true);
			if (!skip)
			{
				string stateName = "Base Layer.GACHA_11";
				if (end_step == enemyLoaderList.Length - 1)
				{
					stateName = "Base Layer.GACHA_SINGLE";
				}
				PlayEnemyAnimation(nowEnemyLoader, stateName);
			}
			rarity_type = quest_datas[end_step].rarity;
			int effect_rarity = rarity_type.ToRarityExpressionID();
			if (rarity_type == RARITY_TYPE.SS)
			{
				effect_rarity = 3;
			}
			end_effects[end_step] = PlayEffect(magicCircles[end_step], endEffectPrefabs[effect_rarity]);
			Play($"MainAnim_End_{end_step + 1:D2}", null, 0f);
			bool is_short = end_step < 11;
			PlayAppearAudio(rarity_type, is_short);
			if (enemy_datas.Length > end_step)
			{
				PlayEnemyAudio(enemy_datas[end_step], is_short);
			}
			end_step++;
			if (end_step >= 11)
			{
				break;
			}
			float waitTime = 0f;
			while (waitTime < showRarityWaitTime)
			{
				waitTime += Time.deltaTime;
				if (base.IsSkipAppearEnemy)
				{
					waitTime = showRarityWaitTime;
				}
				yield return (object)null;
			}
			if (!base.IsSkipAppearEnemy)
			{
				sectionCommandReceiver.OnShowRarity(rarity_type);
			}
			end_time2 += end_step_time;
			while (Step(end_time2))
			{
				if (base.IsSkipAppearEnemy)
				{
					time = end_time2;
				}
				yield return (object)null;
			}
			sectionCommandReceiver.ActivateSkipButton();
			ResetSkipAppearEnemyFlag();
		}
		yield return (object)new WaitForSeconds(lastShowRarityWaitTime);
		sectionCommandReceiver.OnShowRarity(rarity_type);
		end_time2 += end_step_time_last;
		while (Step(end_time2))
		{
			yield return (object)null;
		}
		if (skip)
		{
			while (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
			{
				yield return (object)null;
			}
			int lastIndex = enemyLoaderList.Length - 1;
			if (lastIndex >= 0)
			{
				PlayEnemyAnimation(enemyLoaderList[lastIndex], "Base Layer.IDLE");
			}
			Time.timeScale = 1f;
			if (MonoBehaviourSingleton<TransitionManager>.I.isTransing)
			{
				yield return (object)MonoBehaviourSingleton<TransitionManager>.I.In();
			}
		}
		else
		{
			skip = true;
		}
		sectionCommandReceiver.OnHideRarity();
		Time.timeScale = 1f;
		sectionCommandReceiver.OnEnd();
	}

	public override void SkipAll()
	{
		if (!m_isSkipAll && !skip)
		{
			m_isSkipAll = true;
			skip = true;
			StartCoroutine(DoSkipAll());
		}
	}

	private IEnumerator DoSkipAll()
	{
		yield return (object)MonoBehaviourSingleton<TransitionManager>.I.Out(TransitionManager.TYPE.BLACK);
		Time.timeScale = 100f;
	}
}
