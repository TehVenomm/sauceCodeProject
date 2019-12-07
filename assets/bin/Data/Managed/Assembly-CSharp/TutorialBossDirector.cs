using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBossDirector : MonoBehaviour
{
	[Serializable]
	public class Logo
	{
		public Transform root;

		public Camera camera;

		public Renderer logo;

		public Renderer eye;

		public Renderer fader;

		public GameObject bg;

		public Renderer bgFader;

		public GameObject effect1;

		public GameObject dragonRoot;

		public Renderer dragonPlane;
	}

	private class PlaySoundParam
	{
		public delegate Vector3 GetPosFunc();

		public GetPosFunc func;

		public float time;

		public int id = -1;

		public PlaySoundParam(float _time, int _id, GetPosFunc _func = null)
		{
			time = _time;
			id = _id;
			func = _func;
		}
	}

	[SerializeField]
	private Animation cameraAnim;

	private readonly string BATTLE_ENTER_CAMERA_CLIP_NAME = "CAM_Tutorial";

	private readonly string BATTLE_EXIT_CAMERA_CLIP_NAME = "CAM_TutorialEnd_001";

	[SerializeField]
	private RuntimeAnimatorController playerAnimatorController;

	private RuntimeAnimatorController originalPlayerAnimatorController;

	private Character player;

	private readonly string PLAYER_ANIM_ENTER_CUT_SCENE_START_NAME = "PLC00_1001_tutorial";

	private readonly string PLAYER_ANIM_EXIT_CUT_SCENE_START_NAME = "PLC00_1002_TutorialEnd";

	private Enemy boss;

	private CircleShadow bossShadow;

	private Material bossShadowMaterial;

	private EnemyController enemyController;

	private readonly string BOSS_ANIM_ENTER_CUT_SCENE_STATE_NAME = "ENM011_1001_Tutorial";

	private readonly string BOSS_ANIM_EXIT_CUT_SCENE_STATE_NAME = "ENM011_1002_TutorialEnd";

	private RadialBlurFilter radialBlurFilter;

	[SerializeField]
	private GameObject[] titleEffectPrefab;

	public bool replaceCameraRoationWithCutSceneRotation;

	[SerializeField]
	private RuntimeAnimatorController legendDragonAnimController;

	private GameObject legendDragon;

	private GameObject titleUIPrefab;

	private readonly string LEGEND_DRAGON_ANIM_STATE = "ENM011_1003_TutorialEnd";

	private Vector3 cutChangePosition = Vector3.zero;

	private Quaternion cutChangeRotation = Quaternion.identity;

	[SerializeField]
	public Logo logo;

	public static readonly Vector3 CAMERA_END_POSITION = new Vector3(-0.24f, 2.67f, 31.75705f);

	public static readonly Quaternion CAMERA_END_ROTAION = new Quaternion(0.003f, 0.9898f, -0.1407118f, 0.02110636f);

	private readonly float DURATION_TO_BATTLE_START = 0.4f;

	private GameObject[] effects;

	public Camera logoCamera
	{
		get
		{
			if (logo != null)
			{
				return logo.camera;
			}
			return null;
		}
	}

	public float originalFov
	{
		get;
		set;
	}

	public void StartBattleStartDirection(Enemy enemy, Character character, Action onComplete)
	{
		boss = enemy;
		bossShadow = boss.GetComponentInChildren<CircleShadow>();
		bossShadowMaterial = bossShadow.GetComponent<MeshRenderer>().material;
		bossShadow.setAnimTransform(boss.hip);
		player = character;
		radialBlurFilter = MonoBehaviourSingleton<AppMain>.I.mainCamera.GetComponent<RadialBlurFilter>();
		MonoBehaviourSingleton<SoundManager>.I.requestBGMID = 114;
		MonoBehaviourSingleton<SoundManager>.I.TransitionTo("EventBattle1");
		originalPlayerAnimatorController = player.animator.runtimeAnimatorController;
		player.animator.runtimeAnimatorController = playerAnimatorController;
		player.animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		player.animator.Rebind();
		player._position = new Vector3(0f, 0f, 26f);
		player.PlayMotion(PLAYER_ANIM_ENTER_CUT_SCENE_START_NAME);
		enemy.animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		enemy.animator.Rebind();
		enemy.PlayMotion(BOSS_ANIM_ENTER_CUT_SCENE_STATE_NAME);
		enemyController = enemy.GetComponent<EnemyController>();
		enemyController.enabled = false;
		originalFov = MonoBehaviourSingleton<AppMain>.I.mainCamera.fieldOfView;
		cameraAnim.cullingType = AnimationCullingType.AlwaysAnimate;
		cameraAnim.Play(BATTLE_ENTER_CAMERA_CLIP_NAME);
		MonoBehaviourSingleton<InGameCameraManager>.I.enabled = false;
		StartCoroutine(DoBattleStartDirection(onComplete));
	}

	private IEnumerator WaitAndPlaySounds(List<PlaySoundParam> playSoundParams)
	{
		float timer = 0f;
		while (0 < playSoundParams.Count)
		{
			PlaySoundParam playSoundParam = playSoundParams[0];
			if (timer >= playSoundParam.time)
			{
				if (playSoundParam.func != null)
				{
					Vector3 pos = playSoundParam.func();
					SoundManager.PlayOneShotSE(playSoundParam.id, pos);
				}
				else
				{
					SoundManager.PlayOneShotUISE(playSoundParam.id);
				}
				playSoundParams.Remove(playSoundParam);
			}
			timer += Time.deltaTime;
			yield return null;
		}
	}

	private IEnumerator DoBattleStartDirection(Action onComplete)
	{
		Transform t = cameraAnim.transform;
		Camera mainCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		Transform cameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
		List<PlaySoundParam> list = new List<PlaySoundParam>();
		list.Add(new PlaySoundParam(0f, UITutorialOperationHelper.SE_ID_THUNDERSTORM_01));
		list.Add(new PlaySoundParam(5.53f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01));
		list.Add(new PlaySoundParam(6.53f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01));
		list.Add(new PlaySoundParam(7.56f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01));
		list.Add(new PlaySoundParam(8.56f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01));
		list.Add(new PlaySoundParam(9.56f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01));
		list.Add(new PlaySoundParam(11.3f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01));
		list.Add(new PlaySoundParam(11.93f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01));
		list.Add(new PlaySoundParam(13f, UITutorialOperationHelper.SE_ID_DRAGON_LANDING));
		list.Add(new PlaySoundParam(14.7f, UITutorialOperationHelper.SE_ID_DRAGON_CALL_01, () => boss.head.position));
		StartCoroutine(WaitAndPlaySounds(list));
		StartCoroutine(WaitForTime(14.7f, delegate
		{
			StartCoroutine(DoRadialBlur(0.6f, 0.3f, 1f));
		}));
		while (cameraAnim.isPlaying)
		{
			boss._rigidbody.Sleep();
			if (5f < boss.head.position.y)
			{
				bossShadowMaterial.SetFloat("_AlphaPower", 2.5f / boss.head.position.y);
			}
			cameraTransform.position = t.position;
			cameraTransform.rotation = t.rotation;
			mainCamera.fieldOfView = t.localScale.x;
			yield return null;
		}
		bossShadowMaterial.SetFloat("_AlphaPower", 0.5f);
		boss.transform.position = Vector3.zero;
		player.PlayMotion("idle");
		player.animator.runtimeAnimatorController = originalPlayerAnimatorController;
		Vector3 startCameraPos = cameraTransform.position;
		Quaternion startCameraRotation = cameraTransform.rotation;
		float startFieldOfView = mainCamera.fieldOfView;
		float timer = 0f;
		while (timer < DURATION_TO_BATTLE_START)
		{
			timer += Time.deltaTime;
			float t2 = timer / DURATION_TO_BATTLE_START;
			cameraTransform.position = Vector3.Lerp(startCameraPos, CAMERA_END_POSITION, t2);
			cameraTransform.rotation = Quaternion.Slerp(startCameraRotation, CAMERA_END_ROTAION, t2);
			mainCamera.fieldOfView = Mathf.Lerp(startFieldOfView, originalFov, t2);
			yield return null;
		}
		boss.PlayMotion("idle");
		MonoBehaviourSingleton<InGameCameraManager>.I.ResetMovePositionAndRotaion();
		MonoBehaviourSingleton<InGameCameraManager>.I.enabled = true;
		bossShadow.setAnimTransform(null);
		bossShadow.transform.position = boss.transform.position;
		onComplete?.Invoke();
	}

	public void StartBattleEndDirection(Enemy _boss, Character _player, GameObject legend, GameObject title_ui, Action onComplete)
	{
		boss = _boss;
		player = _player;
		StartBattleEndDirection(legend, title_ui, onComplete);
	}

	public void StartBattleEndDirection(GameObject legend, GameObject title_ui, Action onComplete)
	{
		titleUIPrefab = title_ui;
		originalPlayerAnimatorController = player.animator.runtimeAnimatorController;
		player._collider.enabled = false;
		player.animator.runtimeAnimatorController = playerAnimatorController;
		player.animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		player.animator.Rebind();
		player._transform.position = new Vector3(0f, 0f, 26f);
		player._transform.eulerAngles = new Vector3(0f, 180f, 0f);
		player._rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		player.ActIdle();
		player.PlayMotion(PLAYER_ANIM_EXIT_CUT_SCENE_START_NAME);
		legend.SetActive(value: true);
		legendDragon = legend;
		Animator component = legend.GetComponent<Animator>();
		component.runtimeAnimatorController = legendDragonAnimController;
		component.Play(LEGEND_DRAGON_ANIM_STATE);
		if (boss != null)
		{
			if (boss.colliders != null && boss.colliders.Length != 0)
			{
				int i = 0;
				for (int num = boss.colliders.Length; i < num; i++)
				{
					if (boss.colliders[i] != null)
					{
						boss.colliders[i].enabled = false;
					}
				}
				boss._transform.position = Vector3.zero;
				boss._transform.eulerAngles = Vector3.zero;
				boss._rigidbody.constraints = RigidbodyConstraints.FreezeAll;
				boss.ActIdle();
				boss.animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
				boss.animator.Rebind();
				boss.PlayMotion(BOSS_ANIM_EXIT_CUT_SCENE_STATE_NAME);
			}
			if (boss.hip != null)
			{
				bossShadow.setAnimTransform(boss.hip);
			}
		}
		if (enemyController == null)
		{
			enemyController = (boss.controller as EnemyController);
		}
		if (enemyController != null)
		{
			enemyController.enabled = false;
		}
		originalFov = MonoBehaviourSingleton<AppMain>.I.mainCamera.fieldOfView;
		cameraAnim.transform.position = Vector3.zero;
		cameraAnim.transform.rotation = Quaternion.identity;
		cameraAnim.transform.localScale = Vector3.zero;
		cameraAnim.cullingType = AnimationCullingType.AlwaysAnimate;
		cameraAnim.Play(BATTLE_EXIT_CAMERA_CLIP_NAME);
		cameraAnim.Sample();
		MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position = CAMERA_END_POSITION;
		MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.rotation = CAMERA_END_ROTAION;
		MonoBehaviourSingleton<InGameCameraManager>.I.enabled = false;
		StartCoroutine(DoBattleEndDirection(onComplete));
	}

	private IEnumerator DoBattleEndDirection(Action onComplete)
	{
		Transform t = cameraAnim.transform;
		Camera mainCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		Transform cameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
		List<PlaySoundParam> list = new List<PlaySoundParam>();
		list.Add(new PlaySoundParam(0f, UITutorialOperationHelper.SE_ID_THUNDERSTORM_02));
		list.Add(new PlaySoundParam(0f, UITutorialOperationHelper.SE_ID_DRAGON_CALL_02));
		list.Add(new PlaySoundParam(2.26f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01));
		list.Add(new PlaySoundParam(3.2f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01));
		list.Add(new PlaySoundParam(4.16f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01));
		list.Add(new PlaySoundParam(5.16f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01));
		list.Add(new PlaySoundParam(6.13f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01));
		list.Add(new PlaySoundParam(8.65f, UITutorialOperationHelper.SE_ID_DRAGON_CALL_03));
		list.Add(new PlaySoundParam(11.2f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_02));
		list.Add(new PlaySoundParam(13.2f, UITutorialOperationHelper.SE_ID_DRAGON_CALL_04));
		StartCoroutine(WaitAndPlaySounds(list));
		bool isRequestedLogoAnimation = false;
		while (cameraAnim.isPlaying)
		{
			if (boss != null)
			{
				if (boss._rigidbody != null)
				{
					boss._rigidbody.Sleep();
				}
				if (boss.head != null && 2.5f < boss.head.position.y)
				{
					bossShadowMaterial.SetFloat("_AlphaPower", 1.25f / boss.head.position.y);
				}
			}
			cameraTransform.position = t.position;
			cameraTransform.rotation = t.rotation;
			float x = t.localScale.x;
			if (1f < x)
			{
				mainCamera.fieldOfView = x;
			}
			if (t.localScale.z > 0.5f && !isRequestedLogoAnimation)
			{
				isRequestedLogoAnimation = true;
				StartLogoAnimation(tutorial_flag: true, onComplete);
			}
			yield return null;
		}
		if (bossShadow != null)
		{
			bossShadow.setAnimTransform(null);
			bossShadow.transform.position = boss.transform.position;
		}
		if (!isRequestedLogoAnimation)
		{
			StartLogoAnimation(tutorial_flag: true, onComplete);
		}
	}

	public void StartLogoAnimation(bool tutorial_flag, Action onComplete, Action onLoop = null)
	{
		StartCoroutine(DoStartLogoAnimation(tutorial_flag, onComplete, onLoop));
	}

	public void InitLogo()
	{
		logo.camera.gameObject.SetActive(value: false);
		logo.eye.transform.localScale = Vector3.zero;
		Material material = logo.fader.material;
		Material material2 = logo.logo.material;
		material.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
		material2.SetFloat("_AlphaRate", -1f);
		material2.SetFloat("_BlendRate", 0f);
		logo.effect1.SetActive(value: false);
		logo.bg.SetActive(value: false);
		if (effects == null)
		{
			return;
		}
		for (int i = 0; i < effects.Length; i++)
		{
			if (effects[i] != null)
			{
				UnityEngine.Object.Destroy(effects[i]);
				effects[i] = null;
			}
		}
	}

	private IEnumerator DoFadeOut()
	{
		Material faderMat = logo.fader.material;
		float timer = 0f;
		while (timer < 0.3f)
		{
			timer += Time.deltaTime;
			faderMat.SetColor("_Color", new Color(0f, 0f, 0f, Mathf.Clamp01(timer / 0.3f)));
			yield return null;
		}
		if (legendDragon != null)
		{
			legendDragon.SetActive(value: false);
		}
	}

	private IEnumerator DoStartLogoAnimation(bool tutorial_flag, Action onComplete, Action onLoop)
	{
		logo.root.position = Vector3.up * 1000f;
		logo.root.rotation = Quaternion.identity;
		logo.root.localScale = Vector3.one;
		if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedModifyTitleTop)
		{
			DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
			logo.camera.orthographicSize = specialDeviceInfo.TitleTopCameraSize;
			logo.bg.transform.localScale = specialDeviceInfo.TitleTopBGScale;
		}
		logo.camera.gameObject.SetActive(value: true);
		Material faderMat = logo.fader.material;
		Material logoMat = logo.logo.material;
		logo.camera.depth = -1f;
		logo.dragonRoot.SetActive(value: false);
		Color dragonPlaneColor = new Color(1f, 1f, 1f, 0f);
		logo.dragonPlane.sharedMaterial.SetColor("Color", dragonPlaneColor);
		if (tutorial_flag)
		{
			StartCoroutine(DoFadeOut());
			SoundManager.RequestBGM(11, isLoop: false);
			while (MonoBehaviourSingleton<SoundManager>.I.playingBGMID != 11 || MonoBehaviourSingleton<SoundManager>.I.changingBGM)
			{
				yield return null;
			}
			yield return new WaitForSeconds(2.3f);
		}
		else
		{
			faderMat.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
		}
		effects = new GameObject[titleEffectPrefab.Length];
		for (int i = 0; i < titleEffectPrefab.Length; i++)
		{
			rymFX component = ResourceUtility.Realizes(titleEffectPrefab[i]).GetComponent<rymFX>();
			component.Cameras = new Camera[1]
			{
				logo.camera
			};
			component._transform.localScale = component._transform.localScale * 10f;
			if (i == 1)
			{
				component._transform.position = new Vector3(0.568f, 999.946f, 0.1f);
			}
			else
			{
				component._transform.position = logo.eye.transform.position;
			}
			effects[i] = component.gameObject;
		}
		yield return new WaitForSeconds(1f);
		float timer4 = 0f;
		while (timer4 < 0.17f)
		{
			timer4 += Time.deltaTime;
			float d = Mathf.Clamp01(timer4 / 0.17f);
			logo.eye.transform.localScale = Vector3.one * d * 10f;
			logoMat.SetFloat("_AlphaRate", -1f + timer4 * 2f);
			yield return null;
		}
		logo.dragonRoot.SetActive(value: true);
		while (timer4 < 1f)
		{
			timer4 += Time.deltaTime;
			logoMat.SetFloat("_AlphaRate", -1f + timer4 * 2f);
			dragonPlaneColor.a = timer4;
			logo.dragonPlane.sharedMaterial.SetColor("Color", dragonPlaneColor);
			yield return null;
		}
		dragonPlaneColor.a = 1f;
		logo.dragonPlane.sharedMaterial.SetColor("Color", dragonPlaneColor);
		timer4 = 0f;
		while (timer4 < 0.5f)
		{
			timer4 += Time.deltaTime;
			logoMat.SetFloat("_BlendRate", timer4 * 2f);
			yield return null;
		}
		logo.bg.SetActive(value: true);
		logo.effect1.SetActive(value: true);
		timer4 = 0f;
		Material bgMaterial = logo.bgFader.material;
		while (timer4 < 0.7f)
		{
			timer4 += Time.deltaTime;
			bgMaterial.color = new Color(1f, 1f, 1f, 1f - timer4 / 0.7f);
			yield return null;
		}
		if (!tutorial_flag)
		{
			onLoop?.Invoke();
			while (!tutorial_flag)
			{
				yield return null;
			}
		}
		yield return new WaitForSeconds(0.3f);
		if (titleUIPrefab != null)
		{
			Transform transform = ResourceUtility.Realizes(titleUIPrefab, MonoBehaviourSingleton<UIManager>.I.uiRootTransform, 5);
			if (transform != null)
			{
				Transform transform2 = Utility.Find(transform, "BTN_START");
				if (transform2 != null)
				{
					transform2.GetComponent<Collider>().enabled = false;
				}
				transform2 = Utility.Find(transform, "BTN_ADVANCED_LOGIN");
				if (transform2 != null)
				{
					transform2.gameObject.SetActive(value: false);
				}
				transform2 = Utility.Find(transform, "BTN_CLEARCACHE");
				if (transform2 != null)
				{
					transform2.gameObject.SetActive(value: false);
				}
			}
		}
		yield return new WaitForSeconds(6f);
		timer4 = 0f;
		while (timer4 < 0.3f)
		{
			timer4 += Time.deltaTime;
			faderMat.SetColor("_Color", new Color(0f, 0f, 0f, timer4 / 0.3f));
			yield return null;
		}
		MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, disable: false);
		for (int j = 0; j < effects.Length; j++)
		{
			EffectManager.ReleaseEffect(effects[j]);
		}
		onComplete?.Invoke();
	}

	private void LateUpdate()
	{
		Transform transform = cameraAnim.transform;
		if (!MonoBehaviourSingleton<AppMain>.IsValid())
		{
			return;
		}
		if (!replaceCameraRoationWithCutSceneRotation)
		{
			if (transform.localScale.y > 0.1f)
			{
				replaceCameraRoationWithCutSceneRotation = true;
				cutChangePosition = transform.position;
				cutChangeRotation = transform.rotation;
			}
		}
		else
		{
			if (!replaceCameraRoationWithCutSceneRotation)
			{
				return;
			}
			if (transform.localScale.y > 0.1f)
			{
				if (MonoBehaviourSingleton<AppMain>.IsValid() && MonoBehaviourSingleton<AppMain>.I.mainCameraTransform != null)
				{
					MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position = cutChangePosition;
					MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.rotation = cutChangeRotation;
				}
			}
			else
			{
				replaceCameraRoationWithCutSceneRotation = false;
			}
		}
	}

	private IEnumerator DoRadialBlur(float startDuration, float endDuration, float maxStrength)
	{
		radialBlurFilter.StartFilter();
		float timer2 = 0f;
		Camera camera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		while (timer2 < startDuration)
		{
			timer2 += Time.deltaTime;
			radialBlurFilter.strength = Mathf.Lerp(0f, maxStrength, timer2 / 0.6f);
			radialBlurFilter.SetCenter(camera.WorldToViewportPoint(boss.head.position));
			yield return null;
		}
		timer2 = 0f;
		while (timer2 < endDuration)
		{
			timer2 += Time.deltaTime;
			radialBlurFilter.strength = Mathf.Lerp(maxStrength, 0f, timer2 / 0.3f);
			radialBlurFilter.SetCenter(camera.WorldToViewportPoint(boss.head.position));
			yield return null;
		}
		radialBlurFilter.StopFilter();
	}

	private IEnumerator WaitForTime(float waitTime, Action action)
	{
		yield return new WaitForSeconds(waitTime);
		action();
	}
}
