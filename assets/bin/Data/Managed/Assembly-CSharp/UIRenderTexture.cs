using System;
using System.Collections;
using UnityEngine;

public class UIRenderTexture : MonoBehaviour
{
	public const int BEGIN_LAYER = 24;

	private const int ID_MAX = 16;

	private const int CAMERA_DEPTH = 50;

	private static int idFlags;

	private int layer = -1;

	private int id = -1;

	private int texW;

	private int texH;

	private FilterMode filterMode;

	private FloatInterpolator alpha;

	public UITexture uiTexture
	{
		get;
		private set;
	}

	public float fov
	{
		get;
		private set;
	}

	public float nearClipPlane
	{
		get;
		set;
	}

	public float farClipPlane
	{
		get;
		set;
	}

	public float orthographicSize
	{
		get;
		set;
	}

	public bool linkMainCamera
	{
		get;
		set;
	}

	public Transform renderTransform
	{
		get;
		private set;
	}

	public Transform modelTransform
	{
		get;
		private set;
	}

	public Camera renderCamera
	{
		get;
		private set;
	}

	public FilterBase postEffectFilter
	{
		get;
		set;
	}

	public PostEffector postEffector
	{
		get;
		private set;
	}

	public int renderLayer
	{
		get
		{
			if (layer != -1)
			{
				return layer;
			}
			if (id == -1)
			{
				return 0;
			}
			return 24 + (id & 3);
		}
	}

	public bool enableTexture
	{
		get
		{
			return renderCamera != null;
		}
		set
		{
			if (value)
			{
				Enable();
			}
			else
			{
				Disable();
			}
		}
	}

	public static bool ToRealSize(ref int w, ref int h)
	{
		float num = Screen.width;
		float num2 = Screen.height;
		if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.HasSafeArea)
		{
			DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
			num = specialDeviceInfo.SafeArea.SafeWidth;
			num2 = specialDeviceInfo.SafeArea.SafeHeight;
		}
		float num3 = num / (float)MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth * (float)w;
		float num4 = num2 / (float)MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight * (float)h;
		bool result = false;
		if (num3 > num + 4f)
		{
			num4 *= num / num3;
			num3 = num;
			result = true;
		}
		if (num4 > num2 + 4f)
		{
			num3 *= num2 / num4;
			num4 = num2;
			result = true;
		}
		w = Mathf.RoundToInt(num3);
		h = Mathf.RoundToInt(num4);
		return result;
	}

	public static UIRenderTexture Get(UITexture ui_texture, float fov = -1f, bool link_main_camera = false, int layer = -1)
	{
		if (ui_texture == null)
		{
			return null;
		}
		UIRenderTexture uIRenderTexture = ui_texture.GetComponent<UIRenderTexture>();
		if (uIRenderTexture == null)
		{
			uIRenderTexture = ui_texture.gameObject.AddComponent<UIRenderTexture>();
		}
		uIRenderTexture.uiTexture = ui_texture;
		uIRenderTexture.fov = fov;
		uIRenderTexture.linkMainCamera = link_main_camera;
		uIRenderTexture.layer = layer;
		uIRenderTexture.Init();
		return uIRenderTexture;
	}

	private UIRenderTexture()
	{
		nearClipPlane = -1f;
		farClipPlane = 500f;
		orthographicSize = 0f;
	}

	private void Init()
	{
		if (renderTransform != null)
		{
			return;
		}
		if (layer == -1)
		{
			if (id != -1)
			{
				return;
			}
			for (int i = 0; i < 16; i++)
			{
				int num = 1 << i;
				if ((idFlags & num) == 0)
				{
					idFlags |= num;
					id = i;
					break;
				}
			}
			if (id == -1)
			{
				return;
			}
		}
		renderTransform = Utility.CreateGameObject("RenderTextureNode:" + id, null, renderLayer);
		int num2 = id >> 2;
		int num3 = (id + 1) & 3;
		renderTransform.localPosition = ((!linkMainCamera && layer == -1) ? new Vector3(num2 * 50, (float)(num3 * -50) + MonoBehaviourSingleton<UIManager>.I._transform.position.y, 0f) : Vector3.zero);
		renderTransform.parent = MonoBehaviourSingleton<UIManager>.I._transform;
		renderTransform.localScale = Vector3.one;
		modelTransform = Utility.CreateGameObject("ModelNode", null, renderLayer);
		modelTransform.parent = renderTransform;
		modelTransform.localPosition = Vector3.zero;
		modelTransform.localEulerAngles = Vector3.zero;
	}

	public void Release()
	{
		Disable();
		if (!AppMain.isApplicationQuit && renderTransform != null)
		{
			UnityEngine.Object.Destroy(renderTransform.gameObject);
			renderTransform = null;
		}
		if (id != -1)
		{
			idFlags &= ~(1 << id);
			id = -1;
		}
	}

	private void OnEnable()
	{
		if (renderCamera != null)
		{
			renderCamera.enabled = true;
			Utility.SetLayerWithChildren(renderCamera.transform, renderLayer);
		}
	}

	private void OnDisable()
	{
		if (renderCamera != null)
		{
			renderCamera.enabled = false;
		}
	}

	private void OnDestroy()
	{
		StopAllCoroutines();
		Release();
	}

	private void CreateRenderTexture()
	{
		if (renderCamera.targetTexture == null)
		{
			RenderTexture renderTexture = new RenderTexture(texW, texH, 24);
			renderTexture.name = "(UIRenderTexture)";
			renderTexture.filterMode = filterMode;
			renderTexture.Create();
			renderCamera.targetTexture = renderTexture;
			uiTexture.mainTexture = renderTexture;
		}
	}

	private void DeleteRenderTexture()
	{
		if (renderCamera.targetTexture != null)
		{
			renderCamera.targetTexture.DiscardContents();
			UnityEngine.Object.Destroy(renderCamera.targetTexture);
			renderCamera.targetTexture = null;
			UIPanel panel = uiTexture.panel;
			uiTexture.mainTexture = null;
			if (uiTexture.drawCall != null)
			{
				uiTexture.drawCall.panel.drawCalls.Remove(uiTexture.drawCall);
				UIDrawCall.Destroy(uiTexture.drawCall);
				uiTexture.drawCall = null;
			}
			else if (panel != null)
			{
				panel.ForceUpDate();
			}
		}
	}

	public void Enable(float fadeTime = 0.25f)
	{
		Init();
		if ((layer == -1 && id == -1) || renderCamera != null)
		{
			return;
		}
		renderCamera = renderTransform.gameObject.AddComponent<Camera>();
		renderCamera.depth = 50f;
		renderCamera.clearFlags = CameraClearFlags.Color;
		renderCamera.backgroundColor = new Color(0f, 0f, 0f, 0f);
		renderCamera.renderingPath = RenderingPath.Forward;
		renderCamera.cullingMask = 1 << renderLayer;
		if (orthographicSize == 0f)
		{
			if (fov <= 0f)
			{
				fov = 10f;
			}
			renderCamera.fieldOfView = fov;
		}
		else
		{
			renderCamera.orthographic = true;
			renderCamera.orthographicSize = orthographicSize;
		}
		if (nearClipPlane == -1f)
		{
			nearClipPlane = 0.01f;
		}
		renderCamera.nearClipPlane = nearClipPlane;
		renderCamera.farClipPlane = farClipPlane;
		if (postEffectFilter != null)
		{
			postEffector = renderTransform.gameObject.AddComponent<PostEffector>();
			postEffector.SetFilter(postEffectFilter);
		}
		if (uiTexture != null)
		{
			texW = uiTexture.width;
			texH = uiTexture.height;
			if (ToRealSize(ref texW, ref texH))
			{
				filterMode = FilterMode.Bilinear;
			}
			else
			{
				filterMode = FilterMode.Point;
			}
		}
		else
		{
			texW = (texH = Mathf.Min(Screen.width, Screen.height));
		}
		CreateRenderTexture();
		uiTexture.alpha = 0f;
		alpha = new FloatInterpolator();
		alpha.Set(fadeTime, 0f, 1f, Curves.easeLinear, 0f);
		alpha.Play();
		Nexus6CrashWorkaround.Apply(renderCamera);
	}

	public void Disable()
	{
		if ((layer != -1 || id != -1) && !(renderCamera == null))
		{
			DeleteRenderTexture();
			UnityEngine.Object.Destroy(renderCamera);
			renderCamera = null;
			UnityEngine.Object.Destroy(postEffector);
			postEffector = null;
			alpha = null;
			uiTexture.alpha = 0f;
		}
	}

	public void FadeOutDisable(float fadeTime = 0.25f)
	{
		StartCoroutine(DoFadeOutDisable(fadeTime));
	}

	private IEnumerator DoFadeOutDisable(float fadeTime)
	{
		uiTexture.alpha = 1f;
		alpha = new FloatInterpolator();
		alpha.Set(fadeTime, 1f, 0f, Curves.easeLinear, 0f);
		alpha.Play();
		while (alpha.IsPlaying())
		{
			yield return null;
			if (alpha == null)
			{
				break;
			}
		}
		Disable();
	}

	private void LateUpdate()
	{
		if (layer == -1 && id == -1)
		{
			return;
		}
		if (alpha != null)
		{
			uiTexture.alpha = alpha.Update();
			if (!alpha.IsPlaying())
			{
				alpha = null;
			}
		}
		if (linkMainCamera && renderCamera != null)
		{
			modelTransform.parent = null;
			renderTransform.position = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position;
			renderTransform.rotation = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.rotation;
			modelTransform.parent = renderTransform;
			renderCamera.fieldOfView = MonoBehaviourSingleton<AppMain>.I.mainCamera.fieldOfView;
		}
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (!pauseStatus && MonoBehaviourSingleton<AppMain>.IsValid())
		{
			AppMain i = MonoBehaviourSingleton<AppMain>.I;
			i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, new Action(CheckRenderCameraTarget));
		}
	}

	private void CheckRenderCameraTarget()
	{
		if (uiTexture != null && renderCamera != null && uiTexture.mainTexture != null)
		{
			RenderTexture renderTexture = uiTexture.mainTexture as RenderTexture;
			if (renderTexture != null)
			{
				renderCamera.targetTexture = renderTexture;
			}
		}
	}
}
