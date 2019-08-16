using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ClanSearchSettings : GameSection
{
	public enum UI
	{
		IPT_NAME,
		POP_TARGET_JOIN_TYPE,
		LBL_TARGET_JOIN_TYPE,
		POP_TARGET_LABEL,
		LBL_TARGET_LABEL,
		POP_TARGET_JOINABLE,
		LBL_TARGET_JOINABLE,
		OBJ_SEARCH,
		LBL_DEFAULT
	}

	private class PopuplistParam
	{
		public Transform popup_transform;

		public Transform parent_ctrl;

		public List<string> texts = new List<string>();

		public int select_index;

		public Action<int> callback;

		public string SelectedString
		{
			get
			{
				if (texts != null && texts.Count > select_index)
				{
					return texts[select_index];
				}
				return string.Empty;
			}
		}
	}

	private ClanSearchModel.RequestSendForm searchRequest = new ClanSearchModel.RequestSendForm();

	private PopuplistParam popupJoinType;

	private PopuplistParam popupLabels;

	private PopuplistParam popupJoinable;

	public override void Initialize()
	{
		MonoBehaviourSingleton<ClanMatchingManager>.I.LoadSearchRequestFromPrefs();
		searchRequest = new ClanSearchModel.RequestSendForm();
		MonoBehaviourSingleton<ClanMatchingManager>.I.searchRequest.Copy(ref searchRequest);
		SetActive((Enum)UI.LBL_DEFAULT, string.IsNullOrEmpty(searchRequest.name));
		SetInput(UI.IPT_NAME, searchRequest.name, 16, OnChangeName);
		popupJoinType = new PopuplistParam();
		popupJoinType.texts.Add("Not Specified");
		popupJoinType.texts.Add(StringTable.Get(STRING_CATEGORY.JOIN_TYPE, 0u));
		popupJoinType.texts.Add(StringTable.Get(STRING_CATEGORY.JOIN_TYPE, 1u));
		popupJoinType.select_index = searchRequest.jt + 1;
		popupJoinType.parent_ctrl = GetCtrl(UI.POP_TARGET_JOIN_TYPE);
		popupJoinType.callback = delegate(int index)
		{
			popupJoinType.select_index = index;
			searchRequest.jt = index - 1;
			RefreshUI();
		};
		popupLabels = new PopuplistParam();
		string[] allInCategory = StringTable.GetAllInCategory(STRING_CATEGORY.CLAN_LABEL);
		for (int i = 0; i < allInCategory.Length; i++)
		{
			popupLabels.texts.Add(allInCategory[i]);
		}
		if (popupLabels.texts.Count > searchRequest.lbl)
		{
			popupLabels.select_index = searchRequest.lbl;
		}
		popupLabels.parent_ctrl = GetCtrl(UI.POP_TARGET_LABEL);
		popupLabels.callback = delegate(int index)
		{
			popupLabels.select_index = index;
			searchRequest.lbl = index;
			RefreshUI();
		};
		popupJoinable = new PopuplistParam();
		popupJoinable.texts.Add("Brigades you can join");
		popupJoinable.texts.Add("All Brigades");
		popupJoinable.select_index = searchRequest.isCF;
		popupJoinable.parent_ctrl = GetCtrl(UI.POP_TARGET_JOINABLE);
		popupJoinable.callback = delegate(int index)
		{
			popupJoinable.select_index = index;
			searchRequest.isCF = index;
			RefreshUI();
		};
		GameSection.SetEventData(false);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetLabelText((Enum)UI.LBL_TARGET_JOIN_TYPE, popupJoinType.SelectedString);
		SetLabelText((Enum)UI.LBL_TARGET_LABEL, popupLabels.SelectedString);
		SetLabelText((Enum)UI.LBL_TARGET_JOINABLE, popupJoinable.SelectedString);
	}

	protected void OnChangeName()
	{
		string inputValue = GetInputValue((Enum)UI.IPT_NAME);
		inputValue = inputValue.Replace(" ", string.Empty);
		inputValue = inputValue.Replace("\u3000", string.Empty);
		SetActive((Enum)UI.LBL_DEFAULT, string.IsNullOrEmpty(inputValue));
		searchRequest.name = inputValue;
	}

	private void OnQuery_SEARCH()
	{
		MonoBehaviourSingleton<ClanMatchingManager>.I.SetSearchRequest(searchRequest);
		GameSection.StayEvent();
		MonoBehaviourSingleton<ClanMatchingManager>.I.RequestSearch(delegate(bool is_success, Error err)
		{
			GameSection.ResumeEvent(is_success);
		}, saveSettings: true);
	}

	private void OnQuery_TARGET_JOIN_TYPE()
	{
		popup(popupJoinType);
	}

	private void OnQuery_TARGET_LABEL()
	{
		popup(popupLabels);
	}

	private void OnQuery_TARGET_JOINABLE()
	{
		popup(popupJoinable);
	}

	private void popup(PopuplistParam param)
	{
		if (param.popup_transform == null)
		{
			param.popup_transform = Realizes("ScrollablePopupList", param.parent_ctrl, check_panel: false);
		}
		if (!(param.popup_transform == null))
		{
			bool[] array = new bool[param.texts.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = true;
			}
			if (param.select_index >= param.texts.Count)
			{
				param.select_index = param.texts.Count - 1;
			}
			if (param.select_index < 0)
			{
				param.select_index = 0;
			}
			param.popup_transform.get_gameObject().SetActive(true);
			UIScrollablePopupList.CreatePopup(param.popup_transform, param.parent_ctrl, 5, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, adjust_size: true, param.texts.ToArray(), array, param.select_index, param.callback);
		}
	}
}
