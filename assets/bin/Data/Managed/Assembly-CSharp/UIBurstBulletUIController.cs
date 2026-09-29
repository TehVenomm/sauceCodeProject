// Decompiled with JetBrains decompiler
// Type: UIBurstBulletUIController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIBurstBulletUIController : MonoBehaviour
{
  private static readonly string BULLET_ICON_PATH = "InternalUI/UI_InGame/Burst/InGameUIBurstBulletIcon";
  private const int MAX_BULLET_ICON_UI_GENERATE_COUNT = 15;
  private const int BASE_SPRITE_ADD_WIDTH = 28;
  private const int BASE_SPRITE_DEFAULT_WIDTH = 74;
  private const int EFFECT_SPRITE_WIDTH_DIFF = -30;
  private const float DIA_ICON_DEFULT_POS = 16.2f;
  private const float DIA_ICON_ADD_POS = 39.5f;
  [SerializeField]
  private GameObject m_iconRoot;
  [SerializeField]
  private UISprite m_baseSprite;
  [SerializeField]
  private UISprite m_emptyEffect;
  [SerializeField]
  private UISprite m_diamondIcon_L;
  [SerializeField]
  private UISprite m_diamondIcon_R;
  [SerializeField]
  private Transform m_bulletIconRoot;
  private List<UIBurstBulletIconController> m_bulletIcons = new List<UIBurstBulletIconController>(6);
  private int m_currentRestBulletIconCount;
  private int m_currentMaxBulletIconCount;
  private BoxCollider m_boxCol;

  public bool Initialize(UIBurstBulletUIController.InitParam _param)
  {
    this.m_currentMaxBulletIconCount = _param.MaxBulletCount;
    this.m_currentRestBulletIconCount = _param.CurrentRestBulletCount;
    for (int index = 0; index < this.m_currentMaxBulletIconCount || index < this.m_bulletIcons.Count; ++index)
    {
      if (this.m_bulletIcons.Count > index || this.CreateBulletIcon(index))
      {
        if (this.m_currentMaxBulletIconCount <= index)
          this.m_bulletIcons[index].SetInvisibleAllIcon();
        else
          this.m_bulletIcons[index].SetVisibleAllIcon();
        if (this.m_currentRestBulletIconCount <= index)
          this.m_bulletIcons[index].SetInvisibleBulletIcon();
        else
          this.m_bulletIcons[index].SetVisibleBulletIcon();
      }
    }
    this.SetEmptyAppeal(this.m_currentRestBulletIconCount == 0);
    this.InitUISpriteParameter();
    if (Object.op_Equality((Object) this.m_boxCol, (Object) null) && Object.op_Inequality((Object) this.m_baseSprite, (Object) null))
      this.m_boxCol = ((Component) this.m_baseSprite).GetComponent<BoxCollider>();
    this.m_boxCol.size = new Vector3((float) this.m_baseSprite.width, (float) this.m_baseSprite.height, 1f);
    this.m_boxCol.center = new Vector3((float) this.m_baseSprite.width / 2f, 0.0f, 0.0f);
    return true;
  }

  private bool CreateBulletIcon(int index)
  {
    if (index < 0 || 15 <= index)
      return false;
    Transform transform = ResourceUtility.Realizes(Resources.Load(UIBurstBulletUIController.BULLET_ICON_PATH), this.m_bulletIconRoot);
    if (Object.op_Equality((Object) transform, (Object) null))
      return false;
    UIBurstBulletIconController component = ((Component) transform).GetComponent<UIBurstBulletIconController>();
    if (Object.op_Equality((Object) component, (Object) null))
      return false;
    UIBurstBulletIconController.InitParam initParam = new UIBurstBulletIconController.InitParam()
    {
      IconIndex = index,
      DepthOffset = Object.op_Equality((Object) this.m_baseSprite, (Object) null) ? 0 : this.m_baseSprite.depth + 1
    };
    component.Initialize(initParam);
    this.m_bulletIcons.Add(component);
    return true;
  }

  private void InitUISpriteParameter()
  {
    if (Object.op_Equality((Object) this.m_baseSprite, (Object) null))
      return;
    int num1 = this.m_baseSprite.depth + 2 * (this.m_currentMaxBulletIconCount + 1) + 1;
    this.m_diamondIcon_L.depth = num1;
    this.m_diamondIcon_R.depth = num1;
    this.m_emptyEffect.depth = num1 + 1;
    int num2 = 74 + 28 * this.m_currentMaxBulletIconCount;
    if (this.m_baseSprite.width != num2)
      ((Component) this.m_baseSprite).GetComponent<UIButton>().RemoveAutoAddButtonEffect();
    this.m_baseSprite.width = num2;
    this.m_emptyEffect.width = num2 - 30;
    ((Component) this.m_diamondIcon_R).transform.localPosition = Vector3.op_Multiply(Vector3.right, (float) (16.200000762939453 + 39.5 * (double) this.m_currentMaxBulletIconCount));
  }

  public bool ReloadAction()
  {
    bool flag1 = false;
    if (!this.IsEnableReload())
      return flag1;
    bool flag2 = this.SetVisibleSprite(this.m_currentRestBulletIconCount);
    if (flag2)
    {
      ++this.m_currentRestBulletIconCount;
      if (!this.IsEmpty())
        this.SetEmptyAppeal(false);
    }
    return flag2;
  }

  public bool ConsumeBulletAction()
  {
    bool flag1 = false;
    if (this.m_currentRestBulletIconCount <= 0)
      return flag1;
    --this.m_currentRestBulletIconCount;
    bool flag2 = this.SetInvisibleSprite(this.m_currentRestBulletIconCount);
    if (!flag2)
      ++this.m_currentRestBulletIconCount;
    else if (this.IsEmpty())
      this.SetEmptyAppeal(true);
    return flag2;
  }

  public bool FullBurstAction()
  {
    bool flag1 = false;
    if (this.m_currentRestBulletIconCount <= 0)
      return flag1;
    bool flag2 = true;
    for (int _targetIconIndex = 0; _targetIconIndex < this.m_currentRestBulletIconCount; ++_targetIconIndex)
      this.SetInvisibleSprite(_targetIconIndex);
    this.m_currentRestBulletIconCount = 0;
    this.SetEmptyAppeal(true);
    return flag2;
  }

  public bool SetActivateIconRoot() => this.SwitchActivateIconRoot(true);

  public bool SetDeactivateIconRoot() => this.SwitchActivateIconRoot(false);

  private bool SwitchActivateIconRoot(bool _isActivate)
  {
    if (Object.op_Equality((Object) this.m_iconRoot, (Object) null) || this.m_iconRoot.activeSelf == _isActivate)
      return false;
    this.m_iconRoot.SetActive(_isActivate);
    return true;
  }

  private bool SetVisibleSprite(int _targetIconIndex)
  {
    return this.SwitchSpriteVisible(_targetIconIndex, true);
  }

  private bool SetInvisibleSprite(int _targetIconIndex)
  {
    return this.SwitchSpriteVisible(_targetIconIndex, false);
  }

  private bool SwitchSpriteVisible(int _targetIconIndex, bool _isVisible)
  {
    if (!this.IsValidSpriteTarget(_targetIconIndex))
      return false;
    return _isVisible ? this.m_bulletIcons[_targetIconIndex].SetVisibleBulletIcon() : this.m_bulletIcons[_targetIconIndex].SetInvisibleBulletIcon();
  }

  private bool SetEmptyAppeal(bool _isValid)
  {
    if (Object.op_Equality((Object) this.m_emptyEffect, (Object) null) || ((Behaviour) this.m_emptyEffect).enabled == _isValid)
      return false;
    ((Behaviour) this.m_emptyEffect).enabled = _isValid;
    return true;
  }

  private bool IsValidSpriteTarget(int _targetIconIndex)
  {
    return _targetIconIndex >= 0 && this.m_bulletIcons.Count > _targetIconIndex && !Object.op_Equality((Object) this.m_bulletIcons[_targetIconIndex], (Object) null);
  }

  public void TryManualReload()
  {
    if (!MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
      return;
    Player targetPlayer = MonoBehaviourSingleton<UIPlayerStatus>.I.targetPlayer;
    if (Object.op_Equality((Object) targetPlayer, (Object) null) || targetPlayer.thsCtrl == null)
      return;
    targetPlayer.thsCtrl.TryFirstReloadAction();
  }

  public bool IsEmpty()
  {
    return this.m_currentMaxBulletIconCount > 0 && this.m_currentRestBulletIconCount <= 0;
  }

  public bool IsEnableReload()
  {
    return this.m_currentMaxBulletIconCount > 0 && this.m_currentRestBulletIconCount < this.m_currentMaxBulletIconCount;
  }

  public class InitParam
  {
    public int MaxBulletCount;
    public int CurrentRestBulletCount;
  }
}
