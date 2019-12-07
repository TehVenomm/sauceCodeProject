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

		public class PlayerModelTransform
		{
			public Transform wepR;

			public Transform wepL;

			public Transform face;

			public Transform hair;

			public Transform body;

			public Transform head;

			public Transform arm;

			public Transform leg;
		}

		private Transform CacheContainer;

		private PlayerModelData _selfPlayerModelData;

		private PlayerModelTransform _selfPlayerModelTransform;

		private readonly Dictionary<RESOURCE_CATEGORY, Dictionary<string, Object>> _selfPlayerResourceCache = new Dictionary<RESOURCE_CATEGORY, Dictionary<string, Object>>();

		private static Dictionary<string, Transform> objCaches = new Dictionary<string, Transform>();

		private static Dictionary<string, SceneParameter> lightMapCaches = new Dictionary<string, SceneParameter>();

		private static Texture2D texture2D = null;

		protected override void Awake()
		{
			base.Awake();
			if (CacheContainer != null)
			{
				Object.DestroyImmediate(CacheContainer.gameObject);
			}
			CacheContainer = new GameObject("CacheContainer").transform;
			CacheContainer.parent = base._transform;
		}

		protected override void OnDestroySingleton()
		{
			base.OnDestroySingleton();
			Delete();
		}

		public void Delete()
		{
			foreach (KeyValuePair<string, Transform> objCach in objCaches)
			{
				if (objCach.Value != null)
				{
					Object.DestroyImmediate(objCach.Value.gameObject);
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
				return;
			}
			Object obj = dictionary[resourceName];
			dictionary[resourceName] = loadedObject;
			Object.Destroy(obj);
		}

		public bool IsSelfPlayerResourceCached(RESOURCE_CATEGORY resourceCategory, string resourceName)
		{
			if (resourceName == null)
			{
				return false;
			}
			if (_selfPlayerResourceCache.ContainsKey(resourceCategory))
			{
				return _selfPlayerResourceCache[resourceCategory].ContainsKey(resourceName);
			}
			return false;
		}

		public bool IsSelfPlayerResourceCached(RESOURCE_CATEGORY resourceCategory, string packageName, string[] resourceNames)
		{
			if (packageName != null)
			{
				return resourceNames.Aggregate(seed: true, (bool current, string resourceName) => current | (_selfPlayerResourceCache.ContainsKey(resourceCategory) && _selfPlayerResourceCache[resourceCategory].ContainsKey($"{packageName}/{resourceName}")));
			}
			return resourceNames.Aggregate(seed: true, (bool current, string resourceName) => current | (_selfPlayerResourceCache.ContainsKey(resourceCategory) && _selfPlayerResourceCache[resourceCategory].ContainsKey(resourceName)));
		}

		public Object GetSelfPlayerResourceCache(RESOURCE_CATEGORY resourceCategory, string resourceName)
		{
			if (!IsSelfPlayerResourceCached(resourceCategory, resourceName))
			{
				return null;
			}
			return _selfPlayerResourceCache[resourceCategory][resourceName];
		}

		public void CacheSelfPlayerModel()
		{
			if (_selfPlayerModelData != null)
			{
				if (!string.IsNullOrEmpty(_selfPlayerModelData.wepnName))
				{
					CacheObj(_selfPlayerModelData.wepnName + "R", _selfPlayerModelTransform.wepR);
					CacheObj(_selfPlayerModelData.wepnName + "L", _selfPlayerModelTransform.wepL);
				}
				if (!string.IsNullOrEmpty(_selfPlayerModelData.faceName))
				{
					CacheObj(_selfPlayerModelData.faceName, _selfPlayerModelTransform.face);
				}
				if (!string.IsNullOrEmpty(_selfPlayerModelData.hairName))
				{
					CacheObj(_selfPlayerModelData.hairName, _selfPlayerModelTransform.hair);
				}
				if (!string.IsNullOrEmpty(_selfPlayerModelData.bodyName))
				{
					CacheObj(_selfPlayerModelData.bodyName, _selfPlayerModelTransform.body);
				}
				if (!string.IsNullOrEmpty(_selfPlayerModelData.headName))
				{
					CacheObj(_selfPlayerModelData.headName, _selfPlayerModelTransform.head);
				}
				if (!string.IsNullOrEmpty(_selfPlayerModelData.armName))
				{
					CacheObj(_selfPlayerModelData.armName, _selfPlayerModelTransform.arm);
				}
				if (!string.IsNullOrEmpty(_selfPlayerModelData.legName))
				{
					CacheObj(_selfPlayerModelData.legName, _selfPlayerModelTransform.leg);
				}
			}
		}

		private void SafeDestroy(Object obj)
		{
			if (obj != null)
			{
				Object.Destroy(obj);
			}
		}

		private void SafeDestroy(Transform transform)
		{
			if (transform != null)
			{
				Object.Destroy(transform.gameObject);
			}
		}

		public void ClearCacheSelfPlayerModelNotUse(PlayerModelData newData)
		{
			if (_selfPlayerModelData != null)
			{
				if (!string.IsNullOrEmpty(_selfPlayerModelData.wepnName) && _selfPlayerModelData.wepnName != newData.wepnName)
				{
					SafeDestroy(RetrieveObj(_selfPlayerModelData.wepnName + "R"));
					SafeDestroy(RetrieveObj(_selfPlayerModelData.wepnName + "L"));
				}
				if (!string.IsNullOrEmpty(_selfPlayerModelData.faceName) && _selfPlayerModelData.faceName != newData.faceName)
				{
					SafeDestroy(RetrieveObj(_selfPlayerModelData.faceName));
				}
				if (!string.IsNullOrEmpty(_selfPlayerModelData.hairName) && _selfPlayerModelData.hairName != newData.hairName)
				{
					SafeDestroy(RetrieveObj(_selfPlayerModelData.hairName));
				}
				if (!string.IsNullOrEmpty(_selfPlayerModelData.bodyName) && _selfPlayerModelData.bodyName != newData.bodyName)
				{
					SafeDestroy(RetrieveObj(_selfPlayerModelData.bodyName));
				}
				if (!string.IsNullOrEmpty(_selfPlayerModelData.headName) && _selfPlayerModelData.headName != newData.headName)
				{
					SafeDestroy(RetrieveObj(_selfPlayerModelData.headName));
				}
				if (!string.IsNullOrEmpty(_selfPlayerModelData.armName) && _selfPlayerModelData.armName != newData.armName)
				{
					SafeDestroy(RetrieveObj(_selfPlayerModelData.armName));
				}
				if (!string.IsNullOrEmpty(_selfPlayerModelData.legName) && _selfPlayerModelData.legName != newData.legName)
				{
					SafeDestroy(RetrieveObj(_selfPlayerModelData.legName));
				}
			}
		}

		public void SaveCacheSelfPlayerModel(Self self)
		{
			_selfPlayerModelData = new PlayerModelData
			{
				faceName = self.loader.faceCacheName,
				hairName = self.loader.hairCacheName,
				bodyName = self.loader.bodyCacheName,
				headName = self.loader.headCacheName,
				armName = self.loader.armCacheName,
				legName = self.loader.legCacheName,
				wepnName = self.loader.weaponCacheName
			};
			_selfPlayerModelTransform = new PlayerModelTransform
			{
				wepR = self.loader.wepR,
				wepL = self.loader.wepL,
				face = self.loader.face,
				hair = self.loader.hair,
				body = self.loader.body,
				head = self.loader.head,
				arm = self.loader.arm,
				leg = self.loader.leg
			};
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
			if (string.IsNullOrEmpty(id))
			{
				return false;
			}
			if (objCaches.ContainsKey(id))
			{
				return objCaches[id] != null;
			}
			return false;
		}

		public static void CacheObj(string id, Transform obj)
		{
			if (!(obj == null))
			{
				obj.gameObject.SetActive(value: false);
				obj.parent = MonoBehaviourSingleton<GoGameCacheManager>.I.CacheContainer;
				if (objCaches.ContainsKey(id))
				{
					objCaches[id] = obj;
				}
				else
				{
					objCaches.Add(id, obj);
				}
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
			if (objCaches.ContainsKey(id))
			{
				Transform transform = objCaches[id];
				transform.gameObject.SetActive(value: true);
				if (parent != null)
				{
					transform.parent = parent;
				}
				objCaches.Remove(id);
				return transform;
			}
			return null;
		}

		public static void CacheLightMap(string id, Transform obj)
		{
			SceneParameter sceneParameter = obj.GetComponent<SceneParameter>();
			if (sceneParameter == null)
			{
				sceneParameter = obj.gameObject.AddComponent<SceneParameter>();
			}
			LightmapData[] lightmaps = LightmapSettings.lightmaps;
			if (lightmaps != null && lightmaps.Length != 0)
			{
				int num = lightmaps.Length;
				Texture2D[] array = new Texture2D[num];
				Texture2D[] array2 = new Texture2D[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = lightmaps[i].lightmapColor;
					array2[i] = lightmaps[i].lightmapDir;
				}
				sceneParameter.lightmapsFar = array;
				sceneParameter.lightmapsNear = array2;
				sceneParameter.lightmapMode = LightmapSettings.lightmapsMode;
			}
			sceneParameter.lightProbes = LightmapSettings.lightProbes;
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
