.class final Lcom/google/android/gms/internal/zzbkd;
.super Lcom/google/android/gms/internal/zzbkn;


# instance fields
.field private synthetic zzgif:Z

.field private synthetic zzgig:Lcom/google/android/gms/internal/zzbkc;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzbkc;Lcom/google/android/gms/common/api/GoogleApiClient;Z)V
    .locals 0

    iput-object p1, p0, Lcom/google/android/gms/internal/zzbkd;->zzgig:Lcom/google/android/gms/internal/zzbkc;

    const/4 p3, 0x0

    iput-boolean p3, p0, Lcom/google/android/gms/internal/zzbkd;->zzgif:Z

    const/4 p3, 0x0

    invoke-direct {p0, p1, p2, p3}, Lcom/google/android/gms/internal/zzbkn;-><init>(Lcom/google/android/gms/internal/zzbkc;Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/internal/zzbkd;)V

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

    new-instance v0, Lcom/google/android/gms/internal/zzbkx;

    iget-object v1, p0, Lcom/google/android/gms/internal/zzbkd;->zzgig:Lcom/google/android/gms/internal/zzbkc;

    iget-object v1, v1, Lcom/google/android/gms/internal/zzbkc;->zzgcx:Lcom/google/android/gms/drive/DriveId;

    iget-boolean v2, p0, Lcom/google/android/gms/internal/zzbkd;->zzgif:Z

    invoke-direct {v0, v1, v2}, Lcom/google/android/gms/internal/zzbkx;-><init>(Lcom/google/android/gms/drive/DriveId;Z)V

    new-instance v1, Lcom/google/android/gms/internal/zzbkl;

    invoke-direct {v1, p0}, Lcom/google/android/gms/internal/zzbkl;-><init>(Lcom/google/android/gms/common/api/internal/zzn;)V

    invoke-interface {p1, v0, v1}, Lcom/google/android/gms/internal/zzblb;->zza(Lcom/google/android/gms/internal/zzbkx;Lcom/google/android/gms/internal/zzbld;)V

    return-void
.end method
