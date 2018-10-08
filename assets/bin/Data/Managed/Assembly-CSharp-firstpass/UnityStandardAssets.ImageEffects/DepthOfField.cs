using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Camera/Depth of Field (Lens Blur, Scatter, DX11)")]
	[ExecuteInEditMode]
	public class DepthOfField : PostEffectsBase
	{
		public enum BlurType
		{
			DiscBlur,
			DX11
		}

		public enum BlurSampleCount
		{
			Low,
			Medium,
			High
		}

		public bool visualizeFocus;

		public float focalLength = 10f;

		public float focalSize = 0.05f;

		public float aperture = 0.5f;

		public Transform focalTransform;

		public float maxBlurSize = 2f;

		public bool highResolution;

		public BlurType blurType;

		public BlurSampleCount blurSampleCount = BlurSampleCount.High;

		public bool nearBlur;

		public float foregroundOverlap = 1f;

		public Shader dofHdrShader;

		private Material dofHdrMaterial;

		public Shader dx11BokehShader;

		private Material dx11bokehMaterial;

		public float dx11BokehThreshold = 0.5f;

		public float dx11SpawnHeuristic = 0.0875f;

		public Texture2D dx11BokehTexture;

		public float dx11BokehScale = 1.2f;

		public float dx11BokehIntensity = 2.5f;

		private float focalDistance01 = 10f;

		private ComputeBuffer cbDrawArgs;

		private ComputeBuffer cbPoints;

		private float internalBlurWidth = 1f;

		private Camera cachedCamera;

		public override bool CheckResources()
		{
			CheckSupport(true);
			dofHdrMaterial = CheckShaderAndCreateMaterial(dofHdrShader, dofHdrMaterial);
			if (supportDX11 && blurType == BlurType.DX11)
			{
				dx11bokehMaterial = CheckShaderAndCreateMaterial(dx11BokehShader, dx11bokehMaterial);
				CreateComputeResources();
			}
			if (!isSupported)
			{
				ReportAutoDisable();
			}
			return isSupported;
		}

		private void OnEnable()
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			cachedCamera = this.GetComponent<Camera>();
			Camera obj = cachedCamera;
			obj.set_depthTextureMode(obj.get_depthTextureMode() | 1);
		}

		private void OnDisable()
		{
			ReleaseComputeResources();
			if (Object.op_Implicit(dofHdrMaterial))
			{
				Object.DestroyImmediate(dofHdrMaterial);
			}
			dofHdrMaterial = null;
			if (Object.op_Implicit(dx11bokehMaterial))
			{
				Object.DestroyImmediate(dx11bokehMaterial);
			}
			dx11bokehMaterial = null;
		}

		private void ReleaseComputeResources()
		{
			if (cbDrawArgs != null)
			{
				cbDrawArgs.Release();
			}
			cbDrawArgs = null;
			if (cbPoints != null)
			{
				cbPoints.Release();
			}
			cbPoints = null;
		}

		private void CreateComputeResources()
		{
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Expected O, but got Unknown
			if (cbDrawArgs == null)
			{
				cbDrawArgs = new ComputeBuffer(1, 16, 256);
				int[] data = new int[4]
				{
					0,
					1,
					0,
					0
				};
				cbDrawArgs.SetData((Array)data);
			}
			if (cbPoints == null)
			{
				cbPoints = new ComputeBuffer(90000, 28, 2);
			}
		}

		private float FocalDistance01(float worldDist)
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			Vector3 val = cachedCamera.WorldToViewportPoint((worldDist - cachedCamera.get_nearClipPlane()) * cachedCamera.get_transform().get_forward() + cachedCamera.get_transform().get_position());
			return val.z / (cachedCamera.get_farClipPlane() - cachedCamera.get_nearClipPlane());
		}

		private void WriteCoc(RenderTexture fromTo, bool fgDilate)
		{
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Expected O, but got Unknown
			//IL_0076: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Unknown result type (might be due to invalid IL or missing references)
			//IL_008e: Expected O, but got Unknown
			//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d4: Expected O, but got Unknown
			dofHdrMaterial.SetTexture("_FgOverlap", null);
			if (nearBlur && fgDilate)
			{
				int num = fromTo.get_width() / 2;
				int num2 = fromTo.get_height() / 2;
				RenderTexture val = RenderTexture.GetTemporary(num, num2, 0, fromTo.get_format());
				Graphics.Blit(fromTo, val, dofHdrMaterial, 4);
				float num3 = internalBlurWidth * foregroundOverlap;
				dofHdrMaterial.SetVector("_Offsets", new Vector4(0f, num3, 0f, num3));
				RenderTexture val2 = RenderTexture.GetTemporary(num, num2, 0, fromTo.get_format());
				Graphics.Blit(val, val2, dofHdrMaterial, 2);
				RenderTexture.ReleaseTemporary(val);
				dofHdrMaterial.SetVector("_Offsets", new Vector4(num3, 0f, 0f, num3));
				val = RenderTexture.GetTemporary(num, num2, 0, fromTo.get_format());
				Graphics.Blit(val2, val, dofHdrMaterial, 2);
				RenderTexture.ReleaseTemporary(val2);
				dofHdrMaterial.SetTexture("_FgOverlap", val);
				fromTo.MarkRestoreExpected();
				Graphics.Blit(fromTo, fromTo, dofHdrMaterial, 13);
				RenderTexture.ReleaseTemporary(val);
			}
			else
			{
				fromTo.MarkRestoreExpected();
				Graphics.Blit(fromTo, fromTo, dofHdrMaterial, 0);
			}
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			//IL_0097: Unknown result type (might be due to invalid IL or missing references)
			//IL_009c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0100: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bc: Expected O, but got Unknown
			//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d5: Expected O, but got Unknown
			//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fb: Expected O, but got Unknown
			//IL_020e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0213: Unknown result type (might be due to invalid IL or missing references)
			//IL_0218: Expected O, but got Unknown
			//IL_0247: Unknown result type (might be due to invalid IL or missing references)
			//IL_027f: Unknown result type (might be due to invalid IL or missing references)
			//IL_030a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0379: Unknown result type (might be due to invalid IL or missing references)
			//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0401: Unknown result type (might be due to invalid IL or missing references)
			//IL_043a: Unknown result type (might be due to invalid IL or missing references)
			//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_052b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0530: Unknown result type (might be due to invalid IL or missing references)
			//IL_0535: Expected O, but got Unknown
			//IL_0548: Unknown result type (might be due to invalid IL or missing references)
			//IL_054d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0552: Expected O, but got Unknown
			//IL_0591: Unknown result type (might be due to invalid IL or missing references)
			//IL_0596: Unknown result type (might be due to invalid IL or missing references)
			//IL_059b: Expected O, but got Unknown
			//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b8: Expected O, but got Unknown
			//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_061f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0658: Unknown result type (might be due to invalid IL or missing references)
			//IL_065d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0662: Expected O, but got Unknown
			//IL_06cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0737: Unknown result type (might be due to invalid IL or missing references)
			//IL_0769: Unknown result type (might be due to invalid IL or missing references)
			//IL_07b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_07eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_087e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0883: Unknown result type (might be due to invalid IL or missing references)
			//IL_08f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_08fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_096b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0970: Unknown result type (might be due to invalid IL or missing references)
			//IL_0975: Expected O, but got Unknown
			//IL_0988: Unknown result type (might be due to invalid IL or missing references)
			//IL_098d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0992: Expected O, but got Unknown
			//IL_09e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a21: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a75: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a95: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aa0: Unknown result type (might be due to invalid IL or missing references)
			if (!CheckResources())
			{
				Graphics.Blit(source, destination);
			}
			else
			{
				if (aperture < 0f)
				{
					aperture = 0f;
				}
				if (maxBlurSize < 0.1f)
				{
					maxBlurSize = 0.1f;
				}
				focalSize = Mathf.Clamp(focalSize, 0f, 2f);
				internalBlurWidth = Mathf.Max(maxBlurSize, 0f);
				float num;
				if (Object.op_Implicit(focalTransform))
				{
					Vector3 val = cachedCamera.WorldToViewportPoint(focalTransform.get_position());
					num = val.z / cachedCamera.get_farClipPlane();
				}
				else
				{
					num = FocalDistance01(focalLength);
				}
				focalDistance01 = num;
				dofHdrMaterial.SetVector("_CurveParams", new Vector4(1f, focalSize, 1f / (1f - aperture) - 1f, focalDistance01));
				RenderTexture val2 = null;
				RenderTexture val3 = null;
				RenderTexture val4 = null;
				RenderTexture val5 = null;
				float num2 = internalBlurWidth * foregroundOverlap;
				if (visualizeFocus)
				{
					WriteCoc(source, true);
					Graphics.Blit(source, destination, dofHdrMaterial, 16);
				}
				else if (blurType == BlurType.DX11 && Object.op_Implicit(dx11bokehMaterial))
				{
					if (highResolution)
					{
						internalBlurWidth = ((!(internalBlurWidth < 0.1f)) ? internalBlurWidth : 0.1f);
						num2 = internalBlurWidth * foregroundOverlap;
						val2 = RenderTexture.GetTemporary(source.get_width(), source.get_height(), 0, source.get_format());
						RenderTexture val6 = RenderTexture.GetTemporary(source.get_width(), source.get_height(), 0, source.get_format());
						WriteCoc(source, false);
						val4 = RenderTexture.GetTemporary(source.get_width() >> 1, source.get_height() >> 1, 0, source.get_format());
						val5 = RenderTexture.GetTemporary(source.get_width() >> 1, source.get_height() >> 1, 0, source.get_format());
						Graphics.Blit(source, val4, dofHdrMaterial, 15);
						dofHdrMaterial.SetVector("_Offsets", new Vector4(0f, 1.5f, 0f, 1.5f));
						Graphics.Blit(val4, val5, dofHdrMaterial, 19);
						dofHdrMaterial.SetVector("_Offsets", new Vector4(1.5f, 0f, 0f, 1.5f));
						Graphics.Blit(val5, val4, dofHdrMaterial, 19);
						if (nearBlur)
						{
							Graphics.Blit(source, val5, dofHdrMaterial, 4);
						}
						dx11bokehMaterial.SetTexture("_BlurredColor", val4);
						dx11bokehMaterial.SetFloat("_SpawnHeuristic", dx11SpawnHeuristic);
						dx11bokehMaterial.SetVector("_BokehParams", new Vector4(dx11BokehScale, dx11BokehIntensity, Mathf.Clamp(dx11BokehThreshold, 0.005f, 4f), internalBlurWidth));
						dx11bokehMaterial.SetTexture("_FgCocMask", (!nearBlur) ? null : val5);
						Graphics.SetRandomWriteTarget(1, cbPoints);
						Graphics.Blit(source, val2, dx11bokehMaterial, 0);
						Graphics.ClearRandomWriteTargets();
						if (nearBlur)
						{
							dofHdrMaterial.SetVector("_Offsets", new Vector4(0f, num2, 0f, num2));
							Graphics.Blit(val5, val4, dofHdrMaterial, 2);
							dofHdrMaterial.SetVector("_Offsets", new Vector4(num2, 0f, 0f, num2));
							Graphics.Blit(val4, val5, dofHdrMaterial, 2);
							Graphics.Blit(val5, val2, dofHdrMaterial, 3);
						}
						Graphics.Blit(val2, val6, dofHdrMaterial, 20);
						dofHdrMaterial.SetVector("_Offsets", new Vector4(internalBlurWidth, 0f, 0f, internalBlurWidth));
						Graphics.Blit(val2, source, dofHdrMaterial, 5);
						dofHdrMaterial.SetVector("_Offsets", new Vector4(0f, internalBlurWidth, 0f, internalBlurWidth));
						Graphics.Blit(source, val6, dofHdrMaterial, 21);
						Graphics.SetRenderTarget(val6);
						ComputeBuffer.CopyCount(cbPoints, cbDrawArgs, 0);
						dx11bokehMaterial.SetBuffer("pointBuffer", cbPoints);
						dx11bokehMaterial.SetTexture("_MainTex", dx11BokehTexture);
						dx11bokehMaterial.SetVector("_Screen", Vector4.op_Implicit(new Vector3(1f / (1f * (float)source.get_width()), 1f / (1f * (float)source.get_height()), internalBlurWidth)));
						dx11bokehMaterial.SetPass(2);
						Graphics.DrawProceduralIndirect(5, cbDrawArgs, 0);
						Graphics.Blit(val6, destination);
						RenderTexture.ReleaseTemporary(val6);
						RenderTexture.ReleaseTemporary(val4);
						RenderTexture.ReleaseTemporary(val5);
					}
					else
					{
						val2 = RenderTexture.GetTemporary(source.get_width() >> 1, source.get_height() >> 1, 0, source.get_format());
						val3 = RenderTexture.GetTemporary(source.get_width() >> 1, source.get_height() >> 1, 0, source.get_format());
						num2 = internalBlurWidth * foregroundOverlap;
						WriteCoc(source, false);
						source.set_filterMode(1);
						Graphics.Blit(source, val2, dofHdrMaterial, 6);
						val4 = RenderTexture.GetTemporary(val2.get_width() >> 1, val2.get_height() >> 1, 0, val2.get_format());
						val5 = RenderTexture.GetTemporary(val2.get_width() >> 1, val2.get_height() >> 1, 0, val2.get_format());
						Graphics.Blit(val2, val4, dofHdrMaterial, 15);
						dofHdrMaterial.SetVector("_Offsets", new Vector4(0f, 1.5f, 0f, 1.5f));
						Graphics.Blit(val4, val5, dofHdrMaterial, 19);
						dofHdrMaterial.SetVector("_Offsets", new Vector4(1.5f, 0f, 0f, 1.5f));
						Graphics.Blit(val5, val4, dofHdrMaterial, 19);
						RenderTexture val7 = null;
						if (nearBlur)
						{
							val7 = RenderTexture.GetTemporary(source.get_width() >> 1, source.get_height() >> 1, 0, source.get_format());
							Graphics.Blit(source, val7, dofHdrMaterial, 4);
						}
						dx11bokehMaterial.SetTexture("_BlurredColor", val4);
						dx11bokehMaterial.SetFloat("_SpawnHeuristic", dx11SpawnHeuristic);
						dx11bokehMaterial.SetVector("_BokehParams", new Vector4(dx11BokehScale, dx11BokehIntensity, Mathf.Clamp(dx11BokehThreshold, 0.005f, 4f), internalBlurWidth));
						dx11bokehMaterial.SetTexture("_FgCocMask", val7);
						Graphics.SetRandomWriteTarget(1, cbPoints);
						Graphics.Blit(val2, val3, dx11bokehMaterial, 0);
						Graphics.ClearRandomWriteTargets();
						RenderTexture.ReleaseTemporary(val4);
						RenderTexture.ReleaseTemporary(val5);
						if (nearBlur)
						{
							dofHdrMaterial.SetVector("_Offsets", new Vector4(0f, num2, 0f, num2));
							Graphics.Blit(val7, val2, dofHdrMaterial, 2);
							dofHdrMaterial.SetVector("_Offsets", new Vector4(num2, 0f, 0f, num2));
							Graphics.Blit(val2, val7, dofHdrMaterial, 2);
							Graphics.Blit(val7, val3, dofHdrMaterial, 3);
						}
						dofHdrMaterial.SetVector("_Offsets", new Vector4(internalBlurWidth, 0f, 0f, internalBlurWidth));
						Graphics.Blit(val3, val2, dofHdrMaterial, 5);
						dofHdrMaterial.SetVector("_Offsets", new Vector4(0f, internalBlurWidth, 0f, internalBlurWidth));
						Graphics.Blit(val2, val3, dofHdrMaterial, 5);
						Graphics.SetRenderTarget(val3);
						ComputeBuffer.CopyCount(cbPoints, cbDrawArgs, 0);
						dx11bokehMaterial.SetBuffer("pointBuffer", cbPoints);
						dx11bokehMaterial.SetTexture("_MainTex", dx11BokehTexture);
						dx11bokehMaterial.SetVector("_Screen", Vector4.op_Implicit(new Vector3(1f / (1f * (float)val3.get_width()), 1f / (1f * (float)val3.get_height()), internalBlurWidth)));
						dx11bokehMaterial.SetPass(1);
						Graphics.DrawProceduralIndirect(5, cbDrawArgs, 0);
						dofHdrMaterial.SetTexture("_LowRez", val3);
						dofHdrMaterial.SetTexture("_FgOverlap", val7);
						dofHdrMaterial.SetVector("_Offsets", 1f * (float)source.get_width() / (1f * (float)val3.get_width()) * internalBlurWidth * Vector4.get_one());
						Graphics.Blit(source, destination, dofHdrMaterial, 9);
						if (Object.op_Implicit(val7))
						{
							RenderTexture.ReleaseTemporary(val7);
						}
					}
				}
				else
				{
					source.set_filterMode(1);
					if (highResolution)
					{
						internalBlurWidth *= 2f;
					}
					WriteCoc(source, true);
					val2 = RenderTexture.GetTemporary(source.get_width() >> 1, source.get_height() >> 1, 0, source.get_format());
					val3 = RenderTexture.GetTemporary(source.get_width() >> 1, source.get_height() >> 1, 0, source.get_format());
					int num3 = (blurSampleCount != BlurSampleCount.High && blurSampleCount != BlurSampleCount.Medium) ? 11 : 17;
					if (highResolution)
					{
						dofHdrMaterial.SetVector("_Offsets", new Vector4(0f, internalBlurWidth, 0.025f, internalBlurWidth));
						Graphics.Blit(source, destination, dofHdrMaterial, num3);
					}
					else
					{
						dofHdrMaterial.SetVector("_Offsets", new Vector4(0f, internalBlurWidth, 0.1f, internalBlurWidth));
						Graphics.Blit(source, val2, dofHdrMaterial, 6);
						Graphics.Blit(val2, val3, dofHdrMaterial, num3);
						dofHdrMaterial.SetTexture("_LowRez", val3);
						dofHdrMaterial.SetTexture("_FgOverlap", null);
						dofHdrMaterial.SetVector("_Offsets", Vector4.get_one() * (1f * (float)source.get_width() / (1f * (float)val3.get_width())) * internalBlurWidth);
						Graphics.Blit(source, destination, dofHdrMaterial, (blurSampleCount != BlurSampleCount.High) ? 12 : 18);
					}
				}
				if (Object.op_Implicit(val2))
				{
					RenderTexture.ReleaseTemporary(val2);
				}
				if (Object.op_Implicit(val3))
				{
					RenderTexture.ReleaseTemporary(val3);
				}
			}
		}
	}
}
