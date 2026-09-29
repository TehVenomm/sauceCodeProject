.class final Lcom/google/android/gms/internal/zzbiy;
.super Lcom/google/android/gms/internal/zzbiv;


# instance fields
.field private synthetic zzghe:Lcom/google/android/gms/internal/zzbkr;

.field private synthetic zzghf:Lcom/google/android/gms/internal/zzbmz;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzbiw;Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/internal/zzbmz;Lcom/google/android/gms/internal/zzbkr;)V
    .locals 0

    iput-object p3, p0, Lcom/google/android/gms/internal/zzbiy;->zzghf:Lcom/google/android/gms/internal/zzbmz;

    iput-object p4, p0, Lcom/google/android/gms/internal/zzbiy;->zzghe:Lcom/google/android/gms/internal/zzbkr;

    invoke-direct {p0, p2}, Lcom/google/android/gms/internal/zzbiv;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;)V

    return-void
.end method


# virtual methods
.method protected final synthetic zza(Lcom/google/android/gms/common/api/Api$zzb;)V
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    check-cast p1, Lcom/google/android/gms/internal/zzbiw;

    invoke-virtual {p1}, Lcom/google/android/gms/common/internal/zzd;->zzajj()Landroid/os/IInterface;

    move-result-object p1

    check-cast p1, Lcom/google/android/gms/internal/zzblb;

    iget-object v0, p0, Lcom/google/android/gms/internal/zzbiy;->zzghf:Lcom/google/android/gms/internal/zzbmz;

    iget-object v1, p0, Lcom/google/android/gms/internal/zzbiy;->zzghe:Lcom/google/android/gms/internal/zzbkr;

    new-instance v2, Lcom/google/android/gms/internal/zzbnf;

    invoke-direct {v2, p0}, Lcom/google/android/gms/internal/zzbnf;-><init>(Lcom/google/android/gms/common/api/internal/zzn;)V

    const/4 v3, 0x0

    invoke-interface {p1, v0, v1, v3, v2}, Lcom/google/android/gms/internal/zzblb;->zza(Lcom/google/android/gms/internal/zzbmz;Lcom/google/android/gms/internal/zzblf;Ljava/lang/String;Lcom/google/android/gms/internal/zzbld;)V

    return-void
.end method
