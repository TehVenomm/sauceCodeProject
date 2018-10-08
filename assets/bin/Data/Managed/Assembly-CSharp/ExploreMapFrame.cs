using System;
using UnityEngine;

public class ExploreMapFrame
{
	private static readonly int MAP_HORIZONTAL_MARGIN_FOR_OUTER_FRAME = 6;

	[SerializeField]
	private UIWidget frameWidget;

	[SerializeField]
	private UIWidget mapFrameTop;

	[SerializeField]
	private UIWidget mapFrameBottom;

	[SerializeField]
	private UILabel captionLabel;

	private ExploreMapRoot mapRoot;

	private UIScreenRotationHandler[] rotationHandler;

	public ExploreMapFrame()
		: this()
	{
	}

	private void Awake()
	{
		rotationHandler = this.GetComponentsInChildren<UIScreenRotationHandler>(true);
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			OnScreenRotate(true);
		}
	}

	private void OnEnable()
	{
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
		}
	}

	private void OnDisable()
	{
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
	}

	public void OnScreenRotate(bool isPortrait)
	{
		for (int i = 0; i < rotationHandler.Length; i++)
		{
			rotationHandler[i].InvokeRotate();
		}
		AppMain i2 = MonoBehaviourSingleton<AppMain>.I;
		i2.onDelayCall = (Action)Delegate.Combine(i2.onDelayCall, (Action)delegate
		{
			UpdateMap();
		});
	}

	public void SetMap(ExploreMapRoot map)
	{
		mapRoot = map;
		UpdateMap();
	}

	public void SetCaption(string text)
	{
		if (null != captionLabel)
		{
			captionLabel.text = text;
		}
	}

	private void UpdateMap()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		if (!(mapRoot == null))
		{
			if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
			{
				float mapScale = mapRoot.GetMapScale();
				mapRoot.get_gameObject().get_transform().set_localScale(Vector2.op_Implicit(new Vector2(mapScale, mapScale)));
				Vector2 sonarScale = mapRoot.GetSonarScale();
				if (mapRoot.directionSonar != null)
				{
					mapRoot.directionSonar.get_transform().set_localScale(Vector2.op_Implicit(sonarScale));
				}
			}
			Rect mapAreaRect = CalcMapAreaRect();
			UpdateMapCenter(mapAreaRect);
			UpdateMapVisibleArea(mapAreaRect);
		}
	}

	private Rect CalcMapAreaRect()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		Vector3 localPosition = mapFrameTop.cachedTransform.get_localPosition();
		Vector3 localPosition2 = mapFrameBottom.cachedTransform.get_localPosition();
		float num = (float)frameWidget.width - (float)(MAP_HORIZONTAL_MARGIN_FOR_OUTER_FRAME * 2);
		float num2 = localPosition.y - localPosition2.y;
		Vector2 val = new Vector2(localPosition.x + localPosition2.x, localPosition.y + localPosition2.y) * 0.5f;
		float num3 = num * 0.5f;
		float num4 = num2 * 0.5f;
		float num5 = val.x - num3;
		float num6 = val.y - num4;
		return new Rect(num5, num6, num, num2);
	}

	private void UpdateMapCenter(Rect mapAreaRect)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		mapRoot.get_transform().set_localPosition(Utility.ToVector3XY(mapAreaRect.get_center()));
	}

	private void UpdateMapVisibleArea(Rect mapAreaRect)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		UITexture mapTexture = mapRoot.mapTexture;
		Vector3 localScale = mapTexture.cachedTransform.get_localScale();
		float x = localScale.x;
		Vector3 localScale2 = mapRoot.get_gameObject().get_transform().get_localScale();
		float num = x * localScale2.x;
		float num2 = (float)mapTexture.width * num;
		float num3 = (float)mapTexture.height * num;
		float num4 = (num2 - mapAreaRect.get_width()) / num * 0.5f;
		float num5 = (num3 - mapAreaRect.get_height()) / num * 0.5f;
		Vector3 localPosition = mapTexture.cachedTransform.get_localPosition();
		mapRoot.mapTexture.border = new Vector4(num4 - localPosition.x, num5 - localPosition.y, num4 + localPosition.x, num5 + localPosition.y);
	}
}
