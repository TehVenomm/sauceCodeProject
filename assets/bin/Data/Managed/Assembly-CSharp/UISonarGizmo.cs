using UnityEngine;

public class UISonarGizmo : UIStatusGizmoBase
{
	[SerializeField]
	protected GameObject arrow;

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

	private FieldSonarObject _sonar;

	protected Transform portalTransform;

	protected Transform arrowTransform;

	public FieldSonarObject sonar
	{
		get
		{
			return _sonar;
		}
		set
		{
			_sonar = value;
			if ((Object)_sonar != (Object)null)
			{
				base.gameObject.SetActive(true);
				portalTransform = value.transform;
				UpdateParam();
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		if ((Object)arrow != (Object)null)
		{
			arrowTransform = arrow.transform;
		}
	}

	protected override void UpdateParam()
	{
		if ((Object)sonar == (Object)null || !sonar.gameObject.activeSelf)
		{
			SetActiveSafe(statusSprite.gameObject, false);
			SetActiveSafe(arrow, false);
		}
		else
		{
			Vector3 screenUIPosition = Utility.GetScreenUIPosition(MonoBehaviourSingleton<AppMain>.I.mainCamera, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform, portalTransform.position + offset);
			screenZ = screenUIPosition.z;
			screenUIPosition.z = 0f;
			float num = 1f / MonoBehaviourSingleton<UIManager>.I.uiRoot.pixelSizeAdjustment;
			Vector3 a = screenUIPosition;
			bool flag = false;
			float num2 = (float)Screen.width;
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
				SetActiveSafe(statusSprite.gameObject, true);
				SetActiveSafe(arrow, true);
				Vector3 vector = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screenUIPosition);
				if ((transform.position - vector).sqrMagnitude >= 2E-05f)
				{
					transform.position = vector;
				}
				if ((Object)arrowTransform != (Object)null)
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
				SetActiveSafe(statusSprite.gameObject, false);
				SetActiveSafe(arrow, false);
			}
		}
	}
}
