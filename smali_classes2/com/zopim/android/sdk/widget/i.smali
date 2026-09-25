.class Lcom/zopim/android/sdk/widget/i;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/widget/ChatWidgetService$a;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/widget/i;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 4

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/i;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    iget-object v0, v0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->e:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$200(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Lcom/zopim/android/sdk/widget/view/WidgetView;

    move-result-object v0

    invoke-static {v0}, Landroidx/core/view/ViewCompat;->isAttachedToWindow(Landroid/view/View;)Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/i;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    iget-object v0, v0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->e:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/widget/i;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    iget-object v1, v1, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->e:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v1

    iget v1, v1, Landroid/view/WindowManager$LayoutParams;->x:I

    iget-object v2, p0, Lcom/zopim/android/sdk/widget/i;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    iget v2, v2, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->c:I

    sub-int/2addr v1, v2

    const/4 v2, 0x2

    mul-int/lit8 v1, v1, 0x2

    div-int/lit8 v1, v1, 0x3

    iget-object v3, p0, Lcom/zopim/android/sdk/widget/i;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    iget v3, v3, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->c:I

    add-int/2addr v1, v3

    iput v1, v0, Landroid/view/WindowManager$LayoutParams;->x:I

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/i;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    iget-object v0, v0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->e:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/widget/i;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    iget-object v1, v1, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->e:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v1

    iget v1, v1, Landroid/view/WindowManager$LayoutParams;->y:I

    iget-object v3, p0, Lcom/zopim/android/sdk/widget/i;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    iget v3, v3, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->d:I

    sub-int/2addr v1, v3

    mul-int/lit8 v1, v1, 0x2

    div-int/lit8 v1, v1, 0x3

    iget-object v3, p0, Lcom/zopim/android/sdk/widget/i;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    iget v3, v3, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->d:I

    add-int/2addr v1, v3

    iput v1, v0, Landroid/view/WindowManager$LayoutParams;->y:I

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/i;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    iget-object v0, v0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->e:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$500(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/widget/i;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    iget-object v1, v1, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->e:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$200(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Lcom/zopim/android/sdk/widget/view/WidgetView;

    move-result-object v1

    iget-object v3, p0, Lcom/zopim/android/sdk/widget/i;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    iget-object v3, v3, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->e:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v3}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v3

    invoke-interface {v0, v1, v3}, Landroid/view/WindowManager;->updateViewLayout(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/i;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    iget-object v0, v0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->e:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v0

    iget v0, v0, Landroid/view/WindowManager$LayoutParams;->x:I

    iget-object v1, p0, Lcom/zopim/android/sdk/widget/i;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    iget v1, v1, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->c:I

    sub-int/2addr v0, v1

    invoke-static {v0}, Ljava/lang/Math;->abs(I)I

    move-result v0

    if-ge v0, v2, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/i;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    iget-object v0, v0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->e:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v0

    iget v0, v0, Landroid/view/WindowManager$LayoutParams;->y:I

    iget-object v1, p0, Lcom/zopim/android/sdk/widget/i;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    iget v1, v1, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->d:I

    sub-int/2addr v0, v1

    invoke-static {v0}, Ljava/lang/Math;->abs(I)I

    move-result v0

    if-ge v0, v2, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/i;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->cancel()Z

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/i;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService$a;

    iget-object v0, v0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->e:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$900(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Ljava/util/Timer;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/Timer;->cancel()V

    :cond_0
    return-void
.end method
