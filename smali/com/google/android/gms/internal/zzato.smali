.class final Lcom/google/android/gms/internal/zzato;
.super Lcom/google/android/gms/internal/zzatm;


# instance fields
.field private synthetic zzebt:Lcom/google/android/gms/auth/api/proxy/ProxyRequest;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzatn;Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/auth/api/proxy/ProxyRequest;)V
    .locals 0

    iput-object p3, p0, Lcom/google/android/gms/internal/zzato;->zzebt:Lcom/google/android/gms/auth/api/proxy/ProxyRequest;

    invoke-direct {p0, p2}, Lcom/google/android/gms/internal/zzatm;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;)V

    return-void
.end method


# virtual methods
.method protected final zza(Landroid/content/Context;Lcom/google/android/gms/internal/zzatb;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    new-instance p1, Lcom/google/android/gms/internal/zzatp;

    invoke-direct {p1, p0}, Lcom/google/android/gms/internal/zzatp;-><init>(Lcom/google/android/gms/internal/zzato;)V

    iget-object v0, p0, Lcom/google/android/gms/internal/zzato;->zzebt:Lcom/google/android/gms/auth/api/proxy/ProxyRequest;

    invoke-interface {p2, p1, v0}, Lcom/google/android/gms/internal/zzatb;->zza(Lcom/google/android/gms/internal/zzasz;Lcom/google/android/gms/auth/api/proxy/ProxyRequest;)V

    return-void
.end method
