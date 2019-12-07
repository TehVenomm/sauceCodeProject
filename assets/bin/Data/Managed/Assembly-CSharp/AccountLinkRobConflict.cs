using Network;
using System.Collections;
using UnityEngine;

public class AccountLinkRobConflict : GameSection
{
	private enum UI
	{
		OBJ_LOCAL,
		OBJ_CLOUD,
		LBL_NAME,
		LBL_DATE,
		LBL_HUNTER_ID,
		LBL_LEVEL,
		LBL_GEM,
		LBL_GOLD,
		TEX_GEM,
		TEX_GOLD
	}

	private const int gemIIC = 1;

	private const int goldIIC = 2;

	private bool _accountChangeSuccess;

	private object[] event_data;

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		UserInfo userInfo = MonoBehaviourSingleton<UserInfoManager>.I.userInfo;
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		Transform local = GetCtrl(UI.OBJ_LOCAL);
		SetLabelText(local, UI.LBL_NAME, userInfo.name);
		SetLabelText(local, UI.LBL_DATE, userInfo.lastLogin);
		SetLabelText(local, UI.LBL_HUNTER_ID, userInfo.code);
		SetLabelText(local, UI.LBL_LEVEL, $"Lv.{userStatus.level.ToString()}");
		SetLabelText(local, UI.LBL_GEM, userStatus.crystal.ToString());
		SetLabelText(local, UI.LBL_GOLD, userStatus.money.ToString());
		Transform cloud = GetCtrl(UI.OBJ_CLOUD);
		event_data = (GameSection.GetEventData() as object[]);
		RegistLinkFacebookModel.ExistInfoParam existInfoParam = event_data[0] as RegistLinkFacebookModel.ExistInfoParam;
		if (existInfoParam != null)
		{
			SetLabelText(cloud, UI.LBL_NAME, existInfoParam.name);
			SetLabelText(cloud, UI.LBL_DATE, existInfoParam.lastLogin);
			SetLabelText(cloud, UI.LBL_HUNTER_ID, existInfoParam.code);
			SetLabelText(cloud, UI.LBL_LEVEL, $"Lv.{existInfoParam.level}");
			SetLabelText(cloud, UI.LBL_GEM, existInfoParam.crystal);
			SetLabelText(cloud, UI.LBL_GOLD, existInfoParam.money);
		}
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject lo_gem = loadingQueue.LoadItemIcon(ResourceName.GetItemIcon(1));
		LoadObject lo_gold = loadingQueue.LoadItemIcon(ResourceName.GetItemIcon(2));
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		SetTexture(local, UI.TEX_GEM, lo_gem.loadedObject as Texture);
		SetTexture(local, UI.TEX_GOLD, lo_gold.loadedObject as Texture);
		SetTexture(cloud, UI.TEX_GEM, lo_gem.loadedObject as Texture);
		SetTexture(cloud, UI.TEX_GOLD, lo_gold.loadedObject as Texture);
		base.Initialize();
	}

	private void OnQuery_CURRENT()
	{
		GameSection.StayEvent();
		string email = (string)event_data[1];
		MonoBehaviourSingleton<AccountManager>.I.SendRegistIgnoreCloudData(email, delegate(bool success)
		{
			if (success)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(NOTIFY_FLAG.FACEBOOK_LOGIN);
			}
			GameSection.ResumeEvent(success);
		});
	}

	private void OnQuery_CLOUD()
	{
		GameSection.StayEvent();
		string email = (string)event_data[1];
		MonoBehaviourSingleton<AccountManager>.I.SendLinkRobWithCloudData(email, delegate(bool success)
		{
			if (success)
			{
				MenuReset.needClearCache = true;
				MenuReset.needPredownload = true;
			}
			GameSection.ResumeEvent(success);
		});
	}

	private void OnCloseDialog_AccountUseLocalData()
	{
		_accountChangeSuccess = true;
	}

	public void Update()
	{
		if (_accountChangeSuccess && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() && !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			_accountChangeSuccess = false;
			RequestEvent("CURRENT_DATA");
			GameSection.BackSection();
		}
	}
}
