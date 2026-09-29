.class final Lcom/google/android/gms/internal/zzbjz;
.super Lcom/google/android/gms/internal/zzbhi;


# instance fields
.field private final zzfwc:Lcom/google/android/gms/common/api/internal/zzn;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/common/api/internal/zzn<",
            "Lcom/google/android/gms/drive/DrivePreferencesApi$FileUploadPreferencesResult;",
            ">;"
        }
    .end annotation
.end field

.field private synthetic zzgic:Lcom/google/android/gms/internal/zzbjw;


# direct methods
.method private constructor <init>(Lcom/google/android/gms/internal/zzbjw;Lcom/google/android/gms/common/api/internal/zzn;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/internal/zzn<",
            "Lcom/google/android/gms/drive/DrivePreferencesApi$FileUploadPreferencesResult;",
            ">;)V"
        }
    .end annotation

    iput-object p1, p0, Lcom/google/android/gms/internal/zzbjz;->zzgic:Lcom/google/android/gms/internal/zzbjw;

    invoke-direct {p0}, Lcom/google/android/gms/internal/zzbhi;-><init>()V

    iput-object p2, p0, Lcom/google/android/gms/internal/zzbjz;->zzfwc:Lcom/google/android/gms/common/api/internal/zzn;

    return-void
.end method

.method synthetic constructor <init>(Lcom/google/android/gms/internal/zzbjw;Lcom/google/android/gms/common/api/internal/zzn;Lcom/google/android/gms/internal/zzbjx;)V
    .locals 0

    invoke-direct {p0, p1, p2}, Lcom/google/android/gms/internal/zzbjz;-><init>(Lcom/google/android/gms/internal/zzbjw;Lcom/google/android/gms/common/api/internal/zzn;)V

    return-void
.end method


# virtual methods
.method public final onError(Lcom/google/android/gms/common/api/Status;)V
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    iget-object v0, p0, Lcom/google/android/gms/internal/zzbjz;->zzfwc:Lcom/google/android/gms/common/api/internal/zzn;

    new-instance v1, Lcom/google/android/gms/internal/zzbka;

    iget-object v2, p0, Lcom/google/android/gms/internal/zzbjz;->zzgic:Lcom/google/android/gms/internal/zzbjw;

    const/4 v3, 0x0

    invoke-direct {v1, v2, p1, v3, v3}, Lcom/google/android/gms/internal/zzbka;-><init>(Lcom/google/android/gms/internal/zzbjw;Lcom/google/android/gms/common/api/Status;Lcom/google/android/gms/drive/FileUploadPreferences;Lcom/google/android/gms/internal/zzbjx;)V

    invoke-interface {v0, v1}, Lcom/google/android/gms/common/api/internal/zzn;->setResult(Ljava/lang/Object;)V

    return-void
.end method

.method public final zza(Lcom/google/android/gms/internal/zzblq;)V
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    iget-object v0, p0, Lcom/google/android/gms/internal/zzbjz;->zzfwc:Lcom/google/android/gms/common/api/internal/zzn;

    new-instance v1, Lcom/google/android/gms/internal/zzbka;

    iget-object v2, p0, Lcom/google/android/gms/internal/zzbjz;->zzgic:Lcom/google/android/gms/internal/zzbjw;

    sget-object v3, Lcom/google/android/gms/common/api/Status;->zzfhp:Lcom/google/android/gms/common/api/Status;

    iget-object p1, p1, Lcom/google/android/gms/internal/zzblq;->zzgiz:Lcom/google/android/gms/internal/zzbkv;

    const/4 v4, 0x0

    invoke-direct {v1, v2, v3, p1, v4}, Lcom/google/android/gms/internal/zzbka;-><init>(Lcom/google/android/gms/internal/zzbjw;Lcom/google/android/gms/common/api/Status;Lcom/google/android/gms/drive/FileUploadPreferences;Lcom/google/android/gms/internal/zzbjx;)V

    invoke-interface {v0, v1}, Lcom/google/android/gms/common/api/internal/zzn;->setResult(Ljava/lang/Object;)V

    return-void
.end method
