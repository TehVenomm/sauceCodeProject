using UnityEngine;
using UnityEngine.UI;

namespace gogame
{
	public class FlowLayoutGroup : LayoutGroup
	{
		public enum Constraint
		{
			Flexible,
			FixedColumnCount,
			FixedRowCount
		}

		public enum Corner
		{
			UpperLeft,
			UpperRight,
			LowerLeft,
			LowerRight
		}

		private int cellsPerMainAxis;

		private int actualCellCountX;

		private int actualCellCountY;

		private float lastMax;

		protected Vector2 m_CellSize = new Vector2(100f, 100f);

		[SerializeField]
		protected bool m_Horizontal = true;

		[SerializeField]
		protected Vector2 m_Spacing = Vector2.zero;

		private int positionX;

		private int positionY;

		private float totalHeight;

		private float totalWidth;

		public Vector2 cellSize
		{
			get
			{
				return m_CellSize;
			}
			set
			{
				SetProperty(ref m_CellSize, value);
			}
		}

		public Vector2 spacing
		{
			get
			{
				return m_Spacing;
			}
			set
			{
				SetProperty(ref m_Spacing, value);
			}
		}

		public bool horizontal
		{
			get
			{
				return m_Horizontal;
			}
			set
			{
				SetProperty(ref m_Horizontal, value);
			}
		}

		protected FlowLayoutGroup()
		{
		}

		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			int num = 0;
			int num2 = 0;
			num = 1;
			num2 = Mathf.CeilToInt(Mathf.Sqrt(base.rectChildren.Count));
			SetLayoutInputForAxis((float)base.padding.horizontal + (cellSize.x + spacing.x) * (float)num - spacing.x, (float)base.padding.horizontal + (cellSize.x + spacing.x) * (float)num2 - spacing.x, -1f, 0);
		}

		public override void CalculateLayoutInputVertical()
		{
			int num = 0;
			num = 1;
			float num2 = (float)base.padding.vertical + (cellSize.y + spacing.y) * (float)num - spacing.y;
			SetLayoutInputForAxis(num2, num2, -1f, 1);
		}

		public override void SetLayoutHorizontal()
		{
			SetCellsAlongAxis();
		}

		public override void SetLayoutVertical()
		{
			SetCellsAlongAxis();
		}

		private void SetCellsAlongAxis()
		{
			float x = base.rectTransform.rect.size.x;
			float y = base.rectTransform.rect.size.y;
			int num = 1;
			int num2 = 1;
			num = ((!(cellSize.x + spacing.x <= 0f)) ? Mathf.Max(1, Mathf.FloorToInt((x - (float)base.padding.horizontal + spacing.x + 0.001f) / (cellSize.x + spacing.x))) : int.MaxValue);
			num2 = ((!(cellSize.y + spacing.y <= 0f)) ? Mathf.Max(1, Mathf.FloorToInt((y - (float)base.padding.vertical + spacing.y + 0.001f) / (cellSize.y + spacing.y))) : int.MaxValue);
			cellsPerMainAxis = num;
			actualCellCountX = Mathf.Clamp(num, 1, base.rectChildren.Count);
			actualCellCountY = Mathf.Clamp(num2, 1, Mathf.CeilToInt((float)base.rectChildren.Count / (float)cellsPerMainAxis));
			Vector2 vector = new Vector2((float)actualCellCountX * cellSize.x + (float)(actualCellCountX - 1) * spacing.x, (float)actualCellCountY * cellSize.y + (float)(actualCellCountY - 1) * spacing.y);
			Vector2 vector2 = new Vector2(GetStartOffset(0, vector.x), GetStartOffset(1, vector.y));
			totalWidth = 0f;
			totalHeight = 0f;
			Vector2 zero = Vector2.zero;
			for (int i = 0; i < base.rectChildren.Count; i++)
			{
				SetChildAlongAxis(base.rectChildren[i], 0, vector2.x + totalWidth, base.rectChildren[i].rect.size.x);
				SetChildAlongAxis(base.rectChildren[i], 1, vector2.y + totalHeight, base.rectChildren[i].rect.size.y);
				zero = spacing;
				if (horizontal)
				{
					totalWidth += base.rectChildren[i].rect.width + zero[0];
					if (base.rectChildren[i].rect.height > lastMax)
					{
						lastMax = base.rectChildren[i].rect.height;
					}
					if (i < base.rectChildren.Count - 1 && totalWidth + base.rectChildren[i + 1].rect.width + zero[0] > x - (float)base.padding.horizontal)
					{
						totalWidth = 0f;
						totalHeight += lastMax + zero[1];
						lastMax = 0f;
					}
				}
				else
				{
					totalHeight += base.rectChildren[i].rect.height + zero[1];
					if (base.rectChildren[i].rect.width > lastMax)
					{
						lastMax = base.rectChildren[i].rect.width;
					}
					if (i < base.rectChildren.Count - 1 && totalHeight + base.rectChildren[i + 1].rect.height + zero[1] > y - (float)base.padding.vertical)
					{
						totalHeight = 0f;
						totalWidth += lastMax + zero[0];
						lastMax = 0f;
					}
				}
			}
		}
	}
}
