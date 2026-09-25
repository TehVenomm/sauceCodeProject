.class final Lcom/google/android/gms/internal/zzcid;
.super Lcom/google/android/gms/internal/zzcim;


# instance fields
.field private synthetic zzjbz:Ljava/lang/String;

.field private synthetic zzjch:Lcom/google/android/gms/common/api/internal/zzcj;

.field private synthetic zzjci:Lcom/google/android/gms/nearby/connection/DiscoveryOptions;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzchp;Lcom/google/android/gms/common/api/GoogleApiClient;Ljava/lang/String;Lcom/google/android/gms/common/api/internal/zzcj;Lcom/google/android/gms/nearby/connection/DiscoveryOptions;)V
    .locals 0

    iput-object p3, p0, Lcom/google/android/gms/internal/zzcid;->zzjbz:Ljava/lang/String;

    iput-object p4, p0, Lcom/google/android/gms/internal/zzcid;->zzjch:Lcom/google/android/gms/common/api/internal/zzcj;

    iput-object p5, p0, Lcom/google/android/gms/internal/zzcid;->zzjci:Lcom/google/android/gms/nearby/connection/DiscoveryOptions;

    const/4 p1, 0x0

    invoke-direct {p0, p2, p1}, Lcom/google/android/gms/internal/zzcim;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/internal/zzchq;)V

    return-void
.end method


# virtual methods
.method protected final synthetic zza(Lcom/google/android/gms/common/api/Api$zzb;)V
    .locals 9
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    check-cast p1, Lcom/google/android/gms/internal/zzcgp;

    iget-object v3, p0, Lcom/google/android/gms/internal/zzcid;->zzjbz:Ljava/lang/String;

    iget-object v0, p0, Lcom/google/android/gms/internal/zzcid;->zzjch:Lcom/google/android/gms/common/api/internal/zzcj;

    iget-object v6, p0, Lcom/google/android/gms/internal/zzcid;->zzjci:Lcom/google/android/gms/nearby/connection/DiscoveryOptions;

    invoke-virtual {p1}, Lcom/google/android/gms/common/internal/zzd;->zzajj()Landroid/os/IInterface;

    move-result-object p1

    check-cast p1, Lcom/google/android/gms/internal/zzcjg;

    new-instance v8, Lcom/google/android/gms/internal/zzclc;

    new-instance v1, Lcom/google/android/gms/internal/zzchm;

    invoke-direct {v1, p0}, Lcom/google/android/gms/internal/zzchm;-><init>(Lcom/google/android/gms/common/api/internal/zzn;)V

    invoke-virtual {v1}, Lcom/google/android/gms/internal/zzef;->asBinder()Landroid/os/IBinder;

    move-result-object v1

    new-instance v2, Lcom/google/android/gms/internal/zzcgz;

    invoke-direct {v2, v0}, Lcom/google/android/gms/internal/zzcgz;-><init>(Lcom/google/android/gms/common/api/internal/zzcj;)V

    invoke-virtual {v2}, Lcom/google/android/gms/internal/zzef;->asBinder()Landroid/os/IBinder;

    move-result-object v7

    const/4 v2, 0x0

    const-wide/16 v4, 0x0

    move-object v0, v8

    invoke-direct/range {v0 .. v7}, Lcom/google/android/gms/internal/zzclc;-><init>(Landroid/os/IBinder;Landroid/os/IBinder;Ljava/lang/String;JLcom/google/android/gms/nearby/connection/DiscoveryOptions;Landroid/os/IBinder;)V

    invoke-interface {p1, v8}, Lcom/google/android/gms/internal/zzcjg;->zza(Lcom/google/android/gms/internal/zzclc;)V

    return-void
.end method
