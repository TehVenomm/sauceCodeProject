// Decompiled with JetBrains decompiler
// Type: HomeDragonRandomMove
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class HomeDragonRandomMove : MonoBehaviour
{
  private readonly float MAX_DISTANCE = 0.4f;
  private Transform _transform;
  private Vector3 originalPosition;
  private float maxDistance;
  private Vector2 coordX;
  private Vector2 coordY;
  private Vector2 coordZ;
  private Vector2 dirX;
  private Vector2 dirY;
  private Vector2 dirZ;

  private void Awake()
  {
    this._transform = ((Component) this).transform;
    ((Behaviour) this).enabled = false;
  }

  public void Reset()
  {
    this.originalPosition = this._transform.localPosition;
    this.coordX = Vector2.zero;
    this.coordY = Vector2.zero;
    this.coordZ = Vector2.zero;
    Vector2 vector2_1 = new Vector2(Random.value, Random.value);
    this.dirX = ((Vector2) ref vector2_1).normalized;
    Vector2 vector2_2 = new Vector2(Random.value, Random.value);
    this.dirY = ((Vector2) ref vector2_2).normalized;
    Vector2 vector2_3 = new Vector2(Random.value, Random.value);
    this.dirZ = ((Vector2) ref vector2_3).normalized;
    this.maxDistance = 0.0f;
    ((Behaviour) this).enabled = true;
  }

  private void Update()
  {
    this._transform.localPosition = Vector3.op_Addition(this.originalPosition, new Vector3((Mathf.PerlinNoise(this.coordX.x, this.coordX.y) - 0.5f) * this.maxDistance, (Mathf.PerlinNoise(this.coordY.x, this.coordY.y) - 0.5f) * this.maxDistance, (Mathf.PerlinNoise(this.coordZ.x, this.coordZ.y) - 0.5f) * this.maxDistance));
    float num1 = Time.deltaTime * 0.5f;
    this.coordX.x += this.dirX.x * num1;
    this.coordX.y += this.dirX.y * num1;
    float num2 = num1 * 0.9f;
    this.coordY.x += this.dirY.x * num2;
    this.coordY.y += this.dirY.y * num2;
    float num3 = num2 * 0.9f;
    this.coordZ.x += this.dirZ.x * num3;
    this.coordZ.y += this.dirZ.y * num3;
    if ((double) this.maxDistance >= (double) this.MAX_DISTANCE)
      return;
    this.maxDistance += Time.deltaTime * 0.5f;
    if ((double) this.maxDistance <= (double) this.MAX_DISTANCE)
      return;
    this.maxDistance = this.MAX_DISTANCE;
  }
}
