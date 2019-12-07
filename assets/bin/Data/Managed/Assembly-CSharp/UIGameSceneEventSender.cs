using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("ProjectUI/UIGameSceneEventSender")]
public class UIGameSceneEventSender : MonoBehaviour
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

	private void Awake()
	{
		buttonTweenCtrl = base.gameObject.GetComponent<UIButtonTweenEventCtrl>();
		playSoundCtrl = base.gameObject.GetComponent<UIPlaySoundCustom>();
	}

	private void OnValidate()
	{
		UIButton component = base.gameObject.GetComponent<UIButton>();
		if (component != null && component.onClick.Find((EventDelegate o) => o.target == this) == null)
		{
			component.onClick.Add(new EventDelegate(this, "SendEvent"));
		}
	}

	private void OnPress(bool isDown)
	{
		if (UICamera.currentTouch.current == null || !IsActiveButton())
		{
			return;
		}
		bool flag = base.gameObject.GetInstanceID() == UICamera.currentTouch.current.GetInstanceID();
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
			if ((bool)buttonTweenCtrl && enablePress)
			{
				buttonTweenCtrl.PlayPush(isDown);
			}
		}
	}

	private IEnumerator DoButtonTweenCtrlReset()
	{
		yield return null;
		if (buttonTweenCtrl != null)
		{
			buttonTweenCtrl.Reset();
		}
	}

	public void SendEvent()
	{
		if (!enablePress || !enableRelease || !IsActiveButton())
		{
			return;
		}
		enablePress = (enableRelease = false);
		PlaySound();
		if (buttonTweenCtrl != null && buttonTweenCtrl.tweens.Length != 0 && buttonTweenCtrl.tweens[0] != null)
		{
			if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
			{
				if (MonoBehaviourSingleton<UIManager>.IsValid())
				{
					MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.UITWEEN_SMALL, is_disable: true);
				}
				buttonTweenCtrl.Reset();
				buttonTweenCtrl.Play(forward: true, delegate
				{
					if (MonoBehaviourSingleton<UIManager>.IsValid())
					{
						MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.UITWEEN_SMALL, is_disable: false);
					}
					if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
					{
						_SendEvent();
					}
					else
					{
						StartCoroutine(DoButtonTweenCtrlReset());
					}
				});
			}
		}
		else
		{
			_SendEvent();
		}
	}

	public void _SendEvent()
	{
		SendEvent("UIButton", base.gameObject, eventName, eventData, callback);
	}

	private void PlaySound()
	{
		if (playSoundCtrl != null)
		{
			playSoundCtrl.Play();
			return;
		}
		SoundID.UISE uISE = SoundID.UISE.CLICK;
		uISE = ChooseSE();
		PlayUISE(uISE);
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
			SoundManager.PlaySystemSE(type);
		}
	}

	private bool IsActiveButton()
	{
		if (!TutorialMessage.IsActiveButton(base.gameObject))
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
		else
		{
			if (!MonoBehaviourSingleton<GameSceneManager>.IsValid() || GameSceneEvent.IsStay())
			{
				return;
			}
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
				MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(caller, sender, event_name, event_data, text);
			}
			else
			{
				callback(event_name, event_data, text);
			}
		}
	}
}
