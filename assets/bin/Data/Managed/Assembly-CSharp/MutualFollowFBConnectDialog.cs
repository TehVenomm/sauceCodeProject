using System;
using System.Collections;
using UnityEngine;

public class MutualFollowFBConnectDialog : GameSection
{
	private enum UI
	{
		SPR_MUTUAL_FOLLOW_BANNER,
		BTN_CONNECT
	}

	private const string MUTUAL_FOLLOW_BANNER_NAME = "IMG_00000001";

	public override void Initialize()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(LoadTopBanner());
		base.Initialize();
	}

	private IEnumerator LoadTopBanner()
	{
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject lo_image = loadQueue.Load(RESOURCE_CATEGORY.COMMON, "IMG_00000001", false);
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		if (lo_image.loadedObject == null)
		{
			yield return (object)null;
		}
		Texture bannerImg = lo_image.loadedObject as Texture;
		Transform banner = GetCtrl(UI.SPR_MUTUAL_FOLLOW_BANNER);
		UITexture uiTexture = banner.GetComponent<UITexture>();
		uiTexture.mainTexture = bannerImg;
	}

	private unsafe void OnQuery_CONNECT()
	{
		GameSection.StayEvent();
		if (MonoBehaviourSingleton<FBManager>.I.isLoggedIn)
		{
			_SendRegistLinkFacebook();
		}
		else
		{
			MonoBehaviourSingleton<FBManager>.I.LoginWithReadPermission(new Action<bool, string>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	private unsafe void _SendRegistLinkFacebook()
	{
		AccountManager i = MonoBehaviourSingleton<AccountManager>.I;
		string accessToken = MonoBehaviourSingleton<FBManager>.I.accessToken;
		if (_003C_003Ef__am_0024cache0 == null)
		{
			_003C_003Ef__am_0024cache0 = new Action<bool, RegistLinkFacebookModel>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		i.SendRegistLinkFacebook(accessToken, _003C_003Ef__am_0024cache0);
	}
}
