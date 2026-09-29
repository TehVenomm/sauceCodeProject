// Decompiled with JetBrains decompiler
// Type: HomeMutualFollowerListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class HomeMutualFollowerListItem : MonoBehaviour
{
  private const int STATUS_BG_IMG_DEFAULT_WIDTH = 274;
  private static readonly Vector3 NPC_CHARA_ICON_POS = new Vector3(0.0f, -1.49f, 1.87f);
  private static readonly Vector3 NPC_CHARA_ICON_ROT = new Vector3(0.0f, 154f, 0.0f);
  private const float NPC_CHARA_ICON_FOV = 10f;
  private static readonly Vector3 PC_CHARA_ICON_POS = new Vector3(0.0f, -1.536f, 1.87f);
  private static readonly Vector3 PC_CHARA_ICON_ROT = new Vector3(0.0f, 154f, 0.0f);
  private const int ANIM_ID = 99;
  [SerializeField]
  private GameObject m_userBasicStatusRootObject;
  [SerializeField]
  private UILabel m_userNameText;
  [SerializeField]
  private UILabel m_userLevelText;
  [SerializeField]
  private GameObject m_followIcon;
  [SerializeField]
  private GameObject m_followerIcon;
  [SerializeField]
  private GameObject m_blackListIcon;
  [SerializeField]
  private GameObject m_clanIcon;
  [SerializeField]
  private DegreePlate m_userDegreePlate;
  [SerializeField]
  private UILabel m_userCommentText;
  [SerializeField]
  private UITexture m_userCharaIconTex;
  [SerializeField]
  private GameObject m_disableMask;
  [SerializeField]
  private GameObject m_userCharacterInfoRootObject;
  [SerializeField]
  private UISprite m_userHPBgImg;
  [SerializeField]
  private UILabel m_userHPText;
  [SerializeField]
  private UISprite m_userAtkBgImg;
  [SerializeField]
  private UILabel m_userATKText;
  [SerializeField]
  private UISprite m_userDefBgImg;
  [SerializeField]
  private UILabel m_userDEFText;
  [SerializeField]
  private UILabel m_userLastLoginTimeText;
  [SerializeField]
  private GameObject m_userJoinInfoRootObject;
  [SerializeField]
  private UILabel m_userOnlineStatusText;
  [SerializeField]
  private UILabel m_userOnlineDetailText;
  [SerializeField]
  private UIButton m_joinButton;
  private int m_itemIndex;
  private int m_noReadMsgNum;
  private bool m_isShowUserJoinInfo = true;
  private UIWidget[] m_allUiArray;
  private bool m_isInitialized;
  private Coroutine m_initCoroutine;
  private uint m_loadingBit;
  private Action<int> m_OnClickMe;

  private int ItemIndex => this.m_itemIndex;

  private int NoReadMsgNum => this.m_noReadMsgNum;

  public bool IsInitialized => this.m_isInitialized;

  private void StartInitialize() => this.m_isInitialized = false;

  private void EndInitialize() => this.m_isInitialized = true;

  protected uint LoadingBit => this.m_loadingBit;

  private void SetLoadComplete(HomeMutualFollowerListItem.LOADING_COMP_BIT _type)
  {
    this.m_loadingBit = (uint) ((HomeMutualFollowerListItem.LOADING_COMP_BIT) this.m_loadingBit | _type);
  }

  public void Initialize(HomeMutualFollowerListItem.InitParam _param)
  {
    if (_param == null)
      return;
    if (this.m_initCoroutine != null)
      this.ResetParams();
    this.m_initCoroutine = this.StartCoroutine(this.InitCoroutine(_param));
  }

  private void ResetParams()
  {
    this.m_loadingBit = 0U;
    this.StopCoroutine(this.m_initCoroutine);
    this.m_initCoroutine = (Coroutine) null;
    this.EndInitialize();
  }

  private IEnumerator InitCoroutine(HomeMutualFollowerListItem.InitParam _param)
  {
    this.StartInitialize();
    if (this.m_allUiArray == null)
      this.m_allUiArray = ((Component) this).GetComponentsInChildren<UIWidget>(true);
    this.SetInitParameter(_param);
    this.SetUserBaseInfo(_param.CharacterInfo, _param.Index, _param.IsPermittedMessage, _param.IsUseRenderTextureCharaModel);
    this.SetFollowState(_param);
    if (Object.op_Inequality((Object) this.m_userJoinInfoRootObject, (Object) null))
      this.m_userJoinInfoRootObject.gameObject.SetActive(false);
    if (Object.op_Inequality((Object) this.m_userCharacterInfoRootObject, (Object) null))
      this.m_userCharacterInfoRootObject.gameObject.SetActive(true);
    this.InitButtonSettings();
    bool flag = false;
    while (!flag)
    {
      yield return (object) null;
      flag = true;
      foreach (HomeMutualFollowerListItem.LOADING_COMP_BIT loadingCompBit in Enum.GetValues(typeof (HomeMutualFollowerListItem.LOADING_COMP_BIT)))
      {
        if (loadingCompBit != HomeMutualFollowerListItem.LOADING_COMP_BIT.NONE)
          flag &= (loadingCompBit & (HomeMutualFollowerListItem.LOADING_COMP_BIT) this.LoadingBit) != 0;
      }
    }
    if (_param.OnCompleteLoading != null)
      _param.OnCompleteLoading();
    this.EndInitialize();
  }

  private void SetInitParameter(HomeMutualFollowerListItem.InitParam _p)
  {
    this.m_itemIndex = _p.Index;
    this.m_noReadMsgNum = _p.NoReadMsgNum;
    this.m_OnClickMe = _p.OnClickItem;
  }

  private void SetUserBaseInfo(
    FriendCharaInfo _info,
    int _index,
    bool _isPerMittedUser,
    bool _isUseRernderTexture)
  {
    if (_info == null)
      return;
    bool flag = _info.userId == 0;
    this.SetLabelText(this.m_userNameText, _info.name);
    this.SetLabelText(this.m_userLevelText, _info.level.ToString());
    this.SetLabelText(this.m_userLastLoginTimeText, _info.lastLogin);
    this.SetLabelText(this.m_userCommentText, _info.comment);
    if (Object.op_Inequality((Object) this.m_disableMask, (Object) null))
      this.m_disableMask.SetActive(!_isPerMittedUser);
    int num = _isUseRernderTexture ? 274 : 137;
    if (Object.op_Inequality((Object) this.m_userHPBgImg, (Object) null))
      this.m_userHPBgImg.width = num;
    if (Object.op_Inequality((Object) this.m_userAtkBgImg, (Object) null))
      this.m_userAtkBgImg.width = num;
    if (Object.op_Inequality((Object) this.m_userDefBgImg, (Object) null))
      this.m_userDefBgImg.width = num;
    ((Behaviour) this.m_userCharaIconTex).enabled = false;
    if (((Component) this.m_userCharaIconTex).gameObject.activeSelf != _isUseRernderTexture)
      ((Component) this.m_userCharaIconTex).gameObject.SetActive(_isUseRernderTexture);
    if (!_isUseRernderTexture)
      this.SetLoadComplete(HomeMutualFollowerListItem.LOADING_COMP_BIT.CHARA_RENDER_TEX);
    else if (flag)
      this.SetRenderNPCModel(((Component) this.m_userCharaIconTex).transform, this.m_userCharaIconTex, 0, HomeMutualFollowerListItem.NPC_CHARA_ICON_POS, HomeMutualFollowerListItem.NPC_CHARA_ICON_ROT, 10f, (Action<NPCLoader>) (npcLoader =>
      {
        ((Behaviour) this.m_userCharaIconTex).enabled = true;
        this.SetLoadComplete(HomeMutualFollowerListItem.LOADING_COMP_BIT.CHARA_RENDER_TEX);
      }));
    else
      this.ForceSetRenderPlayerModel(((Component) this.m_userCharaIconTex).transform, this.m_userCharaIconTex, PlayerLoadInfo.FromCharaInfo((CharaInfo) _info, false, true, false, true), 99, HomeMutualFollowerListItem.PC_CHARA_ICON_POS, HomeMutualFollowerListItem.PC_CHARA_ICON_ROT, true, (Action<PlayerLoader>) (playerLoader =>
      {
        ((Behaviour) this.m_userCharaIconTex).enabled = true;
        this.SetLoadComplete(HomeMutualFollowerListItem.LOADING_COMP_BIT.CHARA_RENDER_TEX);
      }));
    EquipSetCalculator equipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(_index + 4);
    equipSetCalculator.SetEquipSet(_info.equipSet);
    SimpleStatus finalStatus = equipSetCalculator.GetFinalStatus(0, (int) _info.hp, (int) _info.atk, (int) _info.def);
    this.SetLabelText(this.m_userATKText, finalStatus.GetAttacksSum().ToString());
    this.SetLabelText(this.m_userDEFText, finalStatus.GetDefencesSum().ToString());
    this.SetLabelText(this.m_userHPText, finalStatus.hp.ToString());
    if (!Object.op_Inequality((Object) this.m_userDegreePlate, (Object) null))
      return;
    this.m_userDegreePlate.Initialize(_info.selectedDegrees, false, (Action<DegreePlate>) (plate => this.SetLoadComplete(HomeMutualFollowerListItem.LOADING_COMP_BIT.DEGREE_ICON)));
  }

  private void InitButtonSettings()
  {
    UIButton component1 = ((Component) this).GetComponent<UIButton>();
    if (Object.op_Inequality((Object) component1, (Object) null))
    {
      component1.onClick.Clear();
      component1.onClick.Add(new EventDelegate((EventDelegate.Callback) (() => this.OnClickMe())));
    }
    UIGameSceneEventSender component2 = ((Component) this).GetComponent<UIGameSceneEventSender>();
    if (!Object.op_Inequality((Object) component2, (Object) null))
      return;
    component2.eventName = string.Empty;
  }

  private void SetFollowState(HomeMutualFollowerListItem.InitParam _param)
  {
    bool flag1 = false;
    if (MonoBehaviourSingleton<BlackListManager>.IsValid() && _param != null && _param.CharacterInfo != null)
      flag1 = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(_param.CharacterInfo.userId);
    int num = flag1 ? 0 : (_param.IsFollowing ? 1 : (_param.IsFollower ? 1 : 0));
    bool flag2 = false;
    Debug.Log((object) ("_param.clanId : " + _param.clanId));
    if (MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered())
    {
      Debug.Log((object) ("UserInfoManager.I.userClan.cId : " + MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId));
      flag2 = _param.clanId == MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId;
    }
    if (Object.op_Inequality((Object) this.m_blackListIcon, (Object) null))
      this.m_blackListIcon.SetActive(flag1);
    if (Object.op_Inequality((Object) this.m_followIcon, (Object) null))
      this.m_followIcon.SetActive(_param.IsFollowing && !flag1);
    if (Object.op_Inequality((Object) this.m_followerIcon, (Object) null))
      this.m_followerIcon.SetActive(_param.IsFollower && !flag1);
    if (!Object.op_Inequality((Object) this.m_clanIcon, (Object) null))
      return;
    this.m_clanIcon.SetActive(flag2);
  }

  public void SetAllUIVisible() => this.SwitchAllUIVisible(true);

  public void SetAllUIInvisible() => this.SwitchAllUIVisible(false);

  private void SwitchAllUIVisible(bool _isVisible)
  {
    if (this.m_allUiArray == null || this.m_allUiArray.Length < 1)
      return;
    for (int index = 0; index < this.m_allUiArray.Length; ++index)
      ((Behaviour) this.m_allUiArray[index]).enabled = _isVisible;
  }

  protected void SetRenderNPCModel(
    Transform targetTrans,
    UITexture _uiTex,
    int npc_id,
    Vector3 pos,
    Vector3 rot,
    float fov = -1f,
    Action<NPCLoader> onload_callback = null)
  {
    if (Object.op_Equality((Object) targetTrans, (Object) null))
      return;
    UIModelRenderTexture.Get(targetTrans).InitNPC(_uiTex, npc_id, pos, rot, fov, onload_callback);
  }

  protected void ForceSetRenderPlayerModel(
    Transform targetTrans,
    UITexture _uiTex,
    PlayerLoadInfo info,
    int anim_id,
    Vector3 pos,
    Vector3 rot,
    bool is_priority_visual_equip,
    Action<PlayerLoader> onload_callback = null)
  {
    if (Object.op_Equality((Object) targetTrans, (Object) null))
    {
      if (onload_callback == null)
        return;
      onload_callback((PlayerLoader) null);
    }
    else
      UIModelRenderTexture.Get(targetTrans).ForceInitPlayer(_uiTex, info, anim_id, pos, rot, is_priority_visual_equip, onload_callback);
  }

  private void SetLabelText(UILabel _target, string _text)
  {
    if (Object.op_Equality((Object) _target, (Object) null))
      return;
    _target.text = _text;
  }

  public void SwitchUserInfo()
  {
    this.m_isShowUserJoinInfo = !this.m_isShowUserJoinInfo;
    if (Object.op_Inequality((Object) this.m_userCharacterInfoRootObject, (Object) null))
      this.m_userCharacterInfoRootObject.SetActive(!this.m_isShowUserJoinInfo);
    if (!Object.op_Inequality((Object) this.m_userJoinInfoRootObject, (Object) null))
      return;
    this.m_userJoinInfoRootObject.SetActive(this.m_isShowUserJoinInfo);
  }

  public void HideAll()
  {
  }

  public void ShowAll()
  {
  }

  public void CleanRenderTexture()
  {
    UIModelRenderTexture.Get(((Component) this.m_userCharaIconTex).transform).Clear();
  }

  public void OnClickJoinButton()
  {
  }

  public void OnClickMe()
  {
    if (this.m_OnClickMe == null)
      return;
    this.m_OnClickMe(this.ItemIndex);
  }

  public class InitParam
  {
    public int Index;
    public FriendCharaInfo CharacterInfo;
    public bool IsFollowing;
    public bool IsFollower;
    public string clanId = "";
    public int NoReadMsgNum;
    public bool IsPermittedMessage = true;
    public bool IsUseRenderTextureCharaModel;
    public Action<int> OnClickItem;
    public System.Action OnCompleteLoading;
  }

  [Flags]
  private enum LOADING_COMP_BIT
  {
    NONE = 0,
    CHARA_RENDER_TEX = 1,
    DEGREE_ICON = 2,
  }
}
