.class Lnet/gogame/chat/ZoomableImageView$ScaleListener;
.super Landroid/view/ScaleGestureDetector$SimpleOnScaleGestureListener;
.source "ZoomableImageView.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/chat/ZoomableImageView;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "ScaleListener"
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/chat/ZoomableImageView;


# direct methods
.method private constructor <init>(Lnet/gogame/chat/ZoomableImageView;)V
    .locals 0

    .line 185
    iput-object p1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-direct {p0}, Landroid/view/ScaleGestureDetector$SimpleOnScaleGestureListener;-><init>()V

    return-void
.end method

.method synthetic constructor <init>(Lnet/gogame/chat/ZoomableImageView;Lnet/gogame/chat/ZoomableImageView$1;)V
    .locals 0

    .line 185
    invoke-direct {p0, p1}, Lnet/gogame/chat/ZoomableImageView$ScaleListener;-><init>(Lnet/gogame/chat/ZoomableImageView;)V

    return-void
.end method


# virtual methods
.method public onScale(Landroid/view/ScaleGestureDetector;)Z
    .locals 8

    .line 195
    invoke-virtual {p1}, Landroid/view/ScaleGestureDetector;->getScaleFactor()F

    move-result v0

    .line 196
    iget-object v1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v1}, Lnet/gogame/chat/ZoomableImageView;->access$700(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v1

    .line 197
    iget-object v2, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    iget-object v3, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v3}, Lnet/gogame/chat/ZoomableImageView;->access$700(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v3

    mul-float v3, v3, v0

    invoke-static {v2, v3}, Lnet/gogame/chat/ZoomableImageView;->access$702(Lnet/gogame/chat/ZoomableImageView;F)F

    .line 198
    iget-object v2, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v2}, Lnet/gogame/chat/ZoomableImageView;->access$700(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v2

    iget-object v3, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v3}, Lnet/gogame/chat/ZoomableImageView;->access$1500(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v3

    cmpl-float v2, v2, v3

    if-lez v2, :cond_0

    .line 199
    iget-object v0, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    iget-object v2, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v2}, Lnet/gogame/chat/ZoomableImageView;->access$1500(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v2

    invoke-static {v0, v2}, Lnet/gogame/chat/ZoomableImageView;->access$702(Lnet/gogame/chat/ZoomableImageView;F)F

    .line 200
    iget-object v0, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v0}, Lnet/gogame/chat/ZoomableImageView;->access$1500(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v0

    div-float/2addr v0, v1

    goto :goto_0

    .line 201
    :cond_0
    iget-object v2, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v2}, Lnet/gogame/chat/ZoomableImageView;->access$700(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v2

    iget-object v3, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v3}, Lnet/gogame/chat/ZoomableImageView;->access$800(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v3

    cmpg-float v2, v2, v3

    if-gez v2, :cond_1

    .line 202
    iget-object v0, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    iget-object v2, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v2}, Lnet/gogame/chat/ZoomableImageView;->access$800(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v2

    invoke-static {v0, v2}, Lnet/gogame/chat/ZoomableImageView;->access$702(Lnet/gogame/chat/ZoomableImageView;F)F

    .line 203
    iget-object v0, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v0}, Lnet/gogame/chat/ZoomableImageView;->access$800(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v0

    div-float/2addr v0, v1

    .line 205
    :cond_1
    :goto_0
    iget-object v1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    iget-object v2, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v2}, Lnet/gogame/chat/ZoomableImageView;->access$1100(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v2

    iget-object v3, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v3}, Lnet/gogame/chat/ZoomableImageView;->access$700(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v3

    mul-float v2, v2, v3

    iget-object v3, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v3}, Lnet/gogame/chat/ZoomableImageView;->access$1100(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v3

    sub-float/2addr v2, v3

    iget-object v3, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v3}, Lnet/gogame/chat/ZoomableImageView;->access$1600(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v3

    const/high16 v4, 0x40000000    # 2.0f

    mul-float v3, v3, v4

    iget-object v5, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v5}, Lnet/gogame/chat/ZoomableImageView;->access$700(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v5

    mul-float v3, v3, v5

    sub-float/2addr v2, v3

    invoke-static {v1, v2}, Lnet/gogame/chat/ZoomableImageView;->access$1402(Lnet/gogame/chat/ZoomableImageView;F)F

    .line 206
    iget-object v1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    iget-object v2, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v2}, Lnet/gogame/chat/ZoomableImageView;->access$1300(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v2

    iget-object v3, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v3}, Lnet/gogame/chat/ZoomableImageView;->access$700(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v3

    mul-float v2, v2, v3

    iget-object v3, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v3}, Lnet/gogame/chat/ZoomableImageView;->access$1300(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v3

    sub-float/2addr v2, v3

    iget-object v3, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v3}, Lnet/gogame/chat/ZoomableImageView;->access$1700(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v3

    mul-float v3, v3, v4

    iget-object v5, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v5}, Lnet/gogame/chat/ZoomableImageView;->access$700(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v5

    mul-float v3, v3, v5

    sub-float/2addr v2, v3

    invoke-static {v1, v2}, Lnet/gogame/chat/ZoomableImageView;->access$1202(Lnet/gogame/chat/ZoomableImageView;F)F

    .line 207
    iget-object v1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v1}, Lnet/gogame/chat/ZoomableImageView;->access$900(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v1

    iget-object v2, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v2}, Lnet/gogame/chat/ZoomableImageView;->access$700(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v2

    mul-float v1, v1, v2

    iget-object v2, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v2}, Lnet/gogame/chat/ZoomableImageView;->access$1100(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v2

    cmpg-float v1, v1, v2

    const/4 v2, 0x5

    const/4 v3, 0x2

    const/high16 v5, 0x3f800000    # 1.0f

    const/4 v6, 0x0

    if-lez v1, :cond_6

    iget-object v1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v1}, Lnet/gogame/chat/ZoomableImageView;->access$1000(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v1

    iget-object v7, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v7}, Lnet/gogame/chat/ZoomableImageView;->access$700(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v7

    mul-float v1, v1, v7

    iget-object v7, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v7}, Lnet/gogame/chat/ZoomableImageView;->access$1300(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v7

    cmpg-float v1, v1, v7

    if-gtz v1, :cond_2

    goto/16 :goto_2

    .line 230
    :cond_2
    iget-object v1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v1}, Lnet/gogame/chat/ZoomableImageView;->access$300(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/Matrix;

    move-result-object v1

    invoke-virtual {p1}, Landroid/view/ScaleGestureDetector;->getFocusX()F

    move-result v4

    .line 231
    invoke-virtual {p1}, Landroid/view/ScaleGestureDetector;->getFocusY()F

    move-result p1

    .line 230
    invoke-virtual {v1, v0, v0, v4, p1}, Landroid/graphics/Matrix;->postScale(FFFF)Z

    .line 232
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1}, Lnet/gogame/chat/ZoomableImageView;->access$300(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/Matrix;

    move-result-object p1

    iget-object v1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v1}, Lnet/gogame/chat/ZoomableImageView;->access$200(Lnet/gogame/chat/ZoomableImageView;)[F

    move-result-object v1

    invoke-virtual {p1, v1}, Landroid/graphics/Matrix;->getValues([F)V

    .line 233
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1}, Lnet/gogame/chat/ZoomableImageView;->access$200(Lnet/gogame/chat/ZoomableImageView;)[F

    move-result-object p1

    aget p1, p1, v3

    .line 234
    iget-object v1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v1}, Lnet/gogame/chat/ZoomableImageView;->access$200(Lnet/gogame/chat/ZoomableImageView;)[F

    move-result-object v1

    aget v1, v1, v2

    cmpg-float v0, v0, v5

    if-gez v0, :cond_a

    .line 236
    iget-object v0, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v0}, Lnet/gogame/chat/ZoomableImageView;->access$1400(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v0

    neg-float v0, v0

    cmpg-float v0, p1, v0

    if-gez v0, :cond_3

    .line 237
    iget-object v0, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v0}, Lnet/gogame/chat/ZoomableImageView;->access$300(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/Matrix;

    move-result-object v0

    iget-object v2, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v2}, Lnet/gogame/chat/ZoomableImageView;->access$1400(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v2

    add-float/2addr p1, v2

    neg-float p1, p1

    invoke-virtual {v0, p1, v6}, Landroid/graphics/Matrix;->postTranslate(FF)Z

    goto :goto_1

    :cond_3
    cmpl-float v0, p1, v6

    if-lez v0, :cond_4

    .line 239
    iget-object v0, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v0}, Lnet/gogame/chat/ZoomableImageView;->access$300(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/Matrix;

    move-result-object v0

    neg-float p1, p1

    invoke-virtual {v0, p1, v6}, Landroid/graphics/Matrix;->postTranslate(FF)Z

    .line 241
    :cond_4
    :goto_1
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1}, Lnet/gogame/chat/ZoomableImageView;->access$1200(Lnet/gogame/chat/ZoomableImageView;)F

    move-result p1

    neg-float p1, p1

    cmpg-float p1, v1, p1

    if-gez p1, :cond_5

    .line 242
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1}, Lnet/gogame/chat/ZoomableImageView;->access$300(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/Matrix;

    move-result-object p1

    iget-object v0, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v0}, Lnet/gogame/chat/ZoomableImageView;->access$1200(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v0

    add-float/2addr v1, v0

    neg-float v0, v1

    invoke-virtual {p1, v6, v0}, Landroid/graphics/Matrix;->postTranslate(FF)Z

    goto/16 :goto_3

    :cond_5
    cmpl-float p1, v1, v6

    if-lez p1, :cond_a

    .line 244
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1}, Lnet/gogame/chat/ZoomableImageView;->access$300(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/Matrix;

    move-result-object p1

    neg-float v0, v1

    invoke-virtual {p1, v6, v0}, Landroid/graphics/Matrix;->postTranslate(FF)Z

    goto/16 :goto_3

    .line 208
    :cond_6
    :goto_2
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1}, Lnet/gogame/chat/ZoomableImageView;->access$300(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/Matrix;

    move-result-object p1

    iget-object v1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v1}, Lnet/gogame/chat/ZoomableImageView;->access$1100(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v1

    div-float/2addr v1, v4

    iget-object v7, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v7}, Lnet/gogame/chat/ZoomableImageView;->access$1300(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v7

    div-float/2addr v7, v4

    invoke-virtual {p1, v0, v0, v1, v7}, Landroid/graphics/Matrix;->postScale(FFFF)Z

    cmpg-float p1, v0, v5

    if-gez p1, :cond_a

    .line 210
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1}, Lnet/gogame/chat/ZoomableImageView;->access$300(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/Matrix;

    move-result-object p1

    iget-object v1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v1}, Lnet/gogame/chat/ZoomableImageView;->access$200(Lnet/gogame/chat/ZoomableImageView;)[F

    move-result-object v1

    invoke-virtual {p1, v1}, Landroid/graphics/Matrix;->getValues([F)V

    .line 211
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1}, Lnet/gogame/chat/ZoomableImageView;->access$200(Lnet/gogame/chat/ZoomableImageView;)[F

    move-result-object p1

    aget p1, p1, v3

    .line 212
    iget-object v1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v1}, Lnet/gogame/chat/ZoomableImageView;->access$200(Lnet/gogame/chat/ZoomableImageView;)[F

    move-result-object v1

    aget v1, v1, v2

    cmpg-float v0, v0, v5

    if-gez v0, :cond_a

    .line 214
    iget-object v0, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v0}, Lnet/gogame/chat/ZoomableImageView;->access$900(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v0

    iget-object v2, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v2}, Lnet/gogame/chat/ZoomableImageView;->access$700(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v2

    mul-float v0, v0, v2

    invoke-static {v0}, Ljava/lang/Math;->round(F)I

    move-result v0

    int-to-float v0, v0

    iget-object v2, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v2}, Lnet/gogame/chat/ZoomableImageView;->access$1100(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v2

    cmpg-float v0, v0, v2

    if-gez v0, :cond_8

    .line 215
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1}, Lnet/gogame/chat/ZoomableImageView;->access$1200(Lnet/gogame/chat/ZoomableImageView;)F

    move-result p1

    neg-float p1, p1

    cmpg-float p1, v1, p1

    if-gez p1, :cond_7

    .line 216
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1}, Lnet/gogame/chat/ZoomableImageView;->access$300(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/Matrix;

    move-result-object p1

    iget-object v0, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v0}, Lnet/gogame/chat/ZoomableImageView;->access$1200(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v0

    add-float/2addr v1, v0

    neg-float v0, v1

    invoke-virtual {p1, v6, v0}, Landroid/graphics/Matrix;->postTranslate(FF)Z

    goto :goto_3

    :cond_7
    cmpl-float p1, v1, v6

    if-lez p1, :cond_a

    .line 218
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1}, Lnet/gogame/chat/ZoomableImageView;->access$300(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/Matrix;

    move-result-object p1

    neg-float v0, v1

    invoke-virtual {p1, v6, v0}, Landroid/graphics/Matrix;->postTranslate(FF)Z

    goto :goto_3

    .line 221
    :cond_8
    iget-object v0, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v0}, Lnet/gogame/chat/ZoomableImageView;->access$1400(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v0

    neg-float v0, v0

    cmpg-float v0, p1, v0

    if-gez v0, :cond_9

    .line 222
    iget-object v0, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v0}, Lnet/gogame/chat/ZoomableImageView;->access$300(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/Matrix;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v1}, Lnet/gogame/chat/ZoomableImageView;->access$1400(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v1

    add-float/2addr p1, v1

    neg-float p1, p1

    invoke-virtual {v0, p1, v6}, Landroid/graphics/Matrix;->postTranslate(FF)Z

    goto :goto_3

    :cond_9
    cmpl-float v0, p1, v6

    if-lez v0, :cond_a

    .line 224
    iget-object v0, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v0}, Lnet/gogame/chat/ZoomableImageView;->access$300(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/Matrix;

    move-result-object v0

    neg-float p1, p1

    invoke-virtual {v0, p1, v6}, Landroid/graphics/Matrix;->postTranslate(FF)Z

    :cond_a
    :goto_3
    const/4 p1, 0x1

    return p1
.end method

.method public onScaleBegin(Landroid/view/ScaleGestureDetector;)Z
    .locals 1

    .line 189
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$ScaleListener;->this$0:Lnet/gogame/chat/ZoomableImageView;

    const/4 v0, 0x2

    invoke-static {p1, v0}, Lnet/gogame/chat/ZoomableImageView;->access$602(Lnet/gogame/chat/ZoomableImageView;I)I

    const/4 p1, 0x1

    return p1
.end method
