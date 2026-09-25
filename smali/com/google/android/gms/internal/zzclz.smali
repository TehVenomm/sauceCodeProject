.class final Lcom/google/android/gms/internal/zzclz;
.super Lcom/google/android/gms/internal/zzclv;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/google/android/gms/internal/zzclv<",
        "Lcom/google/android/gms/nearby/messages/StatusCallback;",
        ">;"
    }
.end annotation


# instance fields
.field private synthetic zzjhd:Z


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzcly;Z)V
    .locals 0

    iput-boolean p2, p0, Lcom/google/android/gms/internal/zzclz;->zzjhd:Z

    invoke-direct {p0}, Lcom/google/android/gms/internal/zzclv;-><init>()V

    return-void
.end method


# virtual methods
.method public final synthetic zzq(Ljava/lang/Object;)V
    .locals 1

    check-cast p1, Lcom/google/android/gms/nearby/messages/StatusCallback;

    iget-boolean v0, p0, Lcom/google/android/gms/internal/zzclz;->zzjhd:Z

    invoke-virtual {p1, v0}, Lcom/google/android/gms/nearby/messages/StatusCallback;->onPermissionChanged(Z)V

    return-void
.end method
