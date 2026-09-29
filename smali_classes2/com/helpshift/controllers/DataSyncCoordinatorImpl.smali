.class public Lcom/helpshift/controllers/DataSyncCoordinatorImpl;
.super Ljava/lang/Object;
.source "DataSyncCoordinatorImpl.java"

# interfaces
.implements Lcom/helpshift/controllers/DataSyncCoordinator;


# static fields
.field public static final FIRST_DEVICE_SYNC_COMPLETE_KEY:Ljava/lang/String; = "firstDeviceSyncComplete"


# instance fields
.field private dataSyncCompletionListener:Lcom/helpshift/controllers/DataSyncCompletionListener;

.field private storage:Lcom/helpshift/storage/KeyValueStorage;


# direct methods
.method protected constructor <init>(Lcom/helpshift/storage/KeyValueStorage;)V
    .locals 0

    .line 10
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 11
    iput-object p1, p0, Lcom/helpshift/controllers/DataSyncCoordinatorImpl;->storage:Lcom/helpshift/storage/KeyValueStorage;

    return-void
.end method

.method protected constructor <init>(Lcom/helpshift/storage/KeyValueStorage;Lcom/helpshift/controllers/DataSyncCompletionListener;)V
    .locals 0

    .line 16
    invoke-direct {p0, p1}, Lcom/helpshift/controllers/DataSyncCoordinatorImpl;-><init>(Lcom/helpshift/storage/KeyValueStorage;)V

    .line 17
    iput-object p2, p0, Lcom/helpshift/controllers/DataSyncCoordinatorImpl;->dataSyncCompletionListener:Lcom/helpshift/controllers/DataSyncCompletionListener;

    return-void
.end method

.method private canSyncProperties(Ljava/lang/String;)Z
    .locals 4

    .line 25
    iget-object v0, p0, Lcom/helpshift/controllers/DataSyncCoordinatorImpl;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "firstDeviceSyncComplete"

    invoke-interface {v0, v1}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Boolean;

    invoke-static {v0}, Lcom/helpshift/controllers/DataSyncCoordinatorImpl;->isBoolean(Ljava/lang/Boolean;)Z

    move-result v0

    .line 26
    iget-object v1, p0, Lcom/helpshift/controllers/DataSyncCoordinatorImpl;->storage:Lcom/helpshift/storage/KeyValueStorage;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "switchUserCompleteFor"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-interface {v1, p1}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/Boolean;

    invoke-static {p1}, Lcom/helpshift/controllers/DataSyncCoordinatorImpl;->isBoolean(Ljava/lang/Boolean;)Z

    move-result p1

    if-eqz v0, :cond_0

    if-eqz p1, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return p1
.end method

.method private static isBoolean(Ljava/lang/Boolean;)Z
    .locals 0

    if-eqz p0, :cond_0

    .line 21
    invoke-virtual {p0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p0

    if-eqz p0, :cond_0

    const/4 p0, 0x1

    goto :goto_0

    :cond_0
    const/4 p0, 0x0

    :goto_0
    return p0
.end method


# virtual methods
.method public canSyncSessionProperties(Ljava/lang/String;)Z
    .locals 0

    .line 38
    invoke-direct {p0, p1}, Lcom/helpshift/controllers/DataSyncCoordinatorImpl;->canSyncProperties(Ljava/lang/String;)Z

    move-result p1

    return p1
.end method

.method public canSyncUserProperties(Ljava/lang/String;)Z
    .locals 0

    .line 33
    invoke-direct {p0, p1}, Lcom/helpshift/controllers/DataSyncCoordinatorImpl;->canSyncProperties(Ljava/lang/String;)Z

    move-result p1

    return p1
.end method

.method public firstDeviceSyncComplete()V
    .locals 3

    .line 43
    iget-object v0, p0, Lcom/helpshift/controllers/DataSyncCoordinatorImpl;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "firstDeviceSyncComplete"

    const/4 v2, 0x1

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    .line 44
    iget-object v0, p0, Lcom/helpshift/controllers/DataSyncCoordinatorImpl;->dataSyncCompletionListener:Lcom/helpshift/controllers/DataSyncCompletionListener;

    if-eqz v0, :cond_0

    .line 45
    iget-object v0, p0, Lcom/helpshift/controllers/DataSyncCoordinatorImpl;->dataSyncCompletionListener:Lcom/helpshift/controllers/DataSyncCompletionListener;

    invoke-interface {v0}, Lcom/helpshift/controllers/DataSyncCompletionListener;->firstDeviceSyncComplete()V

    :cond_0
    return-void
.end method

.method public isFirstDeviceSyncComplete()Z
    .locals 2

    .line 51
    iget-object v0, p0, Lcom/helpshift/controllers/DataSyncCoordinatorImpl;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "firstDeviceSyncComplete"

    invoke-interface {v0, v1}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Boolean;

    invoke-static {v0}, Lcom/helpshift/controllers/DataSyncCoordinatorImpl;->isBoolean(Ljava/lang/Boolean;)Z

    move-result v0

    return v0
.end method

.method public switchUserComplete(Ljava/lang/String;)V
    .locals 3

    .line 61
    iget-object v0, p0, Lcom/helpshift/controllers/DataSyncCoordinatorImpl;->storage:Lcom/helpshift/storage/KeyValueStorage;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "switchUserCompleteFor"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x1

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    .line 62
    iget-object v0, p0, Lcom/helpshift/controllers/DataSyncCoordinatorImpl;->dataSyncCompletionListener:Lcom/helpshift/controllers/DataSyncCompletionListener;

    if-eqz v0, :cond_0

    .line 63
    iget-object v0, p0, Lcom/helpshift/controllers/DataSyncCoordinatorImpl;->dataSyncCompletionListener:Lcom/helpshift/controllers/DataSyncCompletionListener;

    invoke-interface {v0, p1}, Lcom/helpshift/controllers/DataSyncCompletionListener;->switchUserComplete(Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public switchUserPending(Ljava/lang/String;)V
    .locals 3

    .line 56
    iget-object v0, p0, Lcom/helpshift/controllers/DataSyncCoordinatorImpl;->storage:Lcom/helpshift/storage/KeyValueStorage;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "switchUserCompleteFor"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    const/4 v1, 0x0

    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    invoke-interface {v0, p1, v1}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    return-void
.end method
