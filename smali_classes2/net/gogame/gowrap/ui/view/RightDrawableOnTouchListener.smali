.class public abstract Lnet/gogame/gowrap/ui/view/RightDrawableOnTouchListener;
.super Ljava/lang/Object;
.source "RightDrawableOnTouchListener.java"

# interfaces
.implements Landroid/view/View$OnTouchListener;


# static fields
.field public static final DEFAULT_FUZZ:I = 0xa


# instance fields
.field private final fuzz:I


# direct methods
.method public constructor <init>()V
    .locals 1

    const/16 v0, 0xa

    .line 14
    invoke-direct {p0, v0}, Lnet/gogame/gowrap/ui/view/RightDrawableOnTouchListener;-><init>(I)V

    return-void
.end method

.method public constructor <init>(I)V
    .locals 0

    .line 18
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 20
    iput p1, p0, Lnet/gogame/gowrap/ui/view/RightDrawableOnTouchListener;->fuzz:I

    return-void
.end method


# virtual methods
.method public abstract onDrawableTouch(Landroid/view/MotionEvent;)Z
.end method

.method public onTouch(Landroid/view/View;Landroid/view/MotionEvent;)Z
    .locals 4

    .line 25
    instance-of v0, p1, Landroid/widget/TextView;

    if-eqz v0, :cond_1

    .line 26
    move-object v0, p1

    check-cast v0, Landroid/widget/TextView;

    .line 27
    invoke-virtual {v0}, Landroid/widget/TextView;->getCompoundDrawables()[Landroid/graphics/drawable/Drawable;

    move-result-object v0

    const/4 v1, 0x0

    .line 29
    array-length v2, v0

    const/4 v3, 0x4

    if-ne v2, v3, :cond_0

    const/4 v1, 0x2

    .line 30
    aget-object v1, v0, v1

    .line 32
    :cond_0
    invoke-virtual {p2}, Landroid/view/MotionEvent;->getAction()I

    move-result v0

    if-nez v0, :cond_1

    if-eqz v1, :cond_1

    .line 33
    invoke-virtual {p2}, Landroid/view/MotionEvent;->getX()F

    move-result v0

    float-to-int v0, v0

    .line 34
    invoke-virtual {p2}, Landroid/view/MotionEvent;->getY()F

    move-result v2

    float-to-int v2, v2

    .line 35
    invoke-virtual {v1}, Landroid/graphics/drawable/Drawable;->getBounds()Landroid/graphics/Rect;

    move-result-object v1

    .line 36
    invoke-virtual {p1}, Landroid/view/View;->getRight()I

    move-result v3

    invoke-virtual {v1}, Landroid/graphics/Rect;->width()I

    move-result v1

    sub-int/2addr v3, v1

    iget v1, p0, Lnet/gogame/gowrap/ui/view/RightDrawableOnTouchListener;->fuzz:I

    sub-int/2addr v3, v1

    if-lt v0, v3, :cond_1

    .line 37
    invoke-virtual {p1}, Landroid/view/View;->getRight()I

    move-result v1

    invoke-virtual {p1}, Landroid/view/View;->getPaddingRight()I

    move-result v3

    sub-int/2addr v1, v3

    iget v3, p0, Lnet/gogame/gowrap/ui/view/RightDrawableOnTouchListener;->fuzz:I

    add-int/2addr v1, v3

    if-gt v0, v1, :cond_1

    .line 38
    invoke-virtual {p1}, Landroid/view/View;->getPaddingTop()I

    move-result v0

    iget v1, p0, Lnet/gogame/gowrap/ui/view/RightDrawableOnTouchListener;->fuzz:I

    sub-int/2addr v0, v1

    if-lt v2, v0, :cond_1

    .line 39
    invoke-virtual {p1}, Landroid/view/View;->getHeight()I

    move-result v0

    invoke-virtual {p1}, Landroid/view/View;->getPaddingBottom()I

    move-result p1

    sub-int/2addr v0, p1

    iget p1, p0, Lnet/gogame/gowrap/ui/view/RightDrawableOnTouchListener;->fuzz:I

    add-int/2addr v0, p1

    if-gt v2, v0, :cond_1

    .line 40
    invoke-virtual {p0, p2}, Lnet/gogame/gowrap/ui/view/RightDrawableOnTouchListener;->onDrawableTouch(Landroid/view/MotionEvent;)Z

    move-result p1

    return p1

    :cond_1
    const/4 p1, 0x0

    return p1
.end method
