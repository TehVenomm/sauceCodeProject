using MsgPack;
using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoGameResourceManager : MonoBehaviourSingleton<GoGameResourceManager>
{
	public bool isLoadingVariantManifest;

	public Dictionary<int, string> variantManifest;

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
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(IELoadVariantManifest());
	}

	private IEnumerator IELoadVariantManifest()
	{
		isLoadingVariantManifest = true;
		variantManifest = null;
		string url = string.Format("{0}{1}", MonoBehaviourSingleton<ResourceManager>.I.downloadURL, "variants_manifest.bytes");
		Error error_code = Error.None;
		string www_url = url;
		WWW _www = new WWW(www_url);
		yield return (object)_www;
		string www_error = _www.get_error();
		if (!string.IsNullOrEmpty(www_error))
		{
			error_code = ((!www_error.Contains("404")) ? Error.AssetLoadFailed : Error.AssetNotFound);
		}
		else if (_www.get_bytes() != null)
		{
			variantManifest = new ObjectPacker().Unpack<Dictionary<int, string>>(_www.get_bytes());
		}
		else
		{
			error_code = Error.AssetLoadFailed;
			Log.Error(LOG.RESOURCE, _www.get_text());
		}
		_www.Dispose();
		switch (error_code)
		{
		default:
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, StringTable.Format(STRING_CATEGORY.COMMON_DIALOG, 1001u, error_code), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 100u), null, null, null), delegate
			{
				MonoBehaviourSingleton<AppMain>.I.Reset();
			}, true, (int)error_code);
			break;
		case Error.None:
			break;
		}
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
			return string.Empty;
		}
		if (category == RESOURCE_CATEGORY.SOUND_VOICE)
		{
			if (GameSaveData.instance.voiceOption == 2)
			{
				return ".en-sd";
			}
			return "." + InGameManager.voiceVariants[GameSaveData.instance.voiceOption];
		}
		if (variantManifest.ContainsKey((int)category.Value))
		{
			if (variantManifest[(int)category.Value].Contains(InGameManager.languageVariants[GameSaveData.instance.languageOption]))
			{
				return "." + InGameManager.languageVariants[GameSaveData.instance.languageOption];
			}
			return ".en-sd";
		}
		return string.Empty;
	}

	public unsafe string GetFullBundleName(string bundleName)
	{
		string[] split = bundleName.Split('.');
		_003CGetFullBundleName_003Ec__AnonStorey4AE _003CGetFullBundleName_003Ec__AnonStorey4AE;
		if (!InGameManager.languageVariants.Any(new Func<string, bool>((object)_003CGetFullBundleName_003Ec__AnonStorey4AE, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)))
		{
			bundleName += GetVariantName(GetCategory(bundleName));
		}
		return bundleName;
	}

	public unsafe RESOURCE_CATEGORY? GetCategory(string fullBundleName)
	{
		fullBundleName = fullBundleName.ToUpper();
		string[] names = Enum.GetNames(typeof(RESOURCE_CATEGORY));
		_003CGetCategory_003Ec__AnonStorey4AF _003CGetCategory_003Ec__AnonStorey4AF;
		string value = names.FirstOrDefault(new Func<string, bool>((object)_003CGetCategory_003Ec__AnonStorey4AF, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		if (string.IsNullOrEmpty(value))
		{
			return null;
		}
		return (RESOURCE_CATEGORY)(int)Enum.Parse(typeof(RESOURCE_CATEGORY), value);
	}

	public unsafe string GetBundleNameWithoutVariant(string fullBundleName)
	{
		string[] splits = fullBundleName.Split('.');
		_003CGetBundleNameWithoutVariant_003Ec__AnonStorey4B0 _003CGetBundleNameWithoutVariant_003Ec__AnonStorey4B;
		if (InGameManager.languageVariants.Any(new Func<string, bool>((object)_003CGetBundleNameWithoutVariant_003Ec__AnonStorey4B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)))
		{
			return fullBundleName.Remove(fullBundleName.LastIndexOf("."));
		}
		return fullBundleName;
	}
}
