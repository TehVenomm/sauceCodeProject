using System;
using UnityEngine;

public class StatusScreenShot : GameSection
{
	private enum UI
	{
		OBJ_DEGREE_ROOT,
		LBL_LV_NOW,
		LBL_NAME,
		LBL_HOUND_ID,
		LBL_COMMENT,
		OBJ_FRAME_1,
		OBJ_FRAME_2,
		SPR_COMMENT,
		OBJ_LEVEL,
		OBJ_EVENTLISTENER
	}

	private int filter;

	private PlayerLoader playerLoader;

	private UIEventListener eventListener;

	private bool isInitializeDegree;

	public override void Initialize()
	{
		base.Initialize();
		LoadFilterType();
		playerLoader = MonoBehaviourSingleton<StatusStageManager>.I.GetPlayerLoader();
		if (eventListener == null)
		{
			eventListener = GetCtrl(UI.OBJ_EVENTLISTENER).GetComponent<UIEventListener>();
			UIEventListener uIEventListener = eventListener;
			uIEventListener.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uIEventListener.onDrag, new UIEventListener.VectorDelegate(OnDrag));
		}
	}

	private void OnEnable()
	{
		if (eventListener != null)
		{
			UIEventListener uIEventListener = eventListener;
			uIEventListener.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uIEventListener.onDrag, new UIEventListener.VectorDelegate(OnDrag));
		}
	}

	private void OnDisable()
	{
		UIEventListener uIEventListener = eventListener;
		uIEventListener.onDrag = (UIEventListener.VectorDelegate)Delegate.Remove(uIEventListener.onDrag, new UIEventListener.VectorDelegate(OnDrag));
	}

	private void OnDrag(GameObject obj, Vector2 move)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		if (!(playerLoader == null) && !MonoBehaviourSingleton<UIManager>.I.IsDisable() && !(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != "StatusScreenShot"))
		{
			playerLoader.get_transform().Rotate(GameDefine.GetCharaRotateVector(move));
		}
	}

	private void LoadFilterType()
	{
		filter = GameSaveData.instance.ScreenShotUIFilterType;
		if (filter == -1)
		{
			filter = 31;
		}
	}

	public override void UpdateUI()
	{
		CreateDegree();
		SetLabelText((Enum)UI.LBL_LV_NOW, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level.ToString());
		SetLabelText((Enum)UI.LBL_NAME, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name);
		SetLabelText((Enum)UI.LBL_HOUND_ID, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.code);
		SetLabelText((Enum)UI.LBL_COMMENT, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.comment);
		SetEnableAll();
	}

	private void SetEnableAll()
	{
		SetActive((Enum)UI.LBL_NAME, (filter & 1) == 1);
		SetActive((Enum)UI.OBJ_LEVEL, (filter & 2) == 2);
		SetActive((Enum)UI.OBJ_FRAME_1, (filter & 1) == 1 || (filter & 2) == 2);
		SetActive((Enum)UI.OBJ_FRAME_2, (filter & 4) == 4);
		SetActive((Enum)UI.SPR_COMMENT, (filter & 8) == 8);
		if (isInitializeDegree)
		{
			SetActive((Enum)UI.OBJ_DEGREE_ROOT, (filter & 0x10) == 16);
		}
	}

	private void OnQuery_VIEWER_SETTING()
	{
		GameSection.SetEventData(filter);
	}

	private void OnQuery_BACK()
	{
	}

	private void OnCloseDialog_StatusScreenShotViewerSetting()
	{
		LoadFilterType();
		SetEnableAll();
	}

	private void CreateDegree()
	{
		DegreePlate component = GetCtrl(UI.OBJ_DEGREE_ROOT).GetComponent<DegreePlate>();
		component.Initialize(MonoBehaviourSingleton<UserInfoManager>.I.selectedDegreeIds, isButton: false, delegate
		{
			SetActive((Enum)UI.OBJ_DEGREE_ROOT, (filter & 0x10) == 16);
			isInitializeDegree = true;
		});
	}
}
