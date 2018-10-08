using rhyme;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
	private class Pool_List_Point : rymTPool<List<Point>>
	{
	}

	private class Pool_Point : rymTPool<Point>
	{
	}

	private class Point
	{
		public Vector3 beginPos;

		public Vector3 centerPos;

		public Vector3 endPos;

		public float time;
	}

	public enum AXIS
	{
		X,
		Y,
		Z
	}

	public static Func<Trail, bool> onQueryDestroy;

	public static bool settingFixedUpdate = true;

	public Shader shader;

	public Texture texture;

	public Transform targetCameraTransform;

	public bool emit = true;

	public bool fixedUpdate = true;

	public int polygonNum = 64;

	public float life = 0.25f;

	public float delayTime;

	public float checkMoveLength = 0.01f;

	public float divideLength = 1f;

	public int divideAngle = 10;

	public Vector3 offset = Vector3.zero;

	public AXIS axis = AXIS.Z;

	public bool billboard;

	public float width = 1f;

	public bool reverseV;

	public Color color = Color.white;

	public float timeForDelete = 0.25f;

	public Color colorForDelete = new Color(1f, 1f, 1f, 0f);

	public Bounds bounds = new Bounds(Vector3.zero, new Vector3(20f, 20f, 20f));

	private Transform _transform;

	private Material material;

	private Mesh mesh;

	private Vector3[] vertices;

	private Color[] colors;

	private Vector2[] uvs;

	private int[] triangles;

	private List<Point> pointList;

	private Point prevPoint1;

	private Point prevPoint2;

	private float time;

	private float deleteTime;

	private bool autoDelete;

	private bool dirty;

	private Vector3 lastBeginPos = new Vector3(3.40282347E+38f, 3.40282347E+38f, 3.40282347E+38f);

	private Vector3 lastEndPos = new Vector3(3.40282347E+38f, 3.40282347E+38f, 3.40282347E+38f);

	private int pauseStep;

	public bool pause
	{
		get
		{
			return pauseStep != 0;
		}
		set
		{
			if (!autoDelete)
			{
				if (value)
				{
					pauseStep = 1;
				}
				else
				{
					pauseStep = 0;
				}
			}
		}
	}

	public static void ClearPoolObjects()
	{
		rymTPool<List<Point>>.Clear();
		rymTPool<Point>.Clear();
	}

	private void Awake()
	{
		fixedUpdate = settingFixedUpdate;
		pointList = rymTPool<List<Point>>.Get();
		if (pointList == null)
		{
			Debug.LogError("Not found pointList (Pool_List_Point.Get())");
		}
	}

	private void Start()
	{
		if (!((UnityEngine.Object)shader == (UnityEngine.Object)null))
		{
			if ((UnityEngine.Object)targetCameraTransform == (UnityEngine.Object)null)
			{
				targetCameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
			}
			material = new Material(shader);
			material.mainTexture = texture;
			mesh = new Mesh();
			int num = polygonNum * 2 + 2;
			int num2 = polygonNum * 6;
			vertices = new Vector3[num];
			colors = new Color[num];
			uvs = new Vector2[num];
			triangles = new int[num2];
			Color color = this.color;
			Color[] array = colors;
			Vector2[] array2 = uvs;
			float num3 = reverseV ? 1f : 0f;
			float y = 1f - num3;
			for (int i = 0; i < num; i += 2)
			{
				array[i] = (array[i + 1] = color);
				array2[i].y = num3;
				array2[i + 1].y = y;
			}
			int num4 = 0;
			for (int j = 0; j < num2; j += 6)
			{
				triangles[j] = 0 + num4;
				triangles[j + 1] = 1 + num4;
				triangles[j + 2] = 2 + num4;
				triangles[j + 3] = 1 + num4;
				triangles[j + 4] = 3 + num4;
				triangles[j + 5] = 2 + num4;
				num4 += 2;
			}
			mesh.vertices = vertices;
			mesh.uv = uvs;
			mesh.colors = colors;
			mesh.triangles = triangles;
			mesh.MarkDynamic();
			Reset();
		}
	}

	private void ClearPointList()
	{
		if (pointList != null)
		{
			int i = 0;
			for (int count = pointList.Count; i < count; i++)
			{
				Point obj = pointList[i];
				rymTPool<Point>.Release(ref obj);
				pointList[i] = null;
			}
			pointList.Clear();
		}
	}

	private void OnDestroy()
	{
		if (pointList != null)
		{
			ClearPointList();
			rymTPool<List<Point>>.Release(ref pointList);
		}
		if ((UnityEngine.Object)mesh != (UnityEngine.Object)null)
		{
			UnityEngine.Object.DestroyImmediate(mesh);
			mesh = null;
		}
		if ((UnityEngine.Object)material != (UnityEngine.Object)null)
		{
			UnityEngine.Object.DestroyImmediate(material);
			material = null;
		}
	}

	public void Clear()
	{
		if (deleteTime > 0f && colors != null)
		{
			Color color = this.color;
			Color[] array = colors;
			int i = 0;
			for (int num = array.Length; i < num; i += 2)
			{
				array[i] = (array[i + 1] = color);
			}
			mesh.colors = colors;
		}
		time = 0f;
		deleteTime = 0f;
		ClearPointList();
		prevPoint1 = null;
		prevPoint2 = null;
		lastBeginPos = new Vector3(3.40282347E+38f, 3.40282347E+38f, 3.40282347E+38f);
		lastEndPos = new Vector3(3.40282347E+38f, 3.40282347E+38f, 3.40282347E+38f);
		autoDelete = false;
		pauseStep = 0;
	}

	public void Reset()
	{
		_transform = base.transform;
		Clear();
	}

	public void SetAutoDelete()
	{
		autoDelete = true;
		pauseStep = 0;
		StartDeleteFade();
	}

	public void StartDeleteFade()
	{
		emit = false;
		if (timeForDelete > 0f)
		{
			deleteTime = time;
		}
	}

	private void LateUpdate()
	{
		if (!((UnityEngine.Object)_transform == (UnityEngine.Object)null))
		{
			if (!fixedUpdate)
			{
				UpdateTrail(Time.deltaTime);
			}
			if (pointList != null)
			{
				Draw();
			}
		}
	}

	private void FixedUpdate()
	{
		if (!((UnityEngine.Object)_transform == (UnityEngine.Object)null) && fixedUpdate)
		{
			UpdateTrail(Time.fixedDeltaTime);
		}
	}

	private void UpdateTrail(float dt)
	{
		if (pauseStep > 0)
		{
			if (pauseStep == 2)
			{
				return;
			}
			pauseStep = 2;
		}
		time += dt;
		if (!(time < delayTime))
		{
			while (pointList.Count != 0)
			{
				Point obj = pointList[0];
				if (time < obj.time + life)
				{
					break;
				}
				if (obj == prevPoint2)
				{
					prevPoint2 = null;
				}
				else if (obj == prevPoint1)
				{
					prevPoint1 = null;
				}
				pointList.RemoveAt(0);
				rymTPool<Point>.Release(ref obj);
				dirty = true;
			}
			if (autoDelete)
			{
				if ((deleteTime > 0f && time >= deleteTime + timeForDelete) || pointList.Count == 0)
				{
					_transform = null;
					if (onQueryDestroy != null)
					{
						if (onQueryDestroy(this))
						{
							UnityEngine.Object.DestroyImmediate(base.gameObject);
						}
					}
					else
					{
						UnityEngine.Object.DestroyImmediate(base.gameObject);
					}
				}
			}
			else if (emit)
			{
				Emit();
			}
		}
	}

	private void Emit()
	{
		Transform transform = _transform;
		Vector3 a = (axis == AXIS.X) ? transform.right : ((axis != AXIS.Y) ? transform.forward : transform.up);
		Vector3 vector = transform.rotation * offset;
		float num = width;
		Vector3 lossyScale = transform.lossyScale;
		if (lossyScale != Vector3.one)
		{
			float num2 = (lossyScale.x + lossyScale.y + lossyScale.z) * 0.333333343f;
			num *= num2;
			vector *= num2;
		}
		Vector3 vector2 = transform.position + vector;
		Vector3 vector3 = a * num + vector2;
		if (checkMoveLength > 0f)
		{
			float num3 = checkMoveLength * checkMoveLength;
			if ((vector2 - lastBeginPos).sqrMagnitude < num3 && (vector3 - lastEndPos).sqrMagnitude < num3)
			{
				return;
			}
		}
		Point point = rymTPool<Point>.Get();
		point.beginPos = vector2;
		point.endPos = vector3;
		point.centerPos = (vector2 + vector3) * 0.5f;
		point.time = time;
		lastBeginPos = vector2;
		lastEndPos = vector3;
		if (prevPoint1 != null && prevPoint2 != null)
		{
			Point point2 = prevPoint2;
			Point point3 = prevPoint1;
			int num4 = 0;
			Vector3 vector4 = point3.centerPos - point2.centerPos;
			Vector3 vector5 = point.centerPos - point3.centerPos;
			if ((float)divideAngle != 0f && vector4 != vector5)
			{
				num4 = (int)(Vector3.Angle(vector4, vector5) / (float)divideAngle);
			}
			if (divideLength != 0f)
			{
				int num5 = (int)(vector5.magnitude / divideLength);
				if (num4 < num5)
				{
					num4 = num5;
				}
			}
			if (num4 > 0)
			{
				rymUtil.CalcWayOfSpline(out Vector3 out_vec, ref point2.beginPos, ref point3.beginPos, ref point.beginPos);
				rymUtil.CalcWayOfSpline(out Vector3 out_vec2, ref point2.endPos, ref point3.endPos, ref point.endPos);
				num4++;
				float num6 = 1f / (float)num4;
				float num7 = 0.5f / (float)num4;
				if (pointList.Count == 0)
				{
					AddPoint(point2);
					for (int i = 1; i < num4; i++)
					{
						Point point4 = rymTPool<Point>.Get();
						point4.time = (point3.time - point2.time) * ((float)i * num6) + point2.time;
						float t = (float)i * num7;
						rymUtil.CalcSpline(out point4.beginPos, ref point2.beginPos, ref out_vec, ref point.beginPos, t);
						rymUtil.CalcSpline(out point4.endPos, ref point2.endPos, ref out_vec2, ref point.endPos, t);
						point4.centerPos = (point4.beginPos + point4.endPos) * 0.5f;
						AddPoint(point4);
					}
					AddPoint(point3);
				}
				for (int j = 1; j < num4; j++)
				{
					Point point5 = rymTPool<Point>.Get();
					point5.time = (point.time - point3.time) * ((float)j * num6) + point3.time;
					float t2 = (float)j * num7 + 0.5f;
					rymUtil.CalcSpline(out point5.beginPos, ref point2.beginPos, ref out_vec, ref point.beginPos, t2);
					rymUtil.CalcSpline(out point5.endPos, ref point2.endPos, ref out_vec2, ref point.endPos, t2);
					point5.centerPos = (point5.beginPos + point5.endPos) * 0.5f;
					AddPoint(point5);
				}
			}
			else if (pointList.Count == 0)
			{
				AddPoint(point2);
				AddPoint(point3);
			}
			AddPoint(point);
			dirty = true;
		}
		prevPoint2 = prevPoint1;
		prevPoint1 = point;
	}

	private void AddPoint(Point point)
	{
		if (pointList.Count >= polygonNum)
		{
			Point obj = pointList[0];
			if (obj == prevPoint2)
			{
				prevPoint2 = null;
			}
			else if (obj == prevPoint1)
			{
				prevPoint1 = null;
			}
			pointList.RemoveAt(0);
			rymTPool<Point>.Release(ref obj);
		}
		pointList.Add(point);
	}

	private void Draw()
	{
		Vector3[] array = vertices;
		Vector2[] array2 = uvs;
		if (pointList.Count >= 2)
		{
			if (dirty || billboard)
			{
				int num = 0;
				float num2 = pointList[0].time;
				float num3 = pointList[pointList.Count - 1].time - num2;
				float num4 = 1f / num3;
				Point point = null;
				int i = 0;
				if (!billboard)
				{
					while (num < pointList.Count)
					{
						point = pointList[num];
						array[i] = point.beginPos;
						array[i + 1] = point.endPos;
						array2[i].x = (array2[i + 1].x = 1f - (point.time - num2) * num4);
						num++;
						i += 2;
					}
				}
				else
				{
					float num5 = width * 0.5f;
					Vector3 lossyScale = _transform.lossyScale;
					if (lossyScale != Vector3.one)
					{
						num5 *= (lossyScale.x + lossyScale.y + lossyScale.z) * 0.333333343f;
					}
					int num6 = num + 1;
					Point point2 = null;
					Vector3 position = targetCameraTransform.position;
					Vector3 a = Vector3.zero;
					while (num6 < pointList.Count)
					{
						point = pointList[num];
						point2 = pointList[num6];
						a = Vector3.Cross(position - point.centerPos, point2.centerPos - point.centerPos).normalized;
						array[i] = point.centerPos - a * num5;
						array[i + 1] = point.centerPos + a * num5;
						array2[i].x = (array2[i + 1].x = 1f - (point.time - num2) * num4);
						num++;
						num6++;
						i += 2;
					}
					array[i] = point2.centerPos - a * num5;
					array[i + 1] = point2.centerPos + a * num5;
					array2[i].x = (array2[i + 1].x = 1f - (point2.time - num2) * num4);
					i += 2;
				}
				Vector3 vector = array[i - 2];
				for (int num7 = array.Length; i < num7; i += 2)
				{
					array[i] = (array[i + 1] = vector);
				}
				mesh.vertices = array;
				mesh.uv = array2;
				dirty = false;
			}
			if (deleteTime > 0f)
			{
				float num8 = (time - deleteTime) / timeForDelete;
				if (num8 > 1f)
				{
					num8 = 1f;
				}
				Color color = Color.Lerp(this.color, colorForDelete, num8);
				Color[] array3 = colors;
				int j = 0;
				for (int num9 = array.Length; j < num9; j += 2)
				{
					array3[j] = (array3[j + 1] = color);
				}
				mesh.colors = array3;
			}
			Bounds bounds = this.bounds;
			bounds.center = _transform.position;
			mesh.bounds = bounds;
			Graphics.DrawMesh(mesh, Vector3.zero, Quaternion.identity, material, base.gameObject.layer);
		}
	}
}
