.class public Lcom/helpshift/specifications/GenericSyncSpecification;
.super Ljava/lang/Object;
.source "GenericSyncSpecification.java"

# interfaces
.implements Lcom/helpshift/specifications/SyncSpecification;


# instance fields
.field private final dataChangeThreshold:I

.field private final dataType:Ljava/lang/String;

.field private final elapsedTimeThreshold:J


# direct methods
.method public constructor <init>(IJLjava/util/concurrent/TimeUnit;Ljava/lang/String;)V
    .locals 0

    .line 24
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 25
    iput p1, p0, Lcom/helpshift/specifications/GenericSyncSpecification;->dataChangeThreshold:I

    .line 26
    sget-object p1, Ljava/util/concurrent/TimeUnit;->MILLISECONDS:Ljava/util/concurrent/TimeUnit;

    invoke-virtual {p1, p2, p3, p4}, Ljava/util/concurrent/TimeUnit;->convert(JLjava/util/concurrent/TimeUnit;)J

    move-result-wide p1

    iput-wide p1, p0, Lcom/helpshift/specifications/GenericSyncSpecification;->elapsedTimeThreshold:J

    .line 27
    iput-object p5, p0, Lcom/helpshift/specifications/GenericSyncSpecification;->dataType:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getDataType()Ljava/lang/String;
    .locals 1

    .line 32
    iget-object v0, p0, Lcom/helpshift/specifications/GenericSyncSpecification;->dataType:Ljava/lang/String;

    return-object v0
.end method

.method public isSatisfied(IJ)Z
    .locals 2

    .line 46
    invoke-static {p2, p3}, Ljava/lang/Math;->abs(J)J

    move-result-wide p2

    .line 48
    iget v0, p0, Lcom/helpshift/specifications/GenericSyncSpecification;->dataChangeThreshold:I

    if-ge p1, v0, :cond_1

    iget-wide v0, p0, Lcom/helpshift/specifications/GenericSyncSpecification;->elapsedTimeThreshold:J

    cmp-long p1, p2, v0

    if-lez p1, :cond_0

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 p1, 0x1

    :goto_1
    return p1
.end method
