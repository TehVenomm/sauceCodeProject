.class Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;
.super Ljava/lang/Object;
.source "CustomTabbelPanel.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "TabHolder"
.end annotation


# instance fields
.field private final contentTop:F

.field private final innerPath:Landroid/graphics/Path;

.field private final labelBounds:Landroid/graphics/RectF;

.field private final outerPath:Landroid/graphics/Path;

.field private final tabBounds:Landroid/graphics/RectF;


# direct methods
.method public constructor <init>(Landroid/graphics/Path;Landroid/graphics/Path;Landroid/graphics/RectF;Landroid/graphics/RectF;F)V
    .locals 0

    .line 794
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 796
    iput-object p1, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;->outerPath:Landroid/graphics/Path;

    .line 797
    iput-object p2, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;->innerPath:Landroid/graphics/Path;

    .line 798
    iput-object p3, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;->tabBounds:Landroid/graphics/RectF;

    .line 799
    iput-object p4, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;->labelBounds:Landroid/graphics/RectF;

    .line 800
    iput p5, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;->contentTop:F

    return-void
.end method


# virtual methods
.method public getContentTop()F
    .locals 1

    .line 820
    iget v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;->contentTop:F

    return v0
.end method

.method public getInnerPath()Landroid/graphics/Path;
    .locals 1

    .line 808
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;->innerPath:Landroid/graphics/Path;

    return-object v0
.end method

.method public getLabelBounds()Landroid/graphics/RectF;
    .locals 1

    .line 816
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;->labelBounds:Landroid/graphics/RectF;

    return-object v0
.end method

.method public getOuterPath()Landroid/graphics/Path;
    .locals 1

    .line 804
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;->outerPath:Landroid/graphics/Path;

    return-object v0
.end method

.method public getTabBounds()Landroid/graphics/RectF;
    .locals 1

    .line 812
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$TabHolder;->tabBounds:Landroid/graphics/RectF;

    return-object v0
.end method
