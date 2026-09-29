.class Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabGeometry;
.super Ljava/lang/Object;
.source "CustomTabbelPanel.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "TabGeometry"
.end annotation


# instance fields
.field private final contentTop:F

.field private final labelBounds:Landroid/graphics/RectF;

.field private final path:Landroid/graphics/Path;

.field private final tabBounds:Landroid/graphics/RectF;


# direct methods
.method public constructor <init>(Landroid/graphics/Path;Landroid/graphics/RectF;Landroid/graphics/RectF;F)V
    .locals 0

    .line 759
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 761
    iput-object p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabGeometry;->path:Landroid/graphics/Path;

    .line 762
    iput-object p2, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabGeometry;->tabBounds:Landroid/graphics/RectF;

    .line 763
    iput-object p3, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabGeometry;->labelBounds:Landroid/graphics/RectF;

    .line 764
    iput p4, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabGeometry;->contentTop:F

    return-void
.end method


# virtual methods
.method public getContentTop()F
    .locals 1

    .line 780
    iget v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabGeometry;->contentTop:F

    return v0
.end method

.method public getLabelBounds()Landroid/graphics/RectF;
    .locals 1

    .line 776
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabGeometry;->labelBounds:Landroid/graphics/RectF;

    return-object v0
.end method

.method public getPath()Landroid/graphics/Path;
    .locals 1

    .line 768
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabGeometry;->path:Landroid/graphics/Path;

    return-object v0
.end method

.method public getTabBounds()Landroid/graphics/RectF;
    .locals 1

    .line 772
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabGeometry;->tabBounds:Landroid/graphics/RectF;

    return-object v0
.end method
