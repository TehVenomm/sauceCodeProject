.class final Lcom/google/android/gms/internal/zzcic;
.super Lcom/google/android/gms/internal/zzcim;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzchp;Lcom/google/android/gms/common/api/GoogleApiClient;)V
    .locals 0

    const/4 p1, 0x0

    invoke-direct {p0, p2, p1}, Lcom/google/android/gms/internal/zzcim;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/internal/zzchq;)V

    return-void
.end method


# virtual methods
.method protected final synthetic zza(Lcom/google/android/gms/common/api/Api$zzb;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    check-cast p1, Lcom/google/android/gms/internal/zzcgp;

    invoke-virtual {p1}, Lcom/google/android/gms/common/internal/zzd;->zzajj()Landroid/os/IInterface;

    move-result-object p1

    check-cast p1, Lcom/google/android/gms/internal/zzcjg;

    new-instance v0, Lcom/google/android/gms/internal/zzcle;

    invoke-direct {v0}, Lcom/google/android/gms/internal/zzcle;-><init>()V

    invoke-interface {p1, v0}, Lcom/google/android/gms/internal/zzcjg;->zza(Lcom/google/android/gms/internal/zzcle;)V

    return-void
.end method
