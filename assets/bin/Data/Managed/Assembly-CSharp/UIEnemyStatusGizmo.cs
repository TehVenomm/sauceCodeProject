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
			if ((Object)_targetEnemy != (Object)null)
			{
				base.gameObject.SetActive(true);
				hight = _targetEnemy.uiHeight;
				if (_targetEnemy.enemyTableData != null)
				{
					hight *= _targetEnemy.enemyTableData.modelScale;
				}
				if ((Object)nameLabel != (Object)null)
				{
					nameLabel.text = $"{_targetEnemy.charaName} Lv.{_targetEnemy.enemyLevel}";
				}
				SetTargetIcon(null);
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
		if (!((Object)targetEnemy == (Object)null))
		{
			Vector3 position = targetEnemy._position;
			position.y += hight;
			Vector3 vector = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(position);
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
			if ((Object)gaugeAegisUI != (Object)null)
			{
				float aegisPercent = targetEnemy.GetAegisPercent();
				bool flag = aegisPercent > 0f;
				if (gaugeAegisUI.nowPercent != aegisPercent)
				{
					gaugeAegisUI.SetPercent(aegisPercent, true);
				}
				if (gaugeAegisUI.gameObject.activeSelf != flag)
				{
					gaugeAegisUI.gameObject.SetActive(flag);
				}
			}
		}
	}

	public void SetTargetIcon(Texture texture)
	{
		dropTarget.mainTexture = texture;
	}
}
