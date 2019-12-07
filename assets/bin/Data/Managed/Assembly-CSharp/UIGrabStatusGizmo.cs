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
			if (_targetEnemy != null)
			{
				base.gameObject.SetActive(value: true);
				UpdateParam();
			}
			else
			{
				base.gameObject.SetActive(value: false);
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
			if (_targetPoint != null)
			{
				base.gameObject.SetActive(value: true);
				UpdateParam();
			}
			else
			{
				base.gameObject.SetActive(value: false);
			}
		}
	}

	protected override void UpdateParam()
	{
		if (targetEnemy == null || this.targetPoint == null)
		{
			return;
		}
		Vector3 targetPoint = this.targetPoint.GetTargetPoint();
		targetPoint.y += -1f;
		Vector3 position = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(targetPoint);
		screenZ = position.z;
		if (position.z < 0f)
		{
			position *= -1f;
		}
		position.z = 0f;
		Vector3 vector = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(position);
		if ((transform.position - vector).sqrMagnitude >= 2E-05f)
		{
			transform.position = vector;
		}
		if (!(gaugeUI != null))
		{
			return;
		}
		if (targetEnemy.IsValidGrabHp)
		{
			if (!gaugeUI.isActiveAndEnabled)
			{
				gaugeUI.gameObject.SetActive(value: true);
			}
			float num = (float)(int)targetEnemy.GrabHp / (float)(int)targetEnemy.GrabHpMax;
			if (num < 0f)
			{
				num = 0f;
			}
			if (gaugeUI.nowPercent != num)
			{
				gaugeUI.SetPercent(num);
			}
		}
		else if (gaugeUI.isActiveAndEnabled)
		{
			gaugeUI.gameObject.SetActive(value: false);
		}
	}
}
