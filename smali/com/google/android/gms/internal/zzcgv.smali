.class final Lcom/google/android/gms/internal/zzcgv;
.super Lcom/google/android/gms/internal/zzciq;


# annotations
.annotation runtime Ljava/lang/Deprecated;
.end annotation


# instance fields
.field private final zzjbg:Lcom/google/android/gms/common/api/internal/zzcj;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/common/api/internal/zzcj<",
            "Lcom/google/android/gms/nearby/connection/Connections$ConnectionRequestListener;",
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
            "Lcom/google/android/gms/nearby/connection/Connections$ConnectionRequestListener;",
            ">;)V"
        }
    .end annotation

    invoke-direct {p0}, Lcom/google/android/gms/internal/zzciq;-><init>()V

    invoke-static {p1}, Lcom/google/android/gms/common/internal/zzbp;->zzu(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/google/android/gms/common/api/internal/zzcj;

    iput-object p1, p0, Lcom/google/android/gms/internal/zzcgv;->zzjbg:Lcom/google/android/gms/common/api/internal/zzcj;

    return-void
.end method


# virtual methods
.method public final zza(Lcom/google/android/gms/internal/zzcjt;)V
    .locals 2

    iget-object v0, p0, Lcom/google/android/gms/internal/zzcgv;->zzjbg:Lcom/google/android/gms/common/api/internal/zzcj;

    new-instance v1, Lcom/google/android/gms/internal/zzcgw;

    invoke-direct {v1, p0, p1}, Lcom/google/android/gms/internal/zzcgw;-><init>(Lcom/google/android/gms/internal/zzcgv;Lcom/google/android/gms/internal/zzcjt;)V

    invoke-virtual {v0, v1}, Lcom/google/android/gms/common/api/internal/zzcj;->zza(Lcom/google/android/gms/common/api/internal/zzcm;)V

    return-void
.end method

.method public final zza(Lcom/google/android/gms/internal/zzckl;)V
    .locals 0

    return-void
.end method
