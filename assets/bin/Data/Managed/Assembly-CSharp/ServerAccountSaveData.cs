// Decompiled with JetBrains decompiler
// Type: ServerAccountSaveData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
[Serializable]
public class ServerAccountSaveData
{
  public List<ServerAccountSaveData.ServerAccount> serverAccount = new List<ServerAccountSaveData.ServerAccount>();

  public static ServerAccountSaveData instance { get; private set; }

  public static void Load()
  {
    if (!SaveData.HasKey(SaveData.Key.ServerAccount))
    {
      ServerAccountSaveData.instance = new ServerAccountSaveData();
      SaveData.SetData<ServerAccountSaveData>(SaveData.Key.ServerAccount, ServerAccountSaveData.instance);
      SaveData.Save();
    }
    else
      ServerAccountSaveData.instance = SaveData.GetData<ServerAccountSaveData>(SaveData.Key.ServerAccount);
  }

  public static void Save()
  {
    if (ServerAccountSaveData.instance == null)
      return;
    SaveData.SetData<ServerAccountSaveData>(SaveData.Key.ServerAccount, ServerAccountSaveData.instance);
    SaveData.Save();
  }

  public static void Delete()
  {
    SaveData.DeleteKey(SaveData.Key.ServerAccount);
    ServerAccountSaveData.instance = new ServerAccountSaveData();
    SaveData.SetData<ServerAccountSaveData>(SaveData.Key.ServerAccount, ServerAccountSaveData.instance);
    SaveData.Save();
  }

  public void UpdateAccount(string url, AccountManager.Account account)
  {
    if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(account.userHash))
      return;
    foreach (ServerAccountSaveData.ServerAccount serverAccount in this.serverAccount)
    {
      if (serverAccount.url.Equals(NetworkManager.APP_HOST))
      {
        serverAccount.account = account;
        ServerAccountSaveData.Save();
        return;
      }
    }
    this.serverAccount.Add(new ServerAccountSaveData.ServerAccount()
    {
      url = url,
      account = account
    });
    ServerAccountSaveData.Save();
  }

  public AccountManager.Account GetAccountOnServer(string url)
  {
    foreach (ServerAccountSaveData.ServerAccount serverAccount in this.serverAccount)
    {
      if (serverAccount.url.Equals(NetworkManager.APP_HOST) && !string.IsNullOrEmpty(serverAccount.account.userHash))
        return serverAccount.account;
    }
    return (AccountManager.Account) null;
  }

  public void RemoveAccount(string serverUrl)
  {
    foreach (ServerAccountSaveData.ServerAccount serverAccount in this.serverAccount)
    {
      if (serverAccount.url.Equals(serverUrl))
      {
        this.serverAccount.Remove(serverAccount);
        ServerAccountSaveData.Save();
        break;
      }
    }
  }

  [Serializable]
  public class ServerAccount
  {
    public string url;
    public AccountManager.Account account;
  }
}
