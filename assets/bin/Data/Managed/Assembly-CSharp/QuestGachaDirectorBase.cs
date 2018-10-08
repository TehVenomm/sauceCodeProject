using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGachaDirectorBase : AnimationDirector
{
	public interface ISectionCommand
	{
		void OnShowRarity(RARITY_TYPE rarity);

		void OnHideRarity();

		void OnEnd();

		void ActivateSkipButton();
	}

	public enum AUDIO
	{
		RARITY_EXPOSITION = 40000165,
		RARITY_LOW = 40000164,
		RARITY_MID = 40000126,
		RARITY_HIGH_0 = 40000127,
		OPENING_01 = 40000128,
		OPENING_02 = 40000129,
		OPENING_03 = 40000130,
		OPENING_04 = 40000131,
		DOOR_01 = 40000132,
		DOOR_02 = 40000133,
		MAGI_INTRO_01 = 40000134,
		MAGI_INTRO_02 = 40000135,
		MAGICAL_01 = 40000136,
		MAGICAL_02 = 40000137,
		METEOR_01 = 40000138,
		METEOR_02 = 40000139,
		APEAR_LONG = 40000140,
		APEAR_SHORT = 40000141
	}

	public const float DEFAULT_MODEL_LOAD_SCALE = 1f;

	public Animator stageAnimator;

	public Transform npcPosition;

	public GameObject npcEffect;

	public GameObject[] startEffectPrefabs;

	public GameObject[] meteorEffectPrefabs;

	public GameObject[] magicEffectPrefabs;

	public GameObject[] endEffectPrefabs;

	public GameObject[] demoEffectPrefabs;

	public GameObject[] uiRarityEffectPrefabs;

	protected ISectionCommand sectionCommandReceiver;

	protected IEnumerator coroutine;

	protected float time;

	protected int targetRarity;

	protected List<Transform> effects = new List<Transform>();

	protected List<Component> objects = new List<Component>();

	protected Transform magicEffect;

	private IEnumerator m_skipCoroutine;

	private bool m_isFirstSkipped;

	private bool m_isSkipAppearEnemy;

	protected bool IsSkipAppearEnemy => m_isSkipAppearEnemy;

	private void Start()
	{
		Init();
	}

	protected void Init()
	{
		skip = false;
		m_isFirstSkipped = false;
		m_isSkipAppearEnemy = false;
		stageAnimator.Play("StageAnim_Init");
		Play("MainAnim_Init", null, 0f);
		time = 0f;
		Delete();
	}

	protected void Delete()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		int i = 0;
		for (int count = objects.Count; i < count; i++)
		{
			if (objects[i] != null)
			{
				Object.Destroy(objects[i].get_gameObject());
				objects[i] = null;
			}
		}
		objects.Clear();
		effects.ForEach(delegate(Transform o)
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			if (o != null)
			{
				EffectManager.ReleaseEffect(o.get_gameObject(), true, false);
			}
		});
		effects.Clear();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		Delete();
	}

	public void StartDirection(ISectionCommand command_receiver)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		sectionCommandReceiver = command_receiver;
		if (coroutine != null)
		{
			this.StopCoroutine(coroutine);
		}
		this.StartCoroutine(coroutine = GetDirectionCoroutine());
	}

	protected virtual IEnumerator GetDirectionCoroutine()
	{
		return null;
	}

	protected NPCLoader LoadNPC()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		Transform val = Utility.CreateGameObject("NPC", npcPosition, -1);
		NPCLoader nPCLoader = val.get_gameObject().AddComponent<NPCLoader>();
		nPCLoader.Load(Singleton<NPCTable>.I.GetNPCData(2).npcModelID, 0, false, true, SHADER_TYPE.NORMAL, null);
		objects.Add(nPCLoader);
		return nPCLoader;
	}

	protected void CreateNPCEffect(Transform parent)
	{
		Transform val = Utility.Find(parent, "R_Finger02b");
		if (val != null)
		{
			PlayEffect(val, npcEffect);
		}
	}

	protected EnemyLoader LoadEnemy(Transform parent, int model_id, int anim_id, float displayScale, string base_effect, string base_effect_node)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		GameObject val = Utility.CreateGameObject("Enemy", parent, -1).get_gameObject();
		EnemyLoader enemyLoader = val.AddComponent<EnemyLoader>();
		enemyLoader.StartLoad(model_id, anim_id, 1f, base_effect, base_effect_node, false, true, false, SHADER_TYPE.NORMAL, -1, null, false, false, null);
		enemyLoader.DisplayGachaScale = displayScale;
		objects.Add(enemyLoader);
		return enemyLoader;
	}

	protected bool Step(float sec)
	{
		time += Time.get_deltaTime();
		return time < sec;
	}

	protected void PlayMeteorEffect(int rarity)
	{
		if (rarity > 3)
		{
			rarity = 3;
		}
		PlayEffect(meteorEffectPrefabs[rarity - 1]);
	}

	protected void PlayMagicEffect(int rarity, bool rankup)
	{
		if (rarity > 3)
		{
			rarity = 3;
		}
		if (rankup)
		{
			DeleteMagicEffect();
			magicEffect = PlayEffect(magicEffectPrefabs[rarity - 1]);
		}
		Play("MainAnim_MagicAngle0" + rarity, null, 0f);
	}

	protected void DeleteMagicEffect()
	{
		if (magicEffect != null)
		{
			EffectManager.ReleaseEffect(ref magicEffect);
		}
	}

	protected void PlayEndEffect(int rarity)
	{
		if (rarity > 4)
		{
			rarity = 4;
		}
		PlayEffect(endEffectPrefabs[rarity - 1]);
	}

	protected Transform PlayEffect(GameObject prefab)
	{
		return PlayEffect(MonoBehaviourSingleton<StageManager>.I._transform, prefab);
	}

	protected Transform PlayEffect(Transform parent, GameObject prefab)
	{
		Transform val = ResourceUtility.Realizes(prefab, parent, -1);
		effects.Add(val);
		rymFX component = val.GetComponent<rymFX>();
		if (component != null)
		{
			component.Cameras = (Camera[])new Camera[1]
			{
				MonoBehaviourSingleton<AppMain>.I.mainCamera
			};
		}
		return val;
	}

	protected bool UpdateDisplayRarity(ref int rarity)
	{
		if (rarity < targetRarity)
		{
			rarity++;
			return true;
		}
		return false;
	}

	public override void Skip()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		if (!skip)
		{
			if (m_isFirstSkipped)
			{
				m_isSkipAppearEnemy = true;
			}
			else
			{
				ActivateFirstSkipFlag();
				base.Skip();
				if (m_skipCoroutine != null)
				{
					this.StopCoroutine(m_skipCoroutine);
				}
				m_skipCoroutine = DoSkip();
				this.StartCoroutine(m_skipCoroutine);
			}
		}
	}

	private IEnumerator DoSkip()
	{
		yield return (object)MonoBehaviourSingleton<TransitionManager>.I.Out(TransitionManager.TYPE.BLACK);
		Time.set_timeScale(100f);
	}

	protected void ActivateFirstSkipFlag()
	{
		m_isFirstSkipped = true;
	}

	protected void ResetSkipAppearEnemyFlag()
	{
		m_isSkipAppearEnemy = false;
	}

	public void PlayUIRarityEffect(RARITY_TYPE rarity, Transform effect_parent_ui, Transform effect_target_ui)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		PlayRarityAudio(rarity, false);
		GameObject val = null;
		int num = rarity.ToRarityExpressionID2();
		if (num > 0)
		{
			val = uiRarityEffectPrefabs[num - 1];
		}
		if (!(val == null))
		{
			UIWidget componentInChildren = effect_target_ui.GetComponentInChildren<UIWidget>();
			Transform val2 = ResourceUtility.Realizes(val, effect_parent_ui, 5);
			val2.set_position(effect_parent_ui.get_position());
			EffectManager.SetUIEffectDepth(val2, effect_parent_ui, -0.001f, 10, componentInChildren);
			effects.Add(val2);
		}
	}

	protected void CacheAudio(LoadingQueue lo_queue)
	{
		int[] array = (int[])Enum.GetValues(typeof(AUDIO));
		int[] array2 = array;
		foreach (int se_id in array2)
		{
			lo_queue.CacheSE(se_id, null);
		}
	}

	protected void PlayAudio(AUDIO audio)
	{
		PlayAudio(audio, false);
	}

	protected void PlayAudio(AUDIO audio, bool ignore_skip)
	{
		if (!skip || ignore_skip)
		{
			SoundManager.PlayOneShotUISE((int)audio);
		}
	}

	protected void PlayMagicAudio(int display_rarity)
	{
		if (display_rarity > 2)
		{
			PlayAudio(AUDIO.MAGICAL_02);
		}
		else
		{
			PlayAudio(AUDIO.MAGICAL_01);
		}
	}

	protected void PlayAppearAudio(RARITY_TYPE rarity_type, bool is_short)
	{
		AUDIO audio = (!is_short) ? AUDIO.APEAR_LONG : AUDIO.APEAR_SHORT;
		PlayAudio(audio);
	}

	public void PlayRarityAudio(RARITY_TYPE rarity_type, bool ignore_skip_check = false)
	{
		AUDIO audio = AUDIO.RARITY_LOW;
		switch (rarity_type)
		{
		case RARITY_TYPE.A:
			audio = AUDIO.RARITY_MID;
			break;
		case RARITY_TYPE.S:
		case RARITY_TYPE.SS:
		case RARITY_TYPE.SSS:
			audio = AUDIO.RARITY_HIGH_0;
			break;
		}
		PlayAudio(audio, ignore_skip_check);
	}

	protected void CacheEnemyAudio(EnemyTable.EnemyData enemyData, LoadingQueue lo_queue)
	{
		if (lo_queue != null)
		{
			OutGameSettingsManager.EnemyDisplayInfo enemyDisplayInfo = MonoBehaviourSingleton<OutGameSettingsManager>.I.SearchEnemyDisplayInfoForGacha(enemyData);
			if (enemyDisplayInfo != null)
			{
				if (enemyDisplayInfo.seIdGachaShort > 0)
				{
					lo_queue.CacheSE(enemyDisplayInfo.seIdGachaShort, null);
				}
				if (enemyDisplayInfo.seIdGachaLong > 0)
				{
					lo_queue.CacheSE(enemyDisplayInfo.seIdGachaLong, null);
				}
			}
		}
	}

	protected void PlayEnemyAudio(EnemyTable.EnemyData enemyData, bool is_short = false)
	{
		if (!skip)
		{
			OutGameSettingsManager.EnemyDisplayInfo enemyDisplayInfo = MonoBehaviourSingleton<OutGameSettingsManager>.I.SearchEnemyDisplayInfoForGacha(enemyData);
			if (enemyDisplayInfo != null)
			{
				int num = (!is_short) ? enemyDisplayInfo.seIdGachaLong : enemyDisplayInfo.seIdGachaShort;
				if (num > 0)
				{
					SoundManager.PlayOneshotJingle(num, null, null);
				}
			}
		}
	}

	protected void PlayEnemyAnimation(EnemyLoader enemyLoader, string animStateName)
	{
		Animator val = null;
		if (enemyLoader != null)
		{
			val = enemyLoader.GetAnimator();
		}
		if (val != null)
		{
			int num = Animator.StringToHash(animStateName);
			if (val.HasState(0, num))
			{
				val.set_enabled(true);
				val.Play(num, 0, 0f);
				val.Update(0f);
			}
		}
	}

	public override void __FUNCTION__PlayCachedAudio(int se_id)
	{
		if (!skip)
		{
			base.__FUNCTION__PlayCachedAudio(se_id);
		}
	}

	protected void CheckAndReplaceShader(EnemyLoader enemyLoader)
	{
		Shader val = null;
		if (enemyLoader.bodyID == 2023)
		{
			val = ResourceUtility.FindShader("mobile/Custom/enemy_reflective_simple");
		}
		if (val != null)
		{
			enemyLoader.body.GetComponentsInChildren<Renderer>(Temporary.rendererList);
			for (int i = 0; i < Temporary.rendererList.Count; i++)
			{
				Renderer val2 = Temporary.rendererList[i];
				if (val2 is MeshRenderer || val2 is SkinnedMeshRenderer)
				{
					for (int j = 0; j < val2.get_materials().Length; j++)
					{
						val2.get_materials()[j].set_shader(val);
					}
				}
			}
		}
	}
}
