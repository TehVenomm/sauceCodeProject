.class final Lcom/google/android/gms/internal/zzbie;
.super Lcom/google/android/gms/internal/zzbir;


# instance fields
.field private synthetic zzggn:Lcom/google/android/gms/drive/query/Query;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzbid;Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/drive/query/Query;)V
    .locals 0

    iput-object p3, p0, Lcom/google/android/gms/internal/zzbie;->zzggn:Lcom/google/android/gms/drive/query/Query;

    invoke-direct {p0, p2}, Lcom/google/android/gms/internal/zzbir;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;)V

    return-void
.end method


# virtual methods
.method protected final synthetic zza(Lcom/google/android/gms/common/api/Api$zzb;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    check-cast p1, Lcom/google/android/gms/internal/zzbiw;

    invoke-virtual {p1}, Lcom/google/android/gms/common/internal/zzd;->zzajj()Landroid/os/IInterface;

    move-result-object p1

    check-cast p1, Lcom/google/android/gms/internal/zzblb;

    new-instance v0, Lcom/google/android/gms/internal/zzbmx;

    iget-object v1, p0, Lcom/google/android/gms/internal/zzbie;->zzggn:Lcom/google/android/gms/drive/query/Query;

    invoke-direct {v0, v1}, Lcom/google/android/gms/internal/zzbmx;-><init>(Lcom/google/android/gms/drive/query/Query;)V

    new-instance v1, Lcom/google/android/gms/internal/zzbis;

    invoke-direct {v1, p0}, Lcom/google/android/gms/internal/zzbis;-><init>(Lcom/google/android/gms/common/api/internal/zzn;)V

    invoke-interface {p1, v0, v1}, Lcom/google/android/gms/internal/zzblb;->zza(Lcom/google/android/gms/internal/zzbmx;Lcom/google/android/gms/internal/zzbld;)V

    return-void
.end method
