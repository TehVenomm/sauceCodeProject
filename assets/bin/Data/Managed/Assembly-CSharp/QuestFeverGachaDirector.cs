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
		GachaResult gachaResultBonus = MonoBehaviourSingleton<GachaManager>.I.gachaResultBonus;
		int reamMax = gachaResultBonus?.reward.Count ?? 0;
		EnemyTable.EnemyData[] enemy_datas = new EnemyTable.EnemyData[reamMax];
		QuestTable.QuestTableData[] quest_datas = new QuestTable.QuestTableData[reamMax];
		EnemyLoader[] enemyLoaderList = new EnemyLoader[reamMax];
		if (MonoBehaviourSingleton<GachaManager>.IsValid() && gachaResultBonus != null)
		{
			int count = gachaResultBonus.reward.Count;
			int m = 0;
			for (int num = count; m < num; m++)
			{
				GachaResult.GachaReward gachaReward = gachaResultBonus.reward[m];
				uint itemId = (uint)gachaReward.itemId;
				quest_datas[m] = Singleton<QuestTable>.I.GetQuestData(itemId);
				if (quest_datas[m] != null)
				{
					enemy_datas[m] = Singleton<EnemyTable>.I.GetEnemyData((uint)quest_datas[m].GetMainEnemyID());
					if (enemy_datas[m] == null)
					{
						Log.Error("EnemyTable[{0}] == null", quest_datas[m].GetMainEnemyID());
					}
				}
				else
				{
					Log.Error("QuestTable[{0}] == null", gachaReward.itemId);
					quest_datas[m] = new QuestTable.QuestTableData();
					enemy_datas[m] = new EnemyTable.EnemyData();
				}
			}
		}
		NPCLoader npc_loader = LoadNPC();
		npc_loader.Load(Singleton<NPCTable>.I.GetNPCData(2).npcModelID, 0, need_shadow: false, enable_light_probes: true, SHADER_TYPE.NORMAL, null);
		for (int n = 0; n < reamMax; n++)
		{
			float displayScale = enemy_datas[n].modelScale;
			int anim_id = enemy_datas[n].animId;
			int modelId = enemy_datas[n].modelId;
			OutGameSettingsManager.EnemyDisplayInfo enemyDisplayInfo = MonoBehaviourSingleton<OutGameSettingsManager>.I.SearchEnemyDisplayInfoForGacha(enemy_datas[n]);
			if (enemyDisplayInfo != null)
			{
				anim_id = enemyDisplayInfo.animID;
				displayScale = enemyDisplayInfo.gachaScale;
			}
			enemyLoaderList[n] = LoadEnemy(enemyPositions[n], modelId, anim_id, displayScale, enemy_datas[n].baseEffectName, enemy_datas[n].baseEffectNode);
		}
		int l = 0;
		int k = reamMax;
		while (l < k)
		{
			while (enemyLoaderList[l].isLoading)
			{
				yield return null;
			}
			enemyLoaderList[l].ApplyGachaDisplayScaleToParentNode();
			CheckAndReplaceShader(enemyLoaderList[l]);
			enemyLoaderList[l].gameObject.SetActive(value: false);
			int num2 = l + 1;
			l = num2;
		}
		LoadingQueue lo_queue = new LoadingQueue(this);
		CacheAudio(lo_queue);
		for (int num3 = 0; num3 < reamMax; num3++)
		{
			CacheEnemyAudio(enemy_datas[num3], lo_queue);
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
		stageAnimator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
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
		Transform[] magic_effects = new Transform[reamMax];
		Transform[] end_effects = new Transform[reamMax];
		k = 0;
		l = 0;
		int max_step2 = reamMax;
		time -= Time.deltaTime;
		while (k < max_step2 || l < max_step2)
		{
			time += Time.deltaTime;
			if (k < max_step2 && meteorTimings[k] <= time)
			{
				PlayEffect(magicCircles[k], meteorEffectPrefabs[quest_datas[k].rarity.ToRarityExpressionID()]);
				if (k == max_step2 - 1)
				{
					PlayAudio(AUDIO.METEOR_01);
				}
				k++;
			}
			if (l < max_step2 && magicTimings[l] <= time)
			{
				magic_effects[l] = PlayEffect(magicCircles[l], magicEffectPrefabs[quest_datas[l].rarity.ToRarityExpressionID()]);
				PlayMagicAudio((int)quest_datas[l].rarity);
				l++;
			}
			yield return null;
		}
		int num4 = l - 1;
		if (num4 < max_step2 && num4 >= 0)
		{
			PlayMagicAudio((int)quest_datas[num4].rarity);
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
			Time.timeScale = 1f;
			skip = false;
			time = 13.5f;
			yield return MonoBehaviourSingleton<TransitionManager>.I.In();
			sectionCommandReceiver.ActivateSkipButton();
		}
		ActivateFirstSkipFlag();
		max_step2 = 0;
		float end_time2 = 13.5f;
		float end_step_time = 1.5f;
		float end_step_time_last = 2f;
		RARITY_TYPE rarity_type;
		while (true)
		{
			sectionCommandReceiver.OnHideRarity();
			PlayAudio(AUDIO.RARITY_EXPOSITION);
			if (max_step2 > 0)
			{
				int num5 = max_step2 - 1;
				if (enemyLoaderList[num5] != null)
				{
					enemyLoaderList[num5].gameObject.SetActive(value: false);
				}
				if (magic_effects[num5] != null)
				{
					Object.Destroy(magic_effects[num5].gameObject);
					magic_effects[num5] = null;
				}
				if (end_effects[num5] != null)
				{
					Object.Destroy(end_effects[num5].gameObject);
					end_effects[num5] = null;
				}
			}
			EnemyLoader enemyLoader = enemyLoaderList[max_step2];
			enemyLoader.gameObject.SetActive(value: true);
			if (!skip)
			{
				string animStateName = "Base Layer.GACHA_11";
				if (max_step2 == enemyLoaderList.Length - 1)
				{
					animStateName = "Base Layer.GACHA_SINGLE";
				}
				PlayEnemyAnimation(enemyLoader, animStateName);
			}
			rarity_type = quest_datas[max_step2].rarity;
			int num6 = rarity_type.ToRarityExpressionID();
			if (rarity_type == RARITY_TYPE.SS)
			{
				num6 = 3;
			}
			end_effects[max_step2] = PlayEffect(magicCircles[max_step2], endEffectPrefabs[num6]);
			Play("LoopEnemyAppear");
			bool is_short = max_step2 < reamMax;
			PlayAppearAudio(rarity_type, is_short);
			if (enemy_datas.Length > max_step2)
			{
				PlayEnemyAudio(enemy_datas[max_step2], is_short);
			}
			max_step2++;
			if (max_step2 >= reamMax)
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
		yield return new WaitForSeconds(lastShowRarityWaitTime);
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
			int num7 = enemyLoaderList.Length - 1;
			if (num7 >= 0)
			{
				PlayEnemyAnimation(enemyLoaderList[num7], "Base Layer.IDLE");
			}
			Time.timeScale = 1f;
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
		yield return MonoBehaviourSingleton<TransitionManager>.I.Out();
		Time.timeScale = 100f;
	}
}
