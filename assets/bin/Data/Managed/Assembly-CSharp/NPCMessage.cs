using System;
using System.Collections;
using UnityEngine;

public class NPCMessage : UIBehaviour
{
	private enum UI
	{
		TEX_NPC,
		TEX_QUEST_NPC,
		LBL_MESSAGE,
		LBL_NAME,
		SPR_MESSAGE
	}

	private NPCMessageTable.Section section;

	private NPCMessageTable.Message message;

	private NPCTable.NPCData npcData;

	private Transform model;

	private bool isLoading;

	private UI targetTex;

	private Transform voice;

	private bool isShowMessage = true;

	private bool needUpdateAnchors;

	private void OnEnable()
	{
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
		}
	}

	private void OnDisable()
	{
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
	}

	private void OnScreenRotate(bool is_portrait)
	{
		needUpdateAnchors = true;
	}

	public void UpdateMessage(GameSceneTables.SectionData section_data, bool is_open)
	{
		if (!(section_data == (GameSceneTables.SectionData)null) && (!(section_data != (GameSceneTables.SectionData)null) || section == null || !(section.name == section_data.sectionName)))
		{
			if (!is_open)
			{
				Close(UITransition.TYPE.CLOSE);
			}
			else
			{
				int baseDepth = 100;
				section = Singleton<NPCMessageTable>.I.GetSection(section_data.sectionName);
				if (section == null)
				{
					HomeNPCTalk homeNPCTalk = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() as HomeNPCTalk;
					if (homeNPCTalk != null)
					{
						section = Singleton<NPCMessageTable>.I.GetSection($"{section_data.sectionName}_{homeNPCTalk.npcID:D3}");
						baseDepth = homeNPCTalk.baseDepth + 1;
					}
					if (section == null)
					{
						return;
					}
				}
				base.baseDepth = baseDepth;
				message = section.GetNPCMessage();
				LoadModel();
			}
		}
	}

	protected override void OnClose()
	{
		DeleteModel();
		section = null;
	}

	public override void OnNotify(GameSection.NOTIFY_FLAG flags)
	{
		base.OnNotify(flags);
		if ((flags & GameSection.NOTIFY_FLAG.PRETREAT_SCENE) != (GameSection.NOTIFY_FLAG)0L && isLoading)
		{
			DeleteModel();
		}
	}

	private void LoadModel()
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		DeleteModel();
		targetTex = UI.TEX_NPC;
		InitRenderTexture(targetTex, 45f, false);
		model = Utility.CreateGameObject("NPC", GetRenderTextureModelTransform(targetTex), GetRenderTextureLayer(targetTex));
		npcData = Singleton<NPCTable>.I.GetNPCData(message.npc);
		isLoading = true;
		npcData.LoadModel(model.get_gameObject(), false, false, OnModelLoadComplete, false);
	}

	private unsafe void OnModelLoadComplete(Animator animator)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		if (message.has_voice)
		{
			_003COnModelLoadComplete_003Ec__AnonStorey7E0 _003COnModelLoadComplete_003Ec__AnonStorey7E;
			this.StartCoroutine(DoCacheVoice(new Action((object)_003COnModelLoadComplete_003Ec__AnonStorey7E, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
		}
		else
		{
			OpenMessage(animator);
		}
	}

	private IEnumerator DoCacheVoice(Action on_complete)
	{
		if (message != null && message.has_voice && model != null)
		{
			NPCLoader loader = model.GetComponent<NPCLoader>();
			if (loader != null)
			{
				LoadingQueue load_queue = new LoadingQueue(loader);
				load_queue.CacheVoice(message.voice_id, null);
				yield return (object)load_queue.Wait();
			}
		}
		on_complete.Invoke();
	}

	private void OpenMessage(Animator animator)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		isLoading = false;
		Open(UITransition.TYPE.OPEN);
		if (needUpdateAnchors)
		{
			needUpdateAnchors = false;
			UpdateAnchors();
		}
		model.set_localPosition(message.pos);
		model.set_localEulerAngles(message.rot);
		if (animator != null)
		{
			PlayerAnimCtrl.Get(animator, PlayerAnimCtrl.StringToEnum(npcData.anim), null, null, null);
		}
		EnableRenderTexture(targetTex);
		string replaceText = message.GetReplaceText();
		SetColor((Enum)UI.SPR_MESSAGE, (!isShowMessage || string.IsNullOrEmpty(replaceText)) ? Color.get_clear() : Color.get_white());
		isShowMessage = true;
		SetLabelText((Enum)UI.LBL_MESSAGE, replaceText);
		string displayName = npcData.displayName;
		SetLabelText((Enum)UI.LBL_NAME, displayName);
		if (message.has_voice)
		{
			SoundManager.PlayVoice(message.voice_id, 1f, 0u, null, null);
		}
		if (targetTex == UI.TEX_QUEST_NPC)
		{
			InitUITweener<TweenColor>((Enum)UI.TEX_QUEST_NPC, true, (EventDelegate.Callback)DeleteModel);
			InitUITweener<TweenColor>((Enum)UI.SPR_MESSAGE, true, (EventDelegate.Callback)null);
		}
	}

	private void DeleteModel()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		DeleteRenderTexture((Enum)targetTex);
		if (model != null)
		{
			Object.DestroyImmediate(model.get_gameObject());
			model = null;
		}
		isLoading = false;
	}

	public void HideMessage()
	{
		isShowMessage = false;
	}
}
