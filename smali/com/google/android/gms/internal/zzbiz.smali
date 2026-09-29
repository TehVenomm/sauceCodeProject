.class final Lcom/google/android/gms/internal/zzbiz;
.super Lcom/google/android/gms/internal/zzbiv;


# instance fields
.field private synthetic zzghd:Lcom/google/android/gms/internal/zzbhg;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzbiw;Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/internal/zzbhg;)V
    .locals 0

    iput-object p3, p0, Lcom/google/android/gms/internal/zzbiz;->zzghd:Lcom/google/android/gms/internal/zzbhg;

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

    iget-object v0, p0, Lcom/google/android/gms/internal/zzbiz;->zzghd:Lcom/google/android/gms/internal/zzbhg;

    new-instance v1, Lcom/google/android/gms/internal/zzbnf;

    invoke-direct {v1, p0}, Lcom/google/android/gms/internal/zzbnf;-><init>(Lcom/google/android/gms/common/api/internal/zzn;)V

    const/4 v2, 0x0

    invoke-interface {p1, v0, v2, v2, v1}, Lcom/google/android/gms/internal/zzblb;->zza(Lcom/google/android/gms/internal/zzbhg;Lcom/google/android/gms/internal/zzblf;Ljava/lang/String;Lcom/google/android/gms/internal/zzbld;)V

    return-void
.end method
