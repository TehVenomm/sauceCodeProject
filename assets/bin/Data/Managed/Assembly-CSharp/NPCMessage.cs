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
					if ((UnityEngine.Object)homeNPCTalk != (UnityEngine.Object)null)
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
		DeleteModel();
		targetTex = UI.TEX_NPC;
		InitRenderTexture(targetTex, 45f, false);
		model = Utility.CreateGameObject("NPC", GetRenderTextureModelTransform(targetTex), GetRenderTextureLayer(targetTex));
		npcData = Singleton<NPCTable>.I.GetNPCData(message.npc);
		isLoading = true;
		npcData.LoadModel(model.gameObject, false, false, OnModelLoadComplete, false);
	}

	private void OnModelLoadComplete(Animator animator)
	{
		if (message.has_voice)
		{
			StartCoroutine(DoCacheVoice(delegate
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
		if (message != null && message.has_voice && (UnityEngine.Object)model != (UnityEngine.Object)null)
		{
			NPCLoader loader = model.GetComponent<NPCLoader>();
			if ((UnityEngine.Object)loader != (UnityEngine.Object)null)
			{
				LoadingQueue load_queue = new LoadingQueue(loader);
				load_queue.CacheVoice(message.voice_id, null);
				yield return (object)load_queue.Wait();
			}
		}
		on_complete();
	}

	private void OpenMessage(Animator animator)
	{
		isLoading = false;
		Open(UITransition.TYPE.OPEN);
		if (needUpdateAnchors)
		{
			needUpdateAnchors = false;
			UpdateAnchors();
		}
		model.localPosition = message.pos;
		model.localEulerAngles = message.rot;
		if ((UnityEngine.Object)animator != (UnityEngine.Object)null)
		{
			PlayerAnimCtrl.Get(animator, PlayerAnimCtrl.StringToEnum(npcData.anim), null, null, null);
		}
		EnableRenderTexture(targetTex);
		string replaceText = message.GetReplaceText();
		SetColor(UI.SPR_MESSAGE, (!isShowMessage || string.IsNullOrEmpty(replaceText)) ? Color.clear : Color.white);
		isShowMessage = true;
		SetLabelText(UI.LBL_MESSAGE, replaceText);
		string displayName = npcData.displayName;
		SetLabelText(UI.LBL_NAME, displayName);
		if (message.has_voice)
		{
			SoundManager.PlayVoice(message.voice_id, 1f, 0u, null, null);
		}
		if (targetTex == UI.TEX_QUEST_NPC)
		{
			InitUITweener<TweenColor>(UI.TEX_QUEST_NPC, true, DeleteModel);
			InitUITweener<TweenColor>(UI.SPR_MESSAGE, true, null);
		}
	}

	private void DeleteModel()
	{
		DeleteRenderTexture(targetTex);
		if ((UnityEngine.Object)model != (UnityEngine.Object)null)
		{
			UnityEngine.Object.DestroyImmediate(model.gameObject);
			model = null;
		}
		isLoading = false;
	}

	public void HideMessage()
	{
		isShowMessage = false;
	}
}
