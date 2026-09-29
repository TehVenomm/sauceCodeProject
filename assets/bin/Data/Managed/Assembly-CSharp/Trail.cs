// Decompiled with JetBrains decompiler
// Type: Trail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class Trail : MonoBehaviour
{
  public static Func<Trail, bool> onQueryDestroy;
  public static bool settingFixedUpdate = true;
  public Shader shader;
  public Texture texture;
  public Transform targetCameraTransform;
  public bool emit = true;
  public bool fixedUpdate = true;
  public int polygonNum = 64 /*0x40*/;
  public float life = 0.25f;
  public float delayTime;
  public float checkMoveLength = 0.01f;
  public float divideLength = 1f;
  public int divideAngle = 10;
  public Vector3 offset = Vector3.zero;
  public Trail.AXIS axis = Trail.AXIS.Z;
  public bool billboard;
  public float width = 1f;
  public bool reverseV;
  public Color color = Color.white;
  public float timeForDelete = 0.25f;
  public Color colorForDelete = new Color(1f, 1f, 1f, 0.0f);
  public Bounds bounds = new Bounds(Vector3.zero, new Vector3(20f, 20f, 20f));
  private Transform _transform;
  private Material material;
  private Mesh mesh;
  private Vector3[] vertices;
  private Color[] colors;
  private Vector2[] uvs;
  private int[] triangles;
  private List<Trail.Point> pointList;
  private Trail.Point prevPoint1;
  private Trail.Point prevPoint2;
  private float time;
  private float deleteTime;
  private bool autoDelete;
  private bool dirty;
  private Vector3 lastBeginPos = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
  private Vector3 lastEndPos = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
  private int pauseStep;

  public static void ClearPoolObjects()
  {
    rymTPool<List<Trail.Point>>.Clear();
    rymTPool<Trail.Point>.Clear();
  }

  public bool pause
  {
    get => this.pauseStep != 0;
    set
    {
      if (this.autoDelete)
        return;
      if (value)
        this.pauseStep = 1;
      else
        this.pauseStep = 0;
    }
  }

  private void Awake()
  {
    this.fixedUpdate = Trail.settingFixedUpdate;
    this.pointList = rymTPool<List<Trail.Point>>.Get();
    if (this.pointList != null)
      return;
    Debug.LogError((object) "Not found pointList (Pool_List_Point.Get())");
  }

  private void Start()
  {
    if (Object.op_Equality((Object) this.shader, (Object) null))
      return;
    if (Object.op_Equality((Object) this.targetCameraTransform, (Object) null))
      this.targetCameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
    this.material = new Material(this.shader);
    this.material.mainTexture = this.texture;
    this.mesh = new Mesh();
    int length1 = this.polygonNum * 2 + 2;
    int length2 = this.polygonNum * 6;
    this.vertices = new Vector3[length1];
    this.colors = new Color[length1];
    this.uvs = new Vector2[length1];
    this.triangles = new int[length2];
    Color color = this.color;
    Color[] colors = this.colors;
    Vector2[] uvs = this.uvs;
    float num1 = !this.reverseV ? 0.0f : 1f;
    float num2 = 1f - num1;
    for (int index = 0; index < length1; index += 2)
    {
      colors[index] = colors[index + 1] = color;
      uvs[index].y = num1;
      uvs[index + 1].y = num2;
    }
    int num3 = 0;
    for (int index = 0; index < length2; index += 6)
    {
      this.triangles[index] = num3;
      this.triangles[index + 1] = 1 + num3;
      this.triangles[index + 2] = 2 + num3;
      this.triangles[index + 3] = 1 + num3;
      this.triangles[index + 4] = 3 + num3;
      this.triangles[index + 5] = 2 + num3;
      num3 += 2;
    }
    this.mesh.vertices = this.vertices;
    this.mesh.uv = this.uvs;
    this.mesh.colors = this.colors;
    this.mesh.triangles = this.triangles;
    this.mesh.MarkDynamic();
    this.Reset();
  }

  private void ClearPointList()
  {
    if (this.pointList == null)
      return;
    int index = 0;
    for (int count = this.pointList.Count; index < count; ++index)
    {
      Trail.Point point = this.pointList[index];
      rymTPool<Trail.Point>.Release(ref point);
      this.pointList[index] = (Trail.Point) null;
    }
    this.pointList.Clear();
  }

  private void OnDestroy()
  {
    if (this.pointList != null)
    {
      this.ClearPointList();
      rymTPool<List<Trail.Point>>.Release(ref this.pointList);
    }
    if (Object.op_Inequality((Object) this.mesh, (Object) null))
    {
      Object.DestroyImmediate((Object) this.mesh);
      this.mesh = (Mesh) null;
    }
    if (!Object.op_Inequality((Object) this.material, (Object) null))
      return;
    Object.DestroyImmediate((Object) this.material);
    this.material = (Material) null;
  }

  public void Clear()
  {
    if ((double) this.deleteTime > 0.0 && this.colors != null)
    {
      Color color = this.color;
      Color[] colors = this.colors;
      int index = 0;
      for (int length = colors.Length; index < length; index += 2)
        colors[index] = colors[index + 1] = color;
      this.mesh.colors = this.colors;
    }
    this.time = 0.0f;
    this.deleteTime = 0.0f;
    this.ClearPointList();
    this.prevPoint1 = (Trail.Point) null;
    this.prevPoint2 = (Trail.Point) null;
    this.lastBeginPos = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
    this.lastEndPos = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
    this.autoDelete = false;
    this.pauseStep = 0;
  }

  public void Reset()
  {
    this._transform = ((Component) this).transform;
    this.Clear();
  }

  public void SetAutoDelete()
  {
    this.autoDelete = true;
    this.pauseStep = 0;
    this.StartDeleteFade();
  }

  public void StartDeleteFade()
  {
    this.emit = false;
    if ((double) this.timeForDelete <= 0.0)
      return;
    this.deleteTime = this.time;
  }

  private void LateUpdate()
  {
    if (Object.op_Equality((Object) this._transform, (Object) null))
      return;
    if (!this.fixedUpdate)
      this.UpdateTrail(Time.deltaTime);
    if (this.pointList == null)
      return;
    this.Draw();
  }

  private void FixedUpdate()
  {
    if (Object.op_Equality((Object) this._transform, (Object) null) || !this.fixedUpdate)
      return;
    this.UpdateTrail(Time.fixedDeltaTime);
  }

  private void UpdateTrail(float dt)
  {
    if (this.pauseStep > 0)
    {
      if (this.pauseStep == 2)
        return;
      this.pauseStep = 2;
    }
    this.time += dt;
    if ((double) this.time < (double) this.delayTime)
      return;
    while (this.pointList.Count != 0)
    {
      Trail.Point point = this.pointList[0];
      if ((double) this.time >= (double) point.time + (double) this.life)
      {
        if (point == this.prevPoint2)
          this.prevPoint2 = (Trail.Point) null;
        else if (point == this.prevPoint1)
          this.prevPoint1 = (Trail.Point) null;
        this.pointList.RemoveAt(0);
        rymTPool<Trail.Point>.Release(ref point);
        this.dirty = true;
      }
      else
        break;
    }
    if (this.autoDelete)
    {
      if (((double) this.deleteTime <= 0.0 || (double) this.time < (double) this.deleteTime + (double) this.timeForDelete) && this.pointList.Count != 0)
        return;
      this._transform = (Transform) null;
      if (Trail.onQueryDestroy != null)
      {
        if (!Trail.onQueryDestroy(this))
          return;
        Object.DestroyImmediate((Object) ((Component) this).gameObject);
      }
      else
        Object.DestroyImmediate((Object) ((Component) this).gameObject);
    }
    else
    {
      if (!this.emit)
        return;
      this.Emit();
    }
  }

  private void Emit()
  {
    Transform transform = this._transform;
    Vector3 vector3_1 = this.axis != Trail.AXIS.X ? (this.axis != Trail.AXIS.Y ? transform.forward : transform.up) : transform.right;
    Vector3 vector3_2 = Quaternion.op_Multiply(transform.rotation, this.offset);
    float width = this.width;
    Vector3 lossyScale = transform.lossyScale;
    if (Vector3.op_Inequality(lossyScale, Vector3.one))
    {
      float num = (float) (((double) lossyScale.x + (double) lossyScale.y + (double) lossyScale.z) * 0.3333333432674408);
      width *= num;
      vector3_2 = Vector3.op_Multiply(vector3_2, num);
    }
    Vector3 vector3_3 = Vector3.op_Addition(transform.position, vector3_2);
    Vector3 vector3_4 = Vector3.op_Addition(Vector3.op_Multiply(vector3_1, width), vector3_3);
    if ((double) this.checkMoveLength > 0.0)
    {
      float num = this.checkMoveLength * this.checkMoveLength;
      Vector3 vector3_5 = Vector3.op_Subtraction(vector3_3, this.lastBeginPos);
      if ((double) ((Vector3) ref vector3_5).sqrMagnitude < (double) num)
      {
        Vector3 vector3_6 = Vector3.op_Subtraction(vector3_4, this.lastEndPos);
        if ((double) ((Vector3) ref vector3_6).sqrMagnitude < (double) num)
          return;
      }
    }
    Trail.Point point1 = rymTPool<Trail.Point>.Get();
    point1.beginPos = vector3_3;
    point1.endPos = vector3_4;
    point1.centerPos = Vector3.op_Multiply(Vector3.op_Addition(vector3_3, vector3_4), 0.5f);
    point1.time = this.time;
    this.lastBeginPos = vector3_3;
    this.lastEndPos = vector3_4;
    if (this.prevPoint1 != null && this.prevPoint2 != null)
    {
      Trail.Point prevPoint2 = this.prevPoint2;
      Trail.Point prevPoint1 = this.prevPoint1;
      int num1 = 0;
      Vector3 vector3_7 = Vector3.op_Subtraction(prevPoint1.centerPos, prevPoint2.centerPos);
      Vector3 vector3_8 = Vector3.op_Subtraction(point1.centerPos, prevPoint1.centerPos);
      if ((double) this.divideAngle != 0.0 && Vector3.op_Inequality(vector3_7, vector3_8))
        num1 = (int) ((double) Vector3.Angle(vector3_7, vector3_8) / (double) this.divideAngle);
      if ((double) this.divideLength != 0.0)
      {
        int num2 = (int) ((double) ((Vector3) ref vector3_8).magnitude / (double) this.divideLength);
        if (num1 < num2)
          num1 = num2;
      }
      if (num1 > 0)
      {
        Vector3 vector3_9;
        rymUtil.CalcWayOfSpline(ref vector3_9, ref prevPoint2.beginPos, ref prevPoint1.beginPos, ref point1.beginPos);
        Vector3 vector3_10;
        rymUtil.CalcWayOfSpline(ref vector3_10, ref prevPoint2.endPos, ref prevPoint1.endPos, ref point1.endPos);
        int num3 = num1 + 1;
        float num4 = 1f / (float) num3;
        float num5 = 0.5f / (float) num3;
        if (this.pointList.Count == 0)
        {
          this.AddPoint(prevPoint2);
          for (int index = 1; index < num3; ++index)
          {
            Trail.Point point2 = rymTPool<Trail.Point>.Get();
            point2.time = (float) (((double) prevPoint1.time - (double) prevPoint2.time) * ((double) index * (double) num4)) + prevPoint2.time;
            float num6 = (float) index * num5;
            rymUtil.CalcSpline(ref point2.beginPos, ref prevPoint2.beginPos, ref vector3_9, ref point1.beginPos, num6);
            rymUtil.CalcSpline(ref point2.endPos, ref prevPoint2.endPos, ref vector3_10, ref point1.endPos, num6);
            point2.centerPos = Vector3.op_Multiply(Vector3.op_Addition(point2.beginPos, point2.endPos), 0.5f);
            this.AddPoint(point2);
          }
          this.AddPoint(prevPoint1);
        }
        for (int index = 1; index < num3; ++index)
        {
          Trail.Point point3 = rymTPool<Trail.Point>.Get();
          point3.time = (float) (((double) point1.time - (double) prevPoint1.time) * ((double) index * (double) num4)) + prevPoint1.time;
          float num7 = (float) ((double) index * (double) num5 + 0.5);
          rymUtil.CalcSpline(ref point3.beginPos, ref prevPoint2.beginPos, ref vector3_9, ref point1.beginPos, num7);
          rymUtil.CalcSpline(ref point3.endPos, ref prevPoint2.endPos, ref vector3_10, ref point1.endPos, num7);
          point3.centerPos = Vector3.op_Multiply(Vector3.op_Addition(point3.beginPos, point3.endPos), 0.5f);
          this.AddPoint(point3);
        }
      }
      else if (this.pointList.Count == 0)
      {
        this.AddPoint(prevPoint2);
        this.AddPoint(prevPoint1);
      }
      this.AddPoint(point1);
      this.dirty = true;
    }
    this.prevPoint2 = this.prevPoint1;
    this.prevPoint1 = point1;
  }

  private void AddPoint(Trail.Point point)
  {
    if (this.pointList.Count >= this.polygonNum)
    {
      Trail.Point point1 = this.pointList[0];
      if (point1 == this.prevPoint2)
        this.prevPoint2 = (Trail.Point) null;
      else if (point1 == this.prevPoint1)
        this.prevPoint1 = (Trail.Point) null;
      this.pointList.RemoveAt(0);
      rymTPool<Trail.Point>.Release(ref point1);
    }
    this.pointList.Add(point);
  }

  private void Draw()
  {
    Vector3[] vertices = this.vertices;
    Vector2[] uvs = this.uvs;
    if (this.pointList.Count < 2)
      return;
    if (this.dirty || this.billboard)
    {
      int index1 = 0;
      float time = this.pointList[0].time;
      float num1 = 1f / (this.pointList[this.pointList.Count - 1].time - time);
      int index2 = 0;
      if (!this.billboard)
      {
        while (index1 < this.pointList.Count)
        {
          Trail.Point point = this.pointList[index1];
          vertices[index2] = point.beginPos;
          vertices[index2 + 1] = point.endPos;
          uvs[index2].x = uvs[index2 + 1].x = (float) (1.0 - ((double) point.time - (double) time) * (double) num1);
          ++index1;
          index2 += 2;
        }
      }
      else
      {
        float num2 = this.width * 0.5f;
        Vector3 lossyScale = this._transform.lossyScale;
        if (Vector3.op_Inequality(lossyScale, Vector3.one))
          num2 *= (float) (((double) lossyScale.x + (double) lossyScale.y + (double) lossyScale.z) * 0.3333333432674408);
        int index3 = index1 + 1;
        Trail.Point point1 = (Trail.Point) null;
        Vector3 position = this.targetCameraTransform.position;
        Vector3 vector3_1 = Vector3.zero;
        while (index3 < this.pointList.Count)
        {
          Trail.Point point2 = this.pointList[index1];
          point1 = this.pointList[index3];
          Vector3 vector3_2 = Vector3.Cross(Vector3.op_Subtraction(position, point2.centerPos), Vector3.op_Subtraction(point1.centerPos, point2.centerPos));
          vector3_1 = ((Vector3) ref vector3_2).normalized;
          vertices[index2] = Vector3.op_Subtraction(point2.centerPos, Vector3.op_Multiply(vector3_1, num2));
          vertices[index2 + 1] = Vector3.op_Addition(point2.centerPos, Vector3.op_Multiply(vector3_1, num2));
          uvs[index2].x = uvs[index2 + 1].x = (float) (1.0 - ((double) point2.time - (double) time) * (double) num1);
          ++index1;
          ++index3;
          index2 += 2;
        }
        vertices[index2] = Vector3.op_Subtraction(point1.centerPos, Vector3.op_Multiply(vector3_1, num2));
        vertices[index2 + 1] = Vector3.op_Addition(point1.centerPos, Vector3.op_Multiply(vector3_1, num2));
        uvs[index2].x = uvs[index2 + 1].x = (float) (1.0 - ((double) point1.time - (double) time) * (double) num1);
        index2 += 2;
      }
      Vector3 vector3 = vertices[index2 - 2];
      for (int length = vertices.Length; index2 < length; index2 += 2)
        vertices[index2] = vertices[index2 + 1] = vector3;
      this.mesh.vertices = vertices;
      this.mesh.uv = uvs;
      this.dirty = false;
    }
    if ((double) this.deleteTime > 0.0)
    {
      float num = (this.time - this.deleteTime) / this.timeForDelete;
      if ((double) num > 1.0)
        num = 1f;
      Color color = Color.Lerp(this.color, this.colorForDelete, num);
      Color[] colors = this.colors;
      int index = 0;
      for (int length = vertices.Length; index < length; index += 2)
        colors[index] = colors[index + 1] = color;
      this.mesh.colors = colors;
    }
    Bounds bounds = this.bounds;
    ((Bounds) ref bounds).center = this._transform.position;
    this.mesh.bounds = bounds;
    Graphics.DrawMesh(this.mesh, Vector3.zero, Quaternion.identity, this.material, ((Component) this).gameObject.layer);
  }

  private class Pool_List_Point : rymTPool<List<Trail.Point>>
  {
  }

  private class Pool_Point : rymTPool<Trail.Point>
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
    Z,
  }
}
