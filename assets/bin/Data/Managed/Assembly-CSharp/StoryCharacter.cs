using System;
using UnityEngine;

public class StoryCharacter : MonoBehaviour
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

	public static StoryCharacter Initialize(int id, UITexture ui_tex, string _name, string _dir, string idle_anim, int layer = -1)
	{
		NPCTable.NPCData nPCData = Singleton<NPCTable>.I.GetNPCData(_name);
		if (nPCData == null)
		{
			return null;
		}
		UIRenderTexture uIRenderTexture = UIRenderTexture.Get(ui_tex, -1f, true, layer);
		uIRenderTexture.Disable();
		uIRenderTexture.nearClipPlane = 1f;
		uIRenderTexture.farClipPlane = 100f;
		Transform transform = Utility.CreateGameObject("StoryModel", uIRenderTexture.modelTransform, uIRenderTexture.renderLayer);
		StoryCharacter storyCharacter = transform.gameObject.AddComponent<StoryCharacter>();
		storyCharacter.model = transform;
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
		ModelLoaderBase modelLoaderBase = nPCData.LoadModel(transform.gameObject, false, false, storyCharacter.OnModelLoadComplete, false);
		storyCharacter.npcLoader = (modelLoaderBase as NPCLoader);
		storyCharacter.CollectTween(ui_tex.transform);
		return storyCharacter;
	}

	public void SetStandPosition(string _dir, bool doesSetImmidiate = false)
	{
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
			model.localPosition = basePos;
		}
		animRot.Set(Quaternion.Euler(baseRot));
	}

	public void SetPosition(float x, float y, float time)
	{
		basePos = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.leftStandPos;
		Vector3 end_value = new Vector3(x, y, basePos.z);
		animPos.Set(time, end_value, null, default(Vector3), null);
		animPos.Play();
	}

	public void SetModelScale(Vector3 scale)
	{
		model.localScale = scale;
	}

	private void CollectTween(Transform t_ui_tex)
	{
		if (!((UnityEngine.Object)t_ui_tex == (UnityEngine.Object)null))
		{
			tweenAnimations = t_ui_tex.GetComponentsInChildren<UITweenCtrl>();
		}
	}

	public void PlayTween(EaseDir type, bool forward = true, EventDelegate.Callback callback = null)
	{
		if (tweenAnimations != null)
		{
			UITweenCtrl uITweenCtrl = Array.Find(tweenAnimations, (UITweenCtrl t) => t.id == (int)type);
			if ((UnityEngine.Object)uITweenCtrl != (UnityEngine.Object)null)
			{
				uITweenCtrl.Reset();
				uITweenCtrl.Play(forward, callback);
			}
		}
	}

	private void OnModelLoadComplete(Animator animator)
	{
		if ((UnityEngine.Object)animator != (UnityEngine.Object)null)
		{
			playerAnimCtrl = PlayerAnimCtrl.Get(animator, idleAnim, null, null, null);
		}
		isLoading = false;
	}

	private void Update()
	{
		if (!((UnityEngine.Object)model == (UnityEngine.Object)null))
		{
			model.localPosition = animPos.Update();
			model.localRotation = Quaternion.Euler(new Vector3(-6f, 0f, 0f)) * animRot.Update();
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
		float charaFadeTime = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.charaFadeTime;
		renderTex.FadeOutDisable(charaFadeTime);
		float charaFadeMoveX = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.charaFadeMoveX;
		Vector3 vector = Vector3.zero;
		Vector3 end_value = vector;
		bool flag = false;
		if (dir == StoryDirector.POS.LEFT)
		{
			vector = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.leftStandPos;
			end_value = vector;
			end_value.x -= charaFadeMoveX;
			flag = true;
		}
		else if (dir == StoryDirector.POS.RIGHT)
		{
			vector = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.leftStandPos;
			vector.x = 0f - vector.x;
			end_value = vector;
			end_value.x += charaFadeMoveX;
			flag = true;
		}
		if (flag)
		{
			animPos.Set(charaFadeTime, vector, end_value, null, default(Vector3), null);
			animPos.Play();
		}
	}

	public void RotateFront(float time = 0.5f)
	{
		RotateAngle(0f, time);
	}

	public void RotateDefault(float time = 0.5f)
	{
		animRot.Set(time, model.localRotation, Quaternion.Euler(baseRot), null, default(Quaternion), null);
		animRot.Play();
	}

	public void RotateAngle(float angle, float time = 0.5f)
	{
		animRot.Set(time, model.localRotation, Quaternion.Euler(new Vector3(0f, 180f + angle, 0f)), null, default(Quaternion), null);
		animRot.Play();
	}

	public void RequestPose(string pose_name)
	{
		if (!((UnityEngine.Object)playerAnimCtrl == (UnityEngine.Object)null))
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
		if (!((UnityEngine.Object)npcLoader == (UnityEngine.Object)null) && !((UnityEngine.Object)npcLoader.facial == (UnityEngine.Object)null))
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
