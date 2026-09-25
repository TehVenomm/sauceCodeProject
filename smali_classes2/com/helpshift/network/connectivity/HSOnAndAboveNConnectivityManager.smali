.class Lcom/helpshift/network/connectivity/HSOnAndAboveNConnectivityManager;
.super Landroid/net/ConnectivityManager$NetworkCallback;
.source "HSOnAndAboveNConnectivityManager.java"

# interfaces
.implements Lcom/helpshift/network/connectivity/HSAndroidConnectivityManager;


# annotations
.annotation build Landroidx/annotation/RequiresApi;
    api = 0x18
.end annotation


# static fields
.field private static final TAG:Ljava/lang/String; = "Helpshift_AboveNConnMan"


# instance fields
.field private context:Landroid/content/Context;

.field private networkListener:Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;


# direct methods
.method constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 24
    invoke-direct {p0}, Landroid/net/ConnectivityManager$NetworkCallback;-><init>()V

    .line 25
    iput-object p1, p0, Lcom/helpshift/network/connectivity/HSOnAndAboveNConnectivityManager;->context:Landroid/content/Context;

    return-void
.end method

.method private getConnectivityManager()Landroid/net/ConnectivityManager;
    .locals 3

    .line 154
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/network/connectivity/HSOnAndAboveNConnectivityManager;->context:Landroid/content/Context;

    const-string v1, "connectivity"

    invoke-virtual {v0, v1}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/net/ConnectivityManager;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "Helpshift_AboveNConnMan"

    const-string v2, "Exception while getting connectivity manager"

    .line 157
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    const/4 v0, 0x0

    :goto_0
    return-object v0
.end method

.method private getTelephonyManager()Landroid/telephony/TelephonyManager;
    .locals 3

    .line 165
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/network/connectivity/HSOnAndAboveNConnectivityManager;->context:Landroid/content/Context;

    const-string v1, "phone"

    invoke-virtual {v0, v1}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/telephony/TelephonyManager;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "Helpshift_AboveNConnMan"

    const-string v2, "Exception while getting telephony manager"

    .line 168
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    const/4 v0, 0x0

    :goto_0
    return-object v0
.end method


# virtual methods
.method public getConnectivityStatus()Lcom/helpshift/network/connectivity/HSConnectivityStatus;
    .locals 2
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    .annotation build Landroidx/annotation/RequiresApi;
        api = 0x18
    .end annotation

    .line 72
    sget-object v0, Lcom/helpshift/network/connectivity/HSConnectivityStatus;->UNKNOWN:Lcom/helpshift/network/connectivity/HSConnectivityStatus;

    .line 73
    invoke-direct {p0}, Lcom/helpshift/network/connectivity/HSOnAndAboveNConnectivityManager;->getConnectivityManager()Landroid/net/ConnectivityManager;

    move-result-object v1

    if-eqz v1, :cond_1

    .line 75
    invoke-virtual {v1}, Landroid/net/ConnectivityManager;->getActiveNetwork()Landroid/net/Network;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 77
    sget-object v0, Lcom/helpshift/network/connectivity/HSConnectivityStatus;->CONNECTED:Lcom/helpshift/network/connectivity/HSConnectivityStatus;

    goto :goto_0

    .line 80
    :cond_0
    sget-object v0, Lcom/helpshift/network/connectivity/HSConnectivityStatus;->NOT_CONNECTED:Lcom/helpshift/network/connectivity/HSConnectivityStatus;

    :cond_1
    :goto_0
    return-object v0
.end method

.method public getConnectivityType()Lcom/helpshift/network/connectivity/HSConnectivityType;
    .locals 3
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    .line 90
    invoke-direct {p0}, Lcom/helpshift/network/connectivity/HSOnAndAboveNConnectivityManager;->getConnectivityManager()Landroid/net/ConnectivityManager;

    move-result-object v0

    if-nez v0, :cond_0

    .line 92
    sget-object v0, Lcom/helpshift/network/connectivity/HSConnectivityType;->UNKNOWN:Lcom/helpshift/network/connectivity/HSConnectivityType;

    return-object v0

    .line 96
    :cond_0
    invoke-virtual {v0}, Landroid/net/ConnectivityManager;->getActiveNetwork()Landroid/net/Network;

    move-result-object v1

    if-nez v1, :cond_1

    .line 98
    sget-object v0, Lcom/helpshift/network/connectivity/HSConnectivityType;->UNKNOWN:Lcom/helpshift/network/connectivity/HSConnectivityType;

    return-object v0

    .line 102
    :cond_1
    invoke-virtual {v0, v1}, Landroid/net/ConnectivityManager;->getNetworkCapabilities(Landroid/net/Network;)Landroid/net/NetworkCapabilities;

    move-result-object v0

    if-nez v0, :cond_2

    .line 104
    sget-object v0, Lcom/helpshift/network/connectivity/HSConnectivityType;->UNKNOWN:Lcom/helpshift/network/connectivity/HSConnectivityType;

    return-object v0

    .line 107
    :cond_2
    sget-object v1, Lcom/helpshift/network/connectivity/HSConnectivityType;->UNKNOWN:Lcom/helpshift/network/connectivity/HSConnectivityType;

    const/4 v2, 0x1

    .line 109
    invoke-virtual {v0, v2}, Landroid/net/NetworkCapabilities;->hasTransport(I)Z

    move-result v2

    if-eqz v2, :cond_3

    .line 110
    sget-object v0, Lcom/helpshift/network/connectivity/HSConnectivityType;->WIFI:Lcom/helpshift/network/connectivity/HSConnectivityType;

    return-object v0

    :cond_3
    const/4 v2, 0x0

    .line 112
    invoke-virtual {v0, v2}, Landroid/net/NetworkCapabilities;->hasTransport(I)Z

    move-result v0

    if-eqz v0, :cond_5

    .line 113
    invoke-direct {p0}, Lcom/helpshift/network/connectivity/HSOnAndAboveNConnectivityManager;->getTelephonyManager()Landroid/telephony/TelephonyManager;

    move-result-object v0

    if-eqz v0, :cond_5

    .line 115
    invoke-virtual {v0}, Landroid/telephony/TelephonyManager;->getNetworkType()I

    move-result v0

    const/16 v2, 0xd

    if-eq v0, v2, :cond_4

    const/16 v2, 0xf

    if-eq v0, v2, :cond_4

    packed-switch v0, :pswitch_data_0

    goto :goto_0

    .line 122
    :pswitch_0
    sget-object v1, Lcom/helpshift/network/connectivity/HSConnectivityType;->MOBILE_2G:Lcom/helpshift/network/connectivity/HSConnectivityType;

    goto :goto_0

    .line 118
    :cond_4
    sget-object v1, Lcom/helpshift/network/connectivity/HSConnectivityType;->MOBILE_4G:Lcom/helpshift/network/connectivity/HSConnectivityType;

    :cond_5
    :goto_0
    return-object v1

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_0
        :pswitch_0
    .end packed-switch
.end method

.method public onAvailable(Landroid/net/Network;)V
    .locals 0
    .param p1    # Landroid/net/Network;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 132
    iget-object p1, p0, Lcom/helpshift/network/connectivity/HSOnAndAboveNConnectivityManager;->networkListener:Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;

    if-eqz p1, :cond_0

    .line 133
    iget-object p1, p0, Lcom/helpshift/network/connectivity/HSOnAndAboveNConnectivityManager;->networkListener:Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;

    invoke-interface {p1}, Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;->onNetworkAvailable()V

    :cond_0
    return-void
.end method

.method public onLost(Landroid/net/Network;)V
    .locals 0
    .param p1    # Landroid/net/Network;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 139
    iget-object p1, p0, Lcom/helpshift/network/connectivity/HSOnAndAboveNConnectivityManager;->networkListener:Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;

    if-eqz p1, :cond_0

    .line 140
    iget-object p1, p0, Lcom/helpshift/network/connectivity/HSOnAndAboveNConnectivityManager;->networkListener:Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;

    invoke-interface {p1}, Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;->onNetworkUnavailable()V

    :cond_0
    return-void
.end method

.method public onUnavailable()V
    .locals 1

    .line 146
    iget-object v0, p0, Lcom/helpshift/network/connectivity/HSOnAndAboveNConnectivityManager;->networkListener:Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;

    if-eqz v0, :cond_0

    .line 147
    iget-object v0, p0, Lcom/helpshift/network/connectivity/HSOnAndAboveNConnectivityManager;->networkListener:Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;

    invoke-interface {v0}, Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;->onNetworkUnavailable()V

    :cond_0
    return-void
.end method

.method public startListeningConnectivityChange(Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;)V
    .locals 3
    .annotation build Landroidx/annotation/RequiresApi;
        api = 0x18
    .end annotation

    .line 31
    iput-object p1, p0, Lcom/helpshift/network/connectivity/HSOnAndAboveNConnectivityManager;->networkListener:Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;

    .line 32
    invoke-direct {p0}, Lcom/helpshift/network/connectivity/HSOnAndAboveNConnectivityManager;->getConnectivityManager()Landroid/net/ConnectivityManager;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 35
    :try_start_0
    invoke-virtual {v0, p0}, Landroid/net/ConnectivityManager;->registerDefaultNetworkCallback(Landroid/net/ConnectivityManager$NetworkCallback;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "Helpshift_AboveNConnMan"

    const-string v2, "Exception while registering network callback"

    .line 38
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 47
    :cond_0
    :goto_0
    invoke-virtual {p0}, Lcom/helpshift/network/connectivity/HSOnAndAboveNConnectivityManager;->getConnectivityStatus()Lcom/helpshift/network/connectivity/HSConnectivityStatus;

    move-result-object v0

    .line 48
    sget-object v1, Lcom/helpshift/network/connectivity/HSConnectivityStatus;->NOT_CONNECTED:Lcom/helpshift/network/connectivity/HSConnectivityStatus;

    if-ne v0, v1, :cond_1

    .line 49
    invoke-interface {p1}, Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;->onNetworkUnavailable()V

    :cond_1
    return-void
.end method

.method public stopListeningConnectivityChange()V
    .locals 3
    .annotation build Landroidx/annotation/RequiresApi;
        api = 0x18
    .end annotation

    .line 56
    invoke-direct {p0}, Lcom/helpshift/network/connectivity/HSOnAndAboveNConnectivityManager;->getConnectivityManager()Landroid/net/ConnectivityManager;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 59
    :try_start_0
    invoke-virtual {v0, p0}, Landroid/net/ConnectivityManager;->unregisterNetworkCallback(Landroid/net/ConnectivityManager$NetworkCallback;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "Helpshift_AboveNConnMan"

    const-string v2, "Exception while unregistering network callback"

    .line 62
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_0
    :goto_0
    const/4 v0, 0x0

    .line 65
    iput-object v0, p0, Lcom/helpshift/network/connectivity/HSOnAndAboveNConnectivityManager;->networkListener:Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;

    return-void
.end method
