using System.Collections.Generic;
using UnityEngine;

public class DamageDistanceTable : Singleton<DamageDistanceTable>, IDataTable
{
	public class DamagePoint
	{
		public XorFloat distance;

		public XorFloat rate;
	}

	public class DamageDistanceData
	{
		public uint id;

		public DamagePoint[] points;

		private float max = -1f;

		public float GetRate(float distance)
		{
			DamagePoint damagePoint = points[0];
			DamagePoint damagePoint2 = points[points.Length - 1];
			for (int i = 0; i < points.Length; i++)
			{
				DamagePoint damagePoint3 = points[i];
				if ((float)damagePoint3.distance > distance)
				{
					damagePoint2 = damagePoint3;
					break;
				}
				damagePoint = damagePoint3;
			}
			float num = distance - (float)damagePoint.distance;
			float num2 = (float)damagePoint2.distance - (float)damagePoint.distance;
			float num3 = 1f;
			if (distance <= 0f)
			{
				num3 = 0f;
			}
			else if (num2 > 0f)
			{
				num3 = num / num2;
			}
			return Mathf.Lerp((float)damagePoint.rate, (float)damagePoint2.rate, num3);
		}

		public void CalcMaxRate()
		{
			for (int i = 0; i < points.Length; i++)
			{
				DamagePoint damagePoint = points[i];
				max = Mathf.Max(max, (float)damagePoint.rate);
			}
		}

		public bool IsMaxRate(float distance)
		{
			return GetRate(distance) >= max;
		}
	}

	public const string NT = "id,startRate,distance0,rate0,distance1,rate1,distance2,rate2,distance3,rate3,distance4,rate4,distance5,rate5,distance6,rate6,distance7,rate7,distance8,rate8,distance9,rate9";

	private UIntKeyTable<DamageDistanceData> dataTable;

	public static bool cb(CSVReader csv_reader, DamageDistanceData data, ref uint key)
	{
		data.id = key;
		float num = 0f;
		List<DamagePoint> list = new List<DamagePoint>();
		DamagePoint damagePoint = new DamagePoint();
		damagePoint.distance = 0f;
		csv_reader.Pop(ref damagePoint.rate);
		list.Add(damagePoint);
		for (int i = 0; i < 10; i++)
		{
			DamagePoint damagePoint2 = new DamagePoint();
			CSVReader.PopResult result = csv_reader.Pop(ref damagePoint2.distance);
			CSVReader.PopResult result2 = csv_reader.Pop(ref damagePoint2.rate);
			if (!(bool)result || !(bool)result2 || (float)damagePoint2.distance <= 0f || num >= (float)damagePoint2.distance)
			{
				break;
			}
			num = damagePoint2.distance;
			list.Add(damagePoint2);
		}
		data.points = list.ToArray();
		data.CalcMaxRate();
		return true;
	}

	public void CreateTable(string csv_text)
	{
		dataTable = TableUtility.CreateUIntKeyTable<DamageDistanceData>(csv_text, DamageDistanceTable.cb, "id,startRate,distance0,rate0,distance1,rate1,distance2,rate2,distance3,rate3,distance4,rate4,distance5,rate5,distance6,rate6,distance7,rate7,distance8,rate8,distance9,rate9", null);
		dataTable.TrimExcess();
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddUIntKeyTable(dataTable, csv_text, DamageDistanceTable.cb, "id,startRate,distance0,rate0,distance1,rate1,distance2,rate2,distance3,rate3,distance4,rate4,distance5,rate5,distance6,rate6,distance7,rate7,distance8,rate8,distance9,rate9", null);
	}

	public DamageDistanceData GetData(uint id)
	{
		return dataTable.Get(id);
	}
}
