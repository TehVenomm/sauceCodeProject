.class public Lcom/zopim/android/sdk/data/ConnectionPath;
.super Lcom/zopim/android/sdk/data/Path;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/data/ConnectionPath$ConnectivityReceiver;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/zopim/android/sdk/data/Path<",
        "Lcom/zopim/android/sdk/model/Connection;",
        ">;"
    }
.end annotation


# static fields
.field private static final INSTANCE:Lcom/zopim/android/sdk/data/ConnectionPath;

.field private static final LOG_TAG:Ljava/lang/String; = "ConnectionPath"


# instance fields
.field private mDeviceNoConnectivity:Ljava/lang/Boolean;

.field private final mLock:Ljava/lang/Object;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    new-instance v0, Lcom/zopim/android/sdk/data/ConnectionPath;

    invoke-direct {v0}, Lcom/zopim/android/sdk/data/ConnectionPath;-><init>()V

    sput-object v0, Lcom/zopim/android/sdk/data/ConnectionPath;->INSTANCE:Lcom/zopim/android/sdk/data/ConnectionPath;

    return-void
.end method

.method private constructor <init>()V
    .locals 1

    invoke-direct {p0}, Lcom/zopim/android/sdk/data/Path;-><init>()V

    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    iput-object v0, p0, Lcom/zopim/android/sdk/data/ConnectionPath;->mLock:Ljava/lang/Object;

    return-void
.end method

.method static synthetic access$000()Lcom/zopim/android/sdk/data/ConnectionPath;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/data/ConnectionPath;->INSTANCE:Lcom/zopim/android/sdk/data/ConnectionPath;

    return-object v0
.end method

.method static synthetic access$100(Lcom/zopim/android/sdk/data/ConnectionPath;)Ljava/lang/Boolean;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/data/ConnectionPath;->mDeviceNoConnectivity:Ljava/lang/Boolean;

    return-object p0
.end method

.method static synthetic access$102(Lcom/zopim/android/sdk/data/ConnectionPath;Ljava/lang/Boolean;)Ljava/lang/Boolean;
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/data/ConnectionPath;->mDeviceNoConnectivity:Ljava/lang/Boolean;

    return-object p1
.end method

.method public static declared-synchronized getInstance()Lcom/zopim/android/sdk/data/ConnectionPath;
    .locals 2

    const-class v0, Lcom/zopim/android/sdk/data/ConnectionPath;

    monitor-enter v0

    :try_start_0
    sget-object v1, Lcom/zopim/android/sdk/data/ConnectionPath;->INSTANCE:Lcom/zopim/android/sdk/data/ConnectionPath;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit v0

    return-object v1

    :catchall_0
    move-exception v1

    monitor-exit v0

    throw v1
.end method


# virtual methods
.method clear()V
    .locals 1

    const/4 v0, 0x0

    iput-object v0, p0, Lcom/zopim/android/sdk/data/ConnectionPath;->mData:Ljava/lang/Object;

    iput-object v0, p0, Lcom/zopim/android/sdk/data/ConnectionPath;->mDeviceNoConnectivity:Ljava/lang/Boolean;

    return-void
.end method

.method public getData()Lcom/zopim/android/sdk/model/Connection;
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/data/ConnectionPath;->mDeviceNoConnectivity:Ljava/lang/Boolean;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/data/ConnectionPath;->mDeviceNoConnectivity:Ljava/lang/Boolean;

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    if-eqz v0, :cond_0

    sget-object v0, Lcom/zopim/android/sdk/data/ConnectionPath;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Device has no connection. Will return widget\'s connection as NO_CONNECTION"

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    new-instance v0, Lcom/zopim/android/sdk/model/Connection;

    sget-object v1, Lcom/zopim/android/sdk/model/Connection$Status;->NO_CONNECTION:Lcom/zopim/android/sdk/model/Connection$Status;

    invoke-direct {v0, v1}, Lcom/zopim/android/sdk/model/Connection;-><init>(Lcom/zopim/android/sdk/model/Connection$Status;)V

    return-object v0

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/data/ConnectionPath;->mData:Ljava/lang/Object;

    if-nez v0, :cond_1

    new-instance v0, Lcom/zopim/android/sdk/model/Connection;

    sget-object v1, Lcom/zopim/android/sdk/model/Connection$Status;->UNKNOWN:Lcom/zopim/android/sdk/model/Connection$Status;

    invoke-direct {v0, v1}, Lcom/zopim/android/sdk/model/Connection;-><init>(Lcom/zopim/android/sdk/model/Connection$Status;)V

    return-object v0

    :cond_1
    iget-object v0, p0, Lcom/zopim/android/sdk/data/ConnectionPath;->mData:Ljava/lang/Object;

    check-cast v0, Lcom/zopim/android/sdk/model/Connection;

    return-object v0
.end method

.method public bridge synthetic getData()Ljava/lang/Object;
    .locals 1

    invoke-virtual {p0}, Lcom/zopim/android/sdk/data/ConnectionPath;->getData()Lcom/zopim/android/sdk/model/Connection;

    move-result-object v0

    return-object v0
.end method

.method update(Ljava/lang/String;)V
    .locals 3

    if-eqz p1, :cond_1

    invoke-virtual {p1}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/data/ConnectionPath;->mLock:Ljava/lang/Object;

    monitor-enter v0

    :try_start_0
    iget-object v1, p0, Lcom/zopim/android/sdk/data/ConnectionPath;->PARSER:Lcom/zopim/android/sdk/data/Parser;

    new-instance v2, Lcom/zopim/android/sdk/data/a;

    invoke-direct {v2, p0}, Lcom/zopim/android/sdk/data/a;-><init>(Lcom/zopim/android/sdk/data/ConnectionPath;)V

    invoke-virtual {v1, p1, v2}, Lcom/zopim/android/sdk/data/Parser;->parse(Ljava/lang/String;Lcom/fasterxml/jackson/core/type/TypeReference;)Ljava/lang/Object;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/data/ConnectionPath;->mData:Ljava/lang/Object;

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/data/ConnectionPath;->getData()Lcom/zopim/android/sdk/model/Connection;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/data/ConnectionPath;->broadcast(Ljava/lang/Object;)V

    return-void

    :catchall_0
    move-exception p1

    :try_start_1
    monitor-exit v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw p1

    :cond_1
    :goto_0
    return-void
.end method
