using System;
using UnityEngine;

public abstract class UIBasicSprite : UIWidget
{
	public enum Type
	{
		Simple,
		Sliced,
		Tiled,
		Filled,
		Advanced
	}

	public enum FillDirection
	{
		Horizontal,
		Vertical,
		Radial90,
		Radial180,
		Radial360
	}

	public enum AdvancedType
	{
		Invisible,
		Sliced,
		Tiled
	}

	public enum Flip
	{
		Nothing,
		Horizontally,
		Vertically,
		Both
	}

	[SerializeField]
	[HideInInspector]
	protected Type mType;

	[SerializeField]
	[HideInInspector]
	protected FillDirection mFillDirection = FillDirection.Radial360;

	[SerializeField]
	[Range(0f, 1f)]
	[HideInInspector]
	protected float mFillAmount = 1f;

	[SerializeField]
	[HideInInspector]
	protected bool mInvert;

	[SerializeField]
	[HideInInspector]
	protected Flip mFlip;

	[NonSerialized]
	private Rect mInnerUV = default(Rect);

	[NonSerialized]
	private Rect mOuterUV = default(Rect);

	public AdvancedType centerType = AdvancedType.Sliced;

	public AdvancedType leftType = AdvancedType.Sliced;

	public AdvancedType rightType = AdvancedType.Sliced;

	public AdvancedType bottomType = AdvancedType.Sliced;

	public AdvancedType topType = AdvancedType.Sliced;

	protected static Vector2[] mTempPos = (Vector2[])new Vector2[4];

	protected static Vector2[] mTempUVs = (Vector2[])new Vector2[4];

	public virtual Type type
	{
		get
		{
			return mType;
		}
		set
		{
			if (mType != value)
			{
				mType = value;
				MarkAsChanged();
			}
		}
	}

	public Flip flip
	{
		get
		{
			return mFlip;
		}
		set
		{
			if (mFlip != value)
			{
				mFlip = value;
				MarkAsChanged();
			}
		}
	}

	public FillDirection fillDirection
	{
		get
		{
			return mFillDirection;
		}
		set
		{
			if (mFillDirection != value)
			{
				mFillDirection = value;
				mChanged = true;
			}
		}
	}

	public float fillAmount
	{
		get
		{
			return mFillAmount;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (mFillAmount != num)
			{
				mFillAmount = num;
				mChanged = true;
			}
		}
	}

	public override int minWidth
	{
		get
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			if (type == Type.Sliced || type == Type.Advanced)
			{
				Vector4 val = border * pixelSize;
				int num = Mathf.RoundToInt(val.x + val.z);
				return Mathf.Max(base.minWidth, ((num & 1) != 1) ? num : (num + 1));
			}
			return base.minWidth;
		}
	}

	public override int minHeight
	{
		get
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			if (type == Type.Sliced || type == Type.Advanced)
			{
				Vector4 val = border * pixelSize;
				int num = Mathf.RoundToInt(val.y + val.w);
				return Mathf.Max(base.minHeight, ((num & 1) != 1) ? num : (num + 1));
			}
			return base.minHeight;
		}
	}

	public bool invert
	{
		get
		{
			return mInvert;
		}
		set
		{
			if (mInvert != value)
			{
				mInvert = value;
				mChanged = true;
			}
		}
	}

	public bool hasBorder
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			Vector4 border = this.border;
			return border.x != 0f || border.y != 0f || border.z != 0f || border.w != 0f;
		}
	}

	public virtual bool premultipliedAlpha => false;

	public virtual float pixelSize => 1f;

	private Vector4 drawingUVs
	{
		get
		{
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
			switch (mFlip)
			{
			case Flip.Horizontally:
				return new Vector4(mOuterUV.get_xMax(), mOuterUV.get_yMin(), mOuterUV.get_xMin(), mOuterUV.get_yMax());
			case Flip.Vertically:
				return new Vector4(mOuterUV.get_xMin(), mOuterUV.get_yMax(), mOuterUV.get_xMax(), mOuterUV.get_yMin());
			case Flip.Both:
				return new Vector4(mOuterUV.get_xMax(), mOuterUV.get_yMax(), mOuterUV.get_xMin(), mOuterUV.get_yMin());
			default:
				return new Vector4(mOuterUV.get_xMin(), mOuterUV.get_yMin(), mOuterUV.get_xMax(), mOuterUV.get_yMax());
			}
		}
	}

	private Color32 drawingColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Invalid comparison between Unknown and I4
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_006b: Unknown result type (might be due to invalid IL or missing references)
			Color val = base.color;
			val.a = finalAlpha;
			if (premultipliedAlpha)
			{
				val = NGUITools.ApplyPMA(val);
			}
			if ((int)QualitySettings.get_activeColorSpace() == 1)
			{
				val.r = Mathf.GammaToLinearSpace(val.r);
				val.g = Mathf.GammaToLinearSpace(val.g);
				val.b = Mathf.GammaToLinearSpace(val.b);
			}
			return Color32.op_Implicit(val);
		}
	}

	protected void Fill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols, Rect outer, Rect inner)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		mOuterUV = outer;
		mInnerUV = inner;
		switch (type)
		{
		case Type.Simple:
			SimpleFill(verts, uvs, cols);
			break;
		case Type.Sliced:
			SlicedFill(verts, uvs, cols);
			break;
		case Type.Filled:
			FilledFill(verts, uvs, cols);
			break;
		case Type.Tiled:
			TiledFill(verts, uvs, cols);
			break;
		case Type.Advanced:
			AdvancedFill(verts, uvs, cols);
			break;
		}
	}

	private void SimpleFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		Vector4 drawingDimensions = this.drawingDimensions;
		Vector4 drawingUVs = this.drawingUVs;
		Color32 drawingColor = this.drawingColor;
		verts.Add(new Vector3(drawingDimensions.x, drawingDimensions.y));
		verts.Add(new Vector3(drawingDimensions.x, drawingDimensions.w));
		verts.Add(new Vector3(drawingDimensions.z, drawingDimensions.w));
		verts.Add(new Vector3(drawingDimensions.z, drawingDimensions.y));
		uvs.Add(new Vector2(drawingUVs.x, drawingUVs.y));
		uvs.Add(new Vector2(drawingUVs.x, drawingUVs.w));
		uvs.Add(new Vector2(drawingUVs.z, drawingUVs.w));
		uvs.Add(new Vector2(drawingUVs.z, drawingUVs.y));
		cols.Add(drawingColor);
		cols.Add(drawingColor);
		cols.Add(drawingColor);
		cols.Add(drawingColor);
	}

	private void SlicedFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		Vector4 val = border * pixelSize;
		if (val.x == 0f && val.y == 0f && val.z == 0f && val.w == 0f)
		{
			SimpleFill(verts, uvs, cols);
		}
		else
		{
			Color32 drawingColor = this.drawingColor;
			Vector4 drawingDimensions = this.drawingDimensions;
			mTempPos[0].x = drawingDimensions.x;
			mTempPos[0].y = drawingDimensions.y;
			mTempPos[3].x = drawingDimensions.z;
			mTempPos[3].y = drawingDimensions.w;
			if (mFlip == Flip.Horizontally || mFlip == Flip.Both)
			{
				mTempPos[1].x = mTempPos[0].x + val.z;
				mTempPos[2].x = mTempPos[3].x - val.x;
				mTempUVs[3].x = mOuterUV.get_xMin();
				mTempUVs[2].x = mInnerUV.get_xMin();
				mTempUVs[1].x = mInnerUV.get_xMax();
				mTempUVs[0].x = mOuterUV.get_xMax();
			}
			else
			{
				mTempPos[1].x = mTempPos[0].x + val.x;
				mTempPos[2].x = mTempPos[3].x - val.z;
				mTempUVs[0].x = mOuterUV.get_xMin();
				mTempUVs[1].x = mInnerUV.get_xMin();
				mTempUVs[2].x = mInnerUV.get_xMax();
				mTempUVs[3].x = mOuterUV.get_xMax();
			}
			if (mFlip == Flip.Vertically || mFlip == Flip.Both)
			{
				mTempPos[1].y = mTempPos[0].y + val.w;
				mTempPos[2].y = mTempPos[3].y - val.y;
				mTempUVs[3].y = mOuterUV.get_yMin();
				mTempUVs[2].y = mInnerUV.get_yMin();
				mTempUVs[1].y = mInnerUV.get_yMax();
				mTempUVs[0].y = mOuterUV.get_yMax();
			}
			else
			{
				mTempPos[1].y = mTempPos[0].y + val.y;
				mTempPos[2].y = mTempPos[3].y - val.w;
				mTempUVs[0].y = mOuterUV.get_yMin();
				mTempUVs[1].y = mInnerUV.get_yMin();
				mTempUVs[2].y = mInnerUV.get_yMax();
				mTempUVs[3].y = mOuterUV.get_yMax();
			}
			for (int i = 0; i < 3; i++)
			{
				int num = i + 1;
				for (int j = 0; j < 3; j++)
				{
					if (centerType != 0 || i != 1 || j != 1)
					{
						int num2 = j + 1;
						verts.Add(new Vector3(mTempPos[i].x, mTempPos[j].y));
						verts.Add(new Vector3(mTempPos[i].x, mTempPos[num2].y));
						verts.Add(new Vector3(mTempPos[num].x, mTempPos[num2].y));
						verts.Add(new Vector3(mTempPos[num].x, mTempPos[j].y));
						uvs.Add(new Vector2(mTempUVs[i].x, mTempUVs[j].y));
						uvs.Add(new Vector2(mTempUVs[i].x, mTempUVs[num2].y));
						uvs.Add(new Vector2(mTempUVs[num].x, mTempUVs[num2].y));
						uvs.Add(new Vector2(mTempUVs[num].x, mTempUVs[j].y));
						cols.Add(drawingColor);
						cols.Add(drawingColor);
						cols.Add(drawingColor);
						cols.Add(drawingColor);
					}
				}
			}
		}
	}

	private void TiledFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		Texture mainTexture = this.mainTexture;
		if (!(mainTexture == null))
		{
			Vector2 val = default(Vector2);
			val._002Ector(mInnerUV.get_width() * (float)mainTexture.get_width(), mInnerUV.get_height() * (float)mainTexture.get_height());
			val *= pixelSize;
			if (!(mainTexture == null) && !(val.x < 2f) && !(val.y < 2f))
			{
				Color32 drawingColor = this.drawingColor;
				Vector4 drawingDimensions = this.drawingDimensions;
				Vector4 val2 = default(Vector4);
				if (mFlip == Flip.Horizontally || mFlip == Flip.Both)
				{
					val2.x = mInnerUV.get_xMax();
					val2.z = mInnerUV.get_xMin();
				}
				else
				{
					val2.x = mInnerUV.get_xMin();
					val2.z = mInnerUV.get_xMax();
				}
				if (mFlip == Flip.Vertically || mFlip == Flip.Both)
				{
					val2.y = mInnerUV.get_yMax();
					val2.w = mInnerUV.get_yMin();
				}
				else
				{
					val2.y = mInnerUV.get_yMin();
					val2.w = mInnerUV.get_yMax();
				}
				float x = drawingDimensions.x;
				float num = drawingDimensions.y;
				float x2 = val2.x;
				float y = val2.y;
				for (; num < drawingDimensions.w; num += val.y)
				{
					x = drawingDimensions.x;
					float num2 = num + val.y;
					float num3 = val2.w;
					if (num2 > drawingDimensions.w)
					{
						num3 = Mathf.Lerp(val2.y, val2.w, (drawingDimensions.w - num) / val.y);
						num2 = drawingDimensions.w;
					}
					for (; x < drawingDimensions.z; x += val.x)
					{
						float num4 = x + val.x;
						float num5 = val2.z;
						if (num4 > drawingDimensions.z)
						{
							num5 = Mathf.Lerp(val2.x, val2.z, (drawingDimensions.z - x) / val.x);
							num4 = drawingDimensions.z;
						}
						verts.Add(new Vector3(x, num));
						verts.Add(new Vector3(x, num2));
						verts.Add(new Vector3(num4, num2));
						verts.Add(new Vector3(num4, num));
						uvs.Add(new Vector2(x2, y));
						uvs.Add(new Vector2(x2, num3));
						uvs.Add(new Vector2(num5, num3));
						uvs.Add(new Vector2(num5, y));
						cols.Add(drawingColor);
						cols.Add(drawingColor);
						cols.Add(drawingColor);
						cols.Add(drawingColor);
					}
				}
			}
		}
	}

	private void FilledFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_0634: Unknown result type (might be due to invalid IL or missing references)
		//IL_0970: Unknown result type (might be due to invalid IL or missing references)
		//IL_0975: Unknown result type (might be due to invalid IL or missing references)
		//IL_098c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0997: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f6: Unknown result type (might be due to invalid IL or missing references)
		if (!(mFillAmount < 0.001f))
		{
			Vector4 drawingDimensions = this.drawingDimensions;
			Vector4 drawingUVs = this.drawingUVs;
			Color32 drawingColor = this.drawingColor;
			if (mFillDirection == FillDirection.Horizontal || mFillDirection == FillDirection.Vertical)
			{
				if (mFillDirection == FillDirection.Horizontal)
				{
					float num = (drawingUVs.z - drawingUVs.x) * mFillAmount;
					if (mInvert)
					{
						drawingDimensions.x = drawingDimensions.z - (drawingDimensions.z - drawingDimensions.x) * mFillAmount;
						drawingUVs.x = drawingUVs.z - num;
					}
					else
					{
						drawingDimensions.z = drawingDimensions.x + (drawingDimensions.z - drawingDimensions.x) * mFillAmount;
						drawingUVs.z = drawingUVs.x + num;
					}
				}
				else if (mFillDirection == FillDirection.Vertical)
				{
					float num2 = (drawingUVs.w - drawingUVs.y) * mFillAmount;
					if (mInvert)
					{
						drawingDimensions.y = drawingDimensions.w - (drawingDimensions.w - drawingDimensions.y) * mFillAmount;
						drawingUVs.y = drawingUVs.w - num2;
					}
					else
					{
						drawingDimensions.w = drawingDimensions.y + (drawingDimensions.w - drawingDimensions.y) * mFillAmount;
						drawingUVs.w = drawingUVs.y + num2;
					}
				}
			}
			mTempPos[0] = new Vector2(drawingDimensions.x, drawingDimensions.y);
			mTempPos[1] = new Vector2(drawingDimensions.x, drawingDimensions.w);
			mTempPos[2] = new Vector2(drawingDimensions.z, drawingDimensions.w);
			mTempPos[3] = new Vector2(drawingDimensions.z, drawingDimensions.y);
			mTempUVs[0] = new Vector2(drawingUVs.x, drawingUVs.y);
			mTempUVs[1] = new Vector2(drawingUVs.x, drawingUVs.w);
			mTempUVs[2] = new Vector2(drawingUVs.z, drawingUVs.w);
			mTempUVs[3] = new Vector2(drawingUVs.z, drawingUVs.y);
			if (mFillAmount < 1f)
			{
				if (mFillDirection == FillDirection.Radial90)
				{
					if (RadialCut(mTempPos, mTempUVs, mFillAmount, mInvert, 0))
					{
						for (int i = 0; i < 4; i++)
						{
							verts.Add(Vector2.op_Implicit(mTempPos[i]));
							uvs.Add(mTempUVs[i]);
							cols.Add(drawingColor);
						}
					}
					return;
				}
				if (mFillDirection == FillDirection.Radial180)
				{
					for (int j = 0; j < 2; j++)
					{
						float num3 = 0f;
						float num4 = 1f;
						float num5;
						float num6;
						if (j == 0)
						{
							num5 = 0f;
							num6 = 0.5f;
						}
						else
						{
							num5 = 0.5f;
							num6 = 1f;
						}
						mTempPos[0].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, num5);
						mTempPos[1].x = mTempPos[0].x;
						mTempPos[2].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, num6);
						mTempPos[3].x = mTempPos[2].x;
						mTempPos[0].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, num3);
						mTempPos[1].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, num4);
						mTempPos[2].y = mTempPos[1].y;
						mTempPos[3].y = mTempPos[0].y;
						mTempUVs[0].x = Mathf.Lerp(drawingUVs.x, drawingUVs.z, num5);
						mTempUVs[1].x = mTempUVs[0].x;
						mTempUVs[2].x = Mathf.Lerp(drawingUVs.x, drawingUVs.z, num6);
						mTempUVs[3].x = mTempUVs[2].x;
						mTempUVs[0].y = Mathf.Lerp(drawingUVs.y, drawingUVs.w, num3);
						mTempUVs[1].y = Mathf.Lerp(drawingUVs.y, drawingUVs.w, num4);
						mTempUVs[2].y = mTempUVs[1].y;
						mTempUVs[3].y = mTempUVs[0].y;
						float num7 = mInvert ? (mFillAmount * 2f - (float)(1 - j)) : (fillAmount * 2f - (float)j);
						if (RadialCut(mTempPos, mTempUVs, Mathf.Clamp01(num7), !mInvert, NGUIMath.RepeatIndex(j + 3, 4)))
						{
							for (int k = 0; k < 4; k++)
							{
								verts.Add(Vector2.op_Implicit(mTempPos[k]));
								uvs.Add(mTempUVs[k]);
								cols.Add(drawingColor);
							}
						}
					}
					return;
				}
				if (mFillDirection == FillDirection.Radial360)
				{
					for (int l = 0; l < 4; l++)
					{
						float num8;
						float num9;
						if (l < 2)
						{
							num8 = 0f;
							num9 = 0.5f;
						}
						else
						{
							num8 = 0.5f;
							num9 = 1f;
						}
						float num10;
						float num11;
						if (l == 0 || l == 3)
						{
							num10 = 0f;
							num11 = 0.5f;
						}
						else
						{
							num10 = 0.5f;
							num11 = 1f;
						}
						mTempPos[0].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, num8);
						mTempPos[1].x = mTempPos[0].x;
						mTempPos[2].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, num9);
						mTempPos[3].x = mTempPos[2].x;
						mTempPos[0].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, num10);
						mTempPos[1].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, num11);
						mTempPos[2].y = mTempPos[1].y;
						mTempPos[3].y = mTempPos[0].y;
						mTempUVs[0].x = Mathf.Lerp(drawingUVs.x, drawingUVs.z, num8);
						mTempUVs[1].x = mTempUVs[0].x;
						mTempUVs[2].x = Mathf.Lerp(drawingUVs.x, drawingUVs.z, num9);
						mTempUVs[3].x = mTempUVs[2].x;
						mTempUVs[0].y = Mathf.Lerp(drawingUVs.y, drawingUVs.w, num10);
						mTempUVs[1].y = Mathf.Lerp(drawingUVs.y, drawingUVs.w, num11);
						mTempUVs[2].y = mTempUVs[1].y;
						mTempUVs[3].y = mTempUVs[0].y;
						float num12 = (!mInvert) ? (mFillAmount * 4f - (float)(3 - NGUIMath.RepeatIndex(l + 2, 4))) : (mFillAmount * 4f - (float)NGUIMath.RepeatIndex(l + 2, 4));
						if (RadialCut(mTempPos, mTempUVs, Mathf.Clamp01(num12), mInvert, NGUIMath.RepeatIndex(l + 2, 4)))
						{
							for (int m = 0; m < 4; m++)
							{
								verts.Add(Vector2.op_Implicit(mTempPos[m]));
								uvs.Add(mTempUVs[m]);
								cols.Add(drawingColor);
							}
						}
					}
					return;
				}
			}
			for (int n = 0; n < 4; n++)
			{
				verts.Add(Vector2.op_Implicit(mTempPos[n]));
				uvs.Add(mTempUVs[n]);
				cols.Add(drawingColor);
			}
		}
	}

	private void AdvancedFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_07eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9e: Unknown result type (might be due to invalid IL or missing references)
		Texture mainTexture = this.mainTexture;
		if (!(mainTexture == null))
		{
			Vector4 val = border * pixelSize;
			if (val.x == 0f && val.y == 0f && val.z == 0f && val.w == 0f)
			{
				SimpleFill(verts, uvs, cols);
			}
			else
			{
				Color32 drawingColor = this.drawingColor;
				Vector4 drawingDimensions = this.drawingDimensions;
				Vector2 val2 = default(Vector2);
				val2._002Ector(mInnerUV.get_width() * (float)mainTexture.get_width(), mInnerUV.get_height() * (float)mainTexture.get_height());
				val2 *= pixelSize;
				if (val2.x < 1f)
				{
					val2.x = 1f;
				}
				if (val2.y < 1f)
				{
					val2.y = 1f;
				}
				mTempPos[0].x = drawingDimensions.x;
				mTempPos[0].y = drawingDimensions.y;
				mTempPos[3].x = drawingDimensions.z;
				mTempPos[3].y = drawingDimensions.w;
				if (mFlip == Flip.Horizontally || mFlip == Flip.Both)
				{
					mTempPos[1].x = mTempPos[0].x + val.z;
					mTempPos[2].x = mTempPos[3].x - val.x;
					mTempUVs[3].x = mOuterUV.get_xMin();
					mTempUVs[2].x = mInnerUV.get_xMin();
					mTempUVs[1].x = mInnerUV.get_xMax();
					mTempUVs[0].x = mOuterUV.get_xMax();
				}
				else
				{
					mTempPos[1].x = mTempPos[0].x + val.x;
					mTempPos[2].x = mTempPos[3].x - val.z;
					mTempUVs[0].x = mOuterUV.get_xMin();
					mTempUVs[1].x = mInnerUV.get_xMin();
					mTempUVs[2].x = mInnerUV.get_xMax();
					mTempUVs[3].x = mOuterUV.get_xMax();
				}
				if (mFlip == Flip.Vertically || mFlip == Flip.Both)
				{
					mTempPos[1].y = mTempPos[0].y + val.w;
					mTempPos[2].y = mTempPos[3].y - val.y;
					mTempUVs[3].y = mOuterUV.get_yMin();
					mTempUVs[2].y = mInnerUV.get_yMin();
					mTempUVs[1].y = mInnerUV.get_yMax();
					mTempUVs[0].y = mOuterUV.get_yMax();
				}
				else
				{
					mTempPos[1].y = mTempPos[0].y + val.y;
					mTempPos[2].y = mTempPos[3].y - val.w;
					mTempUVs[0].y = mOuterUV.get_yMin();
					mTempUVs[1].y = mInnerUV.get_yMin();
					mTempUVs[2].y = mInnerUV.get_yMax();
					mTempUVs[3].y = mOuterUV.get_yMax();
				}
				for (int i = 0; i < 3; i++)
				{
					int num = i + 1;
					for (int j = 0; j < 3; j++)
					{
						if (centerType != 0 || i != 1 || j != 1)
						{
							int num2 = j + 1;
							if (i == 1 && j == 1)
							{
								if (centerType == AdvancedType.Tiled)
								{
									float x = mTempPos[i].x;
									float x2 = mTempPos[num].x;
									float y = mTempPos[j].y;
									float y2 = mTempPos[num2].y;
									float x3 = mTempUVs[i].x;
									float y3 = mTempUVs[j].y;
									for (float num3 = y; num3 < y2; num3 += val2.y)
									{
										float num4 = x;
										float num5 = mTempUVs[num2].y;
										float num6 = num3 + val2.y;
										if (num6 > y2)
										{
											num5 = Mathf.Lerp(y3, num5, (y2 - num3) / val2.y);
											num6 = y2;
										}
										for (; num4 < x2; num4 += val2.x)
										{
											float num7 = num4 + val2.x;
											float num8 = mTempUVs[num].x;
											if (num7 > x2)
											{
												num8 = Mathf.Lerp(x3, num8, (x2 - num4) / val2.x);
												num7 = x2;
											}
											Fill(verts, uvs, cols, num4, num7, num3, num6, x3, num8, y3, num5, Color32.op_Implicit(drawingColor));
										}
									}
								}
								else if (centerType == AdvancedType.Sliced)
								{
									Fill(verts, uvs, cols, mTempPos[i].x, mTempPos[num].x, mTempPos[j].y, mTempPos[num2].y, mTempUVs[i].x, mTempUVs[num].x, mTempUVs[j].y, mTempUVs[num2].y, Color32.op_Implicit(drawingColor));
								}
							}
							else if (i == 1)
							{
								if ((j == 0 && bottomType == AdvancedType.Tiled) || (j == 2 && topType == AdvancedType.Tiled))
								{
									float x4 = mTempPos[i].x;
									float x5 = mTempPos[num].x;
									float y4 = mTempPos[j].y;
									float y5 = mTempPos[num2].y;
									float x6 = mTempUVs[i].x;
									float y6 = mTempUVs[j].y;
									float y7 = mTempUVs[num2].y;
									for (float num9 = x4; num9 < x5; num9 += val2.x)
									{
										float num10 = num9 + val2.x;
										float num11 = mTempUVs[num].x;
										if (num10 > x5)
										{
											num11 = Mathf.Lerp(x6, num11, (x5 - num9) / val2.x);
											num10 = x5;
										}
										Fill(verts, uvs, cols, num9, num10, y4, y5, x6, num11, y6, y7, Color32.op_Implicit(drawingColor));
									}
								}
								else if ((j == 0 && bottomType != 0) || (j == 2 && topType != 0))
								{
									Fill(verts, uvs, cols, mTempPos[i].x, mTempPos[num].x, mTempPos[j].y, mTempPos[num2].y, mTempUVs[i].x, mTempUVs[num].x, mTempUVs[j].y, mTempUVs[num2].y, Color32.op_Implicit(drawingColor));
								}
							}
							else
							{
								switch (j)
								{
								case 1:
									if ((i == 0 && leftType == AdvancedType.Tiled) || (i == 2 && rightType == AdvancedType.Tiled))
									{
										float x7 = mTempPos[i].x;
										float x8 = mTempPos[num].x;
										float y8 = mTempPos[j].y;
										float y9 = mTempPos[num2].y;
										float x9 = mTempUVs[i].x;
										float x10 = mTempUVs[num].x;
										float y10 = mTempUVs[j].y;
										for (float num12 = y8; num12 < y9; num12 += val2.y)
										{
											float num13 = mTempUVs[num2].y;
											float num14 = num12 + val2.y;
											if (num14 > y9)
											{
												num13 = Mathf.Lerp(y10, num13, (y9 - num12) / val2.y);
												num14 = y9;
											}
											Fill(verts, uvs, cols, x7, x8, num12, num14, x9, x10, y10, num13, Color32.op_Implicit(drawingColor));
										}
									}
									else if ((i == 0 && leftType != 0) || (i == 2 && rightType != 0))
									{
										Fill(verts, uvs, cols, mTempPos[i].x, mTempPos[num].x, mTempPos[j].y, mTempPos[num2].y, mTempUVs[i].x, mTempUVs[num].x, mTempUVs[j].y, mTempUVs[num2].y, Color32.op_Implicit(drawingColor));
									}
									break;
								case 0:
									if (bottomType == AdvancedType.Invisible)
									{
										goto default;
									}
									goto IL_0b12;
								default:
									{
										if ((j != 2 || topType == AdvancedType.Invisible) && (i != 0 || leftType == AdvancedType.Invisible) && (i != 2 || rightType == AdvancedType.Invisible))
										{
											break;
										}
										goto IL_0b12;
									}
									IL_0b12:
									Fill(verts, uvs, cols, mTempPos[i].x, mTempPos[num].x, mTempPos[j].y, mTempPos[num2].y, mTempUVs[i].x, mTempUVs[num].x, mTempUVs[j].y, mTempUVs[num2].y, Color32.op_Implicit(drawingColor));
									break;
								}
							}
						}
					}
				}
			}
		}
	}

	private static bool RadialCut(Vector2[] xy, Vector2[] uv, float fill, bool invert, int corner)
	{
		if (fill < 0.001f)
		{
			return false;
		}
		if ((corner & 1) == 1)
		{
			invert = !invert;
		}
		if (!invert && fill > 0.999f)
		{
			return true;
		}
		float num = Mathf.Clamp01(fill);
		if (invert)
		{
			num = 1f - num;
		}
		num *= 1.57079637f;
		float cos = Mathf.Cos(num);
		float sin = Mathf.Sin(num);
		RadialCut(xy, cos, sin, invert, corner);
		RadialCut(uv, cos, sin, invert, corner);
		return true;
	}

	private static void RadialCut(Vector2[] xy, float cos, float sin, bool invert, int corner)
	{
		int num = NGUIMath.RepeatIndex(corner + 1, 4);
		int num2 = NGUIMath.RepeatIndex(corner + 2, 4);
		int num3 = NGUIMath.RepeatIndex(corner + 3, 4);
		if ((corner & 1) == 1)
		{
			if (sin > cos)
			{
				cos /= sin;
				sin = 1f;
				if (invert)
				{
					xy[num].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
					xy[num2].x = xy[num].x;
				}
			}
			else if (cos > sin)
			{
				sin /= cos;
				cos = 1f;
				if (!invert)
				{
					xy[num2].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
					xy[num3].y = xy[num2].y;
				}
			}
			else
			{
				cos = 1f;
				sin = 1f;
			}
			if (!invert)
			{
				xy[num3].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
			}
			else
			{
				xy[num].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
			}
		}
		else
		{
			if (cos > sin)
			{
				sin /= cos;
				cos = 1f;
				if (!invert)
				{
					xy[num].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
					xy[num2].y = xy[num].y;
				}
			}
			else if (sin > cos)
			{
				cos /= sin;
				sin = 1f;
				if (invert)
				{
					xy[num2].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
					xy[num3].x = xy[num2].x;
				}
			}
			else
			{
				cos = 1f;
				sin = 1f;
			}
			if (invert)
			{
				xy[num3].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
			}
			else
			{
				xy[num].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
			}
		}
	}

	private static void Fill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols, float v0x, float v1x, float v0y, float v1y, float u0x, float u1x, float u0y, float u1y, Color col)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		verts.Add(new Vector3(v0x, v0y));
		verts.Add(new Vector3(v0x, v1y));
		verts.Add(new Vector3(v1x, v1y));
		verts.Add(new Vector3(v1x, v0y));
		uvs.Add(new Vector2(u0x, u0y));
		uvs.Add(new Vector2(u0x, u1y));
		uvs.Add(new Vector2(u1x, u1y));
		uvs.Add(new Vector2(u1x, u0y));
		cols.Add(Color32.op_Implicit(col));
		cols.Add(Color32.op_Implicit(col));
		cols.Add(Color32.op_Implicit(col));
		cols.Add(Color32.op_Implicit(col));
	}
}
