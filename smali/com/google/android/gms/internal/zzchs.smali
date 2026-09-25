.class final Lcom/google/android/gms/internal/zzchs;
.super Lcom/google/android/gms/internal/zzcim;


# instance fields
.field private synthetic zzjbw:Ljava/lang/String;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzchp;Lcom/google/android/gms/common/api/GoogleApiClient;Ljava/lang/String;)V
    .locals 0

    iput-object p3, p0, Lcom/google/android/gms/internal/zzchs;->zzjbw:Ljava/lang/String;

    const/4 p1, 0x0

    invoke-direct {p0, p2, p1}, Lcom/google/android/gms/internal/zzcim;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/internal/zzchq;)V

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

    check-cast p1, Lcom/google/android/gms/internal/zzcgp;

    iget-object v0, p0, Lcom/google/android/gms/internal/zzchs;->zzjbw:Ljava/lang/String;

    invoke-virtual {p1}, Lcom/google/android/gms/common/internal/zzd;->zzajj()Landroid/os/IInterface;

    move-result-object p1

    check-cast p1, Lcom/google/android/gms/internal/zzcjg;

    new-instance v1, Lcom/google/android/gms/internal/zzcin;

    invoke-direct {v1, v0}, Lcom/google/android/gms/internal/zzcin;-><init>(Ljava/lang/String;)V

    invoke-interface {p1, v1}, Lcom/google/android/gms/internal/zzcjg;->zza(Lcom/google/android/gms/internal/zzcin;)V

    return-void
.end method
