.class public Lcom/helpshift/common/poller/ExponentialBackoff;
.super Ljava/lang/Object;
.source "ExponentialBackoff.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/common/poller/ExponentialBackoff$Builder;
    }
.end annotation


# static fields
.field public static final STOP:J = -0x64L


# instance fields
.field private attempts:I

.field private final baseIntervalMillis:J

.field private currentBaseIntervalMillis:J

.field private final maxAttempts:I

.field private final maxIntervalMillis:J

.field private final multiplier:F

.field private final random:Ljava/security/SecureRandom;

.field private final randomness:F


# direct methods
.method constructor <init>(Lcom/helpshift/common/poller/ExponentialBackoff$Builder;)V
    .locals 2

    .line 34
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 35
    iget-wide v0, p1, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->baseIntervalMillis:J

    iput-wide v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->baseIntervalMillis:J

    .line 36
    iget-wide v0, p1, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->maxIntervalMillis:J

    iput-wide v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->maxIntervalMillis:J

    .line 37
    iget v0, p1, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->randomness:F

    iput v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->randomness:F

    .line 38
    iget v0, p1, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->multiplier:F

    iput v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->multiplier:F

    .line 39
    iget p1, p1, Lcom/helpshift/common/poller/ExponentialBackoff$Builder;->maxAttempts:I

    iput p1, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->maxAttempts:I

    .line 40
    new-instance p1, Ljava/security/SecureRandom;

    invoke-direct {p1}, Ljava/security/SecureRandom;-><init>()V

    iput-object p1, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->random:Ljava/security/SecureRandom;

    .line 41
    invoke-virtual {p0}, Lcom/helpshift/common/poller/ExponentialBackoff;->reset()V

    return-void
.end method


# virtual methods
.method public nextIntervalMillis()J
    .locals 7

    .line 59
    iget v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->attempts:I

    iget v1, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->maxAttempts:I

    if-lt v0, v1, :cond_0

    const-wide/16 v0, -0x64

    return-wide v0

    .line 62
    :cond_0
    iget v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->attempts:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->attempts:I

    .line 63
    iget-wide v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->currentBaseIntervalMillis:J

    long-to-float v0, v0

    iget v1, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->randomness:F

    const/high16 v2, 0x3f800000    # 1.0f

    sub-float v1, v2, v1

    mul-float v0, v0, v1

    .line 64
    iget-wide v3, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->currentBaseIntervalMillis:J

    long-to-float v1, v3

    iget v3, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->randomness:F

    add-float/2addr v3, v2

    mul-float v1, v1, v3

    .line 65
    iget-wide v2, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->currentBaseIntervalMillis:J

    iget-wide v4, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->maxIntervalMillis:J

    cmp-long v6, v2, v4

    if-gtz v6, :cond_1

    .line 66
    iget-wide v2, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->currentBaseIntervalMillis:J

    long-to-float v2, v2

    iget v3, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->multiplier:F

    mul-float v2, v2, v3

    float-to-long v2, v2

    iget-wide v4, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->maxIntervalMillis:J

    invoke-static {v2, v3, v4, v5}, Ljava/lang/Math;->min(JJ)J

    move-result-wide v2

    iput-wide v2, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->currentBaseIntervalMillis:J

    .line 68
    :cond_1
    iget-object v2, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->random:Ljava/security/SecureRandom;

    invoke-virtual {v2}, Ljava/security/SecureRandom;->nextFloat()F

    move-result v2

    sub-float/2addr v1, v0

    mul-float v2, v2, v1

    add-float/2addr v0, v2

    float-to-long v0, v0

    return-wide v0
.end method

.method public reset()V
    .locals 2

    .line 49
    iget-wide v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->baseIntervalMillis:J

    iput-wide v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->currentBaseIntervalMillis:J

    const/4 v0, 0x0

    .line 50
    iput v0, p0, Lcom/helpshift/common/poller/ExponentialBackoff;->attempts:I

    return-void
.end method
