using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBossDirector
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

	private Vector3 cutChangePosition = Vector3.get_zero();

	private Quaternion cutChangeRotation = Quaternion.get_identity();

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

	public TutorialBossDirector()
		: this()
	{
	}//IL_004e: Unknown result type (might be due to invalid IL or missing references)
	//IL_0053: Unknown result type (might be due to invalid IL or missing references)
	//IL_0059: Unknown result type (might be due to invalid IL or missing references)
	//IL_005e: Unknown result type (might be due to invalid IL or missing references)


	public void StartBattleStartDirection(Enemy enemy, Character character, Action onComplete)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Expected O, but got Unknown
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		boss = enemy;
		bossShadow = boss.GetComponentInChildren<CircleShadow>();
		bossShadowMaterial = bossShadow.GetComponent<MeshRenderer>().get_material();
		bossShadow.setAnimTransform(boss.hip);
		player = character;
		radialBlurFilter = MonoBehaviourSingleton<AppMain>.I.mainCamera.GetComponent<RadialBlurFilter>();
		MonoBehaviourSingleton<SoundManager>.I.requestBGMID = 114;
		MonoBehaviourSingleton<SoundManager>.I.TransitionTo("EventBattle1", 1f);
		originalPlayerAnimatorController = player.animator.get_runtimeAnimatorController();
		player.animator.set_runtimeAnimatorController(playerAnimatorController);
		player.animator.set_cullingMode(0);
		player.animator.Rebind();
		Character character2 = player;
		Vector3 position = player._position;
		float y = position.y;
		Vector3 position2 = player._position;
		character2._position = new Vector3(0f, y, position2.z);
		player.PlayMotion(PLAYER_ANIM_ENTER_CUT_SCENE_START_NAME, -1f);
		enemy.animator.set_cullingMode(0);
		enemy.animator.Rebind();
		enemy.PlayMotion(BOSS_ANIM_ENTER_CUT_SCENE_STATE_NAME, -1f);
		enemyController = enemy.GetComponent<EnemyController>();
		enemyController.set_enabled(false);
		originalFov = MonoBehaviourSingleton<AppMain>.I.mainCamera.get_fieldOfView();
		cameraAnim.set_cullingType(0);
		cameraAnim.Play(BATTLE_ENTER_CAMERA_CLIP_NAME);
		MonoBehaviourSingleton<InGameCameraManager>.I.set_enabled(false);
		this.StartCoroutine(DoBattleStartDirection(onComplete));
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
			timer += Time.get_deltaTime();
			yield return (object)null;
		}
	}

	private unsafe IEnumerator DoBattleStartDirection(Action onComplete)
	{
		Transform t = cameraAnim.get_transform();
		Camera mainCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		Transform cameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
		this.StartCoroutine(WaitAndPlaySounds(new List<PlaySoundParam>
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
			new PlaySoundParam(14.7f, UITutorialOperationHelper.SE_ID_DRAGON_CALL_01, () => ((_003CDoBattleStartDirection_003Ec__Iterator1ED)/*Error near IL_0169: stateMachine*/)._003C_003Ef__this.boss.head.get_position())
		}));
		this.StartCoroutine(WaitForTime(14.7f, new Action((object)/*Error near IL_01ad: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
		while (cameraAnim.get_isPlaying())
		{
			boss._rigidbody.Sleep();
			Vector3 position = boss.head.get_position();
			if (5f < position.y)
			{
				Material obj = bossShadowMaterial;
				Vector3 position2 = boss.head.get_position();
				obj.SetFloat("_AlphaPower", 2.5f / position2.y);
			}
			cameraTransform.set_position(t.get_position());
			cameraTransform.set_rotation(t.get_rotation());
			Camera obj2 = mainCamera;
			Vector3 localScale = t.get_localScale();
			obj2.set_fieldOfView(localScale.x);
			yield return (object)null;
		}
		bossShadowMaterial.SetFloat("_AlphaPower", 0.5f);
		boss.get_transform().set_position(Vector3.get_zero());
		player.PlayMotion("idle", -1f);
		player.animator.set_runtimeAnimatorController(originalPlayerAnimatorController);
		Vector3 startCameraPos = cameraTransform.get_position();
		Quaternion startCameraRotation = cameraTransform.get_rotation();
		float startFieldOfView = mainCamera.get_fieldOfView();
		float timer = 0f;
		while (timer < DURATION_TO_BATTLE_START)
		{
			timer += Time.get_deltaTime();
			float ratio = timer / DURATION_TO_BATTLE_START;
			cameraTransform.set_position(Vector3.Lerp(startCameraPos, CAMERA_END_POSITION, ratio));
			cameraTransform.set_rotation(Quaternion.Slerp(startCameraRotation, CAMERA_END_ROTAION, ratio));
			mainCamera.set_fieldOfView(Mathf.Lerp(startFieldOfView, originalFov, ratio));
			yield return (object)null;
		}
		boss.PlayMotion("idle", -1f);
		MonoBehaviourSingleton<InGameCameraManager>.I.ResetMovePositionAndRotaion();
		MonoBehaviourSingleton<InGameCameraManager>.I.set_enabled(true);
		bossShadow.setAnimTransform(null);
		bossShadow.get_transform().set_position(boss.get_transform().get_position());
		if (onComplete != null)
		{
			onComplete.Invoke();
		}
	}

	public void StartBattleEndDirection(Enemy _boss, Character _player, GameObject legend, GameObject title_ui, Action onComplete)
	{
		boss = _boss;
		player = _player;
		StartBattleEndDirection(legend, title_ui, onComplete);
	}

	public void StartBattleEndDirection(GameObject legend, GameObject title_ui, Action onComplete)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		titleUIPrefab = title_ui;
		originalPlayerAnimatorController = player.animator.get_runtimeAnimatorController();
		player._collider.set_enabled(false);
		player.animator.set_runtimeAnimatorController(playerAnimatorController);
		player.animator.set_cullingMode(0);
		player.animator.Rebind();
		player._transform.set_position(new Vector3(0f, 0f, 26f));
		player._transform.set_eulerAngles(new Vector3(0f, 180f, 0f));
		player._rigidbody.set_constraints(126);
		player.ActIdle(false, -1f);
		player.PlayMotion(PLAYER_ANIM_EXIT_CUT_SCENE_START_NAME, -1f);
		legend.SetActive(true);
		legendDragon = legend;
		Animator component = legend.GetComponent<Animator>();
		component.set_runtimeAnimatorController(legendDragonAnimController);
		component.Play(LEGEND_DRAGON_ANIM_STATE);
		for (int i = 0; i < boss.colliders.Length; i++)
		{
			boss.colliders[i].set_enabled(false);
		}
		boss._transform.set_position(Vector3.get_zero());
		boss._transform.set_eulerAngles(Vector3.get_zero());
		boss._rigidbody.set_constraints(126);
		boss.ActIdle(false, -1f);
		boss.animator.set_cullingMode(0);
		boss.animator.Rebind();
		boss.PlayMotion(BOSS_ANIM_EXIT_CUT_SCENE_STATE_NAME, -1f);
		bossShadow.setAnimTransform(boss.hip);
		if (enemyController == null)
		{
			enemyController = (boss.controller as EnemyController);
		}
		enemyController.set_enabled(false);
		originalFov = MonoBehaviourSingleton<AppMain>.I.mainCamera.get_fieldOfView();
		cameraAnim.get_transform().set_position(Vector3.get_zero());
		cameraAnim.get_transform().set_rotation(Quaternion.get_identity());
		cameraAnim.get_transform().set_localScale(Vector3.get_zero());
		cameraAnim.set_cullingType(0);
		cameraAnim.Play(BATTLE_EXIT_CAMERA_CLIP_NAME);
		cameraAnim.Sample();
		MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.set_position(CAMERA_END_POSITION);
		MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.set_rotation(CAMERA_END_ROTAION);
		MonoBehaviourSingleton<InGameCameraManager>.I.set_enabled(false);
		this.StartCoroutine(DoBattleEndDirection(onComplete));
	}

	private IEnumerator DoBattleEndDirection(Action onComplete)
	{
		Transform t = cameraAnim.get_transform();
		Camera mainCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		Transform cameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
		this.StartCoroutine(WaitAndPlaySounds(new List<PlaySoundParam>
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
		while (cameraAnim.get_isPlaying())
		{
			boss._rigidbody.Sleep();
			Vector3 position = boss.head.get_position();
			if (2.5f < position.y)
			{
				Material obj = bossShadowMaterial;
				Vector3 position2 = boss.head.get_position();
				obj.SetFloat("_AlphaPower", 1.25f / position2.y);
			}
			cameraTransform.set_position(t.get_position());
			cameraTransform.set_rotation(t.get_rotation());
			Vector3 localScale = t.get_localScale();
			float fov = localScale.x;
			if (1f < fov)
			{
				mainCamera.set_fieldOfView(fov);
			}
			Vector3 localScale2 = t.get_localScale();
			if (localScale2.z > 0.5f && !isRequestedLogoAnimation)
			{
				isRequestedLogoAnimation = true;
				StartLogoAnimation(true, onComplete, null);
			}
			yield return (object)null;
		}
		bossShadow.setAnimTransform(null);
		bossShadow.get_transform().set_position(boss.get_transform().get_position());
		if (!isRequestedLogoAnimation)
		{
			StartLogoAnimation(true, onComplete, null);
		}
	}

	public void StartLogoAnimation(bool tutorial_flag, Action onComplete, Action onLoop = null)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoStartLogoAnimation(tutorial_flag, onComplete, onLoop));
	}

	public void InitLogo()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		logo.camera.get_gameObject().SetActive(false);
		logo.eye.get_transform().set_localScale(Vector3.get_zero());
		Material val = logo.fader.get_material();
		Material val2 = logo.logo.get_material();
		val.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
		val2.SetFloat("_AlphaRate", -1f);
		val2.SetFloat("_BlendRate", 0f);
		logo.effect1.SetActive(false);
		logo.bg.SetActive(false);
		if (effects != null)
		{
			for (int i = 0; i < effects.Length; i++)
			{
				if (effects[i] != null)
				{
					Object.Destroy(effects[i]);
					effects[i] = null;
				}
			}
		}
	}

	private IEnumerator DoFadeOut()
	{
		Material faderMat = logo.fader.get_material();
		float timer = 0f;
		while (timer < 0.3f)
		{
			timer += Time.get_deltaTime();
			faderMat.SetColor("_Color", new Color(0f, 0f, 0f, Mathf.Clamp01(timer / 0.3f)));
			yield return (object)null;
		}
		if (legendDragon != null)
		{
			legendDragon.SetActive(false);
		}
	}

	private IEnumerator DoStartLogoAnimation(bool tutorial_flag, Action onComplete, Action onLoop)
	{
		logo.root.set_position(Vector3.get_up() * 1000f);
		logo.root.set_rotation(Quaternion.get_identity());
		logo.root.set_localScale(Vector3.get_one());
		logo.camera.get_gameObject().SetActive(true);
		Material faderMat = logo.fader.get_material();
		Material logoMat = logo.logo.get_material();
		logo.camera.set_depth(-1f);
		logo.dragonRoot.SetActive(false);
		Color dragonPlaneColor = new Color(1f, 1f, 1f, 0f);
		logo.dragonPlane.get_sharedMaterial().SetColor("Color", dragonPlaneColor);
		if (tutorial_flag)
		{
			this.StartCoroutine(DoFadeOut());
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
		effects = (GameObject[])new GameObject[titleEffectPrefab.Length];
		for (int j = 0; j < titleEffectPrefab.Length; j++)
		{
			rymFX effect = ResourceUtility.Realizes(titleEffectPrefab[j], -1).GetComponent<rymFX>();
			effect.Cameras = (Camera[])new Camera[1]
			{
				logo.camera
			};
			effect.get__transform().set_localScale(effect.get__transform().get_localScale() * 10f);
			if (j == 1)
			{
				effect.get__transform().set_position(new Vector3(0.568f, 999.946f, 0.1f));
			}
			else
			{
				effect.get__transform().set_position(logo.eye.get_transform().get_position());
			}
			effects[j] = effect.get_gameObject();
		}
		yield return (object)new WaitForSeconds(1f);
		float timer4 = 0f;
		while (timer4 < 0.17f)
		{
			timer4 += Time.get_deltaTime();
			float s = Mathf.Clamp01(timer4 / 0.17f);
			logo.eye.get_transform().set_localScale(Vector3.get_one() * s * 10f);
			logoMat.SetFloat("_AlphaRate", -1f + timer4 * 2f);
			yield return (object)null;
		}
		logo.dragonRoot.SetActive(true);
		while (timer4 < 1f)
		{
			timer4 += Time.get_deltaTime();
			logoMat.SetFloat("_AlphaRate", -1f + timer4 * 2f);
			dragonPlaneColor.a = timer4;
			logo.dragonPlane.get_sharedMaterial().SetColor("Color", dragonPlaneColor);
			yield return (object)null;
		}
		dragonPlaneColor.a = 1f;
		logo.dragonPlane.get_sharedMaterial().SetColor("Color", dragonPlaneColor);
		timer4 = 0f;
		while (timer4 < 0.5f)
		{
			timer4 += Time.get_deltaTime();
			logoMat.SetFloat("_BlendRate", timer4 * 2f);
			yield return (object)null;
		}
		logo.bg.SetActive(true);
		logo.effect1.SetActive(true);
		timer4 = 0f;
		Material bgMaterial = logo.bgFader.get_material();
		while (timer4 < 0.7f)
		{
			timer4 += Time.get_deltaTime();
			bgMaterial.set_color(new Color(1f, 1f, 1f, 1f - timer4 / 0.7f));
			yield return (object)null;
		}
		if (!tutorial_flag)
		{
			if (onLoop != null)
			{
				onLoop.Invoke();
			}
			while (!tutorial_flag)
			{
				yield return (object)null;
			}
		}
		yield return (object)new WaitForSeconds(0.3f);
		if (titleUIPrefab != null)
		{
			Transform title_ui = ResourceUtility.Realizes(titleUIPrefab, MonoBehaviourSingleton<UIManager>.I.uiRootTransform, 5);
			if (title_ui != null)
			{
				Transform t3 = Utility.Find(title_ui, "BTN_START");
				if (t3 != null)
				{
					t3.GetComponent<Collider>().set_enabled(false);
				}
				t3 = Utility.Find(title_ui, "BTN_ADVANCED_LOGIN");
				if (t3 != null)
				{
					t3.get_gameObject().SetActive(false);
				}
				t3 = Utility.Find(title_ui, "BTN_CLEARCACHE");
				if (t3 != null)
				{
					t3.get_gameObject().SetActive(false);
				}
			}
		}
		yield return (object)new WaitForSeconds(6f);
		timer4 = 0f;
		while (timer4 < 0.3f)
		{
			timer4 += Time.get_deltaTime();
			faderMat.SetColor("_Color", new Color(0f, 0f, 0f, timer4 / 0.3f));
			yield return (object)null;
		}
		MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
		for (int i = 0; i < effects.Length; i++)
		{
			EffectManager.ReleaseEffect(effects[i], true, false);
		}
		if (onComplete != null)
		{
			onComplete.Invoke();
		}
	}

	private void LateUpdate()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		Transform val = cameraAnim.get_transform();
		if (MonoBehaviourSingleton<AppMain>.IsValid())
		{
			if (!replaceCameraRoationWithCutSceneRotation)
			{
				Vector3 localScale = val.get_localScale();
				if (localScale.y > 0.1f)
				{
					replaceCameraRoationWithCutSceneRotation = true;
					cutChangePosition = val.get_position();
					cutChangeRotation = val.get_rotation();
				}
			}
			else if (replaceCameraRoationWithCutSceneRotation)
			{
				Vector3 localScale2 = val.get_localScale();
				if (localScale2.y > 0.1f)
				{
					if (MonoBehaviourSingleton<AppMain>.IsValid() && MonoBehaviourSingleton<AppMain>.I.mainCameraTransform != null)
					{
						MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.set_position(cutChangePosition);
						MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.set_rotation(cutChangeRotation);
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
			timer2 += Time.get_deltaTime();
			radialBlurFilter.strength = Mathf.Lerp(0f, maxStrength, timer2 / 0.6f);
			radialBlurFilter.SetCenter(Vector2.op_Implicit(camera.WorldToViewportPoint(boss.head.get_position())));
			yield return (object)null;
		}
		timer2 = 0f;
		while (timer2 < endDuration)
		{
			timer2 += Time.get_deltaTime();
			radialBlurFilter.strength = Mathf.Lerp(maxStrength, 0f, timer2 / 0.3f);
			radialBlurFilter.SetCenter(Vector2.op_Implicit(camera.WorldToViewportPoint(boss.head.get_position())));
			yield return (object)null;
		}
		radialBlurFilter.StopFilter();
	}

	private IEnumerator WaitForTime(float waitTime, Action action)
	{
		yield return (object)new WaitForSeconds(waitTime);
		action.Invoke();
	}
}
