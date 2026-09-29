.class public Lio/fabric/sdk/android/services/settings/AnalyticsSettingsData;
.super Ljava/lang/Object;
.source "AnalyticsSettingsData.java"


# static fields
.field public static final DEFAULT_SAMPLING_RATE:I = 0x1


# instance fields
.field public final analyticsURL:Ljava/lang/String;

.field public final flushIntervalSeconds:I

.field public final maxByteSizePerFile:I

.field public final maxFileCountPerSend:I

.field public final maxPendingSendFileCount:I

.field public final samplingRate:I

.field public final trackCustomEvents:Z


# direct methods
.method public constructor <init>(Ljava/lang/String;IIIIZ)V
    .locals 8
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    const/4 v7, 0x1

    move-object v0, p0

    move-object v1, p1

    move v2, p2

    move v3, p3

    move v4, p4

    move v5, p5

    move v6, p6

    .line 55
    invoke-direct/range {v0 .. v7}, Lio/fabric/sdk/android/services/settings/AnalyticsSettingsData;-><init>(Ljava/lang/String;IIIIZI)V

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;IIIIZI)V
    .locals 0

    .line 37
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 38
    iput-object p1, p0, Lio/fabric/sdk/android/services/settings/AnalyticsSettingsData;->analyticsURL:Ljava/lang/String;

    .line 39
    iput p2, p0, Lio/fabric/sdk/android/services/settings/AnalyticsSettingsData;->flushIntervalSeconds:I

    .line 40
    iput p3, p0, Lio/fabric/sdk/android/services/settings/AnalyticsSettingsData;->maxByteSizePerFile:I

    .line 41
    iput p4, p0, Lio/fabric/sdk/android/services/settings/AnalyticsSettingsData;->maxFileCountPerSend:I

    .line 42
    iput p5, p0, Lio/fabric/sdk/android/services/settings/AnalyticsSettingsData;->maxPendingSendFileCount:I

    .line 43
    iput-boolean p6, p0, Lio/fabric/sdk/android/services/settings/AnalyticsSettingsData;->trackCustomEvents:Z

    .line 44
    iput p7, p0, Lio/fabric/sdk/android/services/settings/AnalyticsSettingsData;->samplingRate:I

    return-void
.end method
