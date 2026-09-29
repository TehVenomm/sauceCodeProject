.class public abstract Lnet/gogame/gowrap/bootstrap/AbstractBootstrap;
.super Ljava/lang/Object;
.source "AbstractBootstrap.java"


# instance fields
.field private initialized:Z


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 11
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 13
    iput-boolean v0, p0, Lnet/gogame/gowrap/bootstrap/AbstractBootstrap;->initialized:Z

    return-void
.end method


# virtual methods
.method protected abstract doCustomInit(Landroid/app/Activity;)V
.end method

.method public doInit(Landroid/app/Activity;)V
    .locals 1

    const/4 v0, 0x0

    .line 41
    invoke-virtual {p0, p1, v0}, Lnet/gogame/gowrap/bootstrap/AbstractBootstrap;->doInit(Landroid/app/Activity;Z)V

    return-void
.end method

.method public doInit(Landroid/app/Activity;Z)V
    .locals 3

    .line 18
    iget-boolean v0, p0, Lnet/gogame/gowrap/bootstrap/AbstractBootstrap;->initialized:Z

    if-nez v0, :cond_2

    const-string v0, "goWrap"

    const-string v1, "Initializing..."

    .line 19
    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    const/4 v0, 0x1

    .line 21
    :try_start_0
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/bootstrap/AbstractBootstrap;->doCustomInit(Landroid/app/Activity;)V

    const-string v1, "goWrap"

    const-string v2, "Initialized"

    .line 22
    invoke-static {v1, v2}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz p2, :cond_0

    .line 26
    :try_start_1
    sget-object p2, Lnet/gogame/gowrap/ui/ActivityHelper;->INSTANCE:Lnet/gogame/gowrap/ui/ActivityHelper;

    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    invoke-virtual {p2, p1, v1}, Lnet/gogame/gowrap/ui/ActivityHelper;->onActivityCreated(Landroid/app/Activity;Landroid/os/Bundle;)V

    .line 27
    sget-object p2, Lnet/gogame/gowrap/ui/ActivityHelper;->INSTANCE:Lnet/gogame/gowrap/ui/ActivityHelper;

    invoke-virtual {p2, p1}, Lnet/gogame/gowrap/ui/ActivityHelper;->onActivityStarted(Landroid/app/Activity;)V

    .line 28
    sget-object p2, Lnet/gogame/gowrap/ui/ActivityHelper;->INSTANCE:Lnet/gogame/gowrap/ui/ActivityHelper;

    invoke-virtual {p2, p1}, Lnet/gogame/gowrap/ui/ActivityHelper;->onActivityResumed(Landroid/app/Activity;)V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string p2, "goWrap"

    const-string v1, "Exception"

    .line 30
    invoke-static {p2, v1, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 33
    :cond_0
    :goto_0
    iput-boolean v0, p0, Lnet/gogame/gowrap/bootstrap/AbstractBootstrap;->initialized:Z

    goto :goto_2

    :catchall_0
    move-exception v1

    if-eqz p2, :cond_1

    .line 26
    :try_start_2
    sget-object p2, Lnet/gogame/gowrap/ui/ActivityHelper;->INSTANCE:Lnet/gogame/gowrap/ui/ActivityHelper;

    new-instance v2, Landroid/os/Bundle;

    invoke-direct {v2}, Landroid/os/Bundle;-><init>()V

    invoke-virtual {p2, p1, v2}, Lnet/gogame/gowrap/ui/ActivityHelper;->onActivityCreated(Landroid/app/Activity;Landroid/os/Bundle;)V

    .line 27
    sget-object p2, Lnet/gogame/gowrap/ui/ActivityHelper;->INSTANCE:Lnet/gogame/gowrap/ui/ActivityHelper;

    invoke-virtual {p2, p1}, Lnet/gogame/gowrap/ui/ActivityHelper;->onActivityStarted(Landroid/app/Activity;)V

    .line 28
    sget-object p2, Lnet/gogame/gowrap/ui/ActivityHelper;->INSTANCE:Lnet/gogame/gowrap/ui/ActivityHelper;

    invoke-virtual {p2, p1}, Lnet/gogame/gowrap/ui/ActivityHelper;->onActivityResumed(Landroid/app/Activity;)V
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_1

    goto :goto_1

    :catch_1
    move-exception p1

    const-string p2, "goWrap"

    const-string v2, "Exception"

    .line 30
    invoke-static {p2, v2, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 33
    :cond_1
    :goto_1
    iput-boolean v0, p0, Lnet/gogame/gowrap/bootstrap/AbstractBootstrap;->initialized:Z

    .line 34
    throw v1

    :cond_2
    const-string p1, "goWrap"

    const-string p2, "Already initialized"

    .line 36
    invoke-static {p1, p2}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    :goto_2
    return-void
.end method

.method public doUnityInit()V
    .locals 2

    :try_start_0
    const-string v0, "com.unity3d.player.UnityPlayer"

    .line 46
    invoke-static {v0}, Ljava/lang/Class;->forName(Ljava/lang/String;)Ljava/lang/Class;

    move-result-object v0

    const-string v1, "currentActivity"

    .line 47
    invoke-virtual {v0, v1}, Ljava/lang/Class;->getField(Ljava/lang/String;)Ljava/lang/reflect/Field;

    move-result-object v0

    const/4 v1, 0x0

    .line 48
    invoke-virtual {v0, v1}, Ljava/lang/reflect/Field;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/app/Activity;

    const/4 v1, 0x1

    .line 49
    invoke-virtual {p0, v0, v1}, Lnet/gogame/gowrap/bootstrap/AbstractBootstrap;->doInit(Landroid/app/Activity;Z)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    return-void
.end method
