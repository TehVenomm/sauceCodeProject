// Decompiled with JetBrains decompiler
// Type: SlimeController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class SlimeController : MonoBehaviour
{
  [Tooltip("FadeInアニメーション再生時間(秒)")]
  public float fadeInAnimTime = 0.3f;
  [Tooltip("FadeInの色アニメーション再生時間(秒)")]
  public float fadeInColorAnimTime = 0.3f;
  public AnimationCurve animFadeIn;
  [Tooltip("FadeOutアニメーション再生時間(秒)")]
  public float fadeOutAnimTime = 0.6f;
  [Tooltip("FadeOutの色アニメーション再生時間(秒)")]
  public float fadeOutColorAnimTime = 0.6f;
  public AnimationCurve animFadeOut;
  [Tooltip("Crushアニメーション再生時間(秒)")]
  public float crushAnimTime = 0.1f;
  [Tooltip("Crushの色アニメーション再生時間(秒)")]
  public float crushColorAnimTime = 0.4f;
  public AnimationCurve animCrush;
  public AnimationCurve animCrush_temp;
  [Tooltip("ScaleUpDownアニメーション再生時間(秒)")]
  public float scaleUpDownAnimTime = 1f;
  public AnimationCurve animScaleUpDown;
  [Tooltip("ScaleUpアニメーション最大倍率")]
  public float scaleupAnimMaxScale = 2.4f;
  [Tooltip("ScaleUpアニメーション再生時間(秒)")]
  public float scaleupAnimTime = 0.8f;
  public SlimeAnimation slimeAnim;
  private Vector3 targetVector = Vector3.zero;
  private int subdivionsWidth;
  private int subdivionsHeight;
  private ParametricPlane parametricPlane;
  private MeshFilter meshFilter;
  private MeshRenderer meshRenderer;
  private Vector3[] firstVectors;
  private Vector3[] nowVectors;
  private Vector3[] vertWork;
  private float[] inv_lenghts;
  private Vector3 dragPos;
  private Vector3 startPosition = Vector3.zero;
  private float animTime;
  private bool isMeshUpdate;
  private bool isDrag;
  private bool isSlimeStart;
  private const float MOVE_V = 0.15f;
  private const float RAND_VR = 0.1f;
  private const float RAND_V = 5f;
  private const float RAND_W = 100f;

  public float updateAnimTime { set; get; }

  private void Awake()
  {
    this.meshFilter = ((Component) this).GetComponent<MeshFilter>();
    this.meshRenderer = ((Component) this).GetComponent<MeshRenderer>();
  }

  private void Start()
  {
    Color color = ((Renderer) this.meshRenderer).material.color;
    color.a = 0.0f;
    ((Renderer) this.meshRenderer).material.color = color;
    this.SetInvisible();
    this.parametricPlane = ((Component) this).GetComponent<ParametricPlane>();
    this.parametricPlane.CreateMesh();
    this.Initialize();
    this.slimeAnim = new SlimeAnimation(this);
    this.isMeshUpdate = false;
  }

  private void Update()
  {
    if (this.subdivionsHeight <= 0 || this.subdivionsWidth <= 0)
      return;
    if (this.isMeshUpdate)
    {
      if ((double) this.targetVector.y < 0.5 * (double) this.parametricPlane._height)
      {
        this.isDrag = false;
        this.targetVector = Vector3.zero;
      }
      else
        this.isDrag = true;
      this.SmoothingFilter();
      int index = 0;
      for (int length = this.nowVectors.Length; index < length; ++index)
        this.vertWork[index] = this.nowVectors[index];
      this.meshFilter.mesh.vertices = this.vertWork;
    }
    this.TouchSlimeUpdateAnim();
  }

  private void Initialize()
  {
    this.subdivionsWidth = this.parametricPlane._subdivisionsWidth + 1;
    this.subdivionsHeight = this.parametricPlane._subdivisionsHeight + 1;
    this.firstVectors = this.meshFilter.mesh.vertices;
    this.vertWork = new Vector3[this.firstVectors.Length];
    this.inv_lenghts = new float[this.firstVectors.Length];
    this.nowVectors = new Vector3[this.firstVectors.Length];
    int num1 = this.subdivionsWidth / 2;
    int num2 = 0;
    this.dragPos = this.firstVectors[num2 * this.subdivionsWidth + num1];
    int index1 = 0;
    int subdivionsWidth = this.subdivionsWidth;
    int subdivionsHeight = this.subdivionsHeight;
    for (int index2 = 0; index2 < subdivionsHeight; ++index2)
    {
      for (int index3 = 0; index3 < subdivionsWidth; ++index3)
      {
        this.nowVectors[index1] = this.firstVectors[index1];
        int num3 = (num1 - index3) * (num1 - index3) + (num2 - index2) * (num2 - index2);
        this.inv_lenghts[index1] = num3 == 0 ? 1f : 1f / Mathf.Sqrt((float) num3);
        ++index1;
      }
    }
  }

  private void SmoothingFilter()
  {
    if (Vector3.op_Inequality(this.targetVector, Vector3.zero))
      this.SmoothingTargetNotZero();
    else
      this.SmoothingTargetZero();
    this.CountAnimTime();
  }

  private void SmoothingTargetNotZero()
  {
    float num1 = this.targetVector.x - this.dragPos.x;
    float num2 = this.targetVector.y - this.dragPos.y;
    int index = 0;
    for (int length = this.nowVectors.Length; index < length; ++index)
    {
      Vector3 firstVector = this.firstVectors[index];
      float invLenght = this.inv_lenghts[index];
      firstVector.x += num1 * invLenght;
      firstVector.y += num2 * invLenght;
      this.nowVectors[index] = firstVector;
    }
    this.ResetAnimTime();
    if (!Vector3.op_Equality(this.startPosition, Vector3.zero))
      return;
    this.startPosition = ((Component) this).transform.localPosition;
  }

  private void SmoothingTargetZero()
  {
    int index = 0;
    for (int length = this.nowVectors.Length; index < length; ++index)
    {
      Vector3 firstVector = this.firstVectors[index];
      Vector3 nowVector = this.nowVectors[index];
      float num = 1f - this.animTime;
      firstVector.x = (float) ((double) firstVector.x * (double) this.animTime + (double) nowVector.x * (double) num);
      firstVector.y = (float) ((double) firstVector.y * (double) this.animTime + (double) nowVector.y * (double) num);
      this.nowVectors[index] = firstVector;
    }
    if (!Vector3.op_Inequality(this.startPosition, Vector3.zero))
      return;
    this.startPosition = Vector3.zero;
  }

  public void TouchStartSlime()
  {
    this.isSlimeStart = true;
    this.MeshPosInit();
    this.SetVisible();
    this.TouchStartSlimeAnim();
  }

  public void TouchEndSlime()
  {
    this.isSlimeStart = false;
    this.ResetTarget();
    this.TouchEndSlimeAnim();
  }

  public void SetTargetPos(Vector3 target)
  {
    this.targetVector = target;
    this.isMeshUpdate = true;
  }

  private void TouchStartSlimeAnim() => this.slimeAnim.TouchOn();

  private void TouchEndSlimeAnim()
  {
    if (this.isDrag)
    {
      this.slimeAnim.TouchOff();
    }
    else
    {
      this.CrushPolygon();
      this.slimeAnim.Crush();
    }
  }

  private void TouchSlimeUpdateAnim() => this.slimeAnim.Update();

  private void ResetTarget()
  {
    this.targetVector = Vector3.zero;
    this.isMeshUpdate = true;
  }

  public void SetVisible()
  {
    if (((Renderer) this.meshRenderer).enabled)
      return;
    ((Renderer) this.meshRenderer).enabled = true;
  }

  private void MeshPosInit()
  {
    if ((double) this.animTime < 1.0)
    {
      this.animTime = 0.99f;
      this.SmoothingFilter();
    }
    ((Renderer) this.meshRenderer).enabled = true;
  }

  public void SetInvisible()
  {
    if (!((Renderer) this.meshRenderer).enabled)
      return;
    ((Renderer) this.meshRenderer).enabled = false;
  }

  public bool IsVisible() => ((Renderer) this.meshRenderer).enabled;

  private void ResetAnimTime()
  {
    this.animTime = 0.0f;
    this.isMeshUpdate = true;
  }

  private void CountAnimTime()
  {
    if ((double) this.animTime < 1.0)
    {
      this.animTime += this.updateAnimTime;
      if ((double) this.animTime <= 1.0)
        return;
      this.animTime = 1f;
    }
    else
    {
      this.isMeshUpdate = false;
      if (this.isSlimeStart || this.slimeAnim.IsPlaying())
        return;
      this.SetInvisible();
    }
  }

  public bool isDragSlime() => this.isDrag;

  public void CrushPolygon()
  {
    this.ResetAnimTime();
    float num1 = Random.Range(5f, 5.1f);
    float num2 = Random.Range(0.0f, 100f);
    float num3 = (float) (0.15000000596046448 * (double) this.parametricPlane._height * 0.5);
    int index = 0;
    for (int length = this.nowVectors.Length; index < length; ++index)
    {
      Vector3 nowVector = this.nowVectors[index];
      Vector3 firstVector = this.firstVectors[index];
      float num4 = Mathf.Atan2(nowVector.y, nowVector.x);
      float num5 = num3 * (Mathf.Sin(num4 * num1 + num2) + 1f);
      nowVector.x = firstVector.x + Mathf.Cos(num4) * num5;
      nowVector.y = firstVector.y + Mathf.Sin(num4) * num5;
      this.nowVectors[index] = nowVector;
    }
  }

  private void ButtonPolygon(float rate)
  {
    this.ResetAnimTime();
    float num1 = 4f;
    float num2 = 45f;
    float num3 = 0.5f * rate;
    ((Component) this).transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    int index = 0;
    for (int length = this.nowVectors.Length; index < length; ++index)
    {
      Vector3 nowVector = this.nowVectors[index];
      float num4 = Mathf.Atan2(nowVector.y, nowVector.x);
      float num5 = num4 * 57.29578f;
      if ((double) num5 >= -45.0 && 135.0 >= (double) num5)
      {
        Vector3 firstVector = this.firstVectors[index];
        float num6 = (float) ((double) num3 * ((double) Mathf.Sin(num4 * num1 + num2) + 1.0) / 2.0);
        nowVector.x = firstVector.x + Mathf.Cos(num4) * num6;
        nowVector.y = firstVector.y + Mathf.Sin(num4) * num6;
        this.nowVectors[index] = nowVector;
      }
    }
    this.SmoothingFilter();
  }
}
