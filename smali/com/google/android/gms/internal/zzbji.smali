.class final Lcom/google/android/gms/internal/zzbji;
.super Lcom/google/android/gms/internal/zzbim;


# instance fields
.field private synthetic zzggo:I

.field private synthetic zzghp:Lcom/google/android/gms/drive/DriveFile$DownloadProgressListener;

.field private synthetic zzghq:Lcom/google/android/gms/internal/zzbjh;


# direct methods
.method constructor <init>(Lcom/google/android/gms/internal/zzbjh;Lcom/google/android/gms/common/api/GoogleApiClient;ILcom/google/android/gms/drive/DriveFile$DownloadProgressListener;)V
    .locals 0

    iput-object p1, p0, Lcom/google/android/gms/internal/zzbji;->zzghq:Lcom/google/android/gms/internal/zzbjh;

    iput p3, p0, Lcom/google/android/gms/internal/zzbji;->zzggo:I

    iput-object p4, p0, Lcom/google/android/gms/internal/zzbji;->zzghp:Lcom/google/android/gms/drive/DriveFile$DownloadProgressListener;

    invoke-direct {p0, p2}, Lcom/google/android/gms/internal/zzbim;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;)V

    return-void
.end method


# virtual methods
.method protected final synthetic zza(Lcom/google/android/gms/common/api/Api$zzb;)V
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    check-cast p1, Lcom/google/android/gms/internal/zzbiw;

    invoke-virtual {p1}, Lcom/google/android/gms/common/internal/zzd;->zzajj()Landroid/os/IInterface;

    move-result-object p1

    check-cast p1, Lcom/google/android/gms/internal/zzblb;

    new-instance v0, Lcom/google/android/gms/internal/zzbmq;

    iget-object v1, p0, Lcom/google/android/gms/internal/zzbji;->zzghq:Lcom/google/android/gms/internal/zzbjh;

    invoke-virtual {v1}, Lcom/google/android/gms/internal/zzbkc;->getDriveId()Lcom/google/android/gms/drive/DriveId;

    move-result-object v1

    iget v2, p0, Lcom/google/android/gms/internal/zzbji;->zzggo:I

    const/4 v3, 0x0

    invoke-direct {v0, v1, v2, v3}, Lcom/google/android/gms/internal/zzbmq;-><init>(Lcom/google/android/gms/drive/DriveId;II)V

    new-instance v1, Lcom/google/android/gms/internal/zzbms;

    iget-object v2, p0, Lcom/google/android/gms/internal/zzbji;->zzghp:Lcom/google/android/gms/drive/DriveFile$DownloadProgressListener;

    invoke-direct {v1, p0, v2}, Lcom/google/android/gms/internal/zzbms;-><init>(Lcom/google/android/gms/common/api/internal/zzn;Lcom/google/android/gms/drive/DriveFile$DownloadProgressListener;)V

    invoke-interface {p1, v0, v1}, Lcom/google/android/gms/internal/zzblb;->zza(Lcom/google/android/gms/internal/zzbmq;Lcom/google/android/gms/internal/zzbld;)Lcom/google/android/gms/internal/zzbkp;

    move-result-object p1

    iget-object p1, p1, Lcom/google/android/gms/internal/zzbkp;->zzgij:Landroid/os/IBinder;

    invoke-static {p1}, Lcom/google/android/gms/common/internal/zzaq;->zzaj(Landroid/os/IBinder;)Lcom/google/android/gms/common/internal/zzap;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/google/android/gms/common/api/internal/zzs;->zza(Lcom/google/android/gms/common/internal/zzap;)V

    return-void
.end method
