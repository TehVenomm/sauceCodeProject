.class final Lcom/google/android/gms/internal/zzbke;
.super Lcom/google/android/gms/internal/zzbir;


# instance fields
.field private synthetic zzgig:Lcom/google/android/gms/internal/zzbkc;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzbkc;Lcom/google/android/gms/common/api/GoogleApiClient;)V
    .locals 0

    iput-object p1, p0, Lcom/google/android/gms/internal/zzbke;->zzgig:Lcom/google/android/gms/internal/zzbkc;

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

    new-instance v0, Lcom/google/android/gms/internal/zzblk;

    iget-object v1, p0, Lcom/google/android/gms/internal/zzbke;->zzgig:Lcom/google/android/gms/internal/zzbkc;

    iget-object v1, v1, Lcom/google/android/gms/internal/zzbkc;->zzgcx:Lcom/google/android/gms/drive/DriveId;

    invoke-direct {v0, v1}, Lcom/google/android/gms/internal/zzblk;-><init>(Lcom/google/android/gms/drive/DriveId;)V

    new-instance v1, Lcom/google/android/gms/internal/zzbkk;

    invoke-direct {v1, p0}, Lcom/google/android/gms/internal/zzbkk;-><init>(Lcom/google/android/gms/common/api/internal/zzn;)V

    invoke-interface {p1, v0, v1}, Lcom/google/android/gms/internal/zzblb;->zza(Lcom/google/android/gms/internal/zzblk;Lcom/google/android/gms/internal/zzbld;)V

    return-void
.end method
