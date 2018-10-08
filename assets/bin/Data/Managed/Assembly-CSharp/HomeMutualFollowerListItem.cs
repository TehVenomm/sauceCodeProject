using Network;
using System;
using System.Collections;
using UnityEngine;

public class HomeMutualFollowerListItem : MonoBehaviour
{
	public class InitParam
	{
		public int Index;

		public FriendCharaInfo CharacterInfo;

		public bool IsFollowing;

		public bool IsFollower;

		public int NoReadMsgNum;

		public bool IsPermittedMessage = true;

		public bool IsUseRenderTextureCharaModel;

		public Action<int> OnClickItem;

		public Action OnCompleteLoading;
	}

	[Flags]
	private enum LOADING_COMP_BIT
	{
		NONE = 0x0,
		CHARA_RENDER_TEX = 0x1,
		DEGREE_ICON = 0x2
	}

	private const int STATUS_BG_IMG_DEFAULT_WIDTH = 274;

	private const float NPC_CHARA_ICON_FOV = 10f;

	private const int ANIM_ID = 99;

	private static readonly Vector3 NPC_CHARA_ICON_POS = new Vector3(0f, -1.49f, 1.87f);

	private static readonly Vector3 NPC_CHARA_ICON_ROT = new Vector3(0f, 154f, 0f);

	private static readonly Vector3 PC_CHARA_ICON_POS = new Vector3(0f, -1.536f, 1.87f);

	private static readonly Vector3 PC_CHARA_ICON_ROT = new Vector3(0f, 154f, 0f);

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

	private int ItemIndex => m_itemIndex;

	private int NoReadMsgNum => m_noReadMsgNum;

	public bool IsInitialized => m_isInitialized;

	protected uint LoadingBit => m_loadingBit;

	private void StartInitialize()
	{
		m_isInitialized = false;
	}

	private void EndInitialize()
	{
		m_isInitialized = true;
	}

	private void SetLoadComplete(LOADING_COMP_BIT _type)
	{
		m_loadingBit |= (uint)_type;
	}

	public void Initialize(InitParam _param)
	{
		if (_param != null)
		{
			if (m_initCoroutine != null)
			{
				ResetParams();
			}
			m_initCoroutine = StartCoroutine(InitCoroutine(_param));
		}
	}

	private void ResetParams()
	{
		m_loadingBit = 0u;
		StopCoroutine(m_initCoroutine);
		m_initCoroutine = null;
		EndInitialize();
	}

	private IEnumerator InitCoroutine(InitParam _param)
	{
		StartInitialize();
		if (m_allUiArray == null)
		{
			m_allUiArray = GetComponentsInChildren<UIWidget>(true);
		}
		SetInitParameter(_param);
		SetUserBaseInfo(_param.CharacterInfo, _param.Index, _param.IsPermittedMessage, _param.IsUseRenderTextureCharaModel);
		SetFollowState(_param);
		if ((UnityEngine.Object)m_userJoinInfoRootObject != (UnityEngine.Object)null)
		{
			m_userJoinInfoRootObject.gameObject.SetActive(false);
		}
		if ((UnityEngine.Object)m_userCharacterInfoRootObject != (UnityEngine.Object)null)
		{
			m_userCharacterInfoRootObject.gameObject.SetActive(true);
		}
		InitButtonSettings();
		bool isLoadingComplete = false;
		while (!isLoadingComplete)
		{
			yield return (object)null;
			isLoadingComplete = true;
			foreach (int value in Enum.GetValues(typeof(LOADING_COMP_BIT)))
			{
				if (value != 0)
				{
					isLoadingComplete &= ((value & (int)LoadingBit) != 0);
				}
			}
		}
		if (_param.OnCompleteLoading != null)
		{
			_param.OnCompleteLoading();
		}
		EndInitialize();
	}

	private void SetInitParameter(InitParam _p)
	{
		m_itemIndex = _p.Index;
		m_noReadMsgNum = _p.NoReadMsgNum;
		m_OnClickMe = _p.OnClickItem;
	}

	private void SetUserBaseInfo(FriendCharaInfo _info, int _index, bool _isPerMittedUser, bool _isUseRernderTexture)
	{
		if (_info != null)
		{
			bool flag = _info.userId == 0;
			SetLabelText(m_userNameText, _info.name);
			SetLabelText(m_userLevelText, _info.level.ToString());
			SetLabelText(m_userLastLoginTimeText, _info.lastLogin);
			SetLabelText(m_userCommentText, _info.comment);
			if ((UnityEngine.Object)m_disableMask != (UnityEngine.Object)null)
			{
				m_disableMask.SetActive(!_isPerMittedUser);
			}
			int width = (!_isUseRernderTexture) ? 137 : 274;
			if ((UnityEngine.Object)m_userHPBgImg != (UnityEngine.Object)null)
			{
				m_userHPBgImg.width = width;
			}
			if ((UnityEngine.Object)m_userAtkBgImg != (UnityEngine.Object)null)
			{
				m_userAtkBgImg.width = width;
			}
			if ((UnityEngine.Object)m_userDefBgImg != (UnityEngine.Object)null)
			{
				m_userDefBgImg.width = width;
			}
			m_userCharaIconTex.enabled = false;
			if (m_userCharaIconTex.gameObject.activeSelf != _isUseRernderTexture)
			{
				m_userCharaIconTex.gameObject.SetActive(_isUseRernderTexture);
			}
			if (!_isUseRernderTexture)
			{
				SetLoadComplete(LOADING_COMP_BIT.CHARA_RENDER_TEX);
			}
			else if (flag)
			{
				SetRenderNPCModel(m_userCharaIconTex.transform, m_userCharaIconTex, 0, NPC_CHARA_ICON_POS, NPC_CHARA_ICON_ROT, 10f, delegate
				{
					m_userCharaIconTex.enabled = true;
					SetLoadComplete(LOADING_COMP_BIT.CHARA_RENDER_TEX);
				});
			}
			else
			{
				ForceSetRenderPlayerModel(m_userCharaIconTex.transform, m_userCharaIconTex, PlayerLoadInfo.FromCharaInfo(_info, false, true, false, true), 99, PC_CHARA_ICON_POS, PC_CHARA_ICON_ROT, true, delegate
				{
					m_userCharaIconTex.enabled = true;
					SetLoadComplete(LOADING_COMP_BIT.CHARA_RENDER_TEX);
				});
			}
			EquipSetCalculator otherEquipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(_index + 4);
			otherEquipSetCalculator.SetEquipSet(_info.equipSet, false);
			SimpleStatus finalStatus = otherEquipSetCalculator.GetFinalStatus(0, _info.hp, _info.atk, _info.def);
			SetLabelText(m_userATKText, finalStatus.GetAttacksSum().ToString());
			SetLabelText(m_userDEFText, finalStatus.GetDefencesSum().ToString());
			SetLabelText(m_userHPText, finalStatus.hp.ToString());
			if ((UnityEngine.Object)m_userDegreePlate != (UnityEngine.Object)null)
			{
				m_userDegreePlate.Initialize(_info.selectedDegrees, false, delegate
				{
					SetLoadComplete(LOADING_COMP_BIT.DEGREE_ICON);
				});
			}
		}
	}

	private void InitButtonSettings()
	{
		UIButton component = GetComponent<UIButton>();
		if ((UnityEngine.Object)component != (UnityEngine.Object)null)
		{
			component.onClick.Clear();
			component.onClick.Add(new EventDelegate(delegate
			{
				OnClickMe();
			}));
		}
		UIGameSceneEventSender component2 = GetComponent<UIGameSceneEventSender>();
		if ((UnityEngine.Object)component2 != (UnityEngine.Object)null)
		{
			component2.eventName = string.Empty;
		}
	}

	private void SetFollowState(InitParam _param)
	{
		bool flag = false;
		if (MonoBehaviourSingleton<BlackListManager>.IsValid() && _param != null && _param.CharacterInfo != null)
		{
			flag = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(_param.CharacterInfo.userId);
		}
		if ((UnityEngine.Object)m_blackListIcon != (UnityEngine.Object)null)
		{
			m_blackListIcon.SetActive(flag);
		}
		if ((UnityEngine.Object)m_followIcon == (UnityEngine.Object)null)
		{
			m_followIcon.SetActive(_param.IsFollowing && !flag);
		}
		if ((UnityEngine.Object)m_followerIcon == (UnityEngine.Object)null)
		{
			m_followerIcon.SetActive(_param.IsFollower && !flag);
		}
	}

	public void SetAllUIVisible()
	{
		SwitchAllUIVisible(true);
	}

	public void SetAllUIInvisible()
	{
		SwitchAllUIVisible(false);
	}

	private void SwitchAllUIVisible(bool _isVisible)
	{
		if (m_allUiArray != null && m_allUiArray.Length >= 1)
		{
			for (int i = 0; i < m_allUiArray.Length; i++)
			{
				m_allUiArray[i].enabled = _isVisible;
			}
		}
	}

	protected void SetRenderNPCModel(Transform targetTrans, UITexture _uiTex, int npc_id, Vector3 pos, Vector3 rot, float fov = -1, Action<NPCLoader> onload_callback = null)
	{
		if (!((UnityEngine.Object)targetTrans == (UnityEngine.Object)null))
		{
			UIModelRenderTexture.Get(targetTrans).InitNPC(_uiTex, npc_id, pos, rot, fov, onload_callback);
		}
	}

	protected void ForceSetRenderPlayerModel(Transform targetTrans, UITexture _uiTex, PlayerLoadInfo info, int anim_id, Vector3 pos, Vector3 rot, bool is_priority_visual_equip, Action<PlayerLoader> onload_callback = null)
	{
		if ((UnityEngine.Object)targetTrans == (UnityEngine.Object)null)
		{
			onload_callback?.Invoke(null);
		}
		else
		{
			UIModelRenderTexture.Get(targetTrans).ForceInitPlayer(_uiTex, info, anim_id, pos, rot, is_priority_visual_equip, onload_callback);
		}
	}

	private void SetLabelText(UILabel _target, string _text)
	{
		if (!((UnityEngine.Object)_target == (UnityEngine.Object)null))
		{
			_target.text = _text;
		}
	}

	public void SwitchUserInfo()
	{
		m_isShowUserJoinInfo = !m_isShowUserJoinInfo;
		if ((UnityEngine.Object)m_userCharacterInfoRootObject != (UnityEngine.Object)null)
		{
			m_userCharacterInfoRootObject.SetActive(!m_isShowUserJoinInfo);
		}
		if ((UnityEngine.Object)m_userJoinInfoRootObject != (UnityEngine.Object)null)
		{
			m_userJoinInfoRootObject.SetActive(m_isShowUserJoinInfo);
		}
	}

	public void HideAll()
	{
	}

	public void ShowAll()
	{
	}

	public void CleanRenderTexture()
	{
		UIModelRenderTexture.Get(m_userCharaIconTex.transform).Clear();
	}

	public void OnClickJoinButton()
	{
	}

	public void OnClickMe()
	{
		if (m_OnClickMe != null)
		{
			m_OnClickMe(ItemIndex);
		}
	}
}
