.class final Lcom/google/android/gms/internal/zzbje;
.super Lcom/google/android/gms/internal/zzbiv;


# instance fields
.field private synthetic zzghm:Lcom/google/android/gms/internal/zzbjc;

.field private synthetic zzghn:Lcom/google/android/gms/drive/MetadataChangeSet;

.field private synthetic zzgho:Lcom/google/android/gms/drive/zzp;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzbjc;Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/drive/MetadataChangeSet;Lcom/google/android/gms/drive/zzp;)V
    .locals 0

    iput-object p1, p0, Lcom/google/android/gms/internal/zzbje;->zzghm:Lcom/google/android/gms/internal/zzbjc;

    iput-object p3, p0, Lcom/google/android/gms/internal/zzbje;->zzghn:Lcom/google/android/gms/drive/MetadataChangeSet;

    iput-object p4, p0, Lcom/google/android/gms/internal/zzbje;->zzgho:Lcom/google/android/gms/drive/zzp;

    invoke-direct {p0, p2}, Lcom/google/android/gms/internal/zzbiv;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;)V

    return-void
.end method


# virtual methods
.method protected final synthetic zza(Lcom/google/android/gms/common/api/Api$zzb;)V
    .locals 7
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    check-cast p1, Lcom/google/android/gms/internal/zzbiw;

    iget-object v0, p0, Lcom/google/android/gms/internal/zzbje;->zzghn:Lcom/google/android/gms/drive/MetadataChangeSet;

    invoke-virtual {v0}, Lcom/google/android/gms/drive/MetadataChangeSet;->zzana()Lcom/google/android/gms/drive/metadata/internal/MetadataBundle;

    move-result-object v0

    invoke-virtual {p1}, Lcom/google/android/gms/common/internal/zzd;->getContext()Landroid/content/Context;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/google/android/gms/drive/metadata/internal/MetadataBundle;->setContext(Landroid/content/Context;)V

    invoke-virtual {p1}, Lcom/google/android/gms/common/internal/zzd;->zzajj()Landroid/os/IInterface;

    move-result-object p1

    check-cast p1, Lcom/google/android/gms/internal/zzblb;

    new-instance v6, Lcom/google/android/gms/internal/zzbhl;

    iget-object v0, p0, Lcom/google/android/gms/internal/zzbje;->zzghm:Lcom/google/android/gms/internal/zzbjc;

    invoke-static {v0}, Lcom/google/android/gms/internal/zzbjc;->zza(Lcom/google/android/gms/internal/zzbjc;)Lcom/google/android/gms/drive/zzc;

    move-result-object v0

    invoke-virtual {v0}, Lcom/google/android/gms/drive/zzc;->getDriveId()Lcom/google/android/gms/drive/DriveId;

    move-result-object v1

    iget-object v0, p0, Lcom/google/android/gms/internal/zzbje;->zzghn:Lcom/google/android/gms/drive/MetadataChangeSet;

    invoke-virtual {v0}, Lcom/google/android/gms/drive/MetadataChangeSet;->zzana()Lcom/google/android/gms/drive/metadata/internal/MetadataBundle;

    move-result-object v2

    iget-object v0, p0, Lcom/google/android/gms/internal/zzbje;->zzghm:Lcom/google/android/gms/internal/zzbjc;

    invoke-static {v0}, Lcom/google/android/gms/internal/zzbjc;->zza(Lcom/google/android/gms/internal/zzbjc;)Lcom/google/android/gms/drive/zzc;

    move-result-object v0

    invoke-virtual {v0}, Lcom/google/android/gms/drive/zzc;->getRequestId()I

    move-result v3

    iget-object v0, p0, Lcom/google/android/gms/internal/zzbje;->zzghm:Lcom/google/android/gms/internal/zzbjc;

    invoke-static {v0}, Lcom/google/android/gms/internal/zzbjc;->zza(Lcom/google/android/gms/internal/zzbjc;)Lcom/google/android/gms/drive/zzc;

    move-result-object v0

    invoke-virtual {v0}, Lcom/google/android/gms/drive/zzc;->zzamp()Z

    move-result v4

    iget-object v5, p0, Lcom/google/android/gms/internal/zzbje;->zzgho:Lcom/google/android/gms/drive/zzp;

    move-object v0, v6

    invoke-direct/range {v0 .. v5}, Lcom/google/android/gms/internal/zzbhl;-><init>(Lcom/google/android/gms/drive/DriveId;Lcom/google/android/gms/drive/metadata/internal/MetadataBundle;IZLcom/google/android/gms/drive/zzp;)V

    new-instance v0, Lcom/google/android/gms/internal/zzbnf;

    invoke-direct {v0, p0}, Lcom/google/android/gms/internal/zzbnf;-><init>(Lcom/google/android/gms/common/api/internal/zzn;)V

    invoke-interface {p1, v6, v0}, Lcom/google/android/gms/internal/zzblb;->zza(Lcom/google/android/gms/internal/zzbhl;Lcom/google/android/gms/internal/zzbld;)V

    return-void
.end method
