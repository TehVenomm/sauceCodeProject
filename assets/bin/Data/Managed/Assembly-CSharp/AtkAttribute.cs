using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class AtkAttribute
{
	[Tooltip("通常")]
	public float normal;

	[Tooltip("炎")]
	public float fire;

	[Tooltip("氷")]
	[FormerlySerializedAs("ice")]
	public float water;

	[Tooltip("雷")]
	[FormerlySerializedAs("wind")]
	public float thunder;

	[Tooltip("土")]
	public float soil;

	[Tooltip("光")]
	public float light;

	[Tooltip("闇")]
	public float dark;

	private float[] baseElementTolerances = new float[6];

	private int[,] ElementToleranceScrollTable;

	public void Set(float targetValue)
	{
		normal = (fire = (water = (thunder = (soil = (light = (dark = targetValue))))));
	}

	public void Copy(AtkAttribute srcAtk)
	{
		normal = srcAtk.normal;
		fire = srcAtk.fire;
		water = srcAtk.water;
		thunder = srcAtk.thunder;
		soil = srcAtk.soil;
		light = srcAtk.light;
		dark = srcAtk.dark;
	}

	public void Mul(AtkAttribute val)
	{
		normal *= val.normal;
		fire *= val.fire;
		water *= val.water;
		thunder *= val.thunder;
		soil *= val.soil;
		light *= val.light;
		dark *= val.dark;
	}

	public void Mul(float val)
	{
		normal *= val;
		fire *= val;
		water *= val;
		thunder *= val;
		soil *= val;
		light *= val;
		dark *= val;
	}

	public void Div(float val)
	{
		if (val != 0f)
		{
			normal /= val;
			fire /= val;
			water /= val;
			thunder /= val;
			soil /= val;
			light /= val;
			dark /= val;
		}
	}

	public void Add(AtkAttribute val)
	{
		if (val != null)
		{
			normal += val.normal;
			fire += val.fire;
			water += val.water;
			thunder += val.thunder;
			soil += val.soil;
			light += val.light;
			dark += val.dark;
		}
	}

	public void AddRate(float rate)
	{
		normal += rate;
		fire += rate;
		water += rate;
		thunder += rate;
		soil += rate;
		light += rate;
		dark += rate;
	}

	public void Sub(AtkAttribute val)
	{
		normal -= val.normal;
		fire -= val.fire;
		water -= val.water;
		thunder -= val.thunder;
		soil -= val.soil;
		light -= val.light;
		dark -= val.dark;
	}

	public void ChangeElementType(ELEMENT_TYPE type)
	{
		float num = normal;
		switch (type)
		{
		case ELEMENT_TYPE.FIRE:
			num += fire;
			break;
		case ELEMENT_TYPE.WATER:
			num += water;
			break;
		case ELEMENT_TYPE.THUNDER:
			num += thunder;
			break;
		case ELEMENT_TYPE.SOIL:
			num += soil;
			break;
		case ELEMENT_TYPE.LIGHT:
			num += light;
			break;
		case ELEMENT_TYPE.DARK:
			num += dark;
			break;
		}
		Mul(0f);
		switch (type)
		{
		case ELEMENT_TYPE.FIRE:
			fire = num;
			break;
		case ELEMENT_TYPE.WATER:
			water = num;
			break;
		case ELEMENT_TYPE.THUNDER:
			thunder = num;
			break;
		case ELEMENT_TYPE.SOIL:
			soil = num;
			break;
		case ELEMENT_TYPE.LIGHT:
			light = num;
			break;
		case ELEMENT_TYPE.DARK:
			dark = num;
			break;
		default:
			normal = num;
			break;
		}
	}

	public ELEMENT_TYPE GetElementType()
	{
		ELEMENT_TYPE result = ELEMENT_TYPE.MAX;
		float num = 0f;
		if (fire > num)
		{
			result = ELEMENT_TYPE.FIRE;
			num = fire;
		}
		if (water > num)
		{
			result = ELEMENT_TYPE.WATER;
			num = water;
		}
		if (thunder > num)
		{
			result = ELEMENT_TYPE.THUNDER;
			num = thunder;
		}
		if (soil > num)
		{
			result = ELEMENT_TYPE.SOIL;
			num = soil;
		}
		if (light > num)
		{
			result = ELEMENT_TYPE.LIGHT;
			num = light;
		}
		if (dark > num)
		{
			result = ELEMENT_TYPE.DARK;
			num = dark;
		}
		return result;
	}

	public ELEMENT_TYPE GetAntiElementType()
	{
		ELEMENT_TYPE result = ELEMENT_TYPE.MAX;
		float num = 1f;
		if (normal < num)
		{
			result = ELEMENT_TYPE.MAX;
			num = normal;
		}
		if (fire < num)
		{
			result = ELEMENT_TYPE.FIRE;
			num = fire;
		}
		if (water < num)
		{
			result = ELEMENT_TYPE.WATER;
			num = water;
		}
		if (thunder < num)
		{
			result = ELEMENT_TYPE.THUNDER;
			num = thunder;
		}
		if (soil < num)
		{
			result = ELEMENT_TYPE.SOIL;
			num = soil;
		}
		if (light < num)
		{
			result = ELEMENT_TYPE.LIGHT;
			num = light;
		}
		if (dark < num)
		{
			result = ELEMENT_TYPE.DARK;
			num = dark;
		}
		return result;
	}

	public void AddElementValueWithCheck(float targetValue)
	{
		if (fire > 0f)
		{
			fire += targetValue;
		}
		if (water > 0f)
		{
			water += targetValue;
		}
		if (thunder > 0f)
		{
			thunder += targetValue;
		}
		if (soil > 0f)
		{
			soil += targetValue;
		}
		if (light > 0f)
		{
			light += targetValue;
		}
		if (dark > 0f)
		{
			dark += targetValue;
		}
	}

	public void AddElementOnly(float targetValue)
	{
		fire += targetValue;
		water += targetValue;
		thunder += targetValue;
		soil += targetValue;
		light += targetValue;
		dark += targetValue;
	}

	public void AddAll(float targetValue)
	{
		normal += targetValue;
		fire += targetValue;
		water += targetValue;
		thunder += targetValue;
		soil += targetValue;
		light += targetValue;
		dark += targetValue;
	}

	public void MulElementOnly(float targetValue)
	{
		fire *= targetValue;
		water *= targetValue;
		thunder *= targetValue;
		soil *= targetValue;
		light *= targetValue;
		dark *= targetValue;
	}

	public void SubElementOnly(float targetValue)
	{
		fire -= targetValue;
		if (fire < 0f)
		{
			fire = 0f;
		}
		water -= targetValue;
		if (water < 0f)
		{
			water = 0f;
		}
		thunder -= targetValue;
		if (thunder < 0f)
		{
			thunder = 0f;
		}
		soil -= targetValue;
		if (soil < 0f)
		{
			soil = 0f;
		}
		light -= targetValue;
		if (light < 0f)
		{
			light = 0f;
		}
		dark -= targetValue;
		if (dark < 0f)
		{
			dark = 0f;
		}
	}

	public void CheckMinus()
	{
		if (normal < 0f)
		{
			normal = 0f;
		}
		if (fire < 0f)
		{
			fire = 0f;
		}
		if (water < 0f)
		{
			water = 0f;
		}
		if (thunder < 0f)
		{
			thunder = 0f;
		}
		if (soil < 0f)
		{
			soil = 0f;
		}
		if (light < 0f)
		{
			light = 0f;
		}
		if (dark < 0f)
		{
			dark = 0f;
		}
	}

	public void SetTargetElemetAll(float val)
	{
		SetTargetElement(ELEMENT_TYPE.FIRE, val);
		SetTargetElement(ELEMENT_TYPE.WATER, val);
		SetTargetElement(ELEMENT_TYPE.THUNDER, val);
		SetTargetElement(ELEMENT_TYPE.SOIL, val);
		SetTargetElement(ELEMENT_TYPE.LIGHT, val);
		SetTargetElement(ELEMENT_TYPE.DARK, val);
	}

	public void SetTargetElement(ELEMENT_TYPE type, float val)
	{
		switch (type)
		{
		case ELEMENT_TYPE.FIRE:
			fire = val;
			break;
		case ELEMENT_TYPE.WATER:
			water = val;
			break;
		case ELEMENT_TYPE.THUNDER:
			thunder = val;
			break;
		case ELEMENT_TYPE.SOIL:
			soil = val;
			break;
		case ELEMENT_TYPE.LIGHT:
			light = val;
			break;
		case ELEMENT_TYPE.DARK:
			dark = val;
			break;
		}
	}

	public void AddTargetElement(ELEMENT_TYPE type, float val)
	{
		switch (type)
		{
		case ELEMENT_TYPE.FIRE:
			fire += val;
			break;
		case ELEMENT_TYPE.WATER:
			water += val;
			break;
		case ELEMENT_TYPE.THUNDER:
			thunder += val;
			break;
		case ELEMENT_TYPE.SOIL:
			soil += val;
			break;
		case ELEMENT_TYPE.LIGHT:
			light += val;
			break;
		case ELEMENT_TYPE.DARK:
			dark += val;
			break;
		}
	}

	public float CalcTotal()
	{
		return normal + fire + water + thunder + soil + light + dark;
	}

	public void InitializeElementTolerance(ConverteElementToleranceTable[] convTable)
	{
		int num = convTable.Length;
		if (num > 0)
		{
			ElementToleranceScrollTable = new int[num, 6];
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < 6; j++)
				{
					ElementToleranceScrollTable[i, j] = convTable[i].GetChangeElement(j);
				}
			}
		}
		else
		{
			ElementToleranceScrollTable = new int[5, 6]
			{
				{
					0,
					1,
					2,
					3,
					4,
					5
				},
				{
					1,
					2,
					3,
					0,
					4,
					5
				},
				{
					2,
					3,
					0,
					1,
					4,
					5
				},
				{
					3,
					0,
					1,
					2,
					4,
					5
				},
				{
					0,
					1,
					2,
					3,
					5,
					4
				}
			};
		}
		baseElementTolerances = new float[6];
		baseElementTolerances[0] = fire;
		baseElementTolerances[1] = water;
		baseElementTolerances[2] = thunder;
		baseElementTolerances[3] = soil;
		baseElementTolerances[4] = light;
		baseElementTolerances[5] = dark;
	}

	public void ChangeElementTolerance(int scroll)
	{
		int length = ElementToleranceScrollTable.GetLength(0);
		if (scroll < 0 || scroll >= length)
		{
			Log.Error("scroll is out of range!! ");
			return;
		}
		int i = 0;
		for (int num = 6; i < num; i++)
		{
			int num2 = ElementToleranceScrollTable[scroll, i];
			SetTargetElement((ELEMENT_TYPE)i, baseElementTolerances[num2]);
		}
	}

	public override string ToString()
	{
		return $"[AtkAttribute] normal:{normal} fire:{fire} water:{water} thunder:{thunder} soil:{soil} light:{light} dark:{dark}";
	}

	public string ToShortString()
	{
		string empty = string.Empty;
		string text = empty;
		empty = text + "n:" + normal + " ";
		text = empty;
		empty = text + "f:" + fire + " ";
		text = empty;
		empty = text + "w:" + water + " ";
		text = empty;
		empty = text + "t:" + thunder + " ";
		text = empty;
		empty = text + "s:" + soil + " ";
		text = empty;
		empty = text + "l:" + light + " ";
		text = empty;
		return text + "d:" + dark + " ";
	}
}
