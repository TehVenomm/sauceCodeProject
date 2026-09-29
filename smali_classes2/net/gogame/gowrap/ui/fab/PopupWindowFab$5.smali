.class Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;
.super Ljava/lang/Object;
.source "PopupWindowFab.java"

# interfaces
.implements Landroid/view/View$OnTouchListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->show(Landroid/app/Activity;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field private initialTouchX:F

.field private initialTouchY:F

.field private initialX:I

.field private initialY:I

.field private isDrag:Z

.field final synthetic this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

.field final synthetic val$activity:Landroid/app/Activity;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;)V
    .locals 0

    .line 223
    iput-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->val$activity:Landroid/app/Activity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onTouch(Landroid/view/View;Landroid/view/MotionEvent;)Z
    .locals 5

    .line 233
    invoke-virtual {p2}, Landroid/view/MotionEvent;->getAction()I

    move-result p1

    const/4 v0, 0x1

    const/4 v1, 0x0

    packed-switch p1, :pswitch_data_0

    goto/16 :goto_1

    .line 299
    :pswitch_0
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$100(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    move-result-object p1

    invoke-static {p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$300(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Z

    move-result p1

    if-nez p1, :cond_0

    .line 300
    iget p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->initialX:I

    invoke-virtual {p2}, Landroid/view/MotionEvent;->getRawX()F

    move-result v2

    iget v3, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->initialTouchX:F

    sub-float/2addr v2, v3

    float-to-int v2, v2

    add-int/2addr p1, v2

    iget-object v2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v2}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$1800(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I

    move-result v2

    sub-int/2addr p1, v2

    .line 301
    iget-object v2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    invoke-static {v2, p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$1202(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Ljava/lang/Integer;)Ljava/lang/Integer;

    .line 303
    :cond_0
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$100(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    move-result-object p1

    invoke-static {p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$200(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Z

    move-result p1

    if-nez p1, :cond_2

    .line 305
    iget p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->initialY:I

    invoke-virtual {p2}, Landroid/view/MotionEvent;->getRawY()F

    move-result p2

    iget v2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->initialTouchY:F

    sub-float/2addr p2, v2

    float-to-int p2, p2

    add-int/2addr p1, p2

    iget-object p2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {p2}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$1900(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I

    move-result p2

    sub-int/2addr p1, p2

    .line 307
    iget-object p2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {p2}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$2000(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I

    move-result p2

    if-le p1, p2, :cond_8

    iget-object p2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    .line 308
    invoke-static {p2}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$2100(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I

    move-result p2

    iget-object v2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v2}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$2200(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I

    move-result v2

    sub-int/2addr p2, v2

    if-lt p1, p2, :cond_1

    goto/16 :goto_1

    .line 312
    :cond_1
    iget-object p2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    invoke-static {p2, p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$2302(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Ljava/lang/Integer;)Ljava/lang/Integer;

    .line 316
    :cond_2
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iget-object p2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->val$activity:Landroid/app/Activity;

    invoke-static {p1, p2}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$1700(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;)V

    .line 318
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$500(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I

    move-result p1

    iget p2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->initialX:I

    sub-int/2addr p1, p2

    .line 319
    iget-object p2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {p2}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$600(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I

    move-result p2

    iget v2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->initialY:I

    sub-int/2addr p2, v2

    .line 321
    iget-boolean v2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->isDrag:Z

    if-nez v2, :cond_8

    .line 322
    invoke-static {p1}, Ljava/lang/Math;->abs(I)I

    move-result p1

    const/16 v2, 0xa

    if-gt p1, v2, :cond_3

    invoke-static {p2}, Ljava/lang/Math;->abs(I)I

    move-result p1

    if-le p1, v2, :cond_8

    .line 323
    :cond_3
    iput-boolean v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->isDrag:Z

    goto/16 :goto_1

    .line 254
    :pswitch_1
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$1100(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I

    move-result p1

    div-int/lit8 p1, p1, 0x2

    .line 256
    iget-object v2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v2}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$1200(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Ljava/lang/Integer;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Integer;->intValue()I

    move-result v2

    if-lt v2, p1, :cond_4

    .line 257
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$1100(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I

    move-result v0

    iget-object v2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v2}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$1300(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I

    move-result v2

    sub-int/2addr v0, v2

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    invoke-static {p1, v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$1202(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Ljava/lang/Integer;)Ljava/lang/Integer;

    .line 258
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {p1, v1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$1402(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Z)Z

    goto :goto_0

    .line 259
    :cond_4
    iget-object v2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v2}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$1200(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Ljava/lang/Integer;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Integer;->intValue()I

    move-result v2

    if-ge v2, p1, :cond_5

    .line 260
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iget-object v2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {v2}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$1500(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I

    move-result v2

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-static {p1, v2}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$1202(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Ljava/lang/Integer;)Ljava/lang/Integer;

    .line 261
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {p1, v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$1402(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Z)Z

    .line 264
    :cond_5
    :goto_0
    sget-object p1, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/core/Wrapper;->isSlideOut()Z

    move-result p1

    if-nez p1, :cond_6

    sget-object p1, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/core/Wrapper;->isSlideIn()Z

    move-result p1

    if-eqz p1, :cond_7

    .line 265
    :cond_6
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->val$activity:Landroid/app/Activity;

    invoke-virtual {p1, v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->cancelAnimation(Landroid/app/Activity;)V

    .line 267
    new-instance p1, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5$1;

    invoke-direct {p1, p0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5$1;-><init>(Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;)V

    .line 286
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iget-object v2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->val$activity:Landroid/app/Activity;

    const-wide/16 v3, 0x64

    invoke-static {v0, v2, p1, v3, v4}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$900(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;Ljava/lang/Runnable;J)V

    .line 289
    :cond_7
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->val$activity:Landroid/app/Activity;

    invoke-static {p1, v0}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$1700(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;Landroid/app/Activity;)V

    .line 292
    iget-boolean p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->isDrag:Z

    if-nez p1, :cond_8

    .line 293
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-virtual {p1, p2}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->fireClickListener(Landroid/view/MotionEvent;)V

    goto :goto_1

    .line 235
    :pswitch_2
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$500(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I

    move-result p1

    iput p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->initialX:I

    .line 236
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$600(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)I

    move-result p1

    iput p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->initialY:I

    .line 237
    invoke-virtual {p2}, Landroid/view/MotionEvent;->getRawX()F

    move-result p1

    iput p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->initialTouchX:F

    .line 238
    invoke-virtual {p2}, Landroid/view/MotionEvent;->getRawY()F

    move-result p1

    iput p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->initialTouchY:F

    .line 239
    iput-boolean v1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->isDrag:Z

    .line 241
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->cancelTimer()V

    .line 242
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    iget-object p2, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->val$activity:Landroid/app/Activity;

    invoke-virtual {p1, p2}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->cancelAnimation(Landroid/app/Activity;)V

    .line 244
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$000(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/view/View;

    move-result-object p1

    if-eqz p1, :cond_8

    .line 245
    sget p1, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 p2, 0xb

    if-lt p1, p2, :cond_8

    .line 246
    iget-object p1, p0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab$5;->this$0:Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;->access$000(Lnet/gogame/gowrap/ui/fab/PopupWindowFab;)Landroid/view/View;

    move-result-object p1

    const/high16 p2, 0x3f800000    # 1.0f

    invoke-virtual {p1, p2}, Landroid/view/View;->setAlpha(F)V

    :cond_8
    :goto_1
    return v1

    nop

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method
