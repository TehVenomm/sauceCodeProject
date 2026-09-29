.class Lnet/gogame/chat/ZoomableImageView$1;
.super Ljava/lang/Object;
.source "ZoomableImageView.java"

# interfaces
.implements Landroid/view/View$OnTouchListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/chat/ZoomableImageView;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/chat/ZoomableImageView;


# direct methods
.method constructor <init>(Lnet/gogame/chat/ZoomableImageView;)V
    .locals 0

    .line 46
    iput-object p1, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onTouch(Landroid/view/View;Landroid/view/MotionEvent;)Z
    .locals 7

    .line 50
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1}, Lnet/gogame/chat/ZoomableImageView;->access$100(Lnet/gogame/chat/ZoomableImageView;)Landroid/view/ScaleGestureDetector;

    move-result-object p1

    invoke-virtual {p1, p2}, Landroid/view/ScaleGestureDetector;->onTouchEvent(Landroid/view/MotionEvent;)Z

    .line 52
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1}, Lnet/gogame/chat/ZoomableImageView;->access$300(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/Matrix;

    move-result-object p1

    iget-object v0, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v0}, Lnet/gogame/chat/ZoomableImageView;->access$200(Lnet/gogame/chat/ZoomableImageView;)[F

    move-result-object v0

    invoke-virtual {p1, v0}, Landroid/graphics/Matrix;->getValues([F)V

    .line 53
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1}, Lnet/gogame/chat/ZoomableImageView;->access$200(Lnet/gogame/chat/ZoomableImageView;)[F

    move-result-object p1

    const/4 v0, 0x2

    aget p1, p1, v0

    .line 54
    iget-object v1, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v1}, Lnet/gogame/chat/ZoomableImageView;->access$200(Lnet/gogame/chat/ZoomableImageView;)[F

    move-result-object v1

    const/4 v2, 0x5

    aget v1, v1, v2

    .line 55
    new-instance v2, Landroid/graphics/PointF;

    invoke-virtual {p2}, Landroid/view/MotionEvent;->getX()F

    move-result v3

    invoke-virtual {p2}, Landroid/view/MotionEvent;->getY()F

    move-result v4

    invoke-direct {v2, v3, v4}, Landroid/graphics/PointF;-><init>(FF)V

    .line 57
    invoke-virtual {p2}, Landroid/view/MotionEvent;->getAction()I

    move-result v3

    const/4 v4, 0x0

    const/4 v5, 0x1

    packed-switch v3, :pswitch_data_0

    :pswitch_0
    goto/16 :goto_5

    .line 135
    :pswitch_1
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1, v4}, Lnet/gogame/chat/ZoomableImageView;->access$602(Lnet/gogame/chat/ZoomableImageView;I)I

    goto/16 :goto_5

    .line 68
    :pswitch_2
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1}, Lnet/gogame/chat/ZoomableImageView;->access$400(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/PointF;

    move-result-object p1

    invoke-virtual {p2}, Landroid/view/MotionEvent;->getX()F

    move-result v1

    invoke-virtual {p2}, Landroid/view/MotionEvent;->getY()F

    move-result p2

    invoke-virtual {p1, v1, p2}, Landroid/graphics/PointF;->set(FF)V

    .line 69
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1}, Lnet/gogame/chat/ZoomableImageView;->access$500(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/PointF;

    move-result-object p1

    iget-object p2, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p2}, Lnet/gogame/chat/ZoomableImageView;->access$400(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/PointF;

    move-result-object p2

    invoke-virtual {p1, p2}, Landroid/graphics/PointF;->set(Landroid/graphics/PointF;)V

    .line 70
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1, v0}, Lnet/gogame/chat/ZoomableImageView;->access$602(Lnet/gogame/chat/ZoomableImageView;I)I

    goto/16 :goto_5

    .line 77
    :pswitch_3
    iget-object p2, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p2}, Lnet/gogame/chat/ZoomableImageView;->access$600(Lnet/gogame/chat/ZoomableImageView;)I

    move-result p2

    if-eq p2, v0, :cond_0

    iget-object p2, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p2}, Lnet/gogame/chat/ZoomableImageView;->access$600(Lnet/gogame/chat/ZoomableImageView;)I

    move-result p2

    if-ne p2, v5, :cond_b

    iget-object p2, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p2}, Lnet/gogame/chat/ZoomableImageView;->access$700(Lnet/gogame/chat/ZoomableImageView;)F

    move-result p2

    iget-object v0, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v0}, Lnet/gogame/chat/ZoomableImageView;->access$800(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v0

    cmpl-float p2, p2, v0

    if-lez p2, :cond_b

    .line 78
    :cond_0
    iget p2, v2, Landroid/graphics/PointF;->x:F

    iget-object v0, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v0}, Lnet/gogame/chat/ZoomableImageView;->access$400(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/PointF;

    move-result-object v0

    iget v0, v0, Landroid/graphics/PointF;->x:F

    sub-float/2addr p2, v0

    .line 79
    iget v0, v2, Landroid/graphics/PointF;->y:F

    iget-object v3, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v3}, Lnet/gogame/chat/ZoomableImageView;->access$400(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/PointF;

    move-result-object v3

    iget v3, v3, Landroid/graphics/PointF;->y:F

    sub-float/2addr v0, v3

    .line 80
    iget-object v3, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v3}, Lnet/gogame/chat/ZoomableImageView;->access$900(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v3

    iget-object v4, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v4}, Lnet/gogame/chat/ZoomableImageView;->access$700(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v4

    mul-float v3, v3, v4

    invoke-static {v3}, Ljava/lang/Math;->round(F)I

    move-result v3

    int-to-float v3, v3

    .line 81
    iget-object v4, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v4}, Lnet/gogame/chat/ZoomableImageView;->access$1000(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v4

    iget-object v6, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v6}, Lnet/gogame/chat/ZoomableImageView;->access$700(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v6

    mul-float v4, v4, v6

    invoke-static {v4}, Ljava/lang/Math;->round(F)I

    move-result v4

    int-to-float v4, v4

    .line 85
    iget-object v6, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v6}, Lnet/gogame/chat/ZoomableImageView;->access$1100(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v6

    cmpg-float v3, v3, v6

    const/4 v6, 0x0

    if-gez v3, :cond_3

    add-float p1, v1, v0

    cmpl-float p2, p1, v6

    if-lez p2, :cond_2

    neg-float v0, v1

    :cond_1
    :goto_0
    const/4 p2, 0x0

    goto/16 :goto_4

    .line 89
    :cond_2
    iget-object p2, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p2}, Lnet/gogame/chat/ZoomableImageView;->access$1200(Lnet/gogame/chat/ZoomableImageView;)F

    move-result p2

    neg-float p2, p2

    cmpg-float p1, p1, p2

    if-gez p1, :cond_1

    .line 90
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1}, Lnet/gogame/chat/ZoomableImageView;->access$1200(Lnet/gogame/chat/ZoomableImageView;)F

    move-result p1

    add-float/2addr v1, p1

    neg-float v0, v1

    goto :goto_0

    .line 96
    :cond_3
    iget-object v3, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v3}, Lnet/gogame/chat/ZoomableImageView;->access$1300(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v3

    cmpg-float v3, v4, v3

    if-gez v3, :cond_6

    add-float v0, p1, p2

    cmpl-float v1, v0, v6

    if-lez v1, :cond_5

    neg-float p2, p1

    :cond_4
    :goto_1
    const/4 v0, 0x0

    goto :goto_4

    .line 100
    :cond_5
    iget-object v1, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v1}, Lnet/gogame/chat/ZoomableImageView;->access$1400(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v1

    neg-float v1, v1

    cmpg-float v0, v0, v1

    if-gez v0, :cond_4

    .line 101
    iget-object p2, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p2}, Lnet/gogame/chat/ZoomableImageView;->access$1400(Lnet/gogame/chat/ZoomableImageView;)F

    move-result p2

    add-float/2addr p1, p2

    neg-float p2, p1

    goto :goto_1

    :cond_6
    add-float v3, p1, p2

    cmpl-float v4, v3, v6

    if-lez v4, :cond_7

    neg-float p1, p1

    :goto_2
    move p2, p1

    goto :goto_3

    .line 109
    :cond_7
    iget-object v4, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v4}, Lnet/gogame/chat/ZoomableImageView;->access$1400(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v4

    neg-float v4, v4

    cmpg-float v3, v3, v4

    if-gez v3, :cond_8

    .line 110
    iget-object p2, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p2}, Lnet/gogame/chat/ZoomableImageView;->access$1400(Lnet/gogame/chat/ZoomableImageView;)F

    move-result p2

    add-float/2addr p1, p2

    neg-float p1, p1

    goto :goto_2

    :cond_8
    :goto_3
    add-float p1, v1, v0

    cmpl-float v3, p1, v6

    if-lez v3, :cond_9

    neg-float v0, v1

    goto :goto_4

    .line 114
    :cond_9
    iget-object v3, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v3}, Lnet/gogame/chat/ZoomableImageView;->access$1200(Lnet/gogame/chat/ZoomableImageView;)F

    move-result v3

    neg-float v3, v3

    cmpg-float p1, p1, v3

    if-gez p1, :cond_a

    .line 115
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1}, Lnet/gogame/chat/ZoomableImageView;->access$1200(Lnet/gogame/chat/ZoomableImageView;)F

    move-result p1

    add-float/2addr v1, p1

    neg-float v0, v1

    .line 119
    :cond_a
    :goto_4
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1}, Lnet/gogame/chat/ZoomableImageView;->access$300(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/Matrix;

    move-result-object p1

    invoke-virtual {p1, p2, v0}, Landroid/graphics/Matrix;->postTranslate(FF)Z

    .line 121
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1}, Lnet/gogame/chat/ZoomableImageView;->access$400(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/PointF;

    move-result-object p1

    iget p2, v2, Landroid/graphics/PointF;->x:F

    iget v0, v2, Landroid/graphics/PointF;->y:F

    invoke-virtual {p1, p2, v0}, Landroid/graphics/PointF;->set(FF)V

    goto :goto_5

    .line 126
    :pswitch_4
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1, v4}, Lnet/gogame/chat/ZoomableImageView;->access$602(Lnet/gogame/chat/ZoomableImageView;I)I

    .line 127
    iget p1, v2, Landroid/graphics/PointF;->x:F

    iget-object p2, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p2}, Lnet/gogame/chat/ZoomableImageView;->access$500(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/PointF;

    move-result-object p2

    iget p2, p2, Landroid/graphics/PointF;->x:F

    sub-float/2addr p1, p2

    invoke-static {p1}, Ljava/lang/Math;->abs(F)F

    move-result p1

    float-to-int p1, p1

    .line 128
    iget p2, v2, Landroid/graphics/PointF;->y:F

    iget-object v0, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {v0}, Lnet/gogame/chat/ZoomableImageView;->access$500(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/PointF;

    move-result-object v0

    iget v0, v0, Landroid/graphics/PointF;->y:F

    sub-float/2addr p2, v0

    invoke-static {p2}, Ljava/lang/Math;->abs(F)F

    move-result p2

    float-to-int p2, p2

    const/4 v0, 0x3

    if-ge p1, v0, :cond_b

    if-ge p2, v0, :cond_b

    .line 130
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-virtual {p1}, Lnet/gogame/chat/ZoomableImageView;->performClick()Z

    goto :goto_5

    .line 61
    :pswitch_5
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1}, Lnet/gogame/chat/ZoomableImageView;->access$400(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/PointF;

    move-result-object p1

    invoke-virtual {p2}, Landroid/view/MotionEvent;->getX()F

    move-result v0

    invoke-virtual {p2}, Landroid/view/MotionEvent;->getY()F

    move-result p2

    invoke-virtual {p1, v0, p2}, Landroid/graphics/PointF;->set(FF)V

    .line 62
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1}, Lnet/gogame/chat/ZoomableImageView;->access$500(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/PointF;

    move-result-object p1

    iget-object p2, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p2}, Lnet/gogame/chat/ZoomableImageView;->access$400(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/PointF;

    move-result-object p2

    invoke-virtual {p1, p2}, Landroid/graphics/PointF;->set(Landroid/graphics/PointF;)V

    .line 63
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p1, v5}, Lnet/gogame/chat/ZoomableImageView;->access$602(Lnet/gogame/chat/ZoomableImageView;I)I

    .line 138
    :cond_b
    :goto_5
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    iget-object p2, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-static {p2}, Lnet/gogame/chat/ZoomableImageView;->access$300(Lnet/gogame/chat/ZoomableImageView;)Landroid/graphics/Matrix;

    move-result-object p2

    invoke-virtual {p1, p2}, Lnet/gogame/chat/ZoomableImageView;->setImageMatrix(Landroid/graphics/Matrix;)V

    .line 139
    iget-object p1, p0, Lnet/gogame/chat/ZoomableImageView$1;->this$0:Lnet/gogame/chat/ZoomableImageView;

    invoke-virtual {p1}, Lnet/gogame/chat/ZoomableImageView;->invalidate()V

    return v5

    nop

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_5
        :pswitch_4
        :pswitch_3
        :pswitch_0
        :pswitch_0
        :pswitch_2
        :pswitch_1
    .end packed-switch
.end method
