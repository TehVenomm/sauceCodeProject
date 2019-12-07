using Network;
using System.Collections;
using UnityEngine;

public class QuestFeverGachaLegacyDirector : QuestGachaDirectorBase
{
	public const string ENEMY_MOTION_BASE_LAYER = "Base Layer.";

	public const string ENEMY_MOTION_STATE_IDLE = "Base Layer.IDLE";

	public const string ENEMY_MOTION_STATE_GACHA = "Base Layer.GACHA_SINGLE";

	public const string ENEMY_MOTION_STATE_GACHA11 = "Base Layer.GACHA_11";

	public const float MAGIC_CIRCLE_EFFECT_FINISH_TIME = 13.5f;

	public const int DEFAULT_REAM_MAX = 11;

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
		int reamMax = gachaResultBonus?.reward.Count ?? 9;
		int[] idxList = new int[reamMax];
		for (int n = 0; n < reamMax - 1; n++)
		{
			idxList[n] = n;
		}
		idxList[reamMax - 1] = 10;
		EnemyTable.EnemyData[] enemy_datas = new EnemyTable.EnemyData[reamMax];
		QuestTable.QuestTableData[] quest_datas = new QuestTable.QuestTableData[reamMax];
		EnemyLoader[] enemyLoaderList = new EnemyLoader[reamMax];
		if (MonoBehaviourSingleton<GachaManager>.IsValid() && gachaResultBonus != null)
		{
			int count = gachaResultBonus.reward.Count;
			int num = 0;
			for (int num2 = count; num < num2; num++)
			{
				GachaResult.GachaReward gachaReward = gachaResultBonus.reward[num];
				uint itemId = (uint)gachaReward.itemId;
				quest_datas[num] = Singleton<QuestTable>.I.GetQuestData(itemId);
				if (quest_datas[num] != null)
				{
					enemy_datas[num] = Singleton<EnemyTable>.I.GetEnemyData((uint)quest_datas[num].GetMainEnemyID());
					if (enemy_datas[num] == null)
					{
						Log.Error("EnemyTable[{0}] == null", quest_datas[num].GetMainEnemyID());
					}
				}
				else
				{
					Log.Error("QuestTable[{0}] == null", gachaReward.itemId);
					quest_datas[num] = new QuestTable.QuestTableData();
					enemy_datas[num] = new EnemyTable.EnemyData();
				}
			}
		}
		NPCLoader npc_loader = LoadNPC();
		npc_loader.Load(Singleton<NPCTable>.I.GetNPCData(2).npcModelID, 0, need_shadow: false, enable_light_probes: true, SHADER_TYPE.NORMAL, null);
		int num3 = 0;
		for (int num4 = reamMax; num3 < num4; num3++)
		{
			float displayScale = enemy_datas[num3].modelScale;
			int anim_id = enemy_datas[num3].animId;
			int modelId = enemy_datas[num3].modelId;
			OutGameSettingsManager.EnemyDisplayInfo enemyDisplayInfo = MonoBehaviourSingleton<OutGameSettingsManager>.I.SearchEnemyDisplayInfoForGacha(enemy_datas[num3]);
			if (enemyDisplayInfo != null)
			{
				anim_id = enemyDisplayInfo.animID;
				displayScale = enemyDisplayInfo.gachaScale;
			}
			enemyLoaderList[num3] = LoadEnemy(enemyPositions[idxList[num3]], modelId, anim_id, displayScale, enemy_datas[num3].baseEffectName, enemy_datas[num3].baseEffectNode);
		}
		int m = 0;
		int l = reamMax;
		while (m < l)
		{
			while (enemyLoaderList[m].isLoading)
			{
				yield return null;
			}
			enemyLoaderList[m].ApplyGachaDisplayScaleToParentNode();
			CheckAndReplaceShader(enemyLoaderList[m]);
			enemyLoaderList[m].gameObject.SetActive(value: false);
			int num5 = m + 1;
			m = num5;
		}
		LoadingQueue lo_queue = new LoadingQueue(this);
		CacheAudio(lo_queue);
		for (int num6 = 0; num6 < reamMax; num6++)
		{
			CacheEnemyAudio(enemy_datas[num6], lo_queue);
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
		l = 0;
		m = 0;
		time -= Time.deltaTime;
		while (l < reamMax || m < reamMax)
		{
			time += Time.deltaTime;
			if (l < reamMax && meteorTimings[idxList[l]] <= time)
			{
				PlayEffect(magicCircles[idxList[l]], meteorEffectPrefabs[quest_datas[l].rarity.ToRarityExpressionID()]);
				if (l == reamMax - 1)
				{
					PlayAudio(AUDIO.METEOR_01);
				}
				l++;
			}
			if (m < reamMax && magicTimings[idxList[m]] <= time)
			{
				magic_effects[m] = PlayEffect(magicCircles[idxList[m]], magicEffectPrefabs[quest_datas[m].rarity.ToRarityExpressionID()]);
				PlayMagicAudio((int)quest_datas[m].rarity);
				m++;
			}
			yield return null;
		}
		int num7 = m - 1;
		if (num7 < reamMax && num7 >= 0)
		{
			PlayMagicAudio((int)quest_datas[num7].rarity);
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
		m = 0;
		float end_time2 = 13.5f;
		float end_step_time = 1.5f;
		float end_step_time_last = 2f;
		RARITY_TYPE rarity_type;
		while (true)
		{
			sectionCommandReceiver.OnHideRarity();
			PlayAudio(AUDIO.RARITY_EXPOSITION);
			if (m > 0)
			{
				int num8 = m - 1;
				if (enemyLoaderList[num8] != null)
				{
					enemyLoaderList[num8].gameObject.SetActive(value: false);
				}
				if (magic_effects[num8] != null)
				{
					Object.Destroy(magic_effects[num8].gameObject);
					magic_effects[num8] = null;
				}
				if (end_effects[num8] != null)
				{
					Object.Destroy(end_effects[num8].gameObject);
					end_effects[num8] = null;
				}
			}
			EnemyLoader enemyLoader = enemyLoaderList[m];
			enemyLoader.gameObject.SetActive(value: true);
			if (!skip)
			{
				string animStateName = "Base Layer.GACHA_11";
				if (m == enemyLoaderList.Length - 1)
				{
					animStateName = "Base Layer.GACHA_SINGLE";
				}
				PlayEnemyAnimation(enemyLoader, animStateName);
			}
			rarity_type = quest_datas[m].rarity;
			int num9 = rarity_type.ToRarityExpressionID();
			if (rarity_type == RARITY_TYPE.SS)
			{
				num9 = 3;
			}
			end_effects[m] = PlayEffect(magicCircles[idxList[m]], endEffectPrefabs[num9]);
			Play($"MainAnim_End_{idxList[m] + 1:D2}");
			m++;
			bool is_short = m < reamMax;
			PlayAppearAudio(rarity_type, is_short);
			if (enemy_datas.Length >= m)
			{
				PlayEnemyAudio(enemy_datas[m - 1], is_short);
			}
			if (m >= reamMax)
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
			int num10 = enemyLoaderList.Length - 1;
			if (num10 >= 0)
			{
				PlayEnemyAnimation(enemyLoaderList[num10], "Base Layer.IDLE");
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
