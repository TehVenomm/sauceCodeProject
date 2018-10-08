using System;
using UnityEngine;

public class ExploreMapFrame : MonoBehaviour
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

	private void Awake()
	{
		rotationHandler = GetComponentsInChildren<UIScreenRotationHandler>(true);
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
		if ((UnityEngine.Object)null != (UnityEngine.Object)captionLabel)
		{
			captionLabel.text = text;
		}
	}

	private void UpdateMap()
	{
		if (!((UnityEngine.Object)mapRoot == (UnityEngine.Object)null))
		{
			if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
			{
				float mapScale = mapRoot.GetMapScale();
				mapRoot.gameObject.transform.localScale = new Vector2(mapScale, mapScale);
				Vector2 sonarScale = mapRoot.GetSonarScale();
				if ((UnityEngine.Object)mapRoot.directionSonar != (UnityEngine.Object)null)
				{
					mapRoot.directionSonar.transform.localScale = sonarScale;
				}
			}
			Rect mapAreaRect = CalcMapAreaRect();
			UpdateMapCenter(mapAreaRect);
			UpdateMapVisibleArea(mapAreaRect);
		}
	}

	private Rect CalcMapAreaRect()
	{
		Vector3 localPosition = mapFrameTop.cachedTransform.localPosition;
		Vector3 localPosition2 = mapFrameBottom.cachedTransform.localPosition;
		float num = (float)frameWidget.width - (float)(MAP_HORIZONTAL_MARGIN_FOR_OUTER_FRAME * 2);
		float num2 = localPosition.y - localPosition2.y;
		Vector2 vector = new Vector2(localPosition.x + localPosition2.x, localPosition.y + localPosition2.y) * 0.5f;
		float num3 = num * 0.5f;
		float num4 = num2 * 0.5f;
		float x = vector.x - num3;
		float y = vector.y - num4;
		return new Rect(x, y, num, num2);
	}

	private void UpdateMapCenter(Rect mapAreaRect)
	{
		mapRoot.transform.localPosition = mapAreaRect.center.ToVector3XY();
	}

	private void UpdateMapVisibleArea(Rect mapAreaRect)
	{
		UITexture mapTexture = mapRoot.mapTexture;
		Vector3 localScale = mapTexture.cachedTransform.localScale;
		float x = localScale.x;
		Vector3 localScale2 = mapRoot.gameObject.transform.localScale;
		float num = x * localScale2.x;
		float num2 = (float)mapTexture.width * num;
		float num3 = (float)mapTexture.height * num;
		float num4 = (num2 - mapAreaRect.width) / num * 0.5f;
		float num5 = (num3 - mapAreaRect.height) / num * 0.5f;
		Vector3 localPosition = mapTexture.cachedTransform.localPosition;
		mapRoot.mapTexture.border = new Vector4(num4 - localPosition.x, num5 - localPosition.y, num4 + localPosition.x, num5 + localPosition.y);
	}
}
