.class final Lcom/google/android/gms/internal/zzbjg;
.super Lcom/google/android/gms/internal/zzbiv;


# instance fields
.field private synthetic zzghm:Lcom/google/android/gms/internal/zzbjc;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzbjc;Lcom/google/android/gms/common/api/GoogleApiClient;)V
    .locals 0

    iput-object p1, p0, Lcom/google/android/gms/internal/zzbjg;->zzghm:Lcom/google/android/gms/internal/zzbjc;

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

    new-instance v0, Lcom/google/android/gms/internal/zzbhn;

    iget-object v1, p0, Lcom/google/android/gms/internal/zzbjg;->zzghm:Lcom/google/android/gms/internal/zzbjc;

    invoke-static {v1}, Lcom/google/android/gms/internal/zzbjc;->zza(Lcom/google/android/gms/internal/zzbjc;)Lcom/google/android/gms/drive/zzc;

    move-result-object v1

    invoke-virtual {v1}, Lcom/google/android/gms/drive/zzc;->getRequestId()I

    move-result v1

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/google/android/gms/internal/zzbhn;-><init>(IZ)V

    new-instance v1, Lcom/google/android/gms/internal/zzbnf;

    invoke-direct {v1, p0}, Lcom/google/android/gms/internal/zzbnf;-><init>(Lcom/google/android/gms/common/api/internal/zzn;)V

    invoke-interface {p1, v0, v1}, Lcom/google/android/gms/internal/zzblb;->zza(Lcom/google/android/gms/internal/zzbhn;Lcom/google/android/gms/internal/zzbld;)V

    return-void
.end method
