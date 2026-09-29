.class public Lcom/zopim/android/sdk/data/LivechatAccountPath;
.super Lcom/zopim/android/sdk/data/Path;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/zopim/android/sdk/data/Path<",
        "Lcom/zopim/android/sdk/model/Account;",
        ">;"
    }
.end annotation


# static fields
.field private static final INSTANCE:Lcom/zopim/android/sdk/data/LivechatAccountPath;

.field private static final TAG:Ljava/lang/String; = "LivechatAccountPath"


# instance fields
.field private final mLock:Ljava/lang/Object;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    new-instance v0, Lcom/zopim/android/sdk/data/LivechatAccountPath;

    invoke-direct {v0}, Lcom/zopim/android/sdk/data/LivechatAccountPath;-><init>()V

    sput-object v0, Lcom/zopim/android/sdk/data/LivechatAccountPath;->INSTANCE:Lcom/zopim/android/sdk/data/LivechatAccountPath;

    return-void
.end method

.method private constructor <init>()V
    .locals 1

    invoke-direct {p0}, Lcom/zopim/android/sdk/data/Path;-><init>()V

    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    iput-object v0, p0, Lcom/zopim/android/sdk/data/LivechatAccountPath;->mLock:Ljava/lang/Object;

    return-void
.end method

.method public static declared-synchronized getInstance()Lcom/zopim/android/sdk/data/LivechatAccountPath;
    .locals 2

    const-class v0, Lcom/zopim/android/sdk/data/LivechatAccountPath;

    monitor-enter v0

    :try_start_0
    sget-object v1, Lcom/zopim/android/sdk/data/LivechatAccountPath;->INSTANCE:Lcom/zopim/android/sdk/data/LivechatAccountPath;
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

    iput-object v0, p0, Lcom/zopim/android/sdk/data/LivechatAccountPath;->mData:Ljava/lang/Object;

    return-void
.end method

.method public getData()Lcom/zopim/android/sdk/model/Account;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/data/LivechatAccountPath;->mData:Ljava/lang/Object;

    check-cast v0, Lcom/zopim/android/sdk/model/Account;

    return-object v0
.end method

.method public bridge synthetic getData()Ljava/lang/Object;
    .locals 1

    invoke-virtual {p0}, Lcom/zopim/android/sdk/data/LivechatAccountPath;->getData()Lcom/zopim/android/sdk/model/Account;

    move-result-object v0

    return-object v0
.end method

.method update(Ljava/lang/String;)V
    .locals 3

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/data/LivechatAccountPath;->isClearRequired(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/data/LivechatAccountPath;->clear()V

    return-void

    :cond_0
    invoke-virtual {p1}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_1

    return-void

    :cond_1
    iget-object v0, p0, Lcom/zopim/android/sdk/data/LivechatAccountPath;->mLock:Ljava/lang/Object;

    monitor-enter v0

    :try_start_0
    iget-object v1, p0, Lcom/zopim/android/sdk/data/LivechatAccountPath;->mData:Ljava/lang/Object;

    if-nez v1, :cond_2

    iget-object v1, p0, Lcom/zopim/android/sdk/data/LivechatAccountPath;->PARSER:Lcom/zopim/android/sdk/data/Parser;

    new-instance v2, Lcom/zopim/android/sdk/data/b;

    invoke-direct {v2, p0}, Lcom/zopim/android/sdk/data/b;-><init>(Lcom/zopim/android/sdk/data/LivechatAccountPath;)V

    invoke-virtual {v1, p1, v2}, Lcom/zopim/android/sdk/data/Parser;->parse(Ljava/lang/String;Lcom/fasterxml/jackson/core/type/TypeReference;)Ljava/lang/Object;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/data/LivechatAccountPath;->mData:Ljava/lang/Object;

    goto :goto_1

    :cond_2
    iget-object v1, p0, Lcom/zopim/android/sdk/data/LivechatAccountPath;->PARSER:Lcom/zopim/android/sdk/data/Parser;

    invoke-virtual {v1}, Lcom/zopim/android/sdk/data/Parser;->getMapper()Lcom/fasterxml/jackson/databind/ObjectMapper;

    move-result-object v1

    iget-object v2, p0, Lcom/zopim/android/sdk/data/LivechatAccountPath;->mData:Ljava/lang/Object;

    invoke-virtual {v1, v2}, Lcom/fasterxml/jackson/databind/ObjectMapper;->readerForUpdating(Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/ObjectReader;

    move-result-object v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    :try_start_1
    invoke-virtual {v1, p1}, Lcom/fasterxml/jackson/databind/ObjectReader;->readValue(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/zopim/android/sdk/model/Account;

    iput-object p1, p0, Lcom/zopim/android/sdk/data/LivechatAccountPath;->mData:Ljava/lang/Object;
    :try_end_1
    .catch Lcom/fasterxml/jackson/core/JsonProcessingException; {:try_start_1 .. :try_end_1} :catch_1
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_1

    :catch_0
    move-exception p1

    :try_start_2
    sget-object v1, Lcom/zopim/android/sdk/data/LivechatAccountPath;->TAG:Ljava/lang/String;

    const-string v2, "IO error. Account could not be updated."

    :goto_0
    invoke-static {v1, v2, p1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_1

    :catch_1
    move-exception p1

    sget-object v1, Lcom/zopim/android/sdk/data/LivechatAccountPath;->TAG:Ljava/lang/String;

    const-string v2, "Failed to process json. Account could not be updated."

    goto :goto_0

    :goto_1
    invoke-virtual {p0}, Lcom/zopim/android/sdk/data/LivechatAccountPath;->getData()Lcom/zopim/android/sdk/model/Account;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/data/LivechatAccountPath;->broadcast(Ljava/lang/Object;)V

    monitor-exit v0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v0
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw p1
.end method
