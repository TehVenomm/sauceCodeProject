.class Lcom/helpshift/common/AutoRetryFailedEventDM$3;
.super Lcom/helpshift/common/domain/F;
.source "AutoRetryFailedEventDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/common/AutoRetryFailedEventDM;->scheduleSync(ILjava/util/Set;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/common/AutoRetryFailedEventDM;

.field final synthetic val$eventTypes:Ljava/util/Set;


# direct methods
.method constructor <init>(Lcom/helpshift/common/AutoRetryFailedEventDM;Ljava/util/Set;)V
    .locals 0

    .line 113
    iput-object p1, p0, Lcom/helpshift/common/AutoRetryFailedEventDM$3;->this$0:Lcom/helpshift/common/AutoRetryFailedEventDM;

    iput-object p2, p0, Lcom/helpshift/common/AutoRetryFailedEventDM$3;->val$eventTypes:Ljava/util/Set;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 3

    .line 116
    iget-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM$3;->this$0:Lcom/helpshift/common/AutoRetryFailedEventDM;

    invoke-static {v0}, Lcom/helpshift/common/AutoRetryFailedEventDM;->access$200(Lcom/helpshift/common/AutoRetryFailedEventDM;)Ljava/util/concurrent/atomic/AtomicBoolean;

    move-result-object v0

    const/4 v1, 0x1

    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Ljava/util/concurrent/atomic/AtomicBoolean;->compareAndSet(ZZ)Z

    .line 117
    iget-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM$3;->this$0:Lcom/helpshift/common/AutoRetryFailedEventDM;

    iget-object v1, p0, Lcom/helpshift/common/AutoRetryFailedEventDM$3;->val$eventTypes:Ljava/util/Set;

    invoke-virtual {v0, v1}, Lcom/helpshift/common/AutoRetryFailedEventDM;->retryFailedApis(Ljava/util/Set;)V

    return-void
.end method
