using System;
using UnityEngine;

[AddComponentMenu("NGUI/UI/NGUI Unity2D Sprite")]
[ExecuteInEditMode]
public class UI2DSprite : UIBasicSprite
{
	[SerializeField]
	[HideInInspector]
	private Sprite mSprite;

	[SerializeField]
	[HideInInspector]
	private Material mMat;

	[HideInInspector]
	[SerializeField]
	private Shader mShader;

	[SerializeField]
	[HideInInspector]
	private Vector4 mBorder = Vector4.get_zero();

	[HideInInspector]
	[SerializeField]
	private bool mFixedAspect;

	[SerializeField]
	[HideInInspector]
	private float mPixelSize = 1f;

	public Sprite nextSprite;

	[NonSerialized]
	private int mPMA = -1;

	public Sprite sprite2D
	{
		get
		{
			return mSprite;
		}
		set
		{
			if (mSprite != value)
			{
				RemoveFromPanel();
				mSprite = value;
				nextSprite = null;
				CreatePanel();
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
				RemoveFromPanel();
				mShader = value;
				if (mMat == null)
				{
					mPMA = -1;
					MarkAsChanged();
				}
			}
		}
	}

	public override Texture mainTexture
	{
		get
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Expected O, but got Unknown
			if (mSprite != null)
			{
				return mSprite.get_texture();
			}
			if (mMat != null)
			{
				return mMat.get_mainTexture();
			}
			return null;
		}
	}

	public override bool premultipliedAlpha
	{
		get
		{
			if (mPMA == -1)
			{
				Shader shader = this.shader;
				mPMA = ((shader != null && shader.get_name().Contains("Premultiplied")) ? 1 : 0);
			}
			return mPMA == 1;
		}
	}

	public override float pixelSize => mPixelSize;

	public override Vector4 drawingDimensions
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0061: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_0097: Unknown result type (might be due to invalid IL or missing references)
			//IL_009c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0112: Unknown result type (might be due to invalid IL or missing references)
			//IL_0117: Unknown result type (might be due to invalid IL or missing references)
			//IL_0126: Unknown result type (might be due to invalid IL or missing references)
			//IL_012b: Unknown result type (might be due to invalid IL or missing references)
			//IL_013b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0140: Unknown result type (might be due to invalid IL or missing references)
			//IL_026f: Unknown result type (might be due to invalid IL or missing references)
			//IL_027a: Unknown result type (might be due to invalid IL or missing references)
			//IL_027f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0309: Unknown result type (might be due to invalid IL or missing references)
			Vector2 pivotOffset = base.pivotOffset;
			float num = (0f - pivotOffset.x) * (float)mWidth;
			float num2 = (0f - pivotOffset.y) * (float)mHeight;
			float num3 = num + (float)mWidth;
			float num4 = num2 + (float)mHeight;
			if (mSprite != null && mType != Type.Tiled)
			{
				Rect rect = mSprite.get_rect();
				int num5 = Mathf.RoundToInt(rect.get_width());
				Rect rect2 = mSprite.get_rect();
				int num6 = Mathf.RoundToInt(rect2.get_height());
				Vector2 textureRectOffset = mSprite.get_textureRectOffset();
				int num7 = Mathf.RoundToInt(textureRectOffset.x);
				Vector2 textureRectOffset2 = mSprite.get_textureRectOffset();
				int num8 = Mathf.RoundToInt(textureRectOffset2.y);
				Rect rect3 = mSprite.get_rect();
				float width = rect3.get_width();
				Rect textureRect = mSprite.get_textureRect();
				float num9 = width - textureRect.get_width();
				Vector2 textureRectOffset3 = mSprite.get_textureRectOffset();
				int num10 = Mathf.RoundToInt(num9 - textureRectOffset3.x);
				Rect rect4 = mSprite.get_rect();
				float height = rect4.get_height();
				Rect textureRect2 = mSprite.get_textureRect();
				float num11 = height - textureRect2.get_height();
				Vector2 textureRectOffset4 = mSprite.get_textureRectOffset();
				int num12 = Mathf.RoundToInt(num11 - textureRectOffset4.y);
				float num13 = 1f;
				float num14 = 1f;
				if (num5 > 0 && num6 > 0 && (mType == Type.Simple || mType == Type.Filled))
				{
					if ((num5 & 1) != 0)
					{
						num10++;
					}
					if ((num6 & 1) != 0)
					{
						num12++;
					}
					num13 = 1f / (float)num5 * (float)mWidth;
					num14 = 1f / (float)num6 * (float)mHeight;
				}
				if (mFlip == Flip.Horizontally || mFlip == Flip.Both)
				{
					num += (float)num10 * num13;
					num3 -= (float)num7 * num13;
				}
				else
				{
					num += (float)num7 * num13;
					num3 -= (float)num10 * num13;
				}
				if (mFlip == Flip.Vertically || mFlip == Flip.Both)
				{
					num2 += (float)num12 * num14;
					num4 -= (float)num8 * num14;
				}
				else
				{
					num2 += (float)num8 * num14;
					num4 -= (float)num12 * num14;
				}
			}
			float num15;
			float num16;
			if (mFixedAspect)
			{
				num15 = 0f;
				num16 = 0f;
			}
			else
			{
				Vector4 val = border * pixelSize;
				num15 = val.x + val.z;
				num16 = val.y + val.w;
			}
			float num17 = Mathf.Lerp(num, num3 - num15, mDrawRegion.x);
			float num18 = Mathf.Lerp(num2, num4 - num16, mDrawRegion.y);
			float num19 = Mathf.Lerp(num + num15, num3, mDrawRegion.z);
			float num20 = Mathf.Lerp(num2 + num16, num4, mDrawRegion.w);
			return new Vector4(num17, num18, num19, num20);
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

	protected override void OnUpdate()
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		if (nextSprite != null)
		{
			if (nextSprite != mSprite)
			{
				sprite2D = nextSprite;
			}
			nextSprite = null;
		}
		base.OnUpdate();
		if (mFixedAspect)
		{
			Texture mainTexture = this.mainTexture;
			if (mainTexture != null)
			{
				Rect rect = mSprite.get_rect();
				int num = Mathf.RoundToInt(rect.get_width());
				Rect rect2 = mSprite.get_rect();
				int num2 = Mathf.RoundToInt(rect2.get_height());
				Vector2 textureRectOffset = mSprite.get_textureRectOffset();
				int num3 = Mathf.RoundToInt(textureRectOffset.x);
				Vector2 textureRectOffset2 = mSprite.get_textureRectOffset();
				int num4 = Mathf.RoundToInt(textureRectOffset2.y);
				Rect rect3 = mSprite.get_rect();
				float width = rect3.get_width();
				Rect textureRect = mSprite.get_textureRect();
				float num5 = width - textureRect.get_width();
				Vector2 textureRectOffset3 = mSprite.get_textureRectOffset();
				int num6 = Mathf.RoundToInt(num5 - textureRectOffset3.x);
				Rect rect4 = mSprite.get_rect();
				float height = rect4.get_height();
				Rect textureRect2 = mSprite.get_textureRect();
				float num7 = height - textureRect2.get_height();
				Vector2 textureRectOffset4 = mSprite.get_textureRectOffset();
				int num8 = Mathf.RoundToInt(num7 - textureRectOffset4.y);
				num += num3 + num6;
				num2 += num8 + num4;
				float num9 = (float)mWidth;
				float num10 = (float)mHeight;
				float num11 = num9 / num10;
				float num12 = (float)num / (float)num2;
				if (num12 < num11)
				{
					float num13 = (num9 - num10 * num12) / num9 * 0.5f;
					base.drawRegion = new Vector4(num13, 0f, 1f - num13, 1f);
				}
				else
				{
					float num14 = (num10 - num9 / num12) / num10 * 0.5f;
					base.drawRegion = new Vector4(0f, num14, 1f, 1f - num14);
				}
			}
		}
	}

	public override void MakePixelPerfect()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		base.MakePixelPerfect();
		if (mType != Type.Tiled)
		{
			Texture mainTexture = this.mainTexture;
			if (!(mainTexture == null) && (mType == Type.Simple || mType == Type.Filled || !base.hasBorder) && mainTexture != null)
			{
				Rect rect = mSprite.get_rect();
				int num = Mathf.RoundToInt(rect.get_width());
				int num2 = Mathf.RoundToInt(rect.get_height());
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

	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		Texture mainTexture = this.mainTexture;
		if (!(mainTexture == null))
		{
			Rect val = (!(mSprite != null)) ? new Rect(0f, 0f, (float)mainTexture.get_width(), (float)mainTexture.get_height()) : mSprite.get_textureRect();
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
