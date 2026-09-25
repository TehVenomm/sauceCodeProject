.class public Lcom/helpshift/storage/CachedKeyValueStorage;
.super Ljava/lang/Object;
.source "CachedKeyValueStorage.java"

# interfaces
.implements Lcom/helpshift/storage/KeyValueStorage;


# instance fields
.field private cacheMap:Ljava/util/HashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation
.end field

.field private cacheWhitelistKeys:Ljava/util/Set;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private storage:Lcom/helpshift/storage/KeyValueStorage;


# direct methods
.method public constructor <init>(Lcom/helpshift/storage/KeyValueStorage;Ljava/util/Set;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/storage/KeyValueStorage;",
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 26
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 27
    iput-object p1, p0, Lcom/helpshift/storage/CachedKeyValueStorage;->storage:Lcom/helpshift/storage/KeyValueStorage;

    .line 28
    new-instance p1, Ljava/util/HashSet;

    invoke-direct {p1, p2}, Ljava/util/HashSet;-><init>(Ljava/util/Collection;)V

    iput-object p1, p0, Lcom/helpshift/storage/CachedKeyValueStorage;->cacheWhitelistKeys:Ljava/util/Set;

    .line 29
    new-instance p1, Ljava/util/HashMap;

    invoke-direct {p1}, Ljava/util/HashMap;-><init>()V

    iput-object p1, p0, Lcom/helpshift/storage/CachedKeyValueStorage;->cacheMap:Ljava/util/HashMap;

    return-void
.end method

.method private removeFromCache(Ljava/util/Set;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    if-eqz p1, :cond_0

    .line 127
    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    .line 128
    iget-object v1, p0, Lcom/helpshift/storage/CachedKeyValueStorage;->cacheMap:Ljava/util/HashMap;

    invoke-virtual {v1, v0}, Ljava/util/HashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    :cond_0
    return-void
.end method

.method private setInCache(Ljava/lang/String;Ljava/lang/Object;)V
    .locals 1

    .line 110
    iget-object v0, p0, Lcom/helpshift/storage/CachedKeyValueStorage;->cacheWhitelistKeys:Ljava/util/Set;

    invoke-interface {v0, p1}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 111
    iget-object v0, p0, Lcom/helpshift/storage/CachedKeyValueStorage;->cacheMap:Ljava/util/HashMap;

    invoke-virtual {v0, p1, p2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_0
    return-void
.end method

.method private setInCache(Ljava/util/Map;)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/io/Serializable;",
            ">;)V"
        }
    .end annotation

    if-eqz p1, :cond_1

    .line 117
    invoke-interface {p1}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_0
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/Map$Entry;

    .line 118
    iget-object v1, p0, Lcom/helpshift/storage/CachedKeyValueStorage;->cacheWhitelistKeys:Ljava/util/Set;

    invoke-interface {v0}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v2

    invoke-interface {v1, v2}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 119
    iget-object v1, p0, Lcom/helpshift/storage/CachedKeyValueStorage;->cacheMap:Ljava/util/HashMap;

    invoke-interface {v0}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v2

    invoke-interface {v0}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v0

    invoke-virtual {v1, v2, v0}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    :cond_1
    return-void
.end method


# virtual methods
.method public declared-synchronized get(Ljava/lang/String;)Ljava/lang/Object;
    .locals 1

    monitor-enter p0

    .line 78
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/storage/CachedKeyValueStorage;->cacheMap:Ljava/util/HashMap;

    invoke-virtual {v0, p1}, Ljava/util/HashMap;->containsKey(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 80
    iget-object v0, p0, Lcom/helpshift/storage/CachedKeyValueStorage;->cacheMap:Ljava/util/HashMap;

    invoke-virtual {v0, p1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object p1

    .line 84
    :cond_0
    :try_start_1
    iget-object v0, p0, Lcom/helpshift/storage/CachedKeyValueStorage;->storage:Lcom/helpshift/storage/KeyValueStorage;

    invoke-interface {v0, p1}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    .line 85
    invoke-direct {p0, p1, v0}, Lcom/helpshift/storage/CachedKeyValueStorage;->setInCache(Ljava/lang/String;Ljava/lang/Object;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 86
    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception p1

    .line 77
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized removeAllKeys()V
    .locals 1

    monitor-enter p0

    .line 105
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/storage/CachedKeyValueStorage;->storage:Lcom/helpshift/storage/KeyValueStorage;

    invoke-interface {v0}, Lcom/helpshift/storage/KeyValueStorage;->removeAllKeys()V

    .line 106
    iget-object v0, p0, Lcom/helpshift/storage/CachedKeyValueStorage;->cacheMap:Ljava/util/HashMap;

    invoke-virtual {v0}, Ljava/util/HashMap;->clear()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 107
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    .line 104
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized removeKey(Ljava/lang/String;)V
    .locals 1

    monitor-enter p0

    .line 96
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/storage/CachedKeyValueStorage;->storage:Lcom/helpshift/storage/KeyValueStorage;

    invoke-interface {v0, p1}, Lcom/helpshift/storage/KeyValueStorage;->removeKey(Ljava/lang/String;)V

    .line 97
    iget-object v0, p0, Lcom/helpshift/storage/CachedKeyValueStorage;->cacheMap:Ljava/util/HashMap;

    invoke-virtual {v0, p1}, Ljava/util/HashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 98
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 95
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized set(Ljava/lang/String;Ljava/io/Serializable;)Z
    .locals 1

    monitor-enter p0

    .line 42
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/storage/CachedKeyValueStorage;->cacheMap:Ljava/util/HashMap;

    invoke-virtual {v0, p1}, Ljava/util/HashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 43
    iget-object v0, p0, Lcom/helpshift/storage/CachedKeyValueStorage;->storage:Lcom/helpshift/storage/KeyValueStorage;

    invoke-interface {v0, p1, p2}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 45
    invoke-direct {p0, p1, p2}, Lcom/helpshift/storage/CachedKeyValueStorage;->setInCache(Ljava/lang/String;Ljava/lang/Object;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 47
    :cond_0
    monitor-exit p0

    return v0

    :catchall_0
    move-exception p1

    .line 41
    monitor-exit p0

    throw p1
.end method

.method public setKeyValues(Ljava/util/Map;)Z
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/io/Serializable;",
            ">;)Z"
        }
    .end annotation

    .line 59
    invoke-interface {p1}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object v0

    invoke-direct {p0, v0}, Lcom/helpshift/storage/CachedKeyValueStorage;->removeFromCache(Ljava/util/Set;)V

    .line 60
    iget-object v0, p0, Lcom/helpshift/storage/CachedKeyValueStorage;->storage:Lcom/helpshift/storage/KeyValueStorage;

    invoke-interface {v0, p1}, Lcom/helpshift/storage/KeyValueStorage;->setKeyValues(Ljava/util/Map;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 63
    invoke-direct {p0, p1}, Lcom/helpshift/storage/CachedKeyValueStorage;->setInCache(Ljava/util/Map;)V

    :cond_0
    return v0
.end method
