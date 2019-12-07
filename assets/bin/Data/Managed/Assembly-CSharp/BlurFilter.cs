using UnityEngine;

public class BlurFilter : FilterBase
{
	[SerializeField]
	private float _blurStrength;

	[SerializeField]
	private float strengthLowLimit = 0.0001f;

	[SerializeField]
	private int downsample = 2;

	[SerializeField]
	private int iterationNum = 1;

	private const int PASS_NUM = 3;

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
				if (_blurMaterial[i] == null)
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
		if (shader == null)
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
		if (postEffector != null)
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
			return;
		}
		if (!isValid)
		{
			Graphics.Blit(src, dest);
			return;
		}
		int width = src.width >> downsample;
		int height = src.height >> downsample;
		RenderTexture renderTexture = RenderTexture.GetTemporary(width, height, 0, src.format);
		renderTexture.filterMode = FilterMode.Bilinear;
		Graphics.Blit(src, renderTexture, blurMaterial[0]);
		float num = 1f / (1f * (float)downsample);
		for (int i = 0; i < iterationNum; i++)
		{
			float num2 = i;
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
