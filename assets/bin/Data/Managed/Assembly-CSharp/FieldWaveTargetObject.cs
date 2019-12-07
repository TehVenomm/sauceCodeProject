using System.Collections.Generic;
using UnityEngine;

public class FieldWaveTargetObject : StageObject, IFieldGimmickObject
{
	public class TargetInfo
	{
		public string name = "";

		public float radius;

		public string iconName = "";

		public bool iconEvent;

		public string dispName = "";
	}

	private const int kDefaultModelIndex = 3;

	private const int kShiftIndex = 1000;

	private const float kHateUpdateInterval = 30f;

	private FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE m_gimmickType;

	private Transform m_modelTrans;

	private int _maxHp;

	private int _nowHp;

	private InGameSettingsManager.WaveMatchParam param;

	private float hateWaitSec;

	private TargetInfo _info = new TargetInfo();

	private int modelIndex = 3;

	private bool isBarrier;

	private Transform barrierEffect;

	public FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE gimmickType => m_gimmickType;

	public int maxHp => _maxHp;

	public int nowHp => _nowHp;

	public bool isDead => _nowHp <= 0;

	public TargetInfo info => _info;

	public static int GetModelIndex(string value2)
	{
		if (value2.IsNullOrWhiteSpace())
		{
			return 3;
		}
		string[] array = value2.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(':');
			if (array2 == null || array2.Length != 2)
			{
				continue;
			}
			string a = array2[0];
			if (a == "mi")
			{
				int result = 0;
				if (int.TryParse(array2[1], out result))
				{
					return result;
				}
			}
		}
		return 3;
	}

	public static string[] GetEffectNamesByModelIndex(int modelIndex)
	{
		List<string> list = new List<string>();
		if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsWaveStrategyMatch())
		{
			list.Add(MakeBarrierEffectNameByModelName(modelIndex));
		}
		return list.ToArray();
	}

	public static string MakeBarrierEffectNameByModelName(int modelIndex)
	{
		return $"ef_btl_defense_wavetarget_barrier_{modelIndex:D2}";
	}

	public static string ConvertModelIndexToName(int idx)
	{
		return $"CMN_wavetarget{idx:D2}";
	}

	public static uint ConvertModelIndexToKey(int idx)
	{
		return (uint)(idx * 1000 + 16);
	}

	public float GetRate()
	{
		if (_maxHp <= 0)
		{
			return 0f;
		}
		if (_nowHp <= 0)
		{
			return 0f;
		}
		return (float)_nowHp / (float)_maxHp;
	}

	public void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		base.Initialize();
		param = MonoBehaviourSingleton<InGameSettingsManager>.I.GetWaveMatchParam();
		base.objectType = OBJECT_TYPE.WAVE_TARGET;
		id = (int)pointData.pointID;
		m_gimmickType = pointData.gimmickType;
		ParseParam(pointData.value2);
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable != null)
		{
			uint key = (uint)m_gimmickType;
			if (m_gimmickType == FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET3)
			{
				key = ConvertModelIndexToKey(modelIndex);
			}
			LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable.Get(key);
			if (loadObject != null)
			{
				m_modelTrans = ResourceUtility.Realizes(loadObject.loadedObject, base._transform);
			}
		}
		_maxHp = (_nowHp = (int)pointData.value1);
		base.coopMode = (MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isStageHost ? COOP_MODE_TYPE.ORIGINAL : COOP_MODE_TYPE.MIRROR);
		if (MonoBehaviourSingleton<UIStatusGizmoManager>.IsValid())
		{
			MonoBehaviourSingleton<UIStatusGizmoManager>.I.CreateWaveTarget(this);
		}
		if (MonoBehaviourSingleton<MiniMap>.IsValid())
		{
			MonoBehaviourSingleton<MiniMap>.I.Attach(this);
		}
		if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid() && m_gimmickType == FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET3)
		{
			MonoBehaviourSingleton<SceneSettingsManager>.I.AddWaveTarget(base.gameObject);
		}
	}

	private void ParseParam(string value2)
	{
		if (value2.IsNullOrWhiteSpace())
		{
			return;
		}
		string[] array = value2.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(':');
			if (array2 == null || array2.Length != 2)
			{
				continue;
			}
			switch (array2[0])
			{
			case "n":
				info.name = array2[1];
				break;
			case "i":
				info.iconName = array2[1];
				break;
			case "ie":
				bool.TryParse(array2[1], out info.iconEvent);
				break;
			case "r":
				if (float.TryParse(array2[1], out info.radius))
				{
					SetColliderRadius(info.radius);
				}
				break;
			case "d":
				info.dispName = array2[1];
				break;
			case "mi":
				int.TryParse(array2[1], out modelIndex);
				break;
			}
		}
	}

	public void SetColliderRadius(float radius)
	{
		if (!(radius <= 0f) && !(m_modelTrans == null))
		{
			SphereCollider component = m_modelTrans.GetComponent<SphereCollider>();
			if (!(component == null))
			{
				component.radius = radius;
			}
		}
	}

	protected override bool IsValidAttackedHit(StageObject fromObject)
	{
		if (isDead || isBarrier)
		{
			return false;
		}
		if (!(fromObject is Enemy))
		{
			return false;
		}
		return base.IsValidAttackedHit(fromObject);
	}

	public override void OnAttackedHitOwner(AttackedHitStatusOwner status)
	{
		if (!isDead && !isBarrier)
		{
			Enemy enemy = status.fromObject as Enemy;
			if (!(enemy == null))
			{
				status.damage = (enemy.isWaveMatchBoss ? param.enemyBossDamage : param.enemyNormalDamage);
				status.afterHP = _nowHp - status.damage;
				base.OnAttackedHitOwner(status);
			}
		}
	}

	public override void OnAttackedHitFix(AttackedHitStatusFix status)
	{
		if (isDead || isBarrier)
		{
			return;
		}
		_nowHp = status.afterHP;
		if (_nowHp <= 0)
		{
			_nowHp = 0;
			SoundManager.PlayOneShotSE(param.targetBreakSeId, status.hitPos);
			if ((object)base.gameObject != null)
			{
				Object.Destroy(base.gameObject);
			}
		}
		else
		{
			EffectManager.OneShot(param.targetHitEffect, new Vector3(status.hitPos.x, 0f, status.hitPos.z), Quaternion.identity, param.targetHitEffectScale);
			SoundManager.PlayOneShotSE(param.targetHitSeId, status.hitPos);
		}
	}

	public int GetId()
	{
		return id;
	}

	public Transform GetTransform()
	{
		return base._transform;
	}

	public string GetObjectName()
	{
		return "WaveTarget";
	}

	public void SetTransform(Transform trans)
	{
		m_modelTrans = trans;
	}

	public float GetTargetRadius()
	{
		return 0f;
	}

	public float GetTargetSqrRadius()
	{
		return 0f;
	}

	public void UpdateTargetMarker(bool isNear)
	{
	}

	public bool IsSearchableNearest()
	{
		return true;
	}

	public void RequestDestroy()
	{
	}

	public string GetIconName(int hpRate = 100)
	{
		if (param == null)
		{
			return "";
		}
		if (param.isEvent || info.iconEvent)
		{
			if (hpRate > 60)
			{
				return "wme_03";
			}
			if (hpRate > 30)
			{
				return "wme_02";
			}
			if (hpRate > 0)
			{
				return "wme_01";
			}
			return "wme_00";
		}
		return _info.iconName;
	}

	public string GetRaderIconName()
	{
		if (param == null)
		{
			return "";
		}
		if (param.isEvent || info.iconEvent)
		{
			if (_nowHp == 0)
			{
				return "wme_dead";
			}
			return "wme";
		}
		return _info.iconName;
	}

	protected override void Update()
	{
		base.Update();
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid() || MonoBehaviourSingleton<StageObjectManager>.I.enemyList == null)
		{
			return;
		}
		hateWaitSec -= Time.deltaTime;
		if (hateWaitSec > 0f)
		{
			return;
		}
		hateWaitSec = 30f;
		for (int i = 0; i < MonoBehaviourSingleton<StageObjectManager>.I.enemyList.Count; i++)
		{
			StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.enemyList[i];
			if (!(stageObject == null) && !(stageObject.controller == null))
			{
				Brain brain = stageObject.controller.brain;
				if (!(brain == null))
				{
					brain.HandleEvent(BRAIN_EVENT.WAVE_TARGET, this);
				}
			}
		}
	}

	public void SetHp(int now, int max, bool changeOwner = false)
	{
		_nowHp = now;
		if (_nowHp <= 0)
		{
			_nowHp = 0;
			if ((object)base.gameObject != null)
			{
				Object.Destroy(base.gameObject);
			}
		}
		if (max > 0)
		{
			_maxHp = max;
		}
		if (_maxHp < _nowHp)
		{
			_maxHp = _nowHp;
		}
		if (changeOwner)
		{
			SetOwner(isOwner: false);
		}
	}

	public void SetOwner(bool isOwner)
	{
		base.coopMode = (isOwner ? COOP_MODE_TYPE.ORIGINAL : COOP_MODE_TYPE.MIRROR);
	}

	public void Barrier()
	{
		if (!isBarrier)
		{
			isBarrier = true;
			barrierEffect = EffectManager.GetEffect(MakeBarrierEffectNameByModelName(modelIndex), base._transform);
		}
	}

	protected override void OnDisable()
	{
		if (barrierEffect != null)
		{
			EffectManager.ReleaseEffect(barrierEffect.gameObject);
		}
		base.OnDisable();
	}
}
