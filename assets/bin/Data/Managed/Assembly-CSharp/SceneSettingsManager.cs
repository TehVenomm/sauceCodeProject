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

	protected override void Awake()
	{
		base.Awake();
		ApplyScene(false);
		ParseObjectName();
		addWaveTargetList.Clear();
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
		if (!((UnityEngine.Object)fx == (UnityEngine.Object)null) && (force || fx.BaseColor.a == 0f))
		{
			Color color = fx.BaseColor = ((!MonoBehaviourSingleton<SceneSettingsManager>.IsValid()) ? Color.white : MonoBehaviourSingleton<SceneSettingsManager>.I.effectColor);
		}
	}

	public static T GetLinkResource<T>(string name) where T : UnityEngine.Object
	{
		if (!MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
		{
			return (T)null;
		}
		UnityEngine.Object[] array = MonoBehaviourSingleton<SceneSettingsManager>.I.linkResources;
		if (array == null)
		{
			return (T)null;
		}
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			if (array[i] != (UnityEngine.Object)null && array[i].name == name)
			{
				return array[i] as T;
			}
		}
		return (T)null;
	}

	public void ChangeWeather(float changingTime, float duration)
	{
		StartCoroutine(DoChangeWeather(changingTime, duration));
	}

	private IEnumerator DoChangeWeather(float changingTime, float duration)
	{
		float timer2 = 0f;
		weatherController.OnStartWeatherChange();
		while (timer2 < changingTime)
		{
			timer2 += Time.deltaTime;
			weatherController.Update(timer2 / changingTime);
			yield return (object)null;
		}
		weatherController.OnFinishedWeatherChange();
		yield return (object)new WaitForSeconds(duration);
		timer2 = 0f;
		weatherController.OnStartReturnToOriginal();
		while (timer2 < changingTime)
		{
			timer2 += Time.deltaTime;
			weatherController.Update(1f - timer2 / changingTime);
			yield return (object)null;
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
			if ((UnityEngine.Object)gameObject != (UnityEngine.Object)null && gameObject.name == name)
			{
				return gameObject;
			}
		}
		return null;
	}

	public void AddWaveTarget(GameObject target)
	{
		if (!((UnityEngine.Object)target == (UnityEngine.Object)null))
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
				if ((UnityEngine.Object)gameObject != (UnityEngine.Object)null)
				{
					gameObject.SetActive(false);
				}
			}
		}
		if (waveTargets != null && waveTargets.Length != 0)
		{
			int j = 0;
			for (int num = waveTargets.Length; j < num; j++)
			{
				GameObject gameObject2 = waveTargets[j];
				if ((UnityEngine.Object)gameObject2 != (UnityEngine.Object)null)
				{
					gameObject2.SetActive(false);
				}
			}
		}
	}

	public void SetEventItemCount(List<EventItemCounts> list)
	{
		if (list != null && list.Count > 0 && this.homeInfoCountData != null && this.homeInfoCountData.Length > 0)
		{
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
		if ((UnityEngine.Object)hicd.animator != (UnityEngine.Object)null)
		{
			hicd.animator.SetInteger("Value", value);
		}
	}

	public void SwitchBingoObjectsActivation(bool _isActive)
	{
		if ((UnityEngine.Object)m_bingoBoardObject != (UnityEngine.Object)null && m_bingoBoardObject.activeSelf != _isActive)
		{
			m_bingoBoardObject.SetActive(_isActive);
		}
		if ((UnityEngine.Object)m_bingoBlockObject != (UnityEngine.Object)null && m_bingoBlockObject.activeSelf != _isActive)
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
}
