using Network;
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
		StartCoroutine(LoadTopBanner());
		base.Initialize();
	}

	private IEnumerator LoadTopBanner()
	{
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject lo_image = loadingQueue.Load(RESOURCE_CATEGORY.COMMON, "IMG_00000001");
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		if (lo_image.loadedObject == null)
		{
			yield return null;
		}
		Texture mainTexture = lo_image.loadedObject as Texture;
		GetCtrl(UI.SPR_MUTUAL_FOLLOW_BANNER).GetComponent<UITexture>().mainTexture = mainTexture;
	}

	private void OnQuery_CONNECT()
	{
		GameSection.StayEvent();
		if (MonoBehaviourSingleton<FBManager>.I.isLoggedIn)
		{
			_SendRegistLinkFacebook();
		}
		else
		{
			MonoBehaviourSingleton<FBManager>.I.LoginWithReadPermission(delegate(bool success, string s)
			{
				if (success)
				{
					_SendRegistLinkFacebook();
				}
				else
				{
					GameSection.ResumeEvent(success);
				}
			});
		}
	}

	private void _SendRegistLinkFacebook()
	{
		MonoBehaviourSingleton<AccountManager>.I.SendRegistLinkFacebook(MonoBehaviourSingleton<FBManager>.I.accessToken, delegate(bool success, RegistLinkFacebookModel ret)
		{
			if (success)
			{
				GameSection.ResumeEvent(success);
			}
			else
			{
				if (ret.Error == Error.WRN_REGISTER_FACEBOOK_ACCOUNT_LINKED)
				{
					GameSection.ChangeStayEvent("ACCOUNT_CONFLICT", ret.existInfo);
					success = true;
				}
				GameSection.ResumeEvent(success);
			}
		});
	}
}
