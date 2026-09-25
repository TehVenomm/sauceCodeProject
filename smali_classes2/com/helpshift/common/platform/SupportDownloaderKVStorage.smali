.class public Lcom/helpshift/common/platform/SupportDownloaderKVStorage;
.super Ljava/lang/Object;
.source "SupportDownloaderKVStorage.java"

# interfaces
.implements Lcom/helpshift/android/commons/downloader/contracts/DownloaderKeyValueStorage;


# instance fields
.field private final kvStore:Lcom/helpshift/common/platform/KVStore;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/KVStore;)V
    .locals 0

    .line 11
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 12
    iput-object p1, p0, Lcom/helpshift/common/platform/SupportDownloaderKVStorage;->kvStore:Lcom/helpshift/common/platform/KVStore;

    return-void
.end method


# virtual methods
.method public get(Ljava/lang/String;)Ljava/lang/Object;
    .locals 1

    .line 23
    iget-object v0, p0, Lcom/helpshift/common/platform/SupportDownloaderKVStorage;->kvStore:Lcom/helpshift/common/platform/KVStore;

    invoke-interface {v0, p1}, Lcom/helpshift/common/platform/KVStore;->getSerializable(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method public set(Ljava/lang/String;Ljava/io/Serializable;)Z
    .locals 1

    .line 17
    iget-object v0, p0, Lcom/helpshift/common/platform/SupportDownloaderKVStorage;->kvStore:Lcom/helpshift/common/platform/KVStore;

    invoke-interface {v0, p1, p2}, Lcom/helpshift/common/platform/KVStore;->setSerializable(Ljava/lang/String;Ljava/io/Serializable;)V

    const/4 p1, 0x1

    return p1
.end method
