using UnityEngine;

public class UIGrabStatusGizmo : UIStatusGizmoBase
{
	private const float OFFSET_Y = -1f;

	[SerializeField]
	protected UIHGauge gaugeUI;

	private Enemy _targetEnemy;

	private TargetPoint _targetPoint;

	public Enemy targetEnemy
	{
		get
		{
			return _targetEnemy;
		}
		set
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			_targetEnemy = value;
			if (_targetEnemy != null)
			{
				this.get_gameObject().SetActive(true);
				UpdateParam();
			}
			else
			{
				this.get_gameObject().SetActive(false);
			}
		}
	}

	public TargetPoint targetPoint
	{
		get
		{
			return _targetPoint;
		}
		set
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			_targetPoint = value;
			if (_targetPoint != null)
			{
				this.get_gameObject().SetActive(true);
				UpdateParam();
			}
			else
			{
				this.get_gameObject().SetActive(false);
			}
		}
	}

	protected override void UpdateParam()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		if (!(targetEnemy == null) && !(this.targetPoint == null))
		{
			Vector3 targetPoint = this.targetPoint.GetTargetPoint();
			targetPoint.y += -1f;
			Vector3 val = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(targetPoint);
			screenZ = val.z;
			if (val.z < 0f)
			{
				val *= -1f;
			}
			val.z = 0f;
			Vector3 val2 = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(val);
			Vector3 val3 = transform.get_position() - val2;
			if (val3.get_sqrMagnitude() >= 2E-05f)
			{
				transform.set_position(val2);
			}
			if (gaugeUI != null)
			{
				if (targetEnemy.IsValidGrabHp)
				{
					if (!gaugeUI.get_isActiveAndEnabled())
					{
						gaugeUI.get_gameObject().SetActive(true);
					}
					float num = (float)(int)targetEnemy.GrabHp / (float)(int)targetEnemy.GrabHpMax;
					if (num < 0f)
					{
						num = 0f;
					}
					if (gaugeUI.nowPercent != num)
					{
						gaugeUI.SetPercent(num, true);
					}
				}
				else if (gaugeUI.get_isActiveAndEnabled())
				{
					gaugeUI.get_gameObject().SetActive(false);
				}
			}
		}
	}
}
