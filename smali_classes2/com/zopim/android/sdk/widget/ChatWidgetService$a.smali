.class Lcom/zopim/android/sdk/widget/ChatWidgetService$a;
.super Ljava/util/TimerTask;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/widget/ChatWidgetService;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "a"
.end annotation


# instance fields
.field a:I

.field b:I

.field c:I

.field d:I

.field final synthetic e:Lcom/zopim/android/sdk/widget/ChatWidgetService;


# direct methods
.method public constructor <init>(Lcom/zopim/android/sdk/widget/ChatWidgetService;II)V
    .locals 10

    iput-object p1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->e:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-direct {p0}, Ljava/util/TimerTask;-><init>()V

    const/4 v0, 0x0

    if-gez p2, :cond_0

    const/4 v1, 0x0

    goto :goto_0

    :cond_0
    move v1, p2

    :goto_0
    iput v1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->a:I

    if-gez p3, :cond_1

    goto :goto_1

    :cond_1
    move v0, p3

    :goto_1
    iput v0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->b:I

    invoke-virtual {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object v0

    iget v0, v0, Landroid/util/DisplayMetrics;->heightPixels:I

    invoke-virtual {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    invoke-virtual {v1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    invoke-virtual {v1}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object v1

    iget v1, v1, Landroid/util/DisplayMetrics;->widthPixels:I

    invoke-static {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$200(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Lcom/zopim/android/sdk/widget/view/WidgetView;

    move-result-object v2

    invoke-virtual {v2}, Lcom/zopim/android/sdk/widget/view/WidgetView;->getWidth()I

    move-result v2

    sub-int v2, v1, v2

    sub-int/2addr v2, p2

    invoke-virtual {p0}, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->a()I

    move-result v3

    add-int/2addr v3, v0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->b()I

    move-result v4

    sub-int/2addr v3, v4

    invoke-static {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$200(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Lcom/zopim/android/sdk/widget/view/WidgetView;

    move-result-object v4

    invoke-virtual {v4}, Lcom/zopim/android/sdk/widget/view/WidgetView;->getHeight()I

    move-result v4

    sub-int/2addr v3, v4

    sub-int/2addr v3, p3

    invoke-static {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v4

    iget v4, v4, Landroid/view/WindowManager$LayoutParams;->x:I

    sub-int/2addr v4, p2

    invoke-static {v4}, Ljava/lang/Math;->abs(I)I

    move-result v4

    invoke-static {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v5

    iget v5, v5, Landroid/view/WindowManager$LayoutParams;->x:I

    sub-int/2addr v5, v2

    invoke-static {v5}, Ljava/lang/Math;->abs(I)I

    move-result v5

    invoke-static {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v6

    iget v6, v6, Landroid/view/WindowManager$LayoutParams;->y:I

    sub-int/2addr v6, p3

    invoke-static {v6}, Ljava/lang/Math;->abs(I)I

    move-result v6

    invoke-static {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object v7

    iget v7, v7, Landroid/view/WindowManager$LayoutParams;->y:I

    sub-int/2addr v7, v3

    invoke-static {v7}, Ljava/lang/Math;->abs(I)I

    move-result v7

    invoke-static {v6, v7}, Ljava/lang/Math;->min(II)I

    move-result v8

    invoke-static {v4, v5}, Ljava/lang/Math;->min(II)I

    move-result v9

    if-ge v8, v9, :cond_5

    if-ge v6, v7, :cond_2

    iput p3, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->d:I

    goto :goto_2

    :cond_2
    iput v3, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->d:I

    :goto_2
    invoke-static {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object p3

    iget p3, p3, Landroid/view/WindowManager$LayoutParams;->x:I

    if-ge p3, p2, :cond_3

    :goto_3
    iput p2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->c:I

    goto :goto_5

    :cond_3
    invoke-static {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object p2

    iget p2, p2, Landroid/view/WindowManager$LayoutParams;->x:I

    if-le p2, v2, :cond_4

    iput v2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->c:I

    goto :goto_5

    :cond_4
    invoke-static {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object p2

    iget p2, p2, Landroid/view/WindowManager$LayoutParams;->x:I

    goto :goto_3

    :cond_5
    if-ge v4, v5, :cond_6

    iput p2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->c:I

    goto :goto_4

    :cond_6
    iput v2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->c:I

    :goto_4
    invoke-static {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object p2

    iget p2, p2, Landroid/view/WindowManager$LayoutParams;->y:I

    if-ge p2, p3, :cond_7

    iput p3, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->d:I

    goto :goto_5

    :cond_7
    invoke-static {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object p2

    iget p2, p2, Landroid/view/WindowManager$LayoutParams;->y:I

    if-le p2, v3, :cond_8

    iput v3, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->d:I

    goto :goto_5

    :cond_8
    invoke-static {p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/view/WindowManager$LayoutParams;

    move-result-object p2

    iget p2, p2, Landroid/view/WindowManager$LayoutParams;->y:I

    iput p2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->d:I

    :goto_5
    iget p2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->c:I

    mul-int/lit8 p2, p2, 0x64

    div-int/2addr p2, v1

    int-to-double p2, p2

    invoke-static {p1, p2, p3}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$1002(Lcom/zopim/android/sdk/widget/ChatWidgetService;D)D

    iget p2, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->d:I

    mul-int/lit8 p2, p2, 0x64

    div-int/2addr p2, v0

    int-to-double p2, p2

    invoke-static {p1, p2, p3}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$1102(Lcom/zopim/android/sdk/widget/ChatWidgetService;D)D

    return-void
.end method


# virtual methods
.method public a()I
    .locals 4

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->e:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v1, "status_bar_height"

    const-string v2, "dimen"

    const-string v3, "android"

    invoke-virtual {v0, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    if-lez v0, :cond_0

    iget-object v1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->e:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-virtual {v1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    invoke-virtual {v1, v0}, Landroid/content/res/Resources;->getDimensionPixelSize(I)I

    move-result v0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method b()I
    .locals 4

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->e:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->getBaseContext()Landroid/content/Context;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->e:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-virtual {v1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    invoke-virtual {v1}, Landroid/content/res/Resources;->getConfiguration()Landroid/content/res/Configuration;

    move-result-object v1

    iget v1, v1, Landroid/content/res/Configuration;->orientation:I

    const/4 v2, 0x1

    if-ne v1, v2, :cond_0

    const-string v1, "navigation_bar_height"

    goto :goto_0

    :cond_0
    const-string v1, "navigation_bar_height_landscape"

    :goto_0
    const-string v2, "dimen"

    const-string v3, "android"

    invoke-virtual {v0, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v1

    if-lez v1, :cond_1

    invoke-virtual {v0, v1}, Landroid/content/res/Resources;->getDimensionPixelSize(I)I

    move-result v0

    return v0

    :cond_1
    const/4 v0, 0x0

    return v0
.end method

.method public run()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/ChatWidgetService$a;->e:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$1200(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/os/Handler;

    move-result-object v0

    new-instance v1, Lcom/zopim/android/sdk/widget/i;

    invoke-direct {v1, p0}, Lcom/zopim/android/sdk/widget/i;-><init>(Lcom/zopim/android/sdk/widget/ChatWidgetService$a;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method
