.class public abstract Lcom/helpshift/storage/BaseRetryKeyValueStorage;
.super Ljava/lang/Object;
.source "BaseRetryKeyValueStorage.java"

# interfaces
.implements Lcom/helpshift/storage/KeyValueStorage;


# static fields
.field private static final MAX_RETRY_COUNT:I = 0x1

.field protected static final TAG:Ljava/lang/String; = "Helpshift_RetryKeyValue"


# instance fields
.field protected keyValueStorage:Lcom/helpshift/storage/KeyValueStorage;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 11
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public declared-synchronized get(Ljava/lang/String;)Ljava/lang/Object;
    .locals 6

    monitor-enter p0

    const/4 v0, 0x0

    const/4 v1, 0x0

    .line 67
    :cond_0
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/storage/BaseRetryKeyValueStorage;->keyValueStorage:Lcom/helpshift/storage/KeyValueStorage;

    invoke-interface {v2, p1}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v2
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v2

    :catchall_0
    move-exception p1

    goto :goto_1

    :catch_0
    move-exception v2

    if-nez v1, :cond_1

    :try_start_1
    const-string v3, "Helpshift_RetryKeyValue"

    .line 71
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "Exception getting value for : "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, ", retry count : "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-static {v3, v4, v2}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_1
    const-string v3, "Helpshift_RetryKeyValue"

    .line 74
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "Exception getting value for : "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, ", retry count : "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    new-array v5, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    invoke-static {v3, v4, v2, v5}, Lcom/helpshift/util/HSLogger;->f(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    .line 76
    :goto_0
    invoke-virtual {p0}, Lcom/helpshift/storage/BaseRetryKeyValueStorage;->reInitiateDbInstance()V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    add-int/lit8 v1, v1, 0x1

    const/4 v2, 0x1

    if-le v1, v2, :cond_0

    const/4 p1, 0x0

    .line 82
    monitor-exit p0

    return-object p1

    .line 63
    :goto_1
    monitor-exit p0

    throw p1
.end method

.method protected abstract reInitiateDbInstance()V
.end method

.method public declared-synchronized removeAllKeys()V
    .locals 6

    monitor-enter p0

    const/4 v0, 0x0

    const/4 v1, 0x0

    .line 111
    :cond_0
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/storage/BaseRetryKeyValueStorage;->keyValueStorage:Lcom/helpshift/storage/KeyValueStorage;

    invoke-interface {v2}, Lcom/helpshift/storage/KeyValueStorage;->removeAllKeys()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 112
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    goto :goto_1

    :catch_0
    move-exception v2

    if-nez v1, :cond_1

    :try_start_1
    const-string v3, "Helpshift_RetryKeyValue"

    .line 116
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "Exception removing all keys, retry count : "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-static {v3, v4, v2}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_1
    const-string v3, "Helpshift_RetryKeyValue"

    .line 119
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "Exception removing all keys, retry count : "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    new-array v5, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    invoke-static {v3, v4, v2, v5}, Lcom/helpshift/util/HSLogger;->f(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    .line 121
    :goto_0
    invoke-virtual {p0}, Lcom/helpshift/storage/BaseRetryKeyValueStorage;->reInitiateDbInstance()V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    add-int/lit8 v1, v1, 0x1

    const/4 v2, 0x1

    if-le v1, v2, :cond_0

    .line 125
    monitor-exit p0

    return-void

    .line 107
    :goto_1
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized removeKey(Ljava/lang/String;)V
    .locals 6

    monitor-enter p0

    const/4 v0, 0x0

    const/4 v1, 0x0

    .line 90
    :cond_0
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/storage/BaseRetryKeyValueStorage;->keyValueStorage:Lcom/helpshift/storage/KeyValueStorage;

    invoke-interface {v2, p1}, Lcom/helpshift/storage/KeyValueStorage;->removeKey(Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 91
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    goto :goto_1

    :catch_0
    move-exception v2

    if-nez v1, :cond_1

    :try_start_1
    const-string v3, "Helpshift_RetryKeyValue"

    .line 95
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "Exception removing key : "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, ", retry count : "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-static {v3, v4, v2}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_1
    const-string v3, "Helpshift_RetryKeyValue"

    .line 98
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "Exception removing key : "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, ", retry count : "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    new-array v5, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    invoke-static {v3, v4, v2, v5}, Lcom/helpshift/util/HSLogger;->f(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    .line 100
    :goto_0
    invoke-virtual {p0}, Lcom/helpshift/storage/BaseRetryKeyValueStorage;->reInitiateDbInstance()V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    add-int/lit8 v1, v1, 0x1

    const/4 v2, 0x1

    if-le v1, v2, :cond_0

    .line 104
    monitor-exit p0

    return-void

    .line 86
    :goto_1
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized set(Ljava/lang/String;Ljava/io/Serializable;)Z
    .locals 6

    monitor-enter p0

    const/4 v0, 0x0

    const/4 v1, 0x0

    .line 21
    :cond_0
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/storage/BaseRetryKeyValueStorage;->keyValueStorage:Lcom/helpshift/storage/KeyValueStorage;

    invoke-interface {v2, p1, p2}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    move-result v2
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return v2

    :catchall_0
    move-exception p1

    goto :goto_1

    :catch_0
    move-exception v2

    if-nez v1, :cond_1

    :try_start_1
    const-string v3, "Helpshift_RetryKeyValue"

    .line 25
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "Exception in setting value for key : "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, ", retry count : "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-static {v3, v4, v2}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_1
    const-string v3, "Helpshift_RetryKeyValue"

    .line 28
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "Exception in setting value for key : "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, ", retry count : "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    new-array v5, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    invoke-static {v3, v4, v2, v5}, Lcom/helpshift/util/HSLogger;->f(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    .line 30
    :goto_0
    invoke-virtual {p0}, Lcom/helpshift/storage/BaseRetryKeyValueStorage;->reInitiateDbInstance()V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    add-int/lit8 v1, v1, 0x1

    const/4 v2, 0x1

    if-le v1, v2, :cond_0

    .line 36
    monitor-exit p0

    return v0

    .line 17
    :goto_1
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized setKeyValues(Ljava/util/Map;)Z
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/io/Serializable;",
            ">;)Z"
        }
    .end annotation

    monitor-enter p0

    const/4 v0, 0x0

    const/4 v1, 0x0

    .line 44
    :cond_0
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/storage/BaseRetryKeyValueStorage;->keyValueStorage:Lcom/helpshift/storage/KeyValueStorage;

    invoke-interface {v2, p1}, Lcom/helpshift/storage/KeyValueStorage;->setKeyValues(Ljava/util/Map;)Z

    move-result v2
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return v2

    :catchall_0
    move-exception p1

    goto :goto_1

    :catch_0
    move-exception v2

    if-nez v1, :cond_1

    :try_start_1
    const-string v3, "Helpshift_RetryKeyValue"

    .line 48
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "Exception in bulk insert, retry count : "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-static {v3, v4, v2}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_1
    const-string v3, "Helpshift_RetryKeyValue"

    .line 51
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "Exception in bulk insert, retry count : "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    new-array v5, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    invoke-static {v3, v4, v2, v5}, Lcom/helpshift/util/HSLogger;->f(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    .line 53
    :goto_0
    invoke-virtual {p0}, Lcom/helpshift/storage/BaseRetryKeyValueStorage;->reInitiateDbInstance()V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    add-int/lit8 v1, v1, 0x1

    const/4 v2, 0x1

    if-le v1, v2, :cond_0

    .line 59
    monitor-exit p0

    return v0

    .line 40
    :goto_1
    monitor-exit p0

    throw p1
.end method
