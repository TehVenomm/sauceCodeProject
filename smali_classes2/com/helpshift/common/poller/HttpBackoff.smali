.class public Lcom/helpshift/common/poller/HttpBackoff;
.super Ljava/lang/Object;
.source "HttpBackoff.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/common/poller/HttpBackoff$Builder;,
        Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;
    }
.end annotation


# instance fields
.field private final exponentialBackoff:Lcom/helpshift/common/poller/ExponentialBackoff;

.field private final retryPolicy:Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;


# direct methods
.method constructor <init>(Lcom/helpshift/common/poller/HttpBackoff$Builder;)V
    .locals 2

    .line 15
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 16
    new-instance v0, Lcom/helpshift/common/poller/ExponentialBackoff;

    iget-object v1, p1, Lcom/helpshift/common/poller/HttpBackoff$Builder;->exponentialBackoffBuilder:Lcom/helpshift/common/poller/ExponentialBackoff$Builder;

    invoke-direct {v0, v1}, Lcom/helpshift/common/poller/ExponentialBackoff;-><init>(Lcom/helpshift/common/poller/ExponentialBackoff$Builder;)V

    iput-object v0, p0, Lcom/helpshift/common/poller/HttpBackoff;->exponentialBackoff:Lcom/helpshift/common/poller/ExponentialBackoff;

    .line 17
    iget-object p1, p1, Lcom/helpshift/common/poller/HttpBackoff$Builder;->retryPolicy:Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;

    iput-object p1, p0, Lcom/helpshift/common/poller/HttpBackoff;->retryPolicy:Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;

    return-void
.end method


# virtual methods
.method public nextIntervalMillis(I)J
    .locals 3

    .line 35
    iget-object v0, p0, Lcom/helpshift/common/poller/HttpBackoff;->exponentialBackoff:Lcom/helpshift/common/poller/ExponentialBackoff;

    invoke-virtual {v0}, Lcom/helpshift/common/poller/ExponentialBackoff;->nextIntervalMillis()J

    move-result-wide v0

    .line 36
    iget-object v2, p0, Lcom/helpshift/common/poller/HttpBackoff;->retryPolicy:Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;

    invoke-interface {v2, p1}, Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;->shouldRetry(I)Z

    move-result p1

    if-eqz p1, :cond_0

    return-wide v0

    :cond_0
    const-wide/16 v0, -0x64

    return-wide v0
.end method

.method public reset()V
    .locals 1

    .line 25
    iget-object v0, p0, Lcom/helpshift/common/poller/HttpBackoff;->exponentialBackoff:Lcom/helpshift/common/poller/ExponentialBackoff;

    invoke-virtual {v0}, Lcom/helpshift/common/poller/ExponentialBackoff;->reset()V

    return-void
.end method
