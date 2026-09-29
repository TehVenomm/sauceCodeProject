.class Lcom/helpshift/controllers/SyncController$1;
.super Ljava/lang/Object;
.source "SyncController.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/controllers/SyncController;->getBatcherJob()Ljava/lang/Runnable;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/controllers/SyncController;


# direct methods
.method constructor <init>(Lcom/helpshift/controllers/SyncController;)V
    .locals 0

    .line 72
    iput-object p1, p0, Lcom/helpshift/controllers/SyncController$1;->this$0:Lcom/helpshift/controllers/SyncController;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 4

    .line 75
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController$1;->this$0:Lcom/helpshift/controllers/SyncController;

    iget-object v0, v0, Lcom/helpshift/controllers/SyncController;->dataTypesWithChangedData:Ljava/util/Set;

    if-eqz v0, :cond_0

    .line 76
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController$1;->this$0:Lcom/helpshift/controllers/SyncController;

    const/4 v1, 0x0

    iget-object v2, p0, Lcom/helpshift/controllers/SyncController$1;->this$0:Lcom/helpshift/controllers/SyncController;

    iget-object v2, v2, Lcom/helpshift/controllers/SyncController;->dataTypesWithChangedData:Ljava/util/Set;

    iget-object v3, p0, Lcom/helpshift/controllers/SyncController$1;->this$0:Lcom/helpshift/controllers/SyncController;

    iget-object v3, v3, Lcom/helpshift/controllers/SyncController;->dataTypesWithChangedData:Ljava/util/Set;

    invoke-interface {v3}, Ljava/util/Set;->size()I

    move-result v3

    new-array v3, v3, [Ljava/lang/String;

    invoke-interface {v2, v3}, Ljava/util/Set;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object v2

    check-cast v2, [Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Lcom/helpshift/controllers/SyncController;->triggerSync(Z[Ljava/lang/String;)V

    .line 78
    :cond_0
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController$1;->this$0:Lcom/helpshift/controllers/SyncController;

    invoke-virtual {v0}, Lcom/helpshift/controllers/SyncController;->cleanUpBatcherJob()V

    return-void
.end method
