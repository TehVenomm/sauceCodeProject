using System;
using System.Collections.Generic;

[Serializable]
public class ServerAccountSaveData
{
	[Serializable]
	public class ServerAccount
	{
		public string url;

		public AccountManager.Account account;
	}

	public List<ServerAccount> serverAccount = new List<ServerAccount>();

	public static ServerAccountSaveData instance
	{
		get;
		private set;
	}

	public static void Load()
	{
		if (!SaveData.HasKey(SaveData.Key.ServerAccount))
		{
			instance = new ServerAccountSaveData();
			SaveData.SetData(SaveData.Key.ServerAccount, instance);
			SaveData.Save();
		}
		else
		{
			instance = SaveData.GetData<ServerAccountSaveData>(SaveData.Key.ServerAccount);
		}
	}

	public static void Save()
	{
		if (instance != null)
		{
			SaveData.SetData(SaveData.Key.ServerAccount, instance);
			SaveData.Save();
		}
	}

	public static void Delete()
	{
		SaveData.DeleteKey(SaveData.Key.ServerAccount);
		instance = new ServerAccountSaveData();
		SaveData.SetData(SaveData.Key.ServerAccount, instance);
		SaveData.Save();
	}

	public void UpdateAccount(string url, AccountManager.Account account)
	{
		if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(account.userHash))
		{
			foreach (ServerAccount item in serverAccount)
			{
				if (item.url.Equals(NetworkManager.APP_HOST))
				{
					item.account = account;
					Save();
					return;
				}
			}
			serverAccount.Add(new ServerAccount
			{
				url = url,
				account = account
			});
			Save();
		}
	}

	public AccountManager.Account GetAccountOnServer(string url)
	{
		foreach (ServerAccount item in serverAccount)
		{
			if (item.url.Equals(NetworkManager.APP_HOST) && !string.IsNullOrEmpty(item.account.userHash))
			{
				return item.account;
			}
		}
		return null;
	}

	public void RemoveAccount(string serverUrl)
	{
		foreach (ServerAccount item in serverAccount)
		{
			if (item.url.Equals(serverUrl))
			{
				serverAccount.Remove(item);
				Save();
				break;
			}
		}
	}
}
