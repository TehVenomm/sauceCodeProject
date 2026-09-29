.class final Lcom/google/android/gms/internal/zzatp;
.super Lcom/google/android/gms/internal/zzasx;


# instance fields
.field private synthetic zzebu:Lcom/google/android/gms/internal/zzato;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzato;)V
    .locals 0

    iput-object p1, p0, Lcom/google/android/gms/internal/zzatp;->zzebu:Lcom/google/android/gms/internal/zzato;

    invoke-direct {p0}, Lcom/google/android/gms/internal/zzasx;-><init>()V

    return-void
.end method


# virtual methods
.method public final zza(Lcom/google/android/gms/auth/api/proxy/ProxyResponse;)V
    .locals 2

    iget-object v0, p0, Lcom/google/android/gms/internal/zzatp;->zzebu:Lcom/google/android/gms/internal/zzato;

    new-instance v1, Lcom/google/android/gms/internal/zzatq;

    invoke-direct {v1, p1}, Lcom/google/android/gms/internal/zzatq;-><init>(Lcom/google/android/gms/auth/api/proxy/ProxyResponse;)V

    invoke-virtual {v0, v1}, Lcom/google/android/gms/common/api/internal/zzs;->setResult(Lcom/google/android/gms/common/api/Result;)V

    return-void
.end method
