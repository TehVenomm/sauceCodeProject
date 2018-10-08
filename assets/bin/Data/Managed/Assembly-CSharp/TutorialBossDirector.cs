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
	private Logo logo;

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
		MonoBehaviourSingleton<SoundManager>.I.TransitionTo("EventBattle1", 1f);
		originalPlayerAnimatorController = player.animator.runtimeAnimatorController;
		player.animator.runtimeAnimatorController = playerAnimatorController;
		player.animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		player.animator.Rebind();
		Character character2 = player;
		Vector3 position = player._position;
		float y = position.y;
		Vector3 position2 = player._position;
		character2._position = new Vector3(0f, y, position2.z);
		player.PlayMotion(PLAYER_ANIM_ENTER_CUT_SCENE_START_NAME, -1f);
		enemy.animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		enemy.animator.Rebind();
		enemy.PlayMotion(BOSS_ANIM_ENTER_CUT_SCENE_STATE_NAME, -1f);
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
			PlaySoundParam param = playSoundParams[0];
			if (timer >= param.time)
			{
				if (param.func != null)
				{
					SoundManager.PlayOneShotSE(pos: param.func(), se_id: param.id);
				}
				else
				{
					SoundManager.PlayOneShotUISE(param.id);
				}
				playSoundParams.Remove(param);
			}
			timer += Time.deltaTime;
			yield return (object)null;
		}
	}

	private IEnumerator DoBattleStartDirection(Action onComplete)
	{
		Transform t = cameraAnim.transform;
		Camera mainCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		Transform cameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
		StartCoroutine(WaitAndPlaySounds(new List<PlaySoundParam>
		{
			new PlaySoundParam(0f, UITutorialOperationHelper.SE_ID_THUNDERSTORM_01, null),
			new PlaySoundParam(5.53f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01, null),
			new PlaySoundParam(6.53f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01, null),
			new PlaySoundParam(7.56f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01, null),
			new PlaySoundParam(8.56f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01, null),
			new PlaySoundParam(9.56f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01, null),
			new PlaySoundParam(11.3f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01, null),
			new PlaySoundParam(11.93f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01, null),
			new PlaySoundParam(13f, UITutorialOperationHelper.SE_ID_DRAGON_LANDING, null),
			new PlaySoundParam(14.7f, UITutorialOperationHelper.SE_ID_DRAGON_CALL_01, () => ((_003CDoBattleStartDirection_003Ec__Iterator1E6)/*Error near IL_0169: stateMachine*/)._003C_003Ef__this.boss.head.position)
		}));
		StartCoroutine(WaitForTime(14.7f, delegate
		{
			((_003CDoBattleStartDirection_003Ec__Iterator1E6)/*Error near IL_01ad: stateMachine*/)._003C_003Ef__this.StartCoroutine(((_003CDoBattleStartDirection_003Ec__Iterator1E6)/*Error near IL_01ad: stateMachine*/)._003C_003Ef__this.DoRadialBlur(0.6f, 0.3f, 1f));
		}));
		while (cameraAnim.isPlaying)
		{
			boss._rigidbody.Sleep();
			Vector3 position = boss.head.position;
			if (5f < position.y)
			{
				Material material = bossShadowMaterial;
				Vector3 position2 = boss.head.position;
				material.SetFloat("_AlphaPower", 2.5f / position2.y);
			}
			cameraTransform.position = t.position;
			cameraTransform.rotation = t.rotation;
			Camera camera = mainCamera;
			Vector3 localScale = t.localScale;
			camera.fieldOfView = localScale.x;
			yield return (object)null;
		}
		bossShadowMaterial.SetFloat("_AlphaPower", 0.5f);
		boss.transform.position = Vector3.zero;
		player.PlayMotion("idle", -1f);
		player.animator.runtimeAnimatorController = originalPlayerAnimatorController;
		Vector3 startCameraPos = cameraTransform.position;
		Quaternion startCameraRotation = cameraTransform.rotation;
		float startFieldOfView = mainCamera.fieldOfView;
		float timer = 0f;
		while (timer < DURATION_TO_BATTLE_START)
		{
			timer += Time.deltaTime;
			float ratio = timer / DURATION_TO_BATTLE_START;
			cameraTransform.position = Vector3.Lerp(startCameraPos, CAMERA_END_POSITION, ratio);
			cameraTransform.rotation = Quaternion.Slerp(startCameraRotation, CAMERA_END_ROTAION, ratio);
			mainCamera.fieldOfView = Mathf.Lerp(startFieldOfView, originalFov, ratio);
			yield return (object)null;
		}
		boss.PlayMotion("idle", -1f);
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
		player.ActIdle(false, -1f);
		player.PlayMotion(PLAYER_ANIM_EXIT_CUT_SCENE_START_NAME, -1f);
		legend.SetActive(true);
		legendDragon = legend;
		Animator component = legend.GetComponent<Animator>();
		component.runtimeAnimatorController = legendDragonAnimController;
		component.Play(LEGEND_DRAGON_ANIM_STATE);
		for (int i = 0; i < boss.colliders.Length; i++)
		{
			boss.colliders[i].enabled = false;
		}
		boss._transform.position = Vector3.zero;
		boss._transform.eulerAngles = Vector3.zero;
		boss._rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		boss.ActIdle(false, -1f);
		boss.animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		boss.animator.Rebind();
		boss.PlayMotion(BOSS_ANIM_EXIT_CUT_SCENE_STATE_NAME, -1f);
		bossShadow.setAnimTransform(boss.hip);
		if ((UnityEngine.Object)enemyController == (UnityEngine.Object)null)
		{
			enemyController = (boss.controller as EnemyController);
		}
		enemyController.enabled = false;
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
		StartCoroutine(WaitAndPlaySounds(new List<PlaySoundParam>
		{
			new PlaySoundParam(0f, UITutorialOperationHelper.SE_ID_THUNDERSTORM_02, null),
			new PlaySoundParam(0f, UITutorialOperationHelper.SE_ID_DRAGON_CALL_02, null),
			new PlaySoundParam(2.26f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01, null),
			new PlaySoundParam(3.2f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01, null),
			new PlaySoundParam(4.16f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01, null),
			new PlaySoundParam(5.16f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01, null),
			new PlaySoundParam(6.13f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_01, null),
			new PlaySoundParam(8.65f, UITutorialOperationHelper.SE_ID_DRAGON_CALL_03, null),
			new PlaySoundParam(11.2f, UITutorialOperationHelper.SE_ID_DRAGON_FLUTTER_02, null),
			new PlaySoundParam(13.2f, UITutorialOperationHelper.SE_ID_DRAGON_CALL_04, null)
		}));
		bool isRequestedLogoAnimation = false;
		while (cameraAnim.isPlaying)
		{
			boss._rigidbody.Sleep();
			Vector3 position = boss.head.position;
			if (2.5f < position.y)
			{
				Material material = bossShadowMaterial;
				Vector3 position2 = boss.head.position;
				material.SetFloat("_AlphaPower", 1.25f / position2.y);
			}
			cameraTransform.position = t.position;
			cameraTransform.rotation = t.rotation;
			Vector3 localScale = t.localScale;
			float fov = localScale.x;
			if (1f < fov)
			{
				mainCamera.fieldOfView = fov;
			}
			Vector3 localScale2 = t.localScale;
			if (localScale2.z > 0.5f && !isRequestedLogoAnimation)
			{
				isRequestedLogoAnimation = true;
				StartLogoAnimation(true, onComplete, null);
			}
			yield return (object)null;
		}
		bossShadow.setAnimTransform(null);
		bossShadow.transform.position = boss.transform.position;
		if (!isRequestedLogoAnimation)
		{
			StartLogoAnimation(true, onComplete, null);
		}
	}

	public void StartLogoAnimation(bool tutorial_flag, Action onComplete, Action onLoop = null)
	{
		StartCoroutine(DoStartLogoAnimation(tutorial_flag, onComplete, onLoop));
	}

	public void InitLogo()
	{
		logo.camera.gameObject.SetActive(false);
		logo.eye.transform.localScale = Vector3.zero;
		Material material = logo.fader.material;
		Material material2 = logo.logo.material;
		material.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
		material2.SetFloat("_AlphaRate", -1f);
		material2.SetFloat("_BlendRate", 0f);
		logo.effect1.SetActive(false);
		logo.bg.SetActive(false);
		if (effects != null)
		{
			for (int i = 0; i < effects.Length; i++)
			{
				if ((UnityEngine.Object)effects[i] != (UnityEngine.Object)null)
				{
					UnityEngine.Object.Destroy(effects[i]);
					effects[i] = null;
				}
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
			yield return (object)null;
		}
		if ((UnityEngine.Object)legendDragon != (UnityEngine.Object)null)
		{
			legendDragon.SetActive(false);
		}
	}

	private IEnumerator DoStartLogoAnimation(bool tutorial_flag, Action onComplete, Action onLoop)
	{
		logo.root.position = Vector3.up * 1000f;
		logo.root.rotation = Quaternion.identity;
		logo.root.localScale = Vector3.one;
		logo.camera.gameObject.SetActive(true);
		Material faderMat = logo.fader.material;
		Material logoMat = logo.logo.material;
		logo.camera.depth = -1f;
		logo.dragonRoot.SetActive(false);
		Color dragonPlaneColor = new Color(1f, 1f, 1f, 0f);
		logo.dragonPlane.sharedMaterial.SetColor("Color", dragonPlaneColor);
		if (tutorial_flag)
		{
			StartCoroutine(DoFadeOut());
			SoundManager.RequestBGM(11, false);
			while (MonoBehaviourSingleton<SoundManager>.I.playingBGMID != 11 || MonoBehaviourSingleton<SoundManager>.I.changingBGM)
			{
				yield return (object)null;
			}
			yield return (object)new WaitForSeconds(2.3f);
		}
		else
		{
			faderMat.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
		}
		effects = new GameObject[titleEffectPrefab.Length];
		for (int j = 0; j < titleEffectPrefab.Length; j++)
		{
			rymFX effect = ResourceUtility.Realizes(titleEffectPrefab[j], -1).GetComponent<rymFX>();
			effect.Cameras = new Camera[1]
			{
				logo.camera
			};
			effect._transform.localScale = effect._transform.localScale * 10f;
			if (j == 1)
			{
				effect._transform.position = new Vector3(0.568f, 999.946f, 0.1f);
			}
			else
			{
				effect._transform.position = logo.eye.transform.position;
			}
			effects[j] = effect.gameObject;
		}
		yield return (object)new WaitForSeconds(1f);
		float timer4 = 0f;
		while (timer4 < 0.17f)
		{
			timer4 += Time.deltaTime;
			float s = Mathf.Clamp01(timer4 / 0.17f);
			logo.eye.transform.localScale = Vector3.one * s * 10f;
			logoMat.SetFloat("_AlphaRate", -1f + timer4 * 2f);
			yield return (object)null;
		}
		logo.dragonRoot.SetActive(true);
		while (timer4 < 1f)
		{
			timer4 += Time.deltaTime;
			logoMat.SetFloat("_AlphaRate", -1f + timer4 * 2f);
			dragonPlaneColor.a = timer4;
			logo.dragonPlane.sharedMaterial.SetColor("Color", dragonPlaneColor);
			yield return (object)null;
		}
		dragonPlaneColor.a = 1f;
		logo.dragonPlane.sharedMaterial.SetColor("Color", dragonPlaneColor);
		timer4 = 0f;
		while (timer4 < 0.5f)
		{
			timer4 += Time.deltaTime;
			logoMat.SetFloat("_BlendRate", timer4 * 2f);
			yield return (object)null;
		}
		logo.bg.SetActive(true);
		logo.effect1.SetActive(true);
		timer4 = 0f;
		Material bgMaterial = logo.bgFader.material;
		while (timer4 < 0.7f)
		{
			timer4 += Time.deltaTime;
			bgMaterial.color = new Color(1f, 1f, 1f, 1f - timer4 / 0.7f);
			yield return (object)null;
		}
		if (!tutorial_flag)
		{
			onLoop?.Invoke();
			while (!tutorial_flag)
			{
				yield return (object)null;
			}
		}
		yield return (object)new WaitForSeconds(0.3f);
		if ((UnityEngine.Object)titleUIPrefab != (UnityEngine.Object)null)
		{
			Transform title_ui = ResourceUtility.Realizes(titleUIPrefab, MonoBehaviourSingleton<UIManager>.I.uiRootTransform, 5);
			if ((UnityEngine.Object)title_ui != (UnityEngine.Object)null)
			{
				Transform t3 = Utility.Find(title_ui, "BTN_START");
				if ((UnityEngine.Object)t3 != (UnityEngine.Object)null)
				{
					t3.GetComponent<Collider>().enabled = false;
				}
				t3 = Utility.Find(title_ui, "BTN_ADVANCED_LOGIN");
				if ((UnityEngine.Object)t3 != (UnityEngine.Object)null)
				{
					t3.gameObject.SetActive(false);
				}
				t3 = Utility.Find(title_ui, "BTN_CLEARCACHE");
				if ((UnityEngine.Object)t3 != (UnityEngine.Object)null)
				{
					t3.gameObject.SetActive(false);
				}
			}
		}
		yield return (object)new WaitForSeconds(6f);
		timer4 = 0f;
		while (timer4 < 0.3f)
		{
			timer4 += Time.deltaTime;
			faderMat.SetColor("_Color", new Color(0f, 0f, 0f, timer4 / 0.3f));
			yield return (object)null;
		}
		MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
		for (int i = 0; i < effects.Length; i++)
		{
			EffectManager.ReleaseEffect(effects[i], true, false);
		}
		onComplete?.Invoke();
	}

	private void LateUpdate()
	{
		Transform transform = cameraAnim.transform;
		if (MonoBehaviourSingleton<AppMain>.IsValid())
		{
			if (!replaceCameraRoationWithCutSceneRotation)
			{
				Vector3 localScale = transform.localScale;
				if (localScale.y > 0.1f)
				{
					replaceCameraRoationWithCutSceneRotation = true;
					cutChangePosition = transform.position;
					cutChangeRotation = transform.rotation;
				}
			}
			else if (replaceCameraRoationWithCutSceneRotation)
			{
				Vector3 localScale2 = transform.localScale;
				if (localScale2.y > 0.1f)
				{
					if (MonoBehaviourSingleton<AppMain>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<AppMain>.I.mainCameraTransform != (UnityEngine.Object)null)
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
			yield return (object)null;
		}
		timer2 = 0f;
		while (timer2 < endDuration)
		{
			timer2 += Time.deltaTime;
			radialBlurFilter.strength = Mathf.Lerp(maxStrength, 0f, timer2 / 0.3f);
			radialBlurFilter.SetCenter(camera.WorldToViewportPoint(boss.head.position));
			yield return (object)null;
		}
		radialBlurFilter.StopFilter();
	}

	private IEnumerator WaitForTime(float waitTime, Action action)
	{
		yield return (object)new WaitForSeconds(waitTime);
		action();
	}
}
