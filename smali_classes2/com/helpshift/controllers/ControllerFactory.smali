.class public Lcom/helpshift/controllers/ControllerFactory;
.super Ljava/lang/Object;
.source "ControllerFactory.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/controllers/ControllerFactory$LazyHolder;
    }
.end annotation


# instance fields
.field public final dataSyncCoordinator:Lcom/helpshift/controllers/DataSyncCoordinator;

.field public final syncController:Lcom/helpshift/controllers/SyncController;


# direct methods
.method constructor <init>()V
    .locals 4

    .line 16
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 17
    invoke-static {}, Lcom/helpshift/storage/StorageFactory;->getInstance()Lcom/helpshift/storage/StorageFactory;

    move-result-object v0

    iget-object v0, v0, Lcom/helpshift/storage/StorageFactory;->keyValueStorage:Lcom/helpshift/storage/KeyValueStorage;

    .line 18
    new-instance v1, Lcom/helpshift/controllers/SyncController;

    new-instance v2, Lcom/helpshift/util/TimeUtil;

    invoke-direct {v2}, Lcom/helpshift/util/TimeUtil;-><init>()V

    const/4 v3, 0x0

    new-array v3, v3, [Lcom/helpshift/specifications/SyncSpecification;

    invoke-direct {v1, v0, v2, v3}, Lcom/helpshift/controllers/SyncController;-><init>(Lcom/helpshift/storage/KeyValueStorage;Lcom/helpshift/util/TimeUtil;[Lcom/helpshift/specifications/SyncSpecification;)V

    iput-object v1, p0, Lcom/helpshift/controllers/ControllerFactory;->syncController:Lcom/helpshift/controllers/SyncController;

    .line 19
    new-instance v1, Lcom/helpshift/controllers/DataSyncCoordinatorImpl;

    iget-object v2, p0, Lcom/helpshift/controllers/ControllerFactory;->syncController:Lcom/helpshift/controllers/SyncController;

    invoke-direct {v1, v0, v2}, Lcom/helpshift/controllers/DataSyncCoordinatorImpl;-><init>(Lcom/helpshift/storage/KeyValueStorage;Lcom/helpshift/controllers/DataSyncCompletionListener;)V

    iput-object v1, p0, Lcom/helpshift/controllers/ControllerFactory;->dataSyncCoordinator:Lcom/helpshift/controllers/DataSyncCoordinator;

    return-void
.end method

.method public static getInstance()Lcom/helpshift/controllers/ControllerFactory;
    .locals 1

    .line 23
    sget-object v0, Lcom/helpshift/controllers/ControllerFactory$LazyHolder;->INSTANCE:Lcom/helpshift/controllers/ControllerFactory;

    return-object v0
.end method
