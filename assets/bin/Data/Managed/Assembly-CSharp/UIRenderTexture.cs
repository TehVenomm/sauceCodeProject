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

	private UIRenderTexture()
		: this()
	{
		nearClipPlane = -1f;
		farClipPlane = 500f;
		orthographicSize = 0f;
	}

	public static bool ToRealSize(ref int w, ref int h)
	{
		float num = Screen.get_width();
		float num2 = Screen.get_height();
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
			uIRenderTexture = ui_texture.get_gameObject().AddComponent<UIRenderTexture>();
		}
		uIRenderTexture.uiTexture = ui_texture;
		uIRenderTexture.fov = fov;
		uIRenderTexture.linkMainCamera = link_main_camera;
		uIRenderTexture.layer = layer;
		uIRenderTexture.Init();
		return uIRenderTexture;
	}

	private void Init()
	{
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		if (this.renderTransform != null)
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
		this.renderTransform = Utility.CreateGameObject("RenderTextureNode:" + id, null, renderLayer);
		int num2 = id >> 2;
		int num3 = (id + 1) & 3;
		Transform renderTransform = this.renderTransform;
		_003F localPosition;
		if (!linkMainCamera && layer == -1)
		{
			float num4 = num2 * 50;
			float num5 = num3 * -50;
			Vector3 position = MonoBehaviourSingleton<UIManager>.I._transform.get_position();
			localPosition = new Vector3(num4, num5 + position.y, 0f);
		}
		else
		{
			localPosition = Vector3.get_zero();
		}
		renderTransform.set_localPosition(localPosition);
		this.renderTransform.set_parent(MonoBehaviourSingleton<UIManager>.I._transform);
		this.renderTransform.set_localScale(Vector3.get_one());
		modelTransform = Utility.CreateGameObject("ModelNode", null, renderLayer);
		modelTransform.set_parent(this.renderTransform);
		modelTransform.set_localPosition(Vector3.get_zero());
		modelTransform.set_localEulerAngles(Vector3.get_zero());
	}

	public void Release()
	{
		Disable();
		if (!AppMain.isApplicationQuit && renderTransform != null)
		{
			Object.Destroy(renderTransform.get_gameObject());
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
			renderCamera.set_enabled(true);
			Utility.SetLayerWithChildren(renderCamera.get_transform(), renderLayer);
		}
	}

	private void OnDisable()
	{
		if (renderCamera != null)
		{
			renderCamera.set_enabled(false);
		}
	}

	private void OnDestroy()
	{
		this.StopAllCoroutines();
		Release();
	}

	private void CreateRenderTexture()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		if (renderCamera.get_targetTexture() == null)
		{
			RenderTexture val = new RenderTexture(texW, texH, 24);
			val.set_name("(UIRenderTexture)");
			val.set_filterMode(filterMode);
			val.Create();
			renderCamera.set_targetTexture(val);
			uiTexture.mainTexture = val;
		}
	}

	private void DeleteRenderTexture()
	{
		if (renderCamera.get_targetTexture() != null)
		{
			renderCamera.get_targetTexture().DiscardContents();
			Object.Destroy(renderCamera.get_targetTexture());
			renderCamera.set_targetTexture(null);
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
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		Init();
		if ((layer == -1 && id == -1) || renderCamera != null)
		{
			return;
		}
		renderCamera = renderTransform.get_gameObject().AddComponent<Camera>();
		renderCamera.set_depth(50f);
		renderCamera.set_clearFlags(2);
		renderCamera.set_backgroundColor(new Color(0f, 0f, 0f, 0f));
		renderCamera.set_renderingPath(1);
		renderCamera.set_cullingMask(1 << renderLayer);
		if (orthographicSize == 0f)
		{
			if (fov <= 0f)
			{
				fov = 10f;
			}
			renderCamera.set_fieldOfView(fov);
		}
		else
		{
			renderCamera.set_orthographic(true);
			renderCamera.set_orthographicSize(orthographicSize);
		}
		if (nearClipPlane == -1f)
		{
			nearClipPlane = 0.01f;
		}
		renderCamera.set_nearClipPlane(nearClipPlane);
		renderCamera.set_farClipPlane(farClipPlane);
		if (postEffectFilter != null)
		{
			postEffector = renderTransform.get_gameObject().AddComponent<PostEffector>();
			postEffector.SetFilter(postEffectFilter);
		}
		if (uiTexture != null)
		{
			texW = uiTexture.width;
			texH = uiTexture.height;
			if (ToRealSize(ref texW, ref texH))
			{
				filterMode = 1;
			}
			else
			{
				filterMode = 0;
			}
		}
		else
		{
			texW = (texH = Mathf.Min(Screen.get_width(), Screen.get_height()));
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
			Object.Destroy(renderCamera);
			renderCamera = null;
			Object.Destroy(postEffector);
			postEffector = null;
			alpha = null;
			uiTexture.alpha = 0f;
		}
	}

	public void FadeOutDisable(float fadeTime = 0.25f)
	{
		this.StartCoroutine(DoFadeOutDisable(fadeTime));
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
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
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
			modelTransform.set_parent(null);
			renderTransform.set_position(MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.get_position());
			renderTransform.set_rotation(MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.get_rotation());
			modelTransform.set_parent(renderTransform);
			renderCamera.set_fieldOfView(MonoBehaviourSingleton<AppMain>.I.mainCamera.get_fieldOfView());
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
			RenderTexture val = uiTexture.mainTexture as RenderTexture;
			if (val != null)
			{
				renderCamera.set_targetTexture(val);
			}
		}
	}
}
