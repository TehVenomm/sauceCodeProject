using Network;
using System.Collections;
using UnityEngine;

public class AccountConflict : GameSection
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

	public override void Initialize()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		UserInfo user_info = MonoBehaviourSingleton<UserInfoManager>.I.userInfo;
		UserStatus user_status = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		Transform local = GetCtrl(UI.OBJ_LOCAL);
		SetLabelText(local, UI.LBL_NAME, user_info.name);
		SetLabelText(local, UI.LBL_DATE, user_info.lastLogin);
		SetLabelText(local, UI.LBL_HUNTER_ID, user_info.code);
		SetLabelText(local, UI.LBL_LEVEL, $"Lv.{user_status.level.ToString()}");
		SetLabelText(local, UI.LBL_GEM, user_status.crystal.ToString());
		SetLabelText(local, UI.LBL_GOLD, user_status.money.ToString());
		Transform cloud = GetCtrl(UI.OBJ_CLOUD);
		RegistLinkFacebookModel.ExistInfoParam info = GameSection.GetEventData() as RegistLinkFacebookModel.ExistInfoParam;
		if (info != null)
		{
			SetLabelText(cloud, UI.LBL_NAME, info.name);
			SetLabelText(cloud, UI.LBL_DATE, info.lastLogin);
			SetLabelText(cloud, UI.LBL_HUNTER_ID, info.code);
			SetLabelText(cloud, UI.LBL_LEVEL, $"Lv.{info.level}");
			SetLabelText(cloud, UI.LBL_GEM, info.crystal);
			SetLabelText(cloud, UI.LBL_GOLD, info.money);
		}
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject lo_gem = loadQueue.LoadItemIcon(ResourceName.GetItemIcon(1));
		LoadObject lo_gold = loadQueue.LoadItemIcon(ResourceName.GetItemIcon(2));
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
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
		MonoBehaviourSingleton<AccountManager>.I.SendRegistOverrideFacebook(MonoBehaviourSingleton<FBManager>.I.accessToken, delegate(bool success)
		{
			if (success)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(NOTIFY_FLAG.FACEBOOK_LOGIN);
			}
			GameSection.ResumeEvent(success, null);
		});
	}

	private void OnQuery_CLOUD()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<AccountManager>.I.SendRegistAuthFacebook(MonoBehaviourSingleton<FBManager>.I.accessToken, delegate(bool success)
		{
			if (success)
			{
				MenuReset.needClearCache = true;
				MenuReset.needPredownload = true;
			}
			GameSection.ResumeEvent(success, null);
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
			RequestEvent("CURRENT_DATA", null);
			GameSection.BackSection();
		}
	}
}
