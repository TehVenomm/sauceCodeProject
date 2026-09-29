.class Lcom/helpshift/common/AutoRetryFailedEventDM$1;
.super Lcom/helpshift/common/domain/F;
.source "AutoRetryFailedEventDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/common/AutoRetryFailedEventDM;->sendAllEvents()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/common/AutoRetryFailedEventDM;


# direct methods
.method constructor <init>(Lcom/helpshift/common/AutoRetryFailedEventDM;)V
    .locals 0

    .line 80
    iput-object p1, p0, Lcom/helpshift/common/AutoRetryFailedEventDM$1;->this$0:Lcom/helpshift/common/AutoRetryFailedEventDM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 4

    const/4 v0, 0x0

    const/4 v1, 0x1

    .line 84
    :try_start_0
    iget-object v2, p0, Lcom/helpshift/common/AutoRetryFailedEventDM$1;->this$0:Lcom/helpshift/common/AutoRetryFailedEventDM;

    iget-object v3, p0, Lcom/helpshift/common/AutoRetryFailedEventDM$1;->this$0:Lcom/helpshift/common/AutoRetryFailedEventDM;

    invoke-static {v3}, Lcom/helpshift/common/AutoRetryFailedEventDM;->access$000(Lcom/helpshift/common/AutoRetryFailedEventDM;)Ljava/util/Set;

    move-result-object v3

    invoke-virtual {v2, v3}, Lcom/helpshift/common/AutoRetryFailedEventDM;->retryFailedApis(Ljava/util/Set;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 88
    iget-object v2, p0, Lcom/helpshift/common/AutoRetryFailedEventDM$1;->this$0:Lcom/helpshift/common/AutoRetryFailedEventDM;

    invoke-static {v2}, Lcom/helpshift/common/AutoRetryFailedEventDM;->access$100(Lcom/helpshift/common/AutoRetryFailedEventDM;)Ljava/util/concurrent/atomic/AtomicBoolean;

    move-result-object v2

    invoke-virtual {v2, v1, v0}, Ljava/util/concurrent/atomic/AtomicBoolean;->compareAndSet(ZZ)Z

    return-void

    :catchall_0
    move-exception v2

    iget-object v3, p0, Lcom/helpshift/common/AutoRetryFailedEventDM$1;->this$0:Lcom/helpshift/common/AutoRetryFailedEventDM;

    invoke-static {v3}, Lcom/helpshift/common/AutoRetryFailedEventDM;->access$100(Lcom/helpshift/common/AutoRetryFailedEventDM;)Ljava/util/concurrent/atomic/AtomicBoolean;

    move-result-object v3

    invoke-virtual {v3, v1, v0}, Ljava/util/concurrent/atomic/AtomicBoolean;->compareAndSet(ZZ)Z

    .line 89
    throw v2
.end method
