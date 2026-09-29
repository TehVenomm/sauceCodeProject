// Decompiled with JetBrains decompiler
// Type: StatusEquipListPageDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class StatusEquipListPageDialog : GameSection
{
  private const int PAGE_NO_DIGIT = 3;
  private const string INITIAL_DIGIT_VALUE = "-";
  private string[] pageNo = new string[3]{ "-", "-", "-" };
  private int pageNoIndex;

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    yield return (object) null;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) StatusEquipListPageDialog.UI.LBL_INPUT_PASS_1, this.pageNo[0]);
    this.SetLabelText((Enum) StatusEquipListPageDialog.UI.LBL_INPUT_PASS_2, this.pageNo[1]);
    this.SetLabelText((Enum) StatusEquipListPageDialog.UI.LBL_INPUT_PASS_3, this.pageNo[2]);
    base.UpdateUI();
  }

  private void OnQuery_0() => this.InputNumber(0);

  private void OnQuery_1() => this.InputNumber(1);

  private void OnQuery_2() => this.InputNumber(2);

  private void OnQuery_3() => this.InputNumber(3);

  private void OnQuery_4() => this.InputNumber(4);

  private void OnQuery_5() => this.InputNumber(5);

  private void OnQuery_6() => this.InputNumber(6);

  private void OnQuery_7() => this.InputNumber(7);

  private void OnQuery_8() => this.InputNumber(8);

  private void OnQuery_9() => this.InputNumber(9);

  private void OnQuery_CLEAR()
  {
    this.pageNoIndex = 0;
    for (int index = 0; index < 3; ++index)
      this.pageNo[index] = "-";
    this.RefreshUI();
  }

  private void OnQuery_LIST()
  {
    int result = -1;
    if (!int.TryParse(this.pageNo[0] + this.pageNo[1] + this.pageNo[2], out result))
    {
      GameSection.ChangeEvent("ERROR_NOT_NUMBER");
    }
    else
    {
      StatusEquipList section = MonoBehaviourSingleton<GameSceneManager>.I.FindSection("StatusEquipList") as StatusEquipList;
      if (!Object.op_Inequality((Object) section, (Object) null))
        return;
      if (section.GetMaxPageNum() < result)
      {
        GameSection.ChangeEvent("OVER_NUMBER");
      }
      else
      {
        int pageIndex = Mathf.Max(0, result - 1);
        section.UpdateUI(pageIndex);
      }
    }
  }

  private void InputNumber(int num)
  {
    if (this.pageNoIndex == 3)
      return;
    this.pageNo[this.pageNoIndex++] = num.ToString();
    this.RefreshUI();
  }

  private enum UI
  {
    LBL_INPUT_PASS_1,
    LBL_INPUT_PASS_2,
    LBL_INPUT_PASS_3,
  }
}
