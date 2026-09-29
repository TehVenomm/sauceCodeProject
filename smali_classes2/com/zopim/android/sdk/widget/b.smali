.class Lcom/zopim/android/sdk/widget/b;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:I

.field final synthetic b:I

.field final synthetic c:Lcom/zopim/android/sdk/widget/ChatWidgetService;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/widget/ChatWidgetService;II)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    iput p2, p0, Lcom/zopim/android/sdk/widget/b;->a:I

    iput p3, p0, Lcom/zopim/android/sdk/widget/b;->b:I

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 7

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$200(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Lcom/zopim/android/sdk/widget/view/WidgetView;

    move-result-object v0

    invoke-static {v0}, Landroidx/core/view/ViewCompat;->isAttachedToWindow(Landroid/view/View;)Z

    move-result v0

    if-nez v0, :cond_0

    invoke-static {}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$300()Ljava/lang/String;

    move-result-object v0

    const-string v1, "Not attached to window. Skip loading widget"

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    return-void

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$200(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Lcom/zopim/android/sdk/widget/view/WidgetView;

    move-result-object v0

    invoke-virtual {v0}, Lcom/zopim/android/sdk/widget/view/WidgetView;->getAnchor()Lcom/zopim/android/sdk/widget/view/WidgetView$Anchor;

    move-result-object v0

    sget-object v1, Lcom/zopim/android/sdk/widget/h;->a:[I

    invoke-virtual {v0}, Lcom/zopim/android/sdk/widget/view/WidgetView$Anchor;->ordinal()I

    move-result v0

    aget v0, v1, v0

    packed-switch v0, :pswitch_data_0

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v1

    iget v1, v1, Landroid/view/WindowManager$LayoutParams;->width:I

    neg-int v1, v1

    iput v1, v0, Landroid/view/WindowManager$LayoutParams;->x:I

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v0

    iget v1, p0, Lcom/zopim/android/sdk/widget/b;->b:I

    iget-object v2, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v2}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$200(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Lcom/zopim/android/sdk/widget/view/WidgetView;

    move-result-object v2

    invoke-virtual {v2}, Lcom/zopim/android/sdk/widget/view/WidgetView;->getHeight()I

    move-result v2

    sub-int/2addr v1, v2

    div-int/lit8 v1, v1, 0x2

    :goto_0
    iput v1, v0, Landroid/view/WindowManager$LayoutParams;->y:I

    goto :goto_3

    :pswitch_0
    iget-object v0, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v0

    iget v1, p0, Lcom/zopim/android/sdk/widget/b;->a:I

    iget-object v2, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v2}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v2

    iget v2, v2, Landroid/view/WindowManager$LayoutParams;->width:I

    add-int/2addr v1, v2

    goto :goto_1

    :pswitch_1
    iget-object v0, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v1

    iget v1, v1, Landroid/view/WindowManager$LayoutParams;->width:I

    neg-int v1, v1

    :goto_1
    iput v1, v0, Landroid/view/WindowManager$LayoutParams;->x:I

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v0

    iget v1, p0, Lcom/zopim/android/sdk/widget/b;->b:I

    iget-object v2, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v2}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v2

    iget v2, v2, Landroid/view/WindowManager$LayoutParams;->height:I

    add-int/2addr v1, v2

    goto :goto_0

    :pswitch_2
    iget-object v0, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v0

    iget v1, p0, Lcom/zopim/android/sdk/widget/b;->a:I

    iget-object v2, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v2}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v2

    iget v2, v2, Landroid/view/WindowManager$LayoutParams;->width:I

    add-int/2addr v1, v2

    goto :goto_2

    :pswitch_3
    iget-object v0, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v1

    iget v1, v1, Landroid/view/WindowManager$LayoutParams;->width:I

    neg-int v1, v1

    :goto_2
    iput v1, v0, Landroid/view/WindowManager$LayoutParams;->x:I

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v1

    iget v1, v1, Landroid/view/WindowManager$LayoutParams;->height:I

    neg-int v1, v1

    goto :goto_0

    :goto_3
    iget-object v0, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$500(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$200(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Lcom/zopim/android/sdk/widget/view/WidgetView;

    move-result-object v1

    iget-object v2, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v2}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Landroid/view/WindowManager;->updateViewLayout(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$200(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Lcom/zopim/android/sdk/widget/view/WidgetView;

    move-result-object v0

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/zopim/android/sdk/widget/view/WidgetView;->setVisibility(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    new-instance v1, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    iget-object v2, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    iget-object v3, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v3}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$700(Lcom/zopim/android/sdk/widget/ChatWidgetService;)I

    move-result v3

    iget-object v4, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v4}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$800(Lcom/zopim/android/sdk/widget/ChatWidgetService;)I

    move-result v4

    invoke-direct {v1, v2, v3, v4}, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;-><init>(Lcom/zopim/android/sdk/widget/ChatWidgetService;II)V

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$602(Lcom/zopim/android/sdk/widget/ChatWidgetService;Lcom/zopim/android/sdk/widget/ChatWidgetService$a;)Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    new-instance v1, Ljava/util/Timer;

    invoke-direct {v1}, Ljava/util/Timer;-><init>()V

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$902(Lcom/zopim/android/sdk/widget/ChatWidgetService;Ljava/util/Timer;)Ljava/util/Timer;

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$900(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Ljava/util/Timer;

    move-result-object v1

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/b;->c:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$600(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    move-result-object v2

    const-wide/16 v3, 0x0

    const-wide/16 v5, 0x1e

    invoke-virtual/range {v1 .. v6}, Ljava/util/Timer;->schedule(Ljava/util/TimerTask;JJ)V

    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method
