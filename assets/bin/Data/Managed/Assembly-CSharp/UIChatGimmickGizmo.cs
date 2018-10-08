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

	[Tooltip("スクリ\u30fcン下オフセット")]
	[SerializeField]
	protected float screenBottomOffset = 107f;

	[Tooltip("スクリ\u30fcン下オフセット、フィ\u30fcルド時")]
	[SerializeField]
	protected float screenBottomFieldOffset = 107f;

	[Tooltip("チャット横オフセット")]
	[SerializeField]
	protected float chatSideOffset = 60f;

	[Tooltip("チャット上オフセット")]
	[SerializeField]
	protected float chatTopOffset = 120f;

	private float sizeAdjust = 1f;

	protected FieldChatGimmickObject chatGimmickObj;

	protected Vector3 chatUILocalPos = Vector3.zero;

	protected Transform chatTransform;

	public void Initialize(FieldChatGimmickObject obj)
	{
		chatGimmickObj = obj;
	}

	protected override void OnEnable()
	{
		sizeAdjust = 1f / MonoBehaviourSingleton<UIManager>.I.uiRoot.pixelSizeAdjustment;
		base.OnEnable();
		if ((Object)chatUI != (Object)null)
		{
			chatTransform = chatUI.transform;
			chatUILocalPos = chatTransform.localPosition;
			chatUI.SetActive(false);
		}
		if ((Object)chatTween != (Object)null)
		{
			chatTween.SetOnFinished(OnFinishChat);
		}
	}

	protected override void UpdateParam()
	{
		if (chatUI.activeSelf)
		{
			Vector3 screenUIPosition = Utility.GetScreenUIPosition(MonoBehaviourSingleton<AppMain>.I.mainCamera, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform, chatGimmickObj.GetPosition());
			screenUIPosition.z = 0f;
			Vector3 vector = screenUIPosition;
			float num = (float)Screen.width;
			float num2 = (float)Screen.height;
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
			Vector3 position = screenUIPosition;
			if (chatUI.activeSelf)
			{
				if (position.x < chatSideOffset * sizeAdjust)
				{
					position.x = chatSideOffset * sizeAdjust;
				}
				else if (position.x > num - chatSideOffset * sizeAdjust)
				{
					position.x = num - chatSideOffset * sizeAdjust;
				}
				if (position.y > num2 - chatTopOffset * sizeAdjust)
				{
					position.y = num2 - chatTopOffset * sizeAdjust;
				}
			}
			Vector3 vector2 = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screenUIPosition);
			Vector3 v = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(position);
			if ((transform.position - vector2).sqrMagnitude >= 2E-05f)
			{
				transform.position = vector2;
			}
			Vector3 localPosition = transform.worldToLocalMatrix.MultiplyPoint3x4(v);
			localPosition.y += chatUILocalPos.y;
			localPosition.z = 0f;
			chatTransform.localPosition = localPosition;
		}
	}

	public void SayChat(string message)
	{
		chatLabel.text = message;
		chatUI.SetActive(true);
		if ((Object)chatTween != (Object)null)
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
		return chatUI.activeSelf;
	}
}
