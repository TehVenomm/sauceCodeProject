.class final Lcom/google/android/gms/auth/api/accounttransfer/zzj;
.super Lcom/google/android/gms/auth/api/accounttransfer/AccountTransferClient$zzc;


# instance fields
.field private synthetic zzdzb:Lcom/google/android/gms/internal/zzarw;


# direct methods
.method constructor <init>(Lcom/google/android/gms/auth/api/accounttransfer/AccountTransferClient;Lcom/google/android/gms/internal/zzarw;)V
    .locals 0

    iput-object p2, p0, Lcom/google/android/gms/auth/api/accounttransfer/zzj;->zzdzb:Lcom/google/android/gms/internal/zzarw;

    const/4 p1, 0x0

    invoke-direct {p0, p1}, Lcom/google/android/gms/auth/api/accounttransfer/AccountTransferClient$zzc;-><init>(Lcom/google/android/gms/auth/api/accounttransfer/zzc;)V

    return-void
.end method


# virtual methods
.method protected final zza(Lcom/google/android/gms/internal/zzaru;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    iget-object v0, p0, Lcom/google/android/gms/auth/api/accounttransfer/zzj;->zzdze:Lcom/google/android/gms/internal/zzart;

    iget-object v1, p0, Lcom/google/android/gms/auth/api/accounttransfer/zzj;->zzdzb:Lcom/google/android/gms/internal/zzarw;

    invoke-interface {p1, v0, v1}, Lcom/google/android/gms/internal/zzaru;->zza(Lcom/google/android/gms/internal/zzars;Lcom/google/android/gms/internal/zzarw;)V

    return-void
.end method
