.class final Lcom/google/android/gms/internal/zzchl;
.super Lcom/google/android/gms/internal/zzchi;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/google/android/gms/internal/zzchi<",
        "Lcom/google/android/gms/nearby/connection/PayloadCallback;",
        ">;"
    }
.end annotation


# instance fields
.field private synthetic zzjbs:Lcom/google/android/gms/internal/zzckh;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzchj;Lcom/google/android/gms/internal/zzckh;)V
    .locals 0

    iput-object p2, p0, Lcom/google/android/gms/internal/zzchl;->zzjbs:Lcom/google/android/gms/internal/zzckh;

    const/4 p1, 0x0

    invoke-direct {p0, p1}, Lcom/google/android/gms/internal/zzchi;-><init>(Lcom/google/android/gms/internal/zzcgq;)V

    return-void
.end method


# virtual methods
.method public final synthetic zzq(Ljava/lang/Object;)V
    .locals 2

    check-cast p1, Lcom/google/android/gms/nearby/connection/PayloadCallback;

    iget-object v0, p0, Lcom/google/android/gms/internal/zzchl;->zzjbs:Lcom/google/android/gms/internal/zzckh;

    invoke-virtual {v0}, Lcom/google/android/gms/internal/zzckh;->zzbaj()Ljava/lang/String;

    move-result-object v0

    iget-object v1, p0, Lcom/google/android/gms/internal/zzchl;->zzjbs:Lcom/google/android/gms/internal/zzckh;

    invoke-virtual {v1}, Lcom/google/android/gms/internal/zzckh;->zzbaq()Lcom/google/android/gms/nearby/connection/PayloadTransferUpdate;

    move-result-object v1

    invoke-virtual {p1, v0, v1}, Lcom/google/android/gms/nearby/connection/PayloadCallback;->onPayloadTransferUpdate(Ljava/lang/String;Lcom/google/android/gms/nearby/connection/PayloadTransferUpdate;)V

    return-void
.end method
