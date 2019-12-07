using Network;
using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSettingsManager : MonoBehaviourSingleton<SceneSettingsManager>
{
	[Serializable]
	public class InsideColliderData
	{
		public int minX;

		public int maxX;

		public int minZ;

		public int maxZ;

		public float chipSize = 1f;

		public List<int> insideFlags = new List<int>();
	}

	[Serializable]
	public class GradeToValue
	{
		public int threshold;

		public int value;
	}

	[Serializable]
	public class HomeInfoCountData
	{
		public int eventId;

		public int eventType;

		public GradeToValue[] gradeToValue;

		public Animator animator;
	}

	private const string OBJECTS_ROOT_NAME = "Objects";

	private const string BINGO_BOARD_OBJECT_NAME = "bingoboard";

	private const string BINGO_BLOCK_OBJECT_NAME = "block";

	private const string COLIDER_ROOT_NAME = "Collider";

	[Tooltip("フォグカラ\u30fc")]
	public Color fogColor = Color.white;

	[Tooltip("フォグ開始距離。フォグモ\u30fcドLinearでのみ使用")]
	public float linearFogStart;

	[Tooltip("フォグ終了距離。フォグモ\u30fcドLinearでのみ使用")]
	public float linearFogEnd = 300f;

	[Tooltip("リムカラ\u30fc")]
	public Color rimColor = new Color(1f, 1f, 1f, 1f);

	[Tooltip("アンビエントカラ\u30fc")]
	public Color ambientColor = new Color(0.2f, 0.2f, 0.2f);

	[Tooltip("エフェクト用カラ\u30fc")]
	public Color effectColor = new Color(0.2f, 0.2f, 0.2f);

	[Tooltip("エディタ用アンビエントカラ\u30fc有効フラグ")]
	public bool enableEditorAmbientColor;

	[Tooltip("エディタ用アンビエントカラ\u30fc")]
	public Color editorAmbientColor = new Color(0.2f, 0.2f, 0.2f);

	[Tooltip("属性ID（このIDによってエフェクトが変化する、など")]
	public int attributeID = 1;

	[Tooltip("フォグの開始距離での限界濃度")]
	public float limitFogStart;

	[Tooltip("フォグの終了距離での限界濃度")]
	public float limitFogEnd = 1f;

	[Tooltip("ライトプロ\u30fcブカラ\u30fc乗算")]
	public Color lightProbeMul = new Color(1f, 1f, 1f, 1f);

	[Tooltip("ライトプロ\u30fcブカラ\u30fc加算")]
	public Color lightProbeAdd = new Color(0f, 0f, 0f, 0f);

	[Tooltip("ライトプロ\u30fcブカラ\u30fcクランプ値")]
	public float lightProbePeak = 2f;

	[Tooltip("NPC用アンビエントカラ\u30fc")]
	public Color npcAmbientColor = new Color(1f, 1f, 1f, 0f);

	[Tooltip("NPC用ライト方向")]
	public Vector3 npcLightDir = new Vector3(0f, 0f, 0f);

	[Tooltip("フォグ強制ON")]
	public bool forceFogON;

	public UnityEngine.Object[] linkResources;

	[Tooltip("マップの内外情報保存フラグ")]
	public bool saveInsideCollider = true;

	[Tooltip("マップの内外情報")]
	public InsideColliderData insideColliderData = new InsideColliderData();

	[Tooltip("天候変化の情報")]
	public WeatherController weatherController = new WeatherController();

	[Tooltip("防衛戦対象")]
	public GameObject[] waveTargets;

	private List<GameObject> addWaveTargetList = new List<GameObject>();

	[Tooltip("InsideCheckから外す")]
	public GameObject[] ignoreInsideCheckObjects;

	public HomeInfoCountData[] homeInfoCountData;

	[Tooltip("条件の進行度によってアクティブになるオブジェクト")]
	public GameObject[] objectsByProgress;

	private GameObject m_bingoBoardObject;

	private GameObject m_bingoBlockObject;

	public bool WeatherForceReturn;

	private Vector3 gObjContainCollidersScale;

	private GameObject gObjContainColliders;

	private string colliderNameToCompare = "Colliders";

	public GameObject GObjContainColliders
	{
		get
		{
			return gObjContainColliders;
		}
		set
		{
			gObjContainColliders = value;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		ApplyScene(isEditorAmbientColor: false);
		ParseObjectName();
		addWaveTargetList.Clear();
		gObjContainCollidersScale = base.transform.localScale;
		if (GObjContainColliders == null)
		{
			GObjContainColliders = OnGetGObjContainColliders();
		}
	}

	private void ParseObjectName()
	{
		foreach (Transform item in base.transform)
		{
			if (item.name.Contains("Objects"))
			{
				foreach (Transform item2 in item)
				{
					if (item2.name.Contains("bingoboard"))
					{
						m_bingoBoardObject = item2.gameObject;
					}
					if (item2.name.Contains("block"))
					{
						m_bingoBlockObject = item2.gameObject;
					}
				}
			}
		}
	}

	private void Start()
	{
		ApplyStageMaterial();
		ApplyRaiseCollider();
	}

	public void InitializeScene()
	{
		ApplyScene(isEditorAmbientColor: false);
		ParseObjectName();
		addWaveTargetList.Clear();
		ApplyStageMaterial();
		if (GObjContainColliders == null)
		{
			GObjContainColliders = OnGetGObjContainColliders();
		}
		OnResizeGObjContainColliders(gObjContainCollidersScale);
	}

	public void ApplyScene(bool isEditorAmbientColor)
	{
		if (Application.isPlaying)
		{
			isEditorAmbientColor = false;
		}
		RenderSettings.fog = false;
		if (!isEditorAmbientColor)
		{
			RenderSettings.ambientLight = ambientColor;
		}
		else
		{
			RenderSettings.ambientLight = editorAmbientColor;
		}
		weatherController.Init();
	}

	public void ApplyStageMaterial()
	{
		ShaderGlobal.fogColor = fogColor;
		ShaderGlobal.fogNear = linearFogStart;
		ShaderGlobal.fogFar = linearFogEnd;
		ShaderGlobal.fogNearLimit = limitFogStart;
		ShaderGlobal.fogFarLimit = limitFogEnd;
		ShaderGlobal.globalRimColor = rimColor;
		ShaderGlobal.lightProbeMul = lightProbeMul;
		ShaderGlobal.lightProbeAdd = lightProbeAdd;
		ShaderGlobal.lightProbePeak = lightProbePeak;
		ShaderGlobal.npcAmbientColor = npcAmbientColor;
		if (MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
		{
			MonoBehaviourSingleton<GlobalSettingsManager>.I.npcLightDirection.localEulerAngles = npcLightDir;
		}
	}

	public static void ApplyEffect(rymFX fx, bool force)
	{
		if (!(fx == null) && (force || fx.BaseColor.a == 0f))
		{
			Color color = fx.BaseColor = ((!MonoBehaviourSingleton<SceneSettingsManager>.IsValid()) ? Color.white : MonoBehaviourSingleton<SceneSettingsManager>.I.effectColor);
		}
	}

	public void ApplyRaiseCollider()
	{
		if (!MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
		{
			return;
		}
		GlobalSettingsManager.InGameFieldSetting inGameFieldSetting = MonoBehaviourSingleton<GlobalSettingsManager>.I.inGameFieldSetting;
		if (!inGameFieldSetting.isRaiseWallCollider || !MonoBehaviourSingleton<GameSceneManager>.IsValid() || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "InGameScene")
		{
			return;
		}
		Transform transform = null;
		transform = ((!QuestManager.IsValidInGameWaveMatch()) ? Utility.FindChild(base.transform, "Colliders") : base._transform);
		if (transform != null)
		{
			BoxCollider[] componentsInChildren = transform.GetComponentsInChildren<BoxCollider>(includeInactive: true);
			if (!componentsInChildren.IsNullOrEmpty())
			{
				int i = 0;
				for (int num = componentsInChildren.Length; i < num; i++)
				{
					BoxCollider boxCollider = componentsInChildren[i];
					if (!(boxCollider.size.y > inGameFieldSetting.raiseWallColliderSizeY))
					{
						boxCollider.size = new Vector3(boxCollider.size.x, inGameFieldSetting.raiseWallColliderSizeY, boxCollider.size.z);
						boxCollider.center = new Vector3(boxCollider.center.x, inGameFieldSetting.raiseWallColliderOffsetY, boxCollider.center.z);
					}
				}
			}
		}
		MeshCollider[] componentsInChildren2 = GetComponentsInChildren<MeshCollider>(includeInactive: true);
		if (componentsInChildren2.IsNullOrEmpty())
		{
			return;
		}
		int j = 0;
		for (int num2 = componentsInChildren2.Length; j < num2; j++)
		{
			Transform transform2 = componentsInChildren2[j].transform;
			if (!(transform2.localScale.y > inGameFieldSetting.raiseWallColliderScaleY))
			{
				transform2.localScale = new Vector3(transform2.localScale.x, inGameFieldSetting.raiseWallColliderScaleY, transform2.localScale.z);
			}
		}
	}

	public static T GetLinkResource<T>(string name) where T : UnityEngine.Object
	{
		if (!MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
		{
			return null;
		}
		UnityEngine.Object[] array = MonoBehaviourSingleton<SceneSettingsManager>.I.linkResources;
		if (array == null)
		{
			return null;
		}
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			if (array[i] != null && array[i].name == name)
			{
				return array[i] as T;
			}
		}
		return null;
	}

	public void ChangeWeather(float changingTime, float duration)
	{
		StartCoroutine(DoChangeWeather(changingTime, duration));
	}

	private IEnumerator DoChangeWeather(float changingTime, float duration)
	{
		float timer3 = 0f;
		WeatherForceReturn = false;
		weatherController.OnStartWeatherChange();
		while (timer3 < changingTime)
		{
			timer3 += Time.deltaTime;
			weatherController.Update(timer3 / changingTime);
			yield return null;
		}
		weatherController.OnFinishedWeatherChange();
		timer3 = 0f;
		while (timer3 < duration && !WeatherForceReturn)
		{
			timer3 += Time.deltaTime;
			yield return null;
		}
		timer3 = 0f;
		weatherController.OnStartReturnToOriginal();
		while (timer3 < changingTime)
		{
			timer3 += Time.deltaTime;
			weatherController.Update(1f - timer3 / changingTime);
			yield return null;
		}
		weatherController.OnFinishedReturnToOriginal();
	}

	public GameObject GetWaveTarget(string name)
	{
		if (waveTargets == null || waveTargets.Length == 0)
		{
			return null;
		}
		for (int i = 0; i < waveTargets.Length; i++)
		{
			GameObject gameObject = waveTargets[i];
			if (gameObject != null && gameObject.name == name)
			{
				return gameObject;
			}
		}
		return null;
	}

	public void AddWaveTarget(GameObject target)
	{
		if (!(target == null))
		{
			addWaveTargetList.Add(target);
		}
	}

	public void DisableWaveTarget()
	{
		if (!addWaveTargetList.IsNullOrEmpty())
		{
			int i = 0;
			for (int count = addWaveTargetList.Count; i < count; i++)
			{
				GameObject gameObject = addWaveTargetList[i];
				if (gameObject != null)
				{
					gameObject.SetActive(value: false);
				}
			}
		}
		if (waveTargets == null || waveTargets.Length == 0)
		{
			return;
		}
		int j = 0;
		for (int num = waveTargets.Length; j < num; j++)
		{
			GameObject gameObject2 = waveTargets[j];
			if (gameObject2 != null)
			{
				gameObject2.SetActive(value: false);
			}
		}
	}

	public void SetEventItemCount(List<EventItemCounts> list)
	{
		if (list == null || list.Count <= 0 || this.homeInfoCountData == null || this.homeInfoCountData.Length == 0)
		{
			return;
		}
		int i = 0;
		for (int count = list.Count; i < count; i++)
		{
			EventItemCounts eventItemCounts = list[i];
			int j = 0;
			for (int num = this.homeInfoCountData.Length; j < num; j++)
			{
				HomeInfoCountData homeInfoCountData = this.homeInfoCountData[j];
				if (eventItemCounts.eventId == homeInfoCountData.eventId && eventItemCounts.eventType == homeInfoCountData.eventType)
				{
					_ExecEventItemCount(eventItemCounts, homeInfoCountData);
					break;
				}
			}
		}
	}

	private void _ExecEventItemCount(EventItemCounts eic, HomeInfoCountData hicd)
	{
		int value = eic.rewardGrade;
		if (hicd.gradeToValue != null)
		{
			for (int num = hicd.gradeToValue.Length - 1; num >= 0; num--)
			{
				if (eic.rewardGrade >= hicd.gradeToValue[num].threshold)
				{
					value = hicd.gradeToValue[num].value;
					break;
				}
			}
		}
		if (hicd.animator != null)
		{
			hicd.animator.SetInteger("Value", value);
		}
	}

	public void SwitchBingoObjectsActivation(bool _isActive)
	{
		if (m_bingoBoardObject != null && m_bingoBoardObject.activeSelf != _isActive)
		{
			m_bingoBoardObject.SetActive(_isActive);
		}
		if (m_bingoBlockObject != null && m_bingoBlockObject.activeSelf != _isActive)
		{
			m_bingoBlockObject.SetActive(_isActive);
		}
	}

	public void ActivateObjectsByProgress(int progress)
	{
		if (!objectsByProgress.IsNullOrEmpty() && objectsByProgress.Length > progress)
		{
			objectsByProgress[progress].SetActive(value: true);
		}
	}

	public void Self()
	{
		SelfInstance();
	}

	public void Remove()
	{
		RemoveInstance();
	}

	private GameObject OnGetGObjContainColliders()
	{
		if (base.transform.childCount <= 0)
		{
			return null;
		}
		Transform[] componentsInChildren = base.gameObject.GetComponentsInChildren<Transform>(includeInactive: true);
		if (componentsInChildren == null || componentsInChildren.Length == 0)
		{
			return null;
		}
		Transform[] array = componentsInChildren;
		foreach (Transform transform in array)
		{
			if (!(transform == null) && transform.gameObject.name.Equals(colliderNameToCompare) && transform.childCount > 0)
			{
				Collider[] componentsInChildren2 = transform.GetComponentsInChildren<Collider>(includeInactive: true);
				if (componentsInChildren2 != null && componentsInChildren2.Length != 0)
				{
					return transform.gameObject;
				}
			}
		}
		return null;
	}

	public void OnResizeGObjContainColliders(Vector3 size)
	{
		if (GObjContainColliders == null)
		{
			GObjContainColliders = OnGetGObjContainColliders();
		}
		if (GObjContainColliders == null || size == Vector3.zero)
		{
			return;
		}
		if (MonoBehaviourSingleton<GoGameSettingsManager>.IsValid())
		{
			_ = MonoBehaviourSingleton<GoGameSettingsManager>.I.colliderOfMapScale;
			if (MonoBehaviourSingleton<GoGameSettingsManager>.I.colliderOfMapScale == GObjContainColliders.transform.localScale)
			{
				return;
			}
		}
		Vector3 localScale = GObjContainColliders.transform.localScale;
		size.x = localScale.x;
		size.z = localScale.z;
		GObjContainColliders.transform.localScale = size;
	}
}
