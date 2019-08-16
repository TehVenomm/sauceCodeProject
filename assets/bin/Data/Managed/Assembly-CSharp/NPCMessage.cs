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
		if (section_data == null || (section_data != null && section != null && section.name == section_data.sectionName))
		{
			return;
		}
		if (!is_open)
		{
			Close();
			return;
		}
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
		DeleteModel();
		targetTex = UI.TEX_NPC;
		InitRenderTexture(targetTex, 45f);
		model = Utility.CreateGameObject("NPC", GetRenderTextureModelTransform(targetTex), GetRenderTextureLayer(targetTex));
		npcData = Singleton<NPCTable>.I.GetNPCData(message.npc);
		isLoading = true;
		npcData.LoadModel(model.get_gameObject(), need_shadow: false, enable_light_probe: false, OnModelLoadComplete, useSpecialModel: false);
	}

	private void OnModelLoadComplete(Animator animator)
	{
		if (message.has_voice)
		{
			this.StartCoroutine(DoCacheVoice(delegate
			{
				OpenMessage(animator);
			}));
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
				load_queue.CacheVoice(message.voice_id);
				yield return load_queue.Wait();
			}
		}
		on_complete();
	}

	private void OpenMessage(Animator animator)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		isLoading = false;
		Open();
		if (needUpdateAnchors)
		{
			needUpdateAnchors = false;
			UpdateAnchors();
		}
		model.set_localPosition(message.pos);
		model.set_localEulerAngles(message.rot);
		if (animator != null)
		{
			PlayerAnimCtrl.Get(animator, PlayerAnimCtrl.StringToEnum(npcData.anim));
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
			SoundManager.PlayVoice(message.voice_id);
		}
		if (targetTex == UI.TEX_QUEST_NPC)
		{
			InitUITweener<TweenColor>((Enum)UI.TEX_QUEST_NPC, is_enable: true, (EventDelegate.Callback)DeleteModel);
			InitUITweener<TweenColor>((Enum)UI.SPR_MESSAGE, is_enable: true, (EventDelegate.Callback)null);
		}
	}

	private void DeleteModel()
	{
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
