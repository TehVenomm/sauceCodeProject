using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[AddComponentMenu("Image Effects/Camera/Camera Motion Blur")]
	[RequireComponent(typeof(Camera))]
	[ExecuteInEditMode]
	public class CameraMotionBlur : PostEffectsBase
	{
		public enum MotionBlurFilter
		{
			CameraMotion,
			LocalBlur,
			Reconstruction,
			ReconstructionDX11,
			ReconstructionDisc
		}

		private static float MAX_RADIUS = 10f;

		public MotionBlurFilter filterType = MotionBlurFilter.Reconstruction;

		public bool preview;

		public Vector3 previewScale = Vector3.get_one();

		public float movementScale;

		public float rotationScale = 1f;

		public float maxVelocity = 8f;

		public float minVelocity = 0.1f;

		public float velocityScale = 0.375f;

		public float softZDistance = 0.005f;

		public int velocityDownsample = 1;

		public LayerMask excludeLayers = LayerMask.op_Implicit(0);

		private GameObject tmpCam;

		public Shader shader;

		public Shader dx11MotionBlurShader;

		public Shader replacementClear;

		private Material motionBlurMaterial;

		private Material dx11MotionBlurMaterial;

		public Texture2D noiseTexture;

		public float jitter = 0.05f;

		public bool showVelocity;

		public float showVelocityScale = 1f;

		private Matrix4x4 currentViewProjMat;

		private Matrix4x4 prevViewProjMat;

		private int prevFrameCount;

		private bool wasActive;

		private Vector3 prevFrameForward = Vector3.get_forward();

		private Vector3 prevFrameUp = Vector3.get_up();

		private Vector3 prevFramePos = Vector3.get_zero();

		private Camera _camera;

		private void CalculateViewProjection()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			Matrix4x4 worldToCameraMatrix = _camera.get_worldToCameraMatrix();
			Matrix4x4 gPUProjectionMatrix = GL.GetGPUProjectionMatrix(_camera.get_projectionMatrix(), true);
			currentViewProjMat = gPUProjectionMatrix * worldToCameraMatrix;
		}

		private new void Start()
		{
			CheckResources();
			if (_camera == null)
			{
				_camera = this.GetComponent<Camera>();
			}
			wasActive = this.get_gameObject().get_activeInHierarchy();
			CalculateViewProjection();
			Remember();
			wasActive = false;
		}

		private void OnEnable()
		{
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			if (_camera == null)
			{
				_camera = this.GetComponent<Camera>();
			}
			Camera camera = _camera;
			camera.set_depthTextureMode(camera.get_depthTextureMode() | 1);
		}

		private void OnDisable()
		{
			if (null != motionBlurMaterial)
			{
				Object.DestroyImmediate(motionBlurMaterial);
				motionBlurMaterial = null;
			}
			if (null != dx11MotionBlurMaterial)
			{
				Object.DestroyImmediate(dx11MotionBlurMaterial);
				dx11MotionBlurMaterial = null;
			}
			if (null != tmpCam)
			{
				Object.DestroyImmediate(tmpCam);
				tmpCam = null;
			}
		}

		public override bool CheckResources()
		{
			CheckSupport(needDepth: true, needHdr: true);
			motionBlurMaterial = CheckShaderAndCreateMaterial(shader, motionBlurMaterial);
			if (supportDX11 && filterType == MotionBlurFilter.ReconstructionDX11)
			{
				dx11MotionBlurMaterial = CheckShaderAndCreateMaterial(dx11MotionBlurShader, dx11MotionBlurMaterial);
			}
			if (!isSupported)
			{
				ReportAutoDisable();
			}
			return isSupported;
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0141: Unknown result type (might be due to invalid IL or missing references)
			//IL_014c: Unknown result type (might be due to invalid IL or missing references)
			//IL_01de: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0208: Unknown result type (might be due to invalid IL or missing references)
			//IL_021e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0223: Unknown result type (might be due to invalid IL or missing references)
			//IL_0225: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0302: Unknown result type (might be due to invalid IL or missing references)
			//IL_030c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0311: Unknown result type (might be due to invalid IL or missing references)
			//IL_0316: Unknown result type (might be due to invalid IL or missing references)
			//IL_0326: Unknown result type (might be due to invalid IL or missing references)
			//IL_032c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0331: Unknown result type (might be due to invalid IL or missing references)
			//IL_0334: Unknown result type (might be due to invalid IL or missing references)
			//IL_0336: Unknown result type (might be due to invalid IL or missing references)
			//IL_0338: Unknown result type (might be due to invalid IL or missing references)
			//IL_033d: Unknown result type (might be due to invalid IL or missing references)
			//IL_033f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0344: Unknown result type (might be due to invalid IL or missing references)
			//IL_0355: Unknown result type (might be due to invalid IL or missing references)
			//IL_036b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0370: Unknown result type (might be due to invalid IL or missing references)
			//IL_0372: Unknown result type (might be due to invalid IL or missing references)
			//IL_0387: Unknown result type (might be due to invalid IL or missing references)
			//IL_038c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0394: Unknown result type (might be due to invalid IL or missing references)
			//IL_0399: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0415: Unknown result type (might be due to invalid IL or missing references)
			//IL_041b: Unknown result type (might be due to invalid IL or missing references)
			//IL_045a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0460: Unknown result type (might be due to invalid IL or missing references)
			//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0502: Unknown result type (might be due to invalid IL or missing references)
			//IL_0532: Unknown result type (might be due to invalid IL or missing references)
			//IL_0537: Unknown result type (might be due to invalid IL or missing references)
			//IL_058f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0599: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_063f: Unknown result type (might be due to invalid IL or missing references)
			if (!CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (filterType == MotionBlurFilter.CameraMotion)
			{
				StartFrame();
			}
			RenderTextureFormat val = (!SystemInfo.SupportsRenderTextureFormat(13)) ? 2 : 13;
			RenderTexture temporary = RenderTexture.GetTemporary(divRoundUp(source.get_width(), velocityDownsample), divRoundUp(source.get_height(), velocityDownsample), 0, val);
			int num = 1;
			int num2 = 1;
			maxVelocity = Mathf.Max(2f, maxVelocity);
			float num3 = maxVelocity;
			bool flag = filterType == MotionBlurFilter.ReconstructionDX11 && dx11MotionBlurMaterial == null;
			if (filterType == MotionBlurFilter.Reconstruction || flag || filterType == MotionBlurFilter.ReconstructionDisc)
			{
				maxVelocity = Mathf.Min(maxVelocity, MAX_RADIUS);
				num = divRoundUp(temporary.get_width(), (int)maxVelocity);
				num2 = divRoundUp(temporary.get_height(), (int)maxVelocity);
				num3 = temporary.get_width() / num;
			}
			else
			{
				num = divRoundUp(temporary.get_width(), (int)maxVelocity);
				num2 = divRoundUp(temporary.get_height(), (int)maxVelocity);
				num3 = temporary.get_width() / num;
			}
			RenderTexture temporary2 = RenderTexture.GetTemporary(num, num2, 0, val);
			RenderTexture temporary3 = RenderTexture.GetTemporary(num, num2, 0, val);
			temporary.set_filterMode(0);
			temporary2.set_filterMode(0);
			temporary3.set_filterMode(0);
			if (Object.op_Implicit(noiseTexture))
			{
				noiseTexture.set_filterMode(0);
			}
			source.set_wrapMode(1);
			temporary.set_wrapMode(1);
			temporary3.set_wrapMode(1);
			temporary2.set_wrapMode(1);
			CalculateViewProjection();
			if (this.get_gameObject().get_activeInHierarchy() && !wasActive)
			{
				Remember();
			}
			wasActive = this.get_gameObject().get_activeInHierarchy();
			Matrix4x4 val2 = Matrix4x4.Inverse(currentViewProjMat);
			motionBlurMaterial.SetMatrix("_InvViewProj", val2);
			motionBlurMaterial.SetMatrix("_PrevViewProj", prevViewProjMat);
			motionBlurMaterial.SetMatrix("_ToPrevViewProjCombined", prevViewProjMat * val2);
			motionBlurMaterial.SetFloat("_MaxVelocity", num3);
			motionBlurMaterial.SetFloat("_MaxRadiusOrKInPaper", num3);
			motionBlurMaterial.SetFloat("_MinVelocity", minVelocity);
			motionBlurMaterial.SetFloat("_VelocityScale", velocityScale);
			motionBlurMaterial.SetFloat("_Jitter", jitter);
			motionBlurMaterial.SetTexture("_NoiseTex", noiseTexture);
			motionBlurMaterial.SetTexture("_VelTex", temporary);
			motionBlurMaterial.SetTexture("_NeighbourMaxTex", temporary3);
			motionBlurMaterial.SetTexture("_TileTexDebug", temporary2);
			if (preview)
			{
				Matrix4x4 worldToCameraMatrix = _camera.get_worldToCameraMatrix();
				Matrix4x4 identity = Matrix4x4.get_identity();
				identity.SetTRS(previewScale * 0.3333f, Quaternion.get_identity(), Vector3.get_one());
				Matrix4x4 gPUProjectionMatrix = GL.GetGPUProjectionMatrix(_camera.get_projectionMatrix(), true);
				prevViewProjMat = gPUProjectionMatrix * identity * worldToCameraMatrix;
				motionBlurMaterial.SetMatrix("_PrevViewProj", prevViewProjMat);
				motionBlurMaterial.SetMatrix("_ToPrevViewProjCombined", prevViewProjMat * val2);
			}
			if (filterType == MotionBlurFilter.CameraMotion)
			{
				Vector4 zero = Vector4.get_zero();
				float num4 = Vector3.Dot(this.get_transform().get_up(), Vector3.get_up());
				Vector3 val3 = prevFramePos - this.get_transform().get_position();
				float magnitude = val3.get_magnitude();
				float num5 = 1f;
				num5 = Vector3.Angle(this.get_transform().get_up(), prevFrameUp) / _camera.get_fieldOfView() * ((float)source.get_width() * 0.75f);
				zero.x = rotationScale * num5;
				num5 = Vector3.Angle(this.get_transform().get_forward(), prevFrameForward) / _camera.get_fieldOfView() * ((float)source.get_width() * 0.75f);
				zero.y = rotationScale * num4 * num5;
				num5 = Vector3.Angle(this.get_transform().get_forward(), prevFrameForward) / _camera.get_fieldOfView() * ((float)source.get_width() * 0.75f);
				zero.z = rotationScale * (1f - num4) * num5;
				if (magnitude > Mathf.Epsilon && movementScale > Mathf.Epsilon)
				{
					zero.w = movementScale * Vector3.Dot(this.get_transform().get_forward(), val3) * ((float)source.get_width() * 0.5f);
					zero.x += movementScale * Vector3.Dot(this.get_transform().get_up(), val3) * ((float)source.get_width() * 0.5f);
					zero.y += movementScale * Vector3.Dot(this.get_transform().get_right(), val3) * ((float)source.get_width() * 0.5f);
				}
				if (preview)
				{
					motionBlurMaterial.SetVector("_BlurDirectionPacked", new Vector4(previewScale.y, previewScale.x, 0f, previewScale.z) * 0.5f * _camera.get_fieldOfView());
				}
				else
				{
					motionBlurMaterial.SetVector("_BlurDirectionPacked", zero);
				}
			}
			else
			{
				Graphics.Blit(source, temporary, motionBlurMaterial, 0);
				Camera val4 = null;
				if (excludeLayers.get_value() != 0)
				{
					val4 = GetTmpCam();
				}
				if (Object.op_Implicit(val4) && excludeLayers.get_value() != 0 && Object.op_Implicit(replacementClear) && replacementClear.get_isSupported())
				{
					val4.set_targetTexture(temporary);
					val4.set_cullingMask(LayerMask.op_Implicit(excludeLayers));
					val4.RenderWithShader(replacementClear, string.Empty);
				}
			}
			if (!preview && Time.get_frameCount() != prevFrameCount)
			{
				prevFrameCount = Time.get_frameCount();
				Remember();
			}
			source.set_filterMode(1);
			if (showVelocity)
			{
				motionBlurMaterial.SetFloat("_DisplayVelocityScale", showVelocityScale);
				Graphics.Blit(temporary, destination, motionBlurMaterial, 1);
			}
			else if (filterType == MotionBlurFilter.ReconstructionDX11 && !flag)
			{
				dx11MotionBlurMaterial.SetFloat("_MinVelocity", minVelocity);
				dx11MotionBlurMaterial.SetFloat("_VelocityScale", velocityScale);
				dx11MotionBlurMaterial.SetFloat("_Jitter", jitter);
				dx11MotionBlurMaterial.SetTexture("_NoiseTex", noiseTexture);
				dx11MotionBlurMaterial.SetTexture("_VelTex", temporary);
				dx11MotionBlurMaterial.SetTexture("_NeighbourMaxTex", temporary3);
				dx11MotionBlurMaterial.SetFloat("_SoftZDistance", Mathf.Max(0.00025f, softZDistance));
				dx11MotionBlurMaterial.SetFloat("_MaxRadiusOrKInPaper", num3);
				Graphics.Blit(temporary, temporary2, dx11MotionBlurMaterial, 0);
				Graphics.Blit(temporary2, temporary3, dx11MotionBlurMaterial, 1);
				Graphics.Blit(source, destination, dx11MotionBlurMaterial, 2);
			}
			else if (filterType == MotionBlurFilter.Reconstruction || flag)
			{
				motionBlurMaterial.SetFloat("_SoftZDistance", Mathf.Max(0.00025f, softZDistance));
				Graphics.Blit(temporary, temporary2, motionBlurMaterial, 2);
				Graphics.Blit(temporary2, temporary3, motionBlurMaterial, 3);
				Graphics.Blit(source, destination, motionBlurMaterial, 4);
			}
			else if (filterType == MotionBlurFilter.CameraMotion)
			{
				Graphics.Blit(source, destination, motionBlurMaterial, 6);
			}
			else if (filterType == MotionBlurFilter.ReconstructionDisc)
			{
				motionBlurMaterial.SetFloat("_SoftZDistance", Mathf.Max(0.00025f, softZDistance));
				Graphics.Blit(temporary, temporary2, motionBlurMaterial, 2);
				Graphics.Blit(temporary2, temporary3, motionBlurMaterial, 3);
				Graphics.Blit(source, destination, motionBlurMaterial, 7);
			}
			else
			{
				Graphics.Blit(source, destination, motionBlurMaterial, 5);
			}
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(temporary2);
			RenderTexture.ReleaseTemporary(temporary3);
		}

		private void Remember()
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			prevViewProjMat = currentViewProjMat;
			prevFrameForward = this.get_transform().get_forward();
			prevFrameUp = this.get_transform().get_up();
			prevFramePos = this.get_transform().get_position();
		}

		private Camera GetTmpCam()
		{
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Expected O, but got Unknown
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			if (tmpCam == null)
			{
				string text = "_" + _camera.get_name() + "_MotionBlurTmpCam";
				GameObject val = GameObject.Find(text);
				if (null == val)
				{
					tmpCam = new GameObject(text, new Type[1]
					{
						typeof(Camera)
					});
				}
				else
				{
					tmpCam = val;
				}
			}
			tmpCam.set_hideFlags(52);
			tmpCam.get_transform().set_position(_camera.get_transform().get_position());
			tmpCam.get_transform().set_rotation(_camera.get_transform().get_rotation());
			tmpCam.get_transform().set_localScale(_camera.get_transform().get_localScale());
			tmpCam.GetComponent<Camera>().CopyFrom(_camera);
			tmpCam.GetComponent<Camera>().set_enabled(false);
			tmpCam.GetComponent<Camera>().set_depthTextureMode(0);
			tmpCam.GetComponent<Camera>().set_clearFlags(4);
			return tmpCam.GetComponent<Camera>();
		}

		private void StartFrame()
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			prevFramePos = Vector3.Slerp(prevFramePos, this.get_transform().get_position(), 0.75f);
		}

		private static int divRoundUp(int x, int d)
		{
			return (x + d - 1) / d;
		}
	}
}
