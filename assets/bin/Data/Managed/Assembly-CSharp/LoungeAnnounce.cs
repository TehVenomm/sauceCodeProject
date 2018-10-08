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

	private UIWidget widget;

	private UITweenCtrl tweenCtrl;

	public void Play(AnnounceData data, Action onComplete)
	{
		Play(data.type, data.name, onComplete);
	}

	public void Play(ANNOUNCE_TYPE type, string userName, Action onComplete)
	{
		SetActive((Enum)UI.WGT_ANCHOR_POINT, true);
		if (widget == null || tweenCtrl == null)
		{
			if (onComplete != null)
			{
				onComplete();
			}
		}
		else
		{
			switch (type)
			{
			case ANNOUNCE_TYPE.CREATED_PARTY:
			{
				string text3 = StringTable.Get(STRING_CATEGORY.LOUNGE, 0u);
				SetLabelText((Enum)UI.LBL_ANNOUNCE, text3);
				break;
			}
			case ANNOUNCE_TYPE.JOIN_LOUNGE:
			{
				string text2 = StringTable.Get(STRING_CATEGORY.LOUNGE, 1u);
				SetLabelText((Enum)UI.LBL_ANNOUNCE, text2);
				break;
			}
			case ANNOUNCE_TYPE.LEAVED_LOUNGE:
			{
				string text = StringTable.Get(STRING_CATEGORY.LOUNGE, 2u);
				SetLabelText((Enum)UI.LBL_ANNOUNCE, text);
				break;
			}
			}
			SetLabelText((Enum)UI.LBL_USER_NAME, userName);
			SetFontStyle((Enum)UI.LBL_ANNOUNCE, 2);
			SetFontStyle((Enum)UI.LBL_USER_NAME, 2);
			tweenCtrl.Reset();
			tweenCtrl.Play(true, delegate
			{
				if (onComplete != null)
				{
					onComplete();
				}
			});
		}
	}

	private void Start()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		Transform ctrl = GetCtrl(UI.OBJ_EFFECT);
		if (ctrl != null)
		{
			ctrl.set_localScale(Vector3.get_zero());
		}
		widget = base.GetComponent<UIWidget>((Enum)UI.WGT_ANCHOR_POINT);
		tweenCtrl = base.GetComponent<UITweenCtrl>((Enum)UI.OBJ_TWEENCTRL);
		SetActive((Enum)UI.WGT_ANCHOR_POINT, false);
	}
}
