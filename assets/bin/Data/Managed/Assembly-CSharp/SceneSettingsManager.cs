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
	public Color fogColor = Color.get_white();

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

	public Object[] linkResources;

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
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		base.Awake();
		ApplyScene(isEditorAmbientColor: false);
		ParseObjectName();
		addWaveTargetList.Clear();
		gObjContainCollidersScale = this.get_transform().get_localScale();
		if (GObjContainColliders == null)
		{
			GObjContainColliders = OnGetGObjContainColliders();
		}
	}

	private void ParseObjectName()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Expected O, but got Unknown
		IEnumerator enumerator = this.get_transform().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform val = enumerator.Current;
				if (val.get_name().Contains("Objects"))
				{
					IEnumerator enumerator2 = val.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							Transform val2 = enumerator2.Current;
							if (val2.get_name().Contains("bingoboard"))
							{
								m_bingoBoardObject = val2.get_gameObject();
							}
							if (val2.get_name().Contains("block"))
							{
								m_bingoBlockObject = val2.get_gameObject();
							}
						}
					}
					finally
					{
						IDisposable disposable;
						if ((disposable = (enumerator2 as IDisposable)) != null)
						{
							disposable.Dispose();
						}
					}
				}
			}
		}
		finally
		{
			IDisposable disposable2;
			if ((disposable2 = (enumerator as IDisposable)) != null)
			{
				disposable2.Dispose();
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
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		if (Application.get_isPlaying())
		{
			isEditorAmbientColor = false;
		}
		RenderSettings.set_fog(false);
		if (!isEditorAmbientColor)
		{
			RenderSettings.set_ambientLight(ambientColor);
		}
		else
		{
			RenderSettings.set_ambientLight(editorAmbientColor);
		}
		weatherController.Init();
	}

	public void ApplyStageMaterial()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
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
			MonoBehaviourSingleton<GlobalSettingsManager>.I.npcLightDirection.set_localEulerAngles(npcLightDir);
		}
	}

	public static void ApplyEffect(rymFX fx, bool force)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (!(fx == null) && (force || fx.BaseColor.a == 0f))
		{
			Color val = fx.BaseColor = ((!MonoBehaviourSingleton<SceneSettingsManager>.IsValid()) ? Color.get_white() : MonoBehaviourSingleton<SceneSettingsManager>.I.effectColor);
		}
	}

	public void ApplyRaiseCollider()
	{
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		if (!MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
		{
			return;
		}
		GlobalSettingsManager.InGameFieldSetting inGameFieldSetting = MonoBehaviourSingleton<GlobalSettingsManager>.I.inGameFieldSetting;
		if (!inGameFieldSetting.isRaiseWallCollider || !MonoBehaviourSingleton<GameSceneManager>.IsValid() || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "InGameScene")
		{
			return;
		}
		Transform val = null;
		val = ((!QuestManager.IsValidInGameWaveMatch()) ? Utility.FindChild(this.get_transform(), "Colliders") : base._transform);
		if (val != null)
		{
			BoxCollider[] componentsInChildren = val.GetComponentsInChildren<BoxCollider>(true);
			if (!componentsInChildren.IsNullOrEmpty())
			{
				int i = 0;
				for (int num = componentsInChildren.Length; i < num; i++)
				{
					BoxCollider val2 = componentsInChildren[i];
					Vector3 size = val2.get_size();
					if (!(size.y > inGameFieldSetting.raiseWallColliderSizeY))
					{
						BoxCollider obj = val2;
						Vector3 size2 = val2.get_size();
						float x = size2.x;
						float raiseWallColliderSizeY = inGameFieldSetting.raiseWallColliderSizeY;
						Vector3 size3 = val2.get_size();
						obj.set_size(new Vector3(x, raiseWallColliderSizeY, size3.z));
						BoxCollider obj2 = val2;
						Vector3 center = val2.get_center();
						float x2 = center.x;
						float raiseWallColliderOffsetY = inGameFieldSetting.raiseWallColliderOffsetY;
						Vector3 center2 = val2.get_center();
						obj2.set_center(new Vector3(x2, raiseWallColliderOffsetY, center2.z));
					}
				}
			}
		}
		MeshCollider[] componentsInChildren2 = this.GetComponentsInChildren<MeshCollider>(true);
		if (componentsInChildren2.IsNullOrEmpty())
		{
			return;
		}
		int j = 0;
		for (int num2 = componentsInChildren2.Length; j < num2; j++)
		{
			Transform transform = componentsInChildren2[j].get_transform();
			Vector3 localScale = transform.get_localScale();
			if (!(localScale.y > inGameFieldSetting.raiseWallColliderScaleY))
			{
				Transform obj3 = transform;
				Vector3 localScale2 = transform.get_localScale();
				float x3 = localScale2.x;
				float raiseWallColliderScaleY = inGameFieldSetting.raiseWallColliderScaleY;
				Vector3 localScale3 = transform.get_localScale();
				obj3.set_localScale(new Vector3(x3, raiseWallColliderScaleY, localScale3.z));
			}
		}
	}

	public static T GetLinkResource<T>(string name) where T : Object
	{
		if (!MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
		{
			return (T)(object)null;
		}
		Object[] array = MonoBehaviourSingleton<SceneSettingsManager>.I.linkResources;
		if (array == null)
		{
			return (T)(object)null;
		}
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			if (array[i] != null && array[i].get_name() == name)
			{
				return array[i] as T;
			}
		}
		return (T)(object)null;
	}

	public void ChangeWeather(float changingTime, float duration)
	{
		this.StartCoroutine(DoChangeWeather(changingTime, duration));
	}

	private IEnumerator DoChangeWeather(float changingTime, float duration)
	{
		float timer3 = 0f;
		WeatherForceReturn = false;
		weatherController.OnStartWeatherChange();
		while (timer3 < changingTime)
		{
			timer3 += Time.get_deltaTime();
			weatherController.Update(timer3 / changingTime);
			yield return null;
		}
		weatherController.OnFinishedWeatherChange();
		timer3 = 0f;
		while (timer3 < duration && !WeatherForceReturn)
		{
			timer3 += Time.get_deltaTime();
			yield return null;
		}
		timer3 = 0f;
		weatherController.OnStartReturnToOriginal();
		while (timer3 < changingTime)
		{
			timer3 += Time.get_deltaTime();
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
			GameObject val = waveTargets[i];
			if (val != null && val.get_name() == name)
			{
				return val;
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
				GameObject val = addWaveTargetList[i];
				if (val != null)
				{
					val.SetActive(false);
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
			GameObject val2 = waveTargets[j];
			if (val2 != null)
			{
				val2.SetActive(false);
			}
		}
	}

	public void SetEventItemCount(List<EventItemCounts> list)
	{
		if (list == null || list.Count <= 0 || this.homeInfoCountData == null || this.homeInfoCountData.Length <= 0)
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
		int num = eic.rewardGrade;
		if (hicd.gradeToValue != null)
		{
			for (int num2 = hicd.gradeToValue.Length - 1; num2 >= 0; num2--)
			{
				if (eic.rewardGrade >= hicd.gradeToValue[num2].threshold)
				{
					num = hicd.gradeToValue[num2].value;
					break;
				}
			}
		}
		if (hicd.animator != null)
		{
			hicd.animator.SetInteger("Value", num);
		}
	}

	public void SwitchBingoObjectsActivation(bool _isActive)
	{
		if (m_bingoBoardObject != null && m_bingoBoardObject.get_activeSelf() != _isActive)
		{
			m_bingoBoardObject.SetActive(_isActive);
		}
		if (m_bingoBlockObject != null && m_bingoBlockObject.get_activeSelf() != _isActive)
		{
			m_bingoBlockObject.SetActive(_isActive);
		}
	}

	public void ActivateObjectsByProgress(int progress)
	{
		if (!objectsByProgress.IsNullOrEmpty() && objectsByProgress.Length > progress)
		{
			objectsByProgress[progress].SetActive(true);
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
		if (this.get_transform().get_childCount() <= 0)
		{
			return null;
		}
		Transform[] componentsInChildren = this.get_gameObject().GetComponentsInChildren<Transform>(true);
		if (componentsInChildren == null || componentsInChildren.Length <= 0)
		{
			return null;
		}
		Transform[] array = componentsInChildren;
		foreach (Transform val in array)
		{
			if (!(val == null) && val.get_gameObject().get_name().Equals(colliderNameToCompare) && val.get_childCount() > 0)
			{
				Collider[] componentsInChildren2 = val.GetComponentsInChildren<Collider>(true);
				if (componentsInChildren2 != null && componentsInChildren2.Length > 0)
				{
					return val.get_gameObject();
				}
			}
		}
		return null;
	}

	public void OnResizeGObjContainColliders(Vector3 size)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		if (GObjContainColliders == null)
		{
			GObjContainColliders = OnGetGObjContainColliders();
		}
		if (!(GObjContainColliders == null) && !(size == Vector3.get_zero()) && (!MonoBehaviourSingleton<GoGameSettingsManager>.IsValid() || !(MonoBehaviourSingleton<GoGameSettingsManager>.I.colliderOfMapScale == GObjContainColliders.get_transform().get_localScale())))
		{
			Vector3 localScale = GObjContainColliders.get_transform().get_localScale();
			size.x = localScale.x;
			size.z = localScale.z;
			GObjContainColliders.get_transform().set_localScale(size);
		}
	}
}
