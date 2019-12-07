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
			_owner = value;
			if (_owner != null)
			{
				base.gameObject.SetActive(value: true);
				targetTransform = value.transform;
				UpdateParam();
			}
			else
			{
				base.gameObject.SetActive(value: false);
			}
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		if (arrowSprite != null)
		{
			arrowTransform = arrowSprite.transform.parent;
		}
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.playerList != null)
		{
			myPlayer = (MonoBehaviourSingleton<StageObjectManager>.I.playerList.Find((StageObject x) => x is Self) as Self);
		}
		panel = GetComponent<UIPanel>();
		isPlayAnimation = false;
	}

	protected override void UpdateParam()
	{
		if (owner == null || !owner.gameObject.activeSelf)
		{
			SetSpriteEnable(enable: false);
			isPlayAnimation = false;
			return;
		}
		Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
		if (boss != null)
		{
			if (!boss.IsValidShield())
			{
				SetSpriteEnable(enable: false);
				isPlayAnimation = false;
				return;
			}
			if (myPlayer != null)
			{
				if (myPlayer.IsOnCannonMode())
				{
					SetSpriteEnable(enable: false);
					isPlayAnimation = false;
					return;
				}
				SetSpriteEnable(enable: true);
			}
			if (!isPlayAnimation)
			{
				isPlayAnimation = true;
				UITweenCtrl component = GetComponent<UITweenCtrl>();
				component.Reset();
				component.Play();
			}
		}
		Vector3 screenUIPosition = Utility.GetScreenUIPosition(MonoBehaviourSingleton<AppMain>.I.mainCamera, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform, targetTransform.position + offset);
		screenZ = screenUIPosition.z;
		screenUIPosition.z = 0f;
		float num = 1f / MonoBehaviourSingleton<UIManager>.I.uiRoot.pixelSizeAdjustment;
		Vector3 a = screenUIPosition;
		bool flag = false;
		float num2 = Screen.width;
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
			SetSpriteEnable(enable: true);
			Vector3 vector = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screenUIPosition);
			if ((transform.position - vector).sqrMagnitude >= 2E-05f)
			{
				transform.position = vector;
			}
			if (arrowTransform != null)
			{
				Vector3 vector2 = a - screenUIPosition;
				if (vector2 != Vector3.zero)
				{
					float z = 90f - Vector3.Angle(Vector3.right, vector2);
					arrowTransform.eulerAngles = new Vector3(0f, 0f, z);
				}
				else
				{
					arrowTransform.eulerAngles = new Vector3(0f, 0f, 0f);
				}
			}
		}
		else
		{
			SetSpriteEnable(enable: false);
		}
	}

	private void SetSpriteEnable(bool enable)
	{
		if (panel != null)
		{
			panel.enabled = enable;
		}
	}
}
