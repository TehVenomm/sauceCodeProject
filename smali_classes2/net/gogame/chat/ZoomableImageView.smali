.class public Lnet/gogame/chat/ZoomableImageView;
.super Landroid/widget/ImageView;
.source "ZoomableImageView.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/chat/ZoomableImageView$ScaleListener;
    }
.end annotation


# static fields
.field private static final CLICK:I = 0x3

.field private static final DRAG:I = 0x1

.field private static final NONE:I = 0x0

.field private static final ZOOM:I = 0x2


# instance fields
.field private bmHeight:F

.field private bmWidth:F

.field private bottom:F

.field private context:Landroid/content/Context;

.field private height:F

.field private last:Landroid/graphics/PointF;

.field private m:[F

.field private mScaleDetector:Landroid/view/ScaleGestureDetector;

.field private matrix:Landroid/graphics/Matrix;

.field private maxScale:F

.field private minScale:F

.field private mode:I

.field private origHeight:F

.field private origWidth:F

.field private redundantXSpace:F

.field private redundantYSpace:F

.field private right:F

.field private saveScale:F

.field private start:Landroid/graphics/PointF;

.field private width:F


# direct methods
.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 3

    .line 37
    invoke-direct {p0, p1, p2}, Landroid/widget/ImageView;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 19
    new-instance p2, Landroid/graphics/Matrix;

    invoke-direct {p2}, Landroid/graphics/Matrix;-><init>()V

    iput-object p2, p0, Lnet/gogame/chat/ZoomableImageView;->matrix:Landroid/graphics/Matrix;

    const/4 p2, 0x0

    .line 20
    iput p2, p0, Lnet/gogame/chat/ZoomableImageView;->mode:I

    .line 22
    new-instance p2, Landroid/graphics/PointF;

    invoke-direct {p2}, Landroid/graphics/PointF;-><init>()V

    iput-object p2, p0, Lnet/gogame/chat/ZoomableImageView;->last:Landroid/graphics/PointF;

    .line 23
    new-instance p2, Landroid/graphics/PointF;

    invoke-direct {p2}, Landroid/graphics/PointF;-><init>()V

    iput-object p2, p0, Lnet/gogame/chat/ZoomableImageView;->start:Landroid/graphics/PointF;

    const/high16 p2, 0x3f800000    # 1.0f

    .line 24
    iput p2, p0, Lnet/gogame/chat/ZoomableImageView;->minScale:F

    const/high16 v0, 0x40800000    # 4.0f

    .line 25
    iput v0, p0, Lnet/gogame/chat/ZoomableImageView;->maxScale:F

    .line 30
    iput p2, p0, Lnet/gogame/chat/ZoomableImageView;->saveScale:F

    const/4 v0, 0x1

    .line 38
    invoke-super {p0, v0}, Landroid/widget/ImageView;->setClickable(Z)V

    .line 39
    iput-object p1, p0, Lnet/gogame/chat/ZoomableImageView;->context:Landroid/content/Context;

    .line 40
    new-instance v0, Landroid/view/ScaleGestureDetector;

    new-instance v1, Lnet/gogame/chat/ZoomableImageView$ScaleListener;

    const/4 v2, 0x0

    invoke-direct {v1, p0, v2}, Lnet/gogame/chat/ZoomableImageView$ScaleListener;-><init>(Lnet/gogame/chat/ZoomableImageView;Lnet/gogame/chat/ZoomableImageView$1;)V

    invoke-direct {v0, p1, v1}, Landroid/view/ScaleGestureDetector;-><init>(Landroid/content/Context;Landroid/view/ScaleGestureDetector$OnScaleGestureListener;)V

    iput-object v0, p0, Lnet/gogame/chat/ZoomableImageView;->mScaleDetector:Landroid/view/ScaleGestureDetector;

    .line 41
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView;->matrix:Landroid/graphics/Matrix;

    invoke-virtual {p1, p2, p2}, Landroid/graphics/Matrix;->setTranslate(FF)V

    const/16 p1, 0x9

    .line 42
    new-array p1, p1, [F

    iput-object p1, p0, Lnet/gogame/chat/ZoomableImageView;->m:[F

    .line 43
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView;->matrix:Landroid/graphics/Matrix;

    invoke-virtual {p0, p1}, Lnet/gogame/chat/ZoomableImageView;->setImageMatrix(Landroid/graphics/Matrix;)V

    .line 44
    sget-object p1, Landroid/widget/ImageView$ScaleType;->MATRIX:Landroid/widget/ImageView$ScaleType;

    invoke-virtual {p0, p1}, Lnet/gogame/chat/ZoomableImageView;->setScaleType(Landroid/widget/ImageView$ScaleType;)V

    .line 46
    new-instance p1, Lnet/gogame/chat/ZoomableImageView$1;

    invoke-direct {p1, p0}, Lnet/gogame/chat/ZoomableImageView$1;-><init>(Lnet/gogame/chat/ZoomableImageView;)V

    invoke-virtual {p0, p1}, Lnet/gogame/chat/ZoomableImageView;->setOnTouchListener(Landroid/view/View$OnTouchListener;)V

    return-void
.end method

.method static synthetic access$100(Lnet/gogame/chat/ZoomableImageView;)Landroid/view/ScaleGestureDetector;
    .locals 0

    .line 13
    iget-object p0, p0, Lnet/gogame/chat/ZoomableImageView;->mScaleDetector:Landroid/view/ScaleGestureDetector;

    return-object p0
.end method

.method static synthetic access$1000(Lnet/gogame/chat/ZoomableImageView;)F
    .locals 0

    .line 13
    iget p0, p0, Lnet/gogame/chat/ZoomableImageView;->origHeight:F

    return p0
.end method

.method static synthetic access$1100(Lnet/gogame/chat/ZoomableImageView;)F
    .locals 0

    .line 13
    iget p0, p0, Lnet/gogame/chat/ZoomableImageView;->width:F

    return p0
.end method

.method static synthetic access$1200(Lnet/gogame/chat/ZoomableImageView;)F
    .locals 0

    .line 13
    iget p0, p0, Lnet/gogame/chat/ZoomableImageView;->bottom:F

    return p0
.end method

.method static synthetic access$1202(Lnet/gogame/chat/ZoomableImageView;F)F
    .locals 0

    .line 13
    iput p1, p0, Lnet/gogame/chat/ZoomableImageView;->bottom:F

    return p1
.end method

.method static synthetic access$1300(Lnet/gogame/chat/ZoomableImageView;)F
    .locals 0

    .line 13
    iget p0, p0, Lnet/gogame/chat/ZoomableImageView;->height:F

    return p0
.end method

.method static synthetic access$1400(Lnet/gogame/chat/ZoomableImageView;)F
    .locals 0

    .line 13
    iget p0, p0, Lnet/gogame/chat/ZoomableImageView;->right:F

    return p0
.end method

.method static synthetic access$1402(Lnet/gogame/chat/ZoomableImageView;F)F
    .locals 0

    .line 13
    iput p1, p0, Lnet/gogame/chat/ZoomableImageView;->right:F

    return p1
.end method

.method static synthetic access$1500(Lnet/gogame/chat/ZoomableImageView;)F
    .locals 0

    .line 13
    iget p0, p0, Lnet/gogame/chat/ZoomableImageView;->maxScale:F

    return p0
.end method

.method static synthetic access$1600(Lnet/gogame/chat/ZoomableImageView;)F
    .locals 0

    .line 13
    iget p0, p0, Lnet/gogame/chat/ZoomableImageView;->redundantXSpace:F

    return p0
.end method

.method static synthetic access$1700(Lnet/gogame/chat/ZoomableImageView;)F
    .locals 0

    .line 13
    iget p0, p0, Lnet/gogame/chat/ZoomableImageView;->redundantYSpace:F

    return p0
.end method

.method static synthetic access$200(Lnet/gogame/chat/ZoomableImageView;)[F
    .locals 0

    .line 13
    iget-object p0, p0, Lnet/gogame/chat/ZoomableImageView;->m:[F

    return-object p0
.end method

.method static synthetic access$300(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/Matrix;
    .locals 0

    .line 13
    iget-object p0, p0, Lnet/gogame/chat/ZoomableImageView;->matrix:Landroid/graphics/Matrix;

    return-object p0
.end method

.method static synthetic access$400(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/PointF;
    .locals 0

    .line 13
    iget-object p0, p0, Lnet/gogame/chat/ZoomableImageView;->last:Landroid/graphics/PointF;

    return-object p0
.end method

.method static synthetic access$500(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/PointF;
    .locals 0

    .line 13
    iget-object p0, p0, Lnet/gogame/chat/ZoomableImageView;->start:Landroid/graphics/PointF;

    return-object p0
.end method

.method static synthetic access$600(Lnet/gogame/chat/ZoomableImageView;)I
    .locals 0

    .line 13
    iget p0, p0, Lnet/gogame/chat/ZoomableImageView;->mode:I

    return p0
.end method

.method static synthetic access$602(Lnet/gogame/chat/ZoomableImageView;I)I
    .locals 0

    .line 13
    iput p1, p0, Lnet/gogame/chat/ZoomableImageView;->mode:I

    return p1
.end method

.method static synthetic access$700(Lnet/gogame/chat/ZoomableImageView;)F
    .locals 0

    .line 13
    iget p0, p0, Lnet/gogame/chat/ZoomableImageView;->saveScale:F

    return p0
.end method

.method static synthetic access$702(Lnet/gogame/chat/ZoomableImageView;F)F
    .locals 0

    .line 13
    iput p1, p0, Lnet/gogame/chat/ZoomableImageView;->saveScale:F

    return p1
.end method

.method static synthetic access$800(Lnet/gogame/chat/ZoomableImageView;)F
    .locals 0

    .line 13
    iget p0, p0, Lnet/gogame/chat/ZoomableImageView;->minScale:F

    return p0
.end method

.method static synthetic access$900(Lnet/gogame/chat/ZoomableImageView;)F
    .locals 0

    .line 13
    iget p0, p0, Lnet/gogame/chat/ZoomableImageView;->origWidth:F

    return p0
.end method


# virtual methods
.method protected onMeasure(II)V
    .locals 2

    .line 159
    invoke-super {p0, p1, p2}, Landroid/widget/ImageView;->onMeasure(II)V

    .line 160
    invoke-static {p1}, Landroid/view/View$MeasureSpec;->getSize(I)I

    move-result p1

    int-to-float p1, p1

    iput p1, p0, Lnet/gogame/chat/ZoomableImageView;->width:F

    .line 161
    invoke-static {p2}, Landroid/view/View$MeasureSpec;->getSize(I)I

    move-result p1

    int-to-float p1, p1

    iput p1, p0, Lnet/gogame/chat/ZoomableImageView;->height:F

    .line 163
    iget p1, p0, Lnet/gogame/chat/ZoomableImageView;->width:F

    iget p2, p0, Lnet/gogame/chat/ZoomableImageView;->bmWidth:F

    div-float/2addr p1, p2

    .line 164
    iget p2, p0, Lnet/gogame/chat/ZoomableImageView;->height:F

    iget v0, p0, Lnet/gogame/chat/ZoomableImageView;->bmHeight:F

    div-float/2addr p2, v0

    .line 165
    invoke-static {p1, p2}, Ljava/lang/Math;->min(FF)F

    move-result p1

    .line 166
    iget-object p2, p0, Lnet/gogame/chat/ZoomableImageView;->matrix:Landroid/graphics/Matrix;

    invoke-virtual {p2, p1, p1}, Landroid/graphics/Matrix;->setScale(FF)V

    .line 167
    iget-object p2, p0, Lnet/gogame/chat/ZoomableImageView;->matrix:Landroid/graphics/Matrix;

    invoke-virtual {p0, p2}, Lnet/gogame/chat/ZoomableImageView;->setImageMatrix(Landroid/graphics/Matrix;)V

    const/high16 p2, 0x3f800000    # 1.0f

    .line 168
    iput p2, p0, Lnet/gogame/chat/ZoomableImageView;->saveScale:F

    .line 171
    iget p2, p0, Lnet/gogame/chat/ZoomableImageView;->height:F

    iget v0, p0, Lnet/gogame/chat/ZoomableImageView;->bmHeight:F

    mul-float v0, v0, p1

    sub-float/2addr p2, v0

    iput p2, p0, Lnet/gogame/chat/ZoomableImageView;->redundantYSpace:F

    .line 172
    iget p2, p0, Lnet/gogame/chat/ZoomableImageView;->width:F

    iget v0, p0, Lnet/gogame/chat/ZoomableImageView;->bmWidth:F

    mul-float p1, p1, v0

    sub-float/2addr p2, p1

    iput p2, p0, Lnet/gogame/chat/ZoomableImageView;->redundantXSpace:F

    .line 173
    iget p1, p0, Lnet/gogame/chat/ZoomableImageView;->redundantYSpace:F

    const/high16 p2, 0x40000000    # 2.0f

    div-float/2addr p1, p2

    iput p1, p0, Lnet/gogame/chat/ZoomableImageView;->redundantYSpace:F

    .line 174
    iget p1, p0, Lnet/gogame/chat/ZoomableImageView;->redundantXSpace:F

    div-float/2addr p1, p2

    iput p1, p0, Lnet/gogame/chat/ZoomableImageView;->redundantXSpace:F

    .line 176
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView;->matrix:Landroid/graphics/Matrix;

    iget v0, p0, Lnet/gogame/chat/ZoomableImageView;->redundantXSpace:F

    iget v1, p0, Lnet/gogame/chat/ZoomableImageView;->redundantYSpace:F

    invoke-virtual {p1, v0, v1}, Landroid/graphics/Matrix;->postTranslate(FF)Z

    .line 178
    iget p1, p0, Lnet/gogame/chat/ZoomableImageView;->width:F

    iget v0, p0, Lnet/gogame/chat/ZoomableImageView;->redundantXSpace:F

    mul-float v0, v0, p2

    sub-float/2addr p1, v0

    iput p1, p0, Lnet/gogame/chat/ZoomableImageView;->origWidth:F

    .line 179
    iget p1, p0, Lnet/gogame/chat/ZoomableImageView;->height:F

    iget v0, p0, Lnet/gogame/chat/ZoomableImageView;->redundantYSpace:F

    mul-float v0, v0, p2

    sub-float/2addr p1, v0

    iput p1, p0, Lnet/gogame/chat/ZoomableImageView;->origHeight:F

    .line 180
    iget p1, p0, Lnet/gogame/chat/ZoomableImageView;->width:F

    iget v0, p0, Lnet/gogame/chat/ZoomableImageView;->saveScale:F

    mul-float p1, p1, v0

    iget v0, p0, Lnet/gogame/chat/ZoomableImageView;->width:F

    sub-float/2addr p1, v0

    iget v0, p0, Lnet/gogame/chat/ZoomableImageView;->redundantXSpace:F

    mul-float v0, v0, p2

    iget v1, p0, Lnet/gogame/chat/ZoomableImageView;->saveScale:F

    mul-float v0, v0, v1

    sub-float/2addr p1, v0

    iput p1, p0, Lnet/gogame/chat/ZoomableImageView;->right:F

    .line 181
    iget p1, p0, Lnet/gogame/chat/ZoomableImageView;->height:F

    iget v0, p0, Lnet/gogame/chat/ZoomableImageView;->saveScale:F

    mul-float p1, p1, v0

    iget v0, p0, Lnet/gogame/chat/ZoomableImageView;->height:F

    sub-float/2addr p1, v0

    iget v0, p0, Lnet/gogame/chat/ZoomableImageView;->redundantYSpace:F

    mul-float v0, v0, p2

    iget p2, p0, Lnet/gogame/chat/ZoomableImageView;->saveScale:F

    mul-float v0, v0, p2

    sub-float/2addr p1, v0

    iput p1, p0, Lnet/gogame/chat/ZoomableImageView;->bottom:F

    .line 182
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView;->matrix:Landroid/graphics/Matrix;

    invoke-virtual {p0, p1}, Lnet/gogame/chat/ZoomableImageView;->setImageMatrix(Landroid/graphics/Matrix;)V

    return-void
.end method

.method public setImageBitmap(Landroid/graphics/Bitmap;)V
    .locals 1

    .line 148
    invoke-super {p0, p1}, Landroid/widget/ImageView;->setImageBitmap(Landroid/graphics/Bitmap;)V

    .line 149
    invoke-virtual {p1}, Landroid/graphics/Bitmap;->getWidth()I

    move-result v0

    int-to-float v0, v0

    iput v0, p0, Lnet/gogame/chat/ZoomableImageView;->bmWidth:F

    .line 150
    invoke-virtual {p1}, Landroid/graphics/Bitmap;->getHeight()I

    move-result p1

    int-to-float p1, p1

    iput p1, p0, Lnet/gogame/chat/ZoomableImageView;->bmHeight:F

    return-void
.end method

.method public setMaxZoom(F)V
    .locals 0

    .line 154
    iput p1, p0, Lnet/gogame/chat/ZoomableImageView;->maxScale:F

    return-void
.end method
