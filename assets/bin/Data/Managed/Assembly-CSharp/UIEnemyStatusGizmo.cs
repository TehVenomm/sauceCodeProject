using UnityEngine;

public class UIEnemyStatusGizmo : UIStatusGizmoBase
{
	[SerializeField]
	protected UIHGauge gaugeUI;

	[SerializeField]
	protected UILabel nameLabel;

	[SerializeField]
	protected UITexture dropTarget;

	[SerializeField]
	protected UIHGauge gaugeAegisUI;

	private Enemy _targetEnemy;

	private float hight;

	public Enemy targetEnemy
	{
		get
		{
			return _targetEnemy;
		}
		set
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
			_targetEnemy = value;
			if (_targetEnemy != null)
			{
				this.get_gameObject().SetActive(true);
				hight = _targetEnemy.uiHeight;
				if (_targetEnemy.enemyTableData != null)
				{
					hight *= _targetEnemy.enemyTableData.modelScale;
				}
				if (nameLabel != null)
				{
					nameLabel.text = $"{_targetEnemy.charaName} Lv.{_targetEnemy.enemyLevel}";
				}
				SetTargetIcon(null);
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
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		if (!(targetEnemy == null))
		{
			Vector3 position = targetEnemy._position;
			position.y += hight;
			Vector3 val = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(position);
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
				float num = (float)targetEnemy.hpShow / (float)targetEnemy.hpMax;
				if (num < 0f)
				{
					num = 0f;
				}
				if (gaugeUI.nowPercent != num)
				{
					gaugeUI.SetPercent(num, true);
				}
			}
			if (gaugeAegisUI != null)
			{
				float aegisPercent = targetEnemy.GetAegisPercent();
				bool flag = aegisPercent > 0f;
				if (gaugeAegisUI.nowPercent != aegisPercent)
				{
					gaugeAegisUI.SetPercent(aegisPercent, true);
				}
				if (gaugeAegisUI.get_gameObject().get_activeSelf() != flag)
				{
					gaugeAegisUI.get_gameObject().SetActive(flag);
				}
			}
		}
	}

	public void SetTargetIcon(Texture texture)
	{
		dropTarget.mainTexture = texture;
	}
}
