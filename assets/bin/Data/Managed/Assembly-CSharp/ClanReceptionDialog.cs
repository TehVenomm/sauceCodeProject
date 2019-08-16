using Network;
using System;
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

	private static readonly Color32 buffGreen = new Color32((byte)53, byte.MaxValue, (byte)0, byte.MaxValue);

	private UserClanData m_clanData;

	private SymbolMarkCtrl symbolMark;

	private void Start()
	{
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		m_clanData = MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData;
		SetLabelText((Enum)UI.LBL_CLAN_NAME, m_clanData.name);
		SetClanLv();
		SetLabelText((Enum)UI.LBL_CLAN_PT_NUM, $"{m_clanData.exp:#,0}  pt");
		SetClanPointGauge();
		yield return this.StartCoroutine(CreateSymbolMark());
		if (MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsLeader())
		{
			SetActive((Enum)UI.BTN_CLAN_SETTING, is_visible: true);
		}
		else
		{
			SetActive((Enum)UI.BTN_CLAN_SETTING, is_visible: false);
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsLeader() || MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsSubLeader())
		{
			SetButtonEnabled((Enum)UI.BTN_CLAN_SYMBOL_EDIT, is_enabled: true);
		}
		else
		{
			SetButtonEnabled((Enum)UI.BTN_CLAN_SYMBOL_EDIT, is_enabled: false);
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
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject symbolMarkLoadObj = load_queue.Load(RESOURCE_CATEGORY.UI, "ClanSymbolMark");
		yield return load_queue.Wait();
		GameObject obj = symbolMarkLoadObj.loadedObject as GameObject;
		Transform item = ResourceUtility.Realizes(obj, 5);
		item.set_parent(GetCtrl(UI.OBJ_SYMBOL));
		item.set_localScale(Vector3.get_one());
		item.set_localPosition(Vector3.get_zero());
		symbolMark = item.GetComponent<SymbolMarkCtrl>();
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
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		SetLabelText((Enum)UI.LBL_CLAN_LV_NUM, m_clanData.level.ToString());
		if (m_clanData.isMaxLevel)
		{
			SetColor((Enum)UI.LBL_CLAN_LV_NUM, Color32.op_Implicit(buffGreen));
			SetColor((Enum)UI.LBL_CLAN_LV, Color32.op_Implicit(buffGreen));
			SetActive((Enum)UI.SPR_GAUGE_MAX_BG, is_visible: true);
		}
		else
		{
			SetActive((Enum)UI.SPR_GAUGE_MAX_BG, is_visible: false);
		}
	}

	private void SetClanPointGauge()
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		Transform val = FindCtrl(this.get_transform(), UI.SPR_GAUGE);
		if (val != null)
		{
			int num = m_clanData.exp - m_clanData.expPrev;
			int num2 = m_clanData.expNext - m_clanData.expPrev;
			if (num2 == 0)
			{
				val.set_localScale(new Vector3(0f, 1f, 1f));
			}
			else
			{
				val.set_localScale(new Vector3(Mathf.Clamp((float)num / (float)num2, 0f, 1f), 1f, 1f));
			}
			if (m_clanData.isMaxLevel)
			{
				val.set_localScale(new Vector3(1f, 1f, 1f));
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
