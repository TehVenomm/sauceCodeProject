.class public Lcom/helpshift/specifications/DecayingIntervalSyncSpecification;
.super Ljava/lang/Object;
.source "DecayingIntervalSyncSpecification.java"

# interfaces
.implements Lcom/helpshift/specifications/SyncSpecification;


# instance fields
.field private final dataType:Ljava/lang/String;

.field private elapsedTimeThreshold:J

.field private maxTimeThresholdLimit:J


# direct methods
.method public constructor <init>(ILjava/util/concurrent/TimeUnit;Ljava/lang/String;)V
    .locals 3

    .line 22
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-wide/32 v0, 0x5265c00

    .line 13
    iput-wide v0, p0, Lcom/helpshift/specifications/DecayingIntervalSyncSpecification;->maxTimeThresholdLimit:J

    .line 23
    sget-object v0, Ljava/util/concurrent/TimeUnit;->MILLISECONDS:Ljava/util/concurrent/TimeUnit;

    int-to-long v1, p1

    invoke-virtual {v0, v1, v2, p2}, Ljava/util/concurrent/TimeUnit;->convert(JLjava/util/concurrent/TimeUnit;)J

    move-result-wide p1

    iput-wide p1, p0, Lcom/helpshift/specifications/DecayingIntervalSyncSpecification;->elapsedTimeThreshold:J

    .line 24
    iput-object p3, p0, Lcom/helpshift/specifications/DecayingIntervalSyncSpecification;->dataType:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public decayElapsedTimeThreshold()V
    .locals 5

    .line 55
    iget-wide v0, p0, Lcom/helpshift/specifications/DecayingIntervalSyncSpecification;->elapsedTimeThreshold:J

    long-to-double v0, v0

    const-wide v2, 0x3ff9e353f7ced917L    # 1.618

    invoke-static {v0, v1}, Ljava/lang/Double;->isNaN(D)Z

    mul-double v0, v0, v2

    double-to-long v0, v0

    iput-wide v0, p0, Lcom/helpshift/specifications/DecayingIntervalSyncSpecification;->elapsedTimeThreshold:J

    .line 56
    iget-wide v0, p0, Lcom/helpshift/specifications/DecayingIntervalSyncSpecification;->elapsedTimeThreshold:J

    iget-wide v2, p0, Lcom/helpshift/specifications/DecayingIntervalSyncSpecification;->maxTimeThresholdLimit:J

    cmp-long v4, v0, v2

    if-lez v4, :cond_0

    .line 57
    iget-wide v0, p0, Lcom/helpshift/specifications/DecayingIntervalSyncSpecification;->maxTimeThresholdLimit:J

    iput-wide v0, p0, Lcom/helpshift/specifications/DecayingIntervalSyncSpecification;->elapsedTimeThreshold:J

    :cond_0
    return-void
.end method

.method public getDataType()Ljava/lang/String;
    .locals 1

    .line 29
    iget-object v0, p0, Lcom/helpshift/specifications/DecayingIntervalSyncSpecification;->dataType:Ljava/lang/String;

    return-object v0
.end method

.method public isSatisfied(IJ)Z
    .locals 2

    .line 42
    invoke-static {p2, p3}, Ljava/lang/Math;->abs(J)J

    move-result-wide p2

    if-lez p1, :cond_0

    .line 44
    iget-wide v0, p0, Lcom/helpshift/specifications/DecayingIntervalSyncSpecification;->elapsedTimeThreshold:J

    cmp-long p1, p2, v0

    if-lez p1, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return p1
.end method
