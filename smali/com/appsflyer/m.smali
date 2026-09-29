.class final Lcom/appsflyer/m;
.super Ljava/lang/Object;
.source ""


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/appsflyer/m$d;,
        Lcom/appsflyer/m$e;,
        Lcom/appsflyer/m$a;
    }
.end annotation


# direct methods
.method constructor <init>()V
    .locals 0

    .line 20
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method static ˏ(Landroid/content/Context;)Lcom/appsflyer/m$a;
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 41
    invoke-static {}, Landroid/os/Looper;->myLooper()Landroid/os/Looper;

    move-result-object v0

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v1

    if-eq v0, v1, :cond_4

    .line 44
    :try_start_0
    invoke-virtual {p0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v0

    const-string v1, "com.android.vending"

    const/4 v2, 0x0

    .line 45
    invoke-virtual {v0, v1, v2}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_1

    .line 50
    new-instance v0, Lcom/appsflyer/m$e;

    invoke-direct {v0, v2}, Lcom/appsflyer/m$e;-><init>(B)V

    .line 51
    new-instance v1, Landroid/content/Intent;

    const-string v2, "com.google.android.gms.ads.identifier.service.START"

    invoke-direct {v1, v2}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    const-string v2, "com.google.android.gms"

    .line 52
    invoke-virtual {v1, v2}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    const/4 v2, 0x1

    .line 54
    :try_start_1
    invoke-virtual {p0, v1, v0, v2}, Landroid/content/Context;->bindService(Landroid/content/Intent;Landroid/content/ServiceConnection;I)Z

    move-result v1

    if-eqz v1, :cond_1

    .line 55
    new-instance v1, Lcom/appsflyer/m$d;

    invoke-virtual {v0}, Lcom/appsflyer/m$e;->ॱ()Landroid/os/IBinder;

    move-result-object v2

    invoke-direct {v1, v2}, Lcom/appsflyer/m$d;-><init>(Landroid/os/IBinder;)V

    .line 56
    new-instance v2, Lcom/appsflyer/m$a;

    invoke-virtual {v1}, Lcom/appsflyer/m$d;->ˏ()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1}, Lcom/appsflyer/m$d;->ˊ()Z

    move-result v1

    invoke-direct {v2, v3, v1}, Lcom/appsflyer/m$a;-><init>(Ljava/lang/String;Z)V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    if-eqz p0, :cond_0

    .line 63
    invoke-virtual {p0, v0}, Landroid/content/Context;->unbindService(Landroid/content/ServiceConnection;)V

    :cond_0
    return-object v2

    :cond_1
    if-eqz p0, :cond_2

    invoke-virtual {p0, v0}, Landroid/content/Context;->unbindService(Landroid/content/ServiceConnection;)V

    .line 66
    :cond_2
    new-instance p0, Ljava/io/IOException;

    const-string v0, "Google Play connection failed"

    invoke-direct {p0, v0}, Ljava/io/IOException;-><init>(Ljava/lang/String;)V

    throw p0

    :catchall_0
    move-exception v1

    goto :goto_0

    :catch_0
    move-exception v1

    .line 60
    :try_start_2
    throw v1
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    :goto_0
    if-eqz p0, :cond_3

    .line 63
    invoke-virtual {p0, v0}, Landroid/content/Context;->unbindService(Landroid/content/ServiceConnection;)V

    :cond_3
    throw v1

    :catch_1
    move-exception p0

    .line 47
    throw p0

    .line 41
    :cond_4
    new-instance p0, Ljava/lang/IllegalStateException;

    const-string v0, "Cannot be called from the main thread"

    invoke-direct {p0, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p0
.end method
