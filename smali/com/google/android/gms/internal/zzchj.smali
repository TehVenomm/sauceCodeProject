.class final Lcom/google/android/gms/internal/zzchj;
.super Lcom/google/android/gms/internal/zzcjj;


# instance fields
.field private final zzjbr:Lcom/google/android/gms/common/api/internal/zzcj;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/common/api/internal/zzcj<",
            "Lcom/google/android/gms/nearby/connection/PayloadCallback;",
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
            "Lcom/google/android/gms/nearby/connection/PayloadCallback;",
            ">;)V"
        }
    .end annotation

    invoke-direct {p0}, Lcom/google/android/gms/internal/zzcjj;-><init>()V

    invoke-static {p1}, Lcom/google/android/gms/common/internal/zzbp;->zzu(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/google/android/gms/common/api/internal/zzcj;

    iput-object p1, p0, Lcom/google/android/gms/internal/zzchj;->zzjbr:Lcom/google/android/gms/common/api/internal/zzcj;

    return-void
.end method


# virtual methods
.method public final zza(Lcom/google/android/gms/internal/zzckf;)V
    .locals 2

    iget-object v0, p0, Lcom/google/android/gms/internal/zzchj;->zzjbr:Lcom/google/android/gms/common/api/internal/zzcj;

    new-instance v1, Lcom/google/android/gms/internal/zzchk;

    invoke-direct {v1, p0, p1}, Lcom/google/android/gms/internal/zzchk;-><init>(Lcom/google/android/gms/internal/zzchj;Lcom/google/android/gms/internal/zzckf;)V

    invoke-virtual {v0, v1}, Lcom/google/android/gms/common/api/internal/zzcj;->zza(Lcom/google/android/gms/common/api/internal/zzcm;)V

    return-void
.end method

.method public final zza(Lcom/google/android/gms/internal/zzckh;)V
    .locals 2

    iget-object v0, p0, Lcom/google/android/gms/internal/zzchj;->zzjbr:Lcom/google/android/gms/common/api/internal/zzcj;

    new-instance v1, Lcom/google/android/gms/internal/zzchl;

    invoke-direct {v1, p0, p1}, Lcom/google/android/gms/internal/zzchl;-><init>(Lcom/google/android/gms/internal/zzchj;Lcom/google/android/gms/internal/zzckh;)V

    invoke-virtual {v0, v1}, Lcom/google/android/gms/common/api/internal/zzcj;->zza(Lcom/google/android/gms/common/api/internal/zzcm;)V

    return-void
.end method
