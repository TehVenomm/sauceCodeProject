// Decompiled with JetBrains decompiler
// Type: ShopTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using OnePF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

#nullable disable
public class ShopTop : SkillInfoBase
{
  private const string STR_ENEMY_TEX_ITEM = "enemy_tex_";
  private const string STR_SKILL_TEX_ITEM = "skill_tex_";
  private const string SPR_NAME_CRYSTAL = "Juel5";
  private const string SPR_NAME_TICKET = "Ticket";
  private const int PAGE_GACHA_QUEST = 0;
  private const int PAGE_GACHA_MAGI = 1;
  private string selectProductId = "";
  private bool isPurchase;
  private string pp;
  private bool _isFinishGetNativeProductlist;
  private StoreDataList _nativeStoreList;
  private List<Transform> ticketTitleRootList = new List<Transform>();
  private List<ShopTop.GachaModelInfo> gachaModelInfo;
  private List<uint> pickUpMaterialIDs;
  private int pageIndex;
  private bool isSectionStarted;
  private List<ShopTop.GachaUIInfo> gachaUIInfoList = new List<ShopTop.GachaUIInfo>();
  private List<UITable> gachaBtn = new List<UITable>();
  private LoadingQueue loadQueue;
  private bool isFinished;
  private List<Coroutine> coroutineList = new List<Coroutine>();
  private bool isDoGacha;
  private int currentCrystalRequestCount;
  private const float PICKUP_UPDATE_TIME = 5f;
  private float timer;

  public override bool useOnPressBackKey => true;

  public override void OnPressBackKey() => this.DispatchEvent(GameSection.GetGoingHomeEvent());

  public override void Initialize()
  {
    this.pageIndex = MonoBehaviourSingleton<GachaManager>.I.selectGachaType == GACHA_TYPE.QUEST || MonoBehaviourSingleton<GachaManager>.I.selectGachaType == (GACHA_TYPE) 0 ? 0 : 1;
    this.pickUpMaterialIDs = new List<uint>();
    if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1))
    {
      MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_8_gacha, "Tutorial");
      Debug.LogWarning((object) ("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_8_gacha.ToString()));
      MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_8_gacha, "Tutorial");
    }
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    EnemyLoader.CacheUIElementEffects(load_queue);
    bool wait = true;
    MonoBehaviourSingleton<GachaManager>.I.SendGetGacha((Action<bool>) (b => wait = false));
    while (wait)
      yield return (object) null;
    List<string> productIds = new List<string>();
    MonoBehaviourSingleton<GachaManager>.I.gachaData.types.ForEach((Action<GachaList.GachaType>) (data => data.groups.ForEach((Action<GachaList.GachaGroup>) (group => group.gachas.ForEach((Action<GachaList.Gacha>) (gacha =>
    {
      if (string.IsNullOrEmpty(gacha.productId))
        return;
      productIds.Add(gacha.productId);
    }))))));
    MonoBehaviourSingleton<ShopReceiver>.I.onGetProductDatas += new Action<StoreDataList>(this.OnGetProductDatas);
    this._isFinishGetNativeProductlist = false;
    Native.GetProductDatas(string.Join("----", productIds.ToArray()));
    while (!this._isFinishGetNativeProductlist)
      yield return (object) null;
    this.gachaModelInfo = new List<ShopTop.GachaModelInfo>();
    MonoBehaviourSingleton<GachaManager>.I.gachaData.types.ForEach((Action<GachaList.GachaType>) (data =>
    {
      GACHA_TYPE gacha_type = data.ViewType;
      ShopTop.GachaModelInfo add_data = this.gachaModelInfo.Find((Predicate<ShopTop.GachaModelInfo>) (g => g.type == gacha_type));
      if (add_data == null)
      {
        add_data = new ShopTop.GachaModelInfo();
        add_data.type = gacha_type;
        add_data.url = data.url;
        add_data.gachaDataInfo = new List<ShopTop.GachaModelInfo.GachaDataInfo>();
        this.gachaModelInfo.Add(add_data);
      }
      switch (gacha_type)
      {
        case GACHA_TYPE.SKILL:
          add_data.sortPriority = 2;
          data.groups.ForEach((Action<GachaList.GachaGroup>) (groups =>
          {
            ShopTop.GachaModelInfo.GachaDataInfo gacha_data_info = new ShopTop.GachaModelInfo.GachaDataInfo();
            gacha_data_info.showPickupIndex = 0;
            gacha_data_info.groupID = groups.group;
            gacha_data_info.bannerImg = groups.bannerImg;
            gacha_data_info.url = groups.url;
            gacha_data_info.gachas = groups.gachas;
            gacha_data_info.gachaGuaranteeCampaignInfos = groups.gachaGuaranteeCampaignInfo;
            gacha_data_info.note = groups.note;
            gacha_data_info.priority = groups.priority;
            gacha_data_info.counter = groups.counter;
            Debug.Log((object) ("Gacha Info Counter: " + (object) gacha_data_info.counter));
            gacha_data_info.expireAt = groups.expireAt;
            if (gacha_data_info.gachas != null && gacha_data_info.gachas[0] != null)
              gacha_data_info.buttonImgId = groups.gachas[0].buttonImg;
            gacha_data_info.pickup = new List<ShopTop.PickUp>();
            groups.pickupLineups.ForEach((Action<GachaList.GachaLineup>) (pickup_data => gacha_data_info.pickup.Add((ShopTop.PickUp) new ShopTop.PickUpSkill(pickup_data.orderNo, pickup_data.itemId, pickup_data.anim))));
            add_data.gachaDataInfo.Add(gacha_data_info);
          }));
          break;
        case GACHA_TYPE.QUEST:
          add_data.sortPriority = 1;
          data.groups.ForEach((Action<GachaList.GachaGroup>) (groups =>
          {
            ShopTop.GachaModelInfo.GachaDataInfo gacha_data_info = new ShopTop.GachaModelInfo.GachaDataInfo();
            gacha_data_info.showPickupIndex = 0;
            gacha_data_info.groupID = groups.group;
            gacha_data_info.bannerImg = groups.bannerImg;
            gacha_data_info.url = groups.url;
            gacha_data_info.gachas = groups.gachas;
            gacha_data_info.gachaGuaranteeCampaignInfos = groups.gachaGuaranteeCampaignInfo;
            gacha_data_info.friendPromotionInfo = groups.friendPromotionInfo;
            gacha_data_info.note = groups.note;
            gacha_data_info.priority = groups.priority;
            gacha_data_info.counter = groups.counter;
            Debug.Log((object) ("Gacha Info Counter: " + (object) gacha_data_info.counter));
            gacha_data_info.expireAt = groups.expireAt;
            if (gacha_data_info.gachas != null && gacha_data_info.gachas[0] != null)
              gacha_data_info.buttonImgId = groups.gachas[0].buttonImg;
            gacha_data_info.pickup = new List<ShopTop.PickUp>();
            groups.pickupLineups.ForEach((Action<GachaList.GachaLineup>) (pickup_data =>
            {
              int reward_pri = -1;
              uint reward_id = 0;
              pickup_data.sellItems.ForEach((Action<QuestItem.SellItem>) (reward_data =>
              {
                if (reward_data.type != 3)
                  return;
                ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint) reward_data.itemId);
                if (itemData == null || itemData.type == ITEM_TYPE.USE_ITEM || itemData.type == ITEM_TYPE.FAIRY || itemData.type == ITEM_TYPE.NONE || reward_id != 0U && reward_pri != -1 && reward_pri <= reward_data.pri)
                  return;
                reward_pri = reward_data.pri;
                reward_id = (uint) reward_data.itemId;
              }));
              uint equip = 0;
              CreateEquipItemTable.CreateEquipItemData[] creatableEquipItem = Singleton<CreateEquipItemTable>.I.GetCreatableEquipItem(reward_id);
              if (creatableEquipItem != null && creatableEquipItem.Length != 0)
                equip = creatableEquipItem[0].equipItemID;
              gacha_data_info.pickup.Add((ShopTop.PickUp) new ShopTop.PickUpQuest(pickup_data.orderNo, pickup_data.itemId, reward_id, equip));
              this.CacheQuestAudio((uint) pickup_data.itemId, load_queue);
            }));
            gacha_data_info.pickup.Sort((Comparison<ShopTop.PickUp>) ((l, r) => l.orderNo - r.orderNo));
            add_data.gachaDataInfo.Add(gacha_data_info);
          }));
          break;
        default:
          add_data.sortPriority = 3;
          break;
      }
      add_data.gachaDataInfo.Sort((Comparison<ShopTop.GachaModelInfo.GachaDataInfo>) ((l, r) => r.priority - l.priority));
    }));
    this.gachaModelInfo.Sort((Comparison<ShopTop.GachaModelInfo>) ((l, r) => l.sortPriority - r.sortPriority));
    if (load_queue.IsLoading())
      yield return (object) load_queue.Wait();
    base.Initialize();
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY) == (GameSection.NOTIFY_FLAG) 0 || !MonoBehaviourSingleton<InventoryManager>.IsValid() || this.pageIndex != 0)
      return;
    string text = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((Predicate<ItemInfo>) (x => x.tableData.type == ITEM_TYPE.TICKET)).ToString();
    int index = 0;
    for (int count = this.ticketTitleRootList.Count; index < count; ++index)
      this.SetLabelText(this.ticketTitleRootList[index], (Enum) ShopTop.UI.LBL_HAVE, text);
  }

  public override void StartSection()
  {
    GachaList.Gacha latestPopupGacha = this.GetLatestPopupGacha();
    GachaGuaranteeCampaignInfo latestPopupGualantee = this.GetLatestPopupGualantee();
    DateTime startDateTime;
    string campaignDetailImg;
    if (latestPopupGualantee == null)
    {
      startDateTime = latestPopupGacha.GetStartDateTime();
      campaignDetailImg = latestPopupGacha.campaignDetailImg;
    }
    else if (latestPopupGualantee.GetStartDateTime() > latestPopupGacha.GetStartDateTime())
    {
      startDateTime = latestPopupGualantee.GetStartDateTime();
      campaignDetailImg = latestPopupGualantee.campaignDetailImg;
    }
    else
    {
      startDateTime = latestPopupGacha.GetStartDateTime();
      campaignDetailImg = latestPopupGacha.campaignDetailImg;
    }
    bool flag = this.CheckShowPopUp(startDateTime, campaignDetailImg);
    if (!MonoBehaviourSingleton<GachaManager>.I.IsTutorial() & flag)
      this.ShowPopUp(campaignDetailImg);
    this.isSectionStarted = true;
    base.StartSection();
  }

  private bool CheckShowPopUp(DateTime startDate, string campaignDetailImg)
  {
    if (campaignDetailImg == "")
      return false;
    if (!MonoBehaviourSingleton<GachaManager>.I.HasBeenShowAdvertisement())
    {
      MonoBehaviourSingleton<GachaManager>.I.SetTimeShowShopAdvertisement(startDate);
      return true;
    }
    if (!(MonoBehaviourSingleton<GachaManager>.I.GetTimeShowShopAdvertisement() < startDate))
      return false;
    MonoBehaviourSingleton<GachaManager>.I.SetTimeShowShopAdvertisement(startDate);
    return true;
  }

  private void ShowPopUp(string popupImageName)
  {
    if (!(popupImageName != ""))
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[1]
    {
      new EventData("POP_UP", (object) popupImageName)
    });
  }

  private List<GachaGuaranteeCampaignInfo> GetStepUpInfos()
  {
    List<GachaGuaranteeCampaignInfo> stepUpInfos = new List<GachaGuaranteeCampaignInfo>();
    for (int index1 = 0; index1 < this.gachaModelInfo.Count; ++index1)
    {
      List<ShopTop.GachaModelInfo.GachaDataInfo> gachaDataInfo = this.gachaModelInfo[index1].gachaDataInfo;
      for (int index2 = 0; index2 < gachaDataInfo.Count; ++index2)
      {
        List<GachaGuaranteeCampaignInfo> all = gachaDataInfo[index2].gachaGuaranteeCampaignInfos.FindAll((Predicate<GachaGuaranteeCampaignInfo>) (g => g.IsStepUp()));
        if (all != null)
          stepUpInfos.AddRange((IEnumerable<GachaGuaranteeCampaignInfo>) all);
      }
    }
    return stepUpInfos;
  }

  private GachaList.Gacha GetLatestPopupGacha()
  {
    GachaList.Gacha latestPopupGacha = (GachaList.Gacha) null;
    for (int index1 = 0; index1 < this.gachaModelInfo.Count; ++index1)
    {
      for (int index2 = 0; index2 < this.gachaModelInfo[index1].gachaDataInfo.Count; ++index2)
      {
        for (int index3 = 0; index3 < this.gachaModelInfo[index1].gachaDataInfo[index2].gachas.Count; ++index3)
        {
          GachaList.Gacha gacha = this.gachaModelInfo[index1].gachaDataInfo[index2].gachas[index3];
          if (!(gacha.campaignDetailImg == ""))
          {
            DateTime startDateTime = gacha.GetStartDateTime();
            if (latestPopupGacha == null || !(latestPopupGacha.GetStartDateTime() >= startDateTime))
              latestPopupGacha = gacha;
          }
        }
      }
    }
    return latestPopupGacha;
  }

  private GachaGuaranteeCampaignInfo GetLatestPopupGualantee()
  {
    List<GachaGuaranteeCampaignInfo> stepUpInfos = this.GetStepUpInfos();
    if (stepUpInfos.Count == 0)
      return (GachaGuaranteeCampaignInfo) null;
    GachaGuaranteeCampaignInfo latestPopupGualantee = (GachaGuaranteeCampaignInfo) null;
    int count = stepUpInfos.Count;
    for (int index = 0; index < count; ++index)
    {
      GachaGuaranteeCampaignInfo guaranteeCampaignInfo = stepUpInfos[index];
      if (!(guaranteeCampaignInfo.campaignDetailImg == ""))
      {
        DateTime startDateTime = guaranteeCampaignInfo.GetStartDateTime();
        if (latestPopupGualantee == null || !(latestPopupGualantee.GetStartDateTime() >= startDateTime))
          latestPopupGualantee = guaranteeCampaignInfo;
      }
    }
    return latestPopupGualantee;
  }

  protected void CacheQuestAudio(uint quest_id, LoadingQueue lo_queue)
  {
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(quest_id);
    if (questData == null)
      return;
    int mainEnemyId = questData.GetMainEnemyID();
    if (!Singleton<EnemyTable>.IsValid())
      return;
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) mainEnemyId);
    if (enemyData == null)
      return;
    this.CacheEnemyAudio(enemyData, lo_queue);
  }

  protected void CacheEnemyAudio(EnemyTable.EnemyData enemyData, LoadingQueue lo_queue)
  {
    if (lo_queue == null)
      return;
    OutGameSettingsManager.EnemyDisplayInfo enemyDisplayInfo = MonoBehaviourSingleton<OutGameSettingsManager>.I.SearchEnemyDisplayInfoForGacha(enemyData);
    if (enemyDisplayInfo == null || enemyDisplayInfo.seIdGachaShort <= 0)
      return;
    lo_queue.CacheSE(enemyDisplayInfo.seIdhowl);
  }

  protected override void OnOpen()
  {
    MonoBehaviourSingleton<UIManager>.I.enableShadow = true;
    MonoBehaviourSingleton<ShopReceiver>.I.onBuyGacha = (Action<string>) null;
    MonoBehaviourSingleton<ShopReceiver>.I.onBuyGacha += new Action<string>(this.OnBuyItem);
    MonoBehaviourSingleton<ShopReceiver>.I.onBuyItem += new Action<string>(this.OnBuyItem);
    this.isPurchase = false;
    base.OnOpen();
  }

  protected override void OnClose()
  {
    MonoBehaviourSingleton<UIManager>.I.enableShadow = false;
    base.OnClose();
  }

  public override void Close(UITransition.TYPE type)
  {
    base.Close(type);
    this.DeleteModel();
  }

  private void DeleteModel()
  {
    if (this.gachaModelInfo == null)
      return;
    Transform ctrl = this.GetCtrl((Enum) ShopTop.UI.SCR_LIST_2);
    if (!Object.op_Inequality((Object) ctrl, (Object) null) || ctrl.childCount <= 0)
      return;
    int num = 0;
    for (int childCount = ctrl.childCount; num < childCount; ++num)
    {
      Transform child = ctrl.GetChild(num);
      if (((Object) child).name.Contains("enemy_tex_"))
      {
        int result;
        if (int.TryParse(((Object) child).name.Remove(0, "enemy_tex_".Length), out result))
        {
          Transform root = this.GetCtrl((Enum) ShopTop.UI.SCR_LIST_2).Find("enemy_tex_" + (object) result);
          this.DeleteRenderTexture(root, (Enum) ShopTop.UI.TEX_ENEMY_MODEL);
          this.SetVisibleWidgetEffect((Enum) ShopTop.UI.SCR_LIST_2, root, (Enum) ShopTop.UI.TEX_ENEMY_MODEL, (string) null);
        }
      }
      else
      {
        int result;
        if (((Object) child).name.Contains("skill_tex_") && int.TryParse(((Object) child).name.Remove(0, "skill_tex_".Length), out result))
        {
          Transform root = this.GetCtrl((Enum) ShopTop.UI.SCR_LIST_2).Find("skill_tex_" + (object) result);
          this.DeleteRenderTexture(root, (Enum) ShopTop.UI.TEX_SKILL_NPC_MODEL);
          this.DeleteRenderTexture(root, (Enum) ShopTop.UI.TEX_SKILL_SUB_NPC_MODEL);
          this.DeleteRenderTexture(root, (Enum) ShopTop.UI.TEX_SKILL_BANNER);
        }
      }
    }
  }

  private void SetGachaChangeButton(int pageIndex)
  {
    this.SetActive((Enum) ShopTop.UI.BTN_QUEST_GACHA, pageIndex != 0);
    this.SetActive((Enum) ShopTop.UI.BTN_MAGI_GACHA, pageIndex == 0);
  }

  private void SetGachaListUI()
  {
    this.SetGachaChangeButton(this.pageIndex);
    this.gachaUIInfoList.Clear();
    this.gachaBtn.Clear();
    int count = 0;
    if (this.gachaModelInfo != null && this.gachaModelInfo.Count > this.pageIndex && this.gachaModelInfo[this.pageIndex].gachaDataInfo != null)
    {
      count = this.gachaModelInfo[this.pageIndex].gachaDataInfo.Count;
      count *= 2;
    }
    int item_num = count != 0 ? count + 1 : 0;
    this.StopLoadCoroutine();
    this.SetTable((Enum) ShopTop.UI.TBL_LIST, "GachaListItem", item_num, false, (Func<int, Transform, Transform>) ((i, p) =>
    {
      if (i < count)
      {
        if (i % 2 == 0)
          return this.Realizes("GachaBanner", p);
        return this.gachaModelInfo[this.pageIndex].type == GACHA_TYPE.SKILL ? this.Realizes("GachaSkillListItem", p) : (Transform) null;
      }
      return i == count ? this.Realizes("GachaDescriptionItem", p) : (Transform) null;
    }), (Action<int, Transform, bool>) ((i, t, b) =>
    {
      if (i < count)
      {
        int index = i / 2;
        ShopTop.GachaModelInfo gachaModelInfo = this.gachaModelInfo[this.pageIndex];
        ShopTop.GachaModelInfo.GachaDataInfo gacha_info = gachaModelInfo.gachaDataInfo[index];
        if (i % 2 == 0)
        {
          this.SetGachaBanner(t, gacha_info);
        }
        else
        {
          if (gachaModelInfo.type == GACHA_TYPE.QUEST)
            this.SetGachaNote(t, gacha_info);
          this.SetGachaButtonsTable(t, gacha_info);
          this.gachaUIInfoList.Add(new ShopTop.GachaUIInfo()
          {
            index = index,
            parent = t,
            gachaModelInfo = gachaModelInfo
          });
        }
      }
      else
      {
        if (i != count)
          return;
        ((Component) this.FindCtrl(t, (Enum) ShopTop.UI.LBL_GACHA_DESCRIPTION)).GetComponent<UILabel>().supportEncoding = true;
        string str = string.IsNullOrEmpty(this.gachaModelInfo[this.pageIndex].url) ? (string) null : StringTable.Get(STRING_CATEGORY.SHOP, (uint) (GameDefine.GACHA_VIEW_PROBABILITY + this.pageIndex));
        this.SetLabelText(t, (Enum) ShopTop.UI.LBL_GACHA_DESCRIPTION, StringTable.Get(STRING_CATEGORY.SHOP, (uint) (10 + this.pageIndex)) + str);
        this.SetActive(t, (Enum) ShopTop.UI.BTN_VIEW_PROBABIRITY, !string.IsNullOrEmpty(this.gachaModelInfo[this.pageIndex].url));
      }
    }));
    this.RepositionTables();
  }

  private void SetGachaBanner(Transform t, ShopTop.GachaModelInfo.GachaDataInfo gacha_info)
  {
    bool is_visible = !string.IsNullOrEmpty(gacha_info.bannerImg);
    this.SetActive(t, is_visible);
    if (!is_visible)
      return;
    string bannerImg = gacha_info.bannerImg;
    this.coroutineList.Add(this.StartCoroutine(this.LoadGachaBanner(t, (Enum) ShopTop.UI.TEX_GACHA_BANNER, bannerImg)));
    this.SetButtonEnabled(this.FindCtrl(t, (Enum) ShopTop.UI.BTN_GACHA_BANNER), !string.IsNullOrEmpty(gacha_info.url));
    this.SetEvent(this.FindCtrl(t, (Enum) ShopTop.UI.BTN_GACHA_BANNER), "GACHA_BANNER", (object) gacha_info);
  }

  private void SetGachaNote(Transform t, ShopTop.GachaModelInfo.GachaDataInfo gacha_info)
  {
    if (!string.IsNullOrEmpty(gacha_info.note))
    {
      ((Component) this.FindCtrl(t, (Enum) ShopTop.UI.SPR_NOTE_BG)).gameObject.SetActive(true);
      ((Component) this.FindCtrl(t, (Enum) ShopTop.UI.LBL_NOTE)).gameObject.SetActive(true);
      if (gacha_info.note.Contains("\\n"))
      {
        gacha_info.note = gacha_info.note.Replace("\\n", "\n");
        int num = gacha_info.note.Length - gacha_info.note.Replace("\n", "").Length;
        if (num > 0)
        {
          Transform ctrl = this.FindCtrl(t, (Enum) ShopTop.UI.OBJ_BTN_ROOT);
          Vector3 localPosition = ((Component) ctrl).transform.localPosition;
          ((Component) ctrl).transform.localPosition = new Vector3(localPosition.x, (float) (-204.0 - (double) (num + 1) * 16.0), localPosition.z);
          ((Component) t).gameObject.GetComponent<UIWidget>().height = 471 + (num + 1) * 16 /*0x10*/;
        }
      }
      this.SetLabelText(t, (Enum) ShopTop.UI.LBL_NOTE, gacha_info.note);
    }
    else
    {
      ((Component) this.FindCtrl(t, (Enum) ShopTop.UI.SPR_NOTE_BG)).gameObject.SetActive(false);
      ((Component) this.FindCtrl(t, (Enum) ShopTop.UI.LBL_NOTE)).gameObject.SetActive(false);
      Transform ctrl = this.FindCtrl(t, (Enum) ShopTop.UI.OBJ_BTN_ROOT);
      Vector3 localPosition = ((Component) ctrl).transform.localPosition;
      ((Component) ctrl).transform.localPosition = new Vector3(localPosition.x, -188f, localPosition.z);
    }
  }

  private void SetGachaButtonsTable(
    Transform parent,
    ShopTop.GachaModelInfo.GachaDataInfo gacha_info)
  {
    List<int> subGroupIds = new List<int>();
    int count = gacha_info.gachas.Count;
    for (int index = 0; index < count; ++index)
    {
      GachaList.Gacha gacha = gacha_info.gachas[index];
      if (!subGroupIds.Contains(gacha.subGroup))
        subGroupIds.Add(gacha.subGroup);
    }
    List<int> ticketTitleIndexList = new List<int>();
    List<GachaList.Gacha> gachaList1 = (List<GachaList.Gacha>) null;
    List<int> promotionGachaIndexList = new List<int>();
    int num1 = 0;
    for (int index = 0; index < subGroupIds.Count; ++index)
    {
      int subGroupId = subGroupIds[index];
      List<GachaList.Gacha> list = gacha_info.gachas.Where<GachaList.Gacha>((Func<GachaList.Gacha, bool>) (g => g.subGroup == subGroupId)).ToList<GachaList.Gacha>();
      GachaList.Gacha gacha = list.First<GachaList.Gacha>();
      int requiredItemId = gacha.requiredItemId;
      int num2 = index + ticketTitleIndexList.Count;
      if (requiredItemId > 0 && (gachaList1 == null || requiredItemId != num1))
        ticketTitleIndexList.Add(num2);
      else if (gacha_info.GetGachaFriendPromotionInfo(gacha.gachaId) != null)
        promotionGachaIndexList.Add(num2);
      num1 = requiredItemId;
      gachaList1 = list;
    }
    this.gachaBtn.Add(((Component) this.FindCtrl(parent, (Enum) ShopTop.UI.GRD_BTN)).GetComponent<UITable>());
    int tableListCount = subGroupIds.Count + ticketTitleIndexList.Count;
    bool isHaveGachaTicket = false;
    int requireItemId = num1;
    if (this.gachaModelInfo[this.pageIndex].type == GACHA_TYPE.QUEST && num1 > 0)
    {
      isHaveGachaTicket = true;
      ++tableListCount;
    }
    int ticketNumOfGroup = 0;
    this.SetTable(parent, (Enum) ShopTop.UI.GRD_BTN, "GachaButtonItemTwoLine", tableListCount, false, (Func<int, Transform, Transform>) ((i, t) =>
    {
      if (this.gachaModelInfo[this.pageIndex].type == GACHA_TYPE.QUEST & isHaveGachaTicket && i == tableListCount - 1)
        return this.Realizes("GachaTicketNumOwned", t);
      if (ticketTitleIndexList.Exists((Predicate<int>) (x => x == i)))
      {
        if (gacha_info.counter >= 0)
          return this.Realizes("GachaTicketTitleCounterS", t);
        Transform transform = this.Realizes("GachaTicketTitle", t);
        this.ticketTitleRootList.Add(transform);
        return transform;
      }
      return promotionGachaIndexList.Contains(i) ? this.Realizes("GachaButtonItemFriendPromotion", t) : (Transform) null;
    }), (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      if (this.gachaModelInfo[this.pageIndex].type == GACHA_TYPE.QUEST & isHaveGachaTicket && i == tableListCount - 1)
      {
        if (ticketNumOfGroup < 0)
          return;
        this.SetSupportEncoding(t, (Enum) ShopTop.UI.LBL_MORE_TICKET, true);
        this.SetLabelText(t, (Enum) ShopTop.UI.LBL_CRYSTAL_NUM, ticketNumOfGroup.ToString());
        ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint) requireItemId);
        ResourceLoad.LoadItemIconTexture(((Component) this.FindCtrl(t, (Enum) ShopTop.UI.TEX_TICKET_HAVE)).GetComponent<UITexture>(), itemData.iconID);
      }
      else
      {
        int gachaSubGroupIndex = this.CreateGachaSubGroupIndex(i, ticketTitleIndexList);
        int subGroupId = subGroupIds[gachaSubGroupIndex];
        List<GachaList.Gacha> list1 = gacha_info.gachas.Where<GachaList.Gacha>((Func<GachaList.Gacha, bool>) (g => g.subGroup == subGroupId)).ToList<GachaList.Gacha>();
        GachaList.Gacha firstGacha = list1.First<GachaList.Gacha>();
        if (ticketTitleIndexList.Exists((Predicate<int>) (x => x == i)))
        {
          int id = firstGacha.requiredItemId;
          int itemNum = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((Predicate<ItemInfo>) (x => (long) x.tableData.id == (long) id), 1);
          ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint) id);
          this.SetLabelText(t, (Enum) ShopTop.UI.LBL_TITLE, itemData.name);
          this.SetLabelText(t, (Enum) ShopTop.UI.LBL_HAVE, itemNum.ToString());
          ticketNumOfGroup = itemNum;
          if (gacha_info.counter > 0)
          {
            this.SetActive(t, (Enum) ShopTop.UI.S_COUNTER, true);
            this.SetLabelText(t, (Enum) ShopTop.UI.COUNTER_LBL, (object) gacha_info.counter);
            this.SetActive(t, (Enum) ShopTop.UI.NUMBER_COUNTER_IMG, true);
            ((Component) this.FindCtrl(t, (Enum) ShopTop.UI.NUMBER_COUNTER_IMG)).GetComponent<UISprite>().spriteName = gacha_info.counter.ToString();
            ((Component) this.FindCtrl(t, (Enum) ShopTop.UI.COUNTER_PROGRESSBAR_FOREGROUND)).GetComponent<UISprite>().fillAmount = (float) (10 - gacha_info.counter) / 10f;
            this.SetActive(t, (Enum) ShopTop.UI.S_AVAILABLE, false);
          }
          else
          {
            this.SetActive(t, (Enum) ShopTop.UI.S_COUNTER, false);
            this.SetActive(t, (Enum) ShopTop.UI.S_AVAILABLE, true);
          }
        }
        else
        {
          GachaGuaranteeCampaignInfo gachaGuaranteeInfo = (GachaGuaranteeCampaignInfo) null;
          GachaFriendPromotionInfo friendPromotionInfo = gacha_info.GetGachaFriendPromotionInfo(firstGacha.gachaId);
          if (friendPromotionInfo != null)
          {
            this.SetUpFriendPromotionGachaTableItem(t, friendPromotionInfo, firstGacha);
          }
          else
          {
            gachaGuaranteeInfo = gacha_info.GetGachaGuaranteeCampaignInfo(list1);
            this.SetGachaDetailUI(t, gacha_info, list1, gachaGuaranteeInfo);
          }
          this.SetUpGachaButtonGrid(t, list1, gacha_info, gachaGuaranteeInfo);
          if (gachaGuaranteeInfo != null)
            this.FindCtrl(t, (Enum) ShopTop.UI.SPR_LINE).localPosition = new Vector3(0.0f, -40f, 0.0f);
          List<GachaList.Gacha> gachaList2 = new List<GachaList.Gacha>();
          List<GachaList.Gacha> list2 = list1.Where<GachaList.Gacha>((Func<GachaList.Gacha, bool>) (g => g.detailButtonImg != "")).ToList<GachaList.Gacha>();
          bool flag = gachaGuaranteeInfo != null || list2.Count > 0;
          this.SetActive(t, (Enum) ShopTop.UI.SPR_LINE, i == tableListCount - 1 && !flag);
          if (!(firstGacha.caption != ""))
            return;
          this.SetLabelText(t, (Enum) ShopTop.UI.LBL_GACHA_CAPTION, firstGacha.caption);
          this.SetSupportEncoding(t, (Enum) ShopTop.UI.LBL_GACHA_CAPTION, true);
          this.GetComponent<UIWidget>(t, (Enum) ShopTop.UI.SPR_GACHA_BUTTON_BG).height += 19 * (firstGacha.caption.Count<char>((Func<char, bool>) (c => c == '\n')) + 1);
        }
      }
    }));
  }

  private void SetUpGachaButtonGrid(
    Transform t,
    List<GachaList.Gacha> subGroupGachas,
    ShopTop.GachaModelInfo.GachaDataInfo gachaInfo,
    GachaGuaranteeCampaignInfo gachaGuaranteeInfo)
  {
    int gachaEventTitleCount = 0;
    string gachaEventTitleName = subGroupGachas[0].eventTitleImg;
    if (subGroupGachas.Count<GachaList.Gacha>() == 1 && !string.IsNullOrEmpty(gachaEventTitleName))
    {
      gachaEventTitleCount = 1;
      if (subGroupGachas[0].IsDirectPurchase())
        gachaEventTitleName += "_AND";
    }
    int item_num = subGroupGachas.Count<GachaList.Gacha>() + gachaEventTitleCount;
    this.SetGrid(t, (Enum) ShopTop.UI.GRD_BTN_INNER, "GachaButtonRoot", item_num, false, (Func<int, Transform, Transform>) ((gridIdx, gridTrans) =>
    {
      if (gridIdx >= gachaEventTitleCount)
        return (Transform) null;
      return this.IsTitleMini(gachaInfo, subGroupGachas) ? this.Realizes("GachaEventTitleMini", gridTrans) : this.Realizes("GachaEventTitle", gridTrans);
    }), (Action<int, Transform, bool>) ((gridIdx, gridTrans, gridIsRecycle) =>
    {
      if (gridIdx < gachaEventTitleCount)
      {
        this.coroutineList.Add(this.StartCoroutine(this.LoadGachaEventTitle(gridTrans, (Enum) ShopTop.UI.TEX_GACHA_EVENT_TITLE, gachaEventTitleName)));
      }
      else
      {
        GachaList.Gacha subGroupGacha = subGroupGachas[gridIdx - gachaEventTitleCount];
        string strItemNum;
        if (subGroupGacha.IsOncePurchase())
        {
          strItemNum = "$" + subGroupGacha.yenIncludeTax.ToString();
          if (this._nativeStoreList != null)
            strItemNum = this._nativeStoreList.getProduct(subGroupGacha.productId).price;
        }
        else
          strItemNum = subGroupGacha.requiredItemId == 0 ? "× " + subGroupGacha.crystalNum.ToString() : "× " + subGroupGacha.needItemNum.ToString();
        string empty = string.Empty;
        string buttonImg;
        if (gachaInfo.counter == 0 && subGroupGacha.requiredItemId != 0)
        {
          buttonImg = "BTN_GACHA_TICKET1_Skaku_VER2";
        }
        else
        {
          bool flag = gachaGuaranteeInfo != null && subGroupGacha.gachaId == gachaGuaranteeInfo.gachaId;
          buttonImg = this.CreateButtonName(subGroupGacha, flag ? gachaGuaranteeInfo : (GachaGuaranteeCampaignInfo) null);
        }
        this.coroutineList.Add(this.StartCoroutine(this.LoadGachaButton(gridTrans, buttonImg, strItemNum, subGroupGacha.gachaId, gachaInfo.gachas.IndexOf(subGroupGacha))));
      }
    }));
  }

  private string CreateButtonName(GachaList.Gacha gacha, GachaGuaranteeCampaignInfo guarantee)
  {
    return MonoBehaviourSingleton<GachaManager>.I.CreateButtonBaseName(gacha, guarantee) + "_VER2";
  }

  private bool IsTitleMini(
    ShopTop.GachaModelInfo.GachaDataInfo gachaInfo,
    List<GachaList.Gacha> subGroupGachas)
  {
    return gachaInfo.GetGachaFriendPromotionInfo(subGroupGachas[0].gachaId) != null;
  }

  private int CreateGachaSubGroupIndex(int tableIndex, List<int> ticketTitleIndexList)
  {
    int gachaSubGroupIndex = tableIndex;
    foreach (int ticketTitleIndex in ticketTitleIndexList)
    {
      if (ticketTitleIndex < tableIndex)
        --gachaSubGroupIndex;
      else
        break;
    }
    return gachaSubGroupIndex;
  }

  private void SetGachaDetailUI(
    Transform t,
    ShopTop.GachaModelInfo.GachaDataInfo gachaInfo,
    List<GachaList.Gacha> subGroupGachas,
    GachaGuaranteeCampaignInfo gachaGuaranteeInfo)
  {
    List<GachaList.Gacha> detailImgGachas = subGroupGachas.Where<GachaList.Gacha>((Func<GachaList.Gacha, bool>) (g => g.detailButtonImg != "")).ToList<GachaList.Gacha>();
    bool flag1 = gachaGuaranteeInfo != null;
    if (detailImgGachas.Count == 0 && !flag1)
    {
      this.SetActive(t, (Enum) ShopTop.UI.OBJ_GUARANTEE_HEADER_ROOT, false);
      this.SetActive(t, (Enum) ShopTop.UI.OBJ_GUARANTEE_FOOTER_ROOT, false);
    }
    else
    {
      string event_data = "";
      List<GachaList.Gacha> list;
      string titleImageName;
      string format;
      string link;
      if (flag1)
      {
        list = gachaInfo.gachas.Where<GachaList.Gacha>((Func<GachaList.Gacha, bool>) (g => g.gachaId == gachaGuaranteeInfo.gachaId)).ToList<GachaList.Gacha>();
        titleImageName = gachaGuaranteeInfo.GetTitleImageName();
        format = gachaGuaranteeInfo.endAt;
        link = gachaGuaranteeInfo.link;
      }
      else
      {
        list = detailImgGachas.Where<GachaList.Gacha>((Func<GachaList.Gacha, bool>) (g => g.detailButtonImg == detailImgGachas[0].GetTitleImageName())).ToList<GachaList.Gacha>();
        titleImageName = detailImgGachas[0].GetTitleImageName();
        format = detailImgGachas[0].endDate;
        link = detailImgGachas[0].link;
        event_data = detailImgGachas[0].description;
      }
      int num = subGroupGachas.Contains(list.First<GachaList.Gacha>()) ? 1 : 0;
      bool flag2 = subGroupGachas.Contains(list.Last<GachaList.Gacha>());
      if (num != 0)
      {
        this.SetActive(t, (Enum) ShopTop.UI.OBJ_GUARANTEE_HEADER_ROOT, true);
        this.StartCoroutine(this.LoadGachaGuaranteeCounter(t, (Enum) ShopTop.UI.BTN_GUARANTEE_COUNT_DOWN, gachaGuaranteeInfo.GetImageCount(), titleImageName));
        if (!flag1 || !gachaGuaranteeInfo.IsItemConfirmed())
        {
          Transform ctrl = this.FindCtrl(t, (Enum) ShopTop.UI.BTN_GUARANTEE_COUNT_DOWN);
          ((Behaviour) ((Component) ctrl).GetComponent<UIButton>()).enabled = true;
          if (link == "")
            this.SetEvent(ctrl, "GUARANTEE_GACHA_DETAIL", (object) event_data);
          else
            this.SetEvent(ctrl, "GUARANTEE_GACHA_DETAIL_WEB", (object) link);
        }
        else
        {
          switch ((REWARD_TYPE) gachaGuaranteeInfo.type)
          {
            case REWARD_TYPE.SKILL_ITEM:
              ((Behaviour) ((Component) this.FindCtrl(t, (Enum) ShopTop.UI.BTN_GUARANTEE_COUNT_DOWN)).GetComponent<UIButton>()).enabled = true;
              this.SetEvent(this.FindCtrl(t, (Enum) ShopTop.UI.BTN_GUARANTEE_COUNT_DOWN), "SKILL_DETAIL", (object) new object[2]
              {
                (object) ItemDetailEquip.CURRENT_SECTION.SHOP_TOP,
                (object) Singleton<SkillItemTable>.I.GetSkillItemData((uint) gachaGuaranteeInfo.itemId)
              });
              break;
            case REWARD_TYPE.ACCESSORY:
              ((Behaviour) ((Component) this.FindCtrl(t, (Enum) ShopTop.UI.BTN_GUARANTEE_COUNT_DOWN)).GetComponent<UIButton>()).enabled = true;
              AccessorySortData accessorySortData = new AccessorySortData();
              AccessoryInfo accessoryInfo = new AccessoryInfo();
              accessoryInfo.SetValue((uint) gachaGuaranteeInfo.itemId);
              accessorySortData.SetItem((object) accessoryInfo);
              this.SetEvent(this.FindCtrl(t, (Enum) ShopTop.UI.BTN_GUARANTEE_COUNT_DOWN), "ACCESSORY_SELECT", (object) new object[2]
              {
                (object) ItemDetailEquip.CURRENT_SECTION.SHOP_TOP,
                (object) accessorySortData
              });
              break;
            default:
              ((Behaviour) ((Component) this.FindCtrl(t, (Enum) ShopTop.UI.BTN_GUARANTEE_COUNT_DOWN)).GetComponent<UIButton>()).enabled = false;
              break;
          }
        }
      }
      else
        this.SetActive(t, (Enum) ShopTop.UI.OBJ_GUARANTEE_HEADER_ROOT, false);
      if (flag2)
      {
        this.SetActive(t, (Enum) ShopTop.UI.OBJ_GUARANTEE_FOOTER_ROOT, true);
        StringTable.Get(STRING_CATEGORY.SHOP, 15U);
        this.SetLabelText(t, (Enum) ShopTop.UI.TEX_GUARANTEE_TIME, string.Format(format));
        if (link == "")
          this.SetEvent(this.FindCtrl(t, (Enum) ShopTop.UI.BTN_GUARANTEE_DETAIL), "GUARANTEE_GACHA_DETAIL", (object) event_data);
        else
          this.SetEvent(this.FindCtrl(t, (Enum) ShopTop.UI.BTN_GUARANTEE_DETAIL), "GUARANTEE_GACHA_DETAIL_WEB", (object) link);
      }
      else
        this.SetActive(t, (Enum) ShopTop.UI.OBJ_GUARANTEE_FOOTER_ROOT, false);
    }
  }

  private void SetUpFriendPromotionGachaTableItem(
    Transform t,
    GachaFriendPromotionInfo friendPromotionInfo,
    GachaList.Gacha firstGacha)
  {
    if (friendPromotionInfo == null)
      return;
    string format1 = StringTable.Get(STRING_CATEGORY.SHOP, 15U);
    this.SetLabelText(t, (Enum) ShopTop.UI.LBL_FRIEND_INVITATION_TIME, string.Format(format1, (object) firstGacha.endDate));
    string format2 = StringTable.Get(STRING_CATEGORY.SHOP, 16U /*0x10*/);
    this.SetLabelText(t, (Enum) ShopTop.UI.LBL_FRIEND_INVITATION_REMAIN, string.Format(format2, (object) friendPromotionInfo.remainAllowedCount));
    string format3 = StringTable.Get(STRING_CATEGORY.SHOP, 17U);
    this.SetLabelText(t, (Enum) ShopTop.UI.LBL_FRIEND_INVITATION_INVITED, string.Format(format3, (object) friendPromotionInfo.invitedCount));
  }

  private void UpdateGachaList()
  {
    for (int index = 0; index < this.gachaUIInfoList.Count; ++index)
    {
      ShopTop.GachaUIInfo gachaUiInfo = this.gachaUIInfoList[index];
      if (gachaUiInfo.gachaModelInfo.type == GACHA_TYPE.QUEST)
        this.coroutineList.Add(this.StartCoroutine(this.UpdateQuestList(gachaUiInfo.index, gachaUiInfo.parent)));
      else if (gachaUiInfo.gachaModelInfo.type == GACHA_TYPE.SKILL)
        this.coroutineList.Add(this.StartCoroutine(this.UpdateSkillList(gachaUiInfo.index, gachaUiInfo.parent)));
    }
  }

  private void RepositionTables()
  {
    this.UpdateAnchors();
    for (int index = 0; index < this.gachaBtn.Count; ++index)
      this.gachaBtn[index].Reposition();
    this.GetComponent<UITable>((Enum) ShopTop.UI.TBL_LIST).Reposition();
  }

  public override void UpdateUI()
  {
    UIWidget component = ((Component) this.GetCtrl((Enum) ShopTop.UI.SPR_BG_BLACK)).GetComponent<UIWidget>();
    if (SpecialDeviceManager.HasSpecialDeviceInfo)
    {
      UIVirtualScreen componentInChildren = ((Component) ((Component) this).transform).GetComponentInChildren<UIVirtualScreen>();
      Debug.Log((object) ((Object) ((Component) this).transform).name);
      Debug.Log((object) componentInChildren);
      if (Object.op_Inequality((Object) componentInChildren, (Object) null))
      {
        Debug.Log((object) component);
        component.width = (int) componentInChildren.ScreenWidthFull;
        component.height = (int) componentInChildren.ScreenHeightFull;
      }
    }
    if (!this.isSectionStarted)
      this.SetGachaListUI();
    this.UpdateGachaList();
  }

  private IEnumerator LoadEnemyModel(
    Transform t,
    Enum _enum,
    uint enemy_id,
    string foundation_name,
    ELEMENT_TYPE element_type,
    bool is_Howl)
  {
    yield return (object) null;
    this.SetRenderEnemyModel(t, _enum, enemy_id, foundation_name, OutGameSettingsManager.EnemyDisplayInfo.SCENE.GACHA, (Action<bool, EnemyLoader>) ((ret, loader) =>
    {
      if (!ret || loader == null || loader.body == null)
        return;
      SkinnedMeshRenderer[] componentsInChildren = ((Component) loader.body).GetComponentsInChildren<SkinnedMeshRenderer>(true);
      if (componentsInChildren == null || componentsInChildren.Length == 0)
        return;
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        SkinnedMeshRenderer skinnedMeshRenderer = componentsInChildren[index];
        Bounds localBounds = skinnedMeshRenderer.localBounds;
        ((Bounds) ref localBounds).center = Vector3.zero;
        skinnedMeshRenderer.localBounds = localBounds;
      }
    }), is_Howl: is_Howl);
    if (element_type < ELEMENT_TYPE.MAX)
      this.SetVisibleWidgetEffect((Enum) ShopTop.UI.SCR_LIST_2, t, (Enum) ShopTop.UI.TEX_ENEMY_MODEL, EnemyLoader.GetElementEffectName(element_type));
  }

  private IEnumerator LoadSkillModel(Transform t, Enum _enum, Enum _inner_enum, uint skill_id)
  {
    yield return (object) null;
    this.SetRenderSkillItemModel(t, _enum, skill_id, false, true);
    this.SetRenderSkillItemSymbolModel(t, _inner_enum, skill_id, false);
  }

  private IEnumerator LoadNPCModel(
    Transform t,
    Enum main_npc_enum,
    Enum sub_npc_enum,
    ShopTop.GachaModelInfo.GachaDataInfo gacha_info)
  {
    yield return (object) null;
    this.SetRenderNPCModel(t, main_npc_enum, 1, MonoBehaviourSingleton<OutGameSettingsManager>.I.shopScene.skillNPCPos, MonoBehaviourSingleton<OutGameSettingsManager>.I.shopScene.skillNPCRot, MonoBehaviourSingleton<OutGameSettingsManager>.I.shopScene.skillNPCFOV, (Action<NPCLoader>) (loader =>
    {
      gacha_info.npcLoader = loader;
      PlayerAnimCtrl.Get(loader.animator, PLCA.SKILL_GACHA_TOP);
    }));
    this.SetRenderNPCModel(t, sub_npc_enum, 501, MonoBehaviourSingleton<OutGameSettingsManager>.I.shopScene.skillCatNPCPos, MonoBehaviourSingleton<OutGameSettingsManager>.I.shopScene.skillCatNPCRot, MonoBehaviourSingleton<OutGameSettingsManager>.I.shopScene.skillNPCFOV, (Action<NPCLoader>) (loader => PlayerAnimCtrl.Get(loader.animator, PLCA.LIE)));
  }

  private IEnumerator LoadSkillBanner(
    Transform t,
    Enum root_enum,
    Enum tex_enum,
    SkillItemTable.SkillItemData table,
    ShopTop.GachaModelInfo.GachaDataInfo gacha_info,
    ShopTop.PickUpSkill pickup)
  {
    if (this.loadQueue == null)
      this.loadQueue = new LoadingQueue((MonoBehaviour) this);
    int pattern_index = 0;
    switch (pickup.gachaAnim.pattern)
    {
      case "B":
        pattern_index = 1;
        break;
      case "C":
        pattern_index = 2;
        break;
      case "D":
        pattern_index = 3;
        break;
      case "E":
        pattern_index = 4;
        break;
      default:
        pattern_index = 0;
        break;
    }
    LoadObject lo_image = this.loadQueue.Load(true, RESOURCE_CATEGORY.SHOP_IMG, ResourceName.GetSkillGachaBannerImage((int) table.id));
    if (this.loadQueue.IsLoading())
      yield return (object) this.loadQueue.Wait();
    while (Object.op_Equality((Object) gacha_info.npcLoader, (Object) null))
      yield return (object) null;
    Transform ctrl = this.FindCtrl(t, root_enum);
    if (Object.op_Inequality((Object) ctrl, (Object) null))
    {
      Transform transform = this.SetPrefab(ctrl, "GachaSkillBannerAnim");
      if (Object.op_Inequality((Object) transform, (Object) null))
      {
        GachaSkillBannerAnim component = ((Component) transform).GetComponent<GachaSkillBannerAnim>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          component.Init(pattern_index, table, lo_image.loadedObject as Texture, pickup.gachaAnim);
          if (gacha_info.pickup.Count == 1 || MonoBehaviourSingleton<GachaManager>.I.IsTutorialSkillGacha())
          {
            bool is_skip = gacha_info.isFirstSkillListDirection;
            component.Entry(is_skip, (EventDelegate.Callback) (() => PlayerAnimCtrl.Get(gacha_info.npcLoader.animator, is_skip ? PLCA.SKILL_GACHA_TOP : PLCA.SKILL_GACHA_TOP_SLIDE_END)));
            gacha_info.isFirstSkillListDirection = false;
            component.WaitAndNextPickup((EventDelegate.Callback) (() => PlayerAnimCtrl.Get(gacha_info.npcLoader.animator, PLCA.SKILL_GACHA_TOP_SLIDE)));
          }
          else
          {
            bool is_skip = gacha_info.isFirstSkillListDirection;
            component.Entry(is_skip, (EventDelegate.Callback) (() => PlayerAnimCtrl.Get(gacha_info.npcLoader.animator, is_skip ? PLCA.SKILL_GACHA_TOP : PLCA.SKILL_GACHA_TOP_SLIDE_END)));
            gacha_info.isFirstSkillListDirection = false;
            component.WaitAndNextPickup((EventDelegate.Callback) (() => PlayerAnimCtrl.Get(gacha_info.npcLoader.animator, PLCA.SKILL_GACHA_TOP_SLIDE)));
          }
        }
      }
    }
  }

  private IEnumerator LoadGachaBanner(Transform t, Enum _enum, string banner_img)
  {
    if (this.loadQueue == null)
      this.loadQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_image = this.loadQueue.Load(true, RESOURCE_CATEGORY.GACHA_BANNER, banner_img);
    if (this.loadQueue.IsLoading())
      yield return (object) this.loadQueue.Wait();
    if (Object.op_Inequality(lo_image.loadedObject, (Object) null))
      this.SetTexture(t, _enum, lo_image.loadedObject as Texture);
  }

  private IEnumerator LoadGachaGuaranteeCounter(
    Transform t,
    Enum _enum,
    int remainNum,
    string detailButtonImg = "")
  {
    if (this.loadQueue == null)
      this.loadQueue = new LoadingQueue((MonoBehaviour) this);
    if (detailButtonImg == "")
      detailButtonImg = "GGC_000000000";
    LoadObject lo_image = this.loadQueue.Load(RESOURCE_CATEGORY.GACHA_GUARANTEE_COUNTER, detailButtonImg);
    if (this.loadQueue.IsLoading())
      yield return (object) this.loadQueue.Wait();
    if (Object.op_Inequality(lo_image.loadedObject, (Object) null))
      this.SetTexture(t, _enum, lo_image.loadedObject as Texture);
  }

  private IEnumerator LoadGachaEventTitle(Transform t, Enum _enum, string eventTitleImg)
  {
    if (this.loadQueue == null)
      this.loadQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_image = this.loadQueue.Load(RESOURCE_CATEGORY.GACHA_EVENT_TITLE, eventTitleImg);
    if (this.loadQueue.IsLoading())
      yield return (object) this.loadQueue.Wait();
    if (Object.op_Inequality(lo_image.loadedObject, (Object) null))
      this.SetTexture(t, _enum, lo_image.loadedObject as Texture);
  }

  private IEnumerator LoadGachaButton(
    Transform t,
    string buttonImg,
    string strItemNum,
    int gachaId,
    int gachaIndex)
  {
    if (this.loadQueue == null)
      this.loadQueue = new LoadingQueue((MonoBehaviour) this);
    Transform buttonRoot = t;
    while (buttonRoot.childCount != 0)
    {
      Transform child = buttonRoot.GetChild(0);
      child.parent = (Transform) null;
      ((Component) child).gameObject.SetActive(false);
      Object.Destroy((Object) ((Component) child).gameObject);
    }
    LoadObject lo_button = this.loadQueue.Load(RESOURCE_CATEGORY.GACHA_BUTTON, buttonImg);
    if (this.loadQueue.IsLoading())
      yield return (object) this.loadQueue.Wait();
    GameObject gameObject = Object.Instantiate(lo_button.loadedObject) as GameObject;
    gameObject.transform.parent = buttonRoot;
    ((Object) gameObject.transform).name = ShopTop.UI.BTN_GACHA.ToString();
    gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    gameObject.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    this.SetLabelText(t, (Enum) ShopTop.UI.TXT_GACHA_ITEM_NUM, strItemNum);
    int[] event_data = new int[2]{ gachaId, gachaIndex };
    this.SetEvent(this.FindCtrl(t, (Enum) ShopTop.UI.BTN_GACHA), "GACHA", (object) event_data);
    this.RepositionTables();
  }

  private IEnumerator UpdateSkillList(int index, Transform t)
  {
    ShopTop.GachaModelInfo.GachaDataInfo gacha_info = this.gachaModelInfo[this.pageIndex].gachaDataInfo[index];
    int showPickupIndex = gacha_info.showPickupIndex;
    bool flag = false;
    Transform t1 = this.GetCtrl((Enum) ShopTop.UI.SCR_LIST_2).Find("skill_tex_" + (object) index);
    if (Object.op_Equality((Object) t1, (Object) null))
    {
      flag = true;
      t1 = this.Realizes("GachaSkillListItem2", this.GetCtrl((Enum) ShopTop.UI.SCR_LIST_2));
      ((Object) t1).name = "skill_tex_" + (object) index;
    }
    ((Component) t1).GetComponent<UIScrollOutSideObject>().SetTargetTransform(this.FindCtrl(t, (Enum) ShopTop.UI.OBJ_SKILL_MODEL_ROOT));
    this.GetComponent<UIPanel>((Enum) ShopTop.UI.SCR_LIST_2).depth = this.GetComponent<UIPanel>((Enum) ShopTop.UI.SCR_LIST).depth - 1;
    ShopTop.PickUpSkill pickup = gacha_info.pickup[showPickupIndex] as ShopTop.PickUpSkill;
    SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(pickup.skillID);
    this.SetLabelText(t, (Enum) ShopTop.UI.LBL_SKILL_ITEM_NAME, skillItemData.name);
    this.SetLabelText(t, (Enum) ShopTop.UI.LBL_TYPE_NAME, MonoBehaviourSingleton<StatusManager>.I.GetSkillItemGroupString(skillItemData.type));
    this.SetSkillSlotTypeIcon(t, (Enum) ShopTop.UI.SPR_EQUIP_TYPE_ICON, (Enum) ShopTop.UI.SPR_RARITY_BG, (Enum) ShopTop.UI.SPR_RARITY_ICON, skillItemData);
    Coroutine coroutine1 = this.StartCoroutine(this.LoadNPCModel(t1, (Enum) ShopTop.UI.TEX_SKILL_NPC_MODEL, (Enum) ShopTop.UI.TEX_SKILL_SUB_NPC_MODEL, gacha_info));
    Coroutine coroutine2 = this.StartCoroutine(this.LoadSkillBanner(t1, (Enum) ShopTop.UI.OBJ_SKILL_BANNER, (Enum) ShopTop.UI.TEX_SKILL_BANNER, skillItemData, gacha_info, pickup));
    this.coroutineList.Add(coroutine1);
    this.coroutineList.Add(coroutine2);
    if (index > 0)
    {
      if (flag)
        this.SetActive(t, (Enum) ShopTop.UI.OBJ_SKILL_INFO_ROOT, false);
      yield return (object) new WaitForSeconds((float) index);
      this.SetActive(t, (Enum) ShopTop.UI.OBJ_SKILL_INFO_ROOT, true);
    }
    if (string.IsNullOrEmpty(gacha_info.expireAt))
    {
      this.SetActive(t, (Enum) ShopTop.UI.LBL_TIME, false);
    }
    else
    {
      this.SetActive(t, (Enum) ShopTop.UI.LBL_TIME, true);
      ((Component) this.FindCtrl(t, (Enum) ShopTop.UI.LBL_TIME)).GetComponent<UILabel>().text = this.GetTimeCountDown(gacha_info.expireAt);
    }
  }

  private IEnumerator UpdateQuestList(int index, Transform t)
  {
    ShopTop.GachaModelInfo.GachaDataInfo gacha_info = this.gachaModelInfo[this.pageIndex].gachaDataInfo[index];
    ShopTop.PickUpQuest pickUpQuest = gacha_info.pickup[gacha_info.showPickupIndex] as ShopTop.PickUpQuest;
    this.GetComponent<UIPanel>((Enum) ShopTop.UI.SCR_LIST_2).depth = this.GetComponent<UIPanel>((Enum) ShopTop.UI.SCR_LIST).depth - 1;
    bool flag = false;
    Transform enemy_tex_trans = this.GetCtrl((Enum) ShopTop.UI.SCR_LIST_2).Find("enemy_tex_" + (object) index);
    if (Object.op_Equality((Object) enemy_tex_trans, (Object) null))
    {
      flag = true;
      enemy_tex_trans = this.Realizes("GachaListItem2", this.GetCtrl((Enum) ShopTop.UI.SCR_LIST_2));
      ((Object) enemy_tex_trans).name = "enemy_tex_" + (object) index;
    }
    ((Component) enemy_tex_trans).GetComponent<UIScrollOutSideObject>().SetTargetTransform(t);
    string text = string.Empty;
    string empty = string.Empty;
    uint enemy_id = 0;
    string foundation_name = (string) null;
    ELEMENT_TYPE element_type = ELEMENT_TYPE.MAX;
    RARITY_TYPE rarityType = RARITY_TYPE.D;
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(pickUpQuest.questID);
    if (questData != null)
    {
      EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) questData.GetMainEnemyID());
      if (enemyData != null)
      {
        enemy_id = enemyData.id;
        text = enemyData.name;
        element_type = enemyData.element;
        foundation_name = questData.GetFoundationName();
        empty = questData.GetMainEnemyLv().ToString();
        rarityType = questData.rarity;
      }
    }
    this.ClearRenderModel(enemy_tex_trans, (Enum) ShopTop.UI.TEX_ENEMY_MODEL);
    this.coroutineList.Add(this.StartCoroutine(this.LoadEnemyModel(enemy_tex_trans, (Enum) ShopTop.UI.TEX_ENEMY_MODEL, enemy_id, foundation_name, element_type, index == 0)));
    this.SetLabelText(enemy_tex_trans, (Enum) ShopTop.UI.LBL_ENEMY, text);
    this.SetLabelText(enemy_tex_trans, (Enum) ShopTop.UI.LBL_ENEMY_LV, empty);
    this.SetActive(enemy_tex_trans, (Enum) ShopTop.UI.SPR_RARITY_SSS, rarityType == RARITY_TYPE.SSS);
    this.SetActive(enemy_tex_trans, (Enum) ShopTop.UI.SPR_RARITY_SS, rarityType == RARITY_TYPE.SS);
    this.SetActive(enemy_tex_trans, (Enum) ShopTop.UI.SPR_RARITY_S, rarityType == RARITY_TYPE.S);
    this.SetActive(enemy_tex_trans, (Enum) ShopTop.UI.SPR_RARITY_A, rarityType == RARITY_TYPE.A);
    this.SetActive(enemy_tex_trans, (Enum) ShopTop.UI.SPR_RARITY_B, rarityType == RARITY_TYPE.B);
    this.SetActive(t, (Enum) ShopTop.UI.OBJ_REWARD_ICON_ROOT, true);
    if (pickUpQuest.materialID != 0U)
    {
      int count = this.pickUpMaterialIDs.Count;
      this.pickUpMaterialIDs.Add(pickUpQuest.materialID);
      ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(pickUpQuest.materialID);
      ItemIcon.Create(ITEM_ICON_TYPE.ITEM, itemData.iconID, new RARITY_TYPE?(itemData.rarity), this.FindCtrl(t, (Enum) ShopTop.UI.OBJ_REWARD_ICON_ROOT), event_name: "GACHA_EQUIP_LIST", event_data: count, enemy_icon_id: itemData.enemyIconID, enemy_icon_id2: itemData.enemyIconID2, disable_rarity_text: true);
      gacha_info.rewardIconMaterialInfoID = itemData.id;
    }
    this.SetActive(t, (Enum) ShopTop.UI.OBJ_REWARD_ICON_ROOT, pickUpQuest.materialID > 0U);
    UIVisibleWidgetShriken visibleWidgetShriken = (UIVisibleWidgetShriken) null;
    UIWidget widget = (UIWidget) null;
    Transform ctrl = this.FindCtrl(t, (Enum) ShopTop.UI.WGT_REWARD_EFFECT);
    if (Object.op_Inequality((Object) ctrl, (Object) null))
    {
      visibleWidgetShriken = ((Component) ctrl).GetComponent<UIVisibleWidgetShriken>();
      widget = ((Component) ctrl).GetComponent<UIWidget>();
    }
    if (Object.op_Equality((Object) visibleWidgetShriken, (Object) null))
    {
      UIPanel component = this.GetComponent<UIPanel>((Enum) ShopTop.UI.SCR_LIST_2);
      if (Object.op_Inequality((Object) component, (Object) null))
        UIVisibleWidgetShriken.Set(component, widget);
    }
    if (index >= 0)
    {
      if (flag)
      {
        this.SetActive(enemy_tex_trans, (Enum) ShopTop.UI.OBJ_QUEST_INFO_ROOT, false);
      }
      else
      {
        ItemIcon componentInChildren = ((Component) t).GetComponentInChildren<ItemIcon>();
        if (Object.op_Inequality((Object) componentInChildren, (Object) null) && gacha_info.rewardIconMaterialInfoID != 0U)
        {
          if (pickUpQuest.materialID != 0U)
            this.SetMaterialInfo(componentInChildren.transform, REWARD_TYPE.ITEM, Singleton<ItemTable>.I.GetItemData(pickUpQuest.materialID).id, this.GetCtrl((Enum) ShopTop.UI.SCR_LIST));
          else
            this.SetMaterialInfo(componentInChildren.transform, REWARD_TYPE.ITEM, gacha_info.rewardIconMaterialInfoID, this.GetCtrl((Enum) ShopTop.UI.SCR_LIST));
        }
      }
      yield return (object) new WaitForSeconds((float) index);
      this.SetActive(enemy_tex_trans, (Enum) ShopTop.UI.OBJ_QUEST_INFO_ROOT, true);
    }
    if (string.IsNullOrEmpty(gacha_info.expireAt))
    {
      this.SetActive(enemy_tex_trans, (Enum) ShopTop.UI.LBL_TIME, false);
    }
    else
    {
      this.SetActive(enemy_tex_trans, (Enum) ShopTop.UI.LBL_TIME, true);
      ((Component) this.FindCtrl(enemy_tex_trans, (Enum) ShopTop.UI.LBL_TIME)).GetComponent<UILabel>().text = this.GetTimeCountDown(gacha_info.expireAt);
    }
  }

  private void DestoryListChild()
  {
    this.GetCtrl((Enum) ShopTop.UI.TBL_LIST).DestroyChildren();
    this.GetCtrl((Enum) ShopTop.UI.SCR_LIST_2).DestroyChildren();
  }

  private void StopLoadCoroutine()
  {
    this.coroutineList.ForEach((Action<Coroutine>) (c =>
    {
      if (c == null)
        return;
      this.StopCoroutine(c);
    }));
    this.coroutineList.Clear();
  }

  public void OnQuery_MAGI_GACHA()
  {
    this.pageIndex = 1;
    this.ResetView();
  }

  public void OnQuery_QUEST_GACHA()
  {
    this.pageIndex = 0;
    this.ResetView();
  }

  private void ResetView()
  {
    MonoBehaviourSingleton<UIManager>.I.SetDisableMoment();
    this.StopLoadCoroutine();
    this.DeleteModel();
    this.DestoryListChild();
    this.UpdateShowIndex(true);
    this.timer = 0.0f;
    this.gachaModelInfo[this.pageIndex].gachaDataInfo.ForEach((Action<ShopTop.GachaModelInfo.GachaDataInfo>) (data =>
    {
      data.isFirstSkillListDirection = true;
      data.rewardIconMaterialInfoID = 0U;
    }));
    this.ticketTitleRootList.Clear();
    this.SetDirty((Enum) ShopTop.UI.TBL_LIST);
    this.SetGachaListUI();
    this.RefreshUI();
  }

  public void OnQuery_GACHA_EQUIP_LIST()
  {
    int eventData = (int) GameSection.GetEventData();
    if (eventData >= this.pickUpMaterialIDs.Count || eventData <= -1)
      GameSection.StopEvent();
    else
      GameSection.SetEventData((object) new object[1]
      {
        (object) this.pickUpMaterialIDs[eventData]
      });
  }

  public void OnQuery_GACHA_EQUIP_LIST_FROM_NEWS()
  {
    object[] eventData = (object[]) GameSection.GetEventData();
    int num1 = (int) eventData[0];
    int num2 = (int) eventData[1];
    uint num3 = 0;
    List<ShopTop.GachaModelInfo.GachaDataInfo> gachaDataInfo1 = this.gachaModelInfo[0].gachaDataInfo;
    int index1 = 0;
    for (int count1 = gachaDataInfo1.Count; index1 < count1; ++index1)
    {
      ShopTop.GachaModelInfo.GachaDataInfo gachaDataInfo2 = gachaDataInfo1[index1];
      if (gachaDataInfo2 != null && gachaDataInfo2.pickup != null)
      {
        int index2 = 0;
        for (int count2 = gachaDataInfo2.pickup.Count; index2 < count2; ++index2)
        {
          if (gachaDataInfo2.pickup[index2] is ShopTop.PickUpQuest pickUpQuest && (long) pickUpQuest.questID == (long) num2)
          {
            num3 = pickUpQuest.materialID;
            break;
          }
        }
        if (num3 >= 1U)
          break;
      }
    }
    if (num3 <= 0U)
    {
      GameSection.ChangeEvent("EQUIP_NOT_EXIST");
    }
    else
    {
      int num4 = (int) eventData[2];
      if (num4 >= 0)
      {
        MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent();
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[1]
        {
          new EventData("GACHA_DETAIL_MAX_PARAM_FROM_NEWS", (object) new object[2]
          {
            (object) num3,
            (object) num4
          })
        });
      }
      GameSection.SetEventData((object) new object[1]
      {
        (object) num3
      });
    }
  }

  private void OnQuery_FORCE_ONCE_PURCHASE_GACHA()
  {
    string productId = GameSection.GetEventData() as string;
    GachaList.Gacha targetGacha = (GachaList.Gacha) null;
    int targetIndex = 0;
    MonoBehaviourSingleton<GachaManager>.I.gachaData.types.ForEach((Action<GachaList.GachaType>) (type => type.groups.ForEach((Action<GachaList.GachaGroup>) (gr =>
    {
      GachaList.Gacha gacha = gr.gachas.Where<GachaList.Gacha>((Func<GachaList.Gacha, bool>) (g => g.productId == productId)).FirstOrDefault<GachaList.Gacha>();
      if (gacha == null)
        return;
      targetGacha = gacha;
      targetIndex = gr.gachas.Select((g, j) => new
      {
        Content = g,
        Index = j
      }).Where(ano => ano.Content.gachaId == targetGacha.gachaId).Select(ano => ano.Index).First<int>();
    }))));
    if (targetGacha == null)
      return;
    MonoBehaviourSingleton<GachaManager>.I.SelectGacha(targetGacha.gachaId, targetIndex);
    this.DoGachaOrSendCanPurchaseable();
  }

  private void OnQuery_GACHA()
  {
    int[] eventData = (int[]) GameSection.GetEventData();
    if (eventData[0] < 0)
    {
      GameSection.StopEvent();
    }
    else
    {
      MonoBehaviourSingleton<GachaManager>.I.SelectGacha(eventData[0], eventData[1]);
      if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1))
      {
        this.TutorialDoGacha();
        GameSection.ChangeEvent("GACHA_QUEST_TUTORIAL");
      }
      else if (MonoBehaviourSingleton<GachaManager>.I.selectGacha.IsEnd)
      {
        this.RemoveEndDateModel();
        this.ResetView();
        GameSection.ChangeEvent("GACHA_END");
      }
      else
      {
        string str = "";
        if (MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId > 0)
        {
          int ticketId = MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId;
          int itemNum = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((Predicate<ItemInfo>) (x => (long) x.tableData.id == (long) ticketId), 1);
          ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint) ticketId);
          str = $"{itemData.name} {(object) MonoBehaviourSingleton<GachaManager>.I.selectGacha.needItemNum}{StringTable.Get(STRING_CATEGORY.COMMON, 4000U)}\n";
          if (MonoBehaviourSingleton<GachaManager>.I.selectGacha.needItemNum > itemNum)
          {
            GameSection.ChangeEvent("NOT_ENOUGH_GACHA_TICKET", (object) new object[2]
            {
              (object) itemData.name,
              (object) ((MonoBehaviourSingleton<GachaManager>.I.selectGacha.needItemNum - itemNum).ToString() + StringTable.Get(STRING_CATEGORY.COMMON, 4000U))
            });
            return;
          }
        }
        else if (MonoBehaviourSingleton<GachaManager>.I.selectGacha.IsDirectPurchase())
        {
          this.selectProductId = MonoBehaviourSingleton<GachaManager>.I.selectGacha.productId;
          if ((double) MonoBehaviourSingleton<GachaManager>.I.selectGacha.yenIncludeTax > 0.0)
            this.pp = string.Empty;
          this.DoGachaOrSendCanPurchaseable();
        }
        else
          str = $"{StringTable.Get(STRING_CATEGORY.COMMON, 100U)} {(object) MonoBehaviourSingleton<GachaManager>.I.selectGacha.crystalNum}{StringTable.Get(STRING_CATEGORY.COMMON, 3000U)}";
        GameSection.SetEventData((object) new object[1]
        {
          (object) str
        });
      }
    }
  }

  protected void TutorialDoGacha()
  {
    if (!GameSection.CheckCrystal(MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId > 0 ? MonoBehaviourSingleton<GachaManager>.I.selectGacha.needItemNum : MonoBehaviourSingleton<GachaManager>.I.selectGacha.crystalNum, MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId))
      return;
    Protocol.Force((System.Action) (() => this.DoGacha((Action<Error>) (ret =>
    {
      if (ret == Error.None || ret == Error.ERR_CRYSTAL_NOT_ENOUGH)
        return;
      GameSection.ResumeEvent(false);
    }))));
  }

  private void OnQuery_PP_TO_BUY()
  {
    this.pp = GameSection.GetEventData() as string;
    GameSection.SetEventData((object) null);
    this.DoGachaOrSendCanPurchaseable();
  }

  private void OnQuery_ShopStopper_YES() => this.RequestEvent("STOPPER_TO_BUY");

  private void OnQuery_STOPPER_TO_BUY()
  {
    if (MonoBehaviourSingleton<GachaManager>.I.selectGachaType == GACHA_TYPE.QUEST)
      GameSection.ChangeEvent("BUY_QUEST_GACHA");
    else
      GameSection.ChangeEvent("BUY_SKILL_GACHA");
    GameSection.StayEvent();
    this.DoPurchase();
  }

  private void OnQuery_DETAIL_PROMOTION()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<FriendManager>.I.SendGetFollowLink((Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  private void DoGachaOrSendCanPurchaseable()
  {
    if (MonoBehaviourSingleton<GachaManager>.I.selectGachaType == GACHA_TYPE.QUEST)
      GameSection.ChangeEvent("BUY_QUEST_GACHA");
    else
      GameSection.ChangeEvent("BUY_SKILL_GACHA");
    GameSection.StayEvent();
    this.DoGacha((Action<Error>) (ret =>
    {
      if (ret == Error.None)
      {
        if (MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().oncePurchaseItemToShop != null && !string.IsNullOrEmpty(MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().oncePurchaseItemToShop.productId))
          this.SendGachaCanPurchase();
        else
          GameSection.ResumeEvent(true);
      }
      else
        GameSection.ResumeEvent(false);
    }));
  }

  private void SendGachaCanPurchase()
  {
    MonoBehaviourSingleton<ShopManager>.I.SendGoldCanPurchase(this.selectProductId, this.pp, (Action<Error>) (ret =>
    {
      if (ret != Error.None)
      {
        if (ret == Error.WRN_GOLD_OVER_LIMITTER_OVERUSE)
        {
          GameSection.ChangeStayEvent("STOPPER");
          GameSection.ResumeEvent(true);
        }
        else
          GameSection.ResumeEvent(false);
      }
      else
        this.DoPurchase();
    }));
  }

  private void DoPurchase()
  {
    this.isPurchase = true;
    Native.RequestPurchase(this.selectProductId, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id.ToString(), MonoBehaviourSingleton<UserInfoManager>.I.userIdHash);
  }

  private void OnBuyItem(string productId)
  {
    if (this.isDoGacha)
      return;
    if (!string.IsNullOrEmpty(productId))
    {
      Network.ProductData product_data = (Network.ProductData) null;
      MonoBehaviourSingleton<GachaManager>.I.gachaData.types.ForEach((Action<GachaList.GachaType>) (type => type.groups.ForEach((Action<GachaList.GachaGroup>) (gr =>
      {
        IEnumerable<GachaList.Gacha> source = gr.gachas.Where<GachaList.Gacha>((Func<GachaList.Gacha, bool>) (g => g.productId == productId));
        if (source.Count<GachaList.Gacha>() <= 0)
          return;
        GachaList.Gacha gacha = source.First<GachaList.Gacha>();
        product_data = new Network.ProductData()
        {
          productId = productId,
          price = (double) gacha.yen,
          crystalNum = gacha.crystalNum
        };
      }))));
      if (product_data != null)
      {
        this.isDoGacha = true;
        this.DoGacha((Action<Error>) (ret =>
        {
          this.isPurchase = false;
          this.isDoGacha = false;
          if (ret == Error.None)
            GameSection.ResumeEvent(true);
          else
            GameSection.ResumeEvent(false);
        }));
      }
      else
        this.SendRequestCurrentCrystal((System.Action) (() =>
        {
          this.isPurchase = false;
          GameSection.ResumeEvent(false);
        }));
    }
    else
    {
      if (!this.isPurchase)
        return;
      GameSection.ResumeEvent(false);
    }
  }

  private void OnBuyGoPayItem(GoPayDepositModel ret, Purchase purchase)
  {
    this.OnBuyItem(ret.result.productId);
  }

  private void SendRequestCurrentCrystal(System.Action onFinish)
  {
    Protocol.Send<OnceStatusInfoModel>(OnceStatusInfoModel.URL, (Action<OnceStatusInfoModel>) (result => this.CheckCrystalNum(result, onFinish)));
  }

  private void CheckCrystalNum(OnceStatusInfoModel ret, System.Action onFinish)
  {
    if (ret.Error != Error.None)
      return;
    MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal = ret.result.userStatus.crystal;
    MonoBehaviourSingleton<UserInfoManager>.I.DirtyUserStatus();
    onFinish();
  }

  private void OnQuery_GACHA_BANNER()
  {
    ShopTop.GachaModelInfo.GachaDataInfo eventData = GameSection.GetEventData() as ShopTop.GachaModelInfo.GachaDataInfo;
    if (string.IsNullOrEmpty(eventData.bannerImg) || string.IsNullOrEmpty(eventData.url))
      GameSection.StopEvent();
    GameSection.SetEventData((object) eventData.url);
  }

  private void Update()
  {
    if (this.state != UIBehaviour.STATE.OPEN)
      return;
    if ((double) this.timer < 5.0)
      this.timer += Time.deltaTime;
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != nameof (ShopTop) || (double) this.timer < 5.0)
      return;
    this.timer = 0.0f;
    this.UpdateShowIndex();
    this.RefreshUI();
  }

  private string GetTimeCountDown(string endTime)
  {
    if (string.IsNullOrEmpty(endTime))
      return (string) null;
    DateTime now = TimeManager.GetNow();
    DateTime dateTime = DateTime.Parse(endTime);
    if (dateTime.CompareTo(now) < 0)
      return StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 11U);
    TimeSpan timeSpan = dateTime.Subtract(now);
    StringBuilder stringBuilder = new StringBuilder();
    if (timeSpan.Days > 0)
      stringBuilder.Append(string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 9U), (object) timeSpan.Days));
    else if (timeSpan.Hours > 0)
      stringBuilder.Append(string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 10U), (object) timeSpan.Hours));
    else
      stringBuilder.Append(timeSpan.Minutes.ToString() + " minutes");
    stringBuilder.Append(" " + StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 12U));
    return stringBuilder.ToString();
  }

  private void UpdateShowIndex(bool is_reset = false)
  {
    if (this.gachaModelInfo == null || this.gachaModelInfo.Count == 0)
      return;
    this.gachaModelInfo.ForEach((Action<ShopTop.GachaModelInfo>) (types =>
    {
      if (types == null || types.gachaDataInfo == null || types.gachaDataInfo.Count == 0)
        return;
      types.gachaDataInfo.ForEach((Action<ShopTop.GachaModelInfo.GachaDataInfo>) (groups =>
      {
        if (!is_reset)
        {
          ++groups.showPickupIndex;
          if (groups.showPickupIndex < groups.pickup.Count)
            return;
          groups.showPickupIndex = 0;
        }
        else
          groups.showPickupIndex = 0;
      }));
    }));
    this.DeleteMaterialInfo();
  }

  private void OnQuery_CURRENCY()
  {
    GameSection.ChangeEvent("INFO", (object) WebViewManager.Currency);
  }

  private void OnQuery_COMMERCIAL()
  {
    GameSection.ChangeEvent("INFO", (object) WebViewManager.Commercial);
  }

  private void OnQuery_FUND() => GameSection.ChangeEvent("INFO", (object) WebViewManager.Found);

  private void OnQuery_PROBABILITY()
  {
    GameSection.ChangeEvent("INFO", (object) this.gachaModelInfo[this.pageIndex].url);
  }

  private void RemoveEndDateModel()
  {
    foreach (ShopTop.GachaModelInfo gachaModelInfo in this.gachaModelInfo)
    {
      foreach (ShopTop.GachaModelInfo.GachaDataInfo gachaDataInfo in gachaModelInfo.gachaDataInfo)
        gachaDataInfo.gachas.RemoveAll((Predicate<GachaList.Gacha>) (x => x.IsEnd));
      gachaModelInfo.gachaDataInfo.RemoveAll((Predicate<ShopTop.GachaModelInfo.GachaDataInfo>) (x => x.gachas.Count == 0));
    }
  }

  private void OnQuery_GUARANTEE_GACHA_DETAIL()
  {
    string eventData = (string) GameSection.GetEventData();
    if (eventData == "")
      GameSection.StopEvent();
    else
      GameSection.SetEventData((object) eventData);
  }

  protected override void OnDestroy()
  {
    if (MonoBehaviourSingleton<ShopReceiver>.IsValid())
    {
      MonoBehaviourSingleton<ShopReceiver>.I.onBuyGacha -= new Action<string>(this.OnBuyItem);
      MonoBehaviourSingleton<ShopReceiver>.I.onBuyItem -= new Action<string>(this.OnBuyItem);
      MonoBehaviourSingleton<ShopReceiver>.I.onGetProductDatas -= new Action<StoreDataList>(this.OnGetProductDatas);
    }
    base.OnDestroy();
  }

  private void OnGetProductDatas(StoreDataList list)
  {
    this._isFinishGetNativeProductlist = true;
    this._nativeStoreList = list;
  }

  private enum UI
  {
    GRD_LIST,
    TBL_LIST,
    SCR_LIST,
    SCR_LIST_2,
    BTN_MAGI_GACHA,
    BTN_QUEST_GACHA,
    OBJ_BTN_ROOT,
    GRD_BTN,
    TBL_BTN_HOLIZONTAL,
    GRD_BTN_INNER,
    TXT_GACHA_ITEM_NUM,
    TEX_GACHA_EVENT_TITLE,
    SPR_RARITY_SSS,
    SPR_RARITY_SS,
    SPR_RARITY_S,
    SPR_RARITY_A,
    SPR_RARITY_B,
    SPR_BG_BLACK,
    LBL_NAME,
    LBL_DESCRIPTION,
    TEX_ENEMY_MODEL,
    LBL_ENEMY,
    LBL_ENEMY_LV,
    OBJ_REWARD_ICON_ROOT,
    OBJ_QUEST_INFO_ROOT,
    LBL_TIME,
    OBJ_SKILL_INFO_ROOT,
    LBL_SKILL_ITEM_NAME,
    LBL_TYPE_NAME,
    SPR_RARITY_BG,
    SPR_RARITY_ICON,
    SPR_EQUIP_TYPE_ICON,
    OBJ_SKILL_BANNER,
    OBJ_SKILL_MODEL_ROOT,
    TEX_SKILL_MODEL,
    TEX_SKILL_INNER_MODEL,
    TEX_SKILL_NPC_MODEL,
    TEX_SKILL_SUB_NPC_MODEL,
    BTN_GACHA,
    SPR_CRYSTAL,
    TEX_TICKET,
    LBL_CRYSTAL_PRICE,
    LBL_TICKET_PRICE,
    LBL_HAVE,
    SPR_MULTI,
    SPR_LINE,
    LBL_TITLE,
    SPR_GACHA_BUTTON_BG,
    LBL_GACHA_CAPTION,
    TEX_TICKET_HAVE,
    COUNTER_LBL,
    NUMBER_COUNTER_IMG,
    S_COUNTER,
    S_AVAILABLE,
    COUNTER_PROGRESSBAR_FOREGROUND,
    LBL_CRYSTAL_NUM,
    LBL_MORE_TICKET,
    OBJ_GUARANTEE_HEADER_ROOT,
    OBJ_GUARANTEE_FOOTER_ROOT,
    BTN_GUARANTEE_COUNT_DOWN,
    BTN_GUARANTEE_DETAIL,
    TEX_GUARANTEE_TIME,
    LBL_GACHA_DESCRIPTION,
    TEX_GACHA_BANNER,
    BTN_GACHA_BANNER,
    TEX_SKILL_BANNER,
    WGT_REWARD_EFFECT,
    SPR_NOTE_BG,
    LBL_NOTE,
    BTN_VIEW_PROBABIRITY,
    SPR_GACHA_EFFECT,
    SPR_GACHA_PLAY,
    SPR_GACHA_ICON,
    LBL_FRIEND_INVITATION_REMAIN,
    LBL_FRIEND_INVITATION_INVITED,
    LBL_FRIEND_INVITATION_TIME,
  }

  public class PickUp
  {
    public int orderNo;
    protected uint gachaResultItemID;

    public uint GetGachaResultItemID() => this.gachaResultItemID;
  }

  public class PickUpQuest : ShopTop.PickUp
  {
    public uint materialID;
    public uint equipItemID;

    public uint questID
    {
      set => this.gachaResultItemID = value;
      get => this.gachaResultItemID;
    }

    public PickUpQuest(int no, int quest, uint material, uint equip)
    {
      this.orderNo = no;
      this.questID = (uint) quest;
      this.materialID = material;
      this.equipItemID = equip;
    }
  }

  public class PickUpSkill : ShopTop.PickUp
  {
    public GachaList.GachaPickupAnim gachaAnim;

    public uint skillID
    {
      set => this.gachaResultItemID = value;
      get => this.gachaResultItemID;
    }

    public PickUpSkill(int no, int skill, GachaList.GachaPickupAnim anim)
    {
      this.orderNo = no;
      this.skillID = (uint) skill;
      this.gachaAnim = anim;
    }
  }

  private class GachaModelInfo
  {
    public int sortPriority;
    public GACHA_TYPE type;
    public string url;
    public List<ShopTop.GachaModelInfo.GachaDataInfo> gachaDataInfo;

    public class GachaDataInfo
    {
      public int groupID;
      public int priority;
      public int showPickupIndex;
      public List<GachaList.Gacha> gachas;
      public List<GachaGuaranteeCampaignInfo> gachaGuaranteeCampaignInfos;
      public List<GachaFriendPromotionInfo> friendPromotionInfo;
      public List<ShopTop.PickUp> pickup;
      public string bannerImg;
      public string buttonImgId;
      public string url;
      public string note;
      public int counter = -1;
      public string expireAt;
      public NPCLoader npcLoader;
      public bool isFirstSkillListDirection = true;
      public uint rewardIconMaterialInfoID;

      public GachaGuaranteeCampaignInfo GetGachaGuaranteeCampaignInfo(List<GachaList.Gacha> gachas)
      {
        int num = gachas.Count<GachaList.Gacha>();
        for (int index = 0; index < num; ++index)
        {
          GachaGuaranteeCampaignInfo guaranteeCampaignInfo = this.GetGachaGuaranteeCampaignInfo(gachas[index].gachaId);
          if (guaranteeCampaignInfo != null)
            return guaranteeCampaignInfo;
        }
        return (GachaGuaranteeCampaignInfo) null;
      }

      public GachaGuaranteeCampaignInfo GetGachaGuaranteeCampaignInfo(int gachaId)
      {
        return this.gachaGuaranteeCampaignInfos == null ? (GachaGuaranteeCampaignInfo) null : this.gachaGuaranteeCampaignInfos.Where<GachaGuaranteeCampaignInfo>((Func<GachaGuaranteeCampaignInfo, bool>) (g => g.gachaId == gachaId)).Where<GachaGuaranteeCampaignInfo>((Func<GachaGuaranteeCampaignInfo, bool>) (g => g.IsValid())).FirstOrDefault<GachaGuaranteeCampaignInfo>();
      }

      public GachaFriendPromotionInfo GetGachaFriendPromotionInfo(int gachaId)
      {
        if (this.friendPromotionInfo == null || this.friendPromotionInfo.Count <= 0)
          return (GachaFriendPromotionInfo) null;
        int index = 0;
        for (int count = this.friendPromotionInfo.Count; index < count; ++index)
        {
          GachaFriendPromotionInfo friendPromotionInfo = this.friendPromotionInfo[index];
          if (friendPromotionInfo.gachaId == gachaId)
            return friendPromotionInfo;
        }
        return (GachaFriendPromotionInfo) null;
      }
    }
  }

  private class GachaUIInfo
  {
    public int index;
    public Transform parent;
    public ShopTop.GachaModelInfo gachaModelInfo;
  }

  private class GuaranteeInfoUIStatus
  {
  }
}
