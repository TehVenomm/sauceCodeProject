// Decompiled with JetBrains decompiler
// Type: OnePF.Options
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
namespace OnePF;

public class Options
{
  public const int DISCOVER_TIMEOUT_MS = 5000;
  public const int INVENTORY_CHECK_TIMEOUT_MS = 10000;
  public int discoveryTimeoutMs = 5000;
  public bool checkInventory = true;
  public int checkInventoryTimeoutMs = 10000;
  public OptionsVerifyMode verifyMode;
  public SearchStrategy storeSearchStrategy;
  public Dictionary<string, string> storeKeys = new Dictionary<string, string>();
  public string[] prefferedStoreNames = new string[0];
  public string[] availableStoreNames = new string[0];
  public int samsungCertificationRequestCode;
  public string goPayAppId;
  public string appUserId;
  public string appUserEmail;
}
