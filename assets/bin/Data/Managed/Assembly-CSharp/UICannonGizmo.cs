using UnityEngine;

public class UICannonGizmo : UIStatusGizmoBase
{
	[SerializeField]
	protected UISprite arrowSprite;

	[SerializeField]
	protected UISprite statusSprite;

	[SerializeField]
	protected Vector3 offset;

	[SerializeField]
	[Tooltip("スクリ\u30fcン横オフセット")]
	protected float screenSideOffset = 22f;

	[SerializeField]
	[Tooltip("スクリ\u30fcン下オフセット")]
	protected float screenBottomOffset = 112f;

	private FieldGimmickCannonObject _owner;

	protected Transform targetTransform;

	protected Transform arrowTransform;

	private Self myPlayer;

	private UIPanel panel;

	private bool isPlayAnimation;

	public FieldGimmickCannonObject owner
	{
		get
		{
			return _owner;
		}
		set
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Expected O, but got Unknown
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			_owner = value;
			if (_owner != null)
			{
				this.get_gameObject().SetActive(true);
				targetTransform = value.get_transform();
				UpdateParam();
			}
			else
			{
				this.get_gameObject().SetActive(false);
			}
		}
	}

	protected override void OnEnable()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		base.OnEnable();
		if (arrowSprite != null)
		{
			arrowTransform = arrowSprite.get_transform().get_parent();
		}
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.playerList != null)
		{
			myPlayer = (MonoBehaviourSingleton<StageObjectManager>.I.playerList.Find((StageObject x) => x is Self) as Self);
		}
		panel = this.GetComponent<UIPanel>();
		isPlayAnimation = false;
	}

	protected override void UpdateParam()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		if (owner == null || !owner.get_gameObject().get_activeSelf())
		{
			SetSpriteEnable(false);
			isPlayAnimation = false;
		}
		else
		{
			Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
			if (boss != null)
			{
				if (!boss.IsValidShield())
				{
					SetSpriteEnable(false);
					isPlayAnimation = false;
					return;
				}
				if (myPlayer != null)
				{
					if (myPlayer.IsOnCannonMode())
					{
						SetSpriteEnable(false);
						isPlayAnimation = false;
						return;
					}
					SetSpriteEnable(true);
				}
				if (!isPlayAnimation)
				{
					isPlayAnimation = true;
					UITweenCtrl component = this.GetComponent<UITweenCtrl>();
					component.Reset();
					component.Play(true, null);
				}
			}
			Vector3 screenUIPosition = Utility.GetScreenUIPosition(MonoBehaviourSingleton<AppMain>.I.mainCamera, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform, targetTransform.get_position() + offset);
			screenZ = screenUIPosition.z;
			screenUIPosition.z = 0f;
			float num = 1f / MonoBehaviourSingleton<UIManager>.I.uiRoot.pixelSizeAdjustment;
			Vector3 val = screenUIPosition;
			bool flag = false;
			float num2 = (float)Screen.get_width();
			if (screenUIPosition.x < screenSideOffset * num)
			{
				screenUIPosition.x = screenSideOffset * num;
				flag = true;
			}
			else if (screenUIPosition.x > num2 - screenSideOffset * num)
			{
				screenUIPosition.x = num2 - screenSideOffset * num;
				flag = true;
			}
			if (screenUIPosition.y < screenBottomOffset * num)
			{
				screenUIPosition.y = screenBottomOffset * num;
				flag = true;
			}
			if (flag)
			{
				SetSpriteEnable(true);
				Vector3 val2 = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screenUIPosition);
				Vector3 val3 = transform.get_position() - val2;
				if (val3.get_sqrMagnitude() >= 2E-05f)
				{
					transform.set_position(val2);
				}
				if (arrowTransform != null)
				{
					Vector3 val4 = val - screenUIPosition;
					if (val4 != Vector3.get_zero())
					{
						float num3 = 90f - Vector3.Angle(Vector3.get_right(), val4);
						arrowTransform.set_eulerAngles(new Vector3(0f, 0f, num3));
					}
					else
					{
						arrowTransform.set_eulerAngles(new Vector3(0f, 0f, 0f));
					}
				}
			}
			else
			{
				SetSpriteEnable(false);
			}
		}
	}

	private void SetSpriteEnable(bool enable)
	{
		if (panel != null)
		{
			panel.set_enabled(enable);
		}
	}
}
