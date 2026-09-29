.class final Lcom/google/android/gms/games/internal/api/zzak;
.super Lcom/google/android/gms/games/internal/api/zzar;


# instance fields
.field private synthetic zzhgy:Z

.field private synthetic zzhhf:Ljava/lang/String;

.field private synthetic zzhhg:I

.field private synthetic zzhhh:I

.field private synthetic zzhhi:I


# direct methods
.method constructor <init>(Lcom/google/android/gms/games/internal/api/zzaf;Lcom/google/android/gms/common/api/GoogleApiClient;Ljava/lang/String;IIIZ)V
    .locals 0

    iput-object p3, p0, Lcom/google/android/gms/games/internal/api/zzak;->zzhhf:Ljava/lang/String;

    iput p4, p0, Lcom/google/android/gms/games/internal/api/zzak;->zzhhg:I

    iput p5, p0, Lcom/google/android/gms/games/internal/api/zzak;->zzhhh:I

    iput p6, p0, Lcom/google/android/gms/games/internal/api/zzak;->zzhhi:I

    iput-boolean p7, p0, Lcom/google/android/gms/games/internal/api/zzak;->zzhgy:Z

    const/4 p1, 0x0

    invoke-direct {p0, p2, p1}, Lcom/google/android/gms/games/internal/api/zzar;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/games/internal/api/zzag;)V

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

    move-object v0, p1

    check-cast v0, Lcom/google/android/gms/games/internal/GamesClientImpl;

    iget-object v2, p0, Lcom/google/android/gms/games/internal/api/zzak;->zzhhf:Ljava/lang/String;

    iget v3, p0, Lcom/google/android/gms/games/internal/api/zzak;->zzhhg:I

    iget v4, p0, Lcom/google/android/gms/games/internal/api/zzak;->zzhhh:I

    iget v5, p0, Lcom/google/android/gms/games/internal/api/zzak;->zzhhi:I

    iget-boolean v6, p0, Lcom/google/android/gms/games/internal/api/zzak;->zzhgy:Z

    move-object v1, p0

    invoke-virtual/range {v0 .. v6}, Lcom/google/android/gms/games/internal/GamesClientImpl;->zzb(Lcom/google/android/gms/common/api/internal/zzn;Ljava/lang/String;IIIZ)V

    return-void
.end method
