.class final Lcom/google/android/gms/internal/zzbjo;
.super Lcom/google/android/gms/internal/zzbju;


# instance fields
.field private synthetic zzghu:Lcom/google/android/gms/drive/MetadataChangeSet;

.field private synthetic zzghy:Lcom/google/android/gms/internal/zzbjm;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzbjm;Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/drive/MetadataChangeSet;)V
    .locals 0

    iput-object p1, p0, Lcom/google/android/gms/internal/zzbjo;->zzghy:Lcom/google/android/gms/internal/zzbjm;

    iput-object p3, p0, Lcom/google/android/gms/internal/zzbjo;->zzghu:Lcom/google/android/gms/drive/MetadataChangeSet;

    invoke-direct {p0, p2}, Lcom/google/android/gms/internal/zzbju;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;)V

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

    iget-object v0, p0, Lcom/google/android/gms/internal/zzbjo;->zzghu:Lcom/google/android/gms/drive/MetadataChangeSet;

    invoke-virtual {v0}, Lcom/google/android/gms/drive/MetadataChangeSet;->zzana()Lcom/google/android/gms/drive/metadata/internal/MetadataBundle;

    move-result-object v0

    invoke-virtual {p1}, Lcom/google/android/gms/common/internal/zzd;->getContext()Landroid/content/Context;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/google/android/gms/drive/metadata/internal/MetadataBundle;->setContext(Landroid/content/Context;)V

    invoke-virtual {p1}, Lcom/google/android/gms/common/internal/zzd;->zzajj()Landroid/os/IInterface;

    move-result-object p1

    check-cast p1, Lcom/google/android/gms/internal/zzblb;

    new-instance v0, Lcom/google/android/gms/internal/zzbhw;

    iget-object v1, p0, Lcom/google/android/gms/internal/zzbjo;->zzghy:Lcom/google/android/gms/internal/zzbjm;

    invoke-virtual {v1}, Lcom/google/android/gms/internal/zzbkc;->getDriveId()Lcom/google/android/gms/drive/DriveId;

    move-result-object v1

    iget-object v2, p0, Lcom/google/android/gms/internal/zzbjo;->zzghu:Lcom/google/android/gms/drive/MetadataChangeSet;

    invoke-virtual {v2}, Lcom/google/android/gms/drive/MetadataChangeSet;->zzana()Lcom/google/android/gms/drive/metadata/internal/MetadataBundle;

    move-result-object v2

    invoke-direct {v0, v1, v2}, Lcom/google/android/gms/internal/zzbhw;-><init>(Lcom/google/android/gms/drive/DriveId;Lcom/google/android/gms/drive/metadata/internal/MetadataBundle;)V

    new-instance v1, Lcom/google/android/gms/internal/zzbjq;

    invoke-direct {v1, p0}, Lcom/google/android/gms/internal/zzbjq;-><init>(Lcom/google/android/gms/common/api/internal/zzn;)V

    invoke-interface {p1, v0, v1}, Lcom/google/android/gms/internal/zzblb;->zza(Lcom/google/android/gms/internal/zzbhw;Lcom/google/android/gms/internal/zzbld;)V

    return-void
.end method
