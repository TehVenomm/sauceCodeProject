.class public final Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;
.super Lcom/google/android/gms/games/internal/zzc;

# interfaces
.implements Lcom/google/android/gms/games/internal/experience/ExperienceEvent;


# static fields
.field public static final CREATOR:Landroid/os/Parcelable$Creator;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Landroid/os/Parcelable$Creator<",
            "Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;",
            ">;"
        }
    .end annotation
.end field


# instance fields
.field private final zzeda:I

.field private final zzhbd:Landroid/net/Uri;

.field private final zzhbo:Ljava/lang/String;

.field private final zzhiv:Ljava/lang/String;

.field private final zzhiw:Lcom/google/android/gms/games/GameEntity;

.field private final zzhix:Ljava/lang/String;

.field private final zzhiy:Ljava/lang/String;

.field private final zzhiz:J

.field private final zzhja:J

.field private final zzhjb:J

.field private final zzhjc:I


# direct methods
.method static constructor <clinit>()V
    .locals 1

    new-instance v0, Lcom/google/android/gms/games/internal/experience/zza;

    invoke-direct {v0}, Lcom/google/android/gms/games/internal/experience/zza;-><init>()V

    sput-object v0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->CREATOR:Landroid/os/Parcelable$Creator;

    return-void
.end method

.method constructor <init>(Ljava/lang/String;Lcom/google/android/gms/games/GameEntity;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Landroid/net/Uri;JJJII)V
    .locals 0

    invoke-direct {p0}, Lcom/google/android/gms/games/internal/zzc;-><init>()V

    iput-object p1, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhiv:Ljava/lang/String;

    iput-object p2, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhiw:Lcom/google/android/gms/games/GameEntity;

    iput-object p3, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhix:Ljava/lang/String;

    iput-object p4, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhiy:Ljava/lang/String;

    iput-object p5, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhbo:Ljava/lang/String;

    iput-object p6, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhbd:Landroid/net/Uri;

    iput-wide p7, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhiz:J

    iput-wide p9, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhja:J

    iput-wide p11, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhjb:J

    iput p13, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzeda:I

    iput p14, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhjc:I

    return-void
.end method


# virtual methods
.method public final equals(Ljava/lang/Object;)Z
    .locals 4

    instance-of v0, p1, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;

    if-eqz v0, :cond_1

    const/4 v0, 0x1

    if-ne p0, p1, :cond_0

    return v0

    :cond_0
    check-cast p1, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;

    invoke-interface {p1}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzarb()Ljava/lang/String;

    move-result-object v1

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzarb()Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {p1}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->getGame()Lcom/google/android/gms/games/Game;

    move-result-object v1

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->getGame()Lcom/google/android/gms/games/Game;

    move-result-object v2

    invoke-static {v1, v2}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {p1}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzarc()Ljava/lang/String;

    move-result-object v1

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzarc()Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {p1}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzard()Ljava/lang/String;

    move-result-object v1

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzard()Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {p1}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->getIconImageUrl()Ljava/lang/String;

    move-result-object v1

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->getIconImageUrl()Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {p1}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->getIconImageUri()Landroid/net/Uri;

    move-result-object v1

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->getIconImageUri()Landroid/net/Uri;

    move-result-object v2

    invoke-static {v1, v2}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {p1}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzare()J

    move-result-wide v1

    invoke-static {v1, v2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzare()J

    move-result-wide v2

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-static {v1, v2}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {p1}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzarf()J

    move-result-wide v1

    invoke-static {v1, v2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzarf()J

    move-result-wide v2

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-static {v1, v2}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {p1}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzarg()J

    move-result-wide v1

    invoke-static {v1, v2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzarg()J

    move-result-wide v2

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-static {v1, v2}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {p1}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->getType()I

    move-result v1

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->getType()I

    move-result v2

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-static {v1, v2}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {p1}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzarh()I

    move-result p1

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzarh()I

    move-result v1

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    invoke-static {p1, v1}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_1

    return v0

    :cond_1
    const/4 p1, 0x0

    return p1
.end method

.method public final bridge synthetic freeze()Ljava/lang/Object;
    .locals 0

    return-object p0
.end method

.method public final getGame()Lcom/google/android/gms/games/Game;
    .locals 1

    iget-object v0, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhiw:Lcom/google/android/gms/games/GameEntity;

    return-object v0
.end method

.method public final getIconImageUri()Landroid/net/Uri;
    .locals 1

    iget-object v0, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhbd:Landroid/net/Uri;

    return-object v0
.end method

.method public final getIconImageUrl()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhbo:Ljava/lang/String;

    return-object v0
.end method

.method public final getType()I
    .locals 1

    iget v0, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzeda:I

    return v0
.end method

.method public final hashCode()I
    .locals 3

    const/16 v0, 0xb

    new-array v0, v0, [Ljava/lang/Object;

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzarb()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x0

    aput-object v1, v0, v2

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->getGame()Lcom/google/android/gms/games/Game;

    move-result-object v1

    const/4 v2, 0x1

    aput-object v1, v0, v2

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzarc()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x2

    aput-object v1, v0, v2

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzard()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x3

    aput-object v1, v0, v2

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->getIconImageUrl()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x4

    aput-object v1, v0, v2

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->getIconImageUri()Landroid/net/Uri;

    move-result-object v1

    const/4 v2, 0x5

    aput-object v1, v0, v2

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzare()J

    move-result-wide v1

    invoke-static {v1, v2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1

    const/4 v2, 0x6

    aput-object v1, v0, v2

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzarf()J

    move-result-wide v1

    invoke-static {v1, v2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1

    const/4 v2, 0x7

    aput-object v1, v0, v2

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzarg()J

    move-result-wide v1

    invoke-static {v1, v2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1

    const/16 v2, 0x8

    aput-object v1, v0, v2

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->getType()I

    move-result v1

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    const/16 v2, 0x9

    aput-object v1, v0, v2

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzarh()I

    move-result v1

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    const/16 v2, 0xa

    aput-object v1, v0, v2

    invoke-static {v0}, Ljava/util/Arrays;->hashCode([Ljava/lang/Object;)I

    move-result v0

    return v0
.end method

.method public final isDataValid()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method

.method public final toString()Ljava/lang/String;
    .locals 4

    invoke-static {p0}, Lcom/google/android/gms/common/internal/zzbf;->zzt(Ljava/lang/Object;)Lcom/google/android/gms/common/internal/zzbh;

    move-result-object v0

    const-string v1, "ExperienceId"

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzarb()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Lcom/google/android/gms/common/internal/zzbh;->zzg(Ljava/lang/String;Ljava/lang/Object;)Lcom/google/android/gms/common/internal/zzbh;

    move-result-object v0

    const-string v1, "Game"

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->getGame()Lcom/google/android/gms/games/Game;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Lcom/google/android/gms/common/internal/zzbh;->zzg(Ljava/lang/String;Ljava/lang/Object;)Lcom/google/android/gms/common/internal/zzbh;

    move-result-object v0

    const-string v1, "DisplayTitle"

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzarc()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Lcom/google/android/gms/common/internal/zzbh;->zzg(Ljava/lang/String;Ljava/lang/Object;)Lcom/google/android/gms/common/internal/zzbh;

    move-result-object v0

    const-string v1, "DisplayDescription"

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzard()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Lcom/google/android/gms/common/internal/zzbh;->zzg(Ljava/lang/String;Ljava/lang/Object;)Lcom/google/android/gms/common/internal/zzbh;

    move-result-object v0

    const-string v1, "IconImageUrl"

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->getIconImageUrl()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Lcom/google/android/gms/common/internal/zzbh;->zzg(Ljava/lang/String;Ljava/lang/Object;)Lcom/google/android/gms/common/internal/zzbh;

    move-result-object v0

    const-string v1, "IconImageUri"

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->getIconImageUri()Landroid/net/Uri;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Lcom/google/android/gms/common/internal/zzbh;->zzg(Ljava/lang/String;Ljava/lang/Object;)Lcom/google/android/gms/common/internal/zzbh;

    move-result-object v0

    const-string v1, "CreatedTimestamp"

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzare()J

    move-result-wide v2

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Lcom/google/android/gms/common/internal/zzbh;->zzg(Ljava/lang/String;Ljava/lang/Object;)Lcom/google/android/gms/common/internal/zzbh;

    move-result-object v0

    const-string v1, "XpEarned"

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzarf()J

    move-result-wide v2

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Lcom/google/android/gms/common/internal/zzbh;->zzg(Ljava/lang/String;Ljava/lang/Object;)Lcom/google/android/gms/common/internal/zzbh;

    move-result-object v0

    const-string v1, "CurrentXp"

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzarg()J

    move-result-wide v2

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Lcom/google/android/gms/common/internal/zzbh;->zzg(Ljava/lang/String;Ljava/lang/Object;)Lcom/google/android/gms/common/internal/zzbh;

    move-result-object v0

    const-string v1, "Type"

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->getType()I

    move-result v2

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Lcom/google/android/gms/common/internal/zzbh;->zzg(Ljava/lang/String;Ljava/lang/Object;)Lcom/google/android/gms/common/internal/zzbh;

    move-result-object v0

    const-string v1, "NewLevel"

    invoke-interface {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEvent;->zzarh()I

    move-result v2

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Lcom/google/android/gms/common/internal/zzbh;->zzg(Ljava/lang/String;Ljava/lang/Object;)Lcom/google/android/gms/common/internal/zzbh;

    move-result-object v0

    invoke-virtual {v0}, Lcom/google/android/gms/common/internal/zzbh;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public final writeToParcel(Landroid/os/Parcel;I)V
    .locals 4

    invoke-static {p1}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zze(Landroid/os/Parcel;)I

    move-result v0

    iget-object v1, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhiv:Ljava/lang/String;

    const/4 v2, 0x0

    const/4 v3, 0x1

    invoke-static {p1, v3, v1, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILjava/lang/String;Z)V

    iget-object v1, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhiw:Lcom/google/android/gms/games/GameEntity;

    const/4 v3, 0x2

    invoke-static {p1, v3, v1, p2, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILandroid/os/Parcelable;IZ)V

    iget-object v1, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhix:Ljava/lang/String;

    const/4 v3, 0x3

    invoke-static {p1, v3, v1, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILjava/lang/String;Z)V

    iget-object v1, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhiy:Ljava/lang/String;

    const/4 v3, 0x4

    invoke-static {p1, v3, v1, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILjava/lang/String;Z)V

    invoke-virtual {p0}, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->getIconImageUrl()Ljava/lang/String;

    move-result-object v1

    const/4 v3, 0x5

    invoke-static {p1, v3, v1, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILjava/lang/String;Z)V

    iget-object v1, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhbd:Landroid/net/Uri;

    const/4 v3, 0x6

    invoke-static {p1, v3, v1, p2, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILandroid/os/Parcelable;IZ)V

    iget-wide v1, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhiz:J

    const/4 p2, 0x7

    invoke-static {p1, p2, v1, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;IJ)V

    iget-wide v1, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhja:J

    const/16 p2, 0x8

    invoke-static {p1, p2, v1, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;IJ)V

    iget-wide v1, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhjb:J

    const/16 p2, 0x9

    invoke-static {p1, p2, v1, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;IJ)V

    iget p2, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzeda:I

    const/16 v1, 0xa

    invoke-static {p1, v1, p2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zzc(Landroid/os/Parcel;II)V

    iget p2, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhjc:I

    const/16 v1, 0xb

    invoke-static {p1, v1, p2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zzc(Landroid/os/Parcel;II)V

    invoke-static {p1, v0}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zzai(Landroid/os/Parcel;I)V

    return-void
.end method

.method public final zzarb()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhiv:Ljava/lang/String;

    return-object v0
.end method

.method public final zzarc()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhix:Ljava/lang/String;

    return-object v0
.end method

.method public final zzard()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhiy:Ljava/lang/String;

    return-object v0
.end method

.method public final zzare()J
    .locals 2

    iget-wide v0, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhiz:J

    return-wide v0
.end method

.method public final zzarf()J
    .locals 2

    iget-wide v0, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhja:J

    return-wide v0
.end method

.method public final zzarg()J
    .locals 2

    iget-wide v0, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhjb:J

    return-wide v0
.end method

.method public final zzarh()I
    .locals 1

    iget v0, p0, Lcom/google/android/gms/games/internal/experience/ExperienceEventEntity;->zzhjc:I

    return v0
.end method
