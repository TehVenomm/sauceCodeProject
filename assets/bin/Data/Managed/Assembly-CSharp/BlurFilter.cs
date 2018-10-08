using UnityEngine;

public class BlurFilter : FilterBase
{
	private const int PASS_NUM = 3;

	[SerializeField]
	private float _blurStrength;

	[SerializeField]
	private float strengthLowLimit = 0.0001f;

	[SerializeField]
	private int downsample = 2;

	[SerializeField]
	private int iterationNum = 1;

	private Material[] _blurMaterial = new Material[3];

	private PostEffector postEffector;

	public float blurStrength
	{
		get
		{
			return _blurStrength;
		}
		set
		{
			_blurStrength = value;
		}
	}

	public int downSample
	{
		get
		{
			return downsample;
		}
		set
		{
			downsample = value;
		}
	}

	public Material[] blurMaterial => _blurMaterial;

	private bool isValid
	{
		get
		{
			for (int i = 0; i < 3; i++)
			{
				if ((Object)_blurMaterial[i] == (Object)null)
				{
					return false;
				}
			}
			return true;
		}
	}

	private void Awake()
	{
		_blurMaterial[0] = CreateMaterial("Custom/UI/Blur_Pass0");
		_blurMaterial[1] = CreateMaterial("Custom/UI/Blur_Pass1");
		_blurMaterial[2] = CreateMaterial("Custom/UI/Blur_Pass2");
	}

	private Material CreateMaterial(string shaderName)
	{
		Shader shader = ResourceUtility.FindShader(shaderName);
		if ((Object)shader == (Object)null)
		{
			return null;
		}
		return new Material(shader);
	}

	public override void StartFilter()
	{
		postEffector = base.gameObject.AddComponent<PostEffector>();
		postEffector.SetFilter(this);
	}

	public override void StopFilter()
	{
		if ((Object)postEffector != (Object)null)
		{
			Object.Destroy(postEffector);
			postEffector = null;
		}
	}

	public override void PostEffectProc(RenderTexture src, RenderTexture dest)
	{
		if (blurStrength <= strengthLowLimit)
		{
			Graphics.Blit((Texture)src, (RenderTexture)null);
		}
		else if (!isValid)
		{
			Graphics.Blit(src, dest);
		}
		else
		{
			int width = src.width >> downsample;
			int height = src.height >> downsample;
			RenderTexture renderTexture = RenderTexture.GetTemporary(width, height, 0, src.format);
			renderTexture.filterMode = FilterMode.Bilinear;
			Graphics.Blit(src, renderTexture, blurMaterial[0]);
			float num = 1f / (1f * (float)downsample);
			for (int i = 0; i < iterationNum; i++)
			{
				float num2 = (float)i;
				for (int j = 0; j < 3; j++)
				{
					blurMaterial[j].SetVector("_Parameter", new Vector4(blurStrength * num + num2, (0f - blurStrength) * num - num2, 0f, 0f));
				}
				RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, src.format);
				temporary.filterMode = FilterMode.Bilinear;
				Graphics.Blit(renderTexture, temporary, blurMaterial[1]);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = temporary;
				temporary = RenderTexture.GetTemporary(width, height, 0, src.format);
				temporary.filterMode = FilterMode.Bilinear;
				Graphics.Blit(renderTexture, temporary, blurMaterial[2]);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = temporary;
			}
			Graphics.Blit(renderTexture, dest);
			RenderTexture.ReleaseTemporary(renderTexture);
		}
	}
}
