.class final Lcom/google/android/gms/games/internal/api/zzcz;
.super Lcom/google/android/gms/games/internal/api/zzdp;


# instance fields
.field private synthetic zzhif:Ljava/lang/String;

.field private synthetic zzhih:[B

.field private synthetic zzhii:Ljava/lang/String;

.field private synthetic zzhij:[Lcom/google/android/gms/games/multiplayer/ParticipantResult;


# direct methods
.method constructor <init>(Lcom/google/android/gms/games/internal/api/zzcu;Lcom/google/android/gms/common/api/GoogleApiClient;Ljava/lang/String;[BLjava/lang/String;[Lcom/google/android/gms/games/multiplayer/ParticipantResult;)V
    .locals 0

    iput-object p3, p0, Lcom/google/android/gms/games/internal/api/zzcz;->zzhif:Ljava/lang/String;

    iput-object p4, p0, Lcom/google/android/gms/games/internal/api/zzcz;->zzhih:[B

    iput-object p5, p0, Lcom/google/android/gms/games/internal/api/zzcz;->zzhii:Ljava/lang/String;

    iput-object p6, p0, Lcom/google/android/gms/games/internal/api/zzcz;->zzhij:[Lcom/google/android/gms/games/multiplayer/ParticipantResult;

    const/4 p1, 0x0

    invoke-direct {p0, p2, p1}, Lcom/google/android/gms/games/internal/api/zzdp;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/games/internal/api/zzcv;)V

    return-void
.end method


# virtual methods
.method protected final synthetic zza(Lcom/google/android/gms/common/api/Api$zzb;)V
    .locals 6
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    move-object v0, p1

    check-cast v0, Lcom/google/android/gms/games/internal/GamesClientImpl;

    iget-object v2, p0, Lcom/google/android/gms/games/internal/api/zzcz;->zzhif:Ljava/lang/String;

    iget-object v3, p0, Lcom/google/android/gms/games/internal/api/zzcz;->zzhih:[B

    iget-object v4, p0, Lcom/google/android/gms/games/internal/api/zzcz;->zzhii:Ljava/lang/String;

    iget-object v5, p0, Lcom/google/android/gms/games/internal/api/zzcz;->zzhij:[Lcom/google/android/gms/games/multiplayer/ParticipantResult;

    move-object v1, p0

    invoke-virtual/range {v0 .. v5}, Lcom/google/android/gms/games/internal/GamesClientImpl;->zza(Lcom/google/android/gms/common/api/internal/zzn;Ljava/lang/String;[BLjava/lang/String;[Lcom/google/android/gms/games/multiplayer/ParticipantResult;)V

    return-void
.end method
