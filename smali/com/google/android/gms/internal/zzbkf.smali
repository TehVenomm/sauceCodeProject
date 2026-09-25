.class final Lcom/google/android/gms/internal/zzbkf;
.super Lcom/google/android/gms/internal/zzbiv;


# instance fields
.field private synthetic zzgig:Lcom/google/android/gms/internal/zzbkc;

.field private synthetic zzgih:Ljava/util/List;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzbkc;Lcom/google/android/gms/common/api/GoogleApiClient;Ljava/util/List;)V
    .locals 0

    iput-object p1, p0, Lcom/google/android/gms/internal/zzbkf;->zzgig:Lcom/google/android/gms/internal/zzbkc;

    iput-object p3, p0, Lcom/google/android/gms/internal/zzbkf;->zzgih:Ljava/util/List;

    invoke-direct {p0, p2}, Lcom/google/android/gms/internal/zzbiv;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;)V

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

    new-instance v0, Lcom/google/android/gms/internal/zzbnd;

    iget-object v1, p0, Lcom/google/android/gms/internal/zzbkf;->zzgig:Lcom/google/android/gms/internal/zzbkc;

    iget-object v1, v1, Lcom/google/android/gms/internal/zzbkc;->zzgcx:Lcom/google/android/gms/drive/DriveId;

    iget-object v2, p0, Lcom/google/android/gms/internal/zzbkf;->zzgih:Ljava/util/List;

    invoke-direct {v0, v1, v2}, Lcom/google/android/gms/internal/zzbnd;-><init>(Lcom/google/android/gms/drive/DriveId;Ljava/util/List;)V

    new-instance v1, Lcom/google/android/gms/internal/zzbnf;

    invoke-direct {v1, p0}, Lcom/google/android/gms/internal/zzbnf;-><init>(Lcom/google/android/gms/common/api/internal/zzn;)V

    invoke-interface {p1, v0, v1}, Lcom/google/android/gms/internal/zzblb;->zza(Lcom/google/android/gms/internal/zzbnd;Lcom/google/android/gms/internal/zzbld;)V

    return-void
.end method
