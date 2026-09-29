.class final Lcom/google/android/gms/internal/zzcig;
.super Lcom/google/android/gms/internal/zzcim;


# instance fields
.field private synthetic zzjbw:Ljava/lang/String;

.field private synthetic zzjcj:Lcom/google/android/gms/common/api/internal/zzcj;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzchp;Lcom/google/android/gms/common/api/GoogleApiClient;Ljava/lang/String;Lcom/google/android/gms/common/api/internal/zzcj;)V
    .locals 0

    iput-object p3, p0, Lcom/google/android/gms/internal/zzcig;->zzjbw:Ljava/lang/String;

    iput-object p4, p0, Lcom/google/android/gms/internal/zzcig;->zzjcj:Lcom/google/android/gms/common/api/internal/zzcj;

    const/4 p1, 0x0

    invoke-direct {p0, p2, p1}, Lcom/google/android/gms/internal/zzcim;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/internal/zzchq;)V

    return-void
.end method


# virtual methods
.method protected final synthetic zza(Lcom/google/android/gms/common/api/Api$zzb;)V
    .locals 7
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    check-cast p1, Lcom/google/android/gms/internal/zzcgp;

    iget-object v3, p0, Lcom/google/android/gms/internal/zzcig;->zzjbw:Ljava/lang/String;

    iget-object v0, p0, Lcom/google/android/gms/internal/zzcig;->zzjcj:Lcom/google/android/gms/common/api/internal/zzcj;

    invoke-virtual {p1}, Lcom/google/android/gms/common/internal/zzd;->zzajj()Landroid/os/IInterface;

    move-result-object p1

    check-cast p1, Lcom/google/android/gms/internal/zzcjg;

    new-instance v6, Lcom/google/android/gms/internal/zzcgl;

    new-instance v1, Lcom/google/android/gms/internal/zzchm;

    invoke-direct {v1, p0}, Lcom/google/android/gms/internal/zzchm;-><init>(Lcom/google/android/gms/common/api/internal/zzn;)V

    invoke-virtual {v1}, Lcom/google/android/gms/internal/zzef;->asBinder()Landroid/os/IBinder;

    move-result-object v1

    new-instance v2, Lcom/google/android/gms/internal/zzchj;

    invoke-direct {v2, v0}, Lcom/google/android/gms/internal/zzchj;-><init>(Lcom/google/android/gms/common/api/internal/zzcj;)V

    invoke-virtual {v2}, Lcom/google/android/gms/internal/zzef;->asBinder()Landroid/os/IBinder;

    move-result-object v5

    const/4 v2, 0x0

    const/4 v4, 0x0

    move-object v0, v6

    invoke-direct/range {v0 .. v5}, Lcom/google/android/gms/internal/zzcgl;-><init>(Landroid/os/IBinder;Landroid/os/IBinder;Ljava/lang/String;[BLandroid/os/IBinder;)V

    invoke-interface {p1, v6}, Lcom/google/android/gms/internal/zzcjg;->zza(Lcom/google/android/gms/internal/zzcgl;)V

    return-void
.end method
