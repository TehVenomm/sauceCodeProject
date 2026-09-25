.class public Lcom/helpshift/network/connectivity/HSConnectivityManager;
.super Ljava/lang/Object;
.source "HSConnectivityManager.java"

# interfaces
.implements Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;


# static fields
.field private static instance:Lcom/helpshift/network/connectivity/HSConnectivityManager;


# instance fields
.field private connectivityCallbacks:Ljava/util/Set;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Set<",
            "Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;",
            ">;"
        }
    .end annotation
.end field

.field private context:Landroid/content/Context;

.field private hsAndroidConnectivityManager:Lcom/helpshift/network/connectivity/HSAndroidConnectivityManager;

.field private hsAndroidConnectivityManagerProvider:Lcom/helpshift/network/connectivity/HSAndroidConnectivityManagerProvider;


# direct methods
.method private constructor <init>()V
    .locals 1

    .line 28
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 24
    new-instance v0, Ljava/util/LinkedHashSet;

    invoke-direct {v0}, Ljava/util/LinkedHashSet;-><init>()V

    .line 25
    invoke-static {v0}, Ljava/util/Collections;->synchronizedSet(Ljava/util/Set;)Ljava/util/Set;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->connectivityCallbacks:Ljava/util/Set;

    .line 29
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->context:Landroid/content/Context;

    .line 30
    new-instance v0, Lcom/helpshift/network/connectivity/HSAndroidConnectivityManagerProvider;

    invoke-direct {v0}, Lcom/helpshift/network/connectivity/HSAndroidConnectivityManagerProvider;-><init>()V

    iput-object v0, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->hsAndroidConnectivityManagerProvider:Lcom/helpshift/network/connectivity/HSAndroidConnectivityManagerProvider;

    return-void
.end method

.method public static getInstance()Lcom/helpshift/network/connectivity/HSConnectivityManager;
    .locals 1

    .line 34
    sget-object v0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->instance:Lcom/helpshift/network/connectivity/HSConnectivityManager;

    if-nez v0, :cond_0

    .line 35
    new-instance v0, Lcom/helpshift/network/connectivity/HSConnectivityManager;

    invoke-direct {v0}, Lcom/helpshift/network/connectivity/HSConnectivityManager;-><init>()V

    sput-object v0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->instance:Lcom/helpshift/network/connectivity/HSConnectivityManager;

    .line 37
    :cond_0
    sget-object v0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->instance:Lcom/helpshift/network/connectivity/HSConnectivityManager;

    return-object v0
.end method

.method private startListenNetworkStatus()V
    .locals 2

    .line 75
    iget-object v0, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->hsAndroidConnectivityManager:Lcom/helpshift/network/connectivity/HSAndroidConnectivityManager;

    if-nez v0, :cond_0

    .line 76
    iget-object v0, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->hsAndroidConnectivityManagerProvider:Lcom/helpshift/network/connectivity/HSAndroidConnectivityManagerProvider;

    iget-object v1, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->context:Landroid/content/Context;

    invoke-virtual {v0, v1}, Lcom/helpshift/network/connectivity/HSAndroidConnectivityManagerProvider;->getOSConnectivityManager(Landroid/content/Context;)Lcom/helpshift/network/connectivity/HSAndroidConnectivityManager;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->hsAndroidConnectivityManager:Lcom/helpshift/network/connectivity/HSAndroidConnectivityManager;

    .line 80
    :cond_0
    iget-object v0, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->hsAndroidConnectivityManager:Lcom/helpshift/network/connectivity/HSAndroidConnectivityManager;

    invoke-interface {v0, p0}, Lcom/helpshift/network/connectivity/HSAndroidConnectivityManager;->startListeningConnectivityChange(Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;)V

    return-void
.end method

.method private stopListenNetworkStatus()V
    .locals 1

    .line 84
    iget-object v0, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->hsAndroidConnectivityManager:Lcom/helpshift/network/connectivity/HSAndroidConnectivityManager;

    if-nez v0, :cond_0

    return-void

    .line 88
    :cond_0
    iget-object v0, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->hsAndroidConnectivityManager:Lcom/helpshift/network/connectivity/HSAndroidConnectivityManager;

    invoke-interface {v0}, Lcom/helpshift/network/connectivity/HSAndroidConnectivityManager;->stopListeningConnectivityChange()V

    const/4 v0, 0x0

    .line 89
    iput-object v0, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->hsAndroidConnectivityManager:Lcom/helpshift/network/connectivity/HSAndroidConnectivityManager;

    return-void
.end method


# virtual methods
.method public getConnectivityType()Lcom/helpshift/network/connectivity/HSConnectivityType;
    .locals 2

    .line 115
    iget-object v0, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->hsAndroidConnectivityManager:Lcom/helpshift/network/connectivity/HSAndroidConnectivityManager;

    if-nez v0, :cond_0

    .line 116
    iget-object v0, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->hsAndroidConnectivityManagerProvider:Lcom/helpshift/network/connectivity/HSAndroidConnectivityManagerProvider;

    iget-object v1, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->context:Landroid/content/Context;

    invoke-virtual {v0, v1}, Lcom/helpshift/network/connectivity/HSAndroidConnectivityManagerProvider;->getOSConnectivityManager(Landroid/content/Context;)Lcom/helpshift/network/connectivity/HSAndroidConnectivityManager;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->hsAndroidConnectivityManager:Lcom/helpshift/network/connectivity/HSAndroidConnectivityManager;

    .line 118
    :cond_0
    iget-object v0, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->hsAndroidConnectivityManager:Lcom/helpshift/network/connectivity/HSAndroidConnectivityManager;

    invoke-interface {v0}, Lcom/helpshift/network/connectivity/HSAndroidConnectivityManager;->getConnectivityType()Lcom/helpshift/network/connectivity/HSConnectivityType;

    move-result-object v0

    return-object v0
.end method

.method public onNetworkAvailable()V
    .locals 2

    .line 94
    iget-object v0, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->connectivityCallbacks:Ljava/util/Set;

    invoke-interface {v0}, Ljava/util/Set;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 98
    :cond_0
    iget-object v0, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->connectivityCallbacks:Ljava/util/Set;

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;

    .line 99
    invoke-interface {v1}, Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;->onNetworkAvailable()V

    goto :goto_0

    :cond_1
    return-void
.end method

.method public onNetworkUnavailable()V
    .locals 2

    .line 105
    iget-object v0, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->connectivityCallbacks:Ljava/util/Set;

    invoke-interface {v0}, Ljava/util/Set;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 109
    :cond_0
    iget-object v0, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->connectivityCallbacks:Ljava/util/Set;

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;

    .line 110
    invoke-interface {v1}, Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;->onNetworkUnavailable()V

    goto :goto_0

    :cond_1
    return-void
.end method

.method public declared-synchronized registerNetworkConnectivityListener(Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;)V
    .locals 2
    .param p1    # Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    monitor-enter p0

    .line 42
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->connectivityCallbacks:Ljava/util/Set;

    invoke-interface {v0}, Ljava/util/Set;->isEmpty()Z

    move-result v0

    .line 43
    iget-object v1, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->connectivityCallbacks:Ljava/util/Set;

    invoke-interface {v1, p1}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    if-eqz v0, :cond_0

    .line 47
    invoke-direct {p0}, Lcom/helpshift/network/connectivity/HSConnectivityManager;->startListenNetworkStatus()V

    goto :goto_0

    .line 51
    :cond_0
    iget-object v0, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->hsAndroidConnectivityManager:Lcom/helpshift/network/connectivity/HSAndroidConnectivityManager;

    invoke-interface {v0}, Lcom/helpshift/network/connectivity/HSAndroidConnectivityManager;->getConnectivityStatus()Lcom/helpshift/network/connectivity/HSConnectivityStatus;

    move-result-object v0

    .line 52
    sget-object v1, Lcom/helpshift/network/connectivity/HSConnectivityManager$1;->$SwitchMap$com$helpshift$network$connectivity$HSConnectivityStatus:[I

    invoke-virtual {v0}, Lcom/helpshift/network/connectivity/HSConnectivityStatus;->ordinal()I

    move-result v0

    aget v0, v1, v0

    packed-switch v0, :pswitch_data_0

    goto :goto_0

    .line 57
    :pswitch_0
    invoke-interface {p1}, Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;->onNetworkUnavailable()V

    goto :goto_0

    .line 54
    :pswitch_1
    invoke-interface {p1}, Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;->onNetworkAvailable()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 61
    :goto_0
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 41
    monitor-exit p0

    throw p1

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public declared-synchronized unregisterNetworkConnectivityListener(Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;)V
    .locals 1
    .param p1    # Lcom/helpshift/network/connectivity/HSNetworkConnectivityCallback;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    monitor-enter p0

    .line 65
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->connectivityCallbacks:Ljava/util/Set;

    invoke-interface {v0, p1}, Ljava/util/Set;->remove(Ljava/lang/Object;)Z

    .line 68
    iget-object p1, p0, Lcom/helpshift/network/connectivity/HSConnectivityManager;->connectivityCallbacks:Ljava/util/Set;

    invoke-interface {p1}, Ljava/util/Set;->isEmpty()Z

    move-result p1

    if-eqz p1, :cond_0

    .line 69
    invoke-direct {p0}, Lcom/helpshift/network/connectivity/HSConnectivityManager;->stopListenNetworkStatus()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 71
    :cond_0
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 64
    monitor-exit p0

    throw p1
.end method
