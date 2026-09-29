// Decompiled with JetBrains decompiler
// Type: UIWeaponChange
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class UIWeaponChange : UIInGamePopBase
{
  public static readonly string[] WEAPONICON_PATH = new string[6]
  {
    "WeaponIconSword",
    "WeaponIconBrade",
    "WeaponIconLance",
    "dummy",
    "WeaponIconEdge",
    "WeaponIconAllow"
  };
  public static readonly string[] ELEMENT_PATH = new string[6]
  {
    "IconElementFire",
    "IconElementWater",
    "IconElementThunder",
    "IconElementSoil",
    "IconElementLight",
    "IconElementDark"
  };
  [SerializeField]
  protected UIWeaponChange.WeaponIcons[] weaponIcons;
  [SerializeField]
  protected UISpriteAnimation changeAnim;
  [SerializeField]
  protected float animDelay;
  [SerializeField]
  protected UISprite animSprite;
  [SerializeField]
  protected UITweener[] changeStartAnimTweens;
  [SerializeField]
  protected UITweener[] changeEndAnimTweens;
  [SerializeField]
  protected UIButton changeButton;
  [SerializeField]
  protected GameObject[] oldUI;
  [SerializeField]
  protected GameObject[] newUI;
  [SerializeField]
  protected UIButton[] disableButton;
  [SerializeField]
  protected UISprite changeBtnWepSprite;
  [SerializeField]
  protected UISprite changeBtnEleSprite;
  [SerializeField]
  protected GameObject changeBtnEleObj;
  protected Player targetPlayer;
  private int prevIndex = -1;
  private int prevUniqueEquipmentIndex = -1;
  private bool requestCheck;
  private IEnumerator routineWork;
  public Transform rallyBtn;
  public Transform autoBtn;
  public Transform btnRootGroup;
  public Transform btnFrame;
  public Transform btnFrameOver;

  public bool restrictPopMenu { get; protected set; }

  public void SetRestrictPopMenu(bool isRestrict) => this.restrictPopMenu = isRestrict;

  protected override void Awake()
  {
    base.Awake();
    this.InitAnim();
    if (Object.op_Inequality((Object) this.changeButton, (Object) null))
      ((Component) this.changeButton).gameObject.AddComponent<UIButtonEffect>().isSimple = true;
    for (int index = 0; index < this.weaponIcons.Length; ++index)
    {
      UIWeaponChange.WeaponIcons weaponIcon = this.weaponIcons[index];
      if (Object.op_Inequality((Object) weaponIcon.button, (Object) null))
        ((Component) weaponIcon.button).gameObject.AddComponent<UIButtonEffect>().isSimple = true;
    }
    bool flag = TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.USER_CREATE_02);
    int index1 = 0;
    for (int length = this.oldUI.Length; index1 < length; ++index1)
      this.oldUI[index1].SetActive(!flag);
    int index2 = 0;
    for (int length = this.newUI.Length; index2 < length; ++index2)
      this.newUI[index2].SetActive(flag);
    if (flag)
      this.InitRally();
    this.restrictPopMenu = false;
    this.InitWepIcons();
  }

  private void InitRally()
  {
    if (LoungeMatchingManager.IsValidInLounge() && !QuestManager.IsValidInGameArena())
    {
      ((Component) this.rallyBtn).gameObject.SetActive(true);
      ((Component) this.autoBtn).GetComponent<TweenPosition>().to = new Vector3(0.0f, 410f, 0.0f);
      ((Component) this.btnRootGroup).GetComponent<TweenPosition>().from = new Vector3(0.0f, -40f, 0.0f);
      ((Component) this.btnFrameOver).GetComponent<TweenPosition>().to = new Vector3(-4f, 337f, 0.0f);
      ((Component) this.btnFrame).GetComponent<TweenWidth>().to = 550;
    }
    else
    {
      ((Component) this.rallyBtn).gameObject.SetActive(false);
      ((Component) this.autoBtn).GetComponent<TweenPosition>().to = new Vector3(0.0f, 355f, 0.0f);
      ((Component) this.btnRootGroup).GetComponent<TweenPosition>().from = new Vector3(0.0f, 24f, 0.0f);
      ((Component) this.btnFrameOver).GetComponent<TweenPosition>().to = new Vector3(0.0f, 280f, 0.0f);
      ((Component) this.btnFrame).GetComponent<TweenWidth>().to = 470;
    }
  }

  public void SetDisableRallyBtn(bool isDisable)
  {
    ((Component) this.rallyBtn).GetComponentInChildren<UIButton>().isEnabled = !isDisable;
  }

  private void InitAnim()
  {
    int index1 = 0;
    for (int length = this.weaponIcons.Length; index1 < length; ++index1)
    {
      if (Object.op_Inequality((Object) this.weaponIcons[index1].requestEffect, (Object) null))
        this.weaponIcons[index1].requestEffect.SetActive(false);
    }
    int index2 = 0;
    for (int length = this.changeStartAnimTweens.Length; index2 < length; ++index2)
    {
      ((Behaviour) this.changeStartAnimTweens[index2]).enabled = false;
      this.changeStartAnimTweens[index2].Sample(1f, true);
    }
    int index3 = 0;
    for (int length = this.changeEndAnimTweens.Length; index3 < length; ++index3)
    {
      ((Behaviour) this.changeEndAnimTweens[index3]).enabled = false;
      this.changeEndAnimTweens[index3].Sample(1f, true);
    }
    if (!Object.op_Inequality((Object) this.animSprite, (Object) null))
      return;
    this.animSprite.alpha = 0.0f;
  }

  public void InitWepIcons()
  {
    int index1 = 0;
    int index2 = 0;
    for (int count = this.targetPlayer.equipWeaponList.Count; index2 < count && index1 <= 3; ++index2)
    {
      if (this.targetPlayer.equipWeaponList[index2] != null)
      {
        this.SetWeaponData(this.weaponIcons[index1], this.targetPlayer.equipWeaponList[index2].eId, this.targetPlayer.equipWeaponList[index2].exceed);
        this.weaponIcons[index1].index = index2;
      }
      else
      {
        this.SetWeaponData(this.weaponIcons[index1], -1);
        this.weaponIcons[index1].index = -1;
      }
      ++index1;
    }
    int index3 = index1;
    for (int length = this.weaponIcons.Length; index3 < length; ++index3)
      this.SetWeaponData(this.weaponIcons[index3], -1);
    this.ChangeWepBtnIcon(this.targetPlayer.weaponIndex);
  }

  private void OnDisable()
  {
    if (this.routineWork == null)
      return;
    this.StopCoroutine(this.routineWork);
    this.routineWork = (IEnumerator) null;
    ((Component) this.changeAnim).gameObject.SetActive(false);
    this.panelChange.Lock();
    this.InitAnim();
  }

  public void SetTarget(Player player)
  {
    this.targetPlayer = player;
    this.SetNowWeapon();
  }

  public void SetEnableChangeButton(bool enabled)
  {
    ((Behaviour) this.changeButton).enabled = enabled;
    if (enabled || !this.isPopMenu)
      return;
    this.OnClickPopMenu();
  }

  public bool IsEnableChangeButton() => ((Behaviour) this.changeButton).enabled;

  public override void OnClickPopMenu()
  {
    if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null) && MonoBehaviourSingleton<ScreenOrientationManager>.IsValid() && MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait)
      MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
    if (this.restrictPopMenu && !this.isPopMenu)
      return;
    base.OnClickPopMenu();
  }

  protected override void LateUpdate()
  {
    int index1 = 0;
    for (int length = this.weaponIcons.Length; index1 < length; ++index1)
    {
      if (this.weaponIcons[index1].index != -1 && this.weaponIcons[index1].isEnable != this.IsEnable(index1))
        this.weaponIcons[index1].isEnable = this.IsEnable(index1);
    }
    base.LateUpdate();
    this.SetNowWeapon();
    if (!this.requestCheck || this.IsSelfCommandCheck())
      return;
    int index2 = 0;
    for (int length = this.weaponIcons.Length; index2 < length; ++index2)
    {
      if (Object.op_Inequality((Object) this.weaponIcons[index2].requestEffect, (Object) null))
        this.weaponIcons[index2].requestEffect.SetActive(false);
    }
    this.requestCheck = false;
  }

  private void SetNowWeapon()
  {
    if (this.prevIndex == this.targetPlayer.weaponIndex && this.targetPlayer.weaponIndex != -1 && this.prevUniqueEquipmentIndex == this.targetPlayer.uniqueEquipmentIndex && this.targetPlayer.uniqueEquipmentIndex != -1)
      return;
    if (this.prevIndex != -1 && ((Component) this).gameObject.activeInHierarchy)
    {
      if (this.routineWork != null)
      {
        this.StopCoroutine(this.routineWork);
        this.routineWork = (IEnumerator) null;
      }
      else
        this.panelChange.UnLock();
      if (this.isPopMenu)
        this.OnClickPopMenu();
      this.routineWork = this.ChangeAnim();
      this.StartCoroutine(this.routineWork);
    }
    this.prevIndex = this.targetPlayer.weaponIndex;
    this.prevUniqueEquipmentIndex = this.targetPlayer.uniqueEquipmentIndex;
  }

  public void PlayEvolveIconAnim(System.Action cb)
  {
    if (!((Component) this).gameObject.activeInHierarchy)
      return;
    if (this.routineWork != null)
    {
      this.StopCoroutine(this.routineWork);
      this.routineWork = (IEnumerator) null;
    }
    else
      this.panelChange.UnLock();
    if (this.isPopMenu)
      this.OnClickPopMenu();
    this.routineWork = this.ChangeAnim(false, cb);
    this.StartCoroutine(this.routineWork);
  }

  private void SetWeaponData(
    UIWeaponChange.WeaponIcons icon,
    int weaponId,
    int exceed = 0,
    bool is_top = false)
  {
    if (weaponId == -1)
    {
      if (Object.op_Inequality((Object) icon.weaponName, (Object) null))
        icon.weaponName.text = StringTable.Get(STRING_CATEGORY.IN_GAME, 3000U);
      if (Object.op_Inequality((Object) icon.weaponIcon, (Object) null))
        ((Component) icon.weaponIcon).gameObject.SetActive(false);
      if (Object.op_Inequality((Object) icon.elementIconBase, (Object) null))
        icon.elementIconBase.SetActive(false);
      icon.isEnable = false;
    }
    else
    {
      if (!Singleton<EquipItemTable>.IsValid())
        return;
      EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint) weaponId);
      if (equipItemData == null)
        return;
      icon.isEnable = true;
      if (Object.op_Inequality((Object) icon.weaponName, (Object) null))
        icon.weaponName.text = equipItemData.name;
      if (Object.op_Inequality((Object) icon.weaponIcon, (Object) null))
      {
        if (!is_top)
          ((Component) icon.weaponIcon).gameObject.SetActive(true);
        icon.weaponIcon.spriteName = UIWeaponChange.WEAPONICON_PATH[(int) equipItemData.type];
      }
      EquipItemExceedParamTable.EquipItemExceedParamAll exceedParam = equipItemData.GetExceedParam((uint) exceed);
      bool flag = false;
      if (Object.op_Inequality((Object) icon.elementIcon, (Object) null))
      {
        int index = 0;
        for (int length = equipItemData.atkElement.Length; index < length; ++index)
        {
          if (equipItemData.atkElement[index] > 0)
          {
            icon.elementIcon.spriteName = UIWeaponChange.ELEMENT_PATH[index];
            flag = true;
            break;
          }
          if (exceedParam != null && exceedParam.atkElement[index] > 0)
          {
            icon.elementIcon.spriteName = UIWeaponChange.ELEMENT_PATH[index];
            flag = true;
            break;
          }
        }
      }
      if (!Object.op_Inequality((Object) icon.elementIconBase, (Object) null))
        return;
      icon.elementIconBase.SetActive(flag);
    }
  }

  public void OnChangeWeapon0()
  {
    this.ChangeWeapon(this.weaponIcons[0].index);
    if (Object.op_Inequality((Object) this.weaponIcons[0].requestEffect, (Object) null))
      this.weaponIcons[0].requestEffect.SetActive(true);
    this.requestCheck = true;
  }

  public void OnChangeWeapon1()
  {
    this.ChangeWeapon(this.weaponIcons[1].index);
    if (Object.op_Inequality((Object) this.weaponIcons[1].requestEffect, (Object) null))
      this.weaponIcons[1].requestEffect.SetActive(true);
    this.requestCheck = true;
  }

  public void OnChangeWeapon2()
  {
    this.ChangeWeapon(this.weaponIcons[2].index);
    if (Object.op_Inequality((Object) this.weaponIcons[2].requestEffect, (Object) null))
      this.weaponIcons[2].requestEffect.SetActive(true);
    this.requestCheck = true;
  }

  private void ChangeWepBtnIcon(int index)
  {
    if (Object.op_Inequality((Object) this.animSprite, (Object) null))
      this.animSprite.spriteName = this.weaponIcons[index].weaponIcon.spriteName;
    this.changeBtnWepSprite.spriteName = this.weaponIcons[index].weaponIcon.spriteName;
    this.changeBtnEleSprite.spriteName = this.weaponIcons[index].elementIcon.spriteName;
    if (!this.weaponIcons[index].elementIconBase.activeSelf)
      this.changeBtnEleObj.SetActive(false);
    else
      this.changeBtnEleObj.SetActive(true);
  }

  private bool IsEnable(int index)
  {
    if (this.requestCheck || this.targetPlayer.isDead || this.targetPlayer.weaponIndex == index || !MonoBehaviourSingleton<StatusManager>.IsValid())
      return false;
    SelfController controller = this.targetPlayer.controller as SelfController;
    return !Object.op_Equality((Object) controller, (Object) null) && (controller.nextCommand == null || controller.nextCommand.type != SelfController.COMMAND_TYPE.CHANGE_WEAPON);
  }

  private bool IsSelfCommandCheck()
  {
    if (this.targetPlayer.actionID == (Character.ACTION_ID) 27)
      return true;
    SelfController controller = this.targetPlayer.controller as SelfController;
    return !Object.op_Equality((Object) controller, (Object) null) && controller.nextCommand != null && controller.nextCommand.type == SelfController.COMMAND_TYPE.CHANGE_WEAPON;
  }

  public void ChangeWeapon(int index)
  {
    if (!this.IsEnable(index))
      return;
    SelfController controller = this.targetPlayer.controller as SelfController;
    if (Object.op_Equality((Object) controller, (Object) null))
      return;
    controller.OnWeaponChangeButtonPress(index);
  }

  private IEnumerator ChangeAnim(bool isChangeWeapon = true, System.Action cb = null)
  {
    yield return (object) new WaitForSeconds(this.animDelay);
    if (isChangeWeapon && MonoBehaviourSingleton<UISkillButtonGroup>.IsValid())
      MonoBehaviourSingleton<UISkillButtonGroup>.I.ChangeAnimStart();
    ((Component) this.changeAnim).gameObject.SetActive(true);
    this.changeAnim.Play();
    int n = this.changeStartAnimTweens.Length;
    for (int index = 0; index < n; ++index)
    {
      this.changeStartAnimTweens[index].ResetToBeginning();
      this.changeStartAnimTweens[index].PlayForward();
    }
    int i;
    for (i = 0; i < n; ++i)
    {
      while (((Behaviour) this.changeStartAnimTweens[i]).isActiveAndEnabled)
        yield return (object) null;
    }
    if (isChangeWeapon)
    {
      this.ChangeWepBtnIcon(this.targetPlayer.weaponIndex);
      if (MonoBehaviourSingleton<UISkillButtonGroup>.IsValid())
      {
        while (MonoBehaviourSingleton<UISkillButtonGroup>.I.isChangeAnimStartWait)
          yield return (object) null;
        MonoBehaviourSingleton<UISkillButtonGroup>.I.ChangeAnimEnd();
      }
    }
    else if (cb != null)
      cb();
    n = this.changeEndAnimTweens.Length;
    for (int index = 0; index < n; ++index)
    {
      this.changeEndAnimTweens[index].ResetToBeginning();
      this.changeEndAnimTweens[index].PlayForward();
    }
    for (i = 0; i < n; ++i)
    {
      while (((Behaviour) this.changeEndAnimTweens[i]).isActiveAndEnabled)
        yield return (object) null;
    }
    while (this.changeAnim.isPlaying)
      yield return (object) null;
    ((Component) this.changeAnim).gameObject.SetActive(false);
    this.panelChange.Lock();
    this.routineWork = (IEnumerator) null;
  }

  public void SetDisableButtons(bool disable)
  {
    int index = 0;
    for (int length = this.disableButton.Length; index < length; ++index)
    {
      if (Object.op_Inequality((Object) this.disableButton[index], (Object) null))
        this.disableButton[index].isEnabled = !disable;
    }
  }

  [Serializable]
  public class WeaponIcons
  {
    public UIButton button;
    public UISprite weaponIcon;
    public GameObject elementIconBase;
    public UISprite elementIcon;
    public UILabel weaponName;
    public GameObject requestEffect;
    public int index = -1;
    private bool enable = true;

    public bool isEnable
    {
      get => this.enable;
      set
      {
        if (this.enable == value)
          return;
        this.enable = value;
        if (!Object.op_Inequality((Object) this.button, (Object) null))
          return;
        this.button.isEnabled = value;
      }
    }
  }

  [Serializable]
  public class EndPoint
  {
    public Vector3 position;
    public int width;
  }
}
