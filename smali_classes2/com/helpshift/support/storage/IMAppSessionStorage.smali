.class public Lcom/helpshift/support/storage/IMAppSessionStorage;
.super Ljava/lang/Object;
.source "IMAppSessionStorage.java"

# interfaces
.implements Lcom/helpshift/storage/KeyValueStorage;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/storage/IMAppSessionStorage$LazyHolder;
    }
.end annotation


# instance fields
.field private readLock:Ljava/util/concurrent/locks/Lock;

.field private sessionStorage:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/io/Serializable;",
            ">;"
        }
    .end annotation
.end field

.field private writeLock:Ljava/util/concurrent/locks/Lock;


# direct methods
.method constructor <init>()V
    .locals 2

    .line 24
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 25
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lcom/helpshift/support/storage/IMAppSessionStorage;->sessionStorage:Ljava/util/Map;

    .line 26
    new-instance v0, Ljava/util/concurrent/locks/ReentrantReadWriteLock;

    invoke-direct {v0}, Ljava/util/concurrent/locks/ReentrantReadWriteLock;-><init>()V

    .line 27
    invoke-virtual {v0}, Ljava/util/concurrent/locks/ReentrantReadWriteLock;->readLock()Ljava/util/concurrent/locks/ReentrantReadWriteLock$ReadLock;

    move-result-object v1

    iput-object v1, p0, Lcom/helpshift/support/storage/IMAppSessionStorage;->readLock:Ljava/util/concurrent/locks/Lock;

    .line 28
    invoke-virtual {v0}, Ljava/util/concurrent/locks/ReentrantReadWriteLock;->writeLock()Ljava/util/concurrent/locks/ReentrantReadWriteLock$WriteLock;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/storage/IMAppSessionStorage;->writeLock:Ljava/util/concurrent/locks/Lock;

    return-void
.end method

.method public static getInstance()Lcom/helpshift/support/storage/IMAppSessionStorage;
    .locals 1

    .line 32
    sget-object v0, Lcom/helpshift/support/storage/IMAppSessionStorage$LazyHolder;->INSTANCE:Lcom/helpshift/support/storage/IMAppSessionStorage;

    return-object v0
.end method


# virtual methods
.method public get(Ljava/lang/String;)Ljava/io/Serializable;
    .locals 1

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 75
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/storage/IMAppSessionStorage;->readLock:Ljava/util/concurrent/locks/Lock;

    invoke-interface {v0}, Ljava/util/concurrent/locks/Lock;->lock()V

    .line 76
    iget-object v0, p0, Lcom/helpshift/support/storage/IMAppSessionStorage;->sessionStorage:Ljava/util/Map;

    invoke-interface {v0, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/io/Serializable;

    .line 77
    iget-object v0, p0, Lcom/helpshift/support/storage/IMAppSessionStorage;->readLock:Ljava/util/concurrent/locks/Lock;

    invoke-interface {v0}, Ljava/util/concurrent/locks/Lock;->unlock()V

    return-object p1
.end method

.method public bridge synthetic get(Ljava/lang/String;)Ljava/lang/Object;
    .locals 0

    .line 19
    invoke-virtual {p0, p1}, Lcom/helpshift/support/storage/IMAppSessionStorage;->get(Ljava/lang/String;)Ljava/io/Serializable;

    move-result-object p1

    return-object p1
.end method

.method public removeAllKeys()V
    .locals 1

    .line 101
    iget-object v0, p0, Lcom/helpshift/support/storage/IMAppSessionStorage;->writeLock:Ljava/util/concurrent/locks/Lock;

    invoke-interface {v0}, Ljava/util/concurrent/locks/Lock;->lock()V

    .line 102
    iget-object v0, p0, Lcom/helpshift/support/storage/IMAppSessionStorage;->sessionStorage:Ljava/util/Map;

    invoke-interface {v0}, Ljava/util/Map;->clear()V

    .line 103
    iget-object v0, p0, Lcom/helpshift/support/storage/IMAppSessionStorage;->writeLock:Ljava/util/concurrent/locks/Lock;

    invoke-interface {v0}, Ljava/util/concurrent/locks/Lock;->unlock()V

    return-void
.end method

.method public removeKey(Ljava/lang/String;)V
    .locals 1

    if-nez p1, :cond_0

    return-void

    .line 91
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/storage/IMAppSessionStorage;->writeLock:Ljava/util/concurrent/locks/Lock;

    invoke-interface {v0}, Ljava/util/concurrent/locks/Lock;->lock()V

    .line 92
    iget-object v0, p0, Lcom/helpshift/support/storage/IMAppSessionStorage;->sessionStorage:Ljava/util/Map;

    invoke-interface {v0, p1}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 93
    iget-object p1, p0, Lcom/helpshift/support/storage/IMAppSessionStorage;->writeLock:Ljava/util/concurrent/locks/Lock;

    invoke-interface {p1}, Ljava/util/concurrent/locks/Lock;->unlock()V

    return-void
.end method

.method public set(Ljava/lang/String;Ljava/io/Serializable;)Z
    .locals 1

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return p1

    .line 46
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/storage/IMAppSessionStorage;->writeLock:Ljava/util/concurrent/locks/Lock;

    invoke-interface {v0}, Ljava/util/concurrent/locks/Lock;->lock()V

    .line 47
    iget-object v0, p0, Lcom/helpshift/support/storage/IMAppSessionStorage;->sessionStorage:Ljava/util/Map;

    invoke-interface {v0, p1, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 48
    iget-object p1, p0, Lcom/helpshift/support/storage/IMAppSessionStorage;->writeLock:Ljava/util/concurrent/locks/Lock;

    invoke-interface {p1}, Ljava/util/concurrent/locks/Lock;->unlock()V

    const/4 p1, 0x1

    return p1
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

    if-eqz p1, :cond_1

    .line 54
    invoke-interface {p1}, Ljava/util/Map;->size()I

    move-result v0

    if-nez v0, :cond_0

    goto :goto_0

    .line 57
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/storage/IMAppSessionStorage;->writeLock:Ljava/util/concurrent/locks/Lock;

    invoke-interface {v0}, Ljava/util/concurrent/locks/Lock;->lock()V

    .line 58
    iget-object v0, p0, Lcom/helpshift/support/storage/IMAppSessionStorage;->sessionStorage:Ljava/util/Map;

    invoke-interface {v0, p1}, Ljava/util/Map;->putAll(Ljava/util/Map;)V

    .line 59
    iget-object p1, p0, Lcom/helpshift/support/storage/IMAppSessionStorage;->writeLock:Ljava/util/concurrent/locks/Lock;

    invoke-interface {p1}, Ljava/util/concurrent/locks/Lock;->unlock()V

    const/4 p1, 0x1

    return p1

    :cond_1
    :goto_0
    const/4 p1, 0x0

    return p1
.end method
