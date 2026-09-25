.class public Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;
.super Lcom/zopim/android/sdk/data/Path;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/zopim/android/sdk/data/Path<",
        "Ljava/util/LinkedHashMap<",
        "Ljava/lang/String;",
        "Lcom/zopim/android/sdk/model/Department;",
        ">;>;"
    }
.end annotation


# static fields
.field private static final INSTANCE:Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;

.field private static final TAG:Ljava/lang/String; = "LivechatDepartmentsPath"


# instance fields
.field private final mLock:Ljava/lang/Object;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    new-instance v0, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;

    invoke-direct {v0}, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;-><init>()V

    sput-object v0, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->INSTANCE:Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;

    return-void
.end method

.method private constructor <init>()V
    .locals 1

    invoke-direct {p0}, Lcom/zopim/android/sdk/data/Path;-><init>()V

    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    iput-object v0, p0, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->mLock:Ljava/lang/Object;

    new-instance v0, Ljava/util/LinkedHashMap;

    invoke-direct {v0}, Ljava/util/LinkedHashMap;-><init>()V

    iput-object v0, p0, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->mData:Ljava/lang/Object;

    return-void
.end method

.method public static declared-synchronized getInstance()Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;
    .locals 2

    const-class v0, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;

    monitor-enter v0

    :try_start_0
    sget-object v1, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->INSTANCE:Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit v0

    return-object v1

    :catchall_0
    move-exception v1

    monitor-exit v0

    throw v1
.end method

.method private updateInternal(Ljava/util/LinkedHashMap;)V
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/LinkedHashMap<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/model/Department;",
            ">;)V"
        }
    .end annotation

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->TAG:Ljava/lang/String;

    const-string v0, "Passed parameter must not be null. Aborting update."

    invoke-static {p1, v0}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->mLock:Ljava/lang/Object;

    monitor-enter v0

    :try_start_0
    invoke-virtual {p1}, Ljava/util/LinkedHashMap;->entrySet()Ljava/util/Set;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_1
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_5

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/Map$Entry;

    invoke-interface {v1}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    invoke-interface {v1}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/zopim/android/sdk/model/Department;

    iget-object v3, p0, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->mData:Ljava/lang/Object;

    check-cast v3, Ljava/util/LinkedHashMap;

    invoke-virtual {v3, v2}, Ljava/util/LinkedHashMap;->containsKey(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_4

    if-nez v1, :cond_2

    iget-object v1, p0, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->mData:Ljava/lang/Object;

    check-cast v1, Ljava/util/LinkedHashMap;

    :goto_1
    invoke-virtual {v1, v2}, Ljava/util/LinkedHashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    :cond_2
    iget-object v3, p0, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->PARSER:Lcom/zopim/android/sdk/data/Parser;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/data/Parser;->getMapper()Lcom/fasterxml/jackson/databind/ObjectMapper;

    move-result-object v3

    invoke-virtual {v3, v1}, Lcom/fasterxml/jackson/databind/ObjectMapper;->valueToTree(Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/JsonNode;

    move-result-object v1

    iget-object v4, p0, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->mData:Ljava/lang/Object;

    check-cast v4, Ljava/util/LinkedHashMap;

    invoke-virtual {v4, v2}, Ljava/util/LinkedHashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lcom/zopim/android/sdk/model/Department;

    if-nez v4, :cond_3

    iget-object v1, p0, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->mData:Ljava/lang/Object;

    check-cast v1, Ljava/util/LinkedHashMap;

    goto :goto_1

    :cond_3
    invoke-virtual {v3, v4}, Lcom/fasterxml/jackson/databind/ObjectMapper;->readerForUpdating(Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/ObjectReader;

    move-result-object v3
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    :try_start_1
    invoke-virtual {v3, v1}, Lcom/fasterxml/jackson/databind/ObjectReader;->readValue(Lcom/fasterxml/jackson/databind/JsonNode;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/zopim/android/sdk/model/Department;

    iget-object v3, p0, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->mData:Ljava/lang/Object;

    check-cast v3, Ljava/util/LinkedHashMap;

    invoke-virtual {v3, v2, v1}, Ljava/util/LinkedHashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_1
    .catch Lcom/fasterxml/jackson/core/JsonProcessingException; {:try_start_1 .. :try_end_1} :catch_1
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_0

    :catch_0
    move-exception v1

    :try_start_2
    sget-object v2, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->TAG:Ljava/lang/String;

    const-string v3, "IO error. Department could not be updated."

    :goto_2
    invoke-static {v2, v3, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    :catch_1
    move-exception v1

    sget-object v2, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->TAG:Ljava/lang/String;

    const-string v3, "Failed to process json. Department could not be updated."
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    goto :goto_2

    :cond_4
    if-eqz v1, :cond_1

    :try_start_3
    iget-object v3, p0, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->mData:Ljava/lang/Object;

    check-cast v3, Ljava/util/LinkedHashMap;

    invoke-virtual {v3, v2, v1}, Ljava/util/LinkedHashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_3
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_2
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    goto :goto_0

    :catch_2
    move-exception v1

    :try_start_4
    sget-object v2, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->TAG:Ljava/lang/String;

    const-string v3, "Failed to process json. Department could not be created."

    goto :goto_2

    :cond_5
    invoke-virtual {p0}, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->getData()Ljava/util/LinkedHashMap;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->broadcast(Ljava/lang/Object;)V

    monitor-exit v0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v0
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_0

    throw p1
.end method


# virtual methods
.method clear()V
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->mData:Ljava/lang/Object;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->mData:Ljava/lang/Object;

    check-cast v0, Ljava/util/LinkedHashMap;

    invoke-virtual {v0}, Ljava/util/LinkedHashMap;->clear()V

    :cond_0
    return-void
.end method

.method public bridge synthetic getData()Ljava/lang/Object;
    .locals 1

    invoke-virtual {p0}, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->getData()Ljava/util/LinkedHashMap;

    move-result-object v0

    return-object v0
.end method

.method public getData()Ljava/util/LinkedHashMap;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/LinkedHashMap<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/model/Department;",
            ">;"
        }
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->mData:Ljava/lang/Object;

    if-eqz v0, :cond_0

    new-instance v0, Ljava/util/LinkedHashMap;

    iget-object v1, p0, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->mData:Ljava/lang/Object;

    check-cast v1, Ljava/util/Map;

    invoke-direct {v0, v1}, Ljava/util/LinkedHashMap;-><init>(Ljava/util/Map;)V

    return-object v0

    :cond_0
    new-instance v0, Ljava/util/LinkedHashMap;

    invoke-direct {v0}, Ljava/util/LinkedHashMap;-><init>()V

    return-object v0
.end method

.method update(Ljava/lang/String;)V
    .locals 2

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->isClearRequired(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->clear()V

    return-void

    :cond_0
    invoke-virtual {p1}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_1

    return-void

    :cond_1
    iget-object v0, p0, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->PARSER:Lcom/zopim/android/sdk/data/Parser;

    new-instance v1, Lcom/zopim/android/sdk/data/e;

    invoke-direct {v1, p0}, Lcom/zopim/android/sdk/data/e;-><init>(Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;)V

    invoke-virtual {v0, p1, v1}, Lcom/zopim/android/sdk/data/Parser;->parse(Ljava/lang/String;Lcom/fasterxml/jackson/core/type/TypeReference;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/util/LinkedHashMap;

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->updateInternal(Ljava/util/LinkedHashMap;)V

    return-void
.end method
