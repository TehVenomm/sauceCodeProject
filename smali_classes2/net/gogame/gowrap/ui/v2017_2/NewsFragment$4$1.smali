.class Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4$1;
.super Landroid/view/GestureDetector$SimpleOnGestureListener;
.source "NewsFragment.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field private final SWIPE_MAX_OFF_PATH:I

.field private final SWIPE_MIN_DISTANCE:I

.field private final SWIPE_THRESHOLD_VELOCITY:I

.field final synthetic this$1:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;)V
    .locals 0

    .line 147
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4$1;->this$1:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;

    invoke-direct {p0}, Landroid/view/GestureDetector$SimpleOnGestureListener;-><init>()V

    const/16 p1, 0x64

    .line 149
    iput p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4$1;->SWIPE_MIN_DISTANCE:I

    .line 150
    iput p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4$1;->SWIPE_MAX_OFF_PATH:I

    const/16 p1, 0xc8

    .line 151
    iput p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4$1;->SWIPE_THRESHOLD_VELOCITY:I

    return-void
.end method


# virtual methods
.method public onDown(Landroid/view/MotionEvent;)Z
    .locals 0

    const/4 p1, 0x1

    return p1
.end method

.method public onFling(Landroid/view/MotionEvent;Landroid/view/MotionEvent;FF)Z
    .locals 1

    .line 156
    invoke-virtual {p2}, Landroid/view/MotionEvent;->getX()F

    move-result p4

    invoke-virtual {p1}, Landroid/view/MotionEvent;->getX()F

    move-result v0

    sub-float/2addr p4, v0

    float-to-int p4, p4

    .line 157
    invoke-virtual {p2}, Landroid/view/MotionEvent;->getY()F

    move-result p2

    invoke-virtual {p1}, Landroid/view/MotionEvent;->getY()F

    move-result p1

    sub-float/2addr p2, p1

    float-to-int p1, p2

    .line 158
    invoke-static {p1}, Ljava/lang/Math;->abs(I)I

    move-result p1

    const/16 p2, 0x64

    const/4 v0, 0x0

    if-gt p1, p2, :cond_3

    .line 159
    invoke-static {p4}, Ljava/lang/Math;->abs(I)I

    move-result p1

    if-lt p1, p2, :cond_3

    .line 160
    invoke-static {p3}, Ljava/lang/Math;->abs(F)F

    move-result p1

    const/high16 p2, 0x43480000    # 200.0f

    cmpg-float p1, p1, p2

    if-gez p1, :cond_0

    goto :goto_0

    :cond_0
    const/4 p1, 0x1

    if-lez p4, :cond_1

    .line 164
    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4$1;->this$1:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;

    iget-object p2, p2, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {p2, p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$602(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;Z)Z

    .line 165
    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4$1;->this$1:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;

    iget-object p2, p2, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {p2}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$700(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)V

    return p1

    :cond_1
    if-gez p4, :cond_2

    .line 168
    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4$1;->this$1:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;

    iget-object p2, p2, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {p2, v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$602(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;Z)Z

    .line 169
    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4$1;->this$1:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;

    iget-object p2, p2, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {p2}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$700(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)V

    return p1

    :cond_2
    return v0

    :cond_3
    :goto_0
    return v0
.end method

.method public onSingleTapConfirmed(Landroid/view/MotionEvent;)Z
    .locals 2

    .line 182
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4$1;->this$1:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;

    iget-object p1, p1, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$300(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Ljava/util/ArrayList;

    move-result-object p1

    const/4 v0, 0x1

    if-nez p1, :cond_0

    return v0

    .line 185
    :cond_0
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4$1;->this$1:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;

    iget-object p1, p1, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$300(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)Ljava/util/ArrayList;

    move-result-object p1

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4$1;->this$1:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;

    iget-object v1, v1, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-static {v1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$400(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;)I

    move-result v1

    invoke-virtual {p1, v1}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lnet/gogame/gowrap/model/news/Banner;

    if-nez p1, :cond_1

    return v0

    .line 189
    :cond_1
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4$1;->this$1:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;

    iget-object v1, v1, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment$4;->this$0:Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/Banner;->getLink()Ljava/lang/String;

    move-result-object p1

    invoke-static {v1, p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;->access$500(Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;Ljava/lang/String;)V

    return v0
.end method
