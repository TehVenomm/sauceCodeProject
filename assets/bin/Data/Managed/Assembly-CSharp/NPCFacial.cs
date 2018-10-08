using System;
using UnityEngine;

public class NPCFacial
{
	public enum TYPE
	{
		NORMAL,
		CLOSE,
		ANGER,
		SAD,
		JOY,
		HALF,
		SURPRISED
	}

	public Renderer faceRenderer;

	public Material eyeMaterial;

	public Texture[] eyeTextures;

	public Material mouthMaterial;

	public Texture[] mouthTextures;

	private TYPE _eyeType;

	private TYPE _mouthType;

	private Vector3 lastAnimValue;

	private TYPE lastAnimEyeType;

	private TYPE lastAnimMouthType;

	private bool enableEyeBlick;

	private float eyeBlinkTime;

	public TYPE eyeType
	{
		get
		{
			return _eyeType;
		}
		set
		{
			_eyeType = value;
			ResetEyeBlinkTime();
			SetTexture(eyeMaterial, eyeTextures, _eyeType);
		}
	}

	public TYPE mouthType
	{
		get
		{
			return _mouthType;
		}
		set
		{
			_mouthType = value;
			SetTexture(mouthMaterial, mouthTextures, _mouthType);
		}
	}

	public bool enableAnim
	{
		get;
		set;
	}

	public Transform animNode
	{
		get;
		set;
	}

	public Action<TYPE> animChangeEyeCallback
	{
		get;
		set;
	}

	public Action<TYPE> animChangeMouthCallback
	{
		get;
		set;
	}

	public NPCFacial()
		: this()
	{
	}

	private void Awake()
	{
		if (faceRenderer != null)
		{
			Material[] materials = faceRenderer.get_materials();
			if (eyeMaterial != null)
			{
				eyeMaterial = Array.Find(materials, (Material o) => o.get_name().StartsWith(eyeMaterial.get_name()));
			}
			if (mouthMaterial != null)
			{
				mouthMaterial = Array.Find(materials, (Material o) => o.get_name().StartsWith(mouthMaterial.get_name()));
			}
			if (eyeMaterial != null && eyeTextures[1] != null)
			{
				enableEyeBlick = true;
				ResetEyeBlinkTime();
			}
			enableAnim = true;
		}
	}

	private void Update()
	{
		UpdateAnim();
		UpdateEyeBlink();
	}

	private void UpdateAnim()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		if (!(animNode == null) && enableAnim)
		{
			Vector3 localPosition = animNode.get_localPosition();
			TYPE tYPE = (TYPE)(localPosition.y * 100f + 0.001f);
			TYPE tYPE2 = (TYPE)(localPosition.z * 100f + 0.001f);
			if (lastAnimEyeType != tYPE && Mathf.Abs(lastAnimValue.y - localPosition.y) < 0.0001f)
			{
				lastAnimEyeType = tYPE;
				if (animChangeEyeCallback != null)
				{
					animChangeEyeCallback(tYPE);
				}
				else
				{
					eyeType = tYPE;
				}
			}
			if (lastAnimMouthType != tYPE2 && Mathf.Abs(lastAnimValue.z - localPosition.z) < 0.0001f)
			{
				lastAnimMouthType = tYPE2;
				if (animChangeMouthCallback != null)
				{
					animChangeMouthCallback(tYPE2);
				}
				else
				{
					mouthType = tYPE2;
				}
			}
			lastAnimValue = localPosition;
		}
	}

	private void UpdateEyeBlink()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		if (!(faceRenderer == null) && enableEyeBlick && eyeType == TYPE.NORMAL)
		{
			eyeBlinkTime -= Time.get_deltaTime();
			if (eyeBlinkTime <= 0f)
			{
				if (eyeMaterial.get_mainTexture() != eyeTextures[1])
				{
					SetTexture(eyeMaterial, eyeTextures, TYPE.CLOSE);
					eyeBlinkTime = Random.Range(0.1f, 0.3f);
				}
				else
				{
					SetTexture(eyeMaterial, eyeTextures, _eyeType);
					ResetEyeBlinkTime();
				}
			}
		}
	}

	private void SetTexture(Material material, Texture[] textures, TYPE type)
	{
		if (textures != null && textures.Length != 0)
		{
			int num = (int)type;
			if (num < 0 || num >= textures.Length || textures[num] == null)
			{
				num = 0;
			}
			Texture val = textures[num];
			if (!(val == null))
			{
				material.set_mainTexture(val);
			}
		}
	}

	private void ResetEyeBlinkTime()
	{
		eyeBlinkTime = Random.Range(3f, 6f);
	}
}
