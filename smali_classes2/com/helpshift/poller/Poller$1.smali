.class Lcom/helpshift/poller/Poller$1;
.super Ljava/lang/Object;
.source "Poller.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/poller/Poller;->runDelayed(JLjava/util/concurrent/TimeUnit;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/poller/Poller;

.field final synthetic val$derivedInterval:Lcom/helpshift/common/poller/Delay;


# direct methods
.method constructor <init>(Lcom/helpshift/poller/Poller;Lcom/helpshift/common/poller/Delay;)V
    .locals 0

    .line 89
    iput-object p1, p0, Lcom/helpshift/poller/Poller$1;->this$0:Lcom/helpshift/poller/Poller;

    iput-object p2, p0, Lcom/helpshift/poller/Poller$1;->val$derivedInterval:Lcom/helpshift/common/poller/Delay;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 4

    .line 93
    iget-object v0, p0, Lcom/helpshift/poller/Poller$1;->this$0:Lcom/helpshift/poller/Poller;

    iget-object v1, p0, Lcom/helpshift/poller/Poller$1;->val$derivedInterval:Lcom/helpshift/common/poller/Delay;

    iget-wide v1, v1, Lcom/helpshift/common/poller/Delay;->delay:J

    iget-object v3, p0, Lcom/helpshift/poller/Poller$1;->val$derivedInterval:Lcom/helpshift/common/poller/Delay;

    iget-object v3, v3, Lcom/helpshift/common/poller/Delay;->timeUnit:Ljava/util/concurrent/TimeUnit;

    invoke-virtual {v0, v1, v2, v3}, Lcom/helpshift/poller/Poller;->runDelayed(JLjava/util/concurrent/TimeUnit;)V

    return-void
.end method
