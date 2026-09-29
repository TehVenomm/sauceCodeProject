.class public Lcom/helpshift/specifications/DailyFrequencyBasedSyncSpecification;
.super Ljava/lang/Object;
.source "DailyFrequencyBasedSyncSpecification.java"

# interfaces
.implements Lcom/helpshift/specifications/SyncSpecification;


# instance fields
.field private final dataType:Ljava/lang/String;

.field private final elapsedTimeThreshold:J


# direct methods
.method public constructor <init>(ILjava/lang/String;)V
    .locals 3
    .annotation build Landroid/annotation/TargetApi;
        value = 0x9
    .end annotation

    .line 25
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 26
    sget-object v0, Ljava/util/concurrent/TimeUnit;->MILLISECONDS:Ljava/util/concurrent/TimeUnit;

    const/16 v1, 0x18

    div-int/2addr v1, p1

    int-to-long v1, v1

    sget-object p1, Ljava/util/concurrent/TimeUnit;->HOURS:Ljava/util/concurrent/TimeUnit;

    invoke-virtual {v0, v1, v2, p1}, Ljava/util/concurrent/TimeUnit;->convert(JLjava/util/concurrent/TimeUnit;)J

    move-result-wide v0

    iput-wide v0, p0, Lcom/helpshift/specifications/DailyFrequencyBasedSyncSpecification;->elapsedTimeThreshold:J

    .line 27
    iput-object p2, p0, Lcom/helpshift/specifications/DailyFrequencyBasedSyncSpecification;->dataType:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getDataType()Ljava/lang/String;
    .locals 1

    .line 32
    iget-object v0, p0, Lcom/helpshift/specifications/DailyFrequencyBasedSyncSpecification;->dataType:Ljava/lang/String;

    return-object v0
.end method

.method public isSatisfied(IJ)Z
    .locals 2

    .line 46
    invoke-static {p2, p3}, Ljava/lang/Math;->abs(J)J

    move-result-wide p2

    .line 48
    invoke-static {}, Lcom/helpshift/model/InfoModelFactory;->getInstance()Lcom/helpshift/model/InfoModelFactory;

    move-result-object v0

    iget-object v0, v0, Lcom/helpshift/model/InfoModelFactory;->sdkInfoModel:Lcom/helpshift/model/SdkInfoModel;

    invoke-virtual {v0}, Lcom/helpshift/model/SdkInfoModel;->getDevicePropertiesSyncImmediately()Ljava/lang/Boolean;

    move-result-object v0

    .line 50
    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    if-nez v0, :cond_1

    if-lez p1, :cond_0

    iget-wide v0, p0, Lcom/helpshift/specifications/DailyFrequencyBasedSyncSpecification;->elapsedTimeThreshold:J

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
