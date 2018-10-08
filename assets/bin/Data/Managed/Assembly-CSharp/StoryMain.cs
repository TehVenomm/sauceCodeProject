using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
		SPR_BALLOON,
		SPR_TAIL_L,
		SPR_TAIL_R,
		SPR_TAIL_C,
		LBL_NAME,
		LBL_MESSAGE,
		BTN_SKIP
	}

	[CompilerGenerated]
	private sealed class AddMessage_003Ec__AnonStorey3B8
	{
		internal string name;

		internal string msg;

		internal StoryDirector.POS tail_dir;

		internal StoryDirector.MSG_TYPE msg_type;

		internal StoryDirector.LabelOption labelOption;

		internal StoryMain _003C_003Ef__this;

		internal void _003C_003Em__369()
		{
			_003C_003Ef__this.AddMessage(name, msg, tail_dir, msg_type, labelOption);
		}
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
			if ((UnityEngine.Object)m_btnNext == (UnityEngine.Object)null)
			{
				Transform ctrl = GetCtrl(UI.BTN_NEXT);
				if ((UnityEngine.Object)ctrl != (UnityEngine.Object)null)
				{
					m_btnNext = ctrl.GetComponent<UIButton>();
				}
			}
			return m_btnNext;
		}
	}

	void StoryDirector.IStoryEventReceiver.FadeIn()
	{
		if (!((UnityEngine.Object)this == (UnityEngine.Object)null))
		{
			TweenColor.Begin(GetCtrl(UI.TEX_FADER).gameObject, 1f, new Color(0f, 0f, 0f, 0f));
		}
	}

	void StoryDirector.IStoryEventReceiver.FadeOut(Color fadeout_color)
	{
		if (!((UnityEngine.Object)this == (UnityEngine.Object)null))
		{
			TweenColor.Begin(GetCtrl(UI.TEX_FADER).gameObject, 1f, fadeout_color);
		}
	}

	void StoryDirector.IStoryEventReceiver.AddMessage(string name, string msg, StoryDirector.POS tail_dir, StoryDirector.MSG_TYPE msg_type, StoryDirector.LabelOption labelOption = null)
	{
		if (!((UnityEngine.Object)this == (UnityEngine.Object)null))
		{
			addMessageFunc = delegate
			{
				AddMessage(name, msg, tail_dir, msg_type, labelOption);
			};
		}
	}

	UITexture StoryDirector.IStoryEventReceiver.GetModelUITexture(int id)
	{
		if ((UnityEngine.Object)this == (UnityEngine.Object)null)
		{
			return null;
		}
		return GetComponent<UITexture>((UI)(7 + id));
	}

	void StoryDirector.IStoryEventReceiver.EndLoadFirstBG()
	{
		if (!((UnityEngine.Object)this == (UnityEngine.Object)null))
		{
			SetActive(UI.BTN_SKIP, true);
		}
	}

	void StoryDirector.IStoryEventReceiver.EndStory()
	{
		if (!((UnityEngine.Object)this == (UnityEngine.Object)null))
		{
			DispatchEvent("SKIP", null);
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
		base.Initialize();
	}

	private void CollectTweens()
	{
	}

	protected override void OnOpen()
	{
		base.OnOpen();
		SetColor(UI.TEX_FADER, new Color(0f, 0f, 0f, 1f));
		SetActive(UI.SPR_NEXT, false);
		SetActive(UI.BTN_SKIP, false);
		int? nullable = eventID;
		int script_id = (!nullable.HasValue) ? 1 : nullable.Value;
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
		Transform table_t = GetCtrl(UI.TBL_MESSAGE);
		string prefab_name = "StoryMessageItem0";
		if (msg_type == StoryDirector.MSG_TYPE.MONOLOGUE)
		{
			prefab_name = "StoryMessageItem1";
		}
		Transform message_item_t = Realizes(prefab_name, table_t, true);
		message_item_t.SetSiblingIndex(0);
		UIWidget message_item_w = message_item_t.GetComponent<UIWidget>();
		lastMessageItem = message_item_t;
		balloon = GetComponent<UISprite>(message_item_t, UI.SPR_BALLOON);
		tailLeft = FindCtrl(message_item_t, UI.SPR_TAIL_L);
		tailRight = FindCtrl(message_item_t, UI.SPR_TAIL_R);
		tailCenter = FindCtrl(message_item_t, UI.SPR_TAIL_C);
		nameLabel = GetComponent<UILabel>(message_item_t, UI.LBL_NAME);
		messageLabel = GetComponent<UILabel>(message_item_t, UI.LBL_MESSAGE);
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
		string temp = string.Empty;
		if (messageLabel.Wrap(msg, out temp))
		{
			msg = WordWrap.Convert(messageLabel, msg);
		}
		SetLastMessageFocus(true);
		SetMessageDragEnabled(false);
		if (msg_type == StoryDirector.MSG_TYPE.NORMAL)
		{
			if ((UnityEngine.Object)tailLeft != (UnityEngine.Object)null && tail_dir != StoryDirector.POS.LEFT)
			{
				tailLeft.gameObject.SetActive(false);
			}
			if ((UnityEngine.Object)tailRight != (UnityEngine.Object)null && tail_dir != StoryDirector.POS.RIGHT)
			{
				tailRight.gameObject.SetActive(false);
			}
			if ((UnityEngine.Object)tailCenter != (UnityEngine.Object)null && tail_dir != StoryDirector.POS.CENTER)
			{
				tailCenter.gameObject.SetActive(false);
			}
			nameLabel.text = name;
		}
		UIWidget next_arrow_w = GetComponent<UIWidget>(UI.SPR_NEXT);
		next_arrow_w.gameObject.SetActive(false);
		List<UITweener> tweens = new List<UITweener>();
		message_item_t.GetComponentsInChildren(tweens);
		while ((UnityEngine.Object)tweens.Find((UITweener o) => o.enabled) != (UnityEngine.Object)null)
		{
			yield return (object)null;
		}
		SoundManager.PlaySystemSE(SoundID.UISE.POPUP, 1f);
		messageLabel.text = msg;
		typewriter = messageLabel.gameObject.AddComponent<TypewriterEffect>();
		typewriter.charsPerSecond = StoryDirector.SPEED_TYPEWRITER;
		typewriter.ResetToBeginning();
		while (typewriter.isActive)
		{
			yield return (object)null;
		}
		yield return (object)null;
		next_arrow_w.gameObject.SetActive(true);
		Vector3[] message_corners = message_item_w.worldCorners;
		Vector3[] next_arrow_corners = next_arrow_w.worldCorners;
		next_arrow_w.SetAnchor((Transform)null);
		next_arrow_w.cachedTransform.position = new Vector3((next_arrow_corners[0].x + next_arrow_corners[2].x) * 0.5f, message_corners[0].y - (next_arrow_corners[1].y - next_arrow_corners[0].y) * 0.5f, next_arrow_corners[0].z);
		SetMessageDragEnabled(true);
		UnityEngine.Object.Destroy(typewriter);
		typewriter = null;
		messageHeight = 0;
		messageNum++;
		coroutine = null;
	}

	private void OnQuery_NEXT()
	{
		if ((UnityEngine.Object)typewriter != (UnityEngine.Object)null)
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
				MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Lounge", null, UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
			}
			else
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Home", null, UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
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
			MonoBehaviourSingleton<AppMain>.I.mainCamera.gameObject.SetActive(false);
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameProgress", base.gameObject, "INTERVAL", null, null, true);
		}
		else
		{
			MonoBehaviourSingleton<StoryDirector>.I.HideBG();
			string name = (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge()) ? "MAIN_MENU_HOME" : "MAIN_MENU_LOUNGE";
			EventData[] autoEvents2 = new EventData[2]
			{
				new EventData(name, null),
				new EventData("DELIVERY_CLEAR_REWARD", eventData)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents2);
		}
	}

	private void LateUpdate()
	{
		if ((UnityEngine.Object)lastMessageItem != (UnityEngine.Object)null)
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
					SetLastMessageFocus(false);
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
		float alpha = (!is_focus) ? 1f : 0.6f;
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

	protected override void OnCloseStart()
	{
		SoundManager.StopVoice(0u, 6);
		base.OnCloseStart();
	}
}
