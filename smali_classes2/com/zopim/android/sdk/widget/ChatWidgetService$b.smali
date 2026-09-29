.class Lcom/zopim/android/sdk/widget/ChatWidgetService$b;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/view/View$OnTouchListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/widget/ChatWidgetService;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "b"
.end annotation


# instance fields
.field a:J

.field final synthetic b:Lcom/zopim/android/sdk/widget/ChatWidgetService;

.field private final c:I

.field private d:F

.field private e:F

.field private f:F

.field private g:F


# direct methods
.method private constructor <init>(Lcom/zopim/android/sdk/widget/ChatWidgetService;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->b:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    iget-object p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->b:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->getApplicationContext()Landroid/content/Context;

    move-result-object p1

    invoke-static {p1}, Landroid/view/ViewConfiguration;->get(Landroid/content/Context;)Landroid/view/ViewConfiguration;

    move-result-object p1

    invoke-virtual {p1}, Landroid/view/ViewConfiguration;->getScaledTouchSlop()I

    move-result p1

    iput p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->c:I

    return-void
.end method

.method synthetic constructor <init>(Lcom/zopim/android/sdk/widget/ChatWidgetService;Lcom/zopim/android/sdk/widget/a;)V
    .locals 0

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;-><init>(Lcom/zopim/android/sdk/widget/ChatWidgetService;)V

    return-void
.end method


# virtual methods
.method a()V
    .locals 2

    invoke-static {}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$300()Ljava/lang/String;

    move-result-object v0

    const-string v1, "onClick() chat widget"

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-static {}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$300()Ljava/lang/String;

    move-result-object v0

    const-string v1, "Broadcasting intent action zopim.action.RESUME_CHAT to resume a chat activity"

    invoke-static {v0, v1}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    new-instance v0, Landroid/content/Intent;

    invoke-direct {v0}, Landroid/content/Intent;-><init>()V

    const-string v1, "zopim.action.RESUME_CHAT"

    invoke-virtual {v0, v1}, Landroid/content/Intent;->setAction(Ljava/lang/String;)Landroid/content/Intent;

    const-string v1, "android.intent.category.DEFAULT"

    invoke-virtual {v0, v1}, Landroid/content/Intent;->addCategory(Ljava/lang/String;)Landroid/content/Intent;

    const/high16 v1, 0x30000000

    invoke-virtual {v0, v1}, Landroid/content/Intent;->addFlags(I)Landroid/content/Intent;

    iget-object v1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->b:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-virtual {v1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    invoke-virtual {v1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    iget-object v1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->b:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-virtual {v1, v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->startActivity(Landroid/content/Intent;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->b:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->stopSelf()V

    return-void
.end method

.method public onTouch(Landroid/view/View;Landroid/view/MotionEvent;)Z
    .locals 9

    invoke-virtual {p2}, Landroid/view/MotionEvent;->getActionMasked()I

    move-result p1

    invoke-virtual {p2}, Landroid/view/MotionEvent;->getRawX()F

    move-result v0

    invoke-virtual {p2}, Landroid/view/MotionEvent;->getRawY()F

    move-result p2

    const/4 v1, 0x0

    const/4 v2, 0x1

    packed-switch p1, :pswitch_data_0

    return v1

    :pswitch_0
    iget p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->f:F

    sub-float p1, v0, p1

    iget v1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->g:F

    sub-float v1, p2, v1

    iget-object v3, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->b:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v3}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v3

    iget v4, v3, Landroid/view/WindowManager$LayoutParams;->x:I

    int-to-float v4, v4

    add-float/2addr v4, p1

    float-to-int p1, v4

    iput p1, v3, Landroid/view/WindowManager$LayoutParams;->x:I

    iget-object p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->b:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object p1

    iget v3, p1, Landroid/view/WindowManager$LayoutParams;->y:I

    int-to-float v3, v3

    add-float/2addr v3, v1

    float-to-int v1, v3

    iput v1, p1, Landroid/view/WindowManager$LayoutParams;->y:I

    iput v0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->f:F

    iput p2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->g:F

    iget-object p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->b:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$500(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager;

    move-result-object p1

    iget-object p2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->b:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {p2}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$200(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Lcom/zopim/android/sdk/widget/view/WidgetView;

    move-result-object p2

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->b:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v0

    invoke-interface {p1, p2, v0}, Landroid/view/WindowManager;->updateViewLayout(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    return v2

    :pswitch_1
    invoke-static {}, Landroid/os/SystemClock;->elapsedRealtime()J

    move-result-wide v3

    iget-wide v5, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->a:J

    sub-long/2addr v3, v5

    const-wide/16 v5, 0xc8

    cmp-long p1, v3, v5

    if-gez p1, :cond_0

    iget p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->d:F

    sub-float/2addr v0, p1

    invoke-static {v0}, Ljava/lang/Math;->abs(F)F

    move-result p1

    iget v0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->e:F

    sub-float/2addr p2, v0

    invoke-static {p2}, Ljava/lang/Math;->abs(F)F

    move-result p2

    mul-float p1, p1, p1

    mul-float p2, p2, p2

    add-float/2addr p1, p2

    float-to-double p1, p1

    invoke-static {p1, p2}, Ljava/lang/Math;->sqrt(D)D

    move-result-wide p1

    double-to-int p1, p1

    iget p2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->c:I

    if-ge p1, p2, :cond_0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->a()V

    return v1

    :cond_0
    iget-object p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->b:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    new-instance p2, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->b:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    iget-object v1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->b:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$700(Lcom/zopim/android/sdk/widget/ChatWidgetService;)I

    move-result v1

    iget-object v3, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->b:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v3}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$800(Lcom/zopim/android/sdk/widget/ChatWidgetService;)I

    move-result v3

    invoke-direct {p2, v0, v1, v3}, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;-><init>(Lcom/zopim/android/sdk/widget/ChatWidgetService;II)V

    invoke-static {p1, p2}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$602(Lcom/zopim/android/sdk/widget/ChatWidgetService;Lcom/zopim/android/sdk/widget/ChatWidgetService$a;)Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    iget-object p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->b:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    new-instance p2, Ljava/util/Timer;

    invoke-direct {p2}, Ljava/util/Timer;-><init>()V

    invoke-static {p1, p2}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$902(Lcom/zopim/android/sdk/widget/ChatWidgetService;Ljava/util/Timer;)Ljava/util/Timer;

    iget-object p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->b:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$900(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Ljava/util/Timer;

    move-result-object v3

    iget-object p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->b:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$600(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    move-result-object v4

    const-wide/16 v5, 0x0

    const-wide/16 v7, 0x1e

    invoke-virtual/range {v3 .. v8}, Ljava/util/Timer;->schedule(Ljava/util/TimerTask;JJ)V

    return v2

    :pswitch_2
    invoke-static {}, Landroid/os/SystemClock;->elapsedRealtime()J

    move-result-wide v3

    iput-wide v3, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->a:J

    iget-object p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->b:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$600(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    move-result-object p1

    if-eqz p1, :cond_1

    iget-object p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->b:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$600(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    move-result-object p1

    invoke-virtual {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->cancel()Z

    iget-object p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->b:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$900(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Ljava/util/Timer;

    move-result-object p1

    invoke-virtual {p1}, Ljava/util/Timer;->cancel()V

    :cond_1
    iput v0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->d:F

    iput p2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->e:F

    iput v0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->f:F

    iput p2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$b;->g:F

    return v2

    nop

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_2
        :pswitch_1
        :pswitch_0
        :pswitch_1
    .end packed-switch
.end method
