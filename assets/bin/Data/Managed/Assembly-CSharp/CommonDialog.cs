using System;
using System.Collections.Generic;
using UnityEngine;

public class CommonDialog : GameSection
{
	public enum TYPE
	{
		OK,
		YES_NO,
		YES_NO_CANCEL,
		YES_NO_CLOSE,
		DECLINE_COMFIRM,
		DEFAULT
	}

	protected enum UI
	{
		MESSAGE,
		SPR_BTN_0,
		LBL_BTN_0,
		LBL_BTN_0_R,
		SPR_BTN_1,
		LBL_BTN_1,
		LBL_BTN_1_R,
		SPR_BTN_2,
		LBL_BTN_2,
		LBL_BTN_2_R,
		OBJ_SPACE,
		OBJ_FRAME,
		TBL_BTN,
		BG,
		HEADER,
		CLOSE_BTN,
		LBL_TITLE,
		LBL_TITLE_U,
		LBL_TITLE_D,
		FOOTER
	}

	public class Desc
	{
		public TYPE type;

		public string text;

		public string[] btnText = new string[3];

		public object data;

		public Desc(TYPE _type, string _text, string btn_text0 = null, string btn_text1 = null, string btn_text2 = null, object data = null)
		{
			type = _type;
			text = _text;
			btnText[0] = btn_text0;
			btnText[1] = btn_text1;
			btnText[2] = btn_text2;
			this.data = data;
		}
	}

	private const int MIN_MSG_HEIGHT = 96;

	private const int ADD_MSG_HEIGHT = 20;

	private const float THREE_BUTTON_WIDTH = 90f;

	protected const int CMNBTNO_TEXTCOLOR_BOTTOM = -2644481;

	protected const int CMNBTNO_TEXTCOLOR_EFFECT = 957678335;

	protected static readonly string[] BTN_SPRITE_NAME = new string[4]
	{
		"CmnBtn",
		"CmnBtnG",
		"CmnBtnR",
		"CmnBtnO_n"
	};

	private string backKeyEvent;

	protected virtual SoundID.UISE openingSound => SoundID.UISE.DIALOG_COMMON;

	public override string overrideBackKeyEvent => backKeyEvent;

	protected virtual string GetTransferUIName()
	{
		return "UI_CommonDialog";
	}

	public override void Initialize()
	{
		SetTransferUI(GetTransferUIName(), typeof(UI));
		InitDialog(GameSceneEvent.current.userData);
		base.Initialize();
		PlayTween((Enum)UI.OBJ_FRAME, true, (EventDelegate.Callback)null, false, 0);
	}

	protected string[] GetTexts(object[] args, STRING_CATEGORY message_categoly = STRING_CATEGORY.COMMON_DIALOG)
	{
		string[] currentSectionTypeParams = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionTypeParams();
		if (currentSectionTypeParams == null || currentSectionTypeParams.Length < 2)
		{
			List<GameSceneTables.TextData> currentSectionTextList = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionTextList();
			string[] array = new string[currentSectionTextList.Count];
			if (args != null)
			{
				int i = 0;
				for (int num = array.Length; i < num; i++)
				{
					array[i] = string.Format(currentSectionTextList[i].text, args);
				}
			}
			else
			{
				int j = 0;
				for (int num2 = array.Length; j < num2; j++)
				{
					array[j] = currentSectionTextList[j].text;
				}
			}
			return array;
		}
		int num3 = currentSectionTypeParams.Length;
		string[] array2 = new string[num3 - 1];
		if (args != null && args.Length > 0)
		{
			array2[0] = StringTable.Format(message_categoly, uint.Parse(currentSectionTypeParams[1]), args);
		}
		else
		{
			array2[0] = StringTable.Get(message_categoly, uint.Parse(currentSectionTypeParams[1]));
		}
		if (num3 > 2)
		{
			array2[1] = StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, uint.Parse(currentSectionTypeParams[2]));
		}
		if (num3 > 3)
		{
			array2[2] = StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, uint.Parse(currentSectionTypeParams[3]));
		}
		if (num3 > 4)
		{
			array2[3] = StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, uint.Parse(currentSectionTypeParams[4]));
		}
		return array2;
	}

	protected virtual void InitDialog(object data_object)
	{
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		InitUI();
		Desc desc = data_object as Desc;
		if (desc == null)
		{
			string[] texts = GetTexts(data_object as object[], STRING_CATEGORY.COMMON_DIALOG);
			desc = new Desc(TYPE.YES_NO_CANCEL, (texts.Length <= 0) ? "message" : texts[0], (texts.Length <= 1) ? "YES" : texts[1], (texts.Length <= 2) ? "NO" : texts[2], (texts.Length <= 3) ? "CANCEL" : texts[3], null);
		}
		string text = desc.text;
		if (text.StartsWith("[BB]"))
		{
			text = text.Substring(4);
			UILabel component = base.GetComponent<UILabel>((Enum)UI.MESSAGE);
			if (component != null)
			{
				component.supportEncoding = true;
			}
		}
		SetLabelText((Enum)UI.MESSAGE, text);
		Transform ctrl = GetCtrl(UI.BG);
		int num = GetHeight(UI.MESSAGE) + 20;
		if (num < 96)
		{
			num = 96;
		}
		int num2 = 20 + num - 96;
		int height = GetHeight(UI.BG);
		float num3 = (float)num2;
		Vector3 localScale = ctrl.get_localScale();
		int height2 = height + (int)(num3 / localScale.y);
		SetHeight((Enum)UI.BG, height2);
		Vector3 localPosition = ctrl.get_localPosition();
		localPosition.y += (float)num2 * 0.5f;
		ctrl.set_localPosition(localPosition);
		UpdateAnchors();
		Debug.Log((object)("dialog type: " + desc.type));
		switch (desc.type)
		{
		case TYPE.OK:
			if (string.IsNullOrEmpty(desc.btnText[0]))
			{
				desc.btnText[0] = StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 100u);
			}
			SetActive((Enum)UI.SPR_BTN_0, false);
			SetLabelText((Enum)UI.LBL_BTN_1, desc.btnText[0]);
			SetLabelText((Enum)UI.LBL_BTN_1_R, desc.btnText[0]);
			SetEventName((Enum)UI.SPR_BTN_1, "OK");
			SetFullScreenButton((Enum)UI.SPR_BTN_1);
			SetButtonSprite((Enum)UI.SPR_BTN_1, BTN_SPRITE_NAME[1], true);
			SetActive((Enum)UI.OBJ_SPACE, false);
			SetActive((Enum)UI.SPR_BTN_2, false);
			backKeyEvent = "OK";
			break;
		case TYPE.YES_NO:
			if (string.IsNullOrEmpty(desc.btnText[0]))
			{
				desc.btnText[0] = StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 101u);
			}
			if (string.IsNullOrEmpty(desc.btnText[1]))
			{
				desc.btnText[1] = StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 102u);
			}
			SetLabelText((Enum)UI.LBL_BTN_0, desc.btnText[1]);
			SetLabelText((Enum)UI.LBL_BTN_0_R, desc.btnText[1]);
			SetEventName((Enum)UI.SPR_BTN_0, "NO");
			SetButtonSprite((Enum)UI.SPR_BTN_0, BTN_SPRITE_NAME[2], true);
			SetActive((Enum)UI.SPR_BTN_1, false);
			SetActive((Enum)UI.OBJ_SPACE, true);
			SetLabelText((Enum)UI.LBL_BTN_2, desc.btnText[0]);
			SetLabelText((Enum)UI.LBL_BTN_2_R, desc.btnText[0]);
			SetEventName((Enum)UI.SPR_BTN_2, "YES");
			SetButtonSprite((Enum)UI.SPR_BTN_2, BTN_SPRITE_NAME[1], true);
			backKeyEvent = "NO";
			break;
		case TYPE.YES_NO_CANCEL:
			SetupThreeButton(desc);
			break;
		case TYPE.DECLINE_COMFIRM:
			if (string.IsNullOrEmpty(desc.btnText[0]))
			{
				desc.btnText[0] = StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 101u);
			}
			if (string.IsNullOrEmpty(desc.btnText[1]))
			{
				desc.btnText[1] = StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 102u);
			}
			SetLabelText((Enum)UI.LBL_BTN_0, desc.btnText[1]);
			SetLabelText((Enum)UI.LBL_BTN_0_R, desc.btnText[1]);
			SetEventName((Enum)UI.SPR_BTN_0, "NO");
			SetActive((Enum)UI.SPR_BTN_1, false);
			SetActive((Enum)UI.OBJ_SPACE, true);
			SetLabelText((Enum)UI.LBL_BTN_2, desc.btnText[0]);
			SetLabelText((Enum)UI.LBL_BTN_2_R, desc.btnText[0]);
			SetEventName((Enum)UI.SPR_BTN_2, "YES");
			backKeyEvent = "NO";
			break;
		}
		base.GetComponent<UITable>((Enum)UI.TBL_BTN).Reposition();
		SoundManager.PlaySystemSE(openingSound, 1f);
	}

	protected virtual void SetupThreeButton(Desc data)
	{
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		if (string.IsNullOrEmpty(data.btnText[0]))
		{
			data.btnText[0] = StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 101u);
		}
		if (string.IsNullOrEmpty(data.btnText[1]))
		{
			data.btnText[1] = StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 102u);
		}
		if (string.IsNullOrEmpty(data.btnText[2]))
		{
			data.btnText[2] = StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 103u);
		}
		SetLabelText((Enum)UI.LBL_BTN_0, data.btnText[0]);
		SetLabelText((Enum)UI.LBL_BTN_0_R, data.btnText[0]);
		SetEventName((Enum)UI.SPR_BTN_0, "YES");
		SetButtonSprite((Enum)UI.SPR_BTN_0, BTN_SPRITE_NAME[3], true);
		base.GetComponent<UILabel>((Enum)UI.LBL_BTN_0).gradientBottom = NGUIMath.IntToColor(-2644481);
		base.GetComponent<UILabel>((Enum)UI.LBL_BTN_0).effectColor = NGUIMath.IntToColor(957678335);
		SetLabelText((Enum)UI.LBL_BTN_1, data.btnText[1]);
		SetLabelText((Enum)UI.LBL_BTN_1_R, data.btnText[1]);
		SetEventName((Enum)UI.SPR_BTN_1, "NO");
		SetButtonSprite((Enum)UI.SPR_BTN_1, BTN_SPRITE_NAME[3], true);
		base.GetComponent<UILabel>((Enum)UI.LBL_BTN_1).gradientBottom = NGUIMath.IntToColor(-2644481);
		base.GetComponent<UILabel>((Enum)UI.LBL_BTN_1).effectColor = NGUIMath.IntToColor(957678335);
		SetActive((Enum)UI.OBJ_SPACE, false);
		SetLabelText((Enum)UI.LBL_BTN_2, data.btnText[2]);
		SetLabelText((Enum)UI.LBL_BTN_2_R, data.btnText[2]);
		SetEventName((Enum)UI.SPR_BTN_2, "CANCEL");
		SetButtonSprite((Enum)UI.SPR_BTN_2, BTN_SPRITE_NAME[1], true);
		backKeyEvent = "CANCEL";
	}
}
