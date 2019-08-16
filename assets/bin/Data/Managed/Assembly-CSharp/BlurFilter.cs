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

	private Material[] _blurMaterial = (Material[])new Material[3];

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
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		Shader val = ResourceUtility.FindShader(shaderName);
		if (val == null)
		{
			return null;
		}
		return new Material(val);
	}

	public override void StartFilter()
	{
		postEffector = this.get_gameObject().AddComponent<PostEffector>();
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
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		if (blurStrength <= strengthLowLimit)
		{
			Graphics.Blit(src, null);
			return;
		}
		if (!isValid)
		{
			Graphics.Blit(src, dest);
			return;
		}
		int num = src.get_width() >> downsample;
		int num2 = src.get_height() >> downsample;
		RenderTexture val = RenderTexture.GetTemporary(num, num2, 0, src.get_format());
		val.set_filterMode(1);
		Graphics.Blit(src, val, blurMaterial[0]);
		float num3 = 1f / (1f * (float)downsample);
		for (int i = 0; i < iterationNum; i++)
		{
			float num4 = i;
			for (int j = 0; j < 3; j++)
			{
				blurMaterial[j].SetVector("_Parameter", new Vector4(blurStrength * num3 + num4, (0f - blurStrength) * num3 - num4, 0f, 0f));
			}
			RenderTexture temporary = RenderTexture.GetTemporary(num, num2, 0, src.get_format());
			temporary.set_filterMode(1);
			Graphics.Blit(val, temporary, blurMaterial[1]);
			RenderTexture.ReleaseTemporary(val);
			val = temporary;
			temporary = RenderTexture.GetTemporary(num, num2, 0, src.get_format());
			temporary.set_filterMode(1);
			Graphics.Blit(val, temporary, blurMaterial[2]);
			RenderTexture.ReleaseTemporary(val);
			val = temporary;
		}
		Graphics.Blit(val, dest);
		RenderTexture.ReleaseTemporary(val);
	}
}
