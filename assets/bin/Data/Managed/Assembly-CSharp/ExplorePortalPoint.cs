// Decompiled with JetBrains decompiler
// Type: ExplorePortalPoint
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;

#nullable disable
public class ExplorePortalPoint
{
  public static readonly int USEDFLAG_CLOSED = 0;
  public static readonly int USEDFLAG_OPENED = 1;
  public static readonly int USEDFLAG_PASSED = 2;
  private FieldPortal fieldPortal;
  public int used;

  public int portaiId => this.fieldPortal.pId;

  public int point
  {
    get => this.fieldPortal.point;
    set => this.fieldPortal.point = value;
  }

  public bool closed => ExplorePortalPoint.USEDFLAG_CLOSED == this.used;

  public bool opened => ExplorePortalPoint.USEDFLAG_OPENED == this.used;

  public bool passed => ExplorePortalPoint.USEDFLAG_PASSED == this.used;

  public int linkPortalId { get; private set; }

  public FieldMapTable.PortalTableData portalData { get; private set; }

  public ExplorePortalPoint(FieldMapTable.PortalTableData tableData)
  {
    this.fieldPortal = new FieldPortal();
    this.fieldPortal.pId = (int) tableData.portalID;
    this.used = ExplorePortalPoint.USEDFLAG_CLOSED;
    this.linkPortalId = (int) tableData.linkPortalId;
    this.portalData = tableData;
    if (tableData.portalPoint > 0U)
      return;
    this.used = ExplorePortalPoint.USEDFLAG_PASSED;
  }

  public void UpdatePoint(int point, bool force = false)
  {
    if (!(this.fieldPortal.point < point | force))
      return;
    this.fieldPortal.point = point;
  }

  public void UpdateUsedFlag(int usedFlag)
  {
    if (ExplorePortalPoint.USEDFLAG_CLOSED != usedFlag && ExplorePortalPoint.USEDFLAG_OPENED != usedFlag && ExplorePortalPoint.USEDFLAG_PASSED != usedFlag)
      return;
    this.used = usedFlag;
  }
}
