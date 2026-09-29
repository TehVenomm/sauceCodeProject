.class Lcom/helpshift/common/domain/BackgroundThreader$1;
.super Lcom/helpshift/common/domain/F;
.source "BackgroundThreader.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/common/domain/BackgroundThreader;->thread(Lcom/helpshift/common/domain/F;)Lcom/helpshift/common/domain/F;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/common/domain/BackgroundThreader;

.field final synthetic val$f:Lcom/helpshift/common/domain/F;


# direct methods
.method constructor <init>(Lcom/helpshift/common/domain/BackgroundThreader;Lcom/helpshift/common/domain/F;)V
    .locals 0

    .line 32
    iput-object p1, p0, Lcom/helpshift/common/domain/BackgroundThreader$1;->this$0:Lcom/helpshift/common/domain/BackgroundThreader;

    iput-object p2, p0, Lcom/helpshift/common/domain/BackgroundThreader$1;->val$f:Lcom/helpshift/common/domain/F;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 3

    .line 35
    iget-object v0, p0, Lcom/helpshift/common/domain/BackgroundThreader$1;->val$f:Lcom/helpshift/common/domain/F;

    new-instance v1, Ljava/lang/Throwable;

    invoke-direct {v1}, Ljava/lang/Throwable;-><init>()V

    iput-object v1, v0, Lcom/helpshift/common/domain/F;->cause:Ljava/lang/Throwable;

    .line 37
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/domain/BackgroundThreader$1;->this$0:Lcom/helpshift/common/domain/BackgroundThreader;

    iget-object v0, v0, Lcom/helpshift/common/domain/BackgroundThreader;->service:Ljava/util/concurrent/ExecutorService;

    new-instance v1, Lcom/helpshift/common/domain/BackgroundThreader$1$1;

    invoke-direct {v1, p0}, Lcom/helpshift/common/domain/BackgroundThreader$1$1;-><init>(Lcom/helpshift/common/domain/BackgroundThreader$1;)V

    invoke-interface {v0, v1}, Ljava/util/concurrent/ExecutorService;->submit(Ljava/lang/Runnable;)Ljava/util/concurrent/Future;
    :try_end_0
    .catch Ljava/util/concurrent/RejectedExecutionException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "Helpshift_CoreBgTh"

    const-string v2, "Rejected execution of task in BackgroundThreader"

    .line 64
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :goto_0
    return-void
.end method
