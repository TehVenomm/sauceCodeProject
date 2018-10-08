using UnityEngine;

public class FieldWaveTargetObject : StageObject, IFieldGimmickObject
{
	public class TargetInfo
	{
		public string name = string.Empty;

		public float radius;

		public string iconName = string.Empty;
	}

	private const float kHateUpdateInterval = 30f;

	private FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE m_gimmickType;

	private Transform m_modelTrans;

	private int _maxHp;

	private int _nowHp;

	private InGameSettingsManager.WaveMatchParam param;

	private float hateWaitSec;

	private TargetInfo _info = new TargetInfo();

	public int nowHp => _nowHp;

	public bool isDead => _nowHp <= 0;

	public TargetInfo info => _info;

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
		param = MonoBehaviourSingleton<InGameSettingsManager>.I.waveMatchParam;
		base.objectType = OBJECT_TYPE.WAVE_TARGET;
		id = (int)pointData.pointID;
		m_gimmickType = pointData.gimmickType;
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable != null)
		{
			LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable.Get((uint)m_gimmickType);
			if (loadObject != null)
			{
				m_modelTrans = ResourceUtility.Realizes(loadObject.loadedObject, base._transform, -1);
			}
		}
		_maxHp = (_nowHp = (int)pointData.value1);
		base.coopMode = (MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isStageHost ? COOP_MODE_TYPE.ORIGINAL : COOP_MODE_TYPE.MIRROR);
		ParseParam(pointData.value2);
		if (MonoBehaviourSingleton<UIStatusGizmoManager>.IsValid())
		{
			MonoBehaviourSingleton<UIStatusGizmoManager>.I.CreateWaveTarget(this);
		}
		if (MonoBehaviourSingleton<MiniMap>.IsValid())
		{
			MonoBehaviourSingleton<MiniMap>.I.Attach(this);
		}
	}

	private void ParseParam(string value2)
	{
		if (!value2.IsNullOrWhiteSpace())
		{
			string[] array = value2.Split(',');
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(':');
				if (array2 != null && array2.Length == 2)
				{
					switch (array2[0])
					{
					case "n":
						info.name = array2[1];
						break;
					case "i":
						info.iconName = array2[1];
						break;
					case "r":
						if (float.TryParse(array2[1], out info.radius))
						{
							SetColliderRadius(info.radius);
						}
						break;
					}
				}
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
				component.set_radius(radius);
			}
		}
	}

	protected override bool IsValidAttackedHit(StageObject fromObject)
	{
		if (isDead)
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
		if (!isDead)
		{
			Enemy enemy = status.fromObject as Enemy;
			if (!(enemy == null))
			{
				status.damage = ((!enemy.isWaveMatchBoss) ? param.enemyNormalDamage : param.enemyBossDamage);
				status.afterHP = _nowHp - status.damage;
				base.OnAttackedHitOwner(status);
			}
		}
	}

	public override void OnAttackedHitFix(AttackedHitStatusFix status)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		if (!isDead)
		{
			_nowHp = status.afterHP;
			if (_nowHp <= 0)
			{
				_nowHp = 0;
				SoundManager.PlayOneShotSE(param.targetBreakSeId, status.hitPos);
				if (!object.ReferenceEquals((object)this.get_gameObject(), null))
				{
					Object.Destroy(this.get_gameObject());
				}
			}
			else
			{
				string targetHitEffect = param.targetHitEffect;
				Vector3 hitPos = status.hitPos;
				float x = hitPos.x;
				Vector3 hitPos2 = status.hitPos;
				EffectManager.OneShot(targetHitEffect, new Vector3(x, 0f, hitPos2.z), Quaternion.get_identity(), param.targetHitEffectScale, false, null);
				SoundManager.PlayOneShotSE(param.targetHitSeId, status.hitPos);
			}
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

	public void RequestDestroy()
	{
	}

	protected override void Update()
	{
		base.Update();
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.enemyList != null)
		{
			hateWaitSec -= Time.get_deltaTime();
			if (!(hateWaitSec > 0f))
			{
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
		}
	}
}
