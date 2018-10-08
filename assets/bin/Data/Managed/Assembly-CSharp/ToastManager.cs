using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastManager : MonoBehaviourSingleton<ToastManager>
{
	[Serializable]
	public class DialogInfo
	{
		public Transform link;

		public Color lineColor;

		public string[] spritesNames;
	}

	public class Desc
	{
		public string text;

		public float showTime;
	}

	[SerializeField]
	protected GameObject window;

	[SerializeField]
	protected UILabel messageLabel;

	[SerializeField]
	protected UITweenCtrl tweenCtrl;

	[SerializeField]
	protected UISprite[] sprites;

	[SerializeField]
	protected DialogInfo dialogInfo;

	protected List<Desc> openInfoList = new List<Desc>();

	protected Transform windowTransform;

	public bool isOpenDialog
	{
		get;
		protected set;
	}

	public UITransition[] transitions
	{
		get;
		private set;
	}

	public static void PushOpen(string text, float _show_time = 1.8f)
	{
		if (MonoBehaviourSingleton<ToastManager>.IsValid())
		{
			Desc desc = new Desc();
			desc.text = text;
			desc.showTime = _show_time;
			MonoBehaviourSingleton<ToastManager>.I.openInfoList.Add(desc);
		}
	}

	protected override void Awake()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		base.Awake();
		if (!(window == null))
		{
			windowTransform = window.get_transform();
			transitions = window.GetComponentsInChildren<UITransition>();
			window.SetActive(false);
			isOpenDialog = false;
		}
	}

	private void Update()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		if (!(window == null) && (!MonoBehaviourSingleton<TransitionManager>.IsValid() || !MonoBehaviourSingleton<TransitionManager>.I.isTransing) && !isOpenDialog && openInfoList.Count > 0)
		{
			this.StartCoroutine(DoShowDialog(openInfoList[0]));
			openInfoList.RemoveAt(0);
		}
	}

	public bool IsShowingDialog()
	{
		return isOpenDialog || openInfoList.Count > 0;
	}

	private IEnumerator DoShowDialog(Desc desc)
	{
		isOpenDialog = true;
		window.SetActive(true);
		tweenCtrl.Reset();
		windowTransform.set_parent(dialogInfo.link);
		windowTransform.set_localPosition(Vector3.get_zero());
		int n = 0;
		for (int m = sprites.Length; n < m; n++)
		{
			sprites[n].spriteName = dialogInfo.spritesNames[n];
		}
		messageLabel.effectColor = dialogInfo.lineColor;
		messageLabel.supportEncoding = true;
		messageLabel.text = desc.text;
		if (transitions != null)
		{
			int l = 0;
			for (int k = transitions.Length; l < k; l++)
			{
				transitions[l].Open(null);
			}
		}
		bool play_tween = true;
		tweenCtrl.Play(true, delegate
		{
			((_003CDoShowDialog_003Ec__Iterator2AE)/*Error near IL_01c0: stateMachine*/)._003Cplay_tween_003E__4 = false;
		});
		yield return (object)new WaitForSeconds(desc.showTime);
		while (play_tween)
		{
			yield return (object)null;
		}
		yield return (object)new WaitForSeconds(desc.showTime);
		if (transitions != null)
		{
			int j = 0;
			for (int i = transitions.Length; j < i; j++)
			{
				transitions[j].Close(null);
			}
		}
		yield return (object)this.StartCoroutine(DoWaitTransitions());
		isOpenDialog = false;
	}

	private IEnumerator DoWaitTransitions()
	{
		if (transitions.Length > 0)
		{
			while (true)
			{
				bool busy = false;
				int j = 0;
				for (int i = transitions.Length; j < i; j++)
				{
					if (transitions[j].isBusy)
					{
						busy = true;
						break;
					}
				}
				if (!busy)
				{
					break;
				}
				yield return (object)null;
			}
		}
	}
}
