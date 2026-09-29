// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.PlayGamesUserProfile
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.OurUtils;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms;

#nullable disable
namespace GooglePlayGames;

public class PlayGamesUserProfile : IUserProfile
{
  private string mDisplayName;
  private string mPlayerId;
  private string mAvatarUrl;
  private volatile bool mImageLoading;
  private Texture2D mImage;

  internal PlayGamesUserProfile(string displayName, string playerId, string avatarUrl)
  {
    this.mDisplayName = displayName;
    this.mPlayerId = playerId;
    this.mAvatarUrl = avatarUrl;
    this.mImageLoading = false;
  }

  protected void ResetIdentity(string displayName, string playerId, string avatarUrl)
  {
    this.mDisplayName = displayName;
    this.mPlayerId = playerId;
    if (this.mAvatarUrl != avatarUrl)
    {
      this.mImage = (Texture2D) null;
      this.mAvatarUrl = avatarUrl;
    }
    this.mImageLoading = false;
  }

  public string userName => this.mDisplayName;

  public string id => this.mPlayerId;

  public bool isFriend => true;

  public UserState state => (UserState) 0;

  public Texture2D image
  {
    get
    {
      if (!this.mImageLoading && Object.op_Equality((Object) this.mImage, (Object) null) && !string.IsNullOrEmpty(this.AvatarURL))
      {
        Debug.Log((object) ("Starting to load image: " + this.AvatarURL));
        this.mImageLoading = true;
        PlayGamesHelperObject.RunCoroutine(this.LoadImage());
      }
      return this.mImage;
    }
  }

  public string AvatarURL => this.mAvatarUrl;

  internal IEnumerator LoadImage()
  {
    if (!string.IsNullOrEmpty(this.AvatarURL))
    {
      UnityWebRequest www = UnityWebRequestTexture.GetTexture(this.AvatarURL);
      www.SendWebRequest();
      while (!www.isDone)
        yield return (object) null;
      if (www.error == null)
      {
        this.mImage = DownloadHandlerTexture.GetContent(www);
      }
      else
      {
        this.mImage = Texture2D.blackTexture;
        Debug.Log((object) ("Error downloading image: " + www.error));
      }
      this.mImageLoading = false;
      www = (UnityWebRequest) null;
    }
    else
    {
      Debug.Log((object) "No URL found.");
      this.mImage = Texture2D.blackTexture;
      this.mImageLoading = false;
    }
  }

  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    if (this == obj)
      return true;
    return obj is PlayGamesUserProfile gamesUserProfile && StringComparer.Ordinal.Equals(this.mPlayerId, gamesUserProfile.mPlayerId);
  }

  public override int GetHashCode()
  {
    return typeof (PlayGamesUserProfile).GetHashCode() ^ this.mPlayerId.GetHashCode();
  }

  public override string ToString() => $"[Player: '{this.mDisplayName}' (id {this.mPlayerId})]";
}
