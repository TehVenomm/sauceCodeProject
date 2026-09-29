.class public final Lcom/helpshift/util/HSFormat;
.super Ljava/lang/Object;
.source "HSFormat.java"


# static fields
.field public static final alphaNumericCharacters:Ljava/lang/String; = "abcdefghijklmnopqrstuvwxyz0123456789"

.field public static final datePropertyTsFormat:Lcom/helpshift/common/util/HSSimpleDateFormat;

.field public static final deviceInfoTsFormat:Lcom/helpshift/common/util/HSSimpleDateFormat;

.field public static final errorLogReportingTimeFormat:Lcom/helpshift/common/util/HSSimpleDateFormat;

.field public static final timeStampAnonymousUserFormat:Lcom/helpshift/common/util/HSSimpleDateFormat;

.field public static final tsSecFormatter:Ljava/text/DecimalFormat;


# direct methods
.method static constructor <clinit>()V
    .locals 4

    .line 11
    new-instance v0, Lcom/helpshift/common/util/HSSimpleDateFormat;

    const-string v1, "yyyy-MM-dd\'T\'HH:mm:ss.SSS\'Z\'"

    .line 12
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v2

    invoke-direct {v0, v1, v2}, Lcom/helpshift/common/util/HSSimpleDateFormat;-><init>(Ljava/lang/String;Ljava/util/Locale;)V

    sput-object v0, Lcom/helpshift/util/HSFormat;->deviceInfoTsFormat:Lcom/helpshift/common/util/HSSimpleDateFormat;

    .line 14
    new-instance v0, Lcom/helpshift/common/util/HSSimpleDateFormat;

    const-string v1, "yyyy-MM-dd\'T\'HH:mm:ss.SSS\'Z\'"

    .line 15
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v2

    const-string v3, "UTC"

    invoke-direct {v0, v1, v2, v3}, Lcom/helpshift/common/util/HSSimpleDateFormat;-><init>(Ljava/lang/String;Ljava/util/Locale;Ljava/lang/String;)V

    sput-object v0, Lcom/helpshift/util/HSFormat;->datePropertyTsFormat:Lcom/helpshift/common/util/HSSimpleDateFormat;

    .line 17
    new-instance v0, Lcom/helpshift/common/util/HSSimpleDateFormat;

    const-string v1, "yyyyMMddHHmmssSSS"

    .line 18
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v2

    invoke-direct {v0, v1, v2}, Lcom/helpshift/common/util/HSSimpleDateFormat;-><init>(Ljava/lang/String;Ljava/util/Locale;)V

    sput-object v0, Lcom/helpshift/util/HSFormat;->timeStampAnonymousUserFormat:Lcom/helpshift/common/util/HSSimpleDateFormat;

    .line 20
    new-instance v0, Ljava/text/DecimalFormat;

    const-string v1, "0.000"

    new-instance v2, Ljava/text/DecimalFormatSymbols;

    sget-object v3, Ljava/util/Locale;->US:Ljava/util/Locale;

    invoke-direct {v2, v3}, Ljava/text/DecimalFormatSymbols;-><init>(Ljava/util/Locale;)V

    invoke-direct {v0, v1, v2}, Ljava/text/DecimalFormat;-><init>(Ljava/lang/String;Ljava/text/DecimalFormatSymbols;)V

    sput-object v0, Lcom/helpshift/util/HSFormat;->tsSecFormatter:Ljava/text/DecimalFormat;

    .line 22
    new-instance v0, Lcom/helpshift/common/util/HSSimpleDateFormat;

    const-string v1, "dd/MM/yyyy HH:mm:ss.SSS "

    .line 23
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v2

    invoke-direct {v0, v1, v2}, Lcom/helpshift/common/util/HSSimpleDateFormat;-><init>(Ljava/lang/String;Ljava/util/Locale;)V

    sput-object v0, Lcom/helpshift/util/HSFormat;->errorLogReportingTimeFormat:Lcom/helpshift/common/util/HSSimpleDateFormat;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 9
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method
