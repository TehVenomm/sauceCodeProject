.class public Lcom/helpshift/activities/UnityDelegateActivity;
.super Landroid/app/Activity;
.source "UnityDelegateActivity.java"


# static fields
.field private static final TAG:Ljava/lang/String; = "UnityDelegateActivity"


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 15
    invoke-direct {p0}, Landroid/app/Activity;-><init>()V

    return-void
.end method


# virtual methods
.method protected onCreate(Landroid/os/Bundle;)V
    .locals 3

    .line 21
    invoke-super {p0, p1}, Landroid/app/Activity;->onCreate(Landroid/os/Bundle;)V

    .line 28
    :try_start_0
    invoke-virtual {p0}, Lcom/helpshift/activities/UnityDelegateActivity;->getApplicationContext()Landroid/content/Context;

    move-result-object p1

    invoke-static {p1}, Lcom/helpshift/util/HelpshiftUtil;->installWithCachedCreds(Landroid/content/Context;)Z

    move-result p1

    if-nez p1, :cond_0

    .line 29
    invoke-virtual {p0}, Lcom/helpshift/activities/UnityDelegateActivity;->finish()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 51
    invoke-virtual {p0}, Lcom/helpshift/activities/UnityDelegateActivity;->finish()V

    return-void

    .line 35
    :cond_0
    :try_start_1
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object p1

    invoke-interface {p1}, Lcom/helpshift/util/concurrent/ApiExecutor;->awaitForSyncExecution()V

    .line 38
    invoke-virtual {p0}, Lcom/helpshift/activities/UnityDelegateActivity;->getIntent()Landroid/content/Intent;

    move-result-object p1

    const-string v0, "delegateIntent"

    .line 39
    invoke-virtual {p1, v0}, Landroid/content/Intent;->getParcelableExtra(Ljava/lang/String;)Landroid/os/Parcelable;

    move-result-object p1

    check-cast p1, Landroid/app/PendingIntent;
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    if-eqz p1, :cond_1

    .line 42
    :try_start_2
    invoke-virtual {p1}, Landroid/app/PendingIntent;->send()V
    :try_end_2
    .catch Landroid/app/PendingIntent$CanceledException; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    goto :goto_0

    :catch_0
    move-exception p1

    .line 45
    :try_start_3
    sget-object v0, Lcom/helpshift/activities/UnityDelegateActivity;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Error in sending pending intent : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Landroid/app/PendingIntent$CanceledException;->getMessage()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    .line 51
    :cond_1
    :goto_0
    invoke-virtual {p0}, Lcom/helpshift/activities/UnityDelegateActivity;->finish()V

    return-void

    :catchall_0
    move-exception p1

    invoke-virtual {p0}, Lcom/helpshift/activities/UnityDelegateActivity;->finish()V

    .line 52
    throw p1
.end method
