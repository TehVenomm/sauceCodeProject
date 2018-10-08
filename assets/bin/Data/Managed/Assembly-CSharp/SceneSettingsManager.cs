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

	protected override void Awake()
	{
		base.Awake();
		ApplyScene(false);
		ParseObjectName();
		addWaveTargetList.Clear();
	}

	private void ParseObjectName()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Expected O, but got Unknown
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		foreach (Transform item in this.get_transform())
		{
			Transform val = item;
			if (val.get_name().Contains("Objects"))
			{
				foreach (Transform item2 in val)
				{
					Transform val2 = item2;
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
		}
	}

	private void Start()
	{
		ApplyStageMaterial();
	}

	public void InitializeScene()
	{
		ApplyScene(false);
		ParseObjectName();
		addWaveTargetList.Clear();
		ApplyStageMaterial();
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
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoChangeWeather(changingTime, duration));
	}

	private IEnumerator DoChangeWeather(float changingTime, float duration)
	{
		float timer2 = 0f;
		weatherController.OnStartWeatherChange();
		while (timer2 < changingTime)
		{
			timer2 += Time.get_deltaTime();
			weatherController.Update(timer2 / changingTime);
			yield return (object)null;
		}
		weatherController.OnFinishedWeatherChange();
		yield return (object)new WaitForSeconds(duration);
		timer2 = 0f;
		weatherController.OnStartReturnToOriginal();
		while (timer2 < changingTime)
		{
			timer2 += Time.get_deltaTime();
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
		if (waveTargets != null && waveTargets.Length != 0)
		{
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
}
