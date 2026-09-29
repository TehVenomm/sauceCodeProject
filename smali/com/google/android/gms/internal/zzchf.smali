.class final Lcom/google/android/gms/internal/zzchf;
.super Lcom/google/android/gms/internal/zzcit;


# annotations
.annotation runtime Ljava/lang/Deprecated;
.end annotation


# instance fields
.field private final zzjbp:Lcom/google/android/gms/common/api/internal/zzcj;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/common/api/internal/zzcj<",
            "Lcom/google/android/gms/nearby/connection/Connections$MessageListener;",
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
            "Lcom/google/android/gms/nearby/connection/Connections$MessageListener;",
            ">;)V"
        }
    .end annotation

    invoke-direct {p0}, Lcom/google/android/gms/internal/zzcit;-><init>()V

    invoke-static {p1}, Lcom/google/android/gms/common/internal/zzbp;->zzu(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/google/android/gms/common/api/internal/zzcj;

    iput-object p1, p0, Lcom/google/android/gms/internal/zzchf;->zzjbp:Lcom/google/android/gms/common/api/internal/zzcj;

    return-void
.end method


# virtual methods
.method public final zza(Lcom/google/android/gms/internal/zzcjz;)V
    .locals 2

    iget-object v0, p0, Lcom/google/android/gms/internal/zzchf;->zzjbp:Lcom/google/android/gms/common/api/internal/zzcj;

    new-instance v1, Lcom/google/android/gms/internal/zzchh;

    invoke-direct {v1, p0, p1}, Lcom/google/android/gms/internal/zzchh;-><init>(Lcom/google/android/gms/internal/zzchf;Lcom/google/android/gms/internal/zzcjz;)V

    invoke-virtual {v0, v1}, Lcom/google/android/gms/common/api/internal/zzcj;->zza(Lcom/google/android/gms/common/api/internal/zzcm;)V

    return-void
.end method

.method public final zza(Lcom/google/android/gms/internal/zzckf;)V
    .locals 2

    iget-object v0, p0, Lcom/google/android/gms/internal/zzchf;->zzjbp:Lcom/google/android/gms/common/api/internal/zzcj;

    new-instance v1, Lcom/google/android/gms/internal/zzchg;

    invoke-direct {v1, p0, p1}, Lcom/google/android/gms/internal/zzchg;-><init>(Lcom/google/android/gms/internal/zzchf;Lcom/google/android/gms/internal/zzckf;)V

    invoke-virtual {v0, v1}, Lcom/google/android/gms/common/api/internal/zzcj;->zza(Lcom/google/android/gms/common/api/internal/zzcm;)V

    return-void
.end method

.method public final zza(Lcom/google/android/gms/internal/zzckh;)V
    .locals 0

    return-void
.end method
