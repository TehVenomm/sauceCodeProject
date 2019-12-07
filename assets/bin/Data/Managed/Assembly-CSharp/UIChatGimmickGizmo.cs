using UnityEngine;

public class UIChatGimmickGizmo : UIStatusGizmoBase
{
	[SerializeField]
	protected GameObject chatUI;

	[SerializeField]
	protected UILabel chatLabel;

	[SerializeField]
	protected TweenScale chatTween;

	[SerializeField]
	[Tooltip("スクリ\u30fcン横オフセット")]
	protected float screenSideOffset = 36f;

	[SerializeField]
	[Tooltip("スクリ\u30fcン下オフセット")]
	protected float screenBottomOffset = 107f;

	[SerializeField]
	[Tooltip("スクリ\u30fcン下オフセット、フィ\u30fcルド時")]
	protected float screenBottomFieldOffset = 107f;

	[SerializeField]
	[Tooltip("チャット横オフセット")]
	protected float chatSideOffset = 60f;

	[SerializeField]
	[Tooltip("チャット上オフセット")]
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
		if (chatUI != null)
		{
			chatTransform = chatUI.transform;
			chatUILocalPos = chatTransform.localPosition;
			chatUI.SetActive(value: false);
		}
		if (chatTween != null)
		{
			chatTween.SetOnFinished(OnFinishChat);
		}
	}

	protected override void UpdateParam()
	{
		if (!chatUI.activeSelf)
		{
			return;
		}
		Vector3 screenUIPosition = Utility.GetScreenUIPosition(MonoBehaviourSingleton<AppMain>.I.mainCamera, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform, chatGimmickObj.GetPosition());
		screenUIPosition.z = 0f;
		float num = Screen.width;
		float num2 = Screen.height;
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
		Vector3 vector = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screenUIPosition);
		Vector3 point = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(position);
		if ((transform.position - vector).sqrMagnitude >= 2E-05f)
		{
			transform.position = vector;
		}
		Vector3 localPosition = transform.worldToLocalMatrix.MultiplyPoint3x4(point);
		localPosition.y += chatUILocalPos.y;
		localPosition.z = 0f;
		chatTransform.localPosition = localPosition;
	}

	public void SayChat(string message)
	{
		chatLabel.text = message;
		chatUI.SetActive(value: true);
		if (chatTween != null)
		{
			chatTween.ResetToBeginning();
			chatTween.PlayForward();
		}
	}

	public void OnFinishChat()
	{
		chatUI.SetActive(value: false);
	}

	public bool isDisp()
	{
		return chatUI.activeSelf;
	}
}
