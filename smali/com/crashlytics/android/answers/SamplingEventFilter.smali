.class public Lcom/crashlytics/android/answers/SamplingEventFilter;
.super Ljava/lang/Object;
.source "SamplingEventFilter.java"

# interfaces
.implements Lcom/crashlytics/android/answers/EventFilter;


# static fields
.field static final EVENTS_TYPE_TO_SAMPLE:Ljava/util/Set;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Set<",
            "Lcom/crashlytics/android/answers/SessionEvent$Type;",
            ">;"
        }
    .end annotation
.end field


# instance fields
.field final samplingRate:I


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 19
    new-instance v0, Lcom/crashlytics/android/answers/SamplingEventFilter$1;

    invoke-direct {v0}, Lcom/crashlytics/android/answers/SamplingEventFilter$1;-><init>()V

    sput-object v0, Lcom/crashlytics/android/answers/SamplingEventFilter;->EVENTS_TYPE_TO_SAMPLE:Ljava/util/Set;

    return-void
.end method

.method public constructor <init>(I)V
    .locals 0

    .line 31
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 32
    iput p1, p0, Lcom/crashlytics/android/answers/SamplingEventFilter;->samplingRate:I

    return-void
.end method


# virtual methods
.method isNeverSampledEvent(Lcom/crashlytics/android/answers/SessionEvent;)Z
    .locals 2

    .line 43
    sget-object v0, Lcom/crashlytics/android/answers/SamplingEventFilter;->EVENTS_TYPE_TO_SAMPLE:Ljava/util/Set;

    iget-object v1, p1, Lcom/crashlytics/android/answers/SessionEvent;->type:Lcom/crashlytics/android/answers/SessionEvent$Type;

    invoke-interface {v0, v1}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_1

    iget-object p1, p1, Lcom/crashlytics/android/answers/SessionEvent;->sessionEventMetadata:Lcom/crashlytics/android/answers/SessionEventMetadata;

    iget-object p1, p1, Lcom/crashlytics/android/answers/SessionEventMetadata;->betaDeviceToken:Ljava/lang/String;

    if-eqz p1, :cond_0

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

.method shouldSkipEventBasedOnInstallID(Lcom/crashlytics/android/answers/SessionEvent;)Z
    .locals 1

    .line 48
    iget-object p1, p1, Lcom/crashlytics/android/answers/SessionEvent;->sessionEventMetadata:Lcom/crashlytics/android/answers/SessionEventMetadata;

    iget-object p1, p1, Lcom/crashlytics/android/answers/SessionEventMetadata;->installationId:Ljava/lang/String;

    .line 50
    invoke-virtual {p1}, Ljava/lang/String;->hashCode()I

    move-result p1

    iget v0, p0, Lcom/crashlytics/android/answers/SamplingEventFilter;->samplingRate:I

    rem-int/2addr p1, v0

    invoke-static {p1}, Ljava/lang/Math;->abs(I)I

    move-result p1

    if-eqz p1, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return p1
.end method

.method public skipEvent(Lcom/crashlytics/android/answers/SessionEvent;)Z
    .locals 1

    .line 37
    invoke-virtual {p0, p1}, Lcom/crashlytics/android/answers/SamplingEventFilter;->isNeverSampledEvent(Lcom/crashlytics/android/answers/SessionEvent;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 p1, 0x0

    return p1

    .line 39
    :cond_0
    invoke-virtual {p0, p1}, Lcom/crashlytics/android/answers/SamplingEventFilter;->shouldSkipEventBasedOnInstallID(Lcom/crashlytics/android/answers/SessionEvent;)Z

    move-result p1

    return p1
.end method
