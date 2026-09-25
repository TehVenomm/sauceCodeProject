// Decompiled with JetBrains decompiler
// Type: UISkillButtonGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UISkillButtonGroup : MonoBehaviourSingleton<UISkillButtonGroup>
{
  [SerializeField]
  protected List<UISkillButton> skillButtons = new List<UISkillButton>();
  [SerializeField]
  protected UITweener[] changeStartAnimTweens;
  [SerializeField]
  protected UITweener[] changeEndAnimTweens;
  [SerializeField]
  protected Texture[] maskTextures;
  protected Player target;
  protected bool _isChangeAnimStartWait;

  public bool isChangeAnimStartWait
  {
    get => this._isChangeAnimStartWait;
    private set => this._isChangeAnimStartWait = value;
  }

  protected override void Awake()
  {
    base.Awake();
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    this.SyncRotatePosition();
  }

  protected override void OnDestroySingleton()
  {
    base.OnDestroySingleton();
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
  }

  private void OnScreenRotate(bool is_portrait) => this.SyncRotatePosition();

  private void SyncRotatePosition()
  {
    if (!SpecialDeviceManager.HasSpecialDeviceInfo || !SpecialDeviceManager.SpecialDeviceInfo.NeedModifyInGameSkillButtonPosition)
      return;
    DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
    Transform parent = ((Component) this).gameObject.transform.parent;
    if (!Object.op_Inequality((Object) parent, (Object) null))
      return;
    UIWidget component = ((Component) parent).gameObject.GetComponent<UIWidget>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    if (SpecialDeviceManager.IsPortrait)
    {
      component.leftAnchor.absolute = specialDeviceInfo.SkillButtonAnchorPortrait.left;
      component.rightAnchor.absolute = specialDeviceInfo.SkillButtonAnchorPortrait.right;
      component.bottomAnchor.absolute = specialDeviceInfo.SkillButtonAnchorPortrait.bottom;
      component.topAnchor.absolute = specialDeviceInfo.SkillButtonAnchorPortrait.top;
    }
    else
    {
      component.leftAnchor.absolute = specialDeviceInfo.SkillButtonAnchorLandscape.left;
      component.rightAnchor.absolute = specialDeviceInfo.SkillButtonAnchorLandscape.right;
      component.bottomAnchor.absolute = specialDeviceInfo.SkillButtonAnchorLandscape.bottom;
      component.topAnchor.absolute = specialDeviceInfo.SkillButtonAnchorLandscape.top;
    }
    component.UpdateAnchors();
  }

  public UISkillButton GetUISkillButton(int index)
  {
    return this.skillButtons.Count < index ? (UISkillButton) null : this.skillButtons[index];
  }

  public UISkillButton GetSameButtonIndex(int buttonIndex, ref int arrayIndex)
  {
    arrayIndex = -1;
    int index = 0;
    for (int count = this.skillButtons.Count; index < count; ++index)
    {
      UISkillButton skillButton = this.skillButtons[index];
      if (skillButton.buttonIndex == buttonIndex)
      {
        arrayIndex = index;
        return skillButton;
      }
    }
    return (UISkillButton) null;
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    int length = this.changeEndAnimTweens.Length;
    for (int index = 0; index < length; ++index)
    {
      ((Behaviour) this.changeEndAnimTweens[index]).enabled = false;
      this.changeEndAnimTweens[index].Sample(1f, true);
    }
    int count = this.skillButtons.Count;
    for (int index = 0; index < count; ++index)
      this.skillButtons[index].upDateStop = false;
    this.UpdateIndex();
  }

  public void SetTarget(Player player)
  {
    int index = 0;
    for (int count = this.skillButtons.Count; index < count; ++index)
      this.skillButtons[index].SetTareget(player);
    this.target = player;
    this.UpdateIndex();
  }

  public void UpdateIndex()
  {
    if (Object.op_Equality((Object) this.target, (Object) null) || this.target.weaponData == null)
      return;
    EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint) this.target.weaponData.eId);
    if (equipItemData == null)
      return;
    SkillItemTable.SkillSlotData[] skillSlotDataArray = equipItemData.GetSkillSlot(this.target.weaponData.exceed);
    if (!TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.USER_CREATE_02))
    {
      skillSlotDataArray = new SkillItemTable.SkillSlotData[1]
      {
        new SkillItemTable.SkillSlotData()
      };
      skillSlotDataArray[0].slotType = SKILL_SLOT_TYPE.ATTACK;
      skillSlotDataArray[0].skill_id = 0U;
    }
    int num1 = 0;
    int index1 = 0;
    for (int length = skillSlotDataArray.Length; index1 < length; ++index1)
    {
      if (skillSlotDataArray[index1].slotType != SKILL_SLOT_TYPE.ATTACK && skillSlotDataArray[index1].slotType != SKILL_SLOT_TYPE.SUPPORT && skillSlotDataArray[index1].slotType != SKILL_SLOT_TYPE.HEAL)
        ++num1;
    }
    int index2 = 0;
    int num2 = skillSlotDataArray.Length - num1;
    for (int count = this.skillButtons.Count; num2 < count; ++num2)
    {
      ((Component) this.skillButtons[index2]).gameObject.SetActive(false);
      this.skillButtons[index2].SetButtonIndex(-1);
      ++index2;
    }
    int button_index = 0;
    int index3 = 0;
    for (int length = skillSlotDataArray.Length; index3 < length && index2 < this.skillButtons.Count; ++index3)
    {
      if (skillSlotDataArray[index3].slotType == SKILL_SLOT_TYPE.ATTACK || skillSlotDataArray[index3].slotType == SKILL_SLOT_TYPE.SUPPORT || skillSlotDataArray[index3].slotType == SKILL_SLOT_TYPE.HEAL)
      {
        ((Component) this.skillButtons[index2]).gameObject.SetActive(true);
        SkillInfo.SkillParam skillParam = this.target.skillInfo.GetSkillParam(this.target.skillInfo.weaponOffset + button_index);
        if (skillParam != null && skillParam.tableData.type == skillSlotDataArray[index3].slotType)
        {
          this.skillButtons[index2].SetButtonIndex(button_index);
          ++button_index;
        }
        else
          this.skillButtons[index2].SetInActiveSlot(skillSlotDataArray[index3].slotType);
        ++index2;
      }
    }
  }

  private void Update()
  {
    if (this.IsEnable())
      return;
    ((Component) this).gameObject.SetActive(false);
  }

  public bool IsEnable()
  {
    return MonoBehaviourSingleton<StageObjectManager>.IsValid() && !Object.op_Equality((Object) this.target, (Object) null) && !Object.op_Equality((Object) (this.target.controller as SelfController), (Object) null) && MonoBehaviourSingleton<InGameSettingsManager>.IsValid();
  }

  public void ChangeAnimStart()
  {
    if (!((Component) this).gameObject.activeInHierarchy)
      return;
    this.isChangeAnimStartWait = true;
    this.StartCoroutine(this._ChangeAnimStart());
  }

  private IEnumerator _ChangeAnimStart()
  {
    int index1 = 0;
    for (int count = this.skillButtons.Count; index1 < count; ++index1)
    {
      this.skillButtons[index1].ReleaseEffects();
      this.skillButtons[index1].upDateStop = true;
    }
    yield return (object) null;
    int n = this.changeStartAnimTweens.Length;
    for (int index2 = 0; index2 < n; ++index2)
    {
      this.changeStartAnimTweens[index2].ResetToBeginning();
      this.changeStartAnimTweens[index2].PlayForward();
    }
    for (int i = 0; i < n; ++i)
    {
      while (((Behaviour) this.changeStartAnimTweens[i]).isActiveAndEnabled)
        yield return (object) null;
    }
    this.UpdateIndex();
    n = this.changeEndAnimTweens.Length;
    for (int index3 = 0; index3 < n; ++index3)
      this.changeEndAnimTweens[index3].ResetToBeginning();
    this.isChangeAnimStartWait = false;
  }

  public void ChangeAnimEnd()
  {
    if (!((Component) this).gameObject.activeInHierarchy)
    {
      int length = this.changeEndAnimTweens.Length;
      for (int index = 0; index < length; ++index)
        this.changeEndAnimTweens[index].PlayForward();
      int count = this.skillButtons.Count;
      for (int index = 0; index < count; ++index)
        this.skillButtons[index].upDateStop = false;
    }
    else
      this.StartCoroutine(this._ChangeAnimEnd());
  }

  private IEnumerator _ChangeAnimEnd()
  {
    int n = this.changeEndAnimTweens.Length;
    for (int index = 0; index < n; ++index)
      this.changeEndAnimTweens[index].PlayForward();
    for (int i = 0; i < n; ++i)
    {
      while (((Behaviour) this.changeEndAnimTweens[i]).isActiveAndEnabled)
        yield return (object) null;
    }
    n = this.skillButtons.Count;
    for (int index = 0; index < n; ++index)
      this.skillButtons[index].upDateStop = false;
  }

  public Texture GetMaskTexture(SKILL_SLOT_TYPE type)
  {
    switch (type)
    {
      case SKILL_SLOT_TYPE.ATTACK:
        return this.maskTextures[0];
      case SKILL_SLOT_TYPE.SUPPORT:
        return this.maskTextures[2];
      case SKILL_SLOT_TYPE.HEAL:
        return this.maskTextures[1];
      default:
        return (Texture) null;
    }
  }

  public void DoEnable() => ((Component) this).gameObject.SetActive(true);

  public void DoDisable() => ((Component) this).gameObject.SetActive(false);
}
