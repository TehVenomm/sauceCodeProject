using System;
using UnityEngine;

public class LoungeAnnounce : UIBehaviour
{
	public enum UI
	{
		WGT_ANCHOR_POINT,
		OBJ_TWEENCTRL,
		OBJ_EFFECT,
		LBL_ANNOUNCE,
		LBL_USER_NAME
	}

	public enum ANNOUNCE_TYPE
	{
		CREATED_PARTY,
		JOIN_LOUNGE,
		LEAVED_LOUNGE
	}

	public class AnnounceData
	{
		public ANNOUNCE_TYPE type
		{
			get;
			private set;
		}

		public string name
		{
			get;
			private set;
		}

		public AnnounceData(ANNOUNCE_TYPE setType, string setName)
		{
			type = setType;
			name = setName;
		}
	}

	protected UIWidget widget;

	protected UITweenCtrl tweenCtrl;

	public void Play(AnnounceData data, Action onComplete)
	{
		Play(data.type, data.name, onComplete);
	}

	public virtual void Play(ANNOUNCE_TYPE type, string userName, Action onComplete)
	{
		SetActive(UI.WGT_ANCHOR_POINT, is_visible: true);
		if (widget == null || tweenCtrl == null)
		{
			if (onComplete != null)
			{
				onComplete();
			}
			return;
		}
		switch (type)
		{
		case ANNOUNCE_TYPE.CREATED_PARTY:
		{
			string text3 = StringTable.Get(STRING_CATEGORY.LOUNGE, 0u);
			SetLabelText(UI.LBL_ANNOUNCE, text3);
			break;
		}
		case ANNOUNCE_TYPE.JOIN_LOUNGE:
		{
			string text2 = StringTable.Get(STRING_CATEGORY.LOUNGE, 1u);
			SetLabelText(UI.LBL_ANNOUNCE, text2);
			break;
		}
		case ANNOUNCE_TYPE.LEAVED_LOUNGE:
		{
			string text = StringTable.Get(STRING_CATEGORY.LOUNGE, 2u);
			SetLabelText(UI.LBL_ANNOUNCE, text);
			break;
		}
		}
		SetLabelText(UI.LBL_USER_NAME, userName);
		SetFontStyle(UI.LBL_ANNOUNCE, FontStyle.Italic);
		SetFontStyle(UI.LBL_USER_NAME, FontStyle.Italic);
		tweenCtrl.Reset();
		tweenCtrl.Play(forward: true, delegate
		{
			if (onComplete != null)
			{
				onComplete();
			}
		});
	}

	private void Start()
	{
		Transform ctrl = GetCtrl(UI.OBJ_EFFECT);
		if (ctrl != null)
		{
			ctrl.localScale = Vector3.zero;
		}
		widget = GetComponent<UIWidget>(UI.WGT_ANCHOR_POINT);
		tweenCtrl = GetComponent<UITweenCtrl>(UI.OBJ_TWEENCTRL);
		SetActive(UI.WGT_ANCHOR_POINT, is_visible: false);
	}
}
