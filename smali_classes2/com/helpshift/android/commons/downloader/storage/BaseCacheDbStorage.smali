.class public abstract Lcom/helpshift/android/commons/downloader/storage/BaseCacheDbStorage;
.super Ljava/lang/Object;
.source "BaseCacheDbStorage.java"


# instance fields
.field private keyValueStorage:Lcom/helpshift/android/commons/downloader/contracts/DownloaderKeyValueStorage;


# direct methods
.method public constructor <init>(Lcom/helpshift/android/commons/downloader/contracts/DownloaderKeyValueStorage;)V
    .locals 0

    .line 11
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 12
    iput-object p1, p0, Lcom/helpshift/android/commons/downloader/storage/BaseCacheDbStorage;->keyValueStorage:Lcom/helpshift/android/commons/downloader/contracts/DownloaderKeyValueStorage;

    return-void
.end method


# virtual methods
.method public getFilePath(Ljava/lang/String;)Ljava/lang/String;
    .locals 2

    .line 17
    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/storage/BaseCacheDbStorage;->keyValueStorage:Lcom/helpshift/android/commons/downloader/contracts/DownloaderKeyValueStorage;

    invoke-virtual {p0}, Lcom/helpshift/android/commons/downloader/storage/BaseCacheDbStorage;->getStorageKey()Ljava/lang/String;

    move-result-object v1

    invoke-interface {v0, v1}, Lcom/helpshift/android/commons/downloader/contracts/DownloaderKeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/HashMap;

    if-nez v0, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 23
    :cond_0
    invoke-virtual {v0, p1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/String;

    return-object p1
.end method

.method abstract getStorageKey()Ljava/lang/String;
.end method

.method public insertFilePath(Ljava/lang/String;Ljava/lang/String;)V
    .locals 2

    .line 28
    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/storage/BaseCacheDbStorage;->keyValueStorage:Lcom/helpshift/android/commons/downloader/contracts/DownloaderKeyValueStorage;

    invoke-virtual {p0}, Lcom/helpshift/android/commons/downloader/storage/BaseCacheDbStorage;->getStorageKey()Ljava/lang/String;

    move-result-object v1

    invoke-interface {v0, v1}, Lcom/helpshift/android/commons/downloader/contracts/DownloaderKeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/HashMap;

    if-nez v0, :cond_0

    .line 30
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 32
    :cond_0
    invoke-virtual {v0, p1, p2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 33
    iget-object p1, p0, Lcom/helpshift/android/commons/downloader/storage/BaseCacheDbStorage;->keyValueStorage:Lcom/helpshift/android/commons/downloader/contracts/DownloaderKeyValueStorage;

    invoke-virtual {p0}, Lcom/helpshift/android/commons/downloader/storage/BaseCacheDbStorage;->getStorageKey()Ljava/lang/String;

    move-result-object p2

    invoke-interface {p1, p2, v0}, Lcom/helpshift/android/commons/downloader/contracts/DownloaderKeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    return-void
.end method

.method public removeFilePath(Ljava/lang/String;)V
    .locals 2

    .line 38
    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/storage/BaseCacheDbStorage;->keyValueStorage:Lcom/helpshift/android/commons/downloader/contracts/DownloaderKeyValueStorage;

    invoke-virtual {p0}, Lcom/helpshift/android/commons/downloader/storage/BaseCacheDbStorage;->getStorageKey()Ljava/lang/String;

    move-result-object v1

    invoke-interface {v0, v1}, Lcom/helpshift/android/commons/downloader/contracts/DownloaderKeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/HashMap;

    if-eqz v0, :cond_0

    .line 41
    invoke-virtual {v0, p1}, Ljava/util/HashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 42
    iget-object p1, p0, Lcom/helpshift/android/commons/downloader/storage/BaseCacheDbStorage;->keyValueStorage:Lcom/helpshift/android/commons/downloader/contracts/DownloaderKeyValueStorage;

    invoke-virtual {p0}, Lcom/helpshift/android/commons/downloader/storage/BaseCacheDbStorage;->getStorageKey()Ljava/lang/String;

    move-result-object v1

    invoke-interface {p1, v1, v0}, Lcom/helpshift/android/commons/downloader/contracts/DownloaderKeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    :cond_0
    return-void
.end method
