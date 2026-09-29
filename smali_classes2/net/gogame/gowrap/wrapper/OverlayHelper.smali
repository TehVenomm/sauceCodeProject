.class public Lnet/gogame/gowrap/wrapper/OverlayHelper;
.super Ljava/lang/Object;
.source "OverlayHelper.java"


# static fields
.field public static final DEFAULT_MAX_RETRIES:I = 0x4

.field public static final DEFAULT_RETRY_MS:J = 0x1f4L


# instance fields
.field private final activity:Landroid/app/Activity;

.field private final handler:Landroid/os/Handler;

.field private final maxRetries:I

.field private popupWindow:Landroid/widget/PopupWindow;

.field private retries:I

.field private final retryMs:J

.field private final view:Landroid/view/View;


# direct methods
.method public constructor <init>(Landroid/app/Activity;Landroid/view/View;)V
    .locals 6

    const-wide/16 v3, 0x1f4

    const/4 v5, 0x4

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    .line 26
    invoke-direct/range {v0 .. v5}, Lnet/gogame/gowrap/wrapper/OverlayHelper;-><init>(Landroid/app/Activity;Landroid/view/View;JI)V

    return-void
.end method

.method public constructor <init>(Landroid/app/Activity;Landroid/view/View;JI)V
    .locals 1

    .line 30
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 21
    new-instance v0, Landroid/os/Handler;

    invoke-direct {v0}, Landroid/os/Handler;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper;->handler:Landroid/os/Handler;

    const/4 v0, 0x0

    .line 22
    iput-object v0, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper;->popupWindow:Landroid/widget/PopupWindow;

    const/4 v0, 0x0

    .line 23
    iput v0, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper;->retries:I

    .line 31
    iput-object p1, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper;->activity:Landroid/app/Activity;

    .line 32
    iput-object p2, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper;->view:Landroid/view/View;

    .line 33
    iput-wide p3, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper;->retryMs:J

    .line 34
    iput p5, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper;->maxRetries:I

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/gowrap/wrapper/OverlayHelper;)Landroid/widget/PopupWindow;
    .locals 0

    .line 13
    iget-object p0, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper;->popupWindow:Landroid/widget/PopupWindow;

    return-object p0
.end method

.method private getLeafView(Landroid/view/View;)Landroid/view/View;
    .locals 2

    .line 92
    instance-of v0, p1, Landroid/view/ViewGroup;

    if-eqz v0, :cond_2

    .line 93
    check-cast p1, Landroid/view/ViewGroup;

    const/4 v0, 0x0

    .line 94
    :goto_0
    invoke-virtual {p1}, Landroid/view/ViewGroup;->getChildCount()I

    move-result v1

    if-ge v0, v1, :cond_1

    .line 95
    invoke-virtual {p1, v0}, Landroid/view/ViewGroup;->getChildAt(I)Landroid/view/View;

    move-result-object v1

    .line 96
    invoke-direct {p0, v1}, Lnet/gogame/gowrap/wrapper/OverlayHelper;->getLeafView(Landroid/view/View;)Landroid/view/View;

    move-result-object v1

    if-eqz v1, :cond_0

    return-object v1

    :cond_0
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_1
    const/4 p1, 0x0

    return-object p1

    :cond_2
    return-object p1
.end method


# virtual methods
.method public hide()V
    .locals 1

    .line 85
    iget-object v0, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper;->popupWindow:Landroid/widget/PopupWindow;

    if-eqz v0, :cond_0

    .line 86
    iget-object v0, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper;->popupWindow:Landroid/widget/PopupWindow;

    invoke-virtual {v0}, Landroid/widget/PopupWindow;->dismiss()V

    const/4 v0, 0x0

    .line 87
    iput-object v0, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper;->popupWindow:Landroid/widget/PopupWindow;

    :cond_0
    return-void
.end method

.method public show(III)V
    .locals 9

    .line 38
    iget-object v0, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper;->popupWindow:Landroid/widget/PopupWindow;

    if-eqz v0, :cond_0

    return-void

    .line 42
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper;->activity:Landroid/app/Activity;

    invoke-virtual {v0}, Landroid/app/Activity;->getWindow()Landroid/view/Window;

    move-result-object v0

    invoke-virtual {v0}, Landroid/view/Window;->getDecorView()Landroid/view/View;

    move-result-object v0

    const v1, 0x1020002

    .line 43
    invoke-virtual {v0, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    .line 44
    invoke-direct {p0, v0}, Lnet/gogame/gowrap/wrapper/OverlayHelper;->getLeafView(Landroid/view/View;)Landroid/view/View;

    move-result-object v1

    if-nez v1, :cond_2

    .line 47
    iget v1, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper;->retries:I

    add-int/lit8 v1, v1, 0x1

    iput v1, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper;->retries:I

    .line 48
    iget v1, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper;->retries:I

    iget v2, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper;->maxRetries:I

    if-gt v1, v2, :cond_1

    .line 49
    iget-object v0, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper;->handler:Landroid/os/Handler;

    new-instance v1, Lnet/gogame/gowrap/wrapper/OverlayHelper$1;

    invoke-direct {v1, p0, p1, p2, p3}, Lnet/gogame/gowrap/wrapper/OverlayHelper$1;-><init>(Lnet/gogame/gowrap/wrapper/OverlayHelper;III)V

    iget-wide p1, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper;->retryMs:J

    invoke-virtual {v0, v1, p1, p2}, Landroid/os/Handler;->postDelayed(Ljava/lang/Runnable;J)Z

    return-void

    :cond_1
    move-object v1, v0

    :cond_2
    const/4 v2, 0x0

    .line 62
    iput v2, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper;->retries:I

    .line 64
    invoke-virtual {v1}, Landroid/view/View;->getParent()Landroid/view/ViewParent;

    move-result-object v1

    move-object v5, v1

    check-cast v5, Landroid/view/ViewGroup;

    .line 66
    new-instance v1, Landroid/widget/PopupWindow;

    iget-object v3, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper;->view:Landroid/view/View;

    const/4 v4, -0x2

    invoke-direct {v1, v3, v4, v4, v2}, Landroid/widget/PopupWindow;-><init>(Landroid/view/View;IIZ)V

    iput-object v1, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper;->popupWindow:Landroid/widget/PopupWindow;

    .line 68
    iget-object v1, p0, Lnet/gogame/gowrap/wrapper/OverlayHelper;->popupWindow:Landroid/widget/PopupWindow;

    invoke-virtual {v1, v2}, Landroid/widget/PopupWindow;->setClippingEnabled(Z)V

    .line 69
    new-instance v1, Lnet/gogame/gowrap/wrapper/OverlayHelper$2;

    move-object v3, v1

    move-object v4, p0

    move v6, p1

    move v7, p2

    move v8, p3

    invoke-direct/range {v3 .. v8}, Lnet/gogame/gowrap/wrapper/OverlayHelper$2;-><init>(Lnet/gogame/gowrap/wrapper/OverlayHelper;Landroid/view/ViewGroup;III)V

    invoke-virtual {v0, v1}, Landroid/view/View;->post(Ljava/lang/Runnable;)Z

    return-void
.end method
