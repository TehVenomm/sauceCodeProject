// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.ResponseStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace GooglePlayGames.BasicApi;

public enum ResponseStatus
{
  Timeout = -5, // 0xFFFFFFFB
  VersionUpdateRequired = -4, // 0xFFFFFFFC
  NotAuthorized = -3, // 0xFFFFFFFD
  InternalError = -2, // 0xFFFFFFFE
  LicenseCheckFailed = -1, // 0xFFFFFFFF
  Success = 1,
  SuccessWithStale = 2,
}
