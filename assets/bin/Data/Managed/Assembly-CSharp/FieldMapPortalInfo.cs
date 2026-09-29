// Decompiled with JetBrains decompiler
// Type: FieldMapPortalInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;

#nullable disable
public class FieldMapPortalInfo
{
  public FieldMapTable.PortalTableData portalData;
  public FieldPortal fieldPortal;

  public FieldMapPortalInfo(FieldMapTable.PortalTableData _portal, FieldPortal _fieldPortal)
  {
    this.portalData = _portal;
    this.fieldPortal = _fieldPortal;
  }

  public void SetPortalData(FieldMapTable.PortalTableData _portal, FieldPortal _fieldPortal)
  {
    this.portalData = _portal;
    this.fieldPortal = _fieldPortal;
  }

  public void Clear()
  {
    this.portalData = (FieldMapTable.PortalTableData) null;
    this.fieldPortal = (FieldPortal) null;
  }

  public bool IsValid() => this.portalData != null;

  public bool IsFull()
  {
    if (this.portalData == null || this.portalData.portalPoint == 0U)
      return true;
    return this.fieldPortal != null && (long) this.fieldPortal.point >= (long) this.portalData.portalPoint;
  }

  public int GetNowPortalPoint()
  {
    return this.portalData == null || this.fieldPortal == null ? 0 : this.fieldPortal.point;
  }

  public uint GetMaxPortalPoint() => this.portalData == null ? 0U : this.portalData.portalPoint;

  public bool IsAddPortalPoint()
  {
    return !this.IsFull() && FieldManager.IsOpenPortalClearOrder(this.portalData) && this.portalData.isUnlockedTime();
  }
}
