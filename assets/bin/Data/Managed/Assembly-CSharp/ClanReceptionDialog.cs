using Network;
using System.Collections;
using UnityEngine;

public class ClanReceptionDialog : GameSection
{
	private enum UI
	{
		SPR_FRAME,
		BTN_CLANINFO,
		BTN_CLAN_SEARCH,
		BTN_CLOSE,
		LBL_CLAN_NAME,
		BTN_CLAN_DETAIL,
		BTN_CLAN_SETTING,
		BTN_CLAN_SYMBOL_EDIT,
		LBL_CLAN_LV_NUM,
		LBL_CLAN_LV,
		LBL_CLAN_PT_NUM,
		SPR_GAUGE,
		SPR_GAUGE_MAX_BG,
		OBJ_SYMBOL
	}

	private static readonly Color32 buffGreen = new Color32(53, byte.MaxValue, 0, byte.MaxValue);

	private UserClanData m_clanData;

	private SymbolMarkCtrl symbolMark;

	private void Start()
	{
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		m_clanData = MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData;
		SetLabelText(UI.LBL_CLAN_NAME, m_clanData.name);
		SetClanLv();
		SetLabelText(UI.LBL_CLAN_PT_NUM, $"{m_clanData.exp:#,0}  pt");
		SetClanPointGauge();
		yield return StartCoroutine(CreateSymbolMark());
		if (MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsLeader())
		{
			SetActive(UI.BTN_CLAN_SETTING, is_visible: true);
		}
		else
		{
			SetActive(UI.BTN_CLAN_SETTING, is_visible: false);
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsLeader() || MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsSubLeader())
		{
			SetButtonEnabled(UI.BTN_CLAN_SYMBOL_EDIT, is_enabled: true);
		}
		else
		{
			SetButtonEnabled(UI.BTN_CLAN_SYMBOL_EDIT, is_enabled: false);
		}
		base.UpdateUI();
	}

	public override void InitializeReopen()
	{
		base.InitializeReopen();
		LoadSymbolMark();
	}

	private IEnumerator CreateSymbolMark()
	{
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject symbolMarkLoadObj = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ClanSymbolMark");
		yield return loadingQueue.Wait();
		Transform transform = ResourceUtility.Realizes(symbolMarkLoadObj.loadedObject as GameObject, 5);
		transform.parent = GetCtrl(UI.OBJ_SYMBOL);
		transform.localScale = Vector3.one;
		transform.localPosition = Vector3.zero;
		symbolMark = transform.GetComponent<SymbolMarkCtrl>();
		symbolMark.Initilize();
		LoadSymbolMark();
	}

	private void LoadSymbolMark()
	{
		if (symbolMark != null)
		{
			symbolMark.LoadSymbol(MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData.sym);
		}
	}

	private void SetClanLv()
	{
		SetLabelText(UI.LBL_CLAN_LV_NUM, m_clanData.level.ToString());
		if (m_clanData.isMaxLevel)
		{
			SetColor(UI.LBL_CLAN_LV_NUM, buffGreen);
			SetColor(UI.LBL_CLAN_LV, buffGreen);
			SetActive(UI.SPR_GAUGE_MAX_BG, is_visible: true);
		}
		else
		{
			SetActive(UI.SPR_GAUGE_MAX_BG, is_visible: false);
		}
	}

	private void SetClanPointGauge()
	{
		Transform transform = FindCtrl(base.transform, UI.SPR_GAUGE);
		if (transform != null)
		{
			int num = m_clanData.exp - m_clanData.expPrev;
			int num2 = m_clanData.expNext - m_clanData.expPrev;
			if (num2 == 0)
			{
				transform.localScale = new Vector3(0f, 1f, 1f);
			}
			else
			{
				transform.localScale = new Vector3(Mathf.Clamp((float)num / (float)num2, 0f, 1f), 1f, 1f);
			}
			if (m_clanData.isMaxLevel)
			{
				transform.localScale = new Vector3(1f, 1f, 1f);
			}
		}
	}

	private void OnQuery_CLAN_REWARD_INFO()
	{
		GameSection.SetEventData(WebViewManager.ClanReward);
	}

	private void OnQuery_CLAN_SYMBOL_EDIT()
	{
		GameSection.SetEventData(new object[2]
		{
			MonoBehaviourSingleton<UserInfoManager>.I.userInfo,
			MonoBehaviourSingleton<UserInfoManager>.I.userStatus
		});
	}
}
