.class final Lcom/google/android/gms/internal/zzchu;
.super Lcom/google/android/gms/internal/zzcik;


# instance fields
.field private synthetic val$name:Ljava/lang/String;

.field private synthetic zzjbx:J

.field private synthetic zzjby:Lcom/google/android/gms/common/api/internal/zzcj;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzchp;Lcom/google/android/gms/common/api/GoogleApiClient;Ljava/lang/String;JLcom/google/android/gms/common/api/internal/zzcj;)V
    .locals 0

    iput-object p3, p0, Lcom/google/android/gms/internal/zzchu;->val$name:Ljava/lang/String;

    iput-wide p4, p0, Lcom/google/android/gms/internal/zzchu;->zzjbx:J

    iput-object p6, p0, Lcom/google/android/gms/internal/zzchu;->zzjby:Lcom/google/android/gms/common/api/internal/zzcj;

    const/4 p1, 0x0

    invoke-direct {p0, p2, p1}, Lcom/google/android/gms/internal/zzcik;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/internal/zzchq;)V

    return-void
.end method


# virtual methods
.method protected final synthetic zza(Lcom/google/android/gms/common/api/Api$zzb;)V
    .locals 10
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    check-cast p1, Lcom/google/android/gms/internal/zzcgp;

    iget-object v3, p0, Lcom/google/android/gms/internal/zzchu;->val$name:Ljava/lang/String;

    const-string v4, "__LEGACY_SERVICE_ID__"

    iget-wide v5, p0, Lcom/google/android/gms/internal/zzchu;->zzjbx:J

    iget-object v0, p0, Lcom/google/android/gms/internal/zzchu;->zzjby:Lcom/google/android/gms/common/api/internal/zzcj;

    new-instance v7, Lcom/google/android/gms/nearby/connection/AdvertisingOptions;

    sget-object v1, Lcom/google/android/gms/nearby/connection/Strategy;->P2P_CLUSTER:Lcom/google/android/gms/nearby/connection/Strategy;

    invoke-direct {v7, v1}, Lcom/google/android/gms/nearby/connection/AdvertisingOptions;-><init>(Lcom/google/android/gms/nearby/connection/Strategy;)V

    invoke-virtual {p1}, Lcom/google/android/gms/common/internal/zzd;->zzajj()Landroid/os/IInterface;

    move-result-object p1

    check-cast p1, Lcom/google/android/gms/internal/zzcjg;

    new-instance v9, Lcom/google/android/gms/internal/zzcla;

    new-instance v1, Lcom/google/android/gms/internal/zzcho;

    invoke-direct {v1, p0}, Lcom/google/android/gms/internal/zzcho;-><init>(Lcom/google/android/gms/common/api/internal/zzn;)V

    invoke-virtual {v1}, Lcom/google/android/gms/internal/zzef;->asBinder()Landroid/os/IBinder;

    move-result-object v1

    new-instance v2, Lcom/google/android/gms/internal/zzcgv;

    invoke-direct {v2, v0}, Lcom/google/android/gms/internal/zzcgv;-><init>(Lcom/google/android/gms/common/api/internal/zzcj;)V

    invoke-virtual {v2}, Lcom/google/android/gms/internal/zzef;->asBinder()Landroid/os/IBinder;

    move-result-object v2

    const/4 v8, 0x0

    move-object v0, v9

    invoke-direct/range {v0 .. v8}, Lcom/google/android/gms/internal/zzcla;-><init>(Landroid/os/IBinder;Landroid/os/IBinder;Ljava/lang/String;Ljava/lang/String;JLcom/google/android/gms/nearby/connection/AdvertisingOptions;Landroid/os/IBinder;)V

    invoke-interface {p1, v9}, Lcom/google/android/gms/internal/zzcjg;->zza(Lcom/google/android/gms/internal/zzcla;)V

    return-void
.end method
