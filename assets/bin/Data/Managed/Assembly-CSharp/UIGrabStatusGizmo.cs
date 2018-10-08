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
			_targetEnemy = value;
			if ((Object)_targetEnemy != (Object)null)
			{
				base.gameObject.SetActive(true);
				UpdateParam();
			}
			else
			{
				base.gameObject.SetActive(false);
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
			_targetPoint = value;
			if ((Object)_targetPoint != (Object)null)
			{
				base.gameObject.SetActive(true);
				UpdateParam();
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}
	}

	protected override void UpdateParam()
	{
		if (!((Object)targetEnemy == (Object)null) && !((Object)this.targetPoint == (Object)null))
		{
			Vector3 targetPoint = this.targetPoint.GetTargetPoint();
			targetPoint.y += -1f;
			Vector3 vector = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(targetPoint);
			screenZ = vector.z;
			if (vector.z < 0f)
			{
				vector *= -1f;
			}
			vector.z = 0f;
			Vector3 vector2 = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(vector);
			if ((transform.position - vector2).sqrMagnitude >= 2E-05f)
			{
				transform.position = vector2;
			}
			if ((Object)gaugeUI != (Object)null)
			{
				if (targetEnemy.IsValidGrabHp)
				{
					if (!gaugeUI.isActiveAndEnabled)
					{
						gaugeUI.gameObject.SetActive(true);
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
				else if (gaugeUI.isActiveAndEnabled)
				{
					gaugeUI.gameObject.SetActive(false);
				}
			}
		}
	}
}
