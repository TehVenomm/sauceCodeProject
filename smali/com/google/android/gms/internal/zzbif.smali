.class final Lcom/google/android/gms/internal/zzbif;
.super Lcom/google/android/gms/internal/zzbim;


# instance fields
.field private synthetic zzggo:I


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzbid;Lcom/google/android/gms/common/api/GoogleApiClient;I)V
    .locals 0

    const/high16 p1, 0x20000000

    iput p1, p0, Lcom/google/android/gms/internal/zzbif;->zzggo:I

    invoke-direct {p0, p2}, Lcom/google/android/gms/internal/zzbim;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;)V

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

    new-instance v0, Lcom/google/android/gms/internal/zzbhp;

    iget v1, p0, Lcom/google/android/gms/internal/zzbif;->zzggo:I

    invoke-direct {v0, v1}, Lcom/google/android/gms/internal/zzbhp;-><init>(I)V

    new-instance v1, Lcom/google/android/gms/internal/zzbik;

    invoke-direct {v1, p0}, Lcom/google/android/gms/internal/zzbik;-><init>(Lcom/google/android/gms/common/api/internal/zzn;)V

    invoke-interface {p1, v0, v1}, Lcom/google/android/gms/internal/zzblb;->zza(Lcom/google/android/gms/internal/zzbhp;Lcom/google/android/gms/internal/zzbld;)V

    return-void
.end method
