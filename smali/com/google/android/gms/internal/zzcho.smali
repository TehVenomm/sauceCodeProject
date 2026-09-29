.class final Lcom/google/android/gms/internal/zzcho;
.super Lcom/google/android/gms/internal/zzcjp;


# instance fields
.field private final zzgwg:Lcom/google/android/gms/common/api/internal/zzn;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/common/api/internal/zzn<",
            "Lcom/google/android/gms/nearby/connection/Connections$StartAdvertisingResult;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method constructor <init>(Lcom/google/android/gms/common/api/internal/zzn;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/internal/zzn<",
            "Lcom/google/android/gms/nearby/connection/Connections$StartAdvertisingResult;",
            ">;)V"
        }
    .end annotation

    invoke-direct {p0}, Lcom/google/android/gms/internal/zzcjp;-><init>()V

    invoke-static {p1}, Lcom/google/android/gms/common/internal/zzbp;->zzu(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/google/android/gms/common/api/internal/zzn;

    iput-object p1, p0, Lcom/google/android/gms/internal/zzcho;->zzgwg:Lcom/google/android/gms/common/api/internal/zzn;

    return-void
.end method


# virtual methods
.method public final zza(Lcom/google/android/gms/internal/zzckj;)V
    .locals 4

    iget-object v0, p0, Lcom/google/android/gms/internal/zzcho;->zzgwg:Lcom/google/android/gms/common/api/internal/zzn;

    new-instance v1, Lcom/google/android/gms/internal/zzchn;

    new-instance v2, Lcom/google/android/gms/common/api/Status;

    invoke-virtual {p1}, Lcom/google/android/gms/internal/zzckj;->getStatusCode()I

    move-result v3

    invoke-direct {v2, v3}, Lcom/google/android/gms/common/api/Status;-><init>(I)V

    invoke-virtual {p1}, Lcom/google/android/gms/internal/zzckj;->getLocalEndpointName()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v1, v2, p1}, Lcom/google/android/gms/internal/zzchn;-><init>(Lcom/google/android/gms/common/api/Status;Ljava/lang/String;)V

    invoke-interface {v0, v1}, Lcom/google/android/gms/common/api/internal/zzn;->setResult(Ljava/lang/Object;)V

    return-void
.end method
