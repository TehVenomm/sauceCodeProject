// Decompiled with JetBrains decompiler
// Type: LimitedLoginBonus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class LimitedLoginBonus : GameSection
{
  private static Color bgYellow = new Color(0.972549f, 0.5411765f, 0.149019614f);
  private static Color bgRed = new Color(0.34117648f, 0.7882353f, 0.972549f);
  private static Color bgBlue = new Color(0.972549f, 0.34117648f, 0.3529412f);
  private static float iconHeight = 112f;
  private static float frameHeightMargin = 20f;
  private static Vector2 frameIconSizeBase = new Vector2(440f, 360f);
  private static Vector2 pillerSizeBase = new Vector2(11f, 755f);
  private static Vector2 bg3SizeBase = new Vector2(226f, 327f);
  private static float bg4Height = -360f;
  private static float footerHeight = -412.1f;
  private static float btnHeight = -369f;
  private static float pickUpPosX = -163f;
  private static float pickUpItemPosY = 150f;
  private static float scrollStartHeight = -38f;
  private const int BEGINNER_LOGIN_BONUS_ID = 6;
  private Transform texModel_;
  private UIModelRenderTexture texModelRenderTexture_;
  private UITexture texModelTexture_;
  private Transform texInnerModel_;
  private UIModelRenderTexture texInnerModelRenderTexture_;
  private UITexture texInnerModelTexture_;
  private Transform glowModel_;
  private bool isFirst;
  private bool isModel;
  private float startScrPos;
  private int arrayNow;
  private bool isDetail;
  private Transform info;
  private Transform infoDetail;
  private LoginBonus dummyLogBo;
  private LoginBonus lb;
  private LoginBonus.LoginBonusReward pickUpReward;
  private LoadObject topImageLoadObj;
  private bool showedBLBP;
  private List<Transform> touchAndReleaseList = new List<Transform>();

  public override void Initialize()
  {
    this.glowModel_ = Utility.Find(this._transform, "LIB_00000003");
    this.texModel_ = Utility.Find(this._transform, "TEX_MODEL");
    this.texModelRenderTexture_ = UIModelRenderTexture.Get(this.texModel_);
    this.texModelTexture_ = ((Component) this.texModel_).GetComponent<UITexture>();
    this.texInnerModel_ = Utility.Find(this._transform, "TEX_INNER_MODEL");
    this.texInnerModelRenderTexture_ = UIModelRenderTexture.Get(this.texInnerModel_);
    this.texInnerModelTexture_ = ((Component) this.texInnerModel_).GetComponent<UITexture>();
    this.info = this.SetPrefab(this.GetCtrl((Enum) LimitedLoginBonus.UI.SPR_FRAME), "LimitedLoginBonusInfo");
    this.infoDetail = this.SetPrefab(this.GetCtrl((Enum) LimitedLoginBonus.UI.SPR_FRAME), "LimitedLoginBonusInfoDetail");
    ((Component) this.info).gameObject.SetActive(false);
    ((Component) this.infoDetail).gameObject.SetActive(false);
    this.StartCoroutine(this.DoInitialize());
  }

  protected void OnQuery_RELEASE_ABILITY()
  {
    if (Object.op_Equality((Object) this.infoDetail, (Object) null) || Object.op_Equality((Object) this.info, (Object) null))
      return;
    if (this.isDetail)
    {
      this.SetLabelText(this.infoDetail, (Enum) LimitedLoginBonus.UI.LBL_INFODETAIL_NAME, "");
      this.SetLabelText(this.infoDetail, (Enum) LimitedLoginBonus.UI.LBL_INFODETAIL_DESC, "");
      this.SetLabelText(this.infoDetail, (Enum) LimitedLoginBonus.UI.LBL_INFODETAIL_NUM, "");
      ((Component) this.infoDetail).gameObject.SetActive(false);
    }
    else
    {
      this.SetLabelText(this.infoDetail, (Enum) LimitedLoginBonus.UI.LBL_INFODETAIL_NAME, "");
      this.SetLabelText(this.infoDetail, (Enum) LimitedLoginBonus.UI.LBL_INFODETAIL_DESC, "");
      this.SetLabelText(this.infoDetail, (Enum) LimitedLoginBonus.UI.LBL_INFODETAIL_NUM, "");
    }
    ((Component) this.infoDetail).gameObject.SetActive(false);
    ((Component) this.info).gameObject.SetActive(false);
    GameSection.StopEvent();
  }

  protected void OnQuery_ABILITY_DATA_POPUP()
  {
    int index = (int) (GameSection.GetEventData() as object[])[0];
    string text1 = "";
    string text2 = (string) null;
    LoginBonus.LoginBonusReward loginBonusReward = this.lb.next[index].reward[0];
    if (Singleton<ItemTable>.I.IsExistItemData((uint) loginBonusReward.itemId))
    {
      ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint) loginBonusReward.itemId);
      if (itemData != null)
      {
        text1 = itemData.name;
        text2 = itemData.text;
      }
    }
    if (string.IsNullOrEmpty(text1))
      text1 = loginBonusReward.name;
    string text3 = "x" + loginBonusReward.itemNum.ToString();
    Vector3 vector3;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3).\u002Ector(0.0f, 60f, 0.0f);
    if (text2 != null)
    {
      this.SetLabelText(this.infoDetail, (Enum) LimitedLoginBonus.UI.LBL_INFODETAIL_NAME, text1);
      this.SetLabelText(this.infoDetail, (Enum) LimitedLoginBonus.UI.LBL_INFODETAIL_DESC, text2);
      this.SetLabelText(this.infoDetail, (Enum) LimitedLoginBonus.UI.LBL_INFODETAIL_NUM, text3);
      this.infoDetail.localPosition = vector3;
      ((Component) this.infoDetail).gameObject.SetActive(true);
      this.isDetail = true;
    }
    else
    {
      this.SetLabelText(this.info, (Enum) LimitedLoginBonus.UI.LBL_INFO_NAME, text1);
      this.SetLabelText(this.info, (Enum) LimitedLoginBonus.UI.LBL_INFO_NUM, text3);
      this.info.localPosition = vector3;
      ((Component) this.info).gameObject.SetActive(true);
      this.isDetail = false;
    }
    GameSection.StopEvent();
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    base.OnNotify(flags);
    if ((flags & GameSection.NOTIFY_FLAG.PRETREAT_SCENE) == (GameSection.NOTIFY_FLAG) 0)
      return;
    this.NoEventReleaseTouchAndReleases(this.touchAndReleaseList);
    this.OnQuery_RELEASE_ABILITY();
  }

  private IEnumerator DoInitialize()
  {
    bool connect = false;
    this.lb = (LoginBonus) null;
    if (GameSection.GetEventData() != null)
    {
      Protocol.Send<LoginBonusConfirmModel.RequestSendForm, LoginBonusConfirmModel>(LoginBonusConfirmModel.URL, new LoginBonusConfirmModel.RequestSendForm()
      {
        loginBonusId = (int) GameSection.GetEventData()
      }, (Action<LoginBonusConfirmModel>) (ret =>
      {
        if (ret.Error != Error.None)
          return;
        if (ret != null && ret.result != null && ret.result.Count > 0)
          this.lb = ret.result[0];
        connect = true;
      }));
      while (!connect)
        yield return (object) null;
    }
    if (!connect)
    {
      this.lb = MonoBehaviourSingleton<AccountManager>.I.logInBonus[0];
      MonoBehaviourSingleton<AccountManager>.I.logInBonus.Remove(this.lb);
    }
    if (this.lb == null)
    {
      base.Initialize();
      while (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
        yield return (object) null;
      GameSection.BackSection();
    }
    else
    {
      this.arrayNow = 0;
      int index = 0;
      for (int count = this.lb.next.Count; index < count; ++index)
      {
        if (this.lb.next[index].count == this.lb.nowCount)
        {
          this.arrayNow = index;
          break;
        }
        if (this.lb.next[index].count > this.lb.nowCount)
        {
          this.arrayNow = index;
          break;
        }
      }
      int num1 = 1 + (this.lb.next.Count - 1) / 5;
      int num2 = 1 + this.arrayNow / 5;
      if (num1 > 3)
      {
        if (num2 > num1 - 2)
          num2 = num1 - 2;
        this.startScrPos = LimitedLoginBonus.scrollStartHeight + LimitedLoginBonus.iconHeight * (float) (num2 - 1);
        this.isFirst = true;
      }
      else
        this.isFirst = false;
      this.SetPickUp();
      float val = 35f;
      if (14 == this.pickUpReward.type)
      {
        this.SetRenderAccessoryModel((Enum) LimitedLoginBonus.UI.TEX_MODEL, (uint) this.pickUpReward.itemId, this.pickUpReward.GetScale());
        this.isModel = true;
      }
      else if (5 == this.pickUpReward.type)
      {
        uint itemId = (uint) this.pickUpReward.itemId;
        this.texModelRenderTexture_.InitSkillItem(this.texModelTexture_, itemId, fov: 45f);
        this.texInnerModelRenderTexture_.InitSkillItemSymbol(this.texInnerModelTexture_, itemId, fov: 17f);
        this.isModel = true;
      }
      else if (4 == this.pickUpReward.type)
      {
        this.SetRenderEquipModel((Enum) LimitedLoginBonus.UI.TEX_MODEL, (uint) this.pickUpReward.itemId, scale: this.pickUpReward.GetScale());
        this.isModel = true;
      }
      else if (1 == this.pickUpReward.type || 2 == this.pickUpReward.type)
      {
        this.texModelRenderTexture_.InitItem(this.texModelTexture_, this.GetItemModelID((REWARD_TYPE) this.pickUpReward.type, this.pickUpReward.itemId));
        this.isModel = true;
      }
      else if (3 == this.pickUpReward.type && this.IsDispItem3D(this.pickUpReward.itemId))
      {
        this.texModelRenderTexture_.InitItem(this.texModelTexture_, this.GetItemModelID((REWARD_TYPE) this.pickUpReward.type, this.pickUpReward.itemId));
        this.isModel = true;
      }
      this.texModelRenderTexture_.SetRotateSpeed(val);
      this.texInnerModelRenderTexture_.SetRotateSpeed(val);
      LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
      string loginBonusTopImage = ResourceName.GetLoginBonusTopImage(this.lb.loginBonusId);
      this.topImageLoadObj = loadingQueue.Load(true, RESOURCE_CATEGORY.LOGINBONUS_IMAGE, loginBonusTopImage);
      if (loadingQueue.IsLoading())
        yield return (object) loadingQueue.Wait();
      base.Initialize();
    }
  }

  public override void UpdateUI()
  {
    if (this.lb == null)
    {
      this.SetLabelText((Enum) LimitedLoginBonus.UI.LBL_PICKUP, "");
      this.SetLabelText((Enum) LimitedLoginBonus.UI.LBL_PERIOD, "");
    }
    else
    {
      if (this.topImageLoadObj != null)
      {
        Texture2D loadedObject = this.topImageLoadObj.loadedObject as Texture2D;
        if (Object.op_Inequality((Object) loadedObject, (Object) null))
        {
          Transform ctrl = this.FindCtrl(this._transform, (Enum) LimitedLoginBonus.UI.TEX_LOGIN_BANNER);
          this.SetActive(ctrl, true);
          this.SetTexture(ctrl, (Texture) loadedObject);
        }
      }
      if (!this.isModel)
      {
        this.FindCtrl(this._transform, (Enum) LimitedLoginBonus.UI.OBJ_DETAIL_ROOT).localPosition = new Vector3(LimitedLoginBonus.pickUpPosX, LimitedLoginBonus.pickUpItemPosY, 0.0f);
        LoginBonus.LoginBonusReward pickUpReward = this.pickUpReward;
        ItemIcon.CreateRewardItemIcon((REWARD_TYPE) pickUpReward.type, (uint) pickUpReward.itemId, Utility.Find(this._transform, "OBJ_DETAIL_ROOT")).transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
      }
      this.SetLabelText((Enum) LimitedLoginBonus.UI.LBL_PERIOD, this.lb.period_announce);
      this.SetLabelText((Enum) LimitedLoginBonus.UI.LBL_LOGIN_DAYS, string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 7U), (object) this.lb.nowCount.ToString()));
      ((Component) this.FindCtrl(this._transform, (Enum) LimitedLoginBonus.UI.LBL_PICKUP)).GetComponent<UILabel>().supportEncoding = true;
      this.SetLabelText((Enum) LimitedLoginBonus.UI.LBL_PICKUP, this.pickUpReward.pickUpText);
      int count = this.lb.next.Count;
      this.SetFrame(1 + (this.lb.next.Count - 1) / 5, this.lb.boardType);
      this.touchAndReleaseList.Clear();
      this.SetGrid((Enum) LimitedLoginBonus.UI.GRD_BONUSLIST, "LimitedLoginBonusItem", count, false, (Action<int, Transform, bool>) ((i, t, b) =>
      {
        LoginBonus.LoginBonusReward loginBonusReward = this.lb.next[i].reward[0];
        bool isGet = loginBonusReward.isGet;
        if (this.arrayNow == i && this.lb.reward.Count > 0)
        {
          GameObject gameObject = ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.SPR_STAMP_ANIM)).gameObject;
          gameObject.SetActive(true);
          EventDelegate.Set(gameObject.GetComponentInChildren<TweenScale>().onFinished, new EventDelegate.Callback(this.SetGetDialog));
          ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.SPR_STAMP)).gameObject.SetActive(false);
        }
        else if (isGet)
        {
          ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.SPR_STAMP_ANIM)).gameObject.SetActive(false);
          ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.SPR_STAMP)).gameObject.SetActive(true);
        }
        else
        {
          ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.SPR_STAMP)).gameObject.SetActive(false);
          ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.SPR_STAMP_ANIM)).gameObject.SetActive(false);
        }
        if (loginBonusReward.isPickUp)
        {
          ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.SPR_DAY_BASE)).gameObject.SetActive(false);
          ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.SPR_DAY_BASE_PICKUP)).gameObject.SetActive(true);
          ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.SPR_DAY_BASE_FINE)).gameObject.SetActive(false);
          ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.LBL_DAY_PICKUP)).gameObject.SetActive(true);
          ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.LBL_DAY_FINE)).gameObject.SetActive(false);
          ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.LBL_DAY)).gameObject.SetActive(false);
          this.SetLabelText(t, (Enum) LimitedLoginBonus.UI.LBL_DAY_PICKUP, loginBonusReward.day);
        }
        else if (loginBonusReward.frameType != 0)
        {
          ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.SPR_DAY_BASE)).gameObject.SetActive(false);
          ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.SPR_DAY_BASE_PICKUP)).gameObject.SetActive(false);
          ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.SPR_DAY_BASE_FINE)).gameObject.SetActive(true);
          ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.LBL_DAY_PICKUP)).gameObject.SetActive(false);
          ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.LBL_DAY_FINE)).gameObject.SetActive(true);
          ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.LBL_DAY)).gameObject.SetActive(false);
          this.SetLabelText(t, (Enum) LimitedLoginBonus.UI.LBL_DAY_FINE, loginBonusReward.day);
        }
        else
        {
          ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.SPR_DAY_BASE)).gameObject.SetActive(true);
          ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.SPR_DAY_BASE_PICKUP)).gameObject.SetActive(false);
          ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.SPR_DAY_BASE_FINE)).gameObject.SetActive(false);
          ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.LBL_DAY_PICKUP)).gameObject.SetActive(false);
          ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.LBL_DAY_FINE)).gameObject.SetActive(false);
          ((Component) this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.LBL_DAY)).gameObject.SetActive(true);
          this.SetLabelText(t, (Enum) LimitedLoginBonus.UI.LBL_DAY, loginBonusReward.day);
        }
        this.SetLabelText(t, (Enum) LimitedLoginBonus.UI.LBL_ITEMNUM, "x" + loginBonusReward.itemNum.ToString());
        ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon((REWARD_TYPE) loginBonusReward.type, (uint) loginBonusReward.itemId, this.FindCtrl(t, (Enum) LimitedLoginBonus.UI.OBJ_ICON_ROOT));
        if (Object.op_Inequality((Object) rewardItemIcon, (Object) null))
          rewardItemIcon.SetEnableCollider(false);
        if (isGet)
        {
          UITexture[] componentsInChildren1 = ((Component) rewardItemIcon).GetComponentsInChildren<UITexture>();
          int index1 = 0;
          for (int length = componentsInChildren1.Length; index1 < length; ++index1)
            componentsInChildren1[index1].color = Color.gray;
          UISprite[] componentsInChildren2 = ((Component) rewardItemIcon).GetComponentsInChildren<UISprite>();
          int index2 = 0;
          for (int length = componentsInChildren2.Length; index2 < length; ++index2)
            componentsInChildren2[index2].color = Color.gray;
        }
        this.SetAbilityItemEvent(t, i, this.touchAndReleaseList);
      }));
      if (!this.isFirst)
        return;
      Transform ctrl1 = this.GetCtrl((Enum) LimitedLoginBonus.UI.SCR_BONUSLIST);
      UIPanel component = ((Component) ctrl1).GetComponent<UIPanel>();
      ((Component) ctrl1).transform.localPosition = new Vector3(0.0f, this.startScrPos, 0.0f);
      component.clipOffset = new Vector2(0.0f, -this.startScrPos);
    }
  }

  private void SetFrame(int column_num, int board_type)
  {
    Color color;
    switch (board_type)
    {
      case 2:
        color = LimitedLoginBonus.bgYellow;
        break;
      case 3:
        color = LimitedLoginBonus.bgBlue;
        break;
      default:
        color = LimitedLoginBonus.bgRed;
        break;
    }
    ((Component) this.FindCtrl(this._transform, (Enum) LimitedLoginBonus.UI.SPR_FRAME_BG_1_L)).GetComponent<UISprite>().color = color;
    ((Component) this.FindCtrl(this._transform, (Enum) LimitedLoginBonus.UI.SPR_FRAME_BG_1_R)).GetComponent<UISprite>().color = color;
    ((Component) this.FindCtrl(this._transform, (Enum) LimitedLoginBonus.UI.SPR_FRAME_BG_2_L)).GetComponent<UISprite>().color = color;
    ((Component) this.FindCtrl(this._transform, (Enum) LimitedLoginBonus.UI.SPR_FRAME_BG_2_R)).GetComponent<UISprite>().color = color;
    ((Component) this.FindCtrl(this._transform, (Enum) LimitedLoginBonus.UI.SPR_FRAME_BG_3_L)).GetComponent<UISprite>().color = color;
    ((Component) this.FindCtrl(this._transform, (Enum) LimitedLoginBonus.UI.SPR_FRAME_BG_3_R)).GetComponent<UISprite>().color = color;
    ((Component) this.FindCtrl(this._transform, (Enum) LimitedLoginBonus.UI.SPR_FRAME_BG_4_L)).GetComponent<UISprite>().color = color;
    ((Component) this.FindCtrl(this._transform, (Enum) LimitedLoginBonus.UI.SPR_FRAME_BG_4_R)).GetComponent<UISprite>().color = color;
    if (column_num >= 4)
      return;
    float num1 = (float) column_num * LimitedLoginBonus.iconHeight + LimitedLoginBonus.frameHeightMargin;
    float num2 = LimitedLoginBonus.frameIconSizeBase.y - num1;
    ((Component) this.FindCtrl(this._transform, (Enum) LimitedLoginBonus.UI.SPR_FRAME_PILLER_L)).GetComponent<UIWidget>().height = (int) ((double) LimitedLoginBonus.pillerSizeBase.y - (double) num2);
    ((Component) this.FindCtrl(this._transform, (Enum) LimitedLoginBonus.UI.SPR_FRAME_PILLER_R)).GetComponent<UIWidget>().height = (int) ((double) LimitedLoginBonus.pillerSizeBase.y - (double) num2);
    ((Component) this.FindCtrl(this._transform, (Enum) LimitedLoginBonus.UI.SPR_FRAME_ICONS)).GetComponent<UIWidget>().height = (int) num1;
    ((Component) this.FindCtrl(this._transform, (Enum) LimitedLoginBonus.UI.SPR_FRAME_BG_3_L)).GetComponent<UIWidget>().height = (int) ((double) LimitedLoginBonus.bg3SizeBase.y - (double) num2);
    ((Component) this.FindCtrl(this._transform, (Enum) LimitedLoginBonus.UI.SPR_FRAME_BG_3_R)).GetComponent<UIWidget>().height = (int) ((double) LimitedLoginBonus.bg3SizeBase.y - (double) num2);
    this.FindCtrl(this._transform, (Enum) LimitedLoginBonus.UI.SPR_FRAME_BG_4_L).localPosition = new Vector3(0.0f, LimitedLoginBonus.bg4Height + num2, 0.0f);
    this.FindCtrl(this._transform, (Enum) LimitedLoginBonus.UI.SPR_FRAME_BG_4_R).localPosition = new Vector3(0.0f, LimitedLoginBonus.bg4Height + num2, 0.0f);
    this.FindCtrl(this._transform, (Enum) LimitedLoginBonus.UI.SPR_FRAME_FOOTER_L).localPosition = new Vector3(0.0f, LimitedLoginBonus.footerHeight + num2, 0.0f);
    this.FindCtrl(this._transform, (Enum) LimitedLoginBonus.UI.SPR_FRAME_FOOTER_R).localPosition = new Vector3(0.0f, LimitedLoginBonus.footerHeight + num2, 0.0f);
    this.FindCtrl(this._transform, (Enum) LimitedLoginBonus.UI.BTN_CLOSE).localPosition = new Vector3(0.0f, LimitedLoginBonus.btnHeight + num2, 0.0f);
    this._transform.localPosition = new Vector3(0.0f, (float) (-(double) num2 / 2.0), 0.0f);
  }

  private void SetPickUp()
  {
    if (this.lb.usePickUp != 0)
    {
      int index = this.arrayNow + 1;
      for (int count = this.lb.next.Count; index < count; ++index)
      {
        if (this.lb.next[index].reward[0].isPickUp)
        {
          this.pickUpReward = this.lb.next[index].reward[0];
          return;
        }
      }
      for (int arrayNow = this.arrayNow; arrayNow >= 0; --arrayNow)
      {
        if (this.lb.next[arrayNow].reward[0].isPickUp)
        {
          this.pickUpReward = this.lb.next[arrayNow].reward[0];
          return;
        }
      }
    }
    if (this.lb.next.Count - 1 > this.arrayNow)
      this.pickUpReward = this.lb.next[this.arrayNow + 1].reward[0];
    else
      this.pickUpReward = this.lb.next[this.arrayNow].reward[0];
  }

  private void SetGetDialog()
  {
    this.PlayAudio((Enum) LimitedLoginBonus.AUDIO.REQUEST_COMPLETE);
    this.StartCoroutine("WaitGetDialog");
  }

  private IEnumerator WaitGetDialog()
  {
    yield return (object) new WaitForSeconds(0.8f);
    this.DispatchEvent("LIMITED_LOGIN_GET", (object) this.lb);
  }

  public void OnQuery_SELECT()
  {
  }

  public override string overrideBackKeyEvent => "CLOSE";

  public void OnQuery_CLOSE()
  {
    if (Object.op_Inequality((Object) null, (Object) this.glowModel_))
      ((Component) this.glowModel_).gameObject.SetActive(false);
    if (this.lb.type == 6 && this.lb.isBeginner2Pop && !this.showedBLBP)
    {
      this.showedBLBP = true;
      GameSection.StopEvent();
      this.DispatchEvent("BEGINNER_LOGIN_BONUS_POP");
    }
    else
      GameSection.BackSection();
  }

  private uint GetItemModelID(REWARD_TYPE type, int itemID)
  {
    uint itemModelId = uint.MaxValue;
    switch (type)
    {
      case REWARD_TYPE.CRYSTAL:
        itemModelId = 1U;
        break;
      case REWARD_TYPE.MONEY:
        itemModelId = 2U;
        break;
      case REWARD_TYPE.ITEM:
        itemModelId = (uint) itemID;
        break;
    }
    return itemModelId;
  }

  private bool IsDispItem3D(int itemID)
  {
    switch (itemID)
    {
      case 1200000:
      case 7000100:
      case 7000101:
      case 7000200:
      case 7000201:
      case 7000300:
      case 7000301:
        return true;
      default:
        return false;
    }
  }

  private void SetDummyLogbo()
  {
    this.dummyLogBo = new LoginBonus();
    this.dummyLogBo.next = new List<LoginBonus.NextReward>()
    {
      new LoginBonus.NextReward()
      {
        count = 1,
        reward = new List<LoginBonus.LoginBonusReward>()
        {
          new LoginBonus.LoginBonusReward()
          {
            name = "アイテム名1",
            type = 6,
            itemId = 302610650,
            itemNum = 10,
            isGet = true,
            isPickUp = false,
            pickUpText = "",
            day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8U))
          }
        }
      },
      new LoginBonus.NextReward()
      {
        count = 2,
        reward = new List<LoginBonus.LoginBonusReward>()
        {
          new LoginBonus.LoginBonusReward()
          {
            name = "アイテム名1",
            type = 5,
            itemId = 100100104,
            itemNum = 1,
            isGet = true,
            isPickUp = false,
            pickUpText = "",
            day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8U), (object) 2)
          }
        }
      },
      new LoginBonus.NextReward()
      {
        count = 3,
        reward = new List<LoginBonus.LoginBonusReward>()
        {
          new LoginBonus.LoginBonusReward()
          {
            name = "アイテム名1",
            type = 1,
            itemId = 1,
            itemNum = 5,
            isGet = false,
            isPickUp = false,
            pickUpText = "",
            day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8U), (object) 5)
          }
        }
      },
      new LoginBonus.NextReward()
      {
        count = 4,
        reward = new List<LoginBonus.LoginBonusReward>()
        {
          new LoginBonus.LoginBonusReward()
          {
            name = "アイテム名1",
            type = 1,
            itemId = 1,
            itemNum = 5,
            isGet = false,
            isPickUp = false,
            pickUpText = "",
            day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8U), (object) 5)
          }
        }
      },
      new LoginBonus.NextReward()
      {
        count = 5,
        reward = new List<LoginBonus.LoginBonusReward>()
        {
          new LoginBonus.LoginBonusReward()
          {
            name = "アイテム名1",
            type = 1,
            itemId = 1,
            itemNum = 5,
            isGet = false,
            isPickUp = true,
            pickUpText = "",
            day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8U), (object) 5)
          }
        }
      },
      new LoginBonus.NextReward()
      {
        count = 6,
        reward = new List<LoginBonus.LoginBonusReward>()
        {
          new LoginBonus.LoginBonusReward()
          {
            name = "アイテム名1",
            type = 4,
            itemId = 20260130,
            itemNum = 1,
            isGet = true,
            isPickUp = false,
            pickUpText = "",
            day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8U), (object) 3)
          }
        }
      },
      new LoginBonus.NextReward()
      {
        count = 7,
        reward = new List<LoginBonus.LoginBonusReward>()
        {
          new LoginBonus.LoginBonusReward()
          {
            name = "アイテム名1",
            type = 2,
            itemId = 1,
            itemNum = 1000000,
            isGet = false,
            isPickUp = false,
            pickUpText = "",
            day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8U), (object) 5)
          }
        }
      },
      new LoginBonus.NextReward()
      {
        count = 8,
        reward = new List<LoginBonus.LoginBonusReward>()
        {
          new LoginBonus.LoginBonusReward()
          {
            name = "アイテム名1",
            type = 5,
            itemId = 100100104,
            itemNum = 1,
            isGet = false,
            isPickUp = false,
            pickUpText = "",
            day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8U), (object) 2)
          }
        }
      },
      new LoginBonus.NextReward()
      {
        count = 9,
        reward = new List<LoginBonus.LoginBonusReward>()
        {
          new LoginBonus.LoginBonusReward()
          {
            name = "アイテム名1",
            type = 6,
            itemId = 302610650,
            itemNum = 10,
            isGet = false,
            isPickUp = false,
            pickUpText = "",
            day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8U), (object) 6)
          }
        }
      },
      new LoginBonus.NextReward()
      {
        count = 10,
        reward = new List<LoginBonus.LoginBonusReward>()
        {
          new LoginBonus.LoginBonusReward()
          {
            name = "アイテム名1",
            type = 3,
            itemId = 7000400,
            itemNum = 1,
            isGet = false,
            isPickUp = true,
            pickUpText = "",
            day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8U), (object) 7)
          }
        }
      },
      new LoginBonus.NextReward()
      {
        count = 11,
        reward = new List<LoginBonus.LoginBonusReward>()
        {
          new LoginBonus.LoginBonusReward()
          {
            name = "アイテム名1",
            type = 2,
            itemId = 1,
            itemNum = 100000,
            isGet = false,
            isPickUp = true,
            pickUpText = "",
            day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8U), (object) 5)
          }
        }
      },
      new LoginBonus.NextReward()
      {
        count = 12,
        reward = new List<LoginBonus.LoginBonusReward>()
        {
          new LoginBonus.LoginBonusReward()
          {
            name = "アイテム名1",
            type = 6,
            itemId = 302610650,
            itemNum = 10,
            isGet = false,
            isPickUp = false,
            pickUpText = "",
            day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8U), (object) 6)
          }
        }
      },
      new LoginBonus.NextReward()
      {
        count = 13,
        reward = new List<LoginBonus.LoginBonusReward>()
        {
          new LoginBonus.LoginBonusReward()
          {
            name = "アイテム名1",
            type = 4,
            itemId = 20060100,
            itemNum = 1,
            isGet = false,
            isPickUp = true,
            pickUpText = "",
            day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8U), (object) 7)
          }
        }
      },
      new LoginBonus.NextReward()
      {
        count = 14,
        reward = new List<LoginBonus.LoginBonusReward>()
        {
          new LoginBonus.LoginBonusReward()
          {
            name = "アイテム名1",
            type = 1,
            itemId = 1,
            itemNum = 5,
            isGet = false,
            isPickUp = false,
            pickUpText = "",
            day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8U), (object) 5)
          }
        }
      },
      new LoginBonus.NextReward()
      {
        count = 15,
        reward = new List<LoginBonus.LoginBonusReward>()
        {
          new LoginBonus.LoginBonusReward()
          {
            name = "アイテム名1",
            type = 6,
            itemId = 302610650,
            itemNum = 10,
            isGet = false,
            isPickUp = false,
            pickUpText = "",
            day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8U), (object) 6)
          }
        }
      },
      new LoginBonus.NextReward()
      {
        count = 16 /*0x10*/,
        reward = new List<LoginBonus.LoginBonusReward>()
        {
          new LoginBonus.LoginBonusReward()
          {
            name = "アイテム名1",
            type = 4,
            itemId = 20060100,
            itemNum = 1,
            isGet = false,
            isPickUp = true,
            pickUpText = "",
            day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8U), (object) 7)
          }
        }
      }
    };
    this.dummyLogBo.reward = new List<LoginBonus.LoginBonusReward>()
    {
      new LoginBonus.LoginBonusReward()
      {
        name = "リワード",
        type = 1,
        itemId = 1,
        itemNum = 1,
        isGet = true,
        isPickUp = false,
        pickUpText = "",
        day = string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 8U), (object) 4)
      }
    };
    this.dummyLogBo.name = "スペシャル";
    this.dummyLogBo.type = 1;
    this.dummyLogBo.total = 6;
    this.dummyLogBo.rotate = 0;
    this.dummyLogBo.nowCount = 6;
    this.dummyLogBo.priority = 1;
    this.dummyLogBo.period_announce = "期間指定の文字列";
    this.dummyLogBo.boardType = 2;
    this.dummyLogBo.loginBonusId = 1;
  }

  private enum UI
  {
    TEX_LOGIN_BANNER,
    LBL_PERIOD,
    LBL_PICKUP,
    LBL_LOGIN_DAYS,
    GRD_BONUSLIST,
    SCR_BONUSLIST,
    SPR_FRAME,
    LBL_INFO_NUM,
    LBL_INFO_NAME,
    LBL_INFODETAIL_NUM,
    LBL_INFODETAIL_NAME,
    LBL_INFODETAIL_DESC,
    OBJ_ICON_ROOT,
    LBL_DAY,
    LBL_DAY_PICKUP,
    LBL_DAY_FINE,
    LBL_ITEMNUM,
    SPR_DAY_BASE,
    SPR_DAY_BASE_PICKUP,
    SPR_DAY_BASE_FINE,
    SPR_STAMP,
    SPR_STAMP_ANIM,
    TEX_MODEL,
    TEX_INNER_MODEL,
    OBJ_DETAIL_ROOT,
    SPR_FRAME_PILLER_L,
    SPR_FRAME_PILLER_R,
    SPR_FRAME_FOOTER_L,
    SPR_FRAME_FOOTER_R,
    SPR_FRAME_BG_1_L,
    SPR_FRAME_BG_1_R,
    SPR_FRAME_BG_2_L,
    SPR_FRAME_BG_2_R,
    SPR_FRAME_BG_3_L,
    SPR_FRAME_BG_3_R,
    SPR_FRAME_BG_4_L,
    SPR_FRAME_BG_4_R,
    SPR_FRAME_ICONS,
    BTN_CLOSE,
  }

  private enum BG_COLOR
  {
    RED = 1,
    YELLOW = 2,
    BLUE = 3,
  }

  private enum AUDIO
  {
    REQUEST_COMPLETE = 40000029, // 0x02625A1D
  }
}
