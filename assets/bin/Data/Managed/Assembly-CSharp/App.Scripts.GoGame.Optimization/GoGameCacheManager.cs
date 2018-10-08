using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App.Scripts.GoGame.Optimization
{
	public class GoGameCacheManager : MonoBehaviourSingleton<GoGameCacheManager>
	{
		public class PlayerModelData
		{
			public string faceName;

			public string hairName;

			public string bodyName;

			public string headName;

			public string armName;

			public string legName;

			public string wepnName;

			public HashSet<string> accUIDs;
		}

		private Transform CacheContainer;

		private PlayerModelData _selfPlayerModelData;

		private readonly Dictionary<RESOURCE_CATEGORY, Dictionary<string, Object>> _selfPlayerResourceCache = new Dictionary<RESOURCE_CATEGORY, Dictionary<string, Object>>();

		private static Dictionary<string, Transform> objCaches = new Dictionary<string, Transform>();

		private static Dictionary<string, SceneParameter> lightMapCaches = new Dictionary<string, SceneParameter>();

		private static Texture2D texture2D = null;

		protected override void Awake()
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			base.Awake();
			if (CacheContainer != null)
			{
				Object.DestroyImmediate(CacheContainer.get_gameObject());
			}
			CacheContainer = new GameObject("CacheContainer").get_transform();
			CacheContainer.set_parent(base._transform);
		}

		protected override void OnDestroySingleton()
		{
			base.OnDestroySingleton();
			Delete();
		}

		public void Delete()
		{
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			foreach (KeyValuePair<string, Transform> objCach in objCaches)
			{
				if (objCach.Value != null)
				{
					Object.DestroyImmediate(objCach.Value.get_gameObject());
				}
			}
			objCaches.Clear();
		}

		public void CacheSelfPlayerResource(RESOURCE_CATEGORY resourceCategory, string resourceName, Object loadedObject)
		{
			Dictionary<string, Object> dictionary;
			if (!_selfPlayerResourceCache.ContainsKey(resourceCategory))
			{
				dictionary = new Dictionary<string, Object>();
				_selfPlayerResourceCache.Add(resourceCategory, dictionary);
			}
			else
			{
				dictionary = _selfPlayerResourceCache[resourceCategory];
			}
			if (!dictionary.ContainsKey(resourceName))
			{
				dictionary.Add(resourceName, loadedObject);
			}
			else
			{
				Object val = dictionary[resourceName];
				dictionary[resourceName] = loadedObject;
				Object.Destroy(val);
			}
		}

		public bool IsSelfPlayerResourceCached(RESOURCE_CATEGORY resourceCategory, string resourceName)
		{
			if (resourceName == null)
			{
				return false;
			}
			return _selfPlayerResourceCache.ContainsKey(resourceCategory) && _selfPlayerResourceCache[resourceCategory].ContainsKey(resourceName);
		}

		public unsafe bool IsSelfPlayerResourceCached(RESOURCE_CATEGORY resourceCategory, string packageName, string[] resourceNames)
		{
			_003CIsSelfPlayerResourceCached_003Ec__AnonStorey4C3 _003CIsSelfPlayerResourceCached_003Ec__AnonStorey4C;
			if (packageName != null)
			{
				return resourceNames.Aggregate(true, new Func<bool, string, bool>((object)_003CIsSelfPlayerResourceCached_003Ec__AnonStorey4C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			return resourceNames.Aggregate(true, new Func<bool, string, bool>((object)_003CIsSelfPlayerResourceCached_003Ec__AnonStorey4C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}

		public Object GetSelfPlayerResourceCache(RESOURCE_CATEGORY resourceCategory, string resourceName)
		{
			return (!IsSelfPlayerResourceCached(resourceCategory, resourceName)) ? null : _selfPlayerResourceCache[resourceCategory][resourceName];
		}

		public static bool ShouldCacheStage(string id)
		{
			if (MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
			{
				return MonoBehaviourSingleton<GlobalSettingsManager>.I.stageCache.Contains(id);
			}
			return false;
		}

		public static bool ShouldCacheSky(string id)
		{
			if (MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
			{
				return MonoBehaviourSingleton<GlobalSettingsManager>.I.skyboxCaches.Contains(id);
			}
			return false;
		}

		public static bool ShouldCacheEffect(string id)
		{
			if (MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
			{
				return MonoBehaviourSingleton<GlobalSettingsManager>.I.stageEffectCaches.Contains(id);
			}
			return false;
		}

		public static bool HasCacheObj(string id)
		{
			return objCaches.ContainsKey(id) && objCaches[id] != null;
		}

		public static void CacheObj(string id, Transform obj)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			obj.get_gameObject().SetActive(false);
			obj.set_parent(MonoBehaviourSingleton<GoGameCacheManager>.I.CacheContainer);
			if (objCaches.ContainsKey(id))
			{
				objCaches[id] = obj;
			}
			else
			{
				objCaches.Add(id, obj);
			}
		}

		public static bool CacheEffectIfNeed(string id, Transform obj)
		{
			if (ShouldCacheEffect(id))
			{
				CacheObj(id, obj);
				return true;
			}
			return false;
		}

		public static Transform RetrieveObj(string id, Transform parent = null)
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			if (objCaches.ContainsKey(id))
			{
				Transform val = objCaches[id];
				val.get_gameObject().SetActive(true);
				if (parent != null)
				{
					val.set_parent(parent);
				}
				objCaches.Remove(id);
				return val;
			}
			return null;
		}

		public static void CacheLightMap(string id, Transform obj)
		{
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Expected O, but got Unknown
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Expected O, but got Unknown
			//IL_0088: Unknown result type (might be due to invalid IL or missing references)
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0093: Unknown result type (might be due to invalid IL or missing references)
			//IL_0098: Expected O, but got Unknown
			SceneParameter sceneParameter = obj.GetComponent<SceneParameter>();
			if (sceneParameter == null)
			{
				sceneParameter = obj.get_gameObject().AddComponent<SceneParameter>();
			}
			LightmapData[] lightmaps = LightmapSettings.get_lightmaps();
			if (lightmaps != null && lightmaps.Length > 0)
			{
				int num = lightmaps.Length;
				Texture2D[] array = (Texture2D[])new Texture2D[num];
				Texture2D[] array2 = (Texture2D[])new Texture2D[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = lightmaps[i].get_lightmapFar();
					array2[i] = lightmaps[i].get_lightmapNear();
				}
				sceneParameter.lightmapsFar = array;
				sceneParameter.lightmapsNear = array2;
				sceneParameter.lightmapMode = LightmapSettings.get_lightmapsMode();
			}
			sceneParameter.lightProbes = LightmapSettings.get_lightProbes();
			if (lightMapCaches.ContainsKey(id))
			{
				lightMapCaches[id] = sceneParameter;
			}
			else
			{
				lightMapCaches.Add(id, sceneParameter);
			}
		}

		public static SceneParameter RetrieveLightMap(string id)
		{
			if (lightMapCaches.ContainsKey(id))
			{
				return lightMapCaches[id];
			}
			return null;
		}
	}
}
