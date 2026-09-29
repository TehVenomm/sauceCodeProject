.class public Lcom/helpshift/common/poller/HttpBackoff$Builder;
.super Ljava/lang/Object;
.source "HttpBackoff.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/common/poller/HttpBackoff;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "Builder"
.end annotation


# instance fields
.field final exponentialBackoffBuilder:Lcom/helpshift/common/poller/ExponentialBackoff$Builder;

.field retryPolicy:Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 74
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 75
    new-instance v0, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;

    invoke-direct {v0}, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;-><init>()V

    iput-object v0, p0, Lcom/helpshift/common/poller/HttpBackoff$Builder;->exponentialBackoffBuilder:Lcom/helpshift/common/poller/ExponentialBackoff$Builder;

    .line 76
    sget-object v0, Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;->ALWAYS:Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;

    iput-object v0, p0, Lcom/helpshift/common/poller/HttpBackoff$Builder;->retryPolicy:Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;

    return-void
.end method


# virtual methods
.method public build()Lcom/helpshift/common/poller/HttpBackoff;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/IllegalArgumentException;
        }
    .end annotation

    .line 110
    iget-object v0, p0, Lcom/helpshift/common/poller/HttpBackoff$Builder;->exponentialBackoffBuilder:Lcom/helpshift/common/poller/ExponentialBackoff$Builder;

    invoke-virtual {v0}, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->validate()V

    .line 111
    new-instance v0, Lcom/helpshift/common/poller/HttpBackoff;

    invoke-direct {v0, p0}, Lcom/helpshift/common/poller/HttpBackoff;-><init>(Lcom/helpshift/common/poller/HttpBackoff$Builder;)V

    return-object v0
.end method

.method public setBaseInterval(Lcom/helpshift/common/poller/Delay;)Lcom/helpshift/common/poller/HttpBackoff$Builder;
    .locals 1

    .line 85
    iget-object v0, p0, Lcom/helpshift/common/poller/HttpBackoff$Builder;->exponentialBackoffBuilder:Lcom/helpshift/common/poller/ExponentialBackoff$Builder;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->setBaseInterval(Lcom/helpshift/common/poller/Delay;)Lcom/helpshift/common/poller/ExponentialBackoff$Builder;

    return-object p0
.end method

.method public setMaxAttempts(I)Lcom/helpshift/common/poller/HttpBackoff$Builder;
    .locals 1

    .line 105
    iget-object v0, p0, Lcom/helpshift/common/poller/HttpBackoff$Builder;->exponentialBackoffBuilder:Lcom/helpshift/common/poller/ExponentialBackoff$Builder;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->setMaxAttempts(I)Lcom/helpshift/common/poller/ExponentialBackoff$Builder;

    return-object p0
.end method

.method public setMaxInterval(Lcom/helpshift/common/poller/Delay;)Lcom/helpshift/common/poller/HttpBackoff$Builder;
    .locals 1

    .line 90
    iget-object v0, p0, Lcom/helpshift/common/poller/HttpBackoff$Builder;->exponentialBackoffBuilder:Lcom/helpshift/common/poller/ExponentialBackoff$Builder;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->setMaxInterval(Lcom/helpshift/common/poller/Delay;)Lcom/helpshift/common/poller/ExponentialBackoff$Builder;

    return-object p0
.end method

.method public setMultiplier(F)Lcom/helpshift/common/poller/HttpBackoff$Builder;
    .locals 1

    .line 100
    iget-object v0, p0, Lcom/helpshift/common/poller/HttpBackoff$Builder;->exponentialBackoffBuilder:Lcom/helpshift/common/poller/ExponentialBackoff$Builder;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->setMultiplier(F)Lcom/helpshift/common/poller/ExponentialBackoff$Builder;

    return-object p0
.end method

.method public setRandomness(F)Lcom/helpshift/common/poller/HttpBackoff$Builder;
    .locals 1

    .line 95
    iget-object v0, p0, Lcom/helpshift/common/poller/HttpBackoff$Builder;->exponentialBackoffBuilder:Lcom/helpshift/common/poller/ExponentialBackoff$Builder;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->setRandomness(F)Lcom/helpshift/common/poller/ExponentialBackoff$Builder;

    return-object p0
.end method

.method public setRetryPolicy(Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;)Lcom/helpshift/common/poller/HttpBackoff$Builder;
    .locals 0

    .line 80
    iput-object p1, p0, Lcom/helpshift/common/poller/HttpBackoff$Builder;->retryPolicy:Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;

    return-object p0
.end method
