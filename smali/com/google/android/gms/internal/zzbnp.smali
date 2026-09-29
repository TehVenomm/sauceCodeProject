.class public final Lcom/google/android/gms/internal/zzbnp;
.super Lcom/google/android/gms/internal/zzegi;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/google/android/gms/internal/zzegi<",
        "Lcom/google/android/gms/internal/zzbnp;",
        ">;"
    }
.end annotation


# instance fields
.field public versionCode:I

.field public zzgjz:J

.field public zzgkb:Ljava/lang/String;

.field public zzgkc:J

.field public zzgkd:I


# direct methods
.method public constructor <init>()V
    .locals 2

    invoke-direct {p0}, Lcom/google/android/gms/internal/zzegi;-><init>()V

    const/4 v0, 0x1

    iput v0, p0, Lcom/google/android/gms/internal/zzbnp;->versionCode:I

    const-string v0, ""

    iput-object v0, p0, Lcom/google/android/gms/internal/zzbnp;->zzgkb:Ljava/lang/String;

    const-wide/16 v0, -0x1

    iput-wide v0, p0, Lcom/google/android/gms/internal/zzbnp;->zzgkc:J

    iput-wide v0, p0, Lcom/google/android/gms/internal/zzbnp;->zzgjz:J

    const/4 v0, -0x1

    iput v0, p0, Lcom/google/android/gms/internal/zzbnp;->zzgkd:I

    const/4 v1, 0x0

    iput-object v1, p0, Lcom/google/android/gms/internal/zzbnp;->zzncu:Lcom/google/android/gms/internal/zzegk;

    iput v0, p0, Lcom/google/android/gms/internal/zzbnp;->zzndd:I

    return-void
.end method


# virtual methods
.method public final equals(Ljava/lang/Object;)Z
    .locals 7

    const/4 v0, 0x1

    if-ne p1, p0, :cond_0

    return v0

    :cond_0
    instance-of v1, p1, Lcom/google/android/gms/internal/zzbnp;

    const/4 v2, 0x0

    if-nez v1, :cond_1

    return v2

    :cond_1
    check-cast p1, Lcom/google/android/gms/internal/zzbnp;

    iget v1, p0, Lcom/google/android/gms/internal/zzbnp;->versionCode:I

    iget v3, p1, Lcom/google/android/gms/internal/zzbnp;->versionCode:I

    if-eq v1, v3, :cond_2

    return v2

    :cond_2
    iget-object v1, p0, Lcom/google/android/gms/internal/zzbnp;->zzgkb:Ljava/lang/String;

    if-nez v1, :cond_3

    iget-object v1, p1, Lcom/google/android/gms/internal/zzbnp;->zzgkb:Ljava/lang/String;

    if-eqz v1, :cond_4

    return v2

    :cond_3
    iget-object v1, p0, Lcom/google/android/gms/internal/zzbnp;->zzgkb:Ljava/lang/String;

    iget-object v3, p1, Lcom/google/android/gms/internal/zzbnp;->zzgkb:Ljava/lang/String;

    invoke-virtual {v1, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-nez v1, :cond_4

    return v2

    :cond_4
    iget-wide v3, p0, Lcom/google/android/gms/internal/zzbnp;->zzgkc:J

    iget-wide v5, p1, Lcom/google/android/gms/internal/zzbnp;->zzgkc:J

    cmp-long v1, v3, v5

    if-eqz v1, :cond_5

    return v2

    :cond_5
    iget-wide v3, p0, Lcom/google/android/gms/internal/zzbnp;->zzgjz:J

    iget-wide v5, p1, Lcom/google/android/gms/internal/zzbnp;->zzgjz:J

    cmp-long v1, v3, v5

    if-eqz v1, :cond_6

    return v2

    :cond_6
    iget v1, p0, Lcom/google/android/gms/internal/zzbnp;->zzgkd:I

    iget v3, p1, Lcom/google/android/gms/internal/zzbnp;->zzgkd:I

    if-eq v1, v3, :cond_7

    return v2

    :cond_7
    iget-object v1, p0, Lcom/google/android/gms/internal/zzbnp;->zzncu:Lcom/google/android/gms/internal/zzegk;

    if-eqz v1, :cond_9

    iget-object v1, p0, Lcom/google/android/gms/internal/zzbnp;->zzncu:Lcom/google/android/gms/internal/zzegk;

    invoke-virtual {v1}, Lcom/google/android/gms/internal/zzegk;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_8

    goto :goto_0

    :cond_8
    iget-object v0, p0, Lcom/google/android/gms/internal/zzbnp;->zzncu:Lcom/google/android/gms/internal/zzegk;

    iget-object p1, p1, Lcom/google/android/gms/internal/zzbnp;->zzncu:Lcom/google/android/gms/internal/zzegk;

    invoke-virtual {v0, p1}, Lcom/google/android/gms/internal/zzegk;->equals(Ljava/lang/Object;)Z

    move-result p1

    return p1

    :cond_9
    :goto_0
    iget-object v1, p1, Lcom/google/android/gms/internal/zzbnp;->zzncu:Lcom/google/android/gms/internal/zzegk;

    if-eqz v1, :cond_b

    iget-object p1, p1, Lcom/google/android/gms/internal/zzbnp;->zzncu:Lcom/google/android/gms/internal/zzegk;

    invoke-virtual {p1}, Lcom/google/android/gms/internal/zzegk;->isEmpty()Z

    move-result p1

    if-eqz p1, :cond_a

    goto :goto_1

    :cond_a
    return v2

    :cond_b
    :goto_1
    return v0
.end method

.method public final hashCode()I
    .locals 7

    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/String;->hashCode()I

    move-result v0

    add-int/lit16 v0, v0, 0x20f

    mul-int/lit8 v0, v0, 0x1f

    iget v1, p0, Lcom/google/android/gms/internal/zzbnp;->versionCode:I

    add-int/2addr v0, v1

    mul-int/lit8 v0, v0, 0x1f

    iget-object v1, p0, Lcom/google/android/gms/internal/zzbnp;->zzgkb:Ljava/lang/String;

    const/4 v2, 0x0

    if-nez v1, :cond_0

    const/4 v1, 0x0

    goto :goto_0

    :cond_0
    iget-object v1, p0, Lcom/google/android/gms/internal/zzbnp;->zzgkb:Ljava/lang/String;

    invoke-virtual {v1}, Ljava/lang/String;->hashCode()I

    move-result v1

    :goto_0
    add-int/2addr v0, v1

    mul-int/lit8 v0, v0, 0x1f

    iget-wide v3, p0, Lcom/google/android/gms/internal/zzbnp;->zzgkc:J

    iget-wide v5, p0, Lcom/google/android/gms/internal/zzbnp;->zzgkc:J

    const/16 v1, 0x20

    ushr-long/2addr v5, v1

    xor-long/2addr v3, v5

    long-to-int v3, v3

    add-int/2addr v0, v3

    mul-int/lit8 v0, v0, 0x1f

    iget-wide v3, p0, Lcom/google/android/gms/internal/zzbnp;->zzgjz:J

    iget-wide v5, p0, Lcom/google/android/gms/internal/zzbnp;->zzgjz:J

    ushr-long/2addr v5, v1

    xor-long/2addr v3, v5

    long-to-int v1, v3

    add-int/2addr v0, v1

    mul-int/lit8 v0, v0, 0x1f

    iget v1, p0, Lcom/google/android/gms/internal/zzbnp;->zzgkd:I

    add-int/2addr v0, v1

    mul-int/lit8 v0, v0, 0x1f

    iget-object v1, p0, Lcom/google/android/gms/internal/zzbnp;->zzncu:Lcom/google/android/gms/internal/zzegk;

    if-eqz v1, :cond_2

    iget-object v1, p0, Lcom/google/android/gms/internal/zzbnp;->zzncu:Lcom/google/android/gms/internal/zzegk;

    invoke-virtual {v1}, Lcom/google/android/gms/internal/zzegk;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_1

    goto :goto_1

    :cond_1
    iget-object v1, p0, Lcom/google/android/gms/internal/zzbnp;->zzncu:Lcom/google/android/gms/internal/zzegk;

    invoke-virtual {v1}, Lcom/google/android/gms/internal/zzegk;->hashCode()I

    move-result v2

    :cond_2
    :goto_1
    add-int/2addr v0, v2

    return v0
.end method

.method public final synthetic zza(Lcom/google/android/gms/internal/zzegf;)Lcom/google/android/gms/internal/zzego;
    .locals 6
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    :cond_0
    :goto_0
    invoke-virtual {p1}, Lcom/google/android/gms/internal/zzegf;->zzcbr()I

    move-result v0

    if-eqz v0, :cond_6

    const/16 v1, 0x8

    if-eq v0, v1, :cond_5

    const/16 v1, 0x12

    if-eq v0, v1, :cond_4

    const/16 v1, 0x18

    const-wide/16 v2, 0x1

    const/4 v4, 0x1

    if-eq v0, v1, :cond_3

    const/16 v1, 0x20

    if-eq v0, v1, :cond_2

    const/16 v1, 0x28

    if-eq v0, v1, :cond_1

    invoke-super {p0, p1, v0}, Lcom/google/android/gms/internal/zzegi;->zza(Lcom/google/android/gms/internal/zzegf;I)Z

    move-result v0

    if-nez v0, :cond_0

    return-object p0

    :cond_1
    invoke-virtual {p1}, Lcom/google/android/gms/internal/zzegf;->zzcbz()I

    move-result v0

    iput v0, p0, Lcom/google/android/gms/internal/zzbnp;->zzgkd:I

    goto :goto_0

    :cond_2
    invoke-virtual {p1}, Lcom/google/android/gms/internal/zzegf;->zzcdu()J

    move-result-wide v0

    ushr-long v4, v0, v4

    and-long/2addr v0, v2

    neg-long v0, v0

    xor-long/2addr v0, v4

    iput-wide v0, p0, Lcom/google/android/gms/internal/zzbnp;->zzgjz:J

    goto :goto_0

    :cond_3
    invoke-virtual {p1}, Lcom/google/android/gms/internal/zzegf;->zzcdu()J

    move-result-wide v0

    ushr-long v4, v0, v4

    and-long/2addr v0, v2

    neg-long v0, v0

    xor-long/2addr v0, v4

    iput-wide v0, p0, Lcom/google/android/gms/internal/zzbnp;->zzgkc:J

    goto :goto_0

    :cond_4
    invoke-virtual {p1}, Lcom/google/android/gms/internal/zzegf;->readString()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/google/android/gms/internal/zzbnp;->zzgkb:Ljava/lang/String;

    goto :goto_0

    :cond_5
    invoke-virtual {p1}, Lcom/google/android/gms/internal/zzegf;->zzcbz()I

    move-result v0

    iput v0, p0, Lcom/google/android/gms/internal/zzbnp;->versionCode:I

    goto :goto_0

    :cond_6
    return-object p0
.end method

.method public final zza(Lcom/google/android/gms/internal/zzegg;)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    iget v0, p0, Lcom/google/android/gms/internal/zzbnp;->versionCode:I

    const/4 v1, 0x1

    invoke-virtual {p1, v1, v0}, Lcom/google/android/gms/internal/zzegg;->zzu(II)V

    iget-object v0, p0, Lcom/google/android/gms/internal/zzbnp;->zzgkb:Ljava/lang/String;

    const/4 v1, 0x2

    invoke-virtual {p1, v1, v0}, Lcom/google/android/gms/internal/zzegg;->zzl(ILjava/lang/String;)V

    iget-wide v0, p0, Lcom/google/android/gms/internal/zzbnp;->zzgkc:J

    const/4 v2, 0x3

    invoke-virtual {p1, v2, v0, v1}, Lcom/google/android/gms/internal/zzegg;->zzd(IJ)V

    iget-wide v0, p0, Lcom/google/android/gms/internal/zzbnp;->zzgjz:J

    const/4 v2, 0x4

    invoke-virtual {p1, v2, v0, v1}, Lcom/google/android/gms/internal/zzegg;->zzd(IJ)V

    iget v0, p0, Lcom/google/android/gms/internal/zzbnp;->zzgkd:I

    const/4 v1, -0x1

    if-eq v0, v1, :cond_0

    const/4 v0, 0x5

    iget v1, p0, Lcom/google/android/gms/internal/zzbnp;->zzgkd:I

    invoke-virtual {p1, v0, v1}, Lcom/google/android/gms/internal/zzegg;->zzu(II)V

    :cond_0
    invoke-super {p0, p1}, Lcom/google/android/gms/internal/zzegi;->zza(Lcom/google/android/gms/internal/zzegg;)V

    return-void
.end method

.method protected final zzn()I
    .locals 4

    invoke-super {p0}, Lcom/google/android/gms/internal/zzegi;->zzn()I

    move-result v0

    iget v1, p0, Lcom/google/android/gms/internal/zzbnp;->versionCode:I

    const/4 v2, 0x1

    invoke-static {v2, v1}, Lcom/google/android/gms/internal/zzegg;->zzv(II)I

    move-result v1

    add-int/2addr v0, v1

    iget-object v1, p0, Lcom/google/android/gms/internal/zzbnp;->zzgkb:Ljava/lang/String;

    const/4 v2, 0x2

    invoke-static {v2, v1}, Lcom/google/android/gms/internal/zzegg;->zzm(ILjava/lang/String;)I

    move-result v1

    add-int/2addr v0, v1

    iget-wide v1, p0, Lcom/google/android/gms/internal/zzbnp;->zzgkc:J

    const/4 v3, 0x3

    invoke-static {v3, v1, v2}, Lcom/google/android/gms/internal/zzegg;->zzf(IJ)I

    move-result v1

    add-int/2addr v0, v1

    iget-wide v1, p0, Lcom/google/android/gms/internal/zzbnp;->zzgjz:J

    const/4 v3, 0x4

    invoke-static {v3, v1, v2}, Lcom/google/android/gms/internal/zzegg;->zzf(IJ)I

    move-result v1

    add-int/2addr v0, v1

    iget v1, p0, Lcom/google/android/gms/internal/zzbnp;->zzgkd:I

    const/4 v2, -0x1

    if-eq v1, v2, :cond_0

    const/4 v1, 0x5

    iget v2, p0, Lcom/google/android/gms/internal/zzbnp;->zzgkd:I

    invoke-static {v1, v2}, Lcom/google/android/gms/internal/zzegg;->zzv(II)I

    move-result v1

    add-int/2addr v0, v1

    :cond_0
    return v0
.end method
