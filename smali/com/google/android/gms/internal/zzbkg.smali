.class final Lcom/google/android/gms/internal/zzbkg;
.super Lcom/google/android/gms/internal/zzbkn;


# instance fields
.field private synthetic zzghu:Lcom/google/android/gms/drive/MetadataChangeSet;

.field private synthetic zzgig:Lcom/google/android/gms/internal/zzbkc;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzbkc;Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/drive/MetadataChangeSet;)V
    .locals 0

    iput-object p1, p0, Lcom/google/android/gms/internal/zzbkg;->zzgig:Lcom/google/android/gms/internal/zzbkc;

    iput-object p3, p0, Lcom/google/android/gms/internal/zzbkg;->zzghu:Lcom/google/android/gms/drive/MetadataChangeSet;

    const/4 p3, 0x0

    invoke-direct {p0, p1, p2, p3}, Lcom/google/android/gms/internal/zzbkn;-><init>(Lcom/google/android/gms/internal/zzbkc;Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/internal/zzbkd;)V

    return-void
.end method


# virtual methods
.method protected final synthetic zza(Lcom/google/android/gms/common/api/Api$zzb;)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    check-cast p1, Lcom/google/android/gms/internal/zzbiw;

    iget-object v0, p0, Lcom/google/android/gms/internal/zzbkg;->zzghu:Lcom/google/android/gms/drive/MetadataChangeSet;

    invoke-virtual {v0}, Lcom/google/android/gms/drive/MetadataChangeSet;->zzana()Lcom/google/android/gms/drive/metadata/internal/MetadataBundle;

    move-result-object v0

    invoke-virtual {p1}, Lcom/google/android/gms/common/internal/zzd;->getContext()Landroid/content/Context;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/google/android/gms/drive/metadata/internal/MetadataBundle;->setContext(Landroid/content/Context;)V

    invoke-virtual {p1}, Lcom/google/android/gms/common/internal/zzd;->zzajj()Landroid/os/IInterface;

    move-result-object p1

    check-cast p1, Lcom/google/android/gms/internal/zzblb;

    new-instance v0, Lcom/google/android/gms/internal/zzbnm;

    iget-object v1, p0, Lcom/google/android/gms/internal/zzbkg;->zzgig:Lcom/google/android/gms/internal/zzbkc;

    iget-object v1, v1, Lcom/google/android/gms/internal/zzbkc;->zzgcx:Lcom/google/android/gms/drive/DriveId;

    iget-object v2, p0, Lcom/google/android/gms/internal/zzbkg;->zzghu:Lcom/google/android/gms/drive/MetadataChangeSet;

    invoke-virtual {v2}, Lcom/google/android/gms/drive/MetadataChangeSet;->zzana()Lcom/google/android/gms/drive/metadata/internal/MetadataBundle;

    move-result-object v2

    invoke-direct {v0, v1, v2}, Lcom/google/android/gms/internal/zzbnm;-><init>(Lcom/google/android/gms/drive/DriveId;Lcom/google/android/gms/drive/metadata/internal/MetadataBundle;)V

    new-instance v1, Lcom/google/android/gms/internal/zzbkl;

    invoke-direct {v1, p0}, Lcom/google/android/gms/internal/zzbkl;-><init>(Lcom/google/android/gms/common/api/internal/zzn;)V

    invoke-interface {p1, v0, v1}, Lcom/google/android/gms/internal/zzblb;->zza(Lcom/google/android/gms/internal/zzbnm;Lcom/google/android/gms/internal/zzbld;)V

    return-void
.end method
