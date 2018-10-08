using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("ProjectUI/UIGameSceneEventSender")]
public class UIGameSceneEventSender
{
	public string eventName = string.Empty;

	private bool enablePress;

	private bool enableRelease;

	private UIButtonTweenEventCtrl buttonTweenCtrl;

	private UIPlaySoundCustom playSoundCtrl;

	public object eventData
	{
		get;
		set;
	}

	public Action<string, object, string> callback
	{
		get;
		set;
	}

	public UIGameSceneEventSender()
		: this()
	{
	}

	private void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		buttonTweenCtrl = this.get_gameObject().GetComponent<UIButtonTweenEventCtrl>();
		playSoundCtrl = this.get_gameObject().GetComponent<UIPlaySoundCustom>();
	}

	private void OnValidate()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		UIButton component = this.get_gameObject().GetComponent<UIButton>();
		if (component != null && component.onClick.Find((EventDelegate o) => o.target == this) == null)
		{
			component.onClick.Add(new EventDelegate(this, "SendEvent"));
		}
	}

	private void OnPress(bool isDown)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		if (!(UICamera.currentTouch.current == null) && IsActiveButton())
		{
			bool flag = this.get_gameObject().GetInstanceID() == UICamera.currentTouch.current.GetInstanceID();
			if (isDown)
			{
				enablePress = flag;
				enableRelease = false;
				if (buttonTweenCtrl != null && flag)
				{
					buttonTweenCtrl.PlayPush(isDown);
				}
			}
			else if (enablePress)
			{
				enableRelease = flag;
				if (Object.op_Implicit(buttonTweenCtrl) && enablePress)
				{
					buttonTweenCtrl.PlayPush(isDown);
				}
			}
		}
	}

	private IEnumerator DoButtonTweenCtrlReset()
	{
		yield return (object)null;
		if (buttonTweenCtrl != null)
		{
			buttonTweenCtrl.Reset();
		}
	}

	public void SendEvent()
	{
		if (enablePress && enableRelease && IsActiveButton())
		{
			enablePress = (enableRelease = false);
			PlaySound();
			if (buttonTweenCtrl != null && buttonTweenCtrl.tweens.Length > 0 && buttonTweenCtrl.tweens[0] != null)
			{
				if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
				{
					if (MonoBehaviourSingleton<UIManager>.IsValid())
					{
						MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.UITWEEN_SMALL, true);
					}
					buttonTweenCtrl.Reset();
					buttonTweenCtrl.Play(true, delegate
					{
						//IL_003b: Unknown result type (might be due to invalid IL or missing references)
						if (MonoBehaviourSingleton<UIManager>.IsValid())
						{
							MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.UITWEEN_SMALL, false);
						}
						if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
						{
							_SendEvent();
						}
						else
						{
							this.StartCoroutine(DoButtonTweenCtrlReset());
						}
					});
				}
			}
			else
			{
				_SendEvent();
			}
		}
	}

	public void _SendEvent()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		SendEvent("UIButton", this.get_gameObject(), eventName, eventData, callback);
	}

	private void PlaySound()
	{
		if (playSoundCtrl != null)
		{
			playSoundCtrl.Play();
		}
		else
		{
			SoundID.UISE uISE = SoundID.UISE.CLICK;
			uISE = ChooseSE();
			PlayUISE(uISE);
		}
	}

	private SoundID.UISE ChooseSE()
	{
		SoundID.UISE result = SoundID.UISE.CLICK;
		if (eventName == "[BACK]" || eventName == "CLOSE" || eventName == "NO")
		{
			return SoundID.UISE.CANCEL;
		}
		if (eventName == "DECIDE" || eventName == "OK" || eventName == "YES")
		{
			return SoundID.UISE.OK;
		}
		return result;
	}

	private void PlayUISE(SoundID.UISE type)
	{
		if (type != SoundID.UISE.INVALID && MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			SoundManager.PlaySystemSE(type, 1f);
		}
	}

	private bool IsActiveButton()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		if (!TutorialMessage.IsActiveButton(this.get_gameObject()))
		{
			if (eventName == "TUTORIAL_NEXT")
			{
				return true;
			}
			if (MonoBehaviourSingleton<GameSceneManager>.I.isOpenImportantDialog || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "CommonErrorDialog")
			{
				return true;
			}
			return false;
		}
		return true;
	}

	public static void SendEvent(string caller, GameObject sender, string event_name, object event_data = null, Action<string, object, string> callback = null)
	{
		if (string.IsNullOrEmpty(event_name))
		{
			Log.Error(LOG.UI, "eventName == empty");
		}
		else if (MonoBehaviourSingleton<GameSceneManager>.IsValid() && !GameSceneEvent.IsStay())
		{
			if (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
			{
				UIPanel componentInParent = sender.GetComponentInParent<UIPanel>();
				if (componentInParent == null || componentInParent.depth != 9999)
				{
					Log.Error(LOG.UI, "GameSceneManager.I.isChangeing == true");
					return;
				}
			}
			string text = null;
			UIGameSceneEventSenderVersionRestriction component = sender.GetComponent<UIGameSceneEventSenderVersionRestriction>();
			if (component != null)
			{
				text = component.GetCheckApplicationVersionText();
			}
			if (callback == null)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(caller, sender, event_name, event_data, text, true);
			}
			else
			{
				callback.Invoke(event_name, event_data, text);
			}
		}
	}
}
