using System;
using UnityEngine;

public class StoryCharacter
{
	public enum EaseDir
	{
		LEFT,
		RIGHT
	}

	private UIRenderTexture renderTex;

	private NPCLoader npcLoader;

	private Vector3 basePos;

	private Vector3 baseRot;

	private Vector3Interpolator animPos = new Vector3Interpolator();

	private QuaternionInterpolator animRot = new QuaternionInterpolator();

	private Transform lookTransform;

	private PLCA idleAnim;

	private UITweenCtrl[] tweenAnimations;

	private PlayerAnimCtrl playerAnimCtrl;

	public bool isLoading
	{
		get;
		private set;
	}

	public bool isMoving => animPos.IsPlaying() || animRot.IsPlaying();

	public int id
	{
		get;
		private set;
	}

	public string charaName
	{
		get;
		private set;
	}

	public string displayName => (!string.IsNullOrEmpty(aliasName)) ? aliasName : charaName;

	public string aliasName
	{
		get;
		private set;
	}

	public StoryDirector.POS dir
	{
		get;
		private set;
	}

	public UITexture uiTex
	{
		get;
		private set;
	}

	public Transform model
	{
		get;
		private set;
	}

	public StoryCharacter()
		: this()
	{
	}

	public static StoryCharacter Initialize(int id, UITexture ui_tex, string _name, string _dir, string idle_anim, int layer = -1)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Expected O, but got Unknown
		NPCTable.NPCData nPCData = Singleton<NPCTable>.I.GetNPCData(_name);
		if (nPCData == null)
		{
			return null;
		}
		UIRenderTexture uIRenderTexture = UIRenderTexture.Get(ui_tex, -1f, true, layer);
		uIRenderTexture.Disable();
		uIRenderTexture.nearClipPlane = 1f;
		uIRenderTexture.farClipPlane = 100f;
		Transform val = Utility.CreateGameObject("StoryModel", uIRenderTexture.modelTransform, uIRenderTexture.renderLayer);
		StoryCharacter storyCharacter = val.get_gameObject().AddComponent<StoryCharacter>();
		storyCharacter.model = val;
		storyCharacter.id = id;
		storyCharacter.renderTex = uIRenderTexture;
		storyCharacter.uiTex = ui_tex;
		storyCharacter.charaName = _name;
		storyCharacter.aliasName = string.Empty;
		storyCharacter.SetStandPosition(_dir, false);
		if (string.IsNullOrEmpty(idle_anim))
		{
			storyCharacter.idleAnim = PlayerAnimCtrl.StringToEnum(nPCData.anim);
		}
		else
		{
			storyCharacter.idleAnim = PlayerAnimCtrl.StringToEnum(idle_anim);
		}
		storyCharacter.isLoading = true;
		ModelLoaderBase modelLoaderBase = nPCData.LoadModel(val.get_gameObject(), false, false, storyCharacter.OnModelLoadComplete, false);
		storyCharacter.npcLoader = (modelLoaderBase as NPCLoader);
		storyCharacter.CollectTween(ui_tex.get_transform());
		return storyCharacter;
	}

	public void SetStandPosition(string _dir, bool doesSetImmidiate = false)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		basePos = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.leftStandPos;
		baseRot = new Vector3(0f, MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.leftStandRot, 0f);
		dir = StoryDirector.POS.LEFT;
		switch (_dir)
		{
		case "R":
		case "UR":
			basePos.x = 0f - basePos.x;
			baseRot.y = 0f - baseRot.y;
			dir = StoryDirector.POS.RIGHT;
			break;
		case "C":
		case "UC":
			basePos.x = 0f;
			baseRot.y = 180f;
			dir = StoryDirector.POS.CENTER;
			break;
		}
		if (_dir == "UR" || _dir == "UC" || _dir == "UL")
		{
			basePos.y += MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.leftStandUpOffset;
		}
		animPos.Set(basePos);
		if (doesSetImmidiate)
		{
			model.set_localPosition(basePos);
		}
		animRot.Set(Quaternion.Euler(baseRot));
	}

	public void SetPosition(float x, float y, float time)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		basePos = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.leftStandPos;
		Vector3 end_value = default(Vector3);
		end_value._002Ector(x, y, basePos.z);
		((InterpolatorBase<Vector3>)animPos).Set(time, end_value, null, default(Vector3), null);
		animPos.Play();
	}

	public void SetModelScale(Vector3 scale)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		model.set_localScale(scale);
	}

	private void CollectTween(Transform t_ui_tex)
	{
		if (!(t_ui_tex == null))
		{
			tweenAnimations = t_ui_tex.GetComponentsInChildren<UITweenCtrl>();
		}
	}

	public void PlayTween(EaseDir type, bool forward = true, EventDelegate.Callback callback = null)
	{
		if (tweenAnimations != null)
		{
			UITweenCtrl uITweenCtrl = Array.Find(tweenAnimations, (UITweenCtrl t) => t.id == (int)type);
			if (uITweenCtrl != null)
			{
				uITweenCtrl.Reset();
				uITweenCtrl.Play(forward, callback);
			}
		}
	}

	private void OnModelLoadComplete(Animator animator)
	{
		if (animator != null)
		{
			playerAnimCtrl = PlayerAnimCtrl.Get(animator, idleAnim, null, null, null);
		}
		isLoading = false;
	}

	private void Update()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		if (!(model == null))
		{
			model.set_localPosition(animPos.Update());
			model.set_localRotation(Quaternion.Euler(new Vector3(-6f, 0f, 0f)) * animRot.Update());
		}
	}

	public void Show()
	{
		renderTex.Enable(0.25f);
	}

	public void Hide()
	{
		renderTex.Disable();
	}

	public bool IsShow()
	{
		return renderTex.enableTexture;
	}

	public void FadeIn()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		float charaFadeTime = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.charaFadeTime;
		renderTex.Enable(charaFadeTime);
		float charaFadeMoveX = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.charaFadeMoveX;
		Vector3 leftStandPos;
		Vector3 begin_value;
		if (dir == StoryDirector.POS.LEFT)
		{
			leftStandPos = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.leftStandPos;
			begin_value = leftStandPos;
			begin_value.x -= charaFadeMoveX;
		}
		else if (dir == StoryDirector.POS.RIGHT)
		{
			leftStandPos = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.leftStandPos;
			leftStandPos.x = 0f - leftStandPos.x;
			begin_value = leftStandPos;
			begin_value.x += charaFadeMoveX;
		}
		else
		{
			leftStandPos = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.leftStandPos;
			leftStandPos.x = 0f;
			begin_value = leftStandPos;
		}
		animPos.Set(charaFadeTime, begin_value, leftStandPos, null, default(Vector3), null);
		animPos.Play();
	}

	public void FadeOut()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		float charaFadeTime = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.charaFadeTime;
		renderTex.FadeOutDisable(charaFadeTime);
		float charaFadeMoveX = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.charaFadeMoveX;
		Vector3 val = Vector3.get_zero();
		Vector3 end_value = val;
		bool flag = false;
		if (dir == StoryDirector.POS.LEFT)
		{
			val = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.leftStandPos;
			end_value = val;
			end_value.x -= charaFadeMoveX;
			flag = true;
		}
		else if (dir == StoryDirector.POS.RIGHT)
		{
			val = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.leftStandPos;
			val.x = 0f - val.x;
			end_value = val;
			end_value.x += charaFadeMoveX;
			flag = true;
		}
		if (flag)
		{
			animPos.Set(charaFadeTime, val, end_value, null, default(Vector3), null);
			animPos.Play();
		}
	}

	public void RotateFront(float time = 0.5f)
	{
		RotateAngle(0f, time);
	}

	public void RotateDefault(float time = 0.5f)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		animRot.Set(time, model.get_localRotation(), Quaternion.Euler(baseRot), null, default(Quaternion), null);
		animRot.Play();
	}

	public void RotateAngle(float angle, float time = 0.5f)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		animRot.Set(time, model.get_localRotation(), Quaternion.Euler(new Vector3(0f, 180f + angle, 0f)), null, default(Quaternion), null);
		animRot.Play();
	}

	public void RequestPose(string pose_name)
	{
		if (!(playerAnimCtrl == null))
		{
			try
			{
				PLCA anim = (PLCA)(int)Enum.Parse(typeof(PLCA), pose_name);
				playerAnimCtrl.Play(anim, false);
			}
			catch
			{
				Log.Error(LOG.GAMESCENE, "不正なモ\u30fcションコマンド：" + pose_name);
			}
		}
	}

	public void RequestFace(string eye_type, string mouth_type)
	{
		if (!(npcLoader == null) && !(npcLoader.facial == null))
		{
			NPCFacial facial = npcLoader.facial;
			NPCFacial.TYPE eyeType = facial.eyeType;
			NPCFacial.TYPE mouthType = facial.mouthType;
			if (!string.IsNullOrEmpty(eye_type))
			{
				try
				{
					NPCFacial.TYPE tYPE2 = facial.eyeType = (NPCFacial.TYPE)(int)Enum.Parse(typeof(NPCFacial.TYPE), eye_type);
				}
				catch
				{
					Log.Error(LOG.GAMESCENE, "不正な表情(目)：" + eye_type);
				}
			}
			if (!string.IsNullOrEmpty(mouth_type))
			{
				try
				{
					NPCFacial.TYPE tYPE4 = facial.mouthType = (NPCFacial.TYPE)(int)Enum.Parse(typeof(NPCFacial.TYPE), mouth_type);
				}
				catch
				{
					Log.Error(LOG.GAMESCENE, "不正な表情(口)：" + mouth_type);
				}
			}
			if (eyeType != facial.eyeType || mouthType != facial.mouthType)
			{
				if (facial.eyeType != 0 || facial.mouthType != 0)
				{
					facial.enableAnim = false;
				}
				else
				{
					facial.enableAnim = true;
				}
			}
		}
	}

	public void SetAliasName(string _name)
	{
		aliasName = _name;
	}
}
