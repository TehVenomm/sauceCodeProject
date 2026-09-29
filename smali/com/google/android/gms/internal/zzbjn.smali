.class final Lcom/google/android/gms/internal/zzbjn;
.super Lcom/google/android/gms/internal/zzbjs;


# instance fields
.field private synthetic zzghu:Lcom/google/android/gms/drive/MetadataChangeSet;

.field private synthetic zzghv:I

.field private synthetic zzghw:I

.field private synthetic zzghx:Lcom/google/android/gms/drive/zzm;

.field private synthetic zzghy:Lcom/google/android/gms/internal/zzbjm;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzbjm;Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/drive/MetadataChangeSet;IILcom/google/android/gms/drive/zzm;)V
    .locals 0

    iput-object p1, p0, Lcom/google/android/gms/internal/zzbjn;->zzghy:Lcom/google/android/gms/internal/zzbjm;

    iput-object p3, p0, Lcom/google/android/gms/internal/zzbjn;->zzghu:Lcom/google/android/gms/drive/MetadataChangeSet;

    iput p4, p0, Lcom/google/android/gms/internal/zzbjn;->zzghv:I

    iput p5, p0, Lcom/google/android/gms/internal/zzbjn;->zzghw:I

    iput-object p6, p0, Lcom/google/android/gms/internal/zzbjn;->zzghx:Lcom/google/android/gms/drive/zzm;

    invoke-direct {p0, p2}, Lcom/google/android/gms/internal/zzbjs;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;)V

    return-void
.end method


# virtual methods
.method protected final synthetic zza(Lcom/google/android/gms/common/api/Api$zzb;)V
    .locals 8
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    check-cast p1, Lcom/google/android/gms/internal/zzbiw;

    iget-object v0, p0, Lcom/google/android/gms/internal/zzbjn;->zzghu:Lcom/google/android/gms/drive/MetadataChangeSet;

    invoke-virtual {v0}, Lcom/google/android/gms/drive/MetadataChangeSet;->zzana()Lcom/google/android/gms/drive/metadata/internal/MetadataBundle;

    move-result-object v0

    invoke-virtual {p1}, Lcom/google/android/gms/common/internal/zzd;->getContext()Landroid/content/Context;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/google/android/gms/drive/metadata/internal/MetadataBundle;->setContext(Landroid/content/Context;)V

    new-instance v0, Lcom/google/android/gms/internal/zzbhu;

    iget-object v1, p0, Lcom/google/android/gms/internal/zzbjn;->zzghy:Lcom/google/android/gms/internal/zzbjm;

    invoke-virtual {v1}, Lcom/google/android/gms/internal/zzbkc;->getDriveId()Lcom/google/android/gms/drive/DriveId;

    move-result-object v3

    iget-object v1, p0, Lcom/google/android/gms/internal/zzbjn;->zzghu:Lcom/google/android/gms/drive/MetadataChangeSet;

    invoke-virtual {v1}, Lcom/google/android/gms/drive/MetadataChangeSet;->zzana()Lcom/google/android/gms/drive/metadata/internal/MetadataBundle;

    move-result-object v4

    iget v5, p0, Lcom/google/android/gms/internal/zzbjn;->zzghv:I

    iget v6, p0, Lcom/google/android/gms/internal/zzbjn;->zzghw:I

    iget-object v7, p0, Lcom/google/android/gms/internal/zzbjn;->zzghx:Lcom/google/android/gms/drive/zzm;

    move-object v2, v0

    invoke-direct/range {v2 .. v7}, Lcom/google/android/gms/internal/zzbhu;-><init>(Lcom/google/android/gms/drive/DriveId;Lcom/google/android/gms/drive/metadata/internal/MetadataBundle;IILcom/google/android/gms/drive/zzm;)V

    invoke-virtual {p1}, Lcom/google/android/gms/common/internal/zzd;->zzajj()Landroid/os/IInterface;

    move-result-object p1

    check-cast p1, Lcom/google/android/gms/internal/zzblb;

    new-instance v1, Lcom/google/android/gms/internal/zzbjp;

    invoke-direct {v1, p0}, Lcom/google/android/gms/internal/zzbjp;-><init>(Lcom/google/android/gms/common/api/internal/zzn;)V

    invoke-interface {p1, v0, v1}, Lcom/google/android/gms/internal/zzblb;->zza(Lcom/google/android/gms/internal/zzbhu;Lcom/google/android/gms/internal/zzbld;)V

    return-void
.end method
