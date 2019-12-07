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
		base.Awake();
		if (!(window == null))
		{
			windowTransform = window.transform;
			transitions = window.GetComponentsInChildren<UITransition>();
			window.SetActive(value: false);
			isOpenDialog = false;
			enableDialog = false;
		}
	}

	private void Update()
	{
		if (!(window == null) && enableDialog && !MonoBehaviourSingleton<TransitionManager>.I.isTransing && !isOpenDialog && openInfoList.Count > 0)
		{
			StartCoroutine(DoShowDialog(openInfoList[0]));
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
		if (!isOpenDialog)
		{
			return openInfoList.Count > 0;
		}
		return true;
	}

	private IEnumerator DoShowDialog(Desc desc)
	{
		isOpenDialog = true;
		window.SetActive(value: true);
		tweenCtrl.Reset();
		if (dialogInfo.Length > desc.type)
		{
			windowTransform.parent = dialogInfo[desc.type].link;
			windowTransform.localPosition = Vector3.zero;
			int i = 0;
			for (int num = sprites.Length; i < num; i++)
			{
				sprites[i].spriteName = dialogInfo[desc.type].spritesNames[i];
			}
			messageLabel.effectColor = dialogInfo[desc.type].lineColor;
		}
		messageLabel.supportEncoding = true;
		messageLabel.text = desc.text;
		if (transitions != null)
		{
			int j = 0;
			for (int num2 = transitions.Length; j < num2; j++)
			{
				transitions[j].Open(null);
			}
		}
		bool play_tween = true;
		tweenCtrl.Play(forward: true, delegate
		{
			play_tween = false;
		});
		yield return new WaitForSeconds(desc.showTime);
		while (play_tween)
		{
			yield return null;
		}
		yield return new WaitForSeconds(desc.showTime);
		if (transitions != null)
		{
			int k = 0;
			for (int num3 = transitions.Length; k < num3; k++)
			{
				transitions[k].Close(null);
			}
		}
		yield return StartCoroutine(DoWaitTransitions());
		isOpenDialog = false;
	}

	private IEnumerator DoWaitTransitions()
	{
		if (transitions.Length == 0)
		{
			yield break;
		}
		while (true)
		{
			bool flag = false;
			int i = 0;
			for (int num = transitions.Length; i < num; i++)
			{
				if (transitions[i].isBusy)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				yield return null;
				continue;
			}
			break;
		}
	}
}
