using System;
using UnityEngine;

[AddComponentMenu("NGUI/UI/NGUI Texture")]
[ExecuteInEditMode]
public class UITexture : UIBasicSprite
{
	[HideInInspector]
	[SerializeField]
	private Rect mRect = new Rect(0f, 0f, 1f, 1f);

	[HideInInspector]
	[SerializeField]
	private Texture mTexture;

	[HideInInspector]
	[SerializeField]
	private Material mMat;

	[HideInInspector]
	[SerializeField]
	private Shader mShader;

	[SerializeField]
	[HideInInspector]
	private Vector4 mBorder = Vector4.get_zero();

	[SerializeField]
	[HideInInspector]
	private bool mFixedAspect;

	[NonSerialized]
	private int mPMA = -1;

	public override Texture mainTexture
	{
		get
		{
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Expected O, but got Unknown
			if (mTexture != null)
			{
				return mTexture;
			}
			if (mMat != null)
			{
				return mMat.get_mainTexture();
			}
			return null;
		}
		set
		{
			if (mTexture != value)
			{
				if (drawCall != null && drawCall.widgetCount == 1 && mMat == null)
				{
					mTexture = value;
					drawCall.mainTexture = value;
				}
				else
				{
					RemoveFromPanel();
					mTexture = value;
					mPMA = -1;
					MarkAsChanged();
				}
			}
		}
	}

	public override Material material
	{
		get
		{
			return mMat;
		}
		set
		{
			if (mMat != value)
			{
				RemoveFromPanel();
				mShader = null;
				mMat = value;
				mPMA = -1;
				MarkAsChanged();
			}
		}
	}

	public override Shader shader
	{
		get
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Expected O, but got Unknown
			if (mMat != null)
			{
				return mMat.get_shader();
			}
			if (mShader == null)
			{
				mShader = Shader.Find("Unlit/Transparent Colored");
			}
			return mShader;
		}
		set
		{
			if (mShader != value)
			{
				if (drawCall != null && drawCall.widgetCount == 1 && mMat == null)
				{
					mShader = value;
					drawCall.shader = value;
				}
				else
				{
					RemoveFromPanel();
					mShader = value;
					mPMA = -1;
					mMat = null;
					MarkAsChanged();
				}
			}
		}
	}

	public override bool premultipliedAlpha
	{
		get
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			if (mPMA == -1)
			{
				Material material = this.material;
				mPMA = ((material != null && material.get_shader() != null && material.get_shader().get_name().Contains("Premultiplied")) ? 1 : 0);
			}
			return mPMA == 1;
		}
	}

	public override Vector4 border
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mBorder;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			if (mBorder != value)
			{
				mBorder = value;
				MarkAsChanged();
			}
		}
	}

	public Rect uvRect
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mRect;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			if (mRect != value)
			{
				mRect = value;
				MarkAsChanged();
			}
		}
	}

	public override Vector4 drawingDimensions
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0173: Unknown result type (might be due to invalid IL or missing references)
			//IL_0178: Unknown result type (might be due to invalid IL or missing references)
			//IL_0202: Unknown result type (might be due to invalid IL or missing references)
			Vector2 pivotOffset = base.pivotOffset;
			float num = (0f - pivotOffset.x) * (float)mWidth;
			float num2 = (0f - pivotOffset.y) * (float)mHeight;
			float num3 = num + (float)mWidth;
			float num4 = num2 + (float)mHeight;
			if (mTexture != null && mType != Type.Tiled)
			{
				int width = mTexture.get_width();
				int height = mTexture.get_height();
				int num5 = 0;
				int num6 = 0;
				float num7 = 1f;
				float num8 = 1f;
				if (width > 0 && height > 0 && (mType == Type.Simple || mType == Type.Filled))
				{
					if ((width & 1) != 0)
					{
						num5++;
					}
					if ((height & 1) != 0)
					{
						num6++;
					}
					num7 = 1f / (float)width * (float)mWidth;
					num8 = 1f / (float)height * (float)mHeight;
				}
				if (mFlip == Flip.Horizontally || mFlip == Flip.Both)
				{
					num += (float)num5 * num7;
				}
				else
				{
					num3 -= (float)num5 * num7;
				}
				if (mFlip == Flip.Vertically || mFlip == Flip.Both)
				{
					num2 += (float)num6 * num8;
				}
				else
				{
					num4 -= (float)num6 * num8;
				}
			}
			float num9;
			float num10;
			if (mFixedAspect)
			{
				num9 = 0f;
				num10 = 0f;
			}
			else
			{
				Vector4 border = this.border;
				num9 = border.x + border.z;
				num10 = border.y + border.w;
			}
			float num11 = Mathf.Lerp(num, num3 - num9, mDrawRegion.x);
			float num12 = Mathf.Lerp(num2, num4 - num10, mDrawRegion.y);
			float num13 = Mathf.Lerp(num + num9, num3, mDrawRegion.z);
			float num14 = Mathf.Lerp(num2 + num10, num4, mDrawRegion.w);
			return new Vector4(num11, num12, num13, num14);
		}
	}

	public bool fixedAspect
	{
		get
		{
			return mFixedAspect;
		}
		set
		{
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			if (mFixedAspect != value)
			{
				mFixedAspect = value;
				mDrawRegion = new Vector4(0f, 0f, 1f, 1f);
				MarkAsChanged();
			}
		}
	}

	public override void MakePixelPerfect()
	{
		base.MakePixelPerfect();
		if (mType != Type.Tiled)
		{
			Texture mainTexture = this.mainTexture;
			if (!(mainTexture == null) && (mType == Type.Simple || mType == Type.Filled || !base.hasBorder) && mainTexture != null)
			{
				int num = mainTexture.get_width();
				int num2 = mainTexture.get_height();
				if ((num & 1) == 1)
				{
					num++;
				}
				if ((num2 & 1) == 1)
				{
					num2++;
				}
				base.width = num;
				base.height = num2;
			}
		}
	}

	protected override void OnUpdate()
	{
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		base.OnUpdate();
		if (mFixedAspect)
		{
			Texture mainTexture = this.mainTexture;
			if (mainTexture != null)
			{
				int num = mainTexture.get_width();
				int num2 = mainTexture.get_height();
				if ((num & 1) == 1)
				{
					num++;
				}
				if ((num2 & 1) == 1)
				{
					num2++;
				}
				float num3 = (float)mWidth;
				float num4 = (float)mHeight;
				float num5 = num3 / num4;
				float num6 = (float)num / (float)num2;
				if (num6 < num5)
				{
					float num7 = (num3 - num4 * num6) / num3 * 0.5f;
					base.drawRegion = new Vector4(num7, 0f, 1f - num7, 1f);
				}
				else
				{
					float num8 = (num4 - num3 / num6) / num4 * 0.5f;
					base.drawRegion = new Vector4(0f, num8, 1f, 1f - num8);
				}
			}
		}
	}

	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		Texture mainTexture = this.mainTexture;
		if (!(mainTexture == null))
		{
			Rect val = default(Rect);
			val._002Ector(mRect.get_x() * (float)mainTexture.get_width(), mRect.get_y() * (float)mainTexture.get_height(), (float)mainTexture.get_width() * mRect.get_width(), (float)mainTexture.get_height() * mRect.get_height());
			Rect inner = val;
			Vector4 border = this.border;
			inner.set_xMin(inner.get_xMin() + border.x);
			inner.set_yMin(inner.get_yMin() + border.y);
			inner.set_xMax(inner.get_xMax() - border.z);
			inner.set_yMax(inner.get_yMax() - border.w);
			float num = 1f / (float)mainTexture.get_width();
			float num2 = 1f / (float)mainTexture.get_height();
			val.set_xMin(val.get_xMin() * num);
			val.set_xMax(val.get_xMax() * num);
			val.set_yMin(val.get_yMin() * num2);
			val.set_yMax(val.get_yMax() * num2);
			inner.set_xMin(inner.get_xMin() * num);
			inner.set_xMax(inner.get_xMax() * num);
			inner.set_yMin(inner.get_yMin() * num2);
			inner.set_yMax(inner.get_yMax() * num2);
			int size = verts.size;
			Fill(verts, uvs, cols, val, inner);
			if (onPostFill != null)
			{
				onPostFill(this, size, verts, uvs, cols);
			}
		}
	}
}
