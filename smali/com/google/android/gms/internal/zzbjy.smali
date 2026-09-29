.class final Lcom/google/android/gms/internal/zzbjy;
.super Lcom/google/android/gms/internal/zzbiv;


# instance fields
.field private synthetic zzgid:Lcom/google/android/gms/internal/zzbkv;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzbjw;Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/internal/zzbkv;)V
    .locals 0

    iput-object p3, p0, Lcom/google/android/gms/internal/zzbjy;->zzgid:Lcom/google/android/gms/internal/zzbkv;

    invoke-direct {p0, p2}, Lcom/google/android/gms/internal/zzbiv;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;)V

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

    new-instance v0, Lcom/google/android/gms/internal/zzbnb;

    iget-object v1, p0, Lcom/google/android/gms/internal/zzbjy;->zzgid:Lcom/google/android/gms/internal/zzbkv;

    invoke-direct {v0, v1}, Lcom/google/android/gms/internal/zzbnb;-><init>(Lcom/google/android/gms/internal/zzbkv;)V

    new-instance v1, Lcom/google/android/gms/internal/zzbnf;

    invoke-direct {v1, p0}, Lcom/google/android/gms/internal/zzbnf;-><init>(Lcom/google/android/gms/common/api/internal/zzn;)V

    invoke-interface {p1, v0, v1}, Lcom/google/android/gms/internal/zzblb;->zza(Lcom/google/android/gms/internal/zzbnb;Lcom/google/android/gms/internal/zzbld;)V

    return-void
.end method
