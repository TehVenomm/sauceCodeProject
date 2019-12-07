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
			_targetEnemy = value;
			if (_targetEnemy != null)
			{
				base.gameObject.SetActive(value: true);
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
				base.gameObject.SetActive(value: false);
			}
		}
	}

	protected override void UpdateParam()
	{
		if (targetEnemy == null)
		{
			return;
		}
		Vector3 position = targetEnemy._position;
		position.y += hight;
		Vector3 position2 = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(position);
		screenZ = position2.z;
		if (position2.z < 0f)
		{
			position2 *= -1f;
		}
		position2.z = 0f;
		Vector3 vector = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(position2);
		if ((transform.position - vector).sqrMagnitude >= 2E-05f)
		{
			transform.position = vector;
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
				gaugeUI.SetPercent(num);
			}
		}
		if (gaugeAegisUI != null)
		{
			float aegisPercent = targetEnemy.GetAegisPercent();
			bool flag = aegisPercent > 0f;
			if (gaugeAegisUI.nowPercent != aegisPercent)
			{
				gaugeAegisUI.SetPercent(aegisPercent);
			}
			if (gaugeAegisUI.gameObject.activeSelf != flag)
			{
				gaugeAegisUI.gameObject.SetActive(flag);
			}
		}
	}

	public void SetTargetIcon(Texture texture)
	{
		dropTarget.mainTexture = texture;
	}
}
