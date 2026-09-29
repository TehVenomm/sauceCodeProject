.class public Ljp/colopl/util/DialogUtil;
.super Ljava/lang/Object;
.source "DialogUtil.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 9
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static show(Landroid/app/AlertDialog$Builder;)Landroid/app/Dialog;
    .locals 0

    .line 22
    invoke-virtual {p0}, Landroid/app/AlertDialog$Builder;->create()Landroid/app/AlertDialog;

    move-result-object p0

    invoke-static {p0}, Ljp/colopl/util/DialogUtil;->show(Landroid/app/Dialog;)Landroid/app/Dialog;

    move-result-object p0

    return-object p0
.end method

.method public static show(Landroid/app/Dialog;)Landroid/app/Dialog;
    .locals 2

    .line 28
    invoke-virtual {p0}, Landroid/app/Dialog;->getContext()Landroid/content/Context;

    move-result-object v0

    .line 29
    instance-of v1, v0, Landroid/app/Activity;

    if-eqz v1, :cond_0

    .line 30
    check-cast v0, Landroid/app/Activity;

    goto :goto_0

    .line 31
    :cond_0
    instance-of v1, v0, Landroid/view/ContextThemeWrapper;

    if-eqz v1, :cond_1

    .line 32
    check-cast v0, Landroid/view/ContextThemeWrapper;

    invoke-virtual {v0}, Landroid/view/ContextThemeWrapper;->getBaseContext()Landroid/content/Context;

    move-result-object v0

    .line 33
    instance-of v1, v0, Landroid/app/Activity;

    if-eqz v1, :cond_1

    .line 34
    check-cast v0, Landroid/app/Activity;

    goto :goto_0

    :cond_1
    const/4 v0, 0x0

    :goto_0
    if-eqz v0, :cond_2

    .line 37
    invoke-virtual {v0}, Landroid/app/Activity;->isFinishing()Z

    move-result v0

    if-nez v0, :cond_2

    .line 38
    invoke-virtual {p0}, Landroid/app/Dialog;->show()V

    :cond_2
    return-object p0
.end method

.method private static showErrorDialog(Landroid/content/Context;III)V
    .locals 1

    .line 12
    new-instance v0, Landroid/app/AlertDialog$Builder;

    invoke-direct {v0, p0}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    .line 13
    invoke-virtual {v0, p1}, Landroid/app/AlertDialog$Builder;->setTitle(I)Landroid/app/AlertDialog$Builder;

    move-result-object p0

    .line 14
    invoke-virtual {p0, p2}, Landroid/app/AlertDialog$Builder;->setMessage(I)Landroid/app/AlertDialog$Builder;

    move-result-object p0

    const/4 p1, 0x0

    .line 15
    invoke-virtual {p0, p1}, Landroid/app/AlertDialog$Builder;->setCancelable(Z)Landroid/app/AlertDialog$Builder;

    move-result-object p0

    const/4 p1, 0x0

    .line 16
    invoke-virtual {p0, p3, p1}, Landroid/app/AlertDialog$Builder;->setPositiveButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    move-result-object p0

    invoke-virtual {p0}, Landroid/app/AlertDialog$Builder;->show()Landroid/app/AlertDialog;

    return-void
.end method
