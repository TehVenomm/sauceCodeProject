.class final Lcom/google/android/gms/nearby/messages/internal/zzar;
.super Lcom/google/android/gms/nearby/messages/internal/zzav;


# instance fields
.field private synthetic zzhwy:Lcom/google/android/gms/common/api/internal/zzcj;


# direct methods
.method constructor <init>(Lcom/google/android/gms/nearby/messages/internal/zzak;Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/common/api/internal/zzcj;)V
    .locals 0

    iput-object p3, p0, Lcom/google/android/gms/nearby/messages/internal/zzar;->zzhwy:Lcom/google/android/gms/common/api/internal/zzcj;

    invoke-direct {p0, p2}, Lcom/google/android/gms/nearby/messages/internal/zzav;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;)V

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

    check-cast p1, Lcom/google/android/gms/nearby/messages/internal/zzah;

    invoke-virtual {p0}, Lcom/google/android/gms/nearby/messages/internal/zzav;->zzbbb()Lcom/google/android/gms/common/api/internal/zzcj;

    move-result-object v0

    iget-object v1, p0, Lcom/google/android/gms/nearby/messages/internal/zzar;->zzhwy:Lcom/google/android/gms/common/api/internal/zzcj;

    invoke-virtual {p1, v0, v1}, Lcom/google/android/gms/nearby/messages/internal/zzah;->zza(Lcom/google/android/gms/common/api/internal/zzcj;Lcom/google/android/gms/common/api/internal/zzcj;)V

    return-void
.end method
