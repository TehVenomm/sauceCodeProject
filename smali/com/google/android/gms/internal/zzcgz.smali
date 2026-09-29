.class final Lcom/google/android/gms/internal/zzcgz;
.super Lcom/google/android/gms/internal/zzcje;


# instance fields
.field private final zzjbg:Lcom/google/android/gms/common/api/internal/zzcj;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/common/api/internal/zzcj<",
            "Lcom/google/android/gms/nearby/connection/EndpointDiscoveryCallback;",
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
            "Lcom/google/android/gms/nearby/connection/EndpointDiscoveryCallback;",
            ">;)V"
        }
    .end annotation

    invoke-direct {p0}, Lcom/google/android/gms/internal/zzcje;-><init>()V

    invoke-static {p1}, Lcom/google/android/gms/common/internal/zzbp;->zzu(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/google/android/gms/common/api/internal/zzcj;

    iput-object p1, p0, Lcom/google/android/gms/internal/zzcgz;->zzjbg:Lcom/google/android/gms/common/api/internal/zzcj;

    return-void
.end method


# virtual methods
.method public final zza(Lcom/google/android/gms/internal/zzckb;)V
    .locals 2

    iget-object v0, p0, Lcom/google/android/gms/internal/zzcgz;->zzjbg:Lcom/google/android/gms/common/api/internal/zzcj;

    new-instance v1, Lcom/google/android/gms/internal/zzcha;

    invoke-direct {v1, p0, p1}, Lcom/google/android/gms/internal/zzcha;-><init>(Lcom/google/android/gms/internal/zzcgz;Lcom/google/android/gms/internal/zzckb;)V

    invoke-virtual {v0, v1}, Lcom/google/android/gms/common/api/internal/zzcj;->zza(Lcom/google/android/gms/common/api/internal/zzcm;)V

    return-void
.end method

.method public final zza(Lcom/google/android/gms/internal/zzckd;)V
    .locals 2

    iget-object v0, p0, Lcom/google/android/gms/internal/zzcgz;->zzjbg:Lcom/google/android/gms/common/api/internal/zzcj;

    new-instance v1, Lcom/google/android/gms/internal/zzchb;

    invoke-direct {v1, p0, p1}, Lcom/google/android/gms/internal/zzchb;-><init>(Lcom/google/android/gms/internal/zzcgz;Lcom/google/android/gms/internal/zzckd;)V

    invoke-virtual {v0, v1}, Lcom/google/android/gms/common/api/internal/zzcj;->zza(Lcom/google/android/gms/common/api/internal/zzcm;)V

    return-void
.end method

.method public final zza(Lcom/google/android/gms/internal/zzckn;)V
    .locals 0

    return-void
.end method
