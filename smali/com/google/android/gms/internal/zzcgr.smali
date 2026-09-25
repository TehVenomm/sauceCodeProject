.class final Lcom/google/android/gms/internal/zzcgr;
.super Lcom/google/android/gms/internal/zzciw;


# instance fields
.field private final zzjbg:Lcom/google/android/gms/common/api/internal/zzcj;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/common/api/internal/zzcj<",
            "Lcom/google/android/gms/nearby/connection/ConnectionLifecycleCallback;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method constructor <init>(Lcom/google/android/gms/common/api/internal/zzcj;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/internal/zzcj<",
            "Lcom/google/android/gms/nearby/connection/ConnectionLifecycleCallback;",
            ">;)V"
        }
    .end annotation

    invoke-direct {p0}, Lcom/google/android/gms/internal/zzciw;-><init>()V

    invoke-static {p1}, Lcom/google/android/gms/common/internal/zzbp;->zzu(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/google/android/gms/common/api/internal/zzcj;

    iput-object p1, p0, Lcom/google/android/gms/internal/zzcgr;->zzjbg:Lcom/google/android/gms/common/api/internal/zzcj;

    return-void
.end method


# virtual methods
.method public final zza(Lcom/google/android/gms/internal/zzcjr;)V
    .locals 2

    iget-object v0, p0, Lcom/google/android/gms/internal/zzcgr;->zzjbg:Lcom/google/android/gms/common/api/internal/zzcj;

    new-instance v1, Lcom/google/android/gms/internal/zzcgs;

    invoke-direct {v1, p0, p1}, Lcom/google/android/gms/internal/zzcgs;-><init>(Lcom/google/android/gms/internal/zzcgr;Lcom/google/android/gms/internal/zzcjr;)V

    invoke-virtual {v0, v1}, Lcom/google/android/gms/common/api/internal/zzcj;->zza(Lcom/google/android/gms/common/api/internal/zzcm;)V

    return-void
.end method

.method public final zza(Lcom/google/android/gms/internal/zzcjx;)V
    .locals 2

    iget-object v0, p0, Lcom/google/android/gms/internal/zzcgr;->zzjbg:Lcom/google/android/gms/common/api/internal/zzcj;

    new-instance v1, Lcom/google/android/gms/internal/zzcgt;

    invoke-direct {v1, p0, p1}, Lcom/google/android/gms/internal/zzcgt;-><init>(Lcom/google/android/gms/internal/zzcgr;Lcom/google/android/gms/internal/zzcjx;)V

    invoke-virtual {v0, v1}, Lcom/google/android/gms/common/api/internal/zzcj;->zza(Lcom/google/android/gms/common/api/internal/zzcm;)V

    return-void
.end method

.method public final zza(Lcom/google/android/gms/internal/zzcjz;)V
    .locals 2

    iget-object v0, p0, Lcom/google/android/gms/internal/zzcgr;->zzjbg:Lcom/google/android/gms/common/api/internal/zzcj;

    new-instance v1, Lcom/google/android/gms/internal/zzcgu;

    invoke-direct {v1, p0, p1}, Lcom/google/android/gms/internal/zzcgu;-><init>(Lcom/google/android/gms/internal/zzcgr;Lcom/google/android/gms/internal/zzcjz;)V

    invoke-virtual {v0, v1}, Lcom/google/android/gms/common/api/internal/zzcj;->zza(Lcom/google/android/gms/common/api/internal/zzcm;)V

    return-void
.end method
