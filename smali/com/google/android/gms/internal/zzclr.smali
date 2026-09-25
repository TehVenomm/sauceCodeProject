.class public final Lcom/google/android/gms/internal/zzclr;
.super Lcom/google/android/gms/nearby/messages/internal/zzn;

# interfaces
.implements Lcom/google/android/gms/internal/zzclq;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/google/android/gms/nearby/messages/internal/zzn;",
        "Lcom/google/android/gms/internal/zzclq<",
        "Lcom/google/android/gms/nearby/messages/MessageListener;",
        ">;"
    }
.end annotation


# instance fields
.field private final zzjgz:Lcom/google/android/gms/common/api/internal/zzcj;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/common/api/internal/zzcj<",
            "Lcom/google/android/gms/nearby/messages/MessageListener;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method constructor <init>(Lcom/google/android/gms/common/api/internal/zzcj;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/internal/zzcj<",
            "Lcom/google/android/gms/nearby/messages/MessageListener;",
            ">;)V"
        }
    .end annotation

    invoke-direct {p0}, Lcom/google/android/gms/nearby/messages/internal/zzn;-><init>()V

    iput-object p1, p0, Lcom/google/android/gms/internal/zzclr;->zzjgz:Lcom/google/android/gms/common/api/internal/zzcj;

    return-void
.end method


# virtual methods
.method public final zza(Lcom/google/android/gms/nearby/messages/internal/zzaf;)V
    .locals 0

    return-void
.end method

.method public final zzaf(Ljava/util/List;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/google/android/gms/nearby/messages/internal/Update;",
            ">;)V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    iget-object v0, p0, Lcom/google/android/gms/internal/zzclr;->zzjgz:Lcom/google/android/gms/common/api/internal/zzcj;

    new-instance v1, Lcom/google/android/gms/internal/zzcls;

    invoke-direct {v1, p0, p1}, Lcom/google/android/gms/internal/zzcls;-><init>(Lcom/google/android/gms/internal/zzclr;Ljava/util/List;)V

    invoke-virtual {v0, v1}, Lcom/google/android/gms/common/api/internal/zzcj;->zza(Lcom/google/android/gms/common/api/internal/zzcm;)V

    return-void
.end method

.method public final zzb(Lcom/google/android/gms/nearby/messages/internal/zzaf;)V
    .locals 0

    return-void
.end method

.method public final zzbbb()Lcom/google/android/gms/common/api/internal/zzcj;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Lcom/google/android/gms/common/api/internal/zzcj<",
            "Lcom/google/android/gms/nearby/messages/MessageListener;",
            ">;"
        }
    .end annotation

    iget-object v0, p0, Lcom/google/android/gms/internal/zzclr;->zzjgz:Lcom/google/android/gms/common/api/internal/zzcj;

    return-object v0
.end method
