using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInGamePopupDialog : MonoBehaviourSingleton<UIInGamePopupDialog>
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

		public int type;

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
	protected DialogInfo[] dialogInfo;

	protected List<Desc> openInfoList = new List<Desc>();

	protected Transform windowTransform;

	public bool enableDialog
	{
		get;
		protected set;
	}

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

	public static void PushOpen(string text, bool is_important, float _show_time = 1.8f)
	{
		if (MonoBehaviourSingleton<UIInGamePopupDialog>.IsValid() && MonoBehaviourSingleton<UIInGamePopupDialog>.I.enableDialog)
		{
			Desc desc = new Desc();
			desc.text = text;
			desc.type = (is_important ? 1 : 0);
			desc.showTime = _show_time;
			MonoBehaviourSingleton<UIInGamePopupDialog>.I.openInfoList.Add(desc);
		}
		else if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			Desc desc2 = new Desc();
			desc2.text = text;
			desc2.type = (is_important ? 1 : 0);
			desc2.showTime = _show_time;
			MonoBehaviourSingleton<InGameManager>.I.dialogOpenInfoList.Add(desc2);
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
			enableDialog = false;
		}
	}

	private void Update()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		if (!(window == null) && enableDialog && !MonoBehaviourSingleton<TransitionManager>.I.isTransing && !isOpenDialog && openInfoList.Count > 0)
		{
			this.StartCoroutine(DoShowDialog(openInfoList[0]));
			openInfoList.RemoveAt(0);
		}
	}

	public void SetEnableDialog(bool enable)
	{
		enableDialog = enable;
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			List<Desc> dialogOpenInfoList = MonoBehaviourSingleton<InGameManager>.I.dialogOpenInfoList;
			if (enable)
			{
				openInfoList.AddRange(dialogOpenInfoList.GetRange(0, dialogOpenInfoList.Count));
				dialogOpenInfoList.Clear();
			}
			else
			{
				dialogOpenInfoList.AddRange(openInfoList.GetRange(0, openInfoList.Count));
				openInfoList.Clear();
			}
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
		if (dialogInfo.Length > desc.type)
		{
			windowTransform.set_parent(dialogInfo[desc.type].link);
			windowTransform.set_localPosition(Vector3.get_zero());
			int n = 0;
			for (int m = sprites.Length; n < m; n++)
			{
				sprites[n].spriteName = dialogInfo[desc.type].spritesNames[n];
			}
			messageLabel.effectColor = dialogInfo[desc.type].lineColor;
		}
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
			((_003CDoShowDialog_003Ec__Iterator214)/*Error near IL_0201: stateMachine*/)._003Cplay_tween_003E__4 = false;
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
