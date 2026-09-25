// Decompiled with JetBrains decompiler
// Type: FBManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Facebook.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FBManager : MonoBehaviourSingleton<FBManager>
{
  private const string GRAPH_API_FIELD_AFTER = "&after=";
  private const string GRAPH_API_QUERY_INVITABLE_FRIENDS = "/me/invitable_friends?fields=id,name,picture&pretty=0&limit=5000";
  private const string GRAPH_API_QUERY_FRIENDS = "/me/friends?fields=id,name,picture&pretty=0&limit=5000";
  private const string FACEBOOK_FRIEND_PLAYERPREF_KEY = "fb_friend_key";
  private Action<bool, string> OnActionCallback;
  private bool isActionExecuting;
  private const float LOGOUT_TIMEOUT = 5f;

  public bool isInitialized => FB.IsInitialized;

  public bool isLoggedIn => FB.IsLoggedIn;

  public string accessToken => AccessToken.CurrentAccessToken.TokenString;

  protected override void Awake()
  {
    if (FB.IsInitialized)
    {
      FB.ActivateApp();
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      FB.Init(FBManager.\u003C\u003Ec.\u003C\u003E9__18_0 ?? (FBManager.\u003C\u003Ec.\u003C\u003E9__18_0 = new InitDelegate((object) FBManager.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CAwake\u003Eb__18_0))), (HideUnityDelegate) null, (string) null);
    }
  }

  private bool CheckAndSetActionExecuting()
  {
    if (this.isActionExecuting)
    {
      Log.Error(LOG.SOCIAL, "isActionExecuting is currently true!");
      return false;
    }
    this.SetActionExecutingFlag(true);
    return true;
  }

  private void SetActionExecutingFlag(bool is_enable)
  {
    this.isActionExecuting = is_enable;
    if (!MonoBehaviourSingleton<UIManager>.IsValid())
      return;
    MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.PROTOCOL, is_enable);
  }

  private bool CheckLogin(Action<bool> callback) => FB.IsLoggedIn;

  public void LoginWithReadPermission(Action<bool, string> callback = null)
  {
    if (!this.CheckAndSetActionExecuting())
      return;
    this.OnActionCallback = callback;
    // ISSUE: method pointer
    FB.LogInWithReadPermissions((IEnumerable<string>) new List<string>()
    {
      "public_profile",
      "email"
    }, new FacebookDelegate<ILoginResult>((object) this, __methodptr(_OnActionComplete)));
  }

  public void LoginWithPublishPermission(Action<bool, string> callback = null)
  {
    if (!this.CheckAndSetActionExecuting())
      return;
    this.OnActionCallback = callback;
    // ISSUE: method pointer
    FB.LogInWithPublishPermissions((IEnumerable<string>) new List<string>()
    {
      "publish_actions"
    }, new FacebookDelegate<ILoginResult>((object) this, __methodptr(_OnActionComplete)));
  }

  public void Logout(Action<bool, string> callback = null)
  {
    if (!this.CheckAndSetActionExecuting())
      return;
    this.OnActionCallback = callback;
    FB.LogOut();
    this.StartCoroutine(this.CheckLogOutStatus());
  }

  private IEnumerator CheckLogOutStatus()
  {
    float timecount = 0.0f;
    bool success = true;
    while (FB.IsLoggedIn)
    {
      timecount += Time.deltaTime;
      if ((double) timecount < 5.0)
      {
        yield return (object) null;
      }
      else
      {
        success = false;
        break;
      }
    }
    this.SetActionExecutingFlag(false);
    if (this.OnActionCallback != null)
      this.OnActionCallback(success, (string) null);
  }

  public void ShareLink(
    string url,
    string contentTitle = "",
    string contentDescription = "",
    string photoURL = "",
    Action<bool, string> callback = null)
  {
    if (!this.CheckAndSetActionExecuting())
      return;
    this.OnActionCallback = callback;
    try
    {
      // ISSUE: method pointer
      FB.ShareLink(new Uri(url), contentTitle, contentDescription, new Uri(photoURL), new FacebookDelegate<IShareResult>((object) this, __methodptr(_OnActionComplete)));
    }
    catch
    {
      this.SetActionExecutingFlag(false);
    }
  }

  public void ShareFeed(
    string told = "",
    string url = "",
    string title = "",
    string caption = "",
    string description = "",
    string pictureUrl = "",
    string mediaSource = "",
    Action<bool, string> callback = null)
  {
    if (!this.CheckAndSetActionExecuting())
      return;
    this.OnActionCallback = callback;
    try
    {
      // ISSUE: method pointer
      FB.FeedShare(told, new Uri(url), title, caption, description, new Uri(pictureUrl), mediaSource, new FacebookDelegate<IShareResult>((object) this, __methodptr(_OnActionComplete)));
    }
    catch
    {
      this.SetActionExecutingFlag(false);
    }
  }

  public void AppInvite(Action<bool, string> callback = null)
  {
    if (!this.CheckAndSetActionExecuting())
      return;
    this.OnActionCallback = callback;
  }

  public void AppRequest(
    string message,
    List<string> to,
    string data,
    string title,
    Action<bool, FBManager.AppRequestResult> callback = null)
  {
    if (!this.CheckAndSetActionExecuting())
      return;
    this.OnActionCallback = (Action<bool, string>) ((success, json) =>
    {
      FBManager.AppRequestResult appRequestResult = (FBManager.AppRequestResult) null;
      if (success)
        appRequestResult = JSONSerializer.Deserialize<FBManager.AppRequestResult>(json);
      callback(success, appRequestResult);
    });
    // ISSUE: method pointer
    FB.AppRequest(message, (IEnumerable<string>) to, (IEnumerable<object>) null, (IEnumerable<string>) null, new int?(), data, title, new FacebookDelegate<IAppRequestResult>((object) this, __methodptr(_OnActionComplete)));
  }

  public FBManager.InvitableFriendInfo invitableFriendInfo { get; set; }

  public void GetInvitableFriends(Action<bool> callback)
  {
    if (!this.CheckAndSetActionExecuting())
      return;
    this.OnActionCallback = (Action<bool, string>) ((success, data) =>
    {
      if (success)
      {
        try
        {
          this.invitableFriendInfo = JsonUtility.FromJson<FBManager.InvitableFriendInfo>(data);
          callback(true);
        }
        catch (Exception ex)
        {
          callback(false);
        }
      }
      else
        callback(false);
    });
    // ISSUE: method pointer
    FB.API("/me/invitable_friends?fields=id,name,picture&pretty=0&limit=5000", (HttpMethod) 0, new FacebookDelegate<IGraphResult>((object) this, __methodptr(_OnActionComplete)), (IDictionary<string, string>) null);
  }

  public FBManager.FriendInfo friendInfo { get; set; }

  public void GetFriends(Action<bool> callback)
  {
    if (!this.CheckAndSetActionExecuting())
      return;
    this.OnActionCallback = (Action<bool, string>) ((success, data) =>
    {
      if (success)
      {
        try
        {
          this.friendInfo = JsonUtility.FromJson<FBManager.FriendInfo>(data);
          callback(true);
        }
        catch (Exception ex)
        {
          callback(false);
        }
      }
      else
        callback(false);
    });
    // ISSUE: method pointer
    FB.API("/me/friends?fields=id,name,picture&pretty=0&limit=5000", (HttpMethod) 0, new FacebookDelegate<IGraphResult>((object) this, __methodptr(_OnActionComplete)), (IDictionary<string, string>) null);
  }

  private void _OnActionComplete(IResult result)
  {
    this.SetActionExecutingFlag(false);
    if (this.OnActionCallback == null)
      Log.Warning(LOG.SOCIAL, "OnActionCallback is null => do nothing!");
    else if (result == null)
      this.OnActionCallback(false, (string) null);
    else if (!string.IsNullOrEmpty(result.Error))
      this.OnActionCallback(false, result.Error);
    else if (result.Cancelled)
      this.OnActionCallback(false, result.RawResult);
    else if (!string.IsNullOrEmpty(result.RawResult))
      this.OnActionCallback(true, result.RawResult);
    else
      this.OnActionCallback(false, (string) null);
  }

  [Serializable]
  public class FriendData
  {
    public string id;
    public string name;
    public FBManager.FriendData.Picture picture;

    public override string ToString()
    {
      return $"id:{this.id} name:{this.name} pictureurl:{this.picture.data.url}";
    }

    [Serializable]
    public class Picture
    {
      public FBManager.FriendData.Picture.PictureData data;

      [Serializable]
      public class PictureData
      {
        public bool is_silhouette;
        public string url;
      }
    }
  }

  [Serializable]
  public class Paging
  {
    public FBManager.Paging.Cursors cursors;
    public string next;

    [Serializable]
    public class Cursors
    {
      public string before;
      public string after;
    }
  }

  [Serializable]
  public class InvitableFriendInfo
  {
    public List<FBManager.FriendData> data;
    public FBManager.Paging paging;
  }

  [Serializable]
  public class FriendInfo
  {
    public FBManager.FriendInfo.Summary summary;
    public List<FBManager.FriendData> data;

    [Serializable]
    public class Summary
    {
      public int total_count;
    }
  }

  [Serializable]
  public class AppRequestResult
  {
    public string request;
    public string to;
  }
}
