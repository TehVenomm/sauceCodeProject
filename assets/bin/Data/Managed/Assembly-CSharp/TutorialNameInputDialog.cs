// Decompiled with JetBrains decompiler
// Type: TutorialNameInputDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class TutorialNameInputDialog : GameSection
{
  private UINameInput inputName;
  private bool isTermsEnable;
  private int sexId;
  private string initName;

  public override void Initialize()
  {
    this.sexId = (int) GameSection.GetEventData();
    this.inputName = this.GetComponent<UINameInput>(this.GetCtrl((Enum) TutorialNameInputDialog.UI.OBJ_INPUT), (Enum) TutorialNameInputDialog.UI.IPT_NAME);
    this.SetActive((Enum) TutorialNameInputDialog.UI.SPR_CHECK, this.isTermsEnable);
    this.SetActive((Enum) TutorialNameInputDialog.UI.SPR_CHECK_OFF, !this.isTermsEnable);
    this.SetButtonEnabled((Enum) TutorialNameInputDialog.UI.BTN_CONFIRM, false);
    this.initName = this.sectionData.GetText("DEFAULT_NAME_TEXT");
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetInput(this.GetCtrl((Enum) TutorialNameInputDialog.UI.OBJ_INPUT), (Enum) TutorialNameInputDialog.UI.IPT_NAME, "DPRO Hunter ", 14, new EventDelegate.Callback(this.OnChangeName));
    this.inputName.selectAllTextOnFocus = false;
    this.inputName.isSelected = true;
  }

  private void OnQuery_TERMS()
  {
    this.isTermsEnable = !this.isTermsEnable;
    this.SetActive((Enum) TutorialNameInputDialog.UI.SPR_CHECK, this.isTermsEnable);
    this.SetActive((Enum) TutorialNameInputDialog.UI.SPR_CHECK_OFF, !this.isTermsEnable);
    this.SetButtonEnabled((Enum) TutorialNameInputDialog.UI.BTN_CONFIRM, this.GetInputName().Length > 0 && this.isTermsEnable);
  }

  protected void OnQuery_CONFIRM()
  {
    this.GetInputName();
    this.SendEditFigure((Action<bool>) (success =>
    {
      if (!success)
        return;
      MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("InGame");
    }));
  }

  private void OnQuery_TutorialNameConfirmDialog_YES()
  {
    this.SendEditFigure((Action<bool>) (success =>
    {
      if (!success)
        return;
      MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("InGame");
    }));
  }

  private void OnChangeName()
  {
    string inputName = this.GetInputName();
    if (inputName.Length == 0)
    {
      if (Object.op_Inequality((Object) this.inputName, (Object) null))
        this.inputName.InActiveName();
      this.SetButtonEnabled((Enum) TutorialNameInputDialog.UI.BTN_CONFIRM, false);
    }
    else
    {
      if (Object.op_Inequality((Object) this.inputName, (Object) null))
      {
        this.inputName.ActiveName();
        this.inputName.SetName(inputName);
      }
      this.SetButtonEnabled((Enum) TutorialNameInputDialog.UI.BTN_CONFIRM, this.isTermsEnable);
    }
  }

  private string GetInputName() => this.GetInputValue((Enum) TutorialNameInputDialog.UI.IPT_NAME);

  public void SendEditFigure(Action<bool> call_back)
  {
    OptionEditFigureModel.RequestSendForm send_form = new OptionEditFigureModel.RequestSendForm();
    send_form.sex = this.sexId;
    string str = this.GetInputName().Replace(" ", "");
    send_form.name = str == this.initName.Replace(" ", "") ? "/colopl_rob" : str;
    send_form.crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
    Protocol.Force((System.Action) (() => Protocol.Send<OptionEditFigureModel.RequestSendForm, OptionEditFigureModel>(OptionEditFigureModel.URL, send_form, (Action<OptionEditFigureModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }))));
    PlayerPrefs.SetString("Tut_Name", send_form.name);
  }

  private void OnQuery_CLOSE() => GameSection.BackSection();

  protected enum UI
  {
    TERMS_OF_SERVICE,
    SPR_CHECK,
    SPR_CHECK_OFF,
    IPT_NAME,
    BTN_CONFIRM,
    OBJ_INPUT,
  }
}
