using MsgPack;
using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Networking;

public class GoGameResourceManager : MonoBehaviourSingleton<GoGameResourceManager>
{
	public bool isLoadingVariantManifest;

	public Dictionary<int, string> variantManifest;

	public bool isLoadingServerList;

	private void Start()
	{
		isLoadingVariantManifest = false;
	}

	public static string GetDefaultAssetBundleExtension()
	{
		return ".dat";
	}

	public void LoadVariantManifest()
	{
		StartCoroutine(IELoadVariantManifest());
	}

	private IEnumerator IELoadVariantManifest()
	{
		isLoadingVariantManifest = true;
		variantManifest = null;
		string url = string.Format("{0}{1}", MonoBehaviourSingleton<ResourceManager>.I.downloadURL, "variants_manifest.bytes");
		Error error_code;
		do
		{
			error_code = Error.None;
			UnityWebRequest _www = new UnityWebRequest(url)
			{
				downloadHandler = new DownloadHandlerBuffer()
			};
			yield return _www.SendWebRequest();
			string error = _www.error;
			if (!string.IsNullOrEmpty(error))
			{
				error_code = ((!error.Contains("404")) ? Error.AssetLoadFailed : Error.AssetNotFound);
			}
			else if (_www.downloadHandler.data != null)
			{
				variantManifest = new ObjectPacker().Unpack<Dictionary<int, string>>(_www.downloadHandler.data);
			}
			else
			{
				error_code = Error.AssetLoadFailed;
				Log.Error(LOG.RESOURCE, _www.downloadHandler.text);
			}
			_www.Dispose();
			if (error_code != 0)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, StringTable.Format(STRING_CATEGORY.COMMON_DIALOG, 1001u, error_code), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 100u)), delegate
				{
					MonoBehaviourSingleton<AppMain>.I.Reset();
				}, error: true, (int)error_code);
				break;
			}
		}
		while (error_code != 0);
		if (error_code == Error.None)
		{
			isLoadingVariantManifest = false;
		}
		else
		{
			isLoadingVariantManifest = true;
		}
	}

	public string GetVariantName(RESOURCE_CATEGORY? category)
	{
		if (!category.HasValue)
		{
			return "";
		}
		if (category == RESOURCE_CATEGORY.SOUND_VOICE)
		{
			if (GameSaveData.instance.voiceOption == 2)
			{
				return ".en-sd";
			}
			return "." + InGameManager.voiceVariants[GameSaveData.instance.voiceOption];
		}
		if (variantManifest == null)
		{
			return "";
		}
		if (variantManifest.ContainsKey((int)category.Value))
		{
			if (variantManifest[(int)category.Value].Contains(InGameManager.languageVariants[GameSaveData.instance.languageOption]))
			{
				return "." + InGameManager.languageVariants[GameSaveData.instance.languageOption];
			}
			return ".en-sd";
		}
		return "";
	}

	public string GetFullBundleName(string bundleName)
	{
		string[] split = bundleName.Split('.');
		if (!InGameManager.languageVariants.Any((string s) => split[split.Length - 1].Equals(s)))
		{
			bundleName += GetVariantName(GetCategory(bundleName));
		}
		return bundleName;
	}

	public RESOURCE_CATEGORY? GetCategory(string fullBundleName)
	{
		fullBundleName = fullBundleName.ToUpper();
		string value = Enum.GetNames(typeof(RESOURCE_CATEGORY)).FirstOrDefault((string s) => fullBundleName.Contains(s + "/"));
		if (string.IsNullOrEmpty(value))
		{
			return null;
		}
		return (RESOURCE_CATEGORY)Enum.Parse(typeof(RESOURCE_CATEGORY), value);
	}

	public string GetBundleNameWithoutVariant(string fullBundleName)
	{
		string[] splits = fullBundleName.Split('.');
		if (InGameManager.languageVariants.Any((string s) => splits[splits.Length - 1].Equals(s)))
		{
			return fullBundleName.Remove(fullBundleName.LastIndexOf("."));
		}
		return fullBundleName;
	}

	public void LoadServerList()
	{
		StartCoroutine(IELoadServerList());
	}

	private IEnumerator IELoadServerList()
	{
		string www_url2 = NetworkManager.TABLE_HOST;
		www_url2 += "/serverlisttable.dat";
		isLoadingServerList = true;
		Error error_code;
		do
		{
			error_code = Error.None;
			UnityWebRequest _www = new UnityWebRequest(www_url2)
			{
				downloadHandler = new DownloadHandlerBuffer()
			};
			yield return _www.SendWebRequest();
			string error = _www.error;
			if (!string.IsNullOrEmpty(error))
			{
				error_code = ((!error.Contains("404")) ? Error.AssetLoadFailed : Error.AssetNotFound);
			}
			else if (_www.downloadHandler.data != null)
			{
				Singleton<ServerListTable>.Create();
				Singleton<ServerListTable>.I.CreateTable(DataTableManager.DecompressToString(_www.downloadHandler.data));
			}
			else
			{
				error_code = Error.AssetLoadFailed;
				Log.Error(LOG.RESOURCE, _www.downloadHandler.text);
			}
			_www.Dispose();
			if (error_code != 0)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, StringTable.Format(STRING_CATEGORY.COMMON_DIALOG, 1001u, error_code), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 100u)), delegate
				{
					MonoBehaviourSingleton<AppMain>.I.Reset();
				}, error: true, (int)error_code);
				break;
			}
		}
		while (error_code != 0);
		if (error_code == Error.None)
		{
			isLoadingServerList = false;
		}
		else
		{
			isLoadingVariantManifest = true;
		}
	}
}
