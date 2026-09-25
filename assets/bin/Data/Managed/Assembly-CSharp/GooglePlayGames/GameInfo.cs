// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.GameInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace GooglePlayGames;

public static class GameInfo
{
  private const string UnescapedApplicationId = "APP_ID";
  private const string UnescapedIosClientId = "IOS_CLIENTID";
  private const string UnescapedWebClientId = "WEB_CLIENTID";
  private const string UnescapedNearbyServiceId = "NEARBY_SERVICE_ID";
  public const string ApplicationId = "683498632423";
  public const string IosClientId = "";
  public const string WebClientId = "683498632423-6p90updcgm6b67r4ucmhs82nkq1dc1mi.apps.googleusercontent.com";
  public const string NearbyConnectionServiceId = "";

  public static bool ApplicationIdInitialized()
  {
    return !string.IsNullOrEmpty("683498632423") && !"683498632423".Equals(GameInfo.ToEscapedToken("APP_ID"));
  }

  public static bool IosClientIdInitialized()
  {
    return !string.IsNullOrEmpty("") && !"".Equals(GameInfo.ToEscapedToken("IOS_CLIENTID"));
  }

  public static bool WebClientIdInitialized()
  {
    return !string.IsNullOrEmpty("683498632423-6p90updcgm6b67r4ucmhs82nkq1dc1mi.apps.googleusercontent.com") && !"683498632423-6p90updcgm6b67r4ucmhs82nkq1dc1mi.apps.googleusercontent.com".Equals(GameInfo.ToEscapedToken("WEB_CLIENTID"));
  }

  public static bool NearbyConnectionsInitialized()
  {
    return !string.IsNullOrEmpty("") && !"".Equals(GameInfo.ToEscapedToken("NEARBY_SERVICE_ID"));
  }

  private static string ToEscapedToken(string token) => $"__{token}__";
}
