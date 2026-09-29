// Decompiled with JetBrains decompiler
// Type: BallisticLineRenderer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class BallisticLineRenderer : MonoBehaviour
{
  private const int lineDivide = 30;
  private LineRenderer lineRenderer;
  private BulletData bulletData;

  private void Start()
  {
    Transform transform = ResourceUtility.Realizes((Object) MonoBehaviourSingleton<InGameLinkResourcesCommon>.I.bulletLine, MonoBehaviourSingleton<StageObjectManager>.I._transform);
    if (Object.op_Equality((Object) transform, (Object) null))
      return;
    this.lineRenderer = ((Component) transform).GetComponent<LineRenderer>();
    if (!Object.op_Inequality((Object) this.lineRenderer, (Object) null))
      return;
    ((Renderer) this.lineRenderer).enabled = false;
    this.lineRenderer.startColor = Color.yellow;
    this.lineRenderer.endColor = Color.yellow;
    ((Renderer) this.lineRenderer).material.renderQueue = 3001;
  }

  public void SetBulletData(BulletData bullet) => this.bulletData = bullet;

  public void SetVisible(bool enabled) => ((Renderer) this.lineRenderer).enabled = enabled;

  public void UpdateLine(Vector3 shotPos, Vector3 shotVec)
  {
    if (Object.op_Equality((Object) this.lineRenderer, (Object) null) || !((Renderer) this.lineRenderer).enabled || Object.op_Equality((Object) this.bulletData, (Object) null))
      return;
    ((Vector3) ref shotVec).Normalize();
    float num1 = 0.0f;
    float num2 = 0.0f;
    float num3 = 0.0f;
    if (this.bulletData.type == BulletData.BULLET_TYPE.FALL)
    {
      num1 = this.bulletData.dataFall.gravityRate;
      num2 = this.bulletData.dataFall.gravityStartTime;
      num3 = this.bulletData.data.speed;
    }
    else if (this.bulletData.type == BulletData.BULLET_TYPE.CANNONBALL)
    {
      num1 = this.bulletData.dataCannonball.gravityRate;
      num2 = this.bulletData.dataCannonball.gravityStartTime;
      num3 = this.bulletData.data.speed;
    }
    Vector3 vector3_1 = Vector3.op_Multiply(Physics.gravity, num1);
    this.lineRenderer.positionCount = 30;
    this.lineRenderer.SetPosition(0, shotPos);
    float num4 = this.bulletData.data.appearTime / 30f;
    float num5 = 0.0f;
    for (int index = 1; index < 30; ++index)
    {
      if ((double) num5 <= (double) num2)
        num5 += num4 * 0.1f;
      else
        num5 += num4;
      Vector3 vector3_2 = Vector3.op_Addition(shotPos, Vector3.op_Multiply(Vector3.op_Multiply(shotVec, num3), num5));
      if ((double) num5 >= (double) num2)
        vector3_2 = Vector3.op_Addition(vector3_2, Vector3.op_Division(Vector3.op_Multiply(Vector3.op_Multiply(vector3_1, num5 - num2), num5 - num2), 2f));
      this.lineRenderer.SetPosition(index, vector3_2);
    }
  }

  private void OnDestroy()
  {
    if (!Object.op_Inequality((Object) this.lineRenderer, (Object) null))
      return;
    Object.Destroy((Object) ((Component) this.lineRenderer).gameObject);
    this.lineRenderer = (LineRenderer) null;
  }
}
