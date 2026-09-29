.class final Lcom/google/android/gms/auth/api/accounttransfer/zze;
.super Lcom/google/android/gms/auth/api/accounttransfer/AccountTransferClient$zzb;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/google/android/gms/auth/api/accounttransfer/AccountTransferClient$zzb<",
        "[B>;"
    }
.end annotation


# instance fields
.field private synthetic zzdyw:Lcom/google/android/gms/internal/zzary;


# direct methods
.method constructor <init>(Lcom/google/android/gms/auth/api/accounttransfer/AccountTransferClient;Lcom/google/android/gms/internal/zzary;)V
    .locals 0

    iput-object p2, p0, Lcom/google/android/gms/auth/api/accounttransfer/zze;->zzdyw:Lcom/google/android/gms/internal/zzary;

    const/4 p1, 0x0

    invoke-direct {p0, p1}, Lcom/google/android/gms/auth/api/accounttransfer/AccountTransferClient$zzb;-><init>(Lcom/google/android/gms/auth/api/accounttransfer/zzc;)V

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

    new-instance v0, Lcom/google/android/gms/auth/api/accounttransfer/zzf;

    invoke-direct {v0, p0, p0}, Lcom/google/android/gms/auth/api/accounttransfer/zzf;-><init>(Lcom/google/android/gms/auth/api/accounttransfer/zze;Lcom/google/android/gms/auth/api/accounttransfer/AccountTransferClient$zzb;)V

    iget-object v1, p0, Lcom/google/android/gms/auth/api/accounttransfer/zze;->zzdyw:Lcom/google/android/gms/internal/zzary;

    invoke-interface {p1, v0, v1}, Lcom/google/android/gms/internal/zzaru;->zza(Lcom/google/android/gms/internal/zzars;Lcom/google/android/gms/internal/zzary;)V

    return-void
.end method
