.class public Lcom/helpshift/util/ActivityUtil;
.super Ljava/lang/Object;
.source "ActivityUtil.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 7
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static isFullScreen(Landroid/app/Activity;)Ljava/lang/Boolean;
    .locals 1

    .line 9
    invoke-virtual {p0}, Landroid/app/Activity;->getWindow()Landroid/view/Window;

    move-result-object p0

    .line 10
    invoke-virtual {p0}, Landroid/view/Window;->getAttributes()Landroid/view/WindowManager$LayoutParams;

    move-result-object p0

    .line 11
    iget p0, p0, Landroid/view/WindowManager$LayoutParams;->flags:I

    const/16 v0, 0x400

    and-int/2addr p0, v0

    if-ne p0, v0, :cond_0

    const/4 p0, 0x1

    goto :goto_0

    :cond_0
    const/4 p0, 0x0

    :goto_0
    invoke-static {p0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p0

    return-object p0
.end method
