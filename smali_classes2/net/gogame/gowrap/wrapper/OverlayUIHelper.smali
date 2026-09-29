.class public final Lnet/gogame/gowrap/wrapper/OverlayUIHelper;
.super Ljava/lang/Object;
.source "OverlayUIHelper.java"


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 10
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static getLeafView(Landroid/view/View;)Landroid/view/View;
    .locals 2

    .line 24
    instance-of v0, p0, Landroid/view/ViewGroup;

    if-eqz v0, :cond_2

    .line 25
    check-cast p0, Landroid/view/ViewGroup;

    const/4 v0, 0x0

    .line 26
    :goto_0
    invoke-virtual {p0}, Landroid/view/ViewGroup;->getChildCount()I

    move-result v1

    if-ge v0, v1, :cond_1

    .line 27
    invoke-virtual {p0, v0}, Landroid/view/ViewGroup;->getChildAt(I)Landroid/view/View;

    move-result-object v1

    .line 28
    invoke-static {v1}, Lnet/gogame/gowrap/wrapper/OverlayUIHelper;->getLeafView(Landroid/view/View;)Landroid/view/View;

    move-result-object v1

    if-eqz v1, :cond_0

    return-object v1

    :cond_0
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_1
    const/4 p0, 0x0

    return-object p0

    :cond_2
    return-object p0
.end method

.method public static getRootView(Landroid/app/Activity;)Landroid/view/View;
    .locals 1

    .line 19
    invoke-virtual {p0}, Landroid/app/Activity;->getWindow()Landroid/view/Window;

    move-result-object p0

    invoke-virtual {p0}, Landroid/view/Window;->getDecorView()Landroid/view/View;

    move-result-object p0

    const v0, 0x1020002

    .line 20
    invoke-virtual {p0, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p0

    return-object p0
.end method

.method public static getTopMostView(Landroid/app/Activity;)Landroid/view/View;
    .locals 0

    .line 14
    invoke-static {p0}, Lnet/gogame/gowrap/wrapper/OverlayUIHelper;->getRootView(Landroid/app/Activity;)Landroid/view/View;

    move-result-object p0

    .line 15
    invoke-static {p0}, Lnet/gogame/gowrap/wrapper/OverlayUIHelper;->getLeafView(Landroid/view/View;)Landroid/view/View;

    move-result-object p0

    return-object p0
.end method
