.class public Lnet/gogame/gowrap/ui/dpro/view/Margins;
.super Ljava/lang/Object;
.source "Margins.java"


# instance fields
.field public bottom:F

.field public left:F

.field public right:F

.field public top:F


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 11
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public constructor <init>(FFFF)V
    .locals 0

    .line 15
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 17
    iput p1, p0, Lnet/gogame/gowrap/ui/dpro/view/Margins;->left:F

    .line 18
    iput p2, p0, Lnet/gogame/gowrap/ui/dpro/view/Margins;->top:F

    .line 19
    iput p3, p0, Lnet/gogame/gowrap/ui/dpro/view/Margins;->right:F

    .line 20
    iput p4, p0, Lnet/gogame/gowrap/ui/dpro/view/Margins;->bottom:F

    return-void
.end method


# virtual methods
.method public getBottom()F
    .locals 1

    .line 48
    iget v0, p0, Lnet/gogame/gowrap/ui/dpro/view/Margins;->bottom:F

    return v0
.end method

.method public getLeft()F
    .locals 1

    .line 24
    iget v0, p0, Lnet/gogame/gowrap/ui/dpro/view/Margins;->left:F

    return v0
.end method

.method public getRight()F
    .locals 1

    .line 40
    iget v0, p0, Lnet/gogame/gowrap/ui/dpro/view/Margins;->right:F

    return v0
.end method

.method public getTop()F
    .locals 1

    .line 32
    iget v0, p0, Lnet/gogame/gowrap/ui/dpro/view/Margins;->top:F

    return v0
.end method

.method public setBottom(F)V
    .locals 0

    .line 52
    iput p1, p0, Lnet/gogame/gowrap/ui/dpro/view/Margins;->bottom:F

    return-void
.end method

.method public setLeft(F)V
    .locals 0

    .line 28
    iput p1, p0, Lnet/gogame/gowrap/ui/dpro/view/Margins;->left:F

    return-void
.end method

.method public setRight(F)V
    .locals 0

    .line 44
    iput p1, p0, Lnet/gogame/gowrap/ui/dpro/view/Margins;->right:F

    return-void
.end method

.method public setTop(F)V
    .locals 0

    .line 36
    iput p1, p0, Lnet/gogame/gowrap/ui/dpro/view/Margins;->top:F

    return-void
.end method
