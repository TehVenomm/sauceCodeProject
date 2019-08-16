using Network;
using System.Collections;
using UnityEngine;

public class QuestFeverGachaDirector : QuestGachaDirectorBase
{
	public const string ENEMY_MOTION_BASE_LAYER = "Base Layer.";

	public const string ENEMY_MOTION_STATE_IDLE = "Base Layer.IDLE";

	public const string ENEMY_MOTION_STATE_GACHA = "Base Layer.GACHA_SINGLE";

	public const string ENEMY_MOTION_STATE_GACHA11 = "Base Layer.GACHA_11";

	public const float MAGIC_CIRCLE_EFFECT_FINISH_TIME = 13.5f;

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
		SetLinkCamera(is_link: true);
		GachaResult currentResult = MonoBehaviourSingleton<GachaManager>.I.gachaResultBonus;
		int reamMax = (currentResult != null) ? currentResult.reward.Count : 0;
		EnemyTable.EnemyData[] enemy_datas = new EnemyTable.EnemyData[reamMax];
		QuestTable.QuestTableData[] quest_datas = new QuestTable.QuestTableData[reamMax];
		EnemyLoader[] enemyLoaderList = new EnemyLoader[reamMax];
		if (MonoBehaviourSingleton<GachaManager>.IsValid() && currentResult != null)
		{
			int count = currentResult.reward.Count;
			int k = 0;
			for (int num = count; k < num; k++)
			{
				GachaResult.GachaReward gachaReward = currentResult.reward[k];
				uint itemId = (uint)gachaReward.itemId;
				quest_datas[k] = Singleton<QuestTable>.I.GetQuestData(itemId);
				if (quest_datas[k] != null)
				{
					enemy_datas[k] = Singleton<EnemyTable>.I.GetEnemyData((uint)quest_datas[k].GetMainEnemyID());
					if (enemy_datas[k] == null)
					{
						Log.Error("EnemyTable[{0}] == null", quest_datas[k].GetMainEnemyID());
					}
				}
				else
				{
					Log.Error("QuestTable[{0}] == null", gachaReward.itemId);
					quest_datas[k] = new QuestTable.QuestTableData();
					enemy_datas[k] = new EnemyTable.EnemyData();
				}
			}
		}
		NPCLoader npc_loader = LoadNPC();
		npc_loader.Load(Singleton<NPCTable>.I.GetNPCData(2).npcModelID, 0, need_shadow: false, enable_light_probes: true, SHADER_TYPE.NORMAL, null);
		for (int l = 0; l < reamMax; l++)
		{
			float displayScale = enemy_datas[l].modelScale;
			int anim_id = enemy_datas[l].animId;
			int modelId = enemy_datas[l].modelId;
			OutGameSettingsManager.EnemyDisplayInfo enemyDisplayInfo = MonoBehaviourSingleton<OutGameSettingsManager>.I.SearchEnemyDisplayInfoForGacha(enemy_datas[l]);
			if (enemyDisplayInfo != null)
			{
				anim_id = enemyDisplayInfo.animID;
				displayScale = enemyDisplayInfo.gachaScale;
			}
			enemyLoaderList[l] = LoadEnemy(enemyPositions[l], modelId, anim_id, displayScale, enemy_datas[l].baseEffectName, enemy_datas[l].baseEffectNode);
		}
		int j = 0;
		for (int i = reamMax; j < i; j++)
		{
			while (enemyLoaderList[j].isLoading)
			{
				yield return null;
			}
			enemyLoaderList[j].ApplyGachaDisplayScaleToParentNode();
			CheckAndReplaceShader(enemyLoaderList[j]);
			enemyLoaderList[j].get_gameObject().SetActive(false);
		}
		LoadingQueue lo_queue = new LoadingQueue(this);
		CacheAudio(lo_queue);
		for (int m = 0; m < reamMax; m++)
		{
			CacheEnemyAudio(enemy_datas[m], lo_queue);
		}
		while (npc_loader.isLoading)
		{
			yield return null;
		}
		while (lo_queue.IsLoading())
		{
			yield return null;
		}
		PlayerAnimCtrl npc_anim = PlayerAnimCtrl.Get(npc_loader.animator, PLCA.IDLE_01);
		CreateNPCEffect(npc_loader.model);
		yield return null;
		stageAnimator.set_cullingMode(0);
		stageAnimator.Rebind();
		stageAnimator.Play("StageAnim_Main");
		SetAnimatorInteger("npc_id", 2);
		SetAnimatorInteger("ream_max", reamMax);
		Play("MainAnim_Start");
		PlayEffect(startEffectPrefabs[0]);
		npc_anim.Play(PLCA.QUEST_GACHA, instant: true);
		PlayAudio(AUDIO.OPENING_01);
		while (Step(0.5f))
		{
			yield return null;
		}
		PlayAudio(AUDIO.OPENING_02);
		PlayAudio(AUDIO.OPENING_03);
		while (Step(1.4f))
		{
			yield return null;
		}
		PlayAudio(AUDIO.OPENING_04);
		while (Step(2.6f))
		{
			yield return null;
		}
		PlayAudio(AUDIO.DOOR_01);
		while (Step(3.2f))
		{
			yield return null;
		}
		PlayAudio(AUDIO.DOOR_02);
		while (Step(3.38f))
		{
			yield return null;
		}
		PlayAudio(AUDIO.MAGI_INTRO_01);
		while (Step(5.1f))
		{
			yield return null;
		}
		PlayAudio(AUDIO.MAGI_INTRO_02);
		while (Step(5.5f))
		{
			yield return null;
		}
		PlayEffect(startEffectPrefabs[1]);
		Transform[] magic_effects = (Transform[])new Transform[reamMax];
		Transform[] end_effects = (Transform[])new Transform[reamMax];
		int meteor_step = 0;
		int magic_step = 0;
		int max_step = reamMax;
		time -= Time.get_deltaTime();
		while (meteor_step < max_step || magic_step < max_step)
		{
			time += Time.get_deltaTime();
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
			yield return null;
		}
		int idx = magic_step - 1;
		if (idx < max_step && idx >= 0)
		{
			PlayMagicAudio((int)quest_datas[idx].rarity);
		}
		while (Step(13.5f))
		{
			yield return null;
		}
		if (skip && !m_isSkipAll)
		{
			if (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
			{
				yield return null;
			}
			Time.set_timeScale(1f);
			skip = false;
			time = 13.5f;
			yield return MonoBehaviourSingleton<TransitionManager>.I.In();
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
				int num2 = end_step - 1;
				if (enemyLoaderList[num2] != null)
				{
					enemyLoaderList[num2].get_gameObject().SetActive(false);
				}
				if (magic_effects[num2] != null)
				{
					Object.Destroy(magic_effects[num2].get_gameObject());
					magic_effects[num2] = null;
				}
				if (end_effects[num2] != null)
				{
					Object.Destroy(end_effects[num2].get_gameObject());
					end_effects[num2] = null;
				}
			}
			EnemyLoader nowEnemyLoader = enemyLoaderList[end_step];
			nowEnemyLoader.get_gameObject().SetActive(true);
			if (!skip)
			{
				string animStateName = "Base Layer.GACHA_11";
				if (end_step == enemyLoaderList.Length - 1)
				{
					animStateName = "Base Layer.GACHA_SINGLE";
				}
				PlayEnemyAnimation(nowEnemyLoader, animStateName);
			}
			rarity_type = quest_datas[end_step].rarity;
			int effect_rarity = rarity_type.ToRarityExpressionID();
			if (rarity_type == RARITY_TYPE.SS)
			{
				effect_rarity = 3;
			}
			end_effects[end_step] = PlayEffect(magicCircles[end_step], endEffectPrefabs[effect_rarity]);
			Play("LoopEnemyAppear");
			bool is_short = end_step < reamMax;
			PlayAppearAudio(rarity_type, is_short);
			if (enemy_datas.Length > end_step)
			{
				PlayEnemyAudio(enemy_datas[end_step], is_short);
			}
			end_step++;
			if (end_step >= reamMax)
			{
				break;
			}
			float waitTime = 0f;
			while (waitTime < showRarityWaitTime)
			{
				waitTime += Time.get_deltaTime();
				if (base.IsSkipAppearEnemy)
				{
					waitTime = showRarityWaitTime;
				}
				yield return null;
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
				yield return null;
			}
			sectionCommandReceiver.ActivateSkipButton();
			ResetSkipAppearEnemyFlag();
		}
		yield return (object)new WaitForSeconds(lastShowRarityWaitTime);
		sectionCommandReceiver.OnShowRarity(rarity_type);
		end_time2 += end_step_time_last;
		while (Step(end_time2))
		{
			yield return null;
		}
		if (skip)
		{
			while (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
			{
				yield return null;
			}
			int lastIndex = enemyLoaderList.Length - 1;
			if (lastIndex >= 0)
			{
				PlayEnemyAnimation(enemyLoaderList[lastIndex], "Base Layer.IDLE");
			}
			Time.set_timeScale(1f);
			if (MonoBehaviourSingleton<TransitionManager>.I.isTransing)
			{
				yield return MonoBehaviourSingleton<TransitionManager>.I.In();
			}
		}
		else
		{
			skip = true;
		}
		sectionCommandReceiver.OnHideRarity();
		Time.set_timeScale(1f);
		sectionCommandReceiver.OnEnd();
	}

	public override void SkipAll()
	{
		if (!m_isSkipAll && !skip)
		{
			m_isSkipAll = true;
			skip = true;
			this.StartCoroutine(DoSkipAll());
		}
	}

	private IEnumerator DoSkipAll()
	{
		yield return MonoBehaviourSingleton<TransitionManager>.I.Out();
		Time.set_timeScale(100f);
	}
}
