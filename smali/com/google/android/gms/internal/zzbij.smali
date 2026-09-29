.class final Lcom/google/android/gms/internal/zzbij;
.super Lcom/google/android/gms/internal/zzbhi;


# instance fields
.field private synthetic zzggq:Lcom/google/android/gms/internal/zzbiu;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzbii;Lcom/google/android/gms/internal/zzbiu;)V
    .locals 0

    iput-object p2, p0, Lcom/google/android/gms/internal/zzbij;->zzggq:Lcom/google/android/gms/internal/zzbiu;

    invoke-direct {p0}, Lcom/google/android/gms/internal/zzbhi;-><init>()V

    return-void
.end method


# virtual methods
.method public final zzbi(Z)V
    .locals 3

    iget-object v0, p0, Lcom/google/android/gms/internal/zzbij;->zzggq:Lcom/google/android/gms/internal/zzbiu;

    new-instance v1, Lcom/google/android/gms/common/api/BooleanResult;

    sget-object v2, Lcom/google/android/gms/common/api/Status;->zzfhp:Lcom/google/android/gms/common/api/Status;

    invoke-direct {v1, v2, p1}, Lcom/google/android/gms/common/api/BooleanResult;-><init>(Lcom/google/android/gms/common/api/Status;Z)V

    invoke-virtual {v0, v1}, Lcom/google/android/gms/common/api/internal/zzs;->setResult(Lcom/google/android/gms/common/api/Result;)V

    return-void
.end method
