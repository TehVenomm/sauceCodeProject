.class Lcom/helpshift/common/domain/BackgroundDelayedThreader$1;
.super Lcom/helpshift/common/domain/F;
.source "BackgroundDelayedThreader.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/common/domain/BackgroundDelayedThreader;->thread(Lcom/helpshift/common/domain/F;J)Lcom/helpshift/common/domain/F;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/common/domain/BackgroundDelayedThreader;

.field final synthetic val$delayInMillis:J

.field final synthetic val$f:Lcom/helpshift/common/domain/F;


# direct methods
.method constructor <init>(Lcom/helpshift/common/domain/BackgroundDelayedThreader;Lcom/helpshift/common/domain/F;J)V
    .locals 0

    .line 24
    iput-object p1, p0, Lcom/helpshift/common/domain/BackgroundDelayedThreader$1;->this$0:Lcom/helpshift/common/domain/BackgroundDelayedThreader;

    iput-object p2, p0, Lcom/helpshift/common/domain/BackgroundDelayedThreader$1;->val$f:Lcom/helpshift/common/domain/F;

    iput-wide p3, p0, Lcom/helpshift/common/domain/BackgroundDelayedThreader$1;->val$delayInMillis:J

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 5

    .line 27
    iget-object v0, p0, Lcom/helpshift/common/domain/BackgroundDelayedThreader$1;->val$f:Lcom/helpshift/common/domain/F;

    new-instance v1, Ljava/lang/Throwable;

    invoke-direct {v1}, Ljava/lang/Throwable;-><init>()V

    iput-object v1, v0, Lcom/helpshift/common/domain/F;->cause:Ljava/lang/Throwable;

    .line 29
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/domain/BackgroundDelayedThreader$1;->this$0:Lcom/helpshift/common/domain/BackgroundDelayedThreader;

    iget-object v0, v0, Lcom/helpshift/common/domain/BackgroundDelayedThreader;->service:Ljava/util/concurrent/ScheduledExecutorService;

    new-instance v1, Lcom/helpshift/common/domain/BackgroundDelayedThreader$1$1;

    invoke-direct {v1, p0}, Lcom/helpshift/common/domain/BackgroundDelayedThreader$1$1;-><init>(Lcom/helpshift/common/domain/BackgroundDelayedThreader$1;)V

    iget-wide v2, p0, Lcom/helpshift/common/domain/BackgroundDelayedThreader$1;->val$delayInMillis:J

    sget-object v4, Ljava/util/concurrent/TimeUnit;->MILLISECONDS:Ljava/util/concurrent/TimeUnit;

    invoke-interface {v0, v1, v2, v3, v4}, Ljava/util/concurrent/ScheduledExecutorService;->schedule(Ljava/lang/Runnable;JLjava/util/concurrent/TimeUnit;)Ljava/util/concurrent/ScheduledFuture;
    :try_end_0
    .catch Ljava/util/concurrent/RejectedExecutionException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "Helpshift_CoreDelayTh"

    const-string v2, "Rejected execution of task in BackgroundDelayedThreader"

    .line 56
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :goto_0
    return-void
.end method
