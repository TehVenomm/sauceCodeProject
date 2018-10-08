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
		int i = 0;
		for (int count = objects.Count; i < count; i++)
		{
			if ((UnityEngine.Object)objects[i] != (UnityEngine.Object)null)
			{
				UnityEngine.Object.Destroy(objects[i].gameObject);
				objects[i] = null;
			}
		}
		objects.Clear();
		effects.ForEach(delegate(Transform o)
		{
			if ((UnityEngine.Object)o != (UnityEngine.Object)null)
			{
				EffectManager.ReleaseEffect(o.gameObject, true, false);
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
		sectionCommandReceiver = command_receiver;
		if (coroutine != null)
		{
			StopCoroutine(coroutine);
		}
		StartCoroutine(coroutine = GetDirectionCoroutine());
	}

	protected virtual IEnumerator GetDirectionCoroutine()
	{
		return null;
	}

	protected NPCLoader LoadNPC()
	{
		Transform transform = Utility.CreateGameObject("NPC", npcPosition, -1);
		NPCLoader nPCLoader = transform.gameObject.AddComponent<NPCLoader>();
		nPCLoader.Load(Singleton<NPCTable>.I.GetNPCData(2).npcModelID, 0, false, true, SHADER_TYPE.NORMAL, null);
		objects.Add(nPCLoader);
		return nPCLoader;
	}

	protected void CreateNPCEffect(Transform parent)
	{
		Transform transform = Utility.Find(parent, "R_Finger02b");
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			PlayEffect(transform, npcEffect);
		}
	}

	protected EnemyLoader LoadEnemy(Transform parent, int model_id, int anim_id, float displayScale, string base_effect, string base_effect_node)
	{
		GameObject gameObject = Utility.CreateGameObject("Enemy", parent, -1).gameObject;
		EnemyLoader enemyLoader = gameObject.AddComponent<EnemyLoader>();
		enemyLoader.StartLoad(model_id, anim_id, 1f, base_effect, base_effect_node, false, true, false, SHADER_TYPE.NORMAL, -1, null, false, false, null);
		enemyLoader.DisplayGachaScale = displayScale;
		objects.Add(enemyLoader);
		return enemyLoader;
	}

	protected bool Step(float sec)
	{
		time += Time.deltaTime;
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
		if ((UnityEngine.Object)magicEffect != (UnityEngine.Object)null)
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
		Transform transform = ResourceUtility.Realizes(prefab, parent, -1);
		effects.Add(transform);
		rymFX component = transform.GetComponent<rymFX>();
		if ((UnityEngine.Object)component != (UnityEngine.Object)null)
		{
			component.Cameras = new Camera[1]
			{
				MonoBehaviourSingleton<AppMain>.I.mainCamera
			};
		}
		return transform;
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
					StopCoroutine(m_skipCoroutine);
				}
				m_skipCoroutine = DoSkip();
				StartCoroutine(m_skipCoroutine);
			}
		}
	}

	private IEnumerator DoSkip()
	{
		yield return (object)MonoBehaviourSingleton<TransitionManager>.I.Out(TransitionManager.TYPE.BLACK);
		Time.timeScale = 100f;
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
		PlayRarityAudio(rarity, false);
		GameObject gameObject = null;
		int num = rarity.ToRarityExpressionID2();
		if (num > 0)
		{
			gameObject = uiRarityEffectPrefabs[num - 1];
		}
		if (!((UnityEngine.Object)gameObject == (UnityEngine.Object)null))
		{
			UIWidget componentInChildren = effect_target_ui.GetComponentInChildren<UIWidget>();
			Transform transform = ResourceUtility.Realizes(gameObject, effect_parent_ui, 5);
			transform.position = effect_parent_ui.position;
			EffectManager.SetUIEffectDepth(transform, effect_parent_ui, -0.001f, 10, componentInChildren);
			effects.Add(transform);
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
		Animator animator = null;
		if ((UnityEngine.Object)enemyLoader != (UnityEngine.Object)null)
		{
			animator = enemyLoader.GetAnimator();
		}
		if ((UnityEngine.Object)animator != (UnityEngine.Object)null)
		{
			int num = Animator.StringToHash(animStateName);
			if (animator.HasState(0, num))
			{
				animator.enabled = true;
				animator.Play(num, 0, 0f);
				animator.Update(0f);
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
		Shader shader = null;
		if (enemyLoader.bodyID == 2023)
		{
			shader = ResourceUtility.FindShader("mobile/Custom/Enemy/enemy_reflective_simple");
		}
		if ((UnityEngine.Object)shader != (UnityEngine.Object)null)
		{
			enemyLoader.body.GetComponentsInChildren(Temporary.rendererList);
			for (int i = 0; i < Temporary.rendererList.Count; i++)
			{
				Renderer renderer = Temporary.rendererList[i];
				if (renderer is MeshRenderer || renderer is SkinnedMeshRenderer)
				{
					for (int j = 0; j < renderer.materials.Length; j++)
					{
						renderer.materials[j].shader = shader;
					}
				}
			}
		}
	}
}
