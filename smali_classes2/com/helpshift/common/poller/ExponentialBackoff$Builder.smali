.class public Lcom/helpshift/common/poller/ExponentialBackoff$Builder;
.super Ljava/lang/Object;
.source "ExponentialBackoff.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/common/poller/ExponentialBackoff;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "Builder"
.end annotation


# static fields
.field public static final ERROR_BASE_INTERVAL_RANGE:Ljava/lang/String; = "Base interval can\'t be negative or zero"

.field public static final ERROR_MAX_ATTEMPTS_RANGE:Ljava/lang/String; = "Max attempts can\'t be negative or zero"

.field public static final ERROR_MAX_INTERVAL_LESS_THAN_BASE_INTERVAL:Ljava/lang/String; = "Max interval can\'t be less than base interval"

.field public static final ERROR_MAX_INTERVAL_RANGE:Ljava/lang/String; = "Max interval can\'t be negative or zero"

.field public static final ERROR_MULTIPLIER_RANGE:Ljava/lang/String; = "Multiplier can\'t be less than 1"

.field public static final ERROR_RANDOMNESS_RANGE:Ljava/lang/String; = "Randomness must be between 0 and 1 (both inclusive)"


# instance fields
.field baseIntervalMillis:J

.field maxAttempts:I

.field maxIntervalMillis:J

.field multiplier:F

.field randomness:F


# direct methods
.method public constructor <init>()V
    .locals 3

    .line 87
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 88
    sget-object v0, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    const-wide/16 v1, 0xa

    invoke-virtual {v0, v1, v2}, Ljava/util/concurrent/TimeUnit;->toMillis(J)J

    move-result-wide v0

    iput-wide v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->baseIntervalMillis:J

    .line 89
    sget-object v0, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    const-wide/16 v1, 0x3c

    invoke-virtual {v0, v1, v2}, Ljava/util/concurrent/TimeUnit;->toMillis(J)J

    move-result-wide v0

    iput-wide v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->maxIntervalMillis:J

    const/high16 v0, 0x3f000000    # 0.5f

    .line 90
    iput v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->randomness:F

    const/high16 v0, 0x40000000    # 2.0f

    .line 91
    iput v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->multiplier:F

    const v0, 0x7fffffff

    .line 92
    iput v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->maxAttempts:I

    return-void
.end method


# virtual methods
.method public build()Lcom/helpshift/common/poller/ExponentialBackoff;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/IllegalArgumentException;
        }
    .end annotation

    .line 176
    invoke-virtual {p0}, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->validate()V

    .line 177
    new-instance v0, Lcom/helpshift/common/poller/ExponentialBackoff;

    invoke-direct {v0, p0}, Lcom/helpshift/common/poller/ExponentialBackoff;-><init>(Lcom/helpshift/common/poller/ExponentialBackoff$Builder;)V

    return-object v0
.end method

.method public setBaseInterval(Lcom/helpshift/common/poller/Delay;)Lcom/helpshift/common/poller/ExponentialBackoff$Builder;
    .locals 3

    .line 102
    iget-object v0, p1, Lcom/helpshift/common/poller/Delay;->timeUnit:Ljava/util/concurrent/TimeUnit;

    iget-wide v1, p1, Lcom/helpshift/common/poller/Delay;->delay:J

    invoke-virtual {v0, v1, v2}, Ljava/util/concurrent/TimeUnit;->toMillis(J)J

    move-result-wide v0

    iput-wide v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->baseIntervalMillis:J

    return-object p0
.end method

.method public setMaxAttempts(I)Lcom/helpshift/common/poller/ExponentialBackoff$Builder;
    .locals 0

    .line 150
    iput p1, p0, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->maxAttempts:I

    return-object p0
.end method

.method public setMaxInterval(Lcom/helpshift/common/poller/Delay;)Lcom/helpshift/common/poller/ExponentialBackoff$Builder;
    .locals 3

    .line 113
    iget-object v0, p1, Lcom/helpshift/common/poller/Delay;->timeUnit:Ljava/util/concurrent/TimeUnit;

    iget-wide v1, p1, Lcom/helpshift/common/poller/Delay;->delay:J

    invoke-virtual {v0, v1, v2}, Ljava/util/concurrent/TimeUnit;->toMillis(J)J

    move-result-wide v0

    iput-wide v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->maxIntervalMillis:J

    return-object p0
.end method

.method public setMultiplier(F)Lcom/helpshift/common/poller/ExponentialBackoff$Builder;
    .locals 0

    .line 138
    iput p1, p0, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->multiplier:F

    return-object p0
.end method

.method public setRandomness(F)Lcom/helpshift/common/poller/ExponentialBackoff$Builder;
    .locals 0

    .line 125
    iput p1, p0, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->randomness:F

    return-object p0
.end method

.method validate()V
    .locals 5

    .line 155
    iget-wide v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->baseIntervalMillis:J

    const-wide/16 v2, 0x0

    cmp-long v4, v0, v2

    if-lez v4, :cond_5

    .line 158
    iget-wide v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->maxIntervalMillis:J

    cmp-long v4, v0, v2

    if-lez v4, :cond_4

    .line 161
    iget-wide v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->maxIntervalMillis:J

    iget-wide v2, p0, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->baseIntervalMillis:J

    cmp-long v4, v0, v2

    if-ltz v4, :cond_3

    .line 164
    iget v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->randomness:F

    const/4 v1, 0x0

    cmpg-float v0, v0, v1

    if-ltz v0, :cond_2

    iget v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->randomness:F

    const/high16 v1, 0x3f800000    # 1.0f

    cmpl-float v0, v0, v1

    if-gtz v0, :cond_2

    .line 167
    iget v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->multiplier:F

    cmpg-float v0, v0, v1

    if-ltz v0, :cond_1

    .line 170
    iget v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->maxAttempts:I

    if-lez v0, :cond_0

    return-void

    .line 171
    :cond_0
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v1, "Max attempts can\'t be negative or zero"

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 168
    :cond_1
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v1, "Multiplier can\'t be less than 1"

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 165
    :cond_2
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v1, "Randomness must be between 0 and 1 (both inclusive)"

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 162
    :cond_3
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v1, "Max interval can\'t be less than base interval"

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 159
    :cond_4
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v1, "Max interval can\'t be negative or zero"

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 156
    :cond_5
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v1, "Base interval can\'t be negative or zero"

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method
