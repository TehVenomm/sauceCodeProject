.class public Lcom/helpshift/util/TimeUtil;
.super Ljava/lang/Object;
.source "TimeUtil.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 6
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static getAdjustedTimeInMillis(Ljava/lang/Float;)J
    .locals 4

    .line 20
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    if-eqz p0, :cond_0

    .line 21
    invoke-virtual {p0}, Ljava/lang/Float;->floatValue()F

    move-result v2

    const/4 v3, 0x0

    cmpl-float v2, v2, v3

    if-eqz v2, :cond_0

    long-to-float v0, v0

    .line 22
    invoke-virtual {p0}, Ljava/lang/Float;->floatValue()F

    move-result p0

    const/high16 v1, 0x447a0000    # 1000.0f

    mul-float p0, p0, v1

    add-float/2addr v0, p0

    float-to-long v0, v0

    :cond_0
    return-wide v0
.end method

.method public static getAdjustedTimestamp(Ljava/lang/Float;)Ljava/lang/String;
    .locals 6
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 10
    sget-object v0, Lcom/helpshift/util/HSFormat;->tsSecFormatter:Ljava/text/DecimalFormat;

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v1

    long-to-double v1, v1

    const-wide v3, 0x408f400000000000L    # 1000.0

    invoke-static {v1, v2}, Ljava/lang/Double;->isNaN(D)Z

    div-double/2addr v1, v3

    invoke-virtual {v0, v1, v2}, Ljava/text/DecimalFormat;->format(D)Ljava/lang/String;

    move-result-object v0

    if-eqz p0, :cond_0

    .line 11
    invoke-virtual {p0}, Ljava/lang/Float;->floatValue()F

    move-result v1

    const/4 v2, 0x0

    cmpl-float v1, v1, v2

    if-eqz v1, :cond_0

    .line 12
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    long-to-double v0, v0

    invoke-static {v0, v1}, Ljava/lang/Double;->isNaN(D)Z

    div-double/2addr v0, v3

    invoke-static {v0, v1}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object v0

    .line 13
    sget-object v1, Lcom/helpshift/util/HSFormat;->tsSecFormatter:Ljava/text/DecimalFormat;

    invoke-virtual {v0}, Ljava/lang/Double;->doubleValue()D

    move-result-wide v2

    invoke-virtual {p0}, Ljava/lang/Float;->floatValue()F

    move-result p0

    float-to-double v4, p0

    invoke-static {v4, v5}, Ljava/lang/Double;->isNaN(D)Z

    add-double/2addr v2, v4

    invoke-virtual {v1, v2, v3}, Ljava/text/DecimalFormat;->format(D)Ljava/lang/String;

    move-result-object v0

    :cond_0
    return-object v0
.end method

.method public static getCurrentTimeInMillis()J
    .locals 2

    .line 28
    invoke-static {}, Lcom/helpshift/model/InfoModelFactory;->getInstance()Lcom/helpshift/model/InfoModelFactory;

    move-result-object v0

    iget-object v0, v0, Lcom/helpshift/model/InfoModelFactory;->sdkInfoModel:Lcom/helpshift/model/SdkInfoModel;

    invoke-virtual {v0}, Lcom/helpshift/model/SdkInfoModel;->getServerTimeDelta()Ljava/lang/Float;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/util/TimeUtil;->getAdjustedTimeInMillis(Ljava/lang/Float;)J

    move-result-wide v0

    return-wide v0
.end method

.method public static getCurrentTimestamp()Ljava/lang/String;
    .locals 1

    .line 32
    invoke-static {}, Lcom/helpshift/model/InfoModelFactory;->getInstance()Lcom/helpshift/model/InfoModelFactory;

    move-result-object v0

    iget-object v0, v0, Lcom/helpshift/model/InfoModelFactory;->sdkInfoModel:Lcom/helpshift/model/SdkInfoModel;

    invoke-virtual {v0}, Lcom/helpshift/model/SdkInfoModel;->getServerTimeDelta()Ljava/lang/Float;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/util/TimeUtil;->getAdjustedTimestamp(Ljava/lang/Float;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public static getSinceText(J)Ljava/lang/String;
    .locals 5

    .line 36
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    const-wide/16 v2, 0x3e8

    div-long/2addr v0, v2

    sub-long/2addr v0, p0

    const-wide/16 p0, 0x3c

    cmp-long v2, v0, p0

    if-gez v2, :cond_0

    .line 42
    new-instance p0, Ljava/lang/StringBuilder;

    invoke-direct {p0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p0, v0, v1}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    const-string p1, "s"

    invoke-virtual {p0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    goto :goto_0

    :cond_0
    const-wide/16 v2, 0xe10

    cmp-long v4, v0, v2

    if-gez v4, :cond_1

    .line 45
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    div-long/2addr v0, p0

    invoke-virtual {v2, v0, v1}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    const-string p0, "m"

    invoke-virtual {v2, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    goto :goto_0

    :cond_1
    const-wide/32 p0, 0x15180

    cmp-long v4, v0, p0

    if-gez v4, :cond_2

    .line 48
    new-instance p0, Ljava/lang/StringBuilder;

    invoke-direct {p0}, Ljava/lang/StringBuilder;-><init>()V

    div-long/2addr v0, v2

    invoke-virtual {p0, v0, v1}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    const-string p1, "h"

    invoke-virtual {p0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    goto :goto_0

    .line 51
    :cond_2
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    div-long/2addr v0, p0

    invoke-virtual {v2, v0, v1}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    const-string p0, "d"

    invoke-virtual {v2, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    :goto_0
    return-object p0
.end method


# virtual methods
.method public elapsedTimeMillis()J
    .locals 2

    .line 57
    invoke-static {}, Landroid/os/SystemClock;->elapsedRealtime()J

    move-result-wide v0

    return-wide v0
.end method
