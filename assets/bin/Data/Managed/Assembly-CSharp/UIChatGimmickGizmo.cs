using UnityEngine;

public class UIChatGimmickGizmo : UIStatusGizmoBase
{
	[SerializeField]
	protected GameObject chatUI;

	[SerializeField]
	protected UILabel chatLabel;

	[SerializeField]
	protected TweenScale chatTween;

	[Tooltip("スクリ\u30fcン横オフセット")]
	[SerializeField]
	protected float screenSideOffset = 36f;

	[SerializeField]
	[Tooltip("スクリ\u30fcン下オフセット")]
	protected float screenBottomOffset = 107f;

	[SerializeField]
	[Tooltip("スクリ\u30fcン下オフセット、フィ\u30fcルド時")]
	protected float screenBottomFieldOffset = 107f;

	[Tooltip("チャット横オフセット")]
	[SerializeField]
	protected float chatSideOffset = 60f;

	[Tooltip("チャット上オフセット")]
	[SerializeField]
	protected float chatTopOffset = 120f;

	private float sizeAdjust = 1f;

	protected FieldChatGimmickObject chatGimmickObj;

	protected Vector3 chatUILocalPos = Vector3.get_zero();

	protected Transform chatTransform;

	public void Initialize(FieldChatGimmickObject obj)
	{
		chatGimmickObj = obj;
	}

	protected override void OnEnable()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		sizeAdjust = 1f / MonoBehaviourSingleton<UIManager>.I.uiRoot.pixelSizeAdjustment;
		base.OnEnable();
		if (chatUI != null)
		{
			chatTransform = chatUI.get_transform();
			chatUILocalPos = chatTransform.get_localPosition();
			chatUI.SetActive(false);
		}
		if (chatTween != null)
		{
			chatTween.SetOnFinished(OnFinishChat);
		}
	}

	protected override void UpdateParam()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		if (chatUI.get_activeSelf())
		{
			Vector3 screenUIPosition = Utility.GetScreenUIPosition(MonoBehaviourSingleton<AppMain>.I.mainCamera, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform, chatGimmickObj.GetPosition());
			screenUIPosition.z = 0f;
			Vector3 val = screenUIPosition;
			float num = (float)Screen.get_width();
			float num2 = (float)Screen.get_height();
			if (screenUIPosition.x < screenSideOffset * sizeAdjust)
			{
				screenUIPosition.x = screenSideOffset * sizeAdjust;
			}
			else if (screenUIPosition.x > num - screenSideOffset * sizeAdjust)
			{
				screenUIPosition.x = num - screenSideOffset * sizeAdjust;
			}
			float num3 = screenBottomOffset;
			if (FieldManager.IsValidInGameNoQuest())
			{
				num3 = screenBottomFieldOffset;
			}
			if (screenUIPosition.y < num3 * sizeAdjust)
			{
				screenUIPosition.y = num3 * sizeAdjust;
			}
			Vector3 val2 = screenUIPosition;
			if (chatUI.get_activeSelf())
			{
				if (val2.x < chatSideOffset * sizeAdjust)
				{
					val2.x = chatSideOffset * sizeAdjust;
				}
				else if (val2.x > num - chatSideOffset * sizeAdjust)
				{
					val2.x = num - chatSideOffset * sizeAdjust;
				}
				if (val2.y > num2 - chatTopOffset * sizeAdjust)
				{
					val2.y = num2 - chatTopOffset * sizeAdjust;
				}
			}
			Vector3 val3 = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screenUIPosition);
			Vector3 val4 = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(val2);
			Vector3 val5 = transform.get_position() - val3;
			if (val5.get_sqrMagnitude() >= 2E-05f)
			{
				transform.set_position(val3);
			}
			Matrix4x4 worldToLocalMatrix = transform.get_worldToLocalMatrix();
			Vector3 localPosition = worldToLocalMatrix.MultiplyPoint3x4(val4);
			localPosition.y += chatUILocalPos.y;
			localPosition.z = 0f;
			chatTransform.set_localPosition(localPosition);
		}
	}

	public void SayChat(string message)
	{
		chatLabel.text = message;
		chatUI.SetActive(true);
		if (chatTween != null)
		{
			chatTween.ResetToBeginning();
			chatTween.PlayForward();
		}
	}

	public void OnFinishChat()
	{
		chatUI.SetActive(false);
	}

	public bool isDisp()
	{
		return chatUI.get_activeSelf();
	}
}
