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

	private const string STATE_NAME_CREATE = "CREATE";

	private const string STATE_NAME_GROW = "GROW";

	private const string STATE_NAME_EVOLVE = "EVOLVE";

	private const string STATE_NAME_EXCEED = "EXCEED";

	private const string STATE_NAME_ABILITY_CHANGE = "ABILITY_CHANGE";

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

	private void Start()
	{
	}

	public void SetNPC003(GameObject npcObject)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		if (!(npcObject == null))
		{
			Utility.Attach(npc003Parent, npcObject.get_transform());
			Utility.SetLayerWithChildren(npcObject.get_transform(), npc003Parent.get_gameObject().get_layer());
			npc003Animator = npcObject.GetComponentInChildren<Animator>();
			npc003Animator.set_cullingMode(0);
			npc003TransformList = npcObject.GetComponentsInChildren<Transform>();
		}
	}

	public void SetNPC004(GameObject npcObject)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		Utility.Attach(npcParent, npcObject.get_transform());
		Utility.SetLayerWithChildren(npcObject.get_transform(), npcParent.get_gameObject().get_layer());
		npc004Animator = npcObject.GetComponentInChildren<Animator>();
		npc004Animator.set_cullingMode(0);
		npc004TransformList = npcObject.GetComponentsInChildren<Transform>();
	}

	private Transform SearchNpc004Transform(string targetName)
	{
		if (npc004TransformList == null)
		{
			return null;
		}
		Transform[] array = npc004TransformList;
		foreach (Transform val in array)
		{
			if (val.get_name() == targetName)
			{
				return val;
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
		foreach (Transform val in array)
		{
			if (val.get_name() == targetName)
			{
				return val;
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
		effectBindTarget = Utility.FindChild(npcParent, "NPC004_001/NPC004_Origin/Move/Root/Tools/Hammer_L");
		SetEffects(growEffectPrefabs);
		PlayDirection("GROW", onEnd);
		PlayNPCAnimation("GROW");
		PlayCameraAnimation("GROW");
	}

	public void StartEvolve(Action onEnd)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		updateFOV = false;
		effectBindTarget = Utility.FindChild(npcParent, "NPC004_001/NPC004_Origin/Move/Root/Hip/Spine00/Spine01/L_Shoulder/L_Upperarm/L_Forearm/L_Hand/L_Wep");
		SetEffects(evolveEffectPrefabs);
		PlayDirection("EVOLVE", onEnd);
		PlayNPCAnimation("EVOLVE");
		PlayCameraAnimation("EVOLVE");
		time = 0f;
		this.StartCoroutine(DoEvolveEffect());
	}

	public void StartExceed(Action onEnd)
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		updateFOV = false;
		effectBindTarget = Utility.FindChild(npcParent, "NPC004_001/NPC004_Origin/Move/Root/Tools/Hammer_L");
		SetEffects(exceedEffectPrefabs);
		PlayDirection("EXCEED", onEnd);
		PlayNPCAnimation("EXCEED");
		PlayCameraAnimation("EXCEED");
		if (effectBindTarget != null)
		{
			PlayBindEffect(0, effectBindTarget, new Vector3(0f, 0f, 0.522f));
		}
	}

	public void StartAbilityChange(Action onEnd)
	{
		updateFOV = true;
		SetEffects(abilityChangeEffectPrefabs);
		PlayDirection("ABILITY_CHANGE", onEnd);
		PlayNPCAnimation("ABILITY_CHANGE");
		PlayCameraAnimation("ABILITY_CHANGE");
	}

	private unsafe void PlayDirection(string name, Action onEnd)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
		{
			MonoBehaviourSingleton<StatusStageManager>.I.SetSmithCharacterActivateFlag(false);
		}
		if (MonoBehaviourSingleton<AppMain>.IsValid())
		{
			SetLinkCamera(true);
		}
		_003CPlayDirection_003Ec__AnonStorey47D _003CPlayDirection_003Ec__AnonStorey47D;
		Play(name, new Action((object)_003CPlayDirection_003Ec__AnonStorey47D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), 0f);
	}

	private void PlayAudioOnEnd(string name)
	{
		switch (name)
		{
		case "GROW":
			break;
		case "ABILITY_CHANGE":
			break;
		case "CREATE":
			PlayAudio(AUDIO.CREATE_ONEND);
			break;
		case "EVOLVE":
			PlayAudio(AUDIO.GROW_ONEND);
			break;
		case "EXCEED":
			PlayAudio(AUDIO.GROW_ONEND);
			break;
		}
	}

	private void SetEffects(GameObject[] prefabs)
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		if (prefabs != null && prefabs.Length > 0)
		{
			Camera val = (!MonoBehaviourSingleton<AppMain>.IsValid()) ? ((object)useCamera) : ((object)MonoBehaviourSingleton<AppMain>.I.mainCamera);
			List<Animator> list = new List<Animator>();
			effects = (GameObject[])new GameObject[prefabs.Length];
			for (int i = 0; i < prefabs.Length; i++)
			{
				GameObject val2 = ResourceUtility.Realizes(prefabs[i], effectParent, effectParent.get_gameObject().get_layer()).get_gameObject();
				rymFX component = val2.GetComponent<rymFX>();
				if (component != null)
				{
					component.Cameras = (Camera[])new Camera[1]
					{
						val
					};
					val2.GetComponentsInChildren<Animator>(list);
					animators.AddRange(list);
				}
				val2.SetActive(false);
				effects[i] = val2;
			}
		}
	}

	public override void Reset()
	{
		if (!AppMain.isApplicationQuit)
		{
			skip = false;
			if (effects != null)
			{
				for (int i = 0; i < effects.Length; i++)
				{
					GameObject val = effects[i];
					if (Object.op_Implicit(val))
					{
						Object.Destroy(val);
					}
				}
			}
			animators.Clear();
			base.Reset();
		}
	}

	protected override void Update()
	{
		time += Time.get_deltaTime();
		if (skip)
		{
			time = 10000f;
		}
		if (npc004Animator != null)
		{
			npc004Animator.set_speed((!skip) ? 1f : 10000f);
		}
		if (npc003Animator != null)
		{
			npc003Animator.set_speed((!skip) ? 1f : 10000f);
		}
		if (Object.op_Implicit(cameraAnimator))
		{
			cameraAnimator.set_speed((!skip) ? 1f : 10000f);
		}
		base.Update();
	}

	protected override void LateUpdate()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		if (updateFOV && useCamera != null)
		{
			Vector3 localScale = cameraAnimator.get_transform().get_localScale();
			float x = localScale.x;
			if (x > 0f)
			{
				useCamera.set_fieldOfView(Utility.HorizontalToVerticalFOV(x));
			}
		}
		base.LateUpdate();
	}

	public override void Skip()
	{
		if (!skip)
		{
			for (int i = 0; i < effects.Length; i++)
			{
				GameObject val = effects[i];
				if (val != null)
				{
					rymFX component = val.GetComponent<rymFX>();
					if (component != null)
					{
						float num = component.GetEndFrame() - component.GetCurFrame();
						if (num > 0f)
						{
							float num2 = num / 30f;
							component.UpdateFx(num2, null, component.IsLoop());
						}
					}
				}
			}
			if (animators != null && animators.Count > 0)
			{
				for (int j = 0; j < animators.Count; j++)
				{
					animators[j].set_speed(10000f);
				}
			}
			SoundManager.StopSEAll(0);
			base.Skip();
		}
	}

	private void ResetPlayRate()
	{
		if (animators != null && animators.Count > 0)
		{
			for (int i = 0; i < animators.Count; i++)
			{
				animators[i].set_speed(1f);
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
		switch (stateName)
		{
		case "GROW":
			break;
		case "ABILITY_CHANGE":
			break;
		case "CREATE":
			PlayAudio(AUDIO.CREATE_START);
			break;
		case "EVOLVE":
			PlayAudio(AUDIO.EVOLVE_START);
			break;
		case "EXCEED":
			PlayAudio(AUDIO.EXCEED_START);
			break;
		}
	}

	private void PlayCameraAnimation(string stateName)
	{
		cameraAnimator.Play(stateName, 0, 0f);
		cameraAnimator.Update(0f);
	}

	private void PlayEffect(string name)
	{
		GameObject val = FindEffect(name);
		if (Object.op_Implicit(val))
		{
			val.SetActive(true);
		}
	}

	private void PlayEffectWithParent(string name)
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		string[] array = name.Split('@');
		if (array.Length > 1)
		{
			GameObject val = FindEffect(array[0]);
			if (!(val == null))
			{
				GameObject val2 = Object.Instantiate<GameObject>(val);
				Transform val3 = SearchNpc004Transform(array[1]);
				if (val2 != null && val3 != null)
				{
					Transform val4 = val2.get_transform();
					val4.set_parent(val3);
					val4.set_localPosition(Vector3.get_zero());
					val4.set_localScale(Vector3.get_one());
					val4.set_localRotation(Quaternion.get_identity());
					val2.SetActive(true);
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
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		string[] array = name.Split('@');
		if (array.Length > 1)
		{
			GameObject val = FindEffect(array[0]);
			if (!(val == null))
			{
				GameObject val2 = Object.Instantiate<GameObject>(val);
				Transform val3 = SearchNpc003Transform(array[1]);
				if (val2 != null && val3 != null)
				{
					Transform val4 = val2.get_transform();
					val4.set_parent(val3);
					val4.set_localPosition(Vector3.get_zero());
					val4.set_localScale(Vector3.get_one());
					val4.set_localRotation(Quaternion.get_identity());
					val2.SetActive(true);
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
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		GameObject val = effects[effectIndex];
		val.get_transform().set_parent(bindTarget);
		val.get_transform().set_localPosition(offset);
		val.SetActive(true);
	}

	private GameObject FindEffect(string name)
	{
		for (int i = 0; i < effects.Length; i++)
		{
			GameObject val = effects[i];
			if (val.get_name() == name)
			{
				return val;
			}
		}
		return null;
	}

	private IEnumerator DoEvolveEffect()
	{
		if (Object.op_Implicit(effectBindTarget))
		{
			PlayBindEffect(1, effectBindTarget, new Vector3(-0.022f, -0.041f, 0.04f));
		}
		GameObject e = FindEffect("ef_studio_evolve_01");
		for (float time = 0.3f; time > 0f; time -= Time.get_deltaTime())
		{
			yield return (object)null;
		}
		if (Object.op_Implicit(e))
		{
			e.SetActive(true);
			e.get_transform().set_localPosition(new Vector3(2.846f, 0.968f, 2.001f));
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
