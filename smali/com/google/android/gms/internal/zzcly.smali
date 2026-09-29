.class public final Lcom/google/android/gms/internal/zzcly;
.super Lcom/google/android/gms/nearby/messages/internal/zzy;

# interfaces
.implements Lcom/google/android/gms/internal/zzclq;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/google/android/gms/nearby/messages/internal/zzy;",
        "Lcom/google/android/gms/internal/zzclq<",
        "Lcom/google/android/gms/nearby/messages/StatusCallback;",
        ">;"
    }
.end annotation


# instance fields
.field private final zzjgz:Lcom/google/android/gms/common/api/internal/zzcj;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/common/api/internal/zzcj<",
            "Lcom/google/android/gms/nearby/messages/StatusCallback;",
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
            "Lcom/google/android/gms/nearby/messages/StatusCallback;",
            ">;)V"
        }
    .end annotation

    invoke-direct {p0}, Lcom/google/android/gms/nearby/messages/internal/zzy;-><init>()V

    iput-object p1, p0, Lcom/google/android/gms/internal/zzcly;->zzjgz:Lcom/google/android/gms/common/api/internal/zzcj;

    return-void
.end method


# virtual methods
.method public final onPermissionChanged(Z)V
    .locals 2

    iget-object v0, p0, Lcom/google/android/gms/internal/zzcly;->zzjgz:Lcom/google/android/gms/common/api/internal/zzcj;

    new-instance v1, Lcom/google/android/gms/internal/zzclz;

    invoke-direct {v1, p0, p1}, Lcom/google/android/gms/internal/zzclz;-><init>(Lcom/google/android/gms/internal/zzcly;Z)V

    invoke-virtual {v0, v1}, Lcom/google/android/gms/common/api/internal/zzcj;->zza(Lcom/google/android/gms/common/api/internal/zzcm;)V

    return-void
.end method

.method public final zzbbb()Lcom/google/android/gms/common/api/internal/zzcj;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Lcom/google/android/gms/common/api/internal/zzcj<",
            "Lcom/google/android/gms/nearby/messages/StatusCallback;",
            ">;"
        }
    .end annotation

    iget-object v0, p0, Lcom/google/android/gms/internal/zzcly;->zzjgz:Lcom/google/android/gms/common/api/internal/zzcj;

    return-object v0
.end method
