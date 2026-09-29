.class final Lcom/google/android/gms/internal/zzbjx;
.super Lcom/google/android/gms/internal/zzbkb;


# instance fields
.field private synthetic zzgic:Lcom/google/android/gms/internal/zzbjw;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzbjw;Lcom/google/android/gms/common/api/GoogleApiClient;)V
    .locals 0

    iput-object p1, p0, Lcom/google/android/gms/internal/zzbjx;->zzgic:Lcom/google/android/gms/internal/zzbjw;

    invoke-direct {p0, p1, p2}, Lcom/google/android/gms/internal/zzbkb;-><init>(Lcom/google/android/gms/internal/zzbjw;Lcom/google/android/gms/common/api/GoogleApiClient;)V

    return-void
.end method


# virtual methods
.method protected final synthetic zza(Lcom/google/android/gms/common/api/Api$zzb;)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    check-cast p1, Lcom/google/android/gms/internal/zzbiw;

    invoke-virtual {p1}, Lcom/google/android/gms/common/internal/zzd;->zzajj()Landroid/os/IInterface;

    move-result-object p1

    check-cast p1, Lcom/google/android/gms/internal/zzblb;

    new-instance v0, Lcom/google/android/gms/internal/zzbjz;

    iget-object v1, p0, Lcom/google/android/gms/internal/zzbjx;->zzgic:Lcom/google/android/gms/internal/zzbjw;

    const/4 v2, 0x0

    invoke-direct {v0, v1, p0, v2}, Lcom/google/android/gms/internal/zzbjz;-><init>(Lcom/google/android/gms/internal/zzbjw;Lcom/google/android/gms/common/api/internal/zzn;Lcom/google/android/gms/internal/zzbjx;)V

    invoke-interface {p1, v0}, Lcom/google/android/gms/internal/zzblb;->zzb(Lcom/google/android/gms/internal/zzbld;)V

    return-void
.end method
