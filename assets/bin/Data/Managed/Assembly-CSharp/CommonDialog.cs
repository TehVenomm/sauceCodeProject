// Decompiled with JetBrains decompiler
// Type: CommonDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class CommonDialog : GameSection
{
  private const int MIN_MSG_HEIGHT = 96 /*0x60*/;
  private const int ADD_MSG_HEIGHT = 20;
  private const float THREE_BUTTON_WIDTH = 90f;
  protected static readonly string[] BTN_SPRITE_NAME = new string[4]
  {
    "CmnBtn",
    "CmnBtnG",
    "CmnBtnR",
    "CmnBtnO_n"
  };
  protected const int CMNBTNO_TEXTCOLOR_BOTTOM = -2644481;
  protected const int CMNBTNO_TEXTCOLOR_EFFECT = 957678335;
  private string backKeyEvent;

  protected virtual SoundID.UISE openingSound => SoundID.UISE.DIALOG_COMMON;

  public override string overrideBackKeyEvent => this.backKeyEvent;

  protected virtual string GetTransferUIName() => "UI_CommonDialog";

  public override void Initialize()
  {
    this.SetTransferUI(this.GetTransferUIName(), typeof (CommonDialog.UI));
    this.InitDialog(GameSceneEvent.current.userData);
    base.Initialize();
    this.PlayTween((Enum) CommonDialog.UI.OBJ_FRAME, is_input_block: false);
  }

  protected string[] GetTexts(object[] args, STRING_CATEGORY message_categoly = STRING_CATEGORY.COMMON_DIALOG)
  {
    string[] sectionTypeParams = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionTypeParams();
    if (sectionTypeParams == null || sectionTypeParams.Length < 2)
    {
      List<GameSceneTables.TextData> currentSectionTextList = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionTextList();
      string[] texts = new string[currentSectionTextList.Count];
      if (args != null)
      {
        int index = 0;
        for (int length = texts.Length; index < length; ++index)
          texts[index] = string.Format(currentSectionTextList[index].text, args);
      }
      else
      {
        int index = 0;
        for (int length = texts.Length; index < length; ++index)
          texts[index] = currentSectionTextList[index].text;
      }
      return texts;
    }
    int length1 = sectionTypeParams.Length;
    string[] texts1 = new string[length1 - 1];
    texts1[0] = args == null || args.Length == 0 ? StringTable.Get(message_categoly, uint.Parse(sectionTypeParams[1])) : StringTable.Format(message_categoly, uint.Parse(sectionTypeParams[1]), args);
    if (length1 > 2)
      texts1[1] = StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, uint.Parse(sectionTypeParams[2]));
    if (length1 > 3)
      texts1[2] = StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, uint.Parse(sectionTypeParams[3]));
    if (length1 > 4)
      texts1[3] = StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, uint.Parse(sectionTypeParams[4]));
    return texts1;
  }

  protected virtual void InitDialog(object data_object)
  {
    this.InitUI();
    if (!(data_object is CommonDialog.Desc data))
    {
      string[] texts = this.GetTexts(data_object as object[]);
      data = new CommonDialog.Desc(CommonDialog.TYPE.YES_NO_CANCEL, texts.Length != 0 ? texts[0] : "message", texts.Length > 1 ? texts[1] : "YES", texts.Length > 2 ? texts[2] : "NO", texts.Length > 3 ? texts[3] : "CANCEL");
    }
    string text = data.text;
    if (text.StartsWith("[BB]"))
    {
      text = text.Substring(4);
      UILabel component = this.GetComponent<UILabel>((Enum) CommonDialog.UI.MESSAGE);
      if (Object.op_Inequality((Object) component, (Object) null))
        component.supportEncoding = true;
    }
    this.SetLabelText((Enum) CommonDialog.UI.MESSAGE, text);
    Transform ctrl = this.GetCtrl((Enum) CommonDialog.UI.BG);
    int num1 = this.GetHeight((Enum) CommonDialog.UI.MESSAGE) + 20;
    if (num1 < 96 /*0x60*/)
      num1 = 96 /*0x60*/;
    int num2 = 20 + num1 - 96 /*0x60*/;
    this.SetHeight((Enum) CommonDialog.UI.BG, this.GetHeight((Enum) CommonDialog.UI.BG) + (int) ((double) num2 / (double) ctrl.localScale.y));
    Vector3 localPosition = ctrl.localPosition;
    localPosition.y += (float) num2 * 0.5f;
    ctrl.localPosition = localPosition;
    this.UpdateAnchors();
    Debug.Log((object) ("dialog type: " + (object) data.type));
    switch (data.type)
    {
      case CommonDialog.TYPE.OK:
        if (string.IsNullOrEmpty(data.btnText[0]))
          data.btnText[0] = StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 100U);
        this.SetActive((Enum) CommonDialog.UI.SPR_BTN_0, false);
        this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_1, data.btnText[0]);
        this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_1_R, data.btnText[0]);
        this.SetEventName((Enum) CommonDialog.UI.SPR_BTN_1, "OK");
        this.SetFullScreenButton((Enum) CommonDialog.UI.SPR_BTN_1);
        this.SetButtonSprite((Enum) CommonDialog.UI.SPR_BTN_1, CommonDialog.BTN_SPRITE_NAME[1], true);
        this.SetActive((Enum) CommonDialog.UI.OBJ_SPACE, false);
        this.SetActive((Enum) CommonDialog.UI.SPR_BTN_2, false);
        this.backKeyEvent = "OK";
        break;
      case CommonDialog.TYPE.YES_NO:
        if (string.IsNullOrEmpty(data.btnText[0]))
          data.btnText[0] = StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 101U);
        if (string.IsNullOrEmpty(data.btnText[1]))
          data.btnText[1] = StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 102U);
        this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_0, data.btnText[1]);
        this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_0_R, data.btnText[1]);
        this.SetEventName((Enum) CommonDialog.UI.SPR_BTN_0, "NO");
        this.SetButtonSprite((Enum) CommonDialog.UI.SPR_BTN_0, CommonDialog.BTN_SPRITE_NAME[2], true);
        this.SetActive((Enum) CommonDialog.UI.SPR_BTN_1, false);
        this.SetActive((Enum) CommonDialog.UI.OBJ_SPACE, true);
        this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_2, data.btnText[0]);
        this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_2_R, data.btnText[0]);
        this.SetEventName((Enum) CommonDialog.UI.SPR_BTN_2, "YES");
        this.SetButtonSprite((Enum) CommonDialog.UI.SPR_BTN_2, CommonDialog.BTN_SPRITE_NAME[1], true);
        this.backKeyEvent = "NO";
        break;
      case CommonDialog.TYPE.YES_NO_CANCEL:
        this.SetupThreeButton(data);
        break;
      case CommonDialog.TYPE.DECLINE_COMFIRM:
        if (string.IsNullOrEmpty(data.btnText[0]))
          data.btnText[0] = StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 101U);
        if (string.IsNullOrEmpty(data.btnText[1]))
          data.btnText[1] = StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 102U);
        this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_0, data.btnText[1]);
        this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_0_R, data.btnText[1]);
        this.SetEventName((Enum) CommonDialog.UI.SPR_BTN_0, "NO");
        this.SetActive((Enum) CommonDialog.UI.SPR_BTN_1, false);
        this.SetActive((Enum) CommonDialog.UI.OBJ_SPACE, true);
        this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_2, data.btnText[0]);
        this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_2_R, data.btnText[0]);
        this.SetEventName((Enum) CommonDialog.UI.SPR_BTN_2, "YES");
        this.backKeyEvent = "NO";
        break;
    }
    this.GetComponent<UITable>((Enum) CommonDialog.UI.TBL_BTN).Reposition();
    SoundManager.PlaySystemSE(this.openingSound);
  }

  protected virtual void SetupThreeButton(CommonDialog.Desc data)
  {
    if (string.IsNullOrEmpty(data.btnText[0]))
      data.btnText[0] = StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 101U);
    if (string.IsNullOrEmpty(data.btnText[1]))
      data.btnText[1] = StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 102U);
    if (string.IsNullOrEmpty(data.btnText[2]))
      data.btnText[2] = StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 103U);
    this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_0, data.btnText[0]);
    this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_0_R, data.btnText[0]);
    this.SetEventName((Enum) CommonDialog.UI.SPR_BTN_0, "YES");
    this.SetButtonSprite((Enum) CommonDialog.UI.SPR_BTN_0, CommonDialog.BTN_SPRITE_NAME[3], true);
    this.GetComponent<UILabel>((Enum) CommonDialog.UI.LBL_BTN_0).gradientBottom = NGUIMath.IntToColor(-2644481);
    this.GetComponent<UILabel>((Enum) CommonDialog.UI.LBL_BTN_0).effectColor = NGUIMath.IntToColor(957678335);
    this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_1, data.btnText[1]);
    this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_1_R, data.btnText[1]);
    this.SetEventName((Enum) CommonDialog.UI.SPR_BTN_1, "NO");
    this.SetButtonSprite((Enum) CommonDialog.UI.SPR_BTN_1, CommonDialog.BTN_SPRITE_NAME[3], true);
    this.GetComponent<UILabel>((Enum) CommonDialog.UI.LBL_BTN_1).gradientBottom = NGUIMath.IntToColor(-2644481);
    this.GetComponent<UILabel>((Enum) CommonDialog.UI.LBL_BTN_1).effectColor = NGUIMath.IntToColor(957678335);
    this.SetActive((Enum) CommonDialog.UI.OBJ_SPACE, false);
    this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_2, data.btnText[2]);
    this.SetLabelText((Enum) CommonDialog.UI.LBL_BTN_2_R, data.btnText[2]);
    this.SetEventName((Enum) CommonDialog.UI.SPR_BTN_2, "CANCEL");
    this.SetButtonSprite((Enum) CommonDialog.UI.SPR_BTN_2, CommonDialog.BTN_SPRITE_NAME[1], true);
    this.backKeyEvent = "CANCEL";
  }

  public enum TYPE
  {
    OK,
    YES_NO,
    YES_NO_CANCEL,
    YES_NO_CLOSE,
    DECLINE_COMFIRM,
    DEFAULT,
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
    FOOTER,
  }

  public class Desc
  {
    public CommonDialog.TYPE type;
    public string text;
    public string[] btnText = new string[3];
    public object data;

    public Desc(
      CommonDialog.TYPE _type,
      string _text,
      string btn_text0 = null,
      string btn_text1 = null,
      string btn_text2 = null,
      object data = null)
    {
      this.type = _type;
      this.text = _text;
      this.btnText[0] = btn_text0;
      this.btnText[1] = btn_text1;
      this.btnText[2] = btn_text2;
      this.data = data;
    }
  }
}
