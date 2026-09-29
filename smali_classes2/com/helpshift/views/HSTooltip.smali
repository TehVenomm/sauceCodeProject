.class public Lcom/helpshift/views/HSTooltip;
.super Ljava/lang/Object;
.source "HSTooltip.java"


# instance fields
.field private context:Landroid/content/Context;

.field private targetView:Landroid/view/View;

.field private text:Ljava/lang/String;


# direct methods
.method public constructor <init>(Landroid/view/View;Ljava/lang/String;)V
    .locals 0

    .line 17
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 18
    iput-object p1, p0, Lcom/helpshift/views/HSTooltip;->targetView:Landroid/view/View;

    .line 19
    invoke-virtual {p1}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/views/HSTooltip;->context:Landroid/content/Context;

    .line 20
    iput-object p2, p0, Lcom/helpshift/views/HSTooltip;->text:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public show()V
    .locals 7

    const/4 v0, 0x2

    .line 31
    new-array v1, v0, [I

    .line 32
    new-instance v2, Landroid/graphics/Rect;

    invoke-direct {v2}, Landroid/graphics/Rect;-><init>()V

    .line 33
    iget-object v3, p0, Lcom/helpshift/views/HSTooltip;->targetView:Landroid/view/View;

    invoke-virtual {v3, v1}, Landroid/view/View;->getLocationOnScreen([I)V

    .line 34
    iget-object v3, p0, Lcom/helpshift/views/HSTooltip;->targetView:Landroid/view/View;

    invoke-virtual {v3, v2}, Landroid/view/View;->getWindowVisibleDisplayFrame(Landroid/graphics/Rect;)V

    .line 36
    iget-object v3, p0, Lcom/helpshift/views/HSTooltip;->targetView:Landroid/view/View;

    invoke-virtual {v3}, Landroid/view/View;->getWidth()I

    move-result v3

    .line 37
    iget-object v4, p0, Lcom/helpshift/views/HSTooltip;->targetView:Landroid/view/View;

    invoke-virtual {v4}, Landroid/view/View;->getHeight()I

    move-result v4

    const/4 v5, 0x0

    .line 38
    aget v6, v1, v5

    div-int/2addr v3, v0

    add-int/2addr v6, v3

    const/4 v0, 0x1

    .line 39
    aget v0, v1, v0

    div-int/lit8 v1, v4, 0x2

    add-int/2addr v0, v1

    .line 40
    iget-object v1, p0, Lcom/helpshift/views/HSTooltip;->targetView:Landroid/view/View;

    invoke-static {v1}, Landroidx/core/view/ViewCompat;->getLayoutDirection(Landroid/view/View;)I

    move-result v1

    if-nez v1, :cond_0

    .line 41
    iget-object v1, p0, Lcom/helpshift/views/HSTooltip;->context:Landroid/content/Context;

    invoke-virtual {v1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    invoke-virtual {v1}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object v1

    iget v1, v1, Landroid/util/DisplayMetrics;->widthPixels:I

    sub-int v6, v1, v6

    .line 44
    :cond_0
    iget-object v1, p0, Lcom/helpshift/views/HSTooltip;->context:Landroid/content/Context;

    iget-object v3, p0, Lcom/helpshift/views/HSTooltip;->text:Ljava/lang/String;

    invoke-static {v1, v3, v5}, Lcom/helpshift/views/HSToast;->makeText(Landroid/content/Context;Ljava/lang/CharSequence;I)Landroid/widget/Toast;

    move-result-object v1

    .line 45
    invoke-virtual {v2}, Landroid/graphics/Rect;->height()I

    move-result v2

    if-ge v0, v2, :cond_1

    const v2, 0x800035

    .line 47
    invoke-virtual {v1, v2, v6, v0}, Landroid/widget/Toast;->setGravity(III)V

    goto :goto_0

    :cond_1
    const/16 v0, 0x51

    .line 51
    invoke-virtual {v1, v0, v5, v4}, Landroid/widget/Toast;->setGravity(III)V

    .line 53
    :goto_0
    invoke-virtual {v1}, Landroid/widget/Toast;->show()V

    return-void
.end method
