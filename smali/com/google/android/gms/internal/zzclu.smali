.class final Lcom/google/android/gms/internal/zzclu;
.super Lcom/google/android/gms/internal/zzclv;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/google/android/gms/internal/zzclv<",
        "Lcom/google/android/gms/common/api/internal/zzn<",
        "Lcom/google/android/gms/common/api/Status;",
        ">;>;"
    }
.end annotation


# instance fields
.field private synthetic zzeik:Lcom/google/android/gms/common/api/Status;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzclt;Lcom/google/android/gms/common/api/Status;)V
    .locals 0

    iput-object p2, p0, Lcom/google/android/gms/internal/zzclu;->zzeik:Lcom/google/android/gms/common/api/Status;

    invoke-direct {p0}, Lcom/google/android/gms/internal/zzclv;-><init>()V

    return-void
.end method


# virtual methods
.method public final synthetic zzq(Ljava/lang/Object;)V
    .locals 1

    check-cast p1, Lcom/google/android/gms/common/api/internal/zzn;

    iget-object v0, p0, Lcom/google/android/gms/internal/zzclu;->zzeik:Lcom/google/android/gms/common/api/Status;

    invoke-interface {p1, v0}, Lcom/google/android/gms/common/api/internal/zzn;->setResult(Ljava/lang/Object;)V

    return-void
.end method
