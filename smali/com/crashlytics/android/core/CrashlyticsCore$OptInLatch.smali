.class Lcom/crashlytics/android/core/CrashlyticsCore$OptInLatch;
.super Ljava/lang/Object;
.source "CrashlyticsCore.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/crashlytics/android/core/CrashlyticsCore;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "OptInLatch"
.end annotation


# instance fields
.field private final latch:Ljava/util/concurrent/CountDownLatch;

.field private send:Z

.field final synthetic this$0:Lcom/crashlytics/android/core/CrashlyticsCore;


# direct methods
.method private constructor <init>(Lcom/crashlytics/android/core/CrashlyticsCore;)V
    .locals 1

    .line 1060
    iput-object p1, p0, Lcom/crashlytics/android/core/CrashlyticsCore$OptInLatch;->this$0:Lcom/crashlytics/android/core/CrashlyticsCore;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 p1, 0x0

    .line 1061
    iput-boolean p1, p0, Lcom/crashlytics/android/core/CrashlyticsCore$OptInLatch;->send:Z

    .line 1062
    new-instance p1, Ljava/util/concurrent/CountDownLatch;

    const/4 v0, 0x1

    invoke-direct {p1, v0}, Ljava/util/concurrent/CountDownLatch;-><init>(I)V

    iput-object p1, p0, Lcom/crashlytics/android/core/CrashlyticsCore$OptInLatch;->latch:Ljava/util/concurrent/CountDownLatch;

    return-void
.end method

.method synthetic constructor <init>(Lcom/crashlytics/android/core/CrashlyticsCore;Lcom/crashlytics/android/core/CrashlyticsCore$1;)V
    .locals 0

    .line 1060
    invoke-direct {p0, p1}, Lcom/crashlytics/android/core/CrashlyticsCore$OptInLatch;-><init>(Lcom/crashlytics/android/core/CrashlyticsCore;)V

    return-void
.end method


# virtual methods
.method await()V
    .locals 1

    .line 1075
    :try_start_0
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore$OptInLatch;->latch:Ljava/util/concurrent/CountDownLatch;

    invoke-virtual {v0}, Ljava/util/concurrent/CountDownLatch;->await()V
    :try_end_0
    .catch Ljava/lang/InterruptedException; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    return-void
.end method

.method getOptIn()Z
    .locals 1

    .line 1070
    iget-boolean v0, p0, Lcom/crashlytics/android/core/CrashlyticsCore$OptInLatch;->send:Z

    return v0
.end method

.method setOptIn(Z)V
    .locals 0

    .line 1065
    iput-boolean p1, p0, Lcom/crashlytics/android/core/CrashlyticsCore$OptInLatch;->send:Z

    .line 1066
    iget-object p1, p0, Lcom/crashlytics/android/core/CrashlyticsCore$OptInLatch;->latch:Ljava/util/concurrent/CountDownLatch;

    invoke-virtual {p1}, Ljava/util/concurrent/CountDownLatch;->countDown()V

    return-void
.end method
