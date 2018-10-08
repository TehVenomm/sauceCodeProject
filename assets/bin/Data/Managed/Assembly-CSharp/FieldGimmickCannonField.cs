using UnityEngine;

public class FieldGimmickCannonField : FieldGimmickCannonBase
{
	private const string kDefaultAttackInfoName = "cannonball_field";

	private const int kDefaultModelIndex = 5;

	private const int kShiftIndex = 1000;

	private readonly Vector3 OFFSET_LEFT = new Vector3(-0.4f, 0f, 0f);

	private readonly Vector3 OFFSET_RIGHT = new Vector3(0.4f, 0f, 0f);

	private readonly Vector3 OFFSET_ZERO = Vector3.get_zero();

	private Vector3[] offsetArray;

	private int shotSeId;

	private bool isAimCamera;

	private string attackInfoName = "cannonball_field";

	private int modelIndex = 5;

	public static string GetAttackInfoName(string value2)
	{
		if (value2.IsNullOrWhiteSpace())
		{
			return "cannonball_field";
		}
		string[] array = value2.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(':');
			if (array2 != null && array2.Length == 2)
			{
				switch (array2[0])
				{
				case "ai":
					return array2[1];
				}
			}
		}
		return "cannonball_field";
	}

	public static int GetModelIndex(string value2)
	{
		if (value2.IsNullOrWhiteSpace())
		{
			return 5;
		}
		string[] array = value2.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(':');
			if (array2 != null && array2.Length == 2)
			{
				switch (array2[0])
				{
				case "mi":
				{
					int result = 0;
					if (int.TryParse(array2[1], out result))
					{
						return result;
					}
					break;
				}
				}
			}
		}
		return 5;
	}

	public static string ConvertModelIndexToName(int idx)
	{
		return $"CMN_cannon{idx:D2}";
	}

	public static uint ConvertModelIndexToKey(int idx)
	{
		return (uint)(idx * 1000 + 11);
	}

	public override bool IsAimCamera()
	{
		return isAimCamera;
	}

	public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Expected O, but got Unknown
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		ParseParam(pointData.value2);
		base.Initialize(pointData);
		m_coolTime = MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.coolTimeForField;
		m_baseTrans = modelTrans.Find("CMN_cannon01_Origin/Move/Root/base/rot");
		m_cannonTrans = modelTrans.Find("CMN_cannon01_Origin/Move/Root/base/rot/cannon_rot");
		offsetArray = (Vector3[])new Vector3[3]
		{
			OFFSET_ZERO,
			OFFSET_RIGHT,
			OFFSET_LEFT
		};
		shotSeId = MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForField;
	}

	protected override void CreateModel()
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable != null)
		{
			LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable.Get(ConvertModelIndexToKey(modelIndex));
			if (loadObject != null)
			{
				modelTrans = ResourceUtility.Realizes(loadObject.loadedObject, m_transform, -1);
			}
		}
	}

	protected override void UpdateStateStandBy()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (!(m_owner == null) && !(m_baseTrans == null))
		{
			m_owner._rotation = Quaternion.LookRotation(m_baseTrans.get_forward());
			SetState(STATE.READY);
		}
	}

	public override void Shot()
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		if (IsReadyForShot())
		{
			if (base._animator != null)
			{
				base._animator.Play("Reaction", 0, 0f);
			}
			AttackInfo attackHitInfo = GetAttackHitInfo();
			if (attackHitInfo != null)
			{
				int num = Random.Range(0, 3);
				AttackCannonball.InitParamCannonball initParamCannonball = new AttackCannonball.InitParamCannonball();
				initParamCannonball.attacker = m_owner;
				initParamCannonball.atkInfo = attackHitInfo;
				initParamCannonball.launchTrans = m_cannonTrans;
				initParamCannonball.offsetPos = offsetArray[num];
				initParamCannonball.offsetRot = Quaternion.get_identity();
				initParamCannonball.shotRotation = m_cannonTrans.get_rotation();
				GameObject val = new GameObject("AttackCannonball");
				AttackCannonball attackCannonball = val.AddComponent<AttackCannonball>();
				attackCannonball.Initialize(initParamCannonball);
				if (shotSeId > 0)
				{
					SoundManager.PlayOneShotSE(shotSeId, m_cannonTrans.get_position());
				}
				StartCoolTime();
				SetState(STATE.COOLTIME);
			}
		}
	}

	protected override AttackInfo GetAttackHitInfo()
	{
		if (m_owner == null)
		{
			return null;
		}
		AttackInfo attackInfo = m_owner.GetAttackInfos().Find((AttackInfo info) => info.name == attackInfoName);
		if (attackInfo == null)
		{
			return null;
		}
		return attackInfo;
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
					case "a":
						isAimCamera = (array2[1] != "0");
						break;
					case "ai":
						attackInfoName = array2[1];
						break;
					case "mi":
						int.TryParse(array2[1], out modelIndex);
						break;
					}
				}
			}
		}
	}
}
