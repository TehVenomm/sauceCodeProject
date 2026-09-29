.class public final Lcom/google/android/gms/games/multiplayer/realtime/zzd;
.super Lcom/google/android/gms/games/multiplayer/realtime/RoomConfig;


# instance fields
.field private final zzdww:Ljava/lang/String;

.field private final zzhmd:I

.field private final zzhmp:Lcom/google/android/gms/games/multiplayer/realtime/RoomUpdateListener;

.field private final zzhmq:Lcom/google/android/gms/games/multiplayer/realtime/RoomStatusUpdateListener;

.field private final zzhmr:Lcom/google/android/gms/games/multiplayer/realtime/RealTimeMessageReceivedListener;

.field private final zzhmu:Landroid/os/Bundle;

.field private final zzhmv:[Ljava/lang/String;


# direct methods
.method constructor <init>(Lcom/google/android/gms/games/multiplayer/realtime/RoomConfig$Builder;)V
    .locals 1

    invoke-direct {p0}, Lcom/google/android/gms/games/multiplayer/realtime/RoomConfig;-><init>()V

    iget-object v0, p1, Lcom/google/android/gms/games/multiplayer/realtime/RoomConfig$Builder;->zzhmp:Lcom/google/android/gms/games/multiplayer/realtime/RoomUpdateListener;

    iput-object v0, p0, Lcom/google/android/gms/games/multiplayer/realtime/zzd;->zzhmp:Lcom/google/android/gms/games/multiplayer/realtime/RoomUpdateListener;

    iget-object v0, p1, Lcom/google/android/gms/games/multiplayer/realtime/RoomConfig$Builder;->zzhmq:Lcom/google/android/gms/games/multiplayer/realtime/RoomStatusUpdateListener;

    iput-object v0, p0, Lcom/google/android/gms/games/multiplayer/realtime/zzd;->zzhmq:Lcom/google/android/gms/games/multiplayer/realtime/RoomStatusUpdateListener;

    iget-object v0, p1, Lcom/google/android/gms/games/multiplayer/realtime/RoomConfig$Builder;->zzhmr:Lcom/google/android/gms/games/multiplayer/realtime/RealTimeMessageReceivedListener;

    iput-object v0, p0, Lcom/google/android/gms/games/multiplayer/realtime/zzd;->zzhmr:Lcom/google/android/gms/games/multiplayer/realtime/RealTimeMessageReceivedListener;

    iget-object v0, p1, Lcom/google/android/gms/games/multiplayer/realtime/RoomConfig$Builder;->zzhms:Ljava/lang/String;

    iput-object v0, p0, Lcom/google/android/gms/games/multiplayer/realtime/zzd;->zzdww:Ljava/lang/String;

    iget v0, p1, Lcom/google/android/gms/games/multiplayer/realtime/RoomConfig$Builder;->zzhmd:I

    iput v0, p0, Lcom/google/android/gms/games/multiplayer/realtime/zzd;->zzhmd:I

    iget-object v0, p1, Lcom/google/android/gms/games/multiplayer/realtime/RoomConfig$Builder;->zzhmu:Landroid/os/Bundle;

    iput-object v0, p0, Lcom/google/android/gms/games/multiplayer/realtime/zzd;->zzhmu:Landroid/os/Bundle;

    iget-object v0, p1, Lcom/google/android/gms/games/multiplayer/realtime/RoomConfig$Builder;->zzhmt:Ljava/util/ArrayList;

    invoke-virtual {v0}, Ljava/util/ArrayList;->size()I

    move-result v0

    iget-object p1, p1, Lcom/google/android/gms/games/multiplayer/realtime/RoomConfig$Builder;->zzhmt:Ljava/util/ArrayList;

    new-array v0, v0, [Ljava/lang/String;

    invoke-virtual {p1, v0}, Ljava/util/ArrayList;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object p1

    check-cast p1, [Ljava/lang/String;

    iput-object p1, p0, Lcom/google/android/gms/games/multiplayer/realtime/zzd;->zzhmv:[Ljava/lang/String;

    iget-object p1, p0, Lcom/google/android/gms/games/multiplayer/realtime/zzd;->zzhmr:Lcom/google/android/gms/games/multiplayer/realtime/RealTimeMessageReceivedListener;

    const-string v0, "Must specify a message listener"

    invoke-static {p1, v0}, Lcom/google/android/gms/common/internal/zzbp;->zzb(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    return-void
.end method


# virtual methods
.method public final getAutoMatchCriteria()Landroid/os/Bundle;
    .locals 1

    iget-object v0, p0, Lcom/google/android/gms/games/multiplayer/realtime/zzd;->zzhmu:Landroid/os/Bundle;

    return-object v0
.end method

.method public final getInvitationId()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lcom/google/android/gms/games/multiplayer/realtime/zzd;->zzdww:Ljava/lang/String;

    return-object v0
.end method

.method public final getInvitedPlayerIds()[Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lcom/google/android/gms/games/multiplayer/realtime/zzd;->zzhmv:[Ljava/lang/String;

    return-object v0
.end method

.method public final getMessageReceivedListener()Lcom/google/android/gms/games/multiplayer/realtime/RealTimeMessageReceivedListener;
    .locals 1

    iget-object v0, p0, Lcom/google/android/gms/games/multiplayer/realtime/zzd;->zzhmr:Lcom/google/android/gms/games/multiplayer/realtime/RealTimeMessageReceivedListener;

    return-object v0
.end method

.method public final getRoomStatusUpdateListener()Lcom/google/android/gms/games/multiplayer/realtime/RoomStatusUpdateListener;
    .locals 1

    iget-object v0, p0, Lcom/google/android/gms/games/multiplayer/realtime/zzd;->zzhmq:Lcom/google/android/gms/games/multiplayer/realtime/RoomStatusUpdateListener;

    return-object v0
.end method

.method public final getRoomUpdateListener()Lcom/google/android/gms/games/multiplayer/realtime/RoomUpdateListener;
    .locals 1

    iget-object v0, p0, Lcom/google/android/gms/games/multiplayer/realtime/zzd;->zzhmp:Lcom/google/android/gms/games/multiplayer/realtime/RoomUpdateListener;

    return-object v0
.end method

.method public final getVariant()I
    .locals 1

    iget v0, p0, Lcom/google/android/gms/games/multiplayer/realtime/zzd;->zzhmd:I

    return v0
.end method
