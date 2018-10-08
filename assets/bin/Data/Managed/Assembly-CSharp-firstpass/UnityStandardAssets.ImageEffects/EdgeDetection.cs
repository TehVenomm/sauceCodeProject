using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/Edge Detection/Edge Detection")]
	[RequireComponent(typeof(Camera))]
	public class EdgeDetection : PostEffectsBase
	{
		public enum EdgeDetectMode
		{
			TriangleDepthNormals,
			RobertsCrossDepthNormals,
			SobelDepth,
			SobelDepthThin,
			TriangleLuminance
		}

		public EdgeDetectMode mode = EdgeDetectMode.SobelDepthThin;

		public float sensitivityDepth = 1f;

		public float sensitivityNormals = 1f;

		public float lumThreshold = 0.2f;

		public float edgeExp = 1f;

		public float sampleDist = 1f;

		public float edgesOnly;

		public Color edgesOnlyBgColor = Color.get_white();

		public Shader edgeDetectShader;

		private Material edgeDetectMaterial;

		private EdgeDetectMode oldMode = EdgeDetectMode.SobelDepthThin;

		public override bool CheckResources()
		{
			CheckSupport(true);
			edgeDetectMaterial = CheckShaderAndCreateMaterial(edgeDetectShader, edgeDetectMaterial);
			if (mode != oldMode)
			{
				SetCameraFlag();
			}
			oldMode = mode;
			if (!isSupported)
			{
				ReportAutoDisable();
			}
			return isSupported;
		}

		private new void Start()
		{
			oldMode = mode;
		}

		private void SetCameraFlag()
		{
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			if (mode == EdgeDetectMode.SobelDepth || mode == EdgeDetectMode.SobelDepthThin)
			{
				Camera component = this.GetComponent<Camera>();
				component.set_depthTextureMode(component.get_depthTextureMode() | 1);
			}
			else if (mode == EdgeDetectMode.TriangleDepthNormals || mode == EdgeDetectMode.RobertsCrossDepthNormals)
			{
				Camera component2 = this.GetComponent<Camera>();
				component2.set_depthTextureMode(component2.get_depthTextureMode() | 2);
			}
		}

		private void OnEnable()
		{
			SetCameraFlag();
		}

		[ImageEffectOpaque]
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0092: Unknown result type (might be due to invalid IL or missing references)
			if (!CheckResources())
			{
				Graphics.Blit(source, destination);
			}
			else
			{
				Vector2 val = default(Vector2);
				val._002Ector(sensitivityDepth, sensitivityNormals);
				edgeDetectMaterial.SetVector("_Sensitivity", new Vector4(val.x, val.y, 1f, val.y));
				edgeDetectMaterial.SetFloat("_BgFade", edgesOnly);
				edgeDetectMaterial.SetFloat("_SampleDistance", sampleDist);
				edgeDetectMaterial.SetVector("_BgColor", Color.op_Implicit(edgesOnlyBgColor));
				edgeDetectMaterial.SetFloat("_Exponent", edgeExp);
				edgeDetectMaterial.SetFloat("_Threshold", lumThreshold);
				Graphics.Blit(source, destination, edgeDetectMaterial, (int)mode);
			}
		}
	}
}
