using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryMain : GameSection, StoryDirector.IStoryEventReceiver
{
	private enum UI
	{
		BTN_NEXT,
		SCR_MESSAGE,
		TBL_MESSAGE,
		SPR_NEXT,
		TEX_FADER,
		TEX_LOCATION,
		TEX_IMAGE,
		TEX_MODEL0,
		TEX_MODEL1,
		TEX_MODEL2,
		TEX_MODEL3,
		TEX_EFFECT,
		TEX_FADER_HEADER,
		SPR_BALLOON,
		SPR_TAIL_L,
		SPR_TAIL_R,
		SPR_TAIL_C,
		LBL_NAME,
		LBL_MESSAGE,
		MesBase,
		fukidashibaseflame,
		BTN_SKIP
	}

	private int messageNum;

	private IEnumerator coroutine;

	private Transform lastMessageItem;

	private bool lastMessageFocus;

	private UISprite balloon;

	private Transform tailLeft;

	private Transform tailRight;

	private Transform tailCenter;

	private UILabel nameLabel;

	private UILabel messageLabel;

	private TypewriterEffect typewriter;

	private int initBaseHeight;

	private int initMessageHeight;

	private int messageHeight;

	private Action addMessageFunc;

	private object[] eventData;

	private int? eventID;

	private string requestEndEvent;

	private EventData[] requestEndEventArray;

	private UIButton m_btnNext;

	private UIButton BtnNext
	{
		get
		{
			if (m_btnNext == null)
			{
				Transform ctrl = GetCtrl(UI.BTN_NEXT);
				if (ctrl != null)
				{
					m_btnNext = ctrl.GetComponent<UIButton>();
				}
			}
			return m_btnNext;
		}
	}

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		if (array != null)
		{
			eventID = (int)array[0];
			eventData = new object[2]
			{
				array[1],
				array[2]
			};
			if (array.Length > 3)
			{
				requestEndEvent = (array[3] as string);
				if (string.IsNullOrEmpty(requestEndEvent))
				{
					requestEndEventArray = (array[3] as EventData[]);
				}
			}
		}
		Transform transform = FindCtrl(base.transform, UI.TEX_FADER_HEADER);
		if (transform != null)
		{
			transform.gameObject.SetActive(value: false);
		}
		base.Initialize();
		SyncSpecialDeviceAnctor();
	}

	private void CollectTweens()
	{
	}

	private void SyncSpecialDeviceAnctor()
	{
		if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedModifyStoryAnchor)
		{
			DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
			Transform transform = FindCtrl(base.transform, UI.MesBase);
			if (transform != null)
			{
				UIWidget component = transform.GetComponent<UIWidget>();
				component.leftAnchor.absolute = specialDeviceInfo.StoryMessageBaseAnchor.left;
				component.rightAnchor.absolute = specialDeviceInfo.StoryMessageBaseAnchor.right;
				component.bottomAnchor.absolute = specialDeviceInfo.StoryMessageBaseAnchor.bottom;
				component.topAnchor.absolute = specialDeviceInfo.StoryMessageBaseAnchor.top;
				component.UpdateAnchors();
			}
			Transform transform2 = FindCtrl(base.transform, UI.fukidashibaseflame);
			if (transform2 != null)
			{
				UIWidget component2 = transform2.GetComponent<UIWidget>();
				component2.leftAnchor.absolute = specialDeviceInfo.StoryMainFukidashiBaseFlameAnchor.left;
				component2.rightAnchor.absolute = specialDeviceInfo.StoryMainFukidashiBaseFlameAnchor.right;
				component2.bottomAnchor.absolute = specialDeviceInfo.StoryMainFukidashiBaseFlameAnchor.bottom;
				component2.topAnchor.absolute = specialDeviceInfo.StoryMainFukidashiBaseFlameAnchor.top;
				component2.UpdateAnchors();
			}
			Transform transform3 = FindCtrl(base.transform, UI.TEX_FADER_HEADER);
			if (transform3 != null)
			{
				transform3.gameObject.SetActive(value: true);
			}
		}
	}

	protected override void OnOpen()
	{
		base.OnOpen();
		SetColor(UI.TEX_FADER, new Color(0f, 0f, 0f, 1f));
		SetActive(UI.SPR_NEXT, is_visible: false);
		SetActive(UI.BTN_SKIP, is_visible: false);
		int script_id = eventID ?? 1;
		MonoBehaviourSingleton<StoryDirector>.I.StartScript(script_id, GetComponent<UITexture>(UI.TEX_LOCATION), GetComponent<UITexture>(UI.TEX_EFFECT), this);
	}

	public override void UpdateUI()
	{
	}

	public void AddMessage(string name, string msg, StoryDirector.POS tail_dir, StoryDirector.MSG_TYPE msg_type, StoryDirector.LabelOption labelOption = null)
	{
		StartCoroutine(coroutine = DoAddMessage(name, msg, tail_dir, msg_type, labelOption));
	}

	private IEnumerator DoAddMessage(string name, string msg, StoryDirector.POS tail_dir, StoryDirector.MSG_TYPE msg_type, StoryDirector.LabelOption labelOption = null)
	{
		typewriter = null;
		Transform ctrl = GetCtrl(UI.TBL_MESSAGE);
		string prefab_name = "StoryMessageItem0";
		if (msg_type == StoryDirector.MSG_TYPE.MONOLOGUE)
		{
			prefab_name = "StoryMessageItem1";
		}
		Transform transform = Realizes(prefab_name, ctrl);
		transform.SetSiblingIndex(0);
		UIWidget message_item_w = transform.GetComponent<UIWidget>();
		lastMessageItem = transform;
		balloon = GetComponent<UISprite>(transform, UI.SPR_BALLOON);
		tailLeft = FindCtrl(transform, UI.SPR_TAIL_L);
		tailRight = FindCtrl(transform, UI.SPR_TAIL_R);
		tailCenter = FindCtrl(transform, UI.SPR_TAIL_C);
		nameLabel = GetComponent<UILabel>(transform, UI.LBL_NAME);
		messageLabel = GetComponent<UILabel>(transform, UI.LBL_MESSAGE);
		initBaseHeight = message_item_w.height;
		messageLabel.text = " ";
		initMessageHeight = messageLabel.height;
		messageHeight = initMessageHeight;
		if (labelOption != null)
		{
			messageLabel.supportEncoding = labelOption.BBCode;
			messageLabel.alignment = labelOption.Alignment;
			messageLabel.fontSize = labelOption.FontSize;
		}
		string final = "";
		if (messageLabel.Wrap(msg, out final))
		{
			msg = WordWrap.Convert(messageLabel, msg);
		}
		SetLastMessageFocus(is_focus: true);
		SetMessageDragEnabled(is_enable: false);
		if (msg_type == StoryDirector.MSG_TYPE.NORMAL)
		{
			if (tailLeft != null && tail_dir != StoryDirector.POS.LEFT)
			{
				tailLeft.gameObject.SetActive(value: false);
			}
			if (tailRight != null && tail_dir != StoryDirector.POS.RIGHT)
			{
				tailRight.gameObject.SetActive(value: false);
			}
			if (tailCenter != null && tail_dir != StoryDirector.POS.CENTER)
			{
				tailCenter.gameObject.SetActive(value: false);
			}
			nameLabel.text = name;
		}
		UIWidget next_arrow_w = GetComponent<UIWidget>(UI.SPR_NEXT);
		next_arrow_w.gameObject.SetActive(value: false);
		List<UITweener> tweens = new List<UITweener>();
		transform.GetComponentsInChildren(tweens);
		while (tweens.Find((UITweener o) => o.enabled) != null)
		{
			yield return null;
		}
		SoundManager.PlaySystemSE(SoundID.UISE.POPUP);
		messageLabel.text = msg;
		typewriter = messageLabel.gameObject.AddComponent<TypewriterEffect>();
		typewriter.charsPerSecond = StoryDirector.SPEED_TYPEWRITER;
		typewriter.ResetToBeginning();
		while (typewriter.isActive)
		{
			yield return null;
		}
		yield return null;
		next_arrow_w.gameObject.SetActive(value: true);
		Vector3[] worldCorners = message_item_w.worldCorners;
		Vector3[] worldCorners2 = next_arrow_w.worldCorners;
		next_arrow_w.SetAnchor((Transform)null);
		next_arrow_w.cachedTransform.position = new Vector3((worldCorners2[0].x + worldCorners2[2].x) * 0.5f, worldCorners[0].y - (worldCorners2[1].y - worldCorners2[0].y) * 0.5f, worldCorners2[0].z);
		SetMessageDragEnabled(is_enable: true);
		UnityEngine.Object.Destroy(typewriter);
		typewriter = null;
		messageHeight = 0;
		messageNum++;
		coroutine = null;
	}

	private void OnQuery_NEXT()
	{
		if (typewriter != null)
		{
			typewriter.charsPerSecond = 1000;
		}
		if (coroutine == null)
		{
			MonoBehaviourSingleton<StoryDirector>.I.OnNextMessage();
		}
	}

	private void OnQuery_SKIP()
	{
		GameSection.StopEvent();
		if (eventID == 80000001)
		{
			if (LoungeMatchingManager.IsValidInLounge())
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Lounge");
			}
			else
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Home");
			}
		}
		else if (!string.IsNullOrEmpty(requestEndEvent))
		{
			EventData[] autoEvents = new EventData[1]
			{
				new EventData(requestEndEvent, null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
		}
		else if (requestEndEventArray != null)
		{
			MonoBehaviourSingleton<StoryDirector>.I.HideBG();
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(requestEndEventArray);
		}
		else if (MonoBehaviourSingleton<InGameManager>.I.questTransferInfo != null)
		{
			MonoBehaviourSingleton<AppMain>.I.mainCamera.gameObject.SetActive(value: false);
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", base.gameObject, "INTERVAL");
		}
		else
		{
			MonoBehaviourSingleton<StoryDirector>.I.HideBG();
			string goingHomeEvent = GameSection.GetGoingHomeEvent();
			EventData[] autoEvents2 = new EventData[2]
			{
				new EventData(goingHomeEvent, null),
				new EventData("DELIVERY_CLEAR_REWARD", eventData)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents2);
		}
	}

	private void LateUpdate()
	{
		if (lastMessageItem != null)
		{
			if (messageHeight > 0)
			{
				if (messageHeight < messageLabel.height)
				{
					messageHeight = messageLabel.height;
					lastMessageItem.GetComponent<UIWidget>().height = initBaseHeight + messageHeight - initMessageHeight;
				}
				GetComponent<UITable>(UI.TBL_MESSAGE).Reposition();
				GetComponent<UIScrollView>(UI.SCR_MESSAGE).ResetPosition();
			}
			else if (lastMessageFocus)
			{
				Vector3[] worldCorners = GetComponent<UIPanel>(UI.SCR_MESSAGE).worldCorners;
				if (UIUtility.GetWorldTopY(balloon) > worldCorners[1].y)
				{
					SetLastMessageFocus(is_focus: false);
				}
			}
		}
		if (addMessageFunc != null)
		{
			addMessageFunc();
			addMessageFunc = null;
		}
	}

	private void SetLastMessageFocus(bool is_focus)
	{
		lastMessageFocus = is_focus;
		float alpha = is_focus ? 0.6f : 1f;
		Transform ctrl = GetCtrl(UI.TBL_MESSAGE);
		int i = 1;
		for (int childCount = ctrl.childCount; i < childCount; i++)
		{
			GetComponent<UISprite>(ctrl.GetChild(i), UI.SPR_BALLOON).alpha = alpha;
		}
	}

	private void SetMessageDragEnabled(bool is_enable)
	{
		Transform ctrl = GetCtrl(UI.TBL_MESSAGE);
		int i = 0;
		for (int childCount = ctrl.childCount; i < childCount; i++)
		{
			ctrl.GetChild(i).GetComponent<UIDragScrollView>().enabled = is_enable;
		}
	}

	void StoryDirector.IStoryEventReceiver.FadeIn()
	{
		if (!(this == null))
		{
			TweenColor.Begin(GetCtrl(UI.TEX_FADER).gameObject, 1f, new Color(0f, 0f, 0f, 0f));
		}
	}

	void StoryDirector.IStoryEventReceiver.FadeOut(Color fadeout_color)
	{
		if (!(this == null))
		{
			TweenColor.Begin(GetCtrl(UI.TEX_FADER).gameObject, 1f, fadeout_color);
		}
	}

	void StoryDirector.IStoryEventReceiver.FadeIn(float fade_time)
	{
		if (!(this == null))
		{
			TweenColor.Begin(GetCtrl(UI.TEX_FADER).gameObject, fade_time, new Color(0f, 0f, 0f, 0f));
		}
	}

	void StoryDirector.IStoryEventReceiver.FadeOut(Color fadeout_color, float fade_time)
	{
		if (!(this == null))
		{
			TweenColor.Begin(GetCtrl(UI.TEX_FADER).gameObject, fade_time, fadeout_color);
		}
	}

	void StoryDirector.IStoryEventReceiver.AddMessage(string name, string msg, StoryDirector.POS tail_dir, StoryDirector.MSG_TYPE msg_type, StoryDirector.LabelOption labelOption)
	{
		if (!(this == null))
		{
			addMessageFunc = delegate
			{
				AddMessage(name, msg, tail_dir, msg_type, labelOption);
			};
		}
	}

	UITexture StoryDirector.IStoryEventReceiver.GetModelUITexture(int id)
	{
		if (this == null)
		{
			return null;
		}
		return GetComponent<UITexture>((UI)(7 + id));
	}

	void StoryDirector.IStoryEventReceiver.EndLoadFirstBG()
	{
		if (!(this == null))
		{
			SetActive(UI.BTN_SKIP, is_visible: true);
		}
	}

	void StoryDirector.IStoryEventReceiver.EndStory()
	{
		if (!(this == null))
		{
			DispatchEvent("SKIP");
		}
	}

	protected override void OnCloseStart()
	{
		SoundManager.StopVoice(0u, 6);
		base.OnCloseStart();
	}
}
