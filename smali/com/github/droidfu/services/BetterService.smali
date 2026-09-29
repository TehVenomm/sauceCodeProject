.class public Lcom/github/droidfu/services/BetterService;
.super Landroid/app/Service;
.source "BetterService.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 24
    invoke-direct {p0}, Landroid/app/Service;-><init>()V

    return-void
.end method


# virtual methods
.method public onBind(Landroid/content/Intent;)Landroid/os/IBinder;
    .locals 0

    const/4 p1, 0x0

    return-object p1
.end method

.method public onCreate()V
    .locals 2

    .line 28
    invoke-super {p0}, Landroid/app/Service;->onCreate()V

    .line 29
    invoke-virtual {p0}, Lcom/github/droidfu/services/BetterService;->getApplication()Landroid/app/Application;

    move-result-object v0

    check-cast v0, Lcom/github/droidfu/DroidFuApplication;

    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Class;->getCanonicalName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1, p0}, Lcom/github/droidfu/DroidFuApplication;->setActiveContext(Ljava/lang/String;Landroid/content/Context;)V

    return-void
.end method
