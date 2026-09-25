.class final Lcom/google/android/gms/measurement/internal/zzjd;
.super Ljava/lang/Object;


# instance fields
.field private startTime:J

.field private final zzac:Lcom/google/android/gms/common/util/Clock;


# direct methods
.method public constructor <init>(Lcom/google/android/gms/common/util/Clock;)V
    .locals 0

    .line 1
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 2
    invoke-static {p1}, Lcom/google/android/gms/common/internal/Preconditions;->checkNotNull(Ljava/lang/Object;)Ljava/lang/Object;

    .line 3
    iput-object p1, p0, Lcom/google/android/gms/measurement/internal/zzjd;->zzac:Lcom/google/android/gms/common/util/Clock;

    return-void
.end method


# virtual methods
.method public final clear()V
    .locals 2

    const-wide/16 v0, 0x0

    .line 7
    iput-wide v0, p0, Lcom/google/android/gms/measurement/internal/zzjd;->startTime:J

    return-void
.end method

.method public final start()V
    .locals 2

    .line 5
    iget-object v0, p0, Lcom/google/android/gms/measurement/internal/zzjd;->zzac:Lcom/google/android/gms/common/util/Clock;

    invoke-interface {v0}, Lcom/google/android/gms/common/util/Clock;->elapsedRealtime()J

    move-result-wide v0

    iput-wide v0, p0, Lcom/google/android/gms/measurement/internal/zzjd;->startTime:J

    return-void
.end method

.method public final zzad(J)Z
    .locals 4

    .line 9
    iget-wide p1, p0, Lcom/google/android/gms/measurement/internal/zzjd;->startTime:J

    const-wide/16 v0, 0x0

    cmp-long v2, p1, v0

    const/4 p1, 0x1

    if-nez v2, :cond_0

    return p1

    .line 11
    :cond_0
    iget-object p2, p0, Lcom/google/android/gms/measurement/internal/zzjd;->zzac:Lcom/google/android/gms/common/util/Clock;

    invoke-interface {p2}, Lcom/google/android/gms/common/util/Clock;->elapsedRealtime()J

    move-result-wide v0

    iget-wide v2, p0, Lcom/google/android/gms/measurement/internal/zzjd;->startTime:J

    sub-long/2addr v0, v2

    const-wide/32 v2, 0x36ee80

    cmp-long p2, v0, v2

    if-ltz p2, :cond_1

    return p1

    :cond_1
    const/4 p1, 0x0

    return p1
.end method
