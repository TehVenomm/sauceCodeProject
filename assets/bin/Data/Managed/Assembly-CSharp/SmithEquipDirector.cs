using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmithEquipDirector : AnimationDirector
{
	public enum AUDIO
	{
		CREATE_START = 40000044,
		CREATE_ONEND = 40000045,
		GROW_HIT_01 = 40000046,
		GROW_HIT_02 = 40000047,
		GROW_HIT_03 = 40000048,
		GROW_ONEND = 40000051,
		EVOLVE_START = 40000055,
		EXCEED_START = 40000050,
		EXCEED_ONEND = 40000051,
		ABILITY_CHANGE_01 = 40000058,
		ABILITY_CHANGE_02 = 40000059,
		ABILITY_CHANGE_03 = 40000060,
		ABILITY_CHANGE_ONEND = 40000045
	}

	[SerializeField]
	private Transform npcParent;

	[SerializeField]
	private Transform npc003Parent;

	[SerializeField]
	private Transform effectParent;

	[SerializeField]
	private Animator cameraAnimator;

	[SerializeField]
	private GameObject[] createEffectPrefabs;

	[SerializeField]
	private GameObject[] growEffectPrefabs;

	[SerializeField]
	private GameObject[] evolveEffectPrefabs;

	[SerializeField]
	private GameObject[] exceedEffectPrefabs;

	[SerializeField]
	private GameObject[] abilityChangeEffectPrefabs;

	private GameObject[] effects;

	private List<Animator> animators = new List<Animator>();

	private Transform[] npc004TransformList;

	private Animator npc004Animator;

	private Transform[] npc003TransformList;

	private Animator npc003Animator;

	private Transform effectBindTarget;

	private bool updateFOV;

	private float time;

	private const string STATE_NAME_CREATE = "CREATE";

	private const string STATE_NAME_GROW = "GROW";

	private const string STATE_NAME_EVOLVE = "EVOLVE";

	private const string STATE_NAME_EXCEED = "EXCEED";

	private const string STATE_NAME_ABILITY_CHANGE = "ABILITY_CHANGE";

	private void Start()
	{
	}

	public void SetNPC003(GameObject npcObject)
	{
		if (!(npcObject == null))
		{
			Utility.Attach(npc003Parent, npcObject.transform);
			Utility.SetLayerWithChildren(npcObject.transform, npc003Parent.gameObject.layer);
			npc003Animator = npcObject.GetComponentInChildren<Animator>();
			npc003Animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
			npc003TransformList = npcObject.GetComponentsInChildren<Transform>();
		}
	}

	public void SetNPC004(GameObject npcObject)
	{
		Utility.Attach(npcParent, npcObject.transform);
		Utility.SetLayerWithChildren(npcObject.transform, npcParent.gameObject.layer);
		npc004Animator = npcObject.GetComponentInChildren<Animator>();
		npc004Animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		npc004TransformList = npcObject.GetComponentsInChildren<Transform>();
	}

	private Transform SearchNpc004Transform(string targetName)
	{
		if (npc004TransformList == null)
		{
			return null;
		}
		Transform[] array = npc004TransformList;
		foreach (Transform transform in array)
		{
			if (transform.name == targetName)
			{
				return transform;
			}
		}
		return null;
	}

	private Transform SearchNpc003Transform(string targetName)
	{
		if (npc003TransformList == null)
		{
			return null;
		}
		Transform[] array = npc003TransformList;
		foreach (Transform transform in array)
		{
			if (transform.name == targetName)
			{
				return transform;
			}
		}
		return null;
	}

	public void StartCreate(Action onEnd)
	{
		updateFOV = true;
		SetEffects(createEffectPrefabs);
		PlayDirection("CREATE", onEnd);
		PlayNPCAnimation("CREATE");
		PlayCameraAnimation("CREATE");
		PlayEffect("ef_studio_create_01");
	}

	public void StartGrow(Action onEnd)
	{
		updateFOV = true;
		_ = $"{GetNpcRoot()}Tools/Hammer_L";
		effectBindTarget = Utility.FindChild(npcParent, "NPC004_003/");
		SetEffects(growEffectPrefabs);
		PlayDirection("GROW", onEnd);
		PlayNPCAnimation("GROW");
		PlayCameraAnimation("GROW");
	}

	public void StartEvolve(Action onEnd)
	{
		updateFOV = false;
		string name = $"{GetNpcRoot()}Hip/Spine00/Spine01/L_Shoulder/L_Upperarm/L_Forearm/L_Hand/L_Wep";
		effectBindTarget = Utility.FindChild(npcParent, name);
		SetEffects(evolveEffectPrefabs);
		PlayDirection("EVOLVE", onEnd);
		PlayNPCAnimation("EVOLVE");
		PlayCameraAnimation("EVOLVE");
		time = 0f;
		StartCoroutine(DoEvolveEffect());
	}

	public void StartExceed(Action onEnd)
	{
		updateFOV = false;
		string name = $"{GetNpcRoot()}Tools/Hammer_L";
		effectBindTarget = Utility.FindChild(npcParent, name);
		SetEffects(exceedEffectPrefabs);
		PlayDirection("EXCEED", onEnd);
		PlayNPCAnimation("EXCEED");
		PlayCameraAnimation("EXCEED");
		if (effectBindTarget != null)
		{
			PlayBindEffect(0, effectBindTarget, new Vector3(0f, 0f, 0.522f));
		}
	}

	public string GetNpcRoot()
	{
		int num = (!StatusManager.IsUnique()) ? 1 : 3;
		return $"NPC004_{num:D3}/NPC004_Origin/Move/Root/";
	}

	public void StartAbilityChange(Action onEnd)
	{
		updateFOV = true;
		SetEffects(abilityChangeEffectPrefabs);
		PlayDirection("ABILITY_CHANGE", onEnd);
		PlayNPCAnimation("ABILITY_CHANGE");
		PlayCameraAnimation("ABILITY_CHANGE");
	}

	private void PlayDirection(string name, Action onEnd)
	{
		if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
		{
			MonoBehaviourSingleton<StatusStageManager>.I.SetEnableSmithCharacterActivate(active: false);
		}
		if (MonoBehaviourSingleton<AppMain>.IsValid())
		{
			SetLinkCamera(is_link: true);
		}
		Play(name, delegate
		{
			ResetPlayRate();
			SoundManager.StopSEAll();
			PlayAudioOnEnd(name);
			onEnd();
		});
	}

	private void PlayAudioOnEnd(string name)
	{
		if (!(name == "CREATE"))
		{
			if (name == "GROW")
			{
				return;
			}
			if (!(name == "EVOLVE"))
			{
				if (!(name == "EXCEED"))
				{
					_ = (name == "ABILITY_CHANGE");
				}
				else
				{
					PlayAudio(AUDIO.GROW_ONEND);
				}
			}
			else
			{
				PlayAudio(AUDIO.GROW_ONEND);
			}
		}
		else
		{
			PlayAudio(AUDIO.CREATE_ONEND);
		}
	}

	private void SetEffects(GameObject[] prefabs)
	{
		if (prefabs == null || prefabs.Length == 0)
		{
			return;
		}
		Camera camera = MonoBehaviourSingleton<AppMain>.IsValid() ? MonoBehaviourSingleton<AppMain>.I.mainCamera : useCamera;
		List<Animator> list = new List<Animator>();
		effects = new GameObject[prefabs.Length];
		for (int i = 0; i < prefabs.Length; i++)
		{
			GameObject gameObject = ResourceUtility.Realizes(prefabs[i], effectParent, effectParent.gameObject.layer).gameObject;
			rymFX component = gameObject.GetComponent<rymFX>();
			if (component != null)
			{
				component.Cameras = new Camera[1]
				{
					camera
				};
				gameObject.GetComponentsInChildren(list);
				animators.AddRange(list);
			}
			gameObject.SetActive(value: false);
			effects[i] = gameObject;
		}
	}

	public override void Reset()
	{
		if (AppMain.isApplicationQuit)
		{
			return;
		}
		skip = false;
		if (effects != null)
		{
			for (int i = 0; i < effects.Length; i++)
			{
				GameObject gameObject = effects[i];
				if ((bool)gameObject)
				{
					UnityEngine.Object.Destroy(gameObject);
				}
			}
		}
		animators.Clear();
		base.Reset();
	}

	protected override void Update()
	{
		time += Time.deltaTime;
		if (skip)
		{
			time = 10000f;
		}
		if (npc004Animator != null)
		{
			npc004Animator.speed = (skip ? 10000f : 1f);
		}
		if (npc003Animator != null)
		{
			npc003Animator.speed = (skip ? 10000f : 1f);
		}
		if ((bool)cameraAnimator)
		{
			cameraAnimator.speed = (skip ? 10000f : 1f);
		}
		base.Update();
	}

	protected override void LateUpdate()
	{
		if (updateFOV && useCamera != null)
		{
			float x = cameraAnimator.transform.localScale.x;
			if (x > 0f)
			{
				useCamera.fieldOfView = Utility.HorizontalToVerticalFOV(x);
			}
		}
		base.LateUpdate();
	}

	public override void Skip()
	{
		if (skip)
		{
			return;
		}
		for (int i = 0; i < effects.Length; i++)
		{
			GameObject gameObject = effects[i];
			if (!(gameObject != null))
			{
				continue;
			}
			rymFX component = gameObject.GetComponent<rymFX>();
			if (component != null)
			{
				float num = component.GetEndFrame() - component.GetCurFrame();
				if (num > 0f)
				{
					float delta_time = num / 30f;
					component.UpdateFx(delta_time, null, component.IsLoop());
				}
			}
		}
		if (animators != null && animators.Count > 0)
		{
			for (int j = 0; j < animators.Count; j++)
			{
				animators[j].speed = 10000f;
			}
		}
		SoundManager.StopSEAll();
		base.Skip();
	}

	private void ResetPlayRate()
	{
		if (animators != null && animators.Count > 0)
		{
			for (int i = 0; i < animators.Count; i++)
			{
				animators[i].speed = 1f;
			}
		}
	}

	private void PlayNPCAnimation(string stateName)
	{
		npc004Animator.Play(stateName, 0, 0f);
		npc004Animator.Update(0f);
		if (npc003Animator != null)
		{
			npc003Animator.Play(stateName, 0, 0f);
			npc003Animator.Update(0f);
		}
		if (!(stateName == "CREATE"))
		{
			if (stateName == "GROW")
			{
				return;
			}
			if (!(stateName == "EVOLVE"))
			{
				if (!(stateName == "EXCEED"))
				{
					_ = (stateName == "ABILITY_CHANGE");
				}
				else
				{
					PlayAudio(AUDIO.EXCEED_START);
				}
			}
			else
			{
				PlayAudio(AUDIO.EVOLVE_START);
			}
		}
		else
		{
			PlayAudio(AUDIO.CREATE_START);
		}
	}

	private void PlayCameraAnimation(string stateName)
	{
		cameraAnimator.Play(stateName, 0, 0f);
		cameraAnimator.Update(0f);
	}

	private void PlayEffect(string name)
	{
		GameObject gameObject = FindEffect(name);
		if ((bool)gameObject)
		{
			gameObject.SetActive(value: true);
		}
	}

	private void PlayEffectWithParent(string name)
	{
		string[] array = name.Split('@');
		if (array.Length > 1)
		{
			GameObject gameObject = FindEffect(array[0]);
			if (!(gameObject == null))
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject);
				Transform transform = SearchNpc004Transform(array[1]);
				if (gameObject2 != null && transform != null)
				{
					Transform transform2 = gameObject2.transform;
					transform2.parent = transform;
					transform2.localPosition = Vector3.zero;
					transform2.localScale = Vector3.one;
					transform2.localRotation = Quaternion.identity;
					gameObject2.SetActive(value: true);
				}
			}
		}
		else
		{
			PlayEffect(name);
		}
	}

	private void PlayEffectSilvy(string name)
	{
		string[] array = name.Split('@');
		if (array.Length > 1)
		{
			GameObject gameObject = FindEffect(array[0]);
			if (!(gameObject == null))
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject);
				Transform transform = SearchNpc003Transform(array[1]);
				if (gameObject2 != null && transform != null)
				{
					Transform transform2 = gameObject2.transform;
					transform2.parent = transform;
					transform2.localPosition = Vector3.zero;
					transform2.localScale = Vector3.one;
					transform2.localRotation = Quaternion.identity;
					gameObject2.SetActive(value: true);
				}
			}
		}
		else
		{
			PlayEffect(name);
		}
	}

	private void PlayBindEffect(int effectIndex, Transform bindTarget, Vector3 offset)
	{
		GameObject obj = effects[effectIndex];
		obj.transform.parent = bindTarget;
		obj.transform.localPosition = offset;
		obj.SetActive(value: true);
	}

	private GameObject FindEffect(string name)
	{
		for (int i = 0; i < effects.Length; i++)
		{
			GameObject gameObject = effects[i];
			if (gameObject.name == name)
			{
				return gameObject;
			}
		}
		return null;
	}

	private IEnumerator DoEvolveEffect()
	{
		if ((bool)effectBindTarget)
		{
			PlayBindEffect(1, effectBindTarget, new Vector3(-0.022f, -0.041f, 0.04f));
		}
		GameObject e = FindEffect("ef_studio_evolve_01");
		for (float time = 0.3f; time > 0f; time -= Time.deltaTime)
		{
			yield return null;
		}
		if ((bool)e)
		{
			e.SetActive(value: true);
			e.transform.localPosition = new Vector3(2.846f, 0.968f, 2.001f);
		}
	}

	protected void PlayAudio(AUDIO audio)
	{
		if (!skip)
		{
			SoundManager.PlayOneShotUISE((int)audio);
		}
	}
}
