using Facebook.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBManager : MonoBehaviourSingleton<FBManager>
{
	[Serializable]
	public class FriendData
	{
		[Serializable]
		public class Picture
		{
			[Serializable]
			public class PictureData
			{
				public bool is_silhouette;

				public string url;
			}

			public PictureData data;
		}

		public string id;

		public string name;

		public Picture picture;

		public override string ToString()
		{
			return $"id:{id} name:{name} pictureurl:{picture.data.url}";
		}
	}

	[Serializable]
	public class Paging
	{
		[Serializable]
		public class Cursors
		{
			public string before;

			public string after;
		}

		public Cursors cursors;

		public string next;
	}

	[Serializable]
	public class InvitableFriendInfo
	{
		public List<FriendData> data;

		public Paging paging;
	}

	[Serializable]
	public class FriendInfo
	{
		[Serializable]
		public class Summary
		{
			public int total_count;
		}

		public Summary summary;

		public List<FriendData> data;
	}

	[Serializable]
	public class AppRequestResult
	{
		public string request;

		public string to;
	}

	private const string GRAPH_API_FIELD_AFTER = "&after=";

	private const string GRAPH_API_QUERY_INVITABLE_FRIENDS = "/me/invitable_friends?fields=id,name,picture&pretty=0&limit=5000";

	private const string GRAPH_API_QUERY_FRIENDS = "/me/friends?fields=id,name,picture&pretty=0&limit=5000";

	private const string FACEBOOK_FRIEND_PLAYERPREF_KEY = "fb_friend_key";

	private const float LOGOUT_TIMEOUT = 5f;

	private Action<bool, string> OnActionCallback;

	private bool isActionExecuting;

	public bool isInitialized => FB.get_IsInitialized();

	public bool isLoggedIn => FB.get_IsLoggedIn();

	public string accessToken => AccessToken.get_CurrentAccessToken().get_TokenString();

	public InvitableFriendInfo invitableFriendInfo
	{
		get;
		set;
	}

	public FriendInfo friendInfo
	{
		get;
		set;
	}

	protected unsafe override void Awake()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		if (FB.get_IsInitialized())
		{
			FB.ActivateApp();
		}
		else
		{
			if (_003C_003Ef__am_0024cache4 == null)
			{
				_003C_003Ef__am_0024cache4 = new InitDelegate((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			FB.Init(_003C_003Ef__am_0024cache4, null, (string)null);
		}
	}

	private bool CheckAndSetActionExecuting()
	{
		if (isActionExecuting)
		{
			Log.Error(LOG.SOCIAL, "isActionExecuting is currently true!");
			return false;
		}
		SetActionExecutingFlag(true);
		return true;
	}

	private void SetActionExecutingFlag(bool is_enable)
	{
		isActionExecuting = is_enable;
		if (MonoBehaviourSingleton<UIManager>.IsValid())
		{
			MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.PROTOCOL, is_enable);
		}
	}

	private bool CheckLogin(Action<bool> callback)
	{
		if (!FB.get_IsLoggedIn())
		{
			return false;
		}
		return true;
	}

	public unsafe void LoginWithReadPermission(Action<bool, string> callback = null)
	{
		if (CheckAndSetActionExecuting())
		{
			OnActionCallback = callback;
			List<string> list = new List<string>();
			list.Add("public_profile");
			list.Add("email");
			list.Add("user_friends");
			FB.LogInWithReadPermissions((IEnumerable<string>)list, new FacebookDelegate<ILoginResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	public unsafe void LoginWithPublishPermission(Action<bool, string> callback = null)
	{
		if (CheckAndSetActionExecuting())
		{
			OnActionCallback = callback;
			List<string> list = new List<string>();
			list.Add("publish_actions");
			FB.LogInWithPublishPermissions((IEnumerable<string>)list, new FacebookDelegate<ILoginResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	public void Logout(Action<bool, string> callback = null)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		if (CheckAndSetActionExecuting())
		{
			OnActionCallback = callback;
			FB.LogOut();
			this.StartCoroutine(CheckLogOutStatus());
		}
	}

	private IEnumerator CheckLogOutStatus()
	{
		float timecount = 0f;
		bool success = true;
		while (FB.get_IsLoggedIn())
		{
			timecount += Time.get_deltaTime();
			if (!(timecount < 5f))
			{
				success = false;
				break;
			}
			yield return (object)null;
		}
		SetActionExecutingFlag(false);
		if (OnActionCallback != null)
		{
			OnActionCallback.Invoke(success, (string)null);
		}
	}

	public unsafe void ShareLink(string url, string contentTitle = "", string contentDescription = "", string photoURL = "", Action<bool, string> callback = null)
	{
		if (CheckAndSetActionExecuting())
		{
			OnActionCallback = callback;
			try
			{
				FB.ShareLink(new Uri(url), contentTitle, contentDescription, new Uri(photoURL), new FacebookDelegate<IShareResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			catch
			{
				SetActionExecutingFlag(false);
			}
		}
	}

	public unsafe void ShareFeed(string told = "", string url = "", string title = "", string caption = "", string description = "", string pictureUrl = "", string mediaSource = "", Action<bool, string> callback = null)
	{
		if (CheckAndSetActionExecuting())
		{
			OnActionCallback = callback;
			try
			{
				FB.FeedShare(told, new Uri(url), title, caption, description, new Uri(pictureUrl), mediaSource, new FacebookDelegate<IShareResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			catch
			{
				SetActionExecutingFlag(false);
			}
		}
	}

	public unsafe void AppInvite(Action<bool, string> callback = null)
	{
		if (CheckAndSetActionExecuting())
		{
			OnActionCallback = callback;
			try
			{
				Mobile.AppInvite(new Uri("https://fb.me/892708710750483"), new Uri("http://i.imgur.com/zkYlB.jpg"), new FacebookDelegate<IAppInviteResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			catch (Exception)
			{
				SetActionExecutingFlag(false);
			}
		}
	}

	public unsafe void AppRequest(string message, List<string> to, string data, string title, Action<bool, AppRequestResult> callback = null)
	{
		if (CheckAndSetActionExecuting())
		{
			_003CAppRequest_003Ec__AnonStorey7C9 _003CAppRequest_003Ec__AnonStorey7C;
			OnActionCallback = new Action<bool, string>((object)_003CAppRequest_003Ec__AnonStorey7C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			FB.AppRequest(message, (IEnumerable<string>)to, (IEnumerable<object>)null, (IEnumerable<string>)null, (int?)null, data, title, new FacebookDelegate<IAppRequestResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	public unsafe void GetInvitableFriends(Action<bool> callback)
	{
		if (CheckAndSetActionExecuting())
		{
			_003CGetInvitableFriends_003Ec__AnonStorey7CA _003CGetInvitableFriends_003Ec__AnonStorey7CA;
			OnActionCallback = new Action<bool, string>((object)_003CGetInvitableFriends_003Ec__AnonStorey7CA, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			FB.API("/me/invitable_friends?fields=id,name,picture&pretty=0&limit=5000", 0, new FacebookDelegate<IGraphResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), (IDictionary<string, string>)null);
		}
	}

	public unsafe void GetFriends(Action<bool> callback)
	{
		if (CheckAndSetActionExecuting())
		{
			_003CGetFriends_003Ec__AnonStorey7CB _003CGetFriends_003Ec__AnonStorey7CB;
			OnActionCallback = new Action<bool, string>((object)_003CGetFriends_003Ec__AnonStorey7CB, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			FB.API("/me/friends?fields=id,name,picture&pretty=0&limit=5000", 0, new FacebookDelegate<IGraphResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), (IDictionary<string, string>)null);
		}
	}

	private void _OnActionComplete(IResult result)
	{
		SetActionExecutingFlag(false);
		if (OnActionCallback == null)
		{
			Log.Warning(LOG.SOCIAL, "OnActionCallback is null => do nothing!");
		}
		else if (result == null)
		{
			OnActionCallback.Invoke(false, (string)null);
		}
		else if (!string.IsNullOrEmpty(result.get_Error()))
		{
			OnActionCallback.Invoke(false, result.get_Error());
		}
		else if (result.get_Cancelled())
		{
			OnActionCallback.Invoke(false, result.get_RawResult());
		}
		else if (!string.IsNullOrEmpty(result.get_RawResult()))
		{
			OnActionCallback.Invoke(true, result.get_RawResult());
		}
		else
		{
			OnActionCallback.Invoke(false, (string)null);
		}
	}
}
