// Decompiled with JetBrains decompiler
// Type: UITutorialOperationHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class UITutorialOperationHelper : MonoBehaviour
{
  public static readonly string STR_BASIC_NEW = "LET'S FIGHT! ({0})";
  public static readonly float WAITING_BASIC_TIME = 10f;
  public static readonly int SE_ID_COMPLETE = 40000100;
  public static readonly int SE_ID_THUNDERSTORM_01 = 40000110;
  public static readonly int SE_ID_DRAGON_FLUTTER_01 = 40000111;
  public static readonly int SE_ID_DRAGON_LANDING = 40000112;
  public static readonly int SE_ID_DRAGON_CALL_01 = 40000113;
  public static readonly int SE_ID_THUNDERSTORM_02 = 40000114;
  public static readonly int SE_ID_DRAGON_CALL_02 = 40000115;
  public static readonly int SE_ID_DRAGON_CALL_03 = 40000116;
  public static readonly int SE_ID_DRAGON_FLUTTER_02 = 40000117;
  public static readonly int SE_ID_DRAGON_CALL_04 = 40000118;
  public static readonly int SE_ID_TITLELOGO = 40000119;
  [SerializeField]
  private UITutorialOperationHelper.TutorialCommon _commonHelper = new UITutorialOperationHelper.TutorialCommon();
  [SerializeField]
  private UITutorialOperationHelper.TutorialMove _moveHelper = new UITutorialOperationHelper.TutorialMove();
  [SerializeField]
  private UITutorialOperationHelper.TutorialAvoid _avoidHelper = new UITutorialOperationHelper.TutorialAvoid();
  [SerializeField]
  private UITutorialOperationHelper.TutorialAttack _attackHelper = new UITutorialOperationHelper.TutorialAttack();
  [SerializeField]
  private UITutorialOperationHelper.TutorialGuard _guardHelper = new UITutorialOperationHelper.TutorialGuard();
  [SerializeField]
  private UITutorialOperationHelper.TutorialBattle _battleHelper = new UITutorialOperationHelper.TutorialBattle();
  [SerializeField]
  private UITutorialOperationHelper.TutorialBoss _bossHelper = new UITutorialOperationHelper.TutorialBoss();
  [SerializeField]
  private UITutorialOperationHelper.TutorialBasicNew _basicNewHelper = new UITutorialOperationHelper.TutorialBasicNew();
  [SerializeField]
  private Transform _fingerMove;
  [SerializeField]
  private Transform _fingerRolling;
  [SerializeField]
  private Transform _fingerRoot;
  [SerializeField]
  private Transform _fingerAttack;
  private float counterTimer;
  private int countTime;

  public UITutorialOperationHelper.TutorialCommon commonHelper => this._commonHelper;

  public UITutorialOperationHelper.TutorialMove moveHelper => this._moveHelper;

  public UITutorialOperationHelper.TutorialAvoid avoidHelper => this._avoidHelper;

  public UITutorialOperationHelper.TutorialAttack attackHelper => this._attackHelper;

  public UITutorialOperationHelper.TutorialGuard guardHelper => this._guardHelper;

  public UITutorialOperationHelper.TutorialBattle battleHelper => this._battleHelper;

  public UITutorialOperationHelper.TutorialBoss bossHelper => this._bossHelper;

  public UITutorialOperationHelper.TutorialBasicNew basicNewHelper => this._basicNewHelper;

  public Transform fingerMove => this._fingerMove;

  public Transform fingerRolling => this._fingerRolling;

  public Transform fingerRoot => this._fingerRoot;

  public Transform fingerAttack => this._fingerAttack;

  public static void ShowTutorialWidget(UIWidget widget, float duration = 0.3f)
  {
    ((Component) widget).gameObject.SetActive(true);
    widget.alpha = 0.0f;
    TweenAlpha.Begin(((Component) widget).gameObject, duration, 1f);
  }

  public static void HideTutorialWidget(UIWidget widget, System.Action onHided)
  {
    TweenAlpha ta = TweenAlpha.Begin(((Component) widget).gameObject, 0.2f, 0.0f);
    ta.onFinished.Clear();
    ta.AddOnFinished((EventDelegate.Callback) (() =>
    {
      ((Component) widget).gameObject.SetActive(false);
      if (onHided != null)
        onHided();
      Object.Destroy((Object) ta);
    }));
  }

  private void Awake()
  {
    this.countTime = (int) UITutorialOperationHelper.WAITING_BASIC_TIME;
    if (this._basicNewHelper == null)
      return;
    this._basicNewHelper.HideHelpPicture();
  }

  private void Update()
  {
    if (this._basicNewHelper.isActive() && this.countTime >= 0)
    {
      this._basicNewHelper.SetLabel(this.countTime);
      this.counterTimer += Time.deltaTime;
      if ((double) this.counterTimer < 1.0)
        return;
      --this.countTime;
      this.counterTimer = 0.0f;
      if (this.countTime < 0)
        return;
      this._basicNewHelper.SetLabel(this.countTime);
    }
    else
    {
      if (!this._basicNewHelper.isActive())
        return;
      this._basicNewHelper.HideHelpPicture();
    }
  }

  [Serializable]
  public class TutorialCommon
  {
    [SerializeField]
    private GameObject autoControlMark;
    [SerializeField]
    private UISprite tapFingerSprite;
    [SerializeField]
    private UISprite tapIconSprite;
    [SerializeField]
    private UISprite[] tapCharacters;
    [SerializeField]
    private UISprite longTapFingerSprite;
    [SerializeField]
    private UISprite fingerSprite;
    [SerializeField]
    private UITweenCtrl complete;
    [SerializeField]
    private UITweenCtrl good_job;
    [SerializeField]
    private UITweenCtrl excellent;
    [SerializeField]
    private UITweenCtrl splendid;
    [SerializeField]
    private UITweenCtrl[] enemy_counts;
    public bool OnShowEnemyCount;
    public int CurrentEnemyShow;

    public void ShowAutoControlMark() => this.autoControlMark.SetActive(true);

    public void HideAutoControlMark() => this.autoControlMark.SetActive(false);

    public UISprite ShowTapFinger()
    {
      UITutorialOperationHelper.ShowTutorialWidget((UIWidget) this.tapFingerSprite);
      return this.tapFingerSprite;
    }

    public void HideTapFinger(System.Action onHided = null)
    {
      UITutorialOperationHelper.HideTutorialWidget((UIWidget) this.tapFingerSprite, onHided);
    }

    public void ShowTapIcon(int index = 0)
    {
      ((Component) this.tapIconSprite).gameObject.SetActive(true);
      ((Behaviour) this.tapIconSprite).enabled = true;
      ((Component) this.tapCharacters[index]).gameObject.SetActive(true);
      ((Behaviour) this.tapCharacters[index]).enabled = true;
    }

    public void HideTapIcon()
    {
      ((Component) this.tapIconSprite).gameObject.SetActive(false);
      ((Behaviour) this.tapIconSprite).enabled = false;
      for (int index = 0; index < this.tapCharacters.Length; ++index)
      {
        ((Component) this.tapCharacters[index]).gameObject.SetActive(false);
        ((Behaviour) this.tapCharacters[index]).enabled = false;
      }
    }

    public UISprite ShowFinger(float duration = 0.3f)
    {
      UITutorialOperationHelper.ShowTutorialWidget((UIWidget) this.fingerSprite, duration);
      return this.fingerSprite;
    }

    public void HideFinger(System.Action onHided = null)
    {
      UITutorialOperationHelper.HideTutorialWidget((UIWidget) this.fingerSprite, onHided);
    }

    public UISprite ShowLongTapFinger()
    {
      UITutorialOperationHelper.ShowTutorialWidget((UIWidget) this.longTapFingerSprite);
      return this.longTapFingerSprite;
    }

    public void HideLongTapFinger(System.Action onHided = null)
    {
      UITutorialOperationHelper.HideTutorialWidget((UIWidget) this.longTapFingerSprite, onHided);
    }

    public void ShowComplete()
    {
      ((Component) this.complete).gameObject.SetActive(true);
      this.complete.Reset();
      SoundManager.PlayOneShotUISE(UITutorialOperationHelper.SE_ID_COMPLETE);
      this.complete.Play();
    }

    public void HideComplete() => ((Component) this.complete).gameObject.SetActive(false);

    public void ShowGoodJob()
    {
      ((Component) this.good_job).gameObject.SetActive(true);
      this.good_job.Reset();
      SoundManager.PlayOneShotUISE(UITutorialOperationHelper.SE_ID_COMPLETE);
      this.good_job.Play();
    }

    public void HideGoodJob() => ((Component) this.good_job).gameObject.SetActive(false);

    public void ShowExcellent()
    {
      ((Component) this.excellent).gameObject.SetActive(true);
      this.excellent.Reset();
      SoundManager.PlayOneShotUISE(UITutorialOperationHelper.SE_ID_COMPLETE);
      this.excellent.Play();
    }

    public void HideExcellent() => ((Component) this.excellent).gameObject.SetActive(false);

    public void ShowSplendid()
    {
      ((Component) this.splendid).gameObject.SetActive(true);
      this.splendid.Reset();
      SoundManager.PlayOneShotUISE(UITutorialOperationHelper.SE_ID_COMPLETE);
      this.splendid.Play();
    }

    public void HideSplendid() => ((Component) this.splendid).gameObject.SetActive(false);

    public void ShowEnemyCount(int count)
    {
      this.CurrentEnemyShow = count > this.CurrentEnemyShow ? count : this.CurrentEnemyShow;
      if (this.OnShowEnemyCount)
        return;
      if (count == 0)
      {
        ((Component) this.enemy_counts[count]).gameObject.SetActive(true);
      }
      else
      {
        if (((Component) this.enemy_counts[0]).gameObject.activeInHierarchy)
          ((Component) this.enemy_counts[0]).gameObject.SetActive(true);
        this.OnShowEnemyCount = true;
        ((Component) this.enemy_counts[count]).gameObject.SetActive(true);
        this.enemy_counts[count].Reset();
        this.enemy_counts[count].Play(onFinished: (EventDelegate.Callback) (() =>
        {
          this.OnShowEnemyCount = false;
          ((Component) this.enemy_counts[count]).gameObject.SetActive(false);
          if (this.CurrentEnemyShow <= count)
            return;
          this.ShowEnemyCount(count + 1);
        }));
      }
    }

    public void HideEnemyCount()
    {
      for (int index = 0; index < this.enemy_counts.Length; ++index)
        ((Component) this.enemy_counts[index]).gameObject.SetActive(false);
    }
  }

  [Serializable]
  public class TutorialMove
  {
    [SerializeField]
    private UIWidget tutorialText;
    [SerializeField]
    private UIWidget helpPicture;
    [SerializeField]
    private UIWidget helpPicture_ios;

    public void ShowHelpText() => UITutorialOperationHelper.ShowTutorialWidget(this.tutorialText);

    public void HideHelpText(System.Action onHided = null)
    {
      UITutorialOperationHelper.HideTutorialWidget(this.tutorialText, onHided);
    }

    public void ShowHelpPicture() => UITutorialOperationHelper.ShowTutorialWidget(this.helpPicture);

    public void HideHelpPicture(System.Action onHided = null)
    {
      UITutorialOperationHelper.HideTutorialWidget(this.helpPicture, onHided);
    }
  }

  [Serializable]
  public class TutorialAvoid
  {
    [SerializeField]
    private UIWidget tutorialText;

    public void ShowHelpText() => UITutorialOperationHelper.ShowTutorialWidget(this.tutorialText);

    public void HideHelpText(System.Action onHided = null)
    {
      UITutorialOperationHelper.HideTutorialWidget(this.tutorialText, onHided);
    }
  }

  [Serializable]
  public class TutorialAttack
  {
    [SerializeField]
    private UIWidget tutorialText;
    [SerializeField]
    private UIWidget comboText;
    [SerializeField]
    private UIWidget helpPicture;
    [SerializeField]
    private UIWidget helpPicture_ios;

    public void ShowHelpText() => UITutorialOperationHelper.ShowTutorialWidget(this.tutorialText);

    public void HideHelpText(System.Action onHided = null)
    {
      UITutorialOperationHelper.HideTutorialWidget(this.tutorialText, onHided);
    }

    public void ShowComboHelpText() => UITutorialOperationHelper.ShowTutorialWidget(this.comboText);

    public void HideComboHelpText(System.Action onHided = null)
    {
      UITutorialOperationHelper.HideTutorialWidget(this.comboText, onHided);
    }

    public void ShowHelpPicture() => UITutorialOperationHelper.ShowTutorialWidget(this.helpPicture);

    public void HideHelpPicture(System.Action onHided = null)
    {
      UITutorialOperationHelper.HideTutorialWidget(this.helpPicture, onHided);
    }
  }

  [Serializable]
  public class TutorialGuard
  {
    [SerializeField]
    private UIWidget tutorialText;

    public void ShowHelpText() => UITutorialOperationHelper.ShowTutorialWidget(this.tutorialText);

    public void HideHelpText(System.Action onHided = null)
    {
      UITutorialOperationHelper.HideTutorialWidget(this.tutorialText, onHided);
    }
  }

  [Serializable]
  public class TutorialBattle
  {
    [SerializeField]
    private UISprite helpPicture0;
    [SerializeField]
    private UISprite helpPicture1;

    public void ShowHelpPicture0()
    {
      UITutorialOperationHelper.ShowTutorialWidget((UIWidget) this.helpPicture0);
    }

    public void HideHelpPicture0(System.Action onHided = null)
    {
      UITutorialOperationHelper.HideTutorialWidget((UIWidget) this.helpPicture0, onHided);
    }

    public void ShowHelpPicture1()
    {
      UITutorialOperationHelper.ShowTutorialWidget((UIWidget) this.helpPicture1);
    }

    public void HideHelpPicture1(System.Action onHided = null)
    {
      UITutorialOperationHelper.HideTutorialWidget((UIWidget) this.helpPicture1, onHided);
    }
  }

  [Serializable]
  public class TutorialBoss
  {
    [SerializeField]
    private UISprite helpPicture0;
    [SerializeField]
    private UISprite helpPicture1;
    [SerializeField]
    private UISprite helpPicture2;
    [SerializeField]
    private UISprite helpPicture3;
    [SerializeField]
    private Object logoAnimationPrefab;
    private GameObject logoAnimationGameObject;

    public void ShowHelpPicture0()
    {
      UITutorialOperationHelper.ShowTutorialWidget((UIWidget) this.helpPicture0);
    }

    public void HideHelpPicture0(System.Action onHided = null)
    {
      UITutorialOperationHelper.HideTutorialWidget((UIWidget) this.helpPicture0, onHided);
    }

    public void ShowHelpPicture1()
    {
      UITutorialOperationHelper.ShowTutorialWidget((UIWidget) this.helpPicture1);
    }

    public void HideHelpPicture1(System.Action onHided = null)
    {
      UITutorialOperationHelper.HideTutorialWidget((UIWidget) this.helpPicture1, onHided);
    }

    public void ShowHelpPicture2()
    {
      UITutorialOperationHelper.ShowTutorialWidget((UIWidget) this.helpPicture2);
    }

    public void HideHelpPicture2(System.Action onHided = null)
    {
      UITutorialOperationHelper.HideTutorialWidget((UIWidget) this.helpPicture2, onHided);
    }

    public void ShowHelpPicture3()
    {
      UITutorialOperationHelper.ShowTutorialWidget((UIWidget) this.helpPicture3);
    }

    public void HideHelpPicture3(System.Action onHided = null)
    {
      UITutorialOperationHelper.HideTutorialWidget((UIWidget) this.helpPicture3, onHided);
    }

    public void PlayLogoAnimation()
    {
      this.logoAnimationGameObject = ((Component) ResourceUtility.Realizes(this.logoAnimationPrefab, MonoBehaviourSingleton<AppMain>.I.mainCameraTransform)).gameObject;
    }

    public void StopLogoAnimation() => Object.Destroy((Object) this.logoAnimationGameObject);
  }

  [Serializable]
  public class TutorialBasicNew
  {
    [SerializeField]
    private GameObject helpUI;
    [SerializeField]
    private GameObject helpImage_1;
    [SerializeField]
    private GameObject helpImage_2;
    [SerializeField]
    private GameObject helpImageIOS_1;
    [SerializeField]
    private GameObject helpImageIOS_2;
    [SerializeField]
    private UIButton okBtn;
    [SerializeField]
    private UILabel okLbl;
    private System.Action OnCloseHelp;
    [SerializeField]
    private UITweenCtrl moveCtrl;
    [SerializeField]
    private UITweenCtrl attackCtrl;

    public void SetLabel(int counter)
    {
      if (!Object.op_Inequality((Object) this.okLbl, (Object) null))
        return;
      this.okLbl.text = string.Format(UITutorialOperationHelper.STR_BASIC_NEW, (object) counter);
    }

    public bool isActive() => this.helpUI.activeInHierarchy;

    public void ShowHelpPicture(System.Action OnCloseHelp)
    {
      if (OnCloseHelp != null)
        this.OnCloseHelp = OnCloseHelp;
      if (Object.op_Inequality((Object) this.okBtn, (Object) null))
        this.okBtn.onClick.Add(new EventDelegate((EventDelegate.Callback) (() => this.HideHelpPicture())));
      if (Object.op_Inequality((Object) this.helpUI, (Object) null))
        this.helpUI.SetActive(true);
      this.helpImage_1.SetActive(false);
      this.helpImage_2.SetActive(false);
      this.helpImageIOS_1.SetActive(false);
      this.helpImageIOS_2.SetActive(false);
      if (Object.op_Inequality((Object) this.moveCtrl, (Object) null))
      {
        ((Component) this.moveCtrl).gameObject.SetActive(true);
        this.moveCtrl.Play();
      }
      if (!Object.op_Inequality((Object) this.attackCtrl, (Object) null))
        return;
      ((Component) this.attackCtrl).gameObject.SetActive(true);
      this.attackCtrl.Play();
    }

    public void HideHelpPicture(System.Action onHided = null)
    {
      SoundManager.PlaySystemSE(SoundID.UISE.CLICK);
      if (!Object.op_Inequality((Object) this.helpUI, (Object) null))
        return;
      this.helpUI.SetActive(false);
      if (this.OnCloseHelp == null)
        return;
      this.OnCloseHelp();
    }
  }
}
