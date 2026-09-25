.class public Lnet/gogame/gowrap/wrapper/BannerHelper;
.super Ljava/lang/Object;
.source "BannerHelper.java"


# instance fields
.field private final activity:Landroid/app/Activity;

.field private final gravity:I

.field private parentLayout:Landroid/view/ViewGroup;

.field private popupWindow:Landroid/widget/PopupWindow;

.field private final view:Landroid/view/View;


# direct methods
.method public constructor <init>(Landroid/app/Activity;ILandroid/view/View;)V
    .locals 0

    .line 22
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 24
    iput-object p1, p0, Lnet/gogame/gowrap/wrapper/BannerHelper;->activity:Landroid/app/Activity;

    .line 25
    iput p2, p0, Lnet/gogame/gowrap/wrapper/BannerHelper;->gravity:I

    .line 26
    iput-object p3, p0, Lnet/gogame/gowrap/wrapper/BannerHelper;->view:Landroid/view/View;

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/gowrap/wrapper/BannerHelper;)Landroid/widget/PopupWindow;
    .locals 0

    .line 13
    iget-object p0, p0, Lnet/gogame/gowrap/wrapper/BannerHelper;->popupWindow:Landroid/widget/PopupWindow;

    return-object p0
.end method

.method static synthetic access$002(Lnet/gogame/gowrap/wrapper/BannerHelper;Landroid/widget/PopupWindow;)Landroid/widget/PopupWindow;
    .locals 0

    .line 13
    iput-object p1, p0, Lnet/gogame/gowrap/wrapper/BannerHelper;->popupWindow:Landroid/widget/PopupWindow;

    return-object p1
.end method

.method static synthetic access$100(Lnet/gogame/gowrap/wrapper/BannerHelper;)Landroid/view/View;
    .locals 0

    .line 13
    iget-object p0, p0, Lnet/gogame/gowrap/wrapper/BannerHelper;->view:Landroid/view/View;

    return-object p0
.end method

.method static synthetic access$200(Lnet/gogame/gowrap/wrapper/BannerHelper;)Landroid/view/ViewGroup;
    .locals 0

    .line 13
    iget-object p0, p0, Lnet/gogame/gowrap/wrapper/BannerHelper;->parentLayout:Landroid/view/ViewGroup;

    return-object p0
.end method

.method static synthetic access$300(Lnet/gogame/gowrap/wrapper/BannerHelper;)I
    .locals 0

    .line 13
    iget p0, p0, Lnet/gogame/gowrap/wrapper/BannerHelper;->gravity:I

    return p0
.end method


# virtual methods
.method public hide()V
    .locals 1

    .line 63
    iget-object v0, p0, Lnet/gogame/gowrap/wrapper/BannerHelper;->view:Landroid/view/View;

    instance-of v0, v0, Landroid/view/ViewGroup;

    if-eqz v0, :cond_0

    .line 64
    iget-object v0, p0, Lnet/gogame/gowrap/wrapper/BannerHelper;->view:Landroid/view/View;

    check-cast v0, Landroid/view/ViewGroup;

    .line 65
    invoke-virtual {v0}, Landroid/view/ViewGroup;->removeAllViews()V

    .line 67
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/wrapper/BannerHelper;->popupWindow:Landroid/widget/PopupWindow;

    if-nez v0, :cond_1

    return-void

    .line 70
    :cond_1
    iget-object v0, p0, Lnet/gogame/gowrap/wrapper/BannerHelper;->popupWindow:Landroid/widget/PopupWindow;

    invoke-virtual {v0}, Landroid/widget/PopupWindow;->dismiss()V

    const/4 v0, 0x0

    .line 71
    iput-object v0, p0, Lnet/gogame/gowrap/wrapper/BannerHelper;->popupWindow:Landroid/widget/PopupWindow;

    .line 72
    iput-object v0, p0, Lnet/gogame/gowrap/wrapper/BannerHelper;->parentLayout:Landroid/view/ViewGroup;

    return-void
.end method

.method public show()Z
    .locals 4

    .line 30
    iget-object v0, p0, Lnet/gogame/gowrap/wrapper/BannerHelper;->parentLayout:Landroid/view/ViewGroup;

    const/4 v1, 0x1

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/wrapper/BannerHelper;->popupWindow:Landroid/widget/PopupWindow;

    if-eqz v0, :cond_0

    return v1

    .line 33
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/wrapper/BannerHelper;->parentLayout:Landroid/view/ViewGroup;

    if-nez v0, :cond_3

    .line 34
    iget-object v0, p0, Lnet/gogame/gowrap/wrapper/BannerHelper;->activity:Landroid/app/Activity;

    invoke-static {v0}, Lnet/gogame/gowrap/wrapper/OverlayUIHelper;->getRootView(Landroid/app/Activity;)Landroid/view/View;

    move-result-object v0

    const/4 v2, 0x0

    if-nez v0, :cond_1

    return v2

    .line 38
    :cond_1
    instance-of v3, v0, Landroid/view/ViewGroup;

    if-nez v3, :cond_2

    return v2

    .line 41
    :cond_2
    check-cast v0, Landroid/view/ViewGroup;

    iput-object v0, p0, Lnet/gogame/gowrap/wrapper/BannerHelper;->parentLayout:Landroid/view/ViewGroup;

    .line 43
    :cond_3
    new-instance v0, Landroid/os/Handler;

    iget-object v2, p0, Lnet/gogame/gowrap/wrapper/BannerHelper;->activity:Landroid/app/Activity;

    invoke-virtual {v2}, Landroid/app/Activity;->getMainLooper()Landroid/os/Looper;

    move-result-object v2

    invoke-direct {v0, v2}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    .line 44
    new-instance v2, Lnet/gogame/gowrap/wrapper/BannerHelper$1;

    invoke-direct {v2, p0}, Lnet/gogame/gowrap/wrapper/BannerHelper$1;-><init>(Lnet/gogame/gowrap/wrapper/BannerHelper;)V

    invoke-virtual {v0, v2}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return v1
.end method
