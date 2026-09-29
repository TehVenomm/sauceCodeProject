// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.GameServices
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal class GameServices : BaseReferenceHolder
{
  internal GameServices(IntPtr selfPointer)
    : base(selfPointer)
  {
  }

  internal bool IsAuthenticated() => GooglePlayGames.Native.Cwrapper.GameServices.GameServices_IsAuthorized(this.SelfPtr());

  internal void SignOut() => GooglePlayGames.Native.Cwrapper.GameServices.GameServices_SignOut(this.SelfPtr());

  internal void StartAuthorizationUI()
  {
    GooglePlayGames.Native.Cwrapper.GameServices.GameServices_StartAuthorizationUI(this.SelfPtr());
  }

  public GooglePlayGames.Native.PInvoke.AchievementManager AchievementManager()
  {
    return new GooglePlayGames.Native.PInvoke.AchievementManager(this);
  }

  public GooglePlayGames.Native.PInvoke.LeaderboardManager LeaderboardManager()
  {
    return new GooglePlayGames.Native.PInvoke.LeaderboardManager(this);
  }

  public GooglePlayGames.Native.PInvoke.PlayerManager PlayerManager() => new GooglePlayGames.Native.PInvoke.PlayerManager(this);

  public GooglePlayGames.Native.PInvoke.StatsManager StatsManager() => new GooglePlayGames.Native.PInvoke.StatsManager(this);

  internal HandleRef AsHandle() => this.SelfPtr();

  protected override void CallDispose(HandleRef selfPointer)
  {
    GooglePlayGames.Native.Cwrapper.GameServices.GameServices_Dispose(selfPointer);
  }
}
