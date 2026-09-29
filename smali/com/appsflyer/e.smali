.class final Lcom/appsflyer/e;
.super Ljava/lang/Object;
.source ""

# interfaces
.implements Lcom/android/installreferrer/api/InstallReferrerStateListener;


# instance fields
.field private ˊ:Lcom/android/installreferrer/api/InstallReferrerClient;

.field private ˎ:Lcom/appsflyer/b;


# direct methods
.method constructor <init>()V
    .locals 0

    .line 22
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onInstallReferrerServiceDisconnected()V
    .locals 1

    const-string v0, "Install Referrer service disconnected"

    .line 128
    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V

    return-void
.end method

.method public final onInstallReferrerSetupFinished(I)V
    .locals 5

    .line 77
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "code"

    .line 78
    invoke-static {p1}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const/4 v1, 0x0

    packed-switch p1, :pswitch_data_0

    const-string p1, "responseCode not found."

    .line 120
    invoke-static {p1}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    goto/16 :goto_3

    :pswitch_0
    const-string p1, "InstallReferrer not supported"

    .line 114
    invoke-static {p1}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    goto/16 :goto_3

    :pswitch_1
    const-string p1, "InstallReferrer not supported"

    .line 117
    invoke-static {p1}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    goto/16 :goto_3

    :pswitch_2
    :try_start_0
    const-string p1, "InstallReferrer connected"

    .line 88
    invoke-static {p1}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V

    .line 90
    iget-object p1, p0, Lcom/appsflyer/e;->ˊ:Lcom/android/installreferrer/api/InstallReferrerClient;

    invoke-virtual {p1}, Lcom/android/installreferrer/api/InstallReferrerClient;->isReady()Z

    move-result p1

    if-eqz p1, :cond_0

    .line 91
    iget-object p1, p0, Lcom/appsflyer/e;->ˊ:Lcom/android/installreferrer/api/InstallReferrerClient;

    invoke-virtual {p1}, Lcom/android/installreferrer/api/InstallReferrerClient;->getInstallReferrer()Lcom/android/installreferrer/api/ReferrerDetails;

    move-result-object p1
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_5
    .catch Ljava/lang/IllegalStateException; {:try_start_0 .. :try_end_0} :catch_4
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_3

    .line 92
    :try_start_1
    iget-object v1, p0, Lcom/appsflyer/e;->ˊ:Lcom/android/installreferrer/api/InstallReferrerClient;

    invoke-virtual {v1}, Lcom/android/installreferrer/api/InstallReferrerClient;->endConnection()V
    :try_end_1
    .catch Landroid/os/RemoteException; {:try_start_1 .. :try_end_1} :catch_2
    .catch Ljava/lang/IllegalStateException; {:try_start_1 .. :try_end_1} :catch_1
    .catch Ljava/lang/Throwable; {:try_start_1 .. :try_end_1} :catch_0

    move-object v1, p1

    goto/16 :goto_3

    :catch_0
    move-exception v1

    move-object v4, v1

    move-object v1, p1

    move-object p1, v4

    goto :goto_0

    :catch_1
    move-exception v1

    move-object v4, v1

    move-object v1, p1

    move-object p1, v4

    goto :goto_1

    :catch_2
    move-exception v1

    move-object v4, v1

    move-object v1, p1

    move-object p1, v4

    goto :goto_2

    :cond_0
    :try_start_2
    const-string p1, "ReferrerClient: InstallReferrer is not ready"

    .line 96
    invoke-static {p1}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    const-string p1, "err"

    const-string v2, "ReferrerClient: InstallReferrer is not ready"

    .line 97
    invoke-interface {v0, p1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_2
    .catch Landroid/os/RemoteException; {:try_start_2 .. :try_end_2} :catch_5
    .catch Ljava/lang/IllegalStateException; {:try_start_2 .. :try_end_2} :catch_4
    .catch Ljava/lang/Throwable; {:try_start_2 .. :try_end_2} :catch_3

    goto :goto_3

    :catch_3
    move-exception p1

    .line 109
    :goto_0
    new-instance v2, Ljava/lang/StringBuilder;

    const-string v3, "Failed to get install referrer: "

    invoke-direct {v2, v3}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {p1}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    const-string v2, "err"

    .line 110
    invoke-virtual {p1}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object p1

    invoke-interface {v0, v2, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_3

    :catch_4
    move-exception p1

    .line 105
    :goto_1
    new-instance v2, Ljava/lang/StringBuilder;

    const-string v3, "Failed to get install referrer: "

    invoke-direct {v2, v3}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {p1}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    const-string v2, "err"

    .line 106
    invoke-virtual {p1}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object p1

    invoke-interface {v0, v2, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_3

    :catch_5
    move-exception p1

    .line 101
    :goto_2
    new-instance v2, Ljava/lang/StringBuilder;

    const-string v3, "Failed to get install referrer: "

    invoke-direct {v2, v3}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {p1}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    const-string v2, "err"

    .line 102
    invoke-virtual {p1}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object p1

    invoke-interface {v0, v2, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :goto_3
    if-eqz v1, :cond_2

    .line 3061
    invoke-virtual {v1}, Lcom/android/installreferrer/api/ReferrerDetails;->getInstallReferrer()Ljava/lang/String;

    move-result-object p1

    if-eqz p1, :cond_1

    const-string p1, "val"

    .line 3062
    invoke-virtual {v1}, Lcom/android/installreferrer/api/ReferrerDetails;->getInstallReferrer()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, p1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_1
    const-string p1, "clk"

    .line 3065
    invoke-virtual {v1}, Lcom/android/installreferrer/api/ReferrerDetails;->getReferrerClickTimestampSeconds()J

    move-result-wide v2

    invoke-static {v2, v3}, Ljava/lang/Long;->toString(J)Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, p1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p1, "install"

    .line 3066
    invoke-virtual {v1}, Lcom/android/installreferrer/api/ReferrerDetails;->getInstallBeginTimestampSeconds()J

    move-result-wide v1

    invoke-static {v1, v2}, Ljava/lang/Long;->toString(J)Ljava/lang/String;

    move-result-object v1

    invoke-interface {v0, p1, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 3069
    :cond_2
    iget-object p1, p0, Lcom/appsflyer/e;->ˎ:Lcom/appsflyer/b;

    if-eqz p1, :cond_3

    .line 3070
    iget-object p1, p0, Lcom/appsflyer/e;->ˎ:Lcom/appsflyer/b;

    invoke-interface {p1, v0}, Lcom/appsflyer/b;->onHandleReferrer(Ljava/util/Map;)V

    :cond_3
    return-void

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method protected final ˏ(Landroid/content/Context;Lcom/appsflyer/b;)V
    .locals 0

    .line 34
    iput-object p2, p0, Lcom/appsflyer/e;->ˎ:Lcom/appsflyer/b;

    .line 36
    invoke-static {p1}, Lcom/android/installreferrer/api/InstallReferrerClient;->newBuilder(Landroid/content/Context;)Lcom/android/installreferrer/api/InstallReferrerClient$Builder;

    move-result-object p1

    invoke-virtual {p1}, Lcom/android/installreferrer/api/InstallReferrerClient$Builder;->build()Lcom/android/installreferrer/api/InstallReferrerClient;

    move-result-object p1

    iput-object p1, p0, Lcom/appsflyer/e;->ˊ:Lcom/android/installreferrer/api/InstallReferrerClient;

    .line 39
    :try_start_0
    iget-object p1, p0, Lcom/appsflyer/e;->ˊ:Lcom/android/installreferrer/api/InstallReferrerClient;

    invoke-virtual {p1, p0}, Lcom/android/installreferrer/api/InstallReferrerClient;->startConnection(Lcom/android/installreferrer/api/InstallReferrerStateListener;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception p1

    const-string p2, "referrerClient -> startConnection"

    .line 43
    invoke-static {p2, p1}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    return-void
.end method
