.class public Lcom/helpshift/common/util/HSDateFormatSpec;
.super Ljava/lang/Object;
.source "HSDateFormatSpec.java"


# static fields
.field public static final DISPLAY_DATE_PATTERN:Ljava/lang/String; = "EEEE, MMMM dd, yyyy"

.field public static final DISPLAY_TIME_PATTERN_12HR:Ljava/lang/String; = "h:mm a"

.field public static final DISPLAY_TIME_PATTERN_24HR:Ljava/lang/String; = "H:mm"

.field public static final STORAGE_TIME_FORMAT:Lcom/helpshift/common/util/HSSimpleDateFormat;

.field public static final STORAGE_TIME_PATTERN:Ljava/lang/String; = "yyyy-MM-dd\'T\'HH:mm:ss.SSS\'Z\'"

.field private static final TAG:Ljava/lang/String; = "Helpshift_DFSpec"

.field private static final formatterCache:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lcom/helpshift/common/util/HSSimpleDateFormat;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 22
    new-instance v0, Lcom/helpshift/common/util/HSSimpleDateFormat;

    const-string v1, "yyyy-MM-dd\'T\'HH:mm:ss.SSS\'Z\'"

    const-string v2, "GMT"

    invoke-direct {v0, v1, v2}, Lcom/helpshift/common/util/HSSimpleDateFormat;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    sput-object v0, Lcom/helpshift/common/util/HSDateFormatSpec;->STORAGE_TIME_FORMAT:Lcom/helpshift/common/util/HSSimpleDateFormat;

    .line 23
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    sput-object v0, Lcom/helpshift/common/util/HSDateFormatSpec;->formatterCache:Ljava/util/Map;

    return-void
.end method

.method private constructor <init>()V
    .locals 0

    .line 28
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static addMilliSeconds(Lcom/helpshift/common/util/HSSimpleDateFormat;Ljava/lang/String;I)Ljava/lang/String;
    .locals 5

    .line 90
    :try_start_0
    invoke-virtual {p0, p1}, Lcom/helpshift/common/util/HSSimpleDateFormat;->parse(Ljava/lang/String;)Ljava/util/Date;

    move-result-object v0

    .line 91
    invoke-static {}, Ljava/util/Calendar;->getInstance()Ljava/util/Calendar;

    move-result-object v1

    .line 92
    invoke-virtual {v1, v0}, Ljava/util/Calendar;->setTime(Ljava/util/Date;)V

    .line 93
    new-instance v0, Ljava/util/Date;

    invoke-virtual {v1}, Ljava/util/Calendar;->getTimeInMillis()J

    move-result-wide v1

    int-to-long v3, p2

    add-long/2addr v1, v3

    invoke-direct {v0, v1, v2}, Ljava/util/Date;-><init>(J)V

    .line 94
    invoke-virtual {p0, v0}, Lcom/helpshift/common/util/HSSimpleDateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object p0
    :try_end_0
    .catch Ljava/text/ParseException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    :catch_0
    move-exception p0

    const-string p2, "Helpshift_DFSpec"

    const-string v0, "Parsing exception on adding millisecond"

    .line 97
    invoke-static {p2, v0, p0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    return-object p1
.end method

.method public static calculateTimeDelta(Ljava/lang/String;)F
    .locals 5

    .line 103
    invoke-static {p0}, Ljava/lang/Double;->parseDouble(Ljava/lang/String;)D

    move-result-wide v0

    invoke-static {v0, v1}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object p0

    .line 104
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    long-to-double v0, v0

    const-wide v2, 0x408f400000000000L    # 1000.0

    invoke-static {v0, v1}, Ljava/lang/Double;->isNaN(D)Z

    div-double/2addr v0, v2

    invoke-static {v0, v1}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object v0

    .line 105
    invoke-virtual {p0}, Ljava/lang/Double;->doubleValue()D

    move-result-wide v1

    invoke-virtual {v0}, Ljava/lang/Double;->doubleValue()D

    move-result-wide v3

    sub-double/2addr v1, v3

    double-to-float p0, v1

    return p0
.end method

.method public static convertToEpochTime(Ljava/lang/String;)J
    .locals 2

    .line 111
    :try_start_0
    sget-object v0, Lcom/helpshift/common/util/HSDateFormatSpec;->STORAGE_TIME_FORMAT:Lcom/helpshift/common/util/HSSimpleDateFormat;

    invoke-virtual {v0, p0}, Lcom/helpshift/common/util/HSSimpleDateFormat;->parse(Ljava/lang/String;)Ljava/util/Date;

    move-result-object p0

    .line 112
    invoke-virtual {p0}, Ljava/util/Date;->getTime()J

    move-result-wide v0
    :try_end_0
    .catch Ljava/text/ParseException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p0

    const-string v0, "Helpshift_DFSpec"

    const-string v1, "Parsing exception on converting storageTimeFormat to epochTime"

    .line 115
    invoke-static {v0, v1, p0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    const-wide/16 v0, -0x1

    :goto_0
    return-wide v0
.end method

.method public static getCurrentAdjustedTime(Lcom/helpshift/common/platform/Platform;)Ljava/util/Date;
    .locals 3

    .line 76
    new-instance v0, Ljava/util/Date;

    invoke-static {p0}, Lcom/helpshift/common/util/HSDateFormatSpec;->getCurrentAdjustedTimeInMillis(Lcom/helpshift/common/platform/Platform;)J

    move-result-wide v1

    invoke-direct {v0, v1, v2}, Ljava/util/Date;-><init>(J)V

    return-object v0
.end method

.method public static getCurrentAdjustedTimeForStorage(Lcom/helpshift/common/platform/Platform;)Lcom/helpshift/util/ValuePair;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/common/platform/Platform;",
            ")",
            "Lcom/helpshift/util/ValuePair<",
            "Ljava/lang/String;",
            "Ljava/lang/Long;",
            ">;"
        }
    .end annotation

    .line 80
    invoke-static {p0}, Lcom/helpshift/common/util/HSDateFormatSpec;->getCurrentAdjustedTimeInMillis(Lcom/helpshift/common/platform/Platform;)J

    move-result-wide v0

    invoke-static {v0, v1}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object p0

    .line 81
    new-instance v0, Ljava/util/Date;

    invoke-virtual {p0}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-direct {v0, v1, v2}, Ljava/util/Date;-><init>(J)V

    .line 82
    sget-object v1, Lcom/helpshift/common/util/HSDateFormatSpec;->STORAGE_TIME_FORMAT:Lcom/helpshift/common/util/HSSimpleDateFormat;

    invoke-virtual {v1, v0}, Lcom/helpshift/common/util/HSSimpleDateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v0

    .line 83
    new-instance v1, Lcom/helpshift/util/ValuePair;

    invoke-direct {v1, v0, p0}, Lcom/helpshift/util/ValuePair;-><init>(Ljava/lang/Object;Ljava/lang/Object;)V

    return-object v1
.end method

.method public static getCurrentAdjustedTimeInMillis(Lcom/helpshift/common/platform/Platform;)J
    .locals 4

    .line 55
    invoke-interface {p0}, Lcom/helpshift/common/platform/Platform;->getNetworkRequestDAO()Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    move-result-object p0

    invoke-interface {p0}, Lcom/helpshift/common/platform/network/NetworkRequestDAO;->getServerTimeDelta()F

    move-result p0

    .line 56
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    const v2, -0x457ced91    # -0.001f

    cmpl-float v2, p0, v2

    if-lez v2, :cond_0

    const v2, 0x3a83126f    # 0.001f

    cmpg-float v2, p0, v2

    if-gez v2, :cond_0

    const-wide/16 v2, 0x0

    goto :goto_0

    :cond_0
    const/high16 v2, 0x447a0000    # 1000.0f

    mul-float p0, p0, v2

    float-to-long v2, p0

    :goto_0
    const/4 p0, 0x0

    add-long/2addr v0, v2

    return-wide v0
.end method

.method public static getDateFormatter(Ljava/lang/String;Ljava/util/Locale;)Lcom/helpshift/common/util/HSSimpleDateFormat;
    .locals 2

    .line 44
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "_"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/util/Locale;->getLanguage()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    .line 45
    sget-object v1, Lcom/helpshift/common/util/HSDateFormatSpec;->formatterCache:Ljava/util/Map;

    invoke-interface {v1, v0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/common/util/HSSimpleDateFormat;

    if-nez v1, :cond_0

    .line 47
    new-instance v1, Lcom/helpshift/common/util/HSSimpleDateFormat;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/common/util/HSSimpleDateFormat;-><init>(Ljava/lang/String;Ljava/util/Locale;)V

    .line 48
    sget-object p0, Lcom/helpshift/common/util/HSDateFormatSpec;->formatterCache:Ljava/util/Map;

    invoke-interface {p0, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_0
    return-object v1
.end method

.method public static getDateFormatter(Ljava/lang/String;Ljava/util/Locale;Ljava/lang/String;)Lcom/helpshift/common/util/HSSimpleDateFormat;
    .locals 2

    .line 33
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "_"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/util/Locale;->getLanguage()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "_"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    .line 34
    sget-object v1, Lcom/helpshift/common/util/HSDateFormatSpec;->formatterCache:Ljava/util/Map;

    invoke-interface {v1, v0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/common/util/HSSimpleDateFormat;

    if-nez v1, :cond_0

    .line 36
    new-instance v1, Lcom/helpshift/common/util/HSSimpleDateFormat;

    invoke-direct {v1, p0, p1, p2}, Lcom/helpshift/common/util/HSSimpleDateFormat;-><init>(Ljava/lang/String;Ljava/util/Locale;Ljava/lang/String;)V

    .line 37
    sget-object p0, Lcom/helpshift/common/util/HSDateFormatSpec;->formatterCache:Ljava/util/Map;

    invoke-interface {p0, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_0
    return-object v1
.end method
